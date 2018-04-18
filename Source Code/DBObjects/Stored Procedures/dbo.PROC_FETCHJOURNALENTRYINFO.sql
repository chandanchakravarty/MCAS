IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_FETCHJOURNALENTRYINFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_FETCHJOURNALENTRYINFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : Proc_FetchJournalEntryInfo  
Created by      : Uday Shanker  
Date            : 23-Jan-2008  
Purpose     : Get values in Trans log Details
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--drop proc dbo.Proc_FetchJournalEntryInfo  
CREATE PROC DBO.PROC_FETCHJOURNALENTRYINFO  
(
@TYPE VARCHAR(20),
@REGARDING_ID INT, --IT CUD BE CUST,VEN OR AGEN
@INSURED_ID INT = NULL --OPTIONAL IN CASE OF AGENCY
)
AS
BEGIN

	DECLARE @REGARDING_NAME VARCHAR(150)
	DECLARE @INSURED_NAME VARCHAR(150)
	SET @REGARDING_NAME = ''
	SET @INSURED_NAME = ''



	IF(@TYPE='VEN')
	BEGIN
		SELECT  COMPANY_NAME AS REGARDING_NAME 
		FROM MNT_VENDOR_LIST WITH(NOLOCK)
		WHERE VENDOR_ID=@REGARDING_ID
	END

	IF(@TYPE='CUS')
	BEGIN	
		SELECT  ISNULL(CUSTOMER_FIRST_NAME,'') +  ' ' + 
		ISNULL(CUSTOMER_MIDDLE_NAME,'') + ' ' + 
		ISNULL(CUSTOMER_LAST_NAME, '') AS REGARDING_NAME 
		FROM CLT_CUSTOMER_LIST  WITH(NOLOCK)
		WHERE CUSTOMER_ID=@REGARDING_ID
	END

	IF (@TYPE='AGN')
	BEGIN
		SELECT @REGARDING_NAME  = AGENCY_DISPLAY_NAME FROM MNT_AGENCY_LIST WHERE AGENCY_ID=@REGARDING_ID
		SELECT  @INSURED_NAME  = 
		ISNULL(CUSTOMER_FIRST_NAME,'') +  ' ' + 
		ISNULL(CUSTOMER_MIDDLE_NAME,'') + ' ' + 
		ISNULL(CUSTOMER_LAST_NAME, '') 
		FROM CLT_CUSTOMER_LIST WITH(NOLOCK)
		WHERE CUSTOMER_ID=@INSURED_ID
	
		SELECT @REGARDING_NAME + '^' + @INSURED_NAME AS REGARDING_NAME
		
	
	END
END










GO

