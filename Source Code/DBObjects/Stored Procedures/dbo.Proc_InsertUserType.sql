IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertUserType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertUserType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_InsertUserType
Created by      : Gaurav
Date            : 3/7/2005
Purpose         : To add record in User_Types table
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
 -- drop proc Proc_InsertUserType
CREATE PROC dbo.Proc_InsertUserType
(
@User_Type_Code	nvarchar(5),
@User_Type_Desc	nvarchar(25),
@User_Type_System	nchar(1),
@User_Type_For_Carrier	smallint,
@Is_Active		nchar(1),
@Created_By		int,
@Created_DateTime	DateTime,
@Modified_By		int,
@Last_Updated_DateTime	DateTime,
@User_Type_Id		smallint OUTPUT,
@SYSTEM_GENERATED_CODE INT

)
AS
BEGIN
	
	Declare @Count int
	Set @Count= (SELECT count(*) FROM MNT_USER_TYPES WHERE User_Type_Code =@User_Type_Code)
	if(@Count=0)
	BEGIN
		INSERT INTO MNT_USER_TYPES	(
						User_Type_Code,
						User_Type_Desc,
						User_Type_System,
						User_Type_For_Carrier,
						Is_Active,
						Created_By,
						Created_DateTime,
						Modified_By,
						Last_Updated_DateTime,
						SYSTEM_GENERATED_CODE
					 	)
			VALUES	(
						@User_Type_Code,
						@User_Type_Desc,
						@User_Type_System,
						@User_Type_For_Carrier,
						@Is_Active,
						@Created_By,
						@Created_DateTime,
						@Modified_By,
						@Last_Updated_DateTime,
						@SYSTEM_GENERATED_CODE
					)
		
		
		SELECT @User_Type_Id=Max(USER_TYPE_ID)
					FROM MNT_USER_TYPES
	END
	ELSE
	BEGIN
	/*Record already exist*/
			SELECT @User_Type_Id = -1
	
	END
END




GO

