IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateMNT_ENDORSMENT_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateMNT_ENDORSMENT_DETAILS]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------
Proc Name       : dbo.Proc_ActivateDeactivateMNT_ENDORSMENT_DETAILS
Created by      : 	Ajit Singh Chahal
Date                : 	3/15/2005
Purpose         : 	To update the record in MNT_ENDORSEMENT_DETAILS table
Revison History :
Used In         :  	 Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC dbo.Proc_ActivateDeactivateMNT_ENDORSMENT_DETAILS
(
@CODE		nchar (7),
@IS_ACTIVE 	Char(1)			
)
AS
BEGIN
UPDATE MNT_ENDORSMENT_DETAILS
	SET 
		IS_ACTIVE	= @IS_ACTIVE
	WHERE
		ENDORSMENT_ID 		= @CODE
END


GO

