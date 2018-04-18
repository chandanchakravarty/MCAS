
/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_GET_CHECKSUM_SUMMARY]    Script Date: 12/02/2011 16:43:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MIG_IL_GET_CHECKSUM_SUMMARY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MIG_IL_GET_CHECKSUM_SUMMARY]
GO

/****** Object:  StoredProcedure [dbo].[PROC_MIG_IL_GET_CHECKSUM_SUMMARY]    Script Date: 12/02/2011 16:43:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- MIG_IL_POLICY_RISK_BENIFICIARY_DETAILS  
/*----------------------------------------------------------                                                            
Proc Name             : Dbo.[PROC_MIG_IL_GET_CHECKSUM_SUMMARY]                                                            
Created by            : Santosh Kumar Gautam                                                           
Date                  : 15 SEPT 2011                                                
Purpose               : GET CHECKSUM SUMMARY        
Revison History       :                                                            
Used In               : INITIAL LOAD               
------------------------------------------------------------                                                            
Date     Review By          Comments                               
                      
drop Proc [PROC_MIG_IL_GET_CHECKSUM_SUMMARY] 860,1--860,1                                                  
------   ------------       -------------------------*/                                                            
                          
CREATE PROCEDURE [dbo].[PROC_MIG_IL_GET_CHECKSUM_SUMMARY]                     
 @IMPORT_REQUEST_ID        INT,        
 @LANG_ID                  INT=1            
             
AS                                
BEGIN                         
                    
                    
   SELECT ISNULL(LKPM.LOOKUP_VALUE_DESC, LKP.LOOKUP_VALUE_DESC) AS FILE_TYPE,M_FILE.DISPLAY_FILE_NAME,        
   ----------------------------------------------        
   -- FOR FAILED RECORD COUNT        
   ----------------------------------------------        
  ISNULL(CASE         
  --- FOR CUSTOMER        
  WHEN M_FILE.IMPORT_FILE_TYPE=14936 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CUSTOMER_DETAILS    WITH(NOLOCK) WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)        
  --- FOR COAPPLICANT        
  WHEN M_FILE.IMPORT_FILE_TYPE=14937 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CO_APPLICANT_DETAILS WITH(NOLOCK) WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)        
  --- FOR CONTACT        
  WHEN M_FILE.IMPORT_FILE_TYPE=14938 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CONTACT_DETAILS    WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)        
         
  --- FOR POLICY        
  WHEN M_FILE.IMPORT_FILE_TYPE=14939 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_DETAILS   WITH(NOLOCK) WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)        
  --- FOR REMUNARATION        
  WHEN M_FILE.IMPORT_FILE_TYPE=14940 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_REMUNERATION_DETAILS   WITH(NOLOCK)   WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)        
  --- FOR COISURANCE        
  WHEN M_FILE.IMPORT_FILE_TYPE=14941 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_COINSURER_DETAILS  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)        
  --- FOR REISURANCE        
  WHEN M_FILE.IMPORT_FILE_TYPE=14942 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_REINSURANCE_DETAILS    WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)        
                          
  --- FOR LOCATION        
  WHEN M_FILE.IMPORT_FILE_TYPE=14960 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_LOCATION_DETAILS  WITH(NOLOCK)    WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)        
             
   
   --- FOR coverages        
  WHEN M_FILE.IMPORT_FILE_TYPE=14968 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_RISK_COVERAGES_DETAILS    WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)        
             
        --- FOR BENIFICIARY        
  WHEN M_FILE.IMPORT_FILE_TYPE=14967 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_RISK_BENIFICIARY_DETAILS    WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)        
                  
          --- FOR BILLING        
  WHEN M_FILE.IMPORT_FILE_TYPE=14969 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_BILLING_DETAILS    WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)        
       
                
          --- FOR DISCOUNT        
  WHEN M_FILE.IMPORT_FILE_TYPE=14963 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_DISCOUNTS_DETAILS   WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)        
         --- FOR RISK DISCOUNTS DETAILS         
  WHEN M_FILE.IMPORT_FILE_TYPE=14966 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_RISK_DISCOUNTS_DETAILS   WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)        
   
         --- FOR RISK INFO        
  WHEN M_FILE.IMPORT_FILE_TYPE=15008 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_RISK_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)  
   
         --- FOR policy applicant      
  WHEN M_FILE.IMPORT_FILE_TYPE=14961 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_COAPPLICANT_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)  
  
  ---=================for claim ======================added by pradeep
  --- FOR claim activity  
  WHEN M_FILE.IMPORT_FILE_TYPE=14997 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_ACTIVITY_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)  
  
  --- FOR claim payment  
  WHEN M_FILE.IMPORT_FILE_TYPE=14998 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_PAYMENT_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)  
  
   --- FOR claim victim  
  WHEN M_FILE.IMPORT_FILE_TYPE=14999 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_VICTIM_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)  
   
   --- FOR claim Third Party Damage
  WHEN M_FILE.IMPORT_FILE_TYPE=15000 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_THIRD_PARTY_DAMAGE_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)  
  
  --- FOR claim Occurrence Detail
  WHEN M_FILE.IMPORT_FILE_TYPE=15003 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_OCCURRENCE_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)  
  
  --- FOR 15004	Litigation
  WHEN M_FILE.IMPORT_FILE_TYPE=15004 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_LITIGATION_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)  
  
  --- FOR 15005	Claim Coverages
  WHEN M_FILE.IMPORT_FILE_TYPE=15005 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_COVERAGES_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)  
 
  --- FOR 15006	Claim Coinsurance
  WHEN M_FILE.IMPORT_FILE_TYPE=15006 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_COINSURANCE_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)  
 
  --- FOR 15007	Claim Reserve
  WHEN M_FILE.IMPORT_FILE_TYPE=15007 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_RESERVE_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)  
    
   --- FOR 14944	Parties
  WHEN M_FILE.IMPORT_FILE_TYPE=14944 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_PARTIES_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)  

	--- FOR 15001	Risk Info
  WHEN M_FILE.IMPORT_FILE_TYPE=15001 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_RISK_INFO_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)  
 
 	--- FOR 15002	Payee
  WHEN M_FILE.IMPORT_FILE_TYPE=15002 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_PAYEE_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)  
 
 	--- FOR 14943	Claim Notification
  WHEN M_FILE.IMPORT_FILE_TYPE=14943 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_NOTIFICATION_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =1)  
 --==================end claim ======================
           
  END,0) AS FAILED_RECORDS,        
          
          
   ----------------------------------------------        
   -- FOR SUCCESS RECORD COUNT        
   ----------------------------------------------        
  ISNULL(CASE         
  --- FOR CUSTOMER        
  WHEN M_FILE.IMPORT_FILE_TYPE=14936 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CUSTOMER_DETAILS  WITH(NOLOCK)   WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)        
  --- FOR COAPPLICANT        
  WHEN M_FILE.IMPORT_FILE_TYPE=14937 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CO_APPLICANT_DETAILS WITH(NOLOCK) WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)        
  --- FOR CONTACT        
  WHEN M_FILE.IMPORT_FILE_TYPE=14938 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CONTACT_DETAILS     WITH(NOLOCK) WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)        
         
  --- FOR POLICY        
  WHEN M_FILE.IMPORT_FILE_TYPE=14939 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_DETAILS  WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)        
  --- FOR REMUNARATION        
  WHEN M_FILE.IMPORT_FILE_TYPE=14940 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_REMUNERATION_DETAILS    WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)        
  --- FOR COISURANCE        
  WHEN M_FILE.IMPORT_FILE_TYPE=14941 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_COINSURER_DETAILS      WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)        
  --- FOR REISURANCE        
  WHEN M_FILE.IMPORT_FILE_TYPE=14942 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_REINSURANCE_DETAILS    WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)        
                          
  --- FOR LOCATION        
  WHEN M_FILE.IMPORT_FILE_TYPE=14960 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_LOCATION_DETAILS    WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)        
        
   --- FOR coverages        
  WHEN M_FILE.IMPORT_FILE_TYPE=14968 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_RISK_COVERAGES_DETAILS    WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)        
             
        --- FOR BENIFICIARY        
  WHEN M_FILE.IMPORT_FILE_TYPE=14967 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_RISK_BENIFICIARY_DETAILS    WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)        
                  
          --- FOR BILLING        
  WHEN M_FILE.IMPORT_FILE_TYPE=14969 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_BILLING_DETAILS    WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)        
       
                
          --- FOR DISCOUNT        
  WHEN M_FILE.IMPORT_FILE_TYPE=14963 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_DISCOUNTS_DETAILS   WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)        
         --- FOR RISK DISCOUNTS DETAILS         
  WHEN M_FILE.IMPORT_FILE_TYPE=14966 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_RISK_DISCOUNTS_DETAILS   WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)        
   
           --- FOR RISK INFO        
  WHEN M_FILE.IMPORT_FILE_TYPE=15008 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_RISK_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)  
  
          --- FOR policy applicant      
  WHEN M_FILE.IMPORT_FILE_TYPE=14961 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_POLICY_COAPPLICANT_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)              
  
  ---=================for claim ======================added by pradeep
  --- FOR claim activity  
  WHEN M_FILE.IMPORT_FILE_TYPE=14997 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_ACTIVITY_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)  
  
  --- FOR claim payment  
  WHEN M_FILE.IMPORT_FILE_TYPE=14998 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_PAYMENT_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)  
  
   --- FOR claim victim  
  WHEN M_FILE.IMPORT_FILE_TYPE=14999 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_VICTIM_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)  
   
   --- FOR claim Third Party Damage
  WHEN M_FILE.IMPORT_FILE_TYPE=15000 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_THIRD_PARTY_DAMAGE_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)  
  
  --- FOR claim Occurrence Detail
  WHEN M_FILE.IMPORT_FILE_TYPE=15003 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_OCCURRENCE_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)  
  
  --- FOR 15004	Litigation
  WHEN M_FILE.IMPORT_FILE_TYPE=15004 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_LITIGATION_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)  
  
  --- FOR 15005	Claim Coverages
  WHEN M_FILE.IMPORT_FILE_TYPE=15005 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_COVERAGES_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)  
 
  --- FOR 15006	Claim Coinsurance
  WHEN M_FILE.IMPORT_FILE_TYPE=15006 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_COINSURANCE_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)  
 
  --- FOR 15007	Claim Reserve
  WHEN M_FILE.IMPORT_FILE_TYPE=15007 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_RESERVE_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)  
    
   --- FOR 14944	Parties
  WHEN M_FILE.IMPORT_FILE_TYPE=14944 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_PARTIES_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)  

	--- FOR 15001	Risk Info
  WHEN M_FILE.IMPORT_FILE_TYPE=15001 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_RISK_INFO_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)  
 
 	--- FOR 15002	Payee
  WHEN M_FILE.IMPORT_FILE_TYPE=15002 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_PAYEE_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)  
 
 	--- FOR 14943	Claim Notification
  WHEN M_FILE.IMPORT_FILE_TYPE=14943 THEN (SELECT COUNT(IMPORT_REQUEST_ID)FROM MIG_IL_CLAIM_NOTIFICATION_DETAILS WITH(NOLOCK)  WHERE IMPORT_REQUEST_ID = @IMPORT_REQUEST_ID AND HAS_ERRORS =0)  
 --==================end claim ======================
           
               
  END,0) AS SUCCESS_RECORDS        
                
 FROM MIG_IL_IMPORT_REQUEST_FILES M_FILE WITH(NOLOCK) LEFT OUTER JOIN         
 MNT_LOOKUP_VALUES LKP WITH(NOLOCK) ON M_FILE.IMPORT_FILE_TYPE=LKP.LOOKUP_UNIQUE_ID LEFT OUTER JOIN         
 MNT_LOOKUP_VALUES_MULTILINGUAL LKPM WITH(NOLOCK) ON LKP.LOOKUP_UNIQUE_ID=LKPM.LOOKUP_UNIQUE_ID AND LANG_ID=@LANG_ID         
 WHERE M_FILE.IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID        
       
       
 SELECT REQUEST_STATUS       
 FROM  MIG_IL_IMPORT_REQUEST WITH(NOLOCK)      
 WHERE IMPORT_REQUEST_ID=@IMPORT_REQUEST_ID      
         
END                                
        
 
GO
