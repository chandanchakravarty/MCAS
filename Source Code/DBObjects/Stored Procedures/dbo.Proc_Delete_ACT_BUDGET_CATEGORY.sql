IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Delete_ACT_BUDGET_CATEGORY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Delete_ACT_BUDGET_CATEGORY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------
Proc Name       : dbo.Proc_Delete_ACT_BUDGET_CATEGORY
Created by      : Vijay Arora
Date            : 10/3/2005
Purpose    	  :To Delete the budget category in table name ACT_BUDGET_CATEGORY
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_Delete_ACT_BUDGET_CATEGORY
(
@CATEGEORY_ID int
)
As
Begin
DELETE FROM ACT_BUDGET_CATEGORY WHERE CATEGEORY_ID=@CATEGEORY_ID 
END





GO

