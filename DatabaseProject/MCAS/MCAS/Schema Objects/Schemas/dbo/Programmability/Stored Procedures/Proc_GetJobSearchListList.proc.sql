CREATE PROCEDURE [dbo].[Proc_GetJobSearchListList]
	@query [nvarchar](max)
WITH EXECUTE AS CALLER
AS
BEGIN

IF OBJECT_ID('tempdb..#TempTable') IS NOT NULL DROP TABLE #TempTable
SET FMTONLY OFF;
create table #TempTable
(
    [JobId][nvarchar](50) NULL,
    [ScheduleId] [nvarchar](50) NULL,
	[FileRefNo] [nvarchar](50) NOT NULL,
	[ScheduleStartDateTime] [nvarchar](50) NULL,
	[JobStartDateTime] [nvarchar](50) NULL,
	[JobEndDateTime] [nvarchar](50) NULL,
	[Status] [nvarchar](50) NULL
)
exec (@query)

SELECT JobId,ScheduleId, FileRefNo, ScheduleStartDateTime,JobStartDateTime,JobEndDateTime,Status from #TempTable

END


