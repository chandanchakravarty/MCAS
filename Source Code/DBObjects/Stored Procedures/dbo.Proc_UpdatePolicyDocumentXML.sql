IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyDocumentXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyDocumentXML]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


    
       
 --DROP PROC Proc_UPDATEPolicyDocumentXML        
 -------------------------------------------------------*/         
CREATE PROCEDURE [dbo].[Proc_UpdatePolicyDocumentXML]                     
(          
 @CUSTOMER_ID		INT,        
 @POLICY_ID			INT,        
 @POLICY_VERSION_ID INT,        
 @DOC_XML			TEXT,                 
 @DOC_TYPE			VARCHAR(15) ,
 @CLAIM_ID			INT = NULL,
 @ACTIVITY_ID       INT = NULL  
)        
AS        
 BEGIN
 BEGIN TRAN     
  
    --Modified by Pradeep on 20 -july-2011 -iTrack 1383,1361
  --update print jobs table for boleto regenerate 		
  IF(UPPER(@DOC_TYPE)='BOLETO')
	  BEGIN
		UPDATE PRINT_JOBS SET IS_PROCESSED=0, ATTEMPTS=0 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND DOCUMENT_CODE=@DOC_TYPE AND IS_ACTIVE='Y'
	  END
  ELSE
	  BEGIN
		  ------------------------------------------------------
		  --- MODIFIED BY SANTOSH KUMAR GAUTAM ON 22 JUNE 2012
		  --- UPDATE PRINT JOB FOR CLAIM DOCUMENT
		  ------------------------------------------------------
		  
		  IF(@CLAIM_ID ='' OR @CLAIM_ID<0)
			 SET @CLAIM_ID  = NULL
		    
		  IF(@ACTIVITY_ID ='' OR @ACTIVITY_ID<0)
			 SET @ACTIVITY_ID= NULL
		    
		  UPDATE PRINT_JOBS SET IS_PROCESSED = 0, ATTEMPTS = 0 
		  WHERE CUSTOMER_ID			= @CUSTOMER_ID		 AND 
				POLICY_ID			= @POLICY_ID		 AND 
				POLICY_VERSION_ID	= @POLICY_VERSION_ID AND 
				DOCUMENT_CODE		= CASE WHEN @CLAIM_ID IS NULL THEN 'POLICY_DOC' ELSE  'CLM_RECEIPT' END AND
				(@CLAIM_ID IS NULL    OR CLAIM_ID	 = @CLAIM_ID) AND    
				(@ACTIVITY_ID IS NULL OR ACTIVITY_ID = @ACTIVITY_ID)    
		        
		  
		  IF(@CLAIM_ID IS NULL)
		  BEGIN
			  UPDATE ACT_PREMIUM_NOTICE_PROCCESS_LOG
			  SET DEC_CUSTOMERXML		= @DOC_XML 
			  WHERE CUSTOMER_ID		    = @CUSTOMER_ID		 AND 
					POLICY_ID			= @POLICY_ID		 AND 
					POLICY_VERSION_ID   = @POLICY_VERSION_ID AND 
					CALLED_FOR			= @DOC_TYPE
		  END
      END  
 COMMIT TRAN
 
 END 


GO

