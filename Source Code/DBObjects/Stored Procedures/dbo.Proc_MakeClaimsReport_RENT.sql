IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MakeClaimsReport_RENT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MakeClaimsReport_RENT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP PROC [dbo].[Proc_MakeClaimsReport_RENT] 
CREATE PROC [dbo].[Proc_MakeClaimsReport_RENT]                                                      
(
	@MONTH int,
	@YEAR int, 
	@STATE_ID varchar(50) = NULL
)
AS                                                                                      
BEGIN 
	IF @STATE_ID IS NULL
	BEGIN 
	SELECT LOB, MS.STATE_NAME, LOSS_TYPE, AMOUNT, DETAIL_TYPE_DESCRIPTION, ACTV_MONTH, ACTV_YEAR 
	FROM VW_CLAIMSREPORT_RENT VCR
	INNER JOIN MNT_COUNTRY_STATE_LIST MS
	ON VCR.STATE_ID = MS.STATE_ID
	WHERE VCR.ACTV_MONTH <= @MONTH AND VCR.ACTV_YEAR = @YEAR
	ORDER BY ACTV_MONTH, ACTV_YEAR 
	END 
	ELSE
	SELECT LOB, MS.STATE_NAME, LOSS_TYPE, AMOUNT, DETAIL_TYPE_DESCRIPTION, ACTV_MONTH, ACTV_YEAR 
	FROM VW_CLAIMSREPORT_RENT VCR
	INNER JOIN MNT_COUNTRY_STATE_LIST MS
	ON VCR.STATE_ID = MS.STATE_ID
	WHERE VCR.STATE_ID = @STATE_ID AND VCR.ACTV_MONTH <= @MONTH AND VCR.ACTV_YEAR = @YEAR
	ORDER BY ACTV_MONTH, ACTV_YEAR 
END


--Proc_MakeClaimsReport_RENT 3,2009



GO

