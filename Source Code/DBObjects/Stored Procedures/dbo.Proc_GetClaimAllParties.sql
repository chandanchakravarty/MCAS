IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimAllParties]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimAllParties]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


 /*----------------------------------------------------------        
Proc Name       : dbo.Proc_GetClaimAllParties         
Created by      : Vijay Arora        
Date            : 5/31/2006        
Purpose     : To get the Claim Parties        
Revison History :        
Used In  : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
--drop proc  Proc_GetClaimAllParties 919,3,14896 
CREATE PROC [dbo].[Proc_GetClaimAllParties]        
(        
 @CLAIM_ID         int  ,    
 @ACTIVITY_ID      int ,
 @RECOVERY_TYPE    INT -- ADDED BY SANTOSH GAUTAM ON 05 APRIL 2011(ITRACK:1025)
)        
AS        
BEGIN        
     
--SELECT CAST(PARTY_ID AS VARCHAR) + '^' + ISNULL((SELECT CAST(ISNULL(REQ_SPECIAL_HANDLING,10964) AS VARCHAR) FROM    
--CLM_EXPERT_SERVICE_PROVIDERS WHERE EXPERT_SERVICE_VENDOR_CODE = VENDOR_CODE ) ,'10964') AS PARTY_ID ,    
-- [NAME] FROM CLM_PARTIES    
 

 
  -- IF ACTIVITY IS RECOVERY TYPE THEN SHOW ONLY COI & RI PARTIES 
 IF ( 11776=(SELECT ACTIVITY_REASON  FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID))
 BEGIN
    
    IF(@RECOVERY_TYPE=14896) -- RECOVERY IS TYPE IS NORMAL THEN SHOW RI & COI PARTIES
    BEGIN
      SELECT   
	       CAST(PARTY_ID AS VARCHAR) + '^' +   
           CASE WHEN (SELECT CAST(ISNULL(REQ_SPECIAL_HANDLING,'10964') AS VARCHAR)  
		   FROM  CLM_EXPERT_SERVICE_PROVIDERS 
		   WHERE ISNULL(EXPERT_SERVICE_VENDOR_CODE,'') = ISNULL(VENDOR_CODE,'') ) = '0 ' THEN '10964'   
       ELSE  
          ISNULL( (SELECT CAST(ISNULL(REQ_SPECIAL_HANDLING,'10964') AS VARCHAR)  
	                FROM  CLM_EXPERT_SERVICE_PROVIDERS WHERE isnull(EXPERT_SERVICE_VENDOR_CODE,'') = isnull(VENDOR_CODE,'') ),  
			       '10964')   
                  END AS  PARTY_ID  ,    
          [NAME] 
      FROM CLM_PARTIES    
       WHERE CLAIM_ID = @CLAIM_ID 
         AND IS_ACTIVE='Y' 
         -----------------------------------------------------------------------------
         -- ADDED BY SANTOSH KR GAUTAM ON 19 JUL 2011 (REF ITRACK :1263, 18 JUL NOTES)
         -----------------------------------------------------------------------------
         --AND PARTY_TYPE_ID IN(618,619)
        AND IS_BENEFICIARY = 'Y'-- changed by Aditya for itrack # 1187
     END
     ELSE
     BEGIN
     -- IF RECOVERY IS TYPE IS XOL TYPE THEN SHOW ONLY XOL REINSURANCE PARTIES
     
      SELECT   
	       CAST(PARTY_ID AS VARCHAR) + '^' +   
           CASE WHEN (SELECT CAST(ISNULL(REQ_SPECIAL_HANDLING,'10964') AS VARCHAR)  
		   FROM  CLM_EXPERT_SERVICE_PROVIDERS 
		   WHERE ISNULL(EXPERT_SERVICE_VENDOR_CODE,'') = ISNULL(VENDOR_CODE,'') ) = '0 ' THEN '10964'   
       ELSE  
          ISNULL( (SELECT CAST(ISNULL(REQ_SPECIAL_HANDLING,'10964') AS VARCHAR)  
	                FROM  CLM_EXPERT_SERVICE_PROVIDERS WHERE isnull(EXPERT_SERVICE_VENDOR_CODE,'') = isnull(VENDOR_CODE,'') ),  
			       '10964')   
                  END AS  PARTY_ID  ,    
          [NAME] 
      FROM CLM_PARTIES    
       WHERE CLAIM_ID = @CLAIM_ID 
         AND IS_ACTIVE='Y' 
         AND PARTY_TYPE_ID IN(618,619)
         AND IS_BENEFICIARY = 'Y' -- changed by Aditya for itrack # 1187
         AND SOURCE_PARTY_ID IN (
                   --- GET XOL REINSURANCE PARTY
                   SELECT M.REINSURANCE_COMPANY
				   FROM   CLM_CLAIM_INFO D LEFT OUTER JOIN
				          MNT_REINSURANCE_MAJORMINOR_PARTICIPATION M ON M.CONTRACT_ID =D.CATASTROPHE_EVENT_CODE
				   WHERE D.CLAIM_ID=@CLAIM_ID AND M.IS_ACTIVE='Y' AND D.CATASTROPHE_EVENT_CODE>0             
                 
				   )
     END
     
   
 END
 ELSE
 BEGIN
   SELECT   
	  CAST(PARTY_ID AS VARCHAR) + '^' +   
           CASE WHEN (SELECT CAST(ISNULL(REQ_SPECIAL_HANDLING,'10964') AS VARCHAR)  
		   FROM  CLM_EXPERT_SERVICE_PROVIDERS WHERE ISNULL(EXPERT_SERVICE_VENDOR_CODE,'') = ISNULL(VENDOR_CODE,'') ) = '0 ' THEN '10964'   
       ELSE  
          ISNULL( 
	      (SELECT CAST(ISNULL(REQ_SPECIAL_HANDLING,'10964') AS VARCHAR)  
	      FROM  CLM_EXPERT_SERVICE_PROVIDERS WHERE isnull(EXPERT_SERVICE_VENDOR_CODE,'') = isnull(VENDOR_CODE,'') ),  
			'10964')   
          END AS  PARTY_ID  ,    
       [NAME] FROM CLM_PARTIES    
   WHERE CLAIM_ID = @CLAIM_ID AND IS_ACTIVE='Y' 
   AND IS_BENEFICIARY = 'Y' -- changed by Aditya for itrack # 1187
  END
    
END        
        
    
    
  

GO

