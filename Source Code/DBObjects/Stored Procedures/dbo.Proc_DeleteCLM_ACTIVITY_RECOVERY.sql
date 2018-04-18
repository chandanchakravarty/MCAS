IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteCLM_ACTIVITY_RECOVERY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteCLM_ACTIVITY_RECOVERY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------
Proc Name       : dbo.Proc_DeleteCLM_ACTIVITY_RECOVERY
Created by      : Vijay Arora
Date            : 5/26/2006
Purpose    	: To delete the values from table CLM_ACTIVITY_RECOVERY
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC dbo.Proc_DeleteCLM_ACTIVITY_RECOVERY
(
    @CLAIM_ID  int,
    @RECOVERY_ID int
)
AS
BEGIN

IF EXISTS (SELECT RECOVERY_ID FROM CLM_ACTIVITY_RECOVERY WHERE CLAIM_ID = @CLAIM_ID AND RECOVERY_ID = @RECOVERY_ID)
	DELETE FROM CLM_ACTIVITY_RECOVERY WHERE CLAIM_ID = @CLAIM_ID AND RECOVERY_ID = @RECOVERY_ID
END




GO

