IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchCoverageXml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchCoverageXml]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


/*----------------------------------------------------------
Proc Name      : dbo.Proc_FetchCoverageXml
Created by      	: GAurav Tyagi
Date            	: 27/SEPT/2005
Purpose    	  : retrieving data for coverage
Revison History :
Used In 	      : Wolverine

------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC dbo.Proc_FetchCoverageXml

AS
BEGIN
SELECT  MNT_COVERAGE.COV_ID as COV_ID,MNT_COVERAGE.COV_CODE AS COV_CODE from MNT_COVERAGE
SELECT  MNT_COVERAGE.COV_ID as COV_ID,MNT_COVERAGE.COV_CODE AS COV_CODE,MNT_COVERAGE_RANGES.LIMIT_DEDUC_ID,MNT_COVERAGE_RANGES.LIMIT_DEDUC_AMOUNT ,MNT_COVERAGE_RANGES.LIMIT_DEDUC_TYPE FROM MNT_COVERAGE 
left JOIN MNT_COVERAGE_RANGES  ON MNT_COVERAGE.COV_ID = MNT_COVERAGE_RANGES.COV_ID
--FOR XML AUTO

END

GO

