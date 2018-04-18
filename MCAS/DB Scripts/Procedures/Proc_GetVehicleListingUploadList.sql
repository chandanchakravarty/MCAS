
/****** Object:  StoredProcedure [dbo].[Proc_GetVehicleListingUploadList]    Script Date: 08/22/2014 13:28:17 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetVehicleListingUploadList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetVehicleListingUploadList]
GO


/****** Object:  StoredProcedure [dbo].[Proc_GetVehicleListingUploadList]    Script Date: 08/22/2014 13:28:17 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





create Proc [dbo].[Proc_GetVehicleListingUploadList]
(
@query nvarchar(max)
)
as
BEGIN

IF OBJECT_ID('tempdb..#TempTable') IS NOT NULL DROP TABLE #TempTable
SET FMTONLY OFF;
create table #TempTable
(
UploadFileName nvarchar(100),
UploadFileRefNo nvarchar(50),
UploadedDate datetime,
Status nvarchar(50)
)
exec (@query)

SELECT UploadFileName, UploadFileRefNo, UploadedDate,Status from #TempTable

END






GO


