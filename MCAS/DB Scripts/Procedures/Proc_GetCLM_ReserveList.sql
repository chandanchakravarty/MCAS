IF EXISTS (SELECT
    *
  FROM sys.objects
  WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_ReserveList]')
  AND type IN (N'P', N'PC'))
  DROP PROCEDURE [dbo].[Proc_GetCLM_ReserveList]
GO

CREATE PROC [dbo].[Proc_GetCLM_ReserveList] (@AccidentId nvarchar(100))
AS
  IF OBJECT_ID('tempdb..#TMP') IS NOT NULL
    DROP TABLE #TMP
  IF OBJECT_ID('tempdb..#TMP1') IS NOT NULL
    DROP TABLE #TMP1
  SET FMTONLY OFF;

  SELECT
    * INTO #TMP
  FROM (SELECT
    COUNT(*) Counts,
    MAX(Createddate) DATE,
    ClaimType,
    ClaimID
  FROM CLM_Reserve
  GROUP BY ClaimType,
           ClaimID) TBL

  SELECT
    * INTO #TMP1
  FROM (SELECT
    ROW_NUMBER() OVER (PARTITION BY U.ClaimType ORDER BY ReserveId) RecordNumber,
    Counts,
    (SELECT
      ClaimRecordNo
    FROM CLM_Claims
    WHERE ClaimID = t.ClaimID)
    AS ClaimRecordNo,
    U.*
  FROM #TMP T
  LEFT JOIN CLM_Reserve U
    ON U.ClaimType = T.ClaimType
    AND U.ClaimID = T.ClaimID
    AND U.Createddate = T.DATE
  WHERE U.AccidentId = @AccidentId) TBL1
   alter table #TMP1 drop column IsActive

  SELECT
    *,
    lk.Description AS ClaimTypeDesc,
    lk.Lookupdesc AS ClaimTypeCode
  FROM #TMP1 claim (NOLOCK)
  LEFT JOIN MNT_Lookups lk (NOLOCK)
    ON lk.lookupvalue = claim.ClaimType
    AND lk.Category = 'ClaimType'
  WHERE AccidentId = @AccidentId
  ORDER BY ClaimType

GO