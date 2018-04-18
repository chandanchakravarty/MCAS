IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertClaimCoinsuranceDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertClaimCoinsuranceDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------                                                              
Proc Name             : Dbo.Proc_InsertClaimCoinsuranceDetails                                                              
Created by            : Santosh Kumar Gautam                                                             
Date                  : 14 Dec 2010                                                            
Purpose               : To insert the co-insurance details                    
Revison History       :                                                              
Used In               : claim module                    
------------------------------------------------------------                                                              
Date     Review By          Comments                                 
                        
drop Proc Proc_InsertClaimCoinsuranceDetails                                                     
------   ------------       -------------------------*/                                                              
                            
CREATE PROCEDURE [dbo].[Proc_InsertClaimCoinsuranceDetails]                                  
                   
 @COINSURANCE_ID			int output                    
,@CLAIM_ID					int                    
,@LEADER_SUSEP_CODE			nvarchar(50)              
,@LEADER_POLICY_NUMBER		nvarchar(50)  
,@LEADER_ENDORSEMENT_NUMBER nvarchar(50)  
,@LEADER_CLAIM_NUMBER		nvarchar(50) 
,@LITIGATION_FILE    		int 
,@CLAIM_REGISTRATION_DATE   datetime             
,@CREATED_BY				int              
,@CREATED_DATETIME			datetime              
              
                                  
AS                                  
BEGIN                       
                      
  SELECT @COINSURANCE_ID=(ISNULL(MAX([COINSURANCE_ID]),0)+1)  FROM [dbo].[CLM_CO_INSURANCE]              
 
 INSERT INTO [dbo].[CLM_CO_INSURANCE]
           (
            [COINSURANCE_ID]              
           ,[CLAIM_ID]
           ,[LEADER_SUSEP_CODE]
           ,[LEADER_POLICY_NUMBER]
           ,[LEADER_ENDORSEMENT_NUMBER]
           ,[LEADER_CLAIM_NUMBER]
           ,[CLAIM_REGISTRATION_DATE]
           ,[IS_ACTIVE]
           ,[CREATED_BY]
           ,[CREATED_DATETIME]
           ,[LITIGATION_FILE]
           )
     VALUES
           (
            @COINSURANCE_ID
           ,@CLAIM_ID
           ,@LEADER_SUSEP_CODE
           ,@LEADER_POLICY_NUMBER
           ,@LEADER_ENDORSEMENT_NUMBER
           ,@LEADER_CLAIM_NUMBER
           ,@CLAIM_REGISTRATION_DATE
           ,'Y'
           ,@CREATED_BY
           ,@CREATED_DATETIME
           ,@LITIGATION_FILE
           )
   ---------------------------------------------------------------
   -- ADDED BY SANTOSH KR GAUTAM ON 13 JUL 2011 (REF ITRACK:1044)   
   ---------------------------------------------------------------
  DECLARE @CLAIM_LITIGATION_FILE INT
  SELECT @CLAIM_LITIGATION_FILE=LITIGATION_FILE FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID
  
  
  UPDATE CLM_PRODUCT_COVERAGES 
  SET    LIMIT_OVERRIDE=CASE WHEN @CLAIM_LITIGATION_FILE=10963 OR @LITIGATION_FILE=10963 THEN 'Y' ELSE 'N' END
  WHERE  CLAIM_ID=@CLAIM_ID AND IS_RISK_COVERAGE='Y'
 
                      
END 

GO

