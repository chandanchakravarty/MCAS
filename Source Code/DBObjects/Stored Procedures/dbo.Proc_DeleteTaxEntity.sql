IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteTaxEntity]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteTaxEntity]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--sp_helptext Proc_DeleteCompany

/*----------------------------------------------------------
Proc Name       : dbo.Proc_DeleteTaxEntity
Created by      : GAurav
Date            : 23 Jun,2005
Purpose         : To delete record from MNT_TAX_ENTITY_LIST Table
Revison History :
Used In         :   wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_DeleteTaxEntity
(

	@TaxId	INT
)
AS
BEGIN
	DELETE FROM MNT_TAX_ENTITY_LIST
	WHERE TAX_ID = @TaxId
END








GO

