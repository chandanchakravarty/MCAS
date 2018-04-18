IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteCLM_PAYEE]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteCLM_PAYEE]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_DeleteCLM_PAYEE
Created by      : Vijay Arora
Date            : 6/1/2006
Purpose    	: To delete the record from table named CLM_PAYEE
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_DeleteCLM_PAYEE
(
	@CLAIM_ID int,
	@PAYEE_ID int
)
As
Begin
DELETE FROM CLM_PAYEE WHERE CLAIM_ID=@CLAIM_ID AND PAYEE_ID=@PAYEE_ID 
END







GO

