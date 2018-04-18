

/****** Object:  StoredProcedure [dbo].[Proc_GetPaymentProcessSearch]    Script Date: 11/21/2014 09:32:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPaymentProcessSearch]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPaymentProcessSearch]
GO



/****** Object:  StoredProcedure [dbo].[Proc_GetPaymentProcessSearch]    Script Date: 11/21/2014 09:32:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO







CREATE PROC [dbo].[Proc_GetPaymentProcessSearch] (@query nvarchar(max))
AS
BEGIN

  IF OBJECT_ID('tempdb..#TempTable') IS NOT NULL
    DROP TABLE #TempTable
  SET FMTONLY OFF;
  CREATE TABLE #TempTable (
    [AccidentClaimId] [int] NULL,
    [ClaimNo] [nvarchar](50) NULL,
    [Organization] [nvarchar](100) NULL,
    [AccidentDate] [datetime] NULL,
    [VehicleNo] [nvarchar](50) NULL,
    [ClaimType] [varchar](10) NULL,
    [ClaimID] [int] NULL
  )
  EXEC (@query)

  SELECT
    AccidentClaimId,
    ClaimNo,
    Organization,
    AccidentDate,
    VehicleNo,
    ClaimType,
    ClaimID
  FROM #TempTable

END







GO


