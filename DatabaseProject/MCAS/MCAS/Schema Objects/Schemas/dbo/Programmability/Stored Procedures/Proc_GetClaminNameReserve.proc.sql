CREATE PROCEDURE [dbo].[Proc_GetClaminNameReserve]
	@AccidentId [nvarchar](100)
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;  
  
SELECT * INTO #TMP  
FROM  
(  
SELECT COUNT(*)Counts, MAX(Createddate)DATE,ClaimType,ClaimID FROM  
CLM_Reserve  
GROUP BY ClaimType,ClaimID  
)TBL  
  
SELECT  U.* FROM #TMP T  
LEFT JOIN CLM_Reserve U ON U.ClaimType=T.ClaimType  
AND U.ClaimID=T.ClaimID  
AND U.Createddate=T.DATE 
AND U.ClaimType=3 
Where U.AccidentId=@AccidentId  
order by ClaimType    
DROP TABLE #TMP


