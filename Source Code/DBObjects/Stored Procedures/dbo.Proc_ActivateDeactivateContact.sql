IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateContact]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateContact]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





/*----------------------------------------------------------
Proc Name       : dbo.Proc_ActivateDeactivateContact
Created by      : 	Ajit Singh Chahal
Date                : 	3/15/2005
Purpose         : 	To update the record in MNT_USER_LIST table
Revison History :
Used In         :  	 Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC dbo.Proc_ActivateDeactivateContact
(
@CODE		nchar (7),
@IS_ACTIVE 	Char(1)			
)
AS
BEGIN
UPDATE MNT_CONTACT_LIST
	SET 
		IS_ACTIVE	= @IS_ACTIVE
	WHERE
		CONTACT_ID 		= @CODE
END




GO

