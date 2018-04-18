CREATE PROCEDURE [dbo].[Proc_GetUserSearchList]
	@query [nvarchar](max)
WITH EXECUTE AS CALLER
AS
BEGIN

IF OBJECT_ID('tempdb..#TempTable') IS NOT NULL DROP TABLE #TempTable
SET FMTONLY OFF;
create table #TempTable
(
UserId nvarchar(max),
UserDispName nvarchar(max),
GroupCode nvarchar(max),
BranchCode nvarchar(max),
DeptCode nvarchar(max)
)
exec (@query)

SELECT UserId, UserDispName, GroupCode,BranchCode,DeptCode from #TempTable

END


