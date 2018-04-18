IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateUserType]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateUserType]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_ActivateDeactivateUserType
Created by      : Gaurav
Date            : 3/9/2005
Purpose         : To update the record in MNT_USER_TYPE table
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- drop proc dbo.Proc_ActivateDeactivateUserType
CREATE  PROC dbo.Proc_ActivateDeactivateUserType
(
@CODE  		NUMERIC(9) ,
@IS_ACTIVE 	CHAR(1)			
)
AS
	BEGIN
	IF EXISTS (SELECT USER_ID FROM MNT_USER_LIST WHERE USER_TYPE_ID = @CODE AND IS_ACTIVE = 'Y')
	BEGIN
		RETURN -1
	END
ELSE
	BEGIN
		UPDATE MNT_USER_TYPES
		SET IS_ACTIVE	= @IS_ACTIVE
		WHERE USER_TYPE_ID 		= @CODE
		RETURN 1
	END
END





GO

