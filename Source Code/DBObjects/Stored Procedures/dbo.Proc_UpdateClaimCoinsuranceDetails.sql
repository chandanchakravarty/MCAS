IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateClaimCoinsuranceDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateClaimCoinsuranceDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


            
 /*----------------------------------------------------------                                                          
Proc Name             : Dbo.Proc_UpdateClaimCoinsuranceDetails                                                          
Created by            : Santosh Kumar Gautam                                                         
Date                  : 14 Dec 2010                                                         
Purpose               : To update the co-insurance details                
Revison History       :                                                          
Used In               : claim module                
------------------------------------------------------------                                                          
Date     Review By          Comments                             
                    
drop Proc Proc_UpdateClaimCoinsuranceDetails                                                 
------   ------------       -------------------------*/                                                          
--                             
                              
--                           
                        
CREATE PROCEDURE [dbo].[Proc_UpdateClaimCoinsuranceDetails]                       
              
 @COINSURANCE_ID			int                     
,@CLAIM_ID					int                    
,@LEADER_SUSEP_CODE			nvarchar(50)              
,@LEADER_POLICY_NUMBER		nvarchar(50)  
,@LEADER_ENDORSEMENT_NUMBER nvarchar(50)  
,@LEADER_CLAIM_NUMBER		nvarchar(50)  
,@CLAIM_REGISTRATION_DATE   datetime   
,@LITIGATION_FILE    		int           
,@MODIFIED_BY int                
,@LAST_UPDATED_DATETIME datetime               
            
                                                                                    
                              
AS                              
BEGIN                       

   UPDATE [dbo].[CLM_CO_INSURANCE]
   SET 
       [LEADER_SUSEP_CODE]			= @LEADER_SUSEP_CODE
      ,[LEADER_POLICY_NUMBER]		= @LEADER_POLICY_NUMBER
      ,[LEADER_ENDORSEMENT_NUMBER]  = @LEADER_ENDORSEMENT_NUMBER
      ,[LEADER_CLAIM_NUMBER]		= @LEADER_CLAIM_NUMBER
      ,[CLAIM_REGISTRATION_DATE]	= @CLAIM_REGISTRATION_DATE           
      ,[MODIFIED_BY]				= @MODIFIED_BY
      ,[LAST_UPDATED_DATETIME]		= @LAST_UPDATED_DATETIME
      ,[LITIGATION_FILE]			= @LITIGATION_FILE
   WHERE (COINSURANCE_ID=@COINSURANCE_ID AND CLAIM_ID=@CLAIM_ID)
   
   
   UPDATE CLM_CLAIM_INFO
   SET    LEADER_CLAIM_NUMBER       = @LEADER_CLAIM_NUMBER
   WHERE  CLAIM_ID			        = @CLAIM_ID
   
   ---------------------------------------------------------------
   -- ADDED BY SANTOSH KR GAUTAM ON 13 JUL 2011 (REF ITRACK:1044)   
   ---------------------------------------------------------------
  DECLARE @CLAIM_LITIGATION_FILE INT
  SELECT @CLAIM_LITIGATION_FILE=LITIGATION_FILE FROM CLM_CLAIM_INFO WHERE CLAIM_ID=@CLAIM_ID
  
  IF NOT EXISTS(SELECT CLAIM_ID FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID)
  BEGIN
  
	  UPDATE CLM_PRODUCT_COVERAGES 
	  SET    LIMIT_OVERRIDE=CASE WHEN @CLAIM_LITIGATION_FILE=10963 OR @LITIGATION_FILE=10963 THEN 'Y' ELSE 'N' END
	  WHERE  CLAIM_ID=@CLAIM_ID AND IS_RISK_COVERAGE='Y'
	  
  END

  
                  
                  
END 

GO

