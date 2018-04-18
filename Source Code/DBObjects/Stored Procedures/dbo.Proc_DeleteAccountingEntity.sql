IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteAccountingEntity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteAccountingEntity]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_DeleteAccountingEntity
Created by      : Priya
Date            : 10 Jun,2005
Purpose         : To delete record from Accounting Entity table
Revison History :
Used In         :   wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_DeleteAccountingEntity
(

	@RecId	INT
)
AS
BEGIN
	DELETE FROM MNT_ACCOUNTING_ENTITY_LIST
	WHERE REC_ID = @RecId
END



GO

