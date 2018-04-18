IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Delete_ACT_BANK_RECONCILIATION_ITEMS_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Delete_ACT_BANK_RECONCILIATION_ITEMS_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : dbo.ACT_BANK_RECONCILIATION
Created by      : Ajit Singh Chahal
Rule		: a bank recon can be committed only if fully distributed.
Date            : 6/28/2005
Purpose    	: to delete record of ACT_BANK_RECONCILIATION_ITEMS_DETAILS
Revison History :
Used In 	: Wolverine
Status values	: 
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC Dbo.Proc_Delete_ACT_BANK_RECONCILIATION_ITEMS_DETAILS
(
@IDENTITY_ROW_ID int
)as 
begin
delete from ACT_BANK_RECONCILIATION_ITEMS_DETAILS 
where IDENTITY_ROW_ID = @IDENTITY_ROW_ID
end



GO

