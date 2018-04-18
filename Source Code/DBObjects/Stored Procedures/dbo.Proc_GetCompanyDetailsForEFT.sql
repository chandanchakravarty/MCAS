IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCompanyDetailsForEFT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCompanyDetailsForEFT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------  
Proc Name        : dbo.Proc_GetCompanyDetailsForEFT
Created by       : Ravinda Gupta 
Date             : 1-22-2007
Purpose      	 : Will fetch NACHA details for carier
Revison History :  
Used In   :Wolverine  
exec Proc_GetCompanyDetailsForEFT 'w001'
------------------------------------------------------------  
Date     Review By          Comments  
------------------------------------------------------------*/  
-- drop proc dbo.Proc_GetCompanyDetailsForEFT
CREATE PROC [dbo].[Proc_GetCompanyDetailsForEFT]	
@COMPANY_CODE 	Varchar(16)
AS
BEGIN 

	DECLARE @FISCAL_ID INt 
	DECLARE @CURR_DATE DATETIME 

	SET @CURR_DATE =  CAST(CONVERT(VARCHAR,GETDATE() ,101) AS DATETIME) 

	exec Proc_GetFiscalIDForCurrentDate @CURR_DATE, @FISCAL_ID out 

	--Ravindra(10-08-2007) Get Next EFT Batch Number 
	DECLARE @NEXT_EFT_BATCH Int
	SELECT @NEXT_EFT_BATCH = ISNULL(MAX(BATCH_NUMBER),0) + 1 FROM ACT_NACHA_UPLOAD_HISTORY	
	-- Select Bank Account ID To Be used fot EFT 

	DECLARE @EFT_BANK_ACCOUNT Int 
	SELECT @EFT_BANK_ACCOUNT = BNK_CUST_DEP_EFT_CARD
	FROM ACT_GENERAL_LEDGER
	WHERE FISCAL_ID = @FISCAL_ID 

	SELECT  AGN.AGENCY_ID AS AGENCY_ID,
		substring(AGN.AGENCY_DISPLAY_NAME ,1 , 16 ) AS COMPANY_NAME,
		@EFT_BANK_ACCOUNT AS BANK_ACCOUNT,
		BANK.TRANSIT_ROUTING_NUMBER AS TRANSIT_ROUTING_NUMBER,
		BANK.COMPANY_ID AS COMPANY_ID,
		BANK.BANK_NAME AS BANK_NAME , 
		@NEXT_EFT_BATCH AS NEXT_EFT_BATCH
	FROM 	MNT_AGENCY_LIST AGN,
		ACT_BANK_INFORMATION BANK
	WHERE AGN.AGENCY_CODE = @COMPANY_CODE
	AND BANK.ACCOUNT_ID = @EFT_BANK_ACCOUNT
 	
END



GO

