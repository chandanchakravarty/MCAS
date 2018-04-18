IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPrintJobsEntryOfBoletoRePrint]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPrintJobsEntryOfBoletoRePrint]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Pradeep Kushwaha>
-- Create date: <18-July-2011>
-- Description:	<Add entry in print jobs table of boleto reprint>
-- drop proc dbo.Proc_InsertPrintJobsEntryofBoletoReprint   
-- =============================================
CREATE PROC [dbo].[Proc_InsertPrintJobsEntryOfBoletoRePrint]   
	-- Add the parameters for the stored procedure here
	@CUSTOMER_ID INT,
	@POLICY_ID INT,
	@POLICY_VERSION_ID INT ,
	@DOCUMENT_CODE NVARCHAR(50),
	@ENTITY_ID INT ,
	@CREATED_BY INT ,
	@CREATED_DATETIME DATETIME,
	@FILE_NAME NVARCHAR(400)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	
	 --Update PRINT_JOBS table to mark isactive to 'N'  based on search criteria
          
	UPDATE PRINT_JOBS SET      
		  IS_ACTIVE='N',
		  MODIFIED_BY=@CREATED_BY,      
		  LAST_UPDATED_DATETIME=@CREATED_DATETIME      
	WHERE CUSTOMER_ID=@CUSTOMER_ID 
		  AND POLICY_ID=@POLICY_ID 
		  AND POLICY_VERSION_ID=@POLICY_VERSION_ID
		  AND ENTITY_ID=@ENTITY_ID
		  AND DOCUMENT_CODE=@DOCUMENT_CODE	
		  
	--Insert record in PRINT_JOBS table 	
	INSERT INTO PRINT_JOBS (CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DOCUMENT_CODE,
				PRINT_DATETIME,URL_PATH,ONDEMAND_FLAG,PRINT_SUCCESSFUL,PRINTED_DATETIME,
				DUPLEX,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,IS_ACTIVE,
				ENTITY_TYPE,AGENCY_ID,PICKFROM,FILE_NAME,PROCESS_ID,IS_PROCESSED,ENTITY_ID,ATTEMPTS,
				CLAIM_ID,ACTIVITY_ID,GENERATED_FROM)	  
				SELECT TOP 1 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DOCUMENT_CODE,
				PRINT_DATETIME,URL_PATH,ONDEMAND_FLAG,PRINT_SUCCESSFUL,PRINTED_DATETIME,
				DUPLEX,@CREATED_BY,@CREATED_DATETIME,NULL,NULL,'Y',
				ENTITY_TYPE,AGENCY_ID,PICKFROM,@FILE_NAME,PROCESS_ID,0,ENTITY_ID,0,
				CLAIM_ID,ACTIVITY_ID,null
	FROM PRINT_JOBS	WITH(NOLOCK)		
	WHERE CUSTOMER_ID=@CUSTOMER_ID 
		  AND POLICY_ID=@POLICY_ID 
		  AND POLICY_VERSION_ID=@POLICY_VERSION_ID
		  AND ENTITY_ID=@ENTITY_ID
		  AND DOCUMENT_CODE=@DOCUMENT_CODE	
		  AND IS_ACTIVE='N' 
	
    RETURN  1      
        
    IF(@@ERROR<>0)      
		RETURN  -1    
     
END

GO

