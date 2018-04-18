IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCountryList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCountryList]
GO

CREATE Proc [dbo].[Proc_GetCountryList] 

As

SET FMTONLY OFF;

BEGIN

 

/*

IF OBJECT_ID('tempdb..#mytemptable') IS NOT NULL

DROP TABLE #mytemptable

IF OBJECT_ID('tempdb..#mytemptable1') IS NOT NULL

DROP TABLE #mytemptable1

SELECT * INTO #mytemptable1 FROM MNT_UserCountry ORDER BY CreatedDate DESC

SELECT * INTO #mytemptable FROM (SELECT DISTINCT result1.* FROM #mytemptable1 d

CROSS APPLY (SELECT TOP 1 * FROM #mytemptable1 WHERE #mytemptable1.CountryName = d.CountryName ORDER BY d.CreatedDate) result1) AS #mytemptable

SELECT * FROM #mytemptable where Status='Y' ORDER BY CountryName

*/

 

select *,'Y' as status FROM MNT_Country order by CountryName asc

 

 

END
GO


