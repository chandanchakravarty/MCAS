IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Save_ACT_BANK_RECONCILIATION_ITEMS_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Save_ACT_BANK_RECONCILIATION_ITEMS_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : dbo.ACT_BANK_RECONCILIATION
Created by      : Ajit Singh Chahal
Rule		: 
Date            : 6/28/2005
Purpose    	: to copy records from posting to 
Revison History :
Used In 	: Wolverine
Status values	: 
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
create   PROC Dbo.Proc_Save_ACT_BANK_RECONCILIATION_ITEMS_DETAILS
(
@IDENTITY_ROW_ID int,
@AC_RECONCILIATION_ID int
)as 
begin
insert into ACT_BANK_RECONCILIATION_ITEMS_DETAILS
select IDENTITY_ROW_ID,@AC_RECONCILIATION_ID from ACT_ACCOUNTS_POSTING_DETAILS 
where IDENTITY_ROW_ID = @IDENTITY_ROW_ID
end



GO

