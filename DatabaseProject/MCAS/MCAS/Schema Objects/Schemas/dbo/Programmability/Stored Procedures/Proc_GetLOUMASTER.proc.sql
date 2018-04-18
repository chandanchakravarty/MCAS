CREATE PROCEDURE [dbo].[Proc_GetLOUMASTER]
	@query [nvarchar](max)
WITH EXECUTE AS CALLER
AS
BEGIN  
IF OBJECT_ID('tempdb..#TempTable') IS NOT NULL DROP TABLE #TempTable  
SET FMTONLY OFF;  
create table #TempTable  
(  
Id int,  
LouRate decimal(18,2),  
EffectiveDate datetime null,  
IsActive char(1) null,  
CreatedDate datetime null,  
CreatedBy nvarchar(25) null,  
ClaimId int null  
)  
exec (@query)  
  
SELECT Id,[LouRate] ,[EffectiveDate] ,[IsActive] ,[CreatedDate] ,[CreatedBy] ,[ClaimId] FROM #TempTable order by [EffectiveDate] desc  
  
END


