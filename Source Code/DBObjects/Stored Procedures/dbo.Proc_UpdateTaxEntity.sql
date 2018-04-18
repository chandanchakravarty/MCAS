IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateTaxEntity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateTaxEntity]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






/*----------------------------------------------------------
Proc Name       : dbo.Proc_UpdateTaxEntity
Created by      : Pradeep
Date            : May 17, 2005
Purpose         : To update record in tax entity table
Revison History :
Used In         :   Wolverine

Modified BY	: Priya Arora
Modified On	: 19/07/2005
Purpose		: Increasing size of @code parameter from 4 to 6  	
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE     PROC Dbo.Proc_UpdateTaxEntity
(
	@TAX_ID int,
	@Code   	nvarchar(6) 	,
	@Name   	nvarchar(70) 	,
	@Add1     	nvarchar(70) 	,
	@Add2     	nvarchar(70) 	,
	@City     	nvarchar(40) 	,
	@State		nvarchar(5) 	,
	@Zip     	nvarchar(11) 	,
	@Country     	nvarchar(10) 	,
	@Phone     	nvarchar(13) 	,
	@Extension     	nvarchar(4) 	,
	@Fax     	nvarchar(13) 	,
	@EMail     	nvarchar(50) 	,
	@Website        nvarchar(50)      , 
	@Modified_By     int 		
)
AS

BEGIN
	/*Checking whether the code already exists or not  */
	IF EXISTS
	(
		SELECT * FROM MNT_TAX_ENTITY_LIST
		WHERE TAX_ID <> @TAX_ID
		AND TAX_CODE = @Code
	)
	BEGIN
		RETURN -1
	END

	UPDATE MNT_TAX_ENTITY_LIST
	SET	TAX_CODE = @Code,
		TAX_NAME = @Name,
		TAX_ADDRESS1 = @Add1,
		TAX_AddRESS2 = @Add2,
		TAX_CITY = @City,
	        TAX_STATE = @State,
		TAX_ZIP = @Zip,
		TAX_COUNTRY = @Country,
		TAX_PHONE = @Phone,
		TAX_EXT = @Extension,
		TAX_FAX = @Fax,
		TAX_EMAIL = @EMail,
                TAX_WEBSITE = @Website,
                IS_ACTIVE = 'Y',
		MODIFIED_BY = @Modified_By,
		LASt_UPDATED_DATETIME = GetDate() 
	WHERE TAX_ID = @TAX_ID
	
	RETURN 1
	END
	










GO

