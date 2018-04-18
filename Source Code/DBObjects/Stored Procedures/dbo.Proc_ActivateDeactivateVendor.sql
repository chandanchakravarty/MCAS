IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateVendor]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateVendor]
GO

SET ANSI_NULLS ON
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
CREATE   PROC dbo.Proc_ActivateDeactivateVendor
(
@CODE		nchar (7),
@IS_ACTIVE 	Char(1)			
)
AS
BEGIN
UPDATE MNT_VENDOR_LIST
	SET 
		IS_ACTIVE	= @IS_ACTIVE
	WHERE
		VENDOR_ID 		= @CODE
END





GO

