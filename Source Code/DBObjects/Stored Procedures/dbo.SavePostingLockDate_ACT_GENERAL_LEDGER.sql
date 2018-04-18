IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SavePostingLockDate_ACT_GENERAL_LEDGER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SavePostingLockDate_ACT_GENERAL_LEDGER]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*
CREATED BY: AJIT ON 25/07/2005
PURPOSE: To save/update 
*/
create proc SavePostingLockDate_ACT_GENERAL_LEDGER(
@FISCAL_ID int,
@POSTING_LOCK_DATE datetime
)
as
Begin
update ACT_GENERAL_LEDGER set POSTING_LOCK_DATE = @POSTING_LOCK_DATE where FISCAL_ID=@FISCAL_ID
End



GO

