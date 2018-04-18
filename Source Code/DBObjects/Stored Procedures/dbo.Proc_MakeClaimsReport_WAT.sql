IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MakeClaimsReport_WAT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MakeClaimsReport_WAT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop PROC [dbo].[Proc_MakeClaimsReport_WAT]                                                
CREATE PROC [dbo].[Proc_MakeClaimsReport_WAT]                                                
(
	@MONTH int,
	@YEAR int,                       
	@STATE_ID varchar(50) = NULL
)
AS                                                                                
BEGIN      
	IF @STATE_ID IS NULL
	BEGIN    
		SELECT LOB, MS.STATE_NAME, COV_DES, AMOUNT, DETAIL_TYPE_DESCRIPTION, ACTV_MONTH, ACTV_YEAR
		FROM VW_CLAIMSREPORT_WAT VCW
		INNER JOIN MNT_COUNTRY_STATE_LIST MS
		ON VCW.STATE_ID = MS.STATE_ID
		WHERE VCW.ACTV_MONTH <= @MONTH AND VCW.ACTV_YEAR = @YEAR
		ORDER BY ACTV_MONTH, ACTV_YEAR
	END
	ELSE
	BEGIN
		SELECT LOB, MS.STATE_NAME, COV_DES, AMOUNT, DETAIL_TYPE_DESCRIPTION, ACTV_MONTH, ACTV_YEAR
		FROM VW_CLAIMSREPORT_WAT VCW
		INNER JOIN MNT_COUNTRY_STATE_LIST MS
		ON VCW.STATE_ID = MS.STATE_ID
		WHERE VCW.STATE_ID = @STATE_ID AND VCW.ACTV_MONTH <= @MONTH AND VCW.ACTV_YEAR = @YEAR
		ORDER BY ACTV_MONTH, ACTV_YEAR
	END
END           
   





GO

