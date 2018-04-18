
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_COMPLETE_VALIDATION_PHASE]    Script Date: 12/02/2011 16:43:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_COMPLETE_VALIDATION_PHASE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_COMPLETE_VALIDATION_PHASE]
GO



/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_COMPLETE_VALIDATION_PHASE]    Script Date: 12/02/2011 16:43:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                    
Proc Name             : Dbo.[PROC_MIG_IL_COMPLETE_VALIDATION_PHASE]                                                    
Created by            : Santosh Kumar Gautam                                                   
Date                  : 15 SEPT 2011                                        
Purpose               : TO MARK COMPLETE IMPORT REQUEST    
Revison History       :                                                    
Used In               : INITIAL LOAD       
------------------------------------------------------------                                                    
Date     Review By          Comments                       
              
drop Proc [PROC_MIG_IL_COMPLETE_VALIDATION_PHASE]    1200                                   
------   ------------       -------------------------*/                                                    
                     
                  
CREATE PROCEDURE [dbo].[PROC_MIG_IL_COMPLETE_VALIDATION_PHASE]              
 @IMPORT_REQUEST_ID        INT    
     
AS                        
BEGIN                 
            
            
    DECLARE @TOTAL_RECORDS INT =0    
    DECLARE @FAILED_RECORDS INT =0    
    DECLARE @HAS_ERRORS CHAR(1) ='N'    
            
      
	 ------------------------------------      
	 -- GET CUSTOMER DETAILS
	 ------------------------------------           
     SELECT @TOTAL_RECORDS=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)     
     FROM  MIG_IL_CUSTOMER_DETAILS  WITH(NOLOCK) 
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID    
     
     SELECT @FAILED_RECORDS = ISNULL(COUNT(IMPORT_REQUEST_ID ),0)     
     FROM  MIG_IL_CUSTOMER_DETAILS    WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND
           HAS_ERRORS=1
     
      
     ------------------------------------      
	 -- GET CO-APPLICANT DETAILS
	 ------------------------------------           
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)     
     FROM  MIG_IL_CO_APPLICANT_DETAILS  WITH(NOLOCK) 
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID    
     
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)     
     FROM  MIG_IL_CO_APPLICANT_DETAILS   WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND
           HAS_ERRORS=1  
           
     ------------------------------------      
	 -- GET CONTACT DETAILS
	 ------------------------------------           
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)     
     FROM  MIG_IL_CONTACT_DETAILS  WITH(NOLOCK) 
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID    
     
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)     
     FROM  MIG_IL_CONTACT_DETAILS WITH(NOLOCK)  
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND
           HAS_ERRORS=1  

	
      ------------------------------------        
	  -- GET POLICY DETAILS  
      ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_DETAILS  WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1       
           
           
     ------------------------------------        
	 -- GET POLICY LOCATION DETAILS  
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_LOCATION_DETAILS  WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_LOCATION_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1    
           
     ------------------------------------        
	 -- GET POLICY REMUNRATION DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_REMUNERATION_DETAILS  WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_REMUNERATION_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1 
           
            
     ------------------------------------        
	 -- GET POLICY COVERAGE DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_RISK_COVERAGES_DETAILS  WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_RISK_COVERAGES_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1 
           
     ------------------------------------        
	 -- GET POLICY COINSURANCE DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_COINSURER_DETAILS  WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_COINSURER_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1 
     
     ------------------------------------        
	 -- GET POLICY REINSURANCE DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_REINSURANCE_DETAILS  WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_REINSURANCE_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1 
                    
     ------------------------------------        
	 -- GET POLICY CLAUSES DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_CLAUSES_DETAILS  WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_CLAUSES_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1 
    
     ------------------------------------        
	 -- GET POLICY COAPPLICANT DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_COAPPLICANT_DETAILS  WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_COAPPLICANT_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1 
           
     ------------------------------------        
	 -- GET POLICY DISCOUNT DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_DISCOUNTS_DETAILS  WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_DISCOUNTS_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1 
           
     ------------------------------------        
	 -- GET POLICY RISK DISCOUNT DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_RISK_DISCOUNTS_DETAILS  WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)  
 FROM  MIG_IL_POLICY_RISK_DISCOUNTS_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1 
     
     ------------------------------------           
     -- GET POLICY RISK BENEFICIARY DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_RISK_BENIFICIARY_DETAILS  WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_RISK_BENIFICIARY_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1 
           
     ------------------------------------           
     -- GET POLICY RISK DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_RISK_DETAILS  WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_RISK_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1     
           
           
     ------------------------------------           
     -- GET POLICY BILLING DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_BILLING_DETAILS WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_POLICY_BILLING_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1  
             
        
     ------------------------------------           
     -- GET CLAIM DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_NOTIFICATION_DETAILS  WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_NOTIFICATION_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1                           
      
	 ------------------------------------           
     -- GET CLAIM COI DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_COINSURANCE_DETAILS  WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_COINSURANCE_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1    
          
     ------------------------------------           
     -- GET CLAIM ACTIVITY DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_ACTIVITY_DETAILS  WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_ACTIVITY_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1    
            
     ------------------------------------           
     -- GET CLAIM COVERAGE DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_COVERAGES_DETAILS  WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_COVERAGES_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1     
           
     ------------------------------------           
     -- GET CLAIM LITIAGTION DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_LITIGATION_DETAILS WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_LITIGATION_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1     
           
     ------------------------------------           
     -- GET CLAIM OCCURRENCE DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_OCCURRENCE_DETAILS WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_OCCURRENCE_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1     
     
     ------------------------------------           
     -- GET CLAIM PARTY DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_PARTIES_DETAILS WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_PARTIES_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1    
                       
     ------------------------------------           
     -- GET CLAIM PAYEE DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_PAYEE_DETAILS WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_PAYEE_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1   
     
     ------------------------------------           
     -- GET CLAIM PAYMENT DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_PAYMENT_DETAILS WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_PAYMENT_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1   
    
     ------------------------------------           
     -- GET CLAIM RESERVE DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_RESERVE_DETAILS WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_RESERVE_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1   
           
     ------------------------------------           
     -- GET CLAIM RISK DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_RISK_INFO_DETAILS WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_RISK_INFO_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1   
     
     ------------------------------------           
     -- GET CLAIM THIRD PARTY DAMAGE DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_THIRD_PARTY_DAMAGE_DETAILS WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_THIRD_PARTY_DAMAGE_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1   
           
     ------------------------------------           
     -- GET CLAIM VICTIM DETAILS
     ------------------------------------             
     SELECT @TOTAL_RECORDS+=ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_VICTIM_DETAILS WITH(NOLOCK)   
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID      
       
     SELECT @FAILED_RECORDS+= ISNULL(COUNT(IMPORT_REQUEST_ID ),0)       
     FROM  MIG_IL_CLAIM_VICTIM_DETAILS WITH(NOLOCK)    
     WHERE IMPORT_REQUEST_ID =@IMPORT_REQUEST_ID AND  
           HAS_ERRORS=1   
           
     IF(@FAILED_RECORDS>0)  
      SET @HAS_ERRORS='Y'    
   
    
            
    UPDATE  MIG_IL_IMPORT_REQUEST    
    SET REQUEST_STATUS		  = 'WTCHS',  --  WAITING FOR CHECKSUM VALIDATION
        IS_PROCESSED		  = 'N',    
        FAILED_RECORD_COUNT   = @FAILED_RECORDS,
        SUCCEDDED_RECORD_COUNT = ISNULL(@TOTAL_RECORDS-@FAILED_RECORDS,0),
        PROCESSED_DATE		  = GETDATE(), 
        IMPORT_RECORD_COUNT	  = @TOTAL_RECORDS   ,    
        HAS_ERRORS			  = @HAS_ERRORS        
    WHERE IMPORT_REQUEST_ID   = @IMPORT_REQUEST_ID          
           
END                        
 
GO