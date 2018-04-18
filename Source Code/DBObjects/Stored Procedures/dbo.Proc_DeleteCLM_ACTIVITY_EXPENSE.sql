IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteCLM_ACTIVITY_EXPENSE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteCLM_ACTIVITY_EXPENSE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_DeleteCLM_ACTIVITY_EXPENSE
Created by      : Vijay Arora
Date            : 5/31/2006
Purpose    	: To delete the record in table named CLM_ACTIVITY_EXPENSE
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_DeleteCLM_ACTIVITY_EXPENSE
(
	@CLAIM_ID int,
	@EXPENSE_ID int
)
As
Begin
DELETE FROM CLM_ACTIVITY_EXPENSE WHERE CLAIM_ID=@CLAIM_ID AND EXPENSE_ID=@EXPENSE_ID 
END


GO

