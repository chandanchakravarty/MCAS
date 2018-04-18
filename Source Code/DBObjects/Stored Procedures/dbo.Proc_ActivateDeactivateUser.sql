IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateUser]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateUser]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------
Proc Name       : dbo.Proc_ActivateDeactivateUser
Created by      : Gaurav
Date            : 3/15/2005
Purpose         : To update the record in MNT_USER_LIST table
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE  PROC dbo.Proc_ActivateDeactivateUser
(
@CODE  		NUMERIC(9) ,
@Is_Active 	Char(1)			
)
AS
BEGIN
UPDATE MNT_USER_LIST
	SET 
		Is_Active	= @Is_Active
	WHERE
		USER_ID 		= @CODE
END



GO

