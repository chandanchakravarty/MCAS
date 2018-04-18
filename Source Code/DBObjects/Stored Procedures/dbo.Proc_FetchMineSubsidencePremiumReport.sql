IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchMineSubsidencePremiumReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchMineSubsidencePremiumReport]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop PROC [dbo].[Proc_FetchMineSubsidencePremiumReport] 
CREATE PROC [dbo].[Proc_FetchMineSubsidencePremiumReport] 
(
	@YEAR NVARCHAR(40),
	@MONTH NVARCHAR(40)
)
AS
BEGIN

		SELECT * FROM 
		VW_FetchMineSubsidencePremiumReport
		WHERE EPR_YEAR=@YEAR AND EPR_MONTH=@MONTH 
END



GO

