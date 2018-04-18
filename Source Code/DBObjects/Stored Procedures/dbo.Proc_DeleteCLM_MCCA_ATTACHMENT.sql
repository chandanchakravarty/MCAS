IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteCLM_MCCA_ATTACHMENT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteCLM_MCCA_ATTACHMENT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_DeleteCLM_MCCA_ATTACHMENT
Created by      : Vijay Arora
Date            : 8/8/2006
Purpose    	: To Delete a record from table named CLM_MCCA_ATTACHMENT
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_DeleteCLM_MCCA_ATTACHMENT
(
	@MCCA_ATTACHMENT_ID int
)
AS
BEGIN
    IF EXISTS (SELECT MCCA_ATTACHMENT_ID FROM CLM_MCCA_ATTACHMENT WHERE MCCA_ATTACHMENT_ID=@MCCA_ATTACHMENT_ID)
	DELETE FROM CLM_MCCA_ATTACHMENT WHERE MCCA_ATTACHMENT_ID=@MCCA_ATTACHMENT_ID 
END





GO

