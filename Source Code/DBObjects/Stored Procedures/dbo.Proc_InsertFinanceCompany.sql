IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertFinanceCompany]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertFinanceCompany]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*----------------------------------------------------------
Proc Name       : dbo.Proc_FinanceCompany
Created by      : Vijay
Date            : 3/4/2005
Purpose         : To add record in Finance Company table(MNT_FINANCE_CompANY_LIST
Revison History :
Used In         :   wolvorine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC Dbo.Proc_InsertFinanceCompany
(
@CompanyCode   	nvarchar(6) 	,
@CompanyName   	nvarchar(100) 	,
@Add1     	nvarchar(100) 	,
@Add2     	nvarchar(100) 	,
@City     	nvarchar(70) 	,
@State		nvarchar(5) 	,
@Zip     	nvarchar(11) 	,
@Country     	nvarchar(10) 	,
@MainPhoneNo    nvarchar(13) 	,
@TollFreeNo     nvarchar(13) 	,
@Extension     	nvarchar(5) 	,
@Fax     	nvarchar(13) 	,
@EMail     	nvarchar(70) 	,
@Website     	nvarchar(50) 	,
@Mobile     	nvarchar(15) 	,
@Terms     	nvarchar(3) 	,
@TermsDesc     	nvarchar(1000) 	,
@Note     	nvarchar(1000) 	,
@Created_By     int 		,
@Created_Date   datetime 	,
@MODIFIED_BY	int		,
@LAST_UPDATED_DATETIME datetime ,
@CompanyId     	numeric		OUTPUT ,
@INSERTUPDATE	char(1)
)
AS
BEGIN
	if @INSERTUPDATE = 'I'
	BEGIN
		/*Inseting the record if @INSERTUPDATE parameter is I*/

		/*Checking whether the code already exists or not  */
		Declare @Count numeric
		SELECT @Count = Count(COMPANY_CODE) 
			FROM MNT_FINANCE_COMPANY_LIST
			WHERE COMPANY_CODE = @CompanyCode 

		IF @Count = 0 
		BEGIN
			INSERT INTO MNT_FINANCE_COMPANY_LIST(
				COMPANY_CODE, COMPANY_NAME, COMPANY_ADD1, COMPANY_ADD2, COMPANY_CITY,
				COMPANY_COUNTRY, COMPANY_STATE,	COMPANY_ZIP, COMPANY_MAIN_PHONE_NO,
				COMPANY_TOLL_FREE_NO, COMPANY_EXT,
				COMPANY_FAX, COMPANY_EMAIL,
				COMPANY_WEBSITE, COMPANY_MOBILE, COMPANY_TERMS,	COMPANY_TERMS_DESC,
				COMPANY_NOTE, IS_ACTIVE, CREATED_BY, CREATED_DATETIME
				)
			VALUES(
				@CompanyCode, @CompanyName, @Add1, @Add2, @City,
				@Country, @State, @Zip, @MainPhoneNo, 
				@TollFreeNo, @Extension,
				@Fax, @EMail,
				@Website, @Mobile, @Terms, @TermsDesc,
				@Note, 'Y', @Created_By, @Created_Date
				)
			/*Finding the last company id and returning it to output variable*/
			SELECT @CompanyId = Max(COMPANY_ID)
				FROM MNT_FINANCE_COMPANY_LIST
	
		END
		ELSE
		BEGIN
			/*Code already exist*/
			SELECT @CompanyId = -1
			RETURN -1
		END
	END
	ELSE
	BEGIN
		/*Updating the record if @INSERTUPDATE parameter is not I*/

		If Exists(SELECT COMPANY_CODE
			FROM MNT_FINANCE_COMPANY_LIST
			WHERE COMPANY_CODE = @CompanyCode AND COMPANY_ID <> @CompanyId)
		BEGIN
			/*Code already exists*/
			print('hi')
			return -1
		END
		ELSE
		BEGIN
			/*updating the record*/
			UPDATE MNT_FINANCE_COMPANY_LIST
			SET 
			COMPANY_CODE = @CompanyCode,
			COMPANY_NAME = @CompanyName,
			COMPANY_ADD1 = @Add1,
			COMPANY_ADD2 = @Add2,
			COMPANY_CITY = @City,
			COMPANY_COUNTRY = @Country,
			COMPANY_STATE = @State,
			COMPANY_ZIP = @Zip,
			COMPANY_MAIN_PHONE_NO = @MainPhoneNo,
			COMPANY_TOLL_FREE_NO = @TollFreeNo,
			COMPANY_EXT = @Extension,
			COMPANY_FAX = @Fax,
			COMPANY_EMAIL = @EMail,
			COMPANY_WEBSITE = @Website,
			COMPANY_MOBILE = @Mobile,
			COMPANY_TERMS = @Terms,
			COMPANY_TERMS_DESC = @TermsDesc,
			COMPANY_NOTE = @Note,
			MODIFIED_BY = @MODIFIED_BY,
			LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME
			WHERE COMPANY_ID = @CompanyId

		END

	END

	RETURN 1	
END





GO

