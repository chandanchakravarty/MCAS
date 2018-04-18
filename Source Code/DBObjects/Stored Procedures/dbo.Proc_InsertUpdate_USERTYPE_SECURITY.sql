IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertUpdate_USERTYPE_SECURITY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertUpdate_USERTYPE_SECURITY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_InsertUpdate_USERTYPE_SECURITY
Created by      : Anshuman
Date            : 5/31/2005
Purpose    	: Set security rights aginst user_type_id and screen_id
Revison History :
Used In 	: BRICS
------------------------------------------------------------
Date     Review By          Comments*/

CREATE PROCEDURE Proc_InsertUpdate_USERTYPE_SECURITY
(
	@USER_TYPE_ID	smallint,
	@SCREEN_ID	varchar(20),
	@PERMISSION_XML	varchar(100),
	@IS_ACTIVE	nchar(1),
	@CREATED_BY	int,
	@CREATED_DATETIME	datetime
)
AS
BEGIN
	if exists
	(
		SELECT 
			PERMISSION_XML 
		FROM 
			MNT_USER_TYPE_PERMISSION 
		WHERE
			USER_TYPE_ID 	=	@USER_TYPE_ID AND
			SCREEN_ID	=	@SCREEN_ID
	)
	begin
		UPDATE 	
			MNT_USER_TYPE_PERMISSION
		SET	
			PERMISSION_XML		=	@PERMISSION_XML,
			MODIFIED_BY		=	@CREATED_BY,
			LAST_UPDATED_DATETIME	=	@CREATED_DATETIME
		WHERE
			USER_TYPE_ID 		=	@USER_TYPE_ID AND
			SCREEN_ID		=	@SCREEN_ID
	end
	else
	begin
		INSERT INTO	MNT_USER_TYPE_PERMISSION
		(
			USER_TYPE_ID,
			SCREEN_ID,
			PERMISSION_XML,
			IS_ACTIVE,
			CREATED_BY,
			CREATED_DATETIME
		)
		VALUES
		(
			@USER_TYPE_ID,
			@SCREEN_ID,
			@PERMISSION_XML,
			@IS_ACTIVE,
			@CREATED_BY,
			@CREATED_DATETIME
		)
	end
END



GO

