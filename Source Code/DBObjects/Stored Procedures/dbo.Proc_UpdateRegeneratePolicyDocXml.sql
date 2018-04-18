IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateRegeneratePolicyDocXml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateRegeneratePolicyDocXml]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
    
/*----------------------------------------------------------                                                            
Proc Name       : dbo.Proc_UpdateRegeneratePolicyDocXml                         
Created by      : Shubhanshu Pandey                                                         
Date            : 16/06/2011                                                           
Purpose         :                                                                                               
Revison History :                                                            
Used In        :                                                           
------------------------------------------------------------                                                            
Date     Review By          Comments
                                                                
 --DROP PROC Proc_UpdateRegeneratePolicyDocXml      
 -------------------------------------------------------*/       
CREATE PROCEDURE Proc_UpdateRegeneratePolicyDocXml                   
(        
  @POLICY_NUMBER NVARCHAR(150),
  --@DISPLAY_VERSION_NO SMALLINT,
  @DOC_XML TEXT,
  @DOC_TYPE VARCHAR(15)      
)      
AS
DECLARE  @CUSTOMER_ID INT, 
		 @POLICY_ID INT,      
		 @POLICY_DISP_VERSION INT,
		 @POLICY_VERSION_ID INT           
 BEGIN      
  SELECT 
		@CUSTOMER_ID = PCPL.CUSTOMER_ID,
		@POLICY_ID = PCPL.POLICY_ID,
		@POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID
		--@POLICY_DISP_VERSION = PCPL.POLICY_DISP_VERSION  
  FROM 
	POL_CUSTOMER_POLICY_LIST PCPL WITH(NOLOCK)
  WHERE 
	PCPL.POLICY_NUMBER = @POLICY_NUMBER 
	--AND
	--PCPL.POLICY_DISP_VERSION = @DISPLAY_VERSION_NO  
	
BEGIN TRAN
  UPDATE ACT_PREMIUM_NOTICE_PROCCESS_LOG SET DEC_CUSTOMERXML=@DOC_XML WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND  @DOC_TYPE ='POLICY_DOC'
  UPDATE  PRINT_JOBS SET IS_PROCESSED = 0, ATTEMPTS = 0 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND  DOCUMENT_CODE = 'POLICY_DOC'
COMMIT TRAN
  
  

 END 

GO

