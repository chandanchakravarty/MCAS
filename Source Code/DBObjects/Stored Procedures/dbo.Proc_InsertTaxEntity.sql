IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertTaxEntity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertTaxEntity]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------
Proc Name       : dbo.Proc_InsertTaxEntity
Created by      : Priya
Date            : 4/12/2005
Purpose         : To add record in tax entity table
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE    PROC Dbo.Proc_InsertTaxEntity
(
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
@Created_By     int 		,
@Created_DateTime   datetime 	,
@TaxEntityId numeric		= null	OUTPUT 
)
AS
BEGIN
	/*Checking whether the code already exists or not  */
	Declare @Count numeric
	SELECT @Count = Count(TAX_CODE) 
		FROM MNT_TAX_ENTITY_LIST
		WHERE TAX_CODE = @CODE 
	
	IF @Count >= 1 
	BEGIN
		/*Record already exist*/
		SELECT @TaxEntityId= -1
	END
	ELSE
	BEGIN
		INSERT INTO MNT_TAX_ENTITY_LIST(
			TAX_CODE,
			TAX_NAME,
			TAX_ADDRESS1,
			TAX_AddRESS2,
			TAX_CITY,
		        TAX_STATE,
			TAX_ZIP,
			TAX_COUNTRY,
			TAX_PHONE,
			TAX_EXT,
			TAX_FAX,
			TAX_EMAIL,
                        TAX_WEBSITE,
                        IS_ACTIVE,
			CREATED_BY,
			CREATED_DATETIME
			)
		VALUES(
			@Code,
			@Name,
			@Add1,
			@Add2,
			@City,
			@State,
			@Zip,
			@Country,
			@Phone,
			@Extension,
			@Fax,
			@EMail,
                        @Website,
                        'Y',
			@Created_By,
			@Created_DateTime
			)
		SELECT @TaxEntityId = Max(TAX_ID)
			FROM MNT_TAX_ENTITY_LIST
	END
	
END


GO

