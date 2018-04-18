IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetFiscalIDForCurrentDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetFiscalIDForCurrentDate]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE proc Proc_GetFiscalIDForCurrentDate
(
	@FOR_DATE	Datetime,
	@FISCAL_ID	Int out
)
AS
BEGIN 
	SELECT @FISCAL_ID = FISCAL_ID 
	FROM ACT_GENERAL_LEDGER
	WHERE	CAST(CONVERT(VARCHAR,@FOR_DATE,101) AS Datetime) 
					>= CAST(CONVERT(VARCHAR,FISCAL_BEGIN_DATE,101) AS Datetime)
	AND		CAST(CONVERT(VARCHAR,@FOR_DATE,101) AS Datetime) 
					<= CAST(CONVERT(VARCHAR,FISCAL_END_DATE,101) AS Datetime)
END





GO

