IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ConfirmACT_CURRENT_DEPOSITS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ConfirmACT_CURRENT_DEPOSITS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_InsertACT_CURRENT_DEPOSITS
Created by      : Vijay Joshi
Date            : 22/8/2005
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_ConfirmACT_CURRENT_DEPOSITS
(
	@DEPOSIT_ID int
)
AS
BEGIN

	UPDATE ACT_CURRENT_DEPOSITS
	SET IS_CONFIRM = 'Y'
	WHERE DEPOSIT_ID = @DEPOSIT_ID
	
	return 1
END





GO

