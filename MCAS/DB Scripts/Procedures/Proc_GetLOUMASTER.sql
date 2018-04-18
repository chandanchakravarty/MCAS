
/****** Object:  StoredProcedure [dbo].[Proc_GetLOUMASTER]    Script Date: 08/25/2014 14:35:50 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLOUMASTER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLOUMASTER]
GO


/****** Object:  StoredProcedure [dbo].[Proc_GetLOUMASTER]    Script Date: 08/25/2014 14:35:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE Proc [dbo].[Proc_GetLOUMASTER]
(
@query nvarchar(max)
)
as
BEGIN
IF OBJECT_ID('tempdb..#TempTable') IS NOT NULL DROP TABLE #TempTable
SET FMTONLY OFF;
create table #TempTable
(
Id int,
LouRate bigint,
EffectiveDate datetime null,
IsActive char(1) null,
CreatedDate datetime null,
CreatedBy nvarchar(25) null,
ClaimId int null
)
exec (@query)

SELECT Id,[LouRate] ,[EffectiveDate] ,[IsActive] ,[CreatedDate] ,[CreatedBy] ,[ClaimId] FROM #TempTable order by [EffectiveDate] desc

END





GO


