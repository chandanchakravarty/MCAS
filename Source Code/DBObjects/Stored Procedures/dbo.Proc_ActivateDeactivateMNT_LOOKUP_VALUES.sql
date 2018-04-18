IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateMNT_LOOKUP_VALUES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateMNT_LOOKUP_VALUES]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_ActivateDeactivateContact
Created by      : 	Mohit
Date                : 	3/15/2005
Purpose         : 	To update the record in MNT_USER_LIST table
Revison History :
Used In         :  	 Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC dbo.Proc_ActivateDeactivateMNT_LOOKUP_VALUES
(
@CODE	nchar (7),
@IS_ACTIVE 	Char(1)			
)
AS
BEGIN
UPDATE MNT_LOOKUP_VALUES
	SET IS_ACTIVE	= @IS_ACTIVE
	WHERE LOOKUP_UNIQUE_ID= @CODE
END


GO

