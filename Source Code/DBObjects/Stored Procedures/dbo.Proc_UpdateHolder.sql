IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateHolder]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateHolder]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





/*----------------------------------------------------------
Proc Name       : dbo.Proc_UpdateHolder
Created by      : Shrikant Bhatt
Date            : 27/4/2005
Purpose         : To modify record in holder interest table(MNT_HOLDER_INTEREST_LIST
Revison History :
Used In         :   wolvorine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE  PROC Proc_UpdateHolder
(
@HolderId    	nvarchar(6) 	,
@HolderCode   	nvarchar(6) 	,
@HolderName   	nvarchar(100) 	,
@Add1     	nvarchar(100) 	,
@Add2     	nvarchar(100) 	,
@City     	nvarchar(70) 	,
@State		nvarchar(5) 	,
@Zip     	nvarchar(11) 	,
@Country     	nvarchar(10) 	,
@MainPhoneNo    nvarchar(15) 	,
@Fax     	nvarchar(15) ,
@ModifiedBy	nvarchar(4) 
)
AS
BEGIN   
 UPDATE MNT_HOLDER_INTEREST_LIST  
 SET 
	HOLDER_CODE = @HolderCode,
	HOLDER_NAME=@HolderName,
	HOLDER_ADD1=@Add1,
	HOLDER_ADD2=@Add2,
	HOLDER_CITY=@City,
	HOLDER_COUNTRY=@Country,
	HOLDER_STATE=@State,
	HOLDER_ZIP=@Zip,
	HOLDER_MAIN_PHONE_NO=@MainPhoneNo,
	HOLDER_FAX=@Fax,
	MODIFIED_BY=@ModifiedBy,
	Last_Updated_DateTime = GetDate()	
 WHERE	HOLDER_ID = @HolderId  		

	
END



GO

