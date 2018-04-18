
/****** Object:  StoredProcedure [dbo].[Proc_GetJobSearchListList]    Script Date: 11/21/2014 09:32:20 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetJobSearchListList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetJobSearchListList]
GO


/****** Object:  StoredProcedure [dbo].[Proc_GetJobSearchListList]    Script Date: 11/21/2014 09:32:20 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







create Proc [dbo].[Proc_GetJobSearchListList]
(
@query nvarchar(max)
)
as
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









GO

