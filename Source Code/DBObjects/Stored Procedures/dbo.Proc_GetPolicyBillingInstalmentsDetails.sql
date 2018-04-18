IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyBillingInstalmentsDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyBillingInstalmentsDetails] 
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*                        
sp_find Proc_InsertPolicyPremiumItems,p                          
----------------------------------------------------------                                            
Proc Name       : dbo.Proc_GetPolicyBillingInstalmentsDetails                                  
Created by      : LALIT CHAUHAN                                
Date            : 05/20/2010                                            
Purpose         : Get Policy Premium Installment breakup details for billing                                           
Revison History :                                            
Used In   : Ebix Advantage                                            
------------------------------------------------------------                                            
Date     Review By          Comments                                            
------   ------------       -------------------------                          
drop Proc Proc_GetPolicyBillingInstalmentsDetails                      
Proc_GetPolicyBillingInstalmentsDetails 28070,360,4,2      
*/                        
CREATE Proc [dbo].[Proc_GetPolicyBillingInstalmentsDetails]                         
(                        
@CUSTOMER_ID INT,                        
@POLICY_ID INT,                        
@POLICY_VERSION_ID SMALLINT  ,                
@LANG_ID  INT = 1             
)                        
AS                         
BEGIN                
 DECLARE @FLAG INT                
 DECLARE @CURRENT_TERM INT                
 DECLARE @OPEN_POLICY INT = 14560   
                   
 DECLARE @TRANSACTION_TYPE INT     
  SET  @TRANSACTION_TYPE=(SELECT TRANSACTION_TYPE from POL_CUSTOMER_POLICY_LIST where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID )                   
 SELECT @CURRENT_TERM = CURRENT_TERM FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                 
IF EXISTS(SELECT COMPLETED_DATETIME FROM POL_POLICY_PROCESS  WITH(NOLOCK)                
       WHERE CUSTOMER_ID = @CUSTOMER_ID AND  NEW_POLICY_ID = @POLICY_ID AND  PROCESS_STATUS='COMPLETE')                
   BEGIN                
   SELECT @FLAG =1                
   END                
   ELSE IF EXISTS(SELECT LAST_UPDATED_DATETIME FROM ACT_POLICY_INSTALLMENT_DETAILS  WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND LAST_UPDATED_DATETIME IS NOT NULL )                
                
   BEGIN                
    SELECT @FLAG =1                
   END                    
          
           --Applied condition to show billing detail of co-applicant selected on endorsement screen, itrack 629
           CREATE TABLE #TempPolicyVersions(POLICY_VERSION INT)  
   
 IF(@TRANSACTION_TYPE <> 14560)  
 INSERT INTO #TempPolicyVersions SELECT  DISTINCT POLICY_VERSION_ID FROM ACT_POLICY_INSTALLMENT_DETAILS (NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID= @POLICY_ID   
    ELSE                 
    INSERT INTO #TempPolicyVersions(POLICY_VERSION) VALUES (@POLICY_VERSION_ID) --SELECT  DISTINCT POLICY_VERSION_ID FROM ACT_POLICY_INSTALLMENT_DETAILS WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID= @POLICY_ID  AND POLICY_VERSION_ID = @POLICY_VERSION_ID
          
     SELECT      
     PROCESS.PROCESS_ID       ,      
          INSTALL_DETAILS.CUSTOMER_ID AS CUSTOMER_ID,                
          INSTALL_DETAILS.POLICY_ID AS POLICY_ID,                
          INSTALL_DETAILS.POLICY_VERSION_ID AS POLICY_VERSION_ID,                
     INSTALL_DETAILS.INSTALLMENT_NO AS INSTALLMENT_NO,                     
     PLAN_DATA.PLAN_ID AS PLAN_ID,                
     CASE WHEN @LANG_ID = 2 THEN            
        CASE WHEN PLAN_DATA.TRAN_TYPE IN ('NBS','REW') THEN            
        'EMI'            
        ELSE PLAN_DATA.TRAN_TYPE            
        END            
      ELSE             
     PLAN_DATA.TRAN_TYPE            
     END AS TRAN_TYPE,                
      ENDO_NO = CASE                  
     WHEN PLAN_DATA.TRAN_TYPE IN ('END')    
     THEN                 
    (ISNULL(CONVERT(NVARCHAR(5),ENDO.ENDORSEMENT_NO),'')+ '.0')                 
     WHEN PLAN_DATA.TRAN_TYPE IN ('CAN')    
     THEN     
     ''    
     ELSE                     
     CONVERT(NVARCHAR(5),ENDO.ENDORSEMENT_NO)                
     END                 
     ,                         
     INSTALLMENT_EFFECTIVE_DATE = case                 
      WHEN @FLAG =1                
      THEN                
       INSTALL_DETAILS.INSTALLMENT_EFFECTIVE_DATE                 
      WHEN PLAN_DATA.TRAN_TYPE = 'END'                
      THEN                
       INSTALL_DETAILS.INSTALLMENT_EFFECTIVE_DATE                 
      ELSE                
       INSTALL_DETAILS.INSTALLMENT_EFFECTIVE_DATE                 
      END,                
            
     INSTALL_DETAILS.INSTALLMENT_AMOUNT AS INSTALLMENT_AMOUNT,                        
     INSTALL_DETAILS.INTEREST_AMOUNT AS INTEREST_AMOUNT,          
     INSTALL_DETAILS.FEE AS FEE,                        
     INSTALL_DETAILS.TAXES AS TAXES,                    
     INSTALL_DETAILS.TOTAL AS TOTAL,                      
     PLAN_DATA.TOTAL_CHANGE_INFORCE_PRM ,                  
     PLAN_DATA.TOTAL_INFO_PRM,                     
     PLAN_DATA.PRM_DIST_TYPE,                
     PLAN_DATA.TOTAL_PREMIUM AS TOTAL_PREMIUM,                             
     PLAN_DATA.TOTAL_TRAN_PREMIUM AS TOTAL_TRAN_PREMIUM,                 
     PLAN_DATA.TOTAL_INTEREST_AMOUNT AS TOTAL_INTEREST_AMOUNT,                
     PLAN_DATA.TOTAL_TRAN_INTEREST_AMOUNT AS TOTAL_TRAN_INTEREST_AMOUNT,                   
     PLAN_DATA.TOTAL_FEES AS TOTAL_FEES,                      
     PLAN_DATA.TOTAL_TRAN_FEES AS TOTAL_TRAN_FEES,                     
     PLAN_DATA.TOTAL_TAXES AS TOTAL_TAXES,                        
     PLAN_DATA.TOTAL_TRAN_TAXES AS TOTAL_TRAN_TAXES,                  
     PLAN_DATA.TOTAL_AMOUNT AS TOTAL_AMOUNT,                
     PLAN_DATA.TOTAL_TRAN_AMOUNT AS TOTAL_TRAN_AMOUNT,                  
     PLAN_DATA.TOTAL_STATE_FEES AS TOTAL_STATE_FEES,                  
     PLAN_DATA.TOTAL_TRAN_STATE_FEES AS TOTAL_TRAN_STATE_FEES,                  
     PLAN_DATA.MODE_OF_PAYMENT AS MODE_OF_PAYMENT ,                
     PLAN_DATA.MODE_OF_DOWN_PAYMENT AS MODE_OF_DOWN_PAYMENT,                
     CASE WHEN INSTALL_DETAILS.RELEASED_STATUS = 'N'               
     THEN '' ELSE CASE WHEN INSTALL_DETAILS.RELEASED_STATUS = 'Y' and  @LANG_ID='2' Then 'P' Else  INSTALL_DETAILS.RELEASED_STATUS END End
     RELEASED_STATUS,                  
     INSTALL_DETAILS.CURRENT_TERM AS CURRENT_TERM,                
    (ISNULL(CLT_APPLICANT_LIST.FIRST_NAME,'') +' '+ ISNULL(CLT_APPLICANT_LIST.LAST_NAME,'') ) AS CO_APPLICANT_NAME,                
     INSTALL_DETAILS.ROW_ID AS ROW_ID,                
     INSTALL_DETAILS.CO_APPLICANT_ID AS CO_APPLICANT_ID,                
     INSTALL_DETAILS.BOLETO_NO as BOLETO_NO ,              
     INSTALL_DETAILS.RECEIVED_DATE as RECEIVED_DATE  --Added By Lalit Dec 15,2010              
                   
     FROM ACT_POLICY_INSTALLMENT_DETAILS INSTALL_DETAILS WITH(NOLOCK)                    
     JOIN POL_CUSTOMER_POLICY_LIST POL WITH(NOLOCK)  ON                
      POL.CUSTOMER_ID = INSTALL_DETAILS.CUSTOMER_ID AND                 
      POL.POLICY_ID = INSTALL_DETAILS.POLICY_ID AND                 
      POL.POLICY_VERSION_ID = INSTALL_DETAILS.POLICY_VERSION_ID                 
     LEFT OUTER JOIN POL_POLICY_PROCESS PROCESS WITH(NOLOCK) ON                
      INSTALL_DETAILS.CUSTOMER_ID  = PROCESS.CUSTOMER_ID  AND                 
      INSTALL_DETAILS.POLICY_ID = PROCESS.POLICY_ID   AND                 
      INSTALL_DETAILS.POLICY_VERSION_ID =PROCESS.NEW_POLICY_VERSION_ID AND                 
      PROCESS.PROCESS_STATUS <> 'ROLLBACK'      AND  PROCESS.CO_APPLICANT_ID= INSTALL_DETAILS.CO_APPLICANT_ID                
     LEFT OUTER JOIN  ACT_POLICY_INSTALL_PLAN_DATA  PLAN_DATA WITH(NOLOCK) ON                 
      PLAN_DATA.CUSTOMER_ID=INSTALL_DETAILS.CUSTOMER_ID AND                         
      PLAN_DATA.POLICY_ID=INSTALL_DETAILS.POLICY_ID AND                         
      PLAN_DATA.POLICY_VERSION_ID=INSTALL_DETAILS.POLICY_VERSION_ID                
     LEFT OUTER JOIN POL_POLICY_ENDORSEMENTS ENDO WITH(NOLOCK) ON                
      ENDO.CUSTOMER_ID = INSTALL_DETAILS.CUSTOMER_ID AND                
      ENDO.POLICY_ID = INSTALL_DETAILS.POLICY_ID AND                
      ENDO.POLICY_VERSION_ID = INSTALL_DETAILS.POLICY_VERSION_ID                          
      AND ENDO.ENDORSEMENT_NO = PROCESS.ENDORSEMENT_NO                
     LEFT OUTER JOIN  CLT_APPLICANT_LIST WITH(NOLOCK) ON                 
      INSTALL_DETAILS.CO_APPLICANT_ID = CLT_APPLICANT_LIST.APPLICANT_ID                
     WHERE INSTALL_DETAILS.CUSTOMER_ID=@CUSTOMER_ID AND INSTALL_DETAILS.POLICY_ID=@POLICY_ID                   
     
     
      AND POL.CURRENT_TERM = @CURRENT_TERM   
      AND     
	  --Applied condition to show billing detail of co-applicant selected on endorsement screen, itrack 629
      INSTALL_DETAILS.POLICY_VERSION_ID IN ( SELECT temp.POLICY_VERSION FROM    #TempPolicyVersions temp  
    )  
                  
      ORDER BY               
      --CASE WHEN POL.TRANSACTION_TYPE = @OPEN_POLICY THEN              
         --ISNULL(ENDO.ENDORSEMENT_NO,0) END DESC ,        
      -- CASE WHEN POL.TRANSACTION_TYPE = @OPEN_POLICY THEN              
         ISNULL(ENDO.ENDORSEMENT_NO,0)  DESC  ,      
          INSTALL_DETAILS.ROW_ID  asc ,       
       --CASE WHEN POL.TRANSACTION_TYPE <> @OPEN_POLICY THEN              
         --INSTALL_DETAILS.ROW_ID END ASC ,          
         INSTALL_DETAILS.CO_APPLICANT_ID      ,        
 INSTALL_DETAILS.INSTALLMENT_EFFECTIVE_DATE ASC---,TRAN_TYPE  desc               
     END       
GO

