IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteCLM_CATASTROPHE_EVENT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteCLM_CATASTROPHE_EVENT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_DeleteCLM_CATASTROPHE_EVENT
Created by      : Vijay Arora
Date            : 4/24/2006
Purpose    	: To Delete the Data from table named CLM_CATASTROPHE_EVENT
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_DeleteCLM_CATASTROPHE_EVENT
(
	@CATASTROPHE_EVENT_ID int
)
As
Begin
DELETE FROM CLM_CATASTROPHE_EVENT WHERE CATASTROPHE_EVENT_ID=@CATASTROPHE_EVENT_ID 
END



GO

