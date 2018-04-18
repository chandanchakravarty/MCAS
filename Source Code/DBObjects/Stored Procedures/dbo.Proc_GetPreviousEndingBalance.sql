IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPreviousEndingBalance]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPreviousEndingBalance]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE   PROC Dbo.Proc_GetPreviousEndingBalance
(
@ACCOUNT_ID int
)as 
begin
select ENDING_BALANCE from ACT_BANK_RECONCILIATION
where ACCOUNT_ID = @ACCOUNT_ID and
isnull(IS_COMMITED,'N')='Y' and
STATEMENT_DATE=(select max(STATEMENT_DATE) from ACT_BANK_RECONCILIATION where  ACCOUNT_ID = @ACCOUNT_ID and isnull(IS_COMMITED,'N')='Y')
end



GO

