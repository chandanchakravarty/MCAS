IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertHolder]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertHolder]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_InsertHolder
Created by      : Vijay
Date            : 3/4/2005
Purpose         : To add record in holder interest table(MNT_HOLDER_INTEREST_LIST
Revison History :
Used In         :   wolvorine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Proc_InsertHolder
(
@HolderCode   	nvarchar(6) 	,
@HolderName   	nvarchar(200) 	,
@Add1     	nvarchar(100) 	,
@Add2     	nvarchar(100) 	,
@City     	nvarchar(70) 	,
@State		nvarchar(5) 	,
@Zip     	nvarchar(11) 	,
@Country     	nvarchar(10) 	,
@MainPhoneNo    nvarchar(15) 	,
@Extension     	nvarchar(5) 	,
@Mobile     	nvarchar(15) 	,
@Fax     	nvarchar(15) 	,
@EMail     	nvarchar(70) 	,
@LegalEntity 	nvarchar(70) 	,
@Type 		nvarchar(6) 	,
@Memo		nvarchar(50)	,
@Created_By     int 		,
@Created_Date   datetime 	,
@MODIFIED_BY	int		,
@LAST_UPDATED_DATETIME datetime ,
@HolderId     	numeric		OUTPUT ,
@INSERTUPDATE	char(1)
)
AS
BEGIN
	If @INSERTUPDATE = 'I'
	BEGIN
		/*Inserting the record*/
		/*Checking whether the code already exists or not  */
		Declare @Count numeric
		SELECT @Count = Count(HOLDER_CODE) 
			FROM MNT_HOLDER_INTEREST_LIST
			WHERE HOLDER_CODE = @HolderCode 
	
		IF @Count = 0 
		BEGIN
			INSERT INTO MNT_HOLDER_INTEREST_LIST(
				HOLDER_CODE, HOLDER_NAME, HOLDER_ADD1, HOLDER_ADD2,
				HOLDER_CITY, HOLDER_COUNTRY, HOLDER_STATE, HOLDER_ZIP,
				HOLDER_MAIN_PHONE_NO, HOLDER_EXT, HOLDER_MOBILE,
				HOLDER_FAX, HOLDER_EMAIL, HOLDER_LEGAL_ENTITY, HOLDER_TYPE,
				HOLDER_MEMO, IS_ACTIVE, CREATED_BY, CREATED_DATETIME
				)
			VALUES(
				@HolderCode, @HolderName, @Add1, @Add2, 
				@City, @Country, @State, @Zip,
				@MainPhoneNo, @Extension, @Mobile,
				@Fax, @EMail, @LegalEntity, @Type,
				@Memo, 'Y', @Created_By, @Created_Date
				)
			
			/*Retreiving the maximum holder id and passing it to output param*/
			SELECT @HolderId = Max(Holder_ID)
				FROM MNT_HOLDER_INTEREST_LIST
	
		END
		ELSE
		BEGIN
			/*Record already exist*/
			SELECT @HolderId = -1
		END
	END
	ELSE
	BEGIN
		/*Updating the record*/
		If Exists(SELECT HOLDER_CODE
			FROM MNT_HOLDER_INTEREST_LIST
			WHERE HOLDER_CODE = @HolderCode AND 
			HOLDER_ID <> @HolderId)
		BEGIN
			/*Code already exits hence returning 0*/
			return 0
		END
		
		UPDATE MNT_HOLDER_INTEREST_LIST
		SET HOLDER_CODE = @HolderCode, 
			HOLDER_NAME = @HolderName, 
			HOLDER_ADD1 = @Add1, 
			HOLDER_ADD2 = @Add2,
			HOLDER_CITY = @City, 
			HOLDER_COUNTRY = @Country, 
			HOLDER_STATE = @State, 
			HOLDER_ZIP = @Zip,
			HOLDER_MAIN_PHONE_NO = @MainPhoneNo, 
			HOLDER_EXT = @Extension, 
			HOLDER_MOBILE = @Mobile,
			HOLDER_FAX = @Fax, 
			HOLDER_EMAIL = @EMail, 
			HOLDER_LEGAL_ENTITY = @LegalEntity, 
			HOLDER_TYPE = @Type,
			HOLDER_MEMO = @Memo,
			MODIFIED_BY = @MODIFIED_BY,
			LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME
		WHERE HOLDER_ID = @HolderId
		
		
	END

	
END




GO

