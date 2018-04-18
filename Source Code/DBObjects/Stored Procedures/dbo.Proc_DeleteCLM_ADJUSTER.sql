IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteCLM_ADJUSTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteCLM_ADJUSTER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : Dbo.Proc_DeleteCLM_ADJUSTER
Created by      : Amar
Date            : 4/21/2006
Purpose    	  :Evaluation
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_DeleteCLM_ADJUSTER
(
@ADJUSTER_ID int)
As
Begin
DELETE FROM CLM_ADJUSTER WHERE ADJUSTER_ID=@ADJUSTER_ID 
END



GO

