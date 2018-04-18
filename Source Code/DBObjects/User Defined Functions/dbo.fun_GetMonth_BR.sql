IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fun_GetMonth_BR]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[fun_GetMonth_BR]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		Naveen Pujari
-- Create date: 20/1/2011
-- Description:	Function is used for Converting the date in Brazilian format
-- =============================================
--drop function dbo.fun_GetMonth_BR
CREATE FUNCTION [dbo].[fun_GetMonth_BR]
(
	@Month int
)
RETURNS varchar(50)
AS
BEGIN

	RETURN CASE 
	WHEN @MONTH=1 THEN 'JANEIRO'
	WHEN @MONTH=2 THEN 'FEVEREIRO'
	WHEN @MONTH=3 THEN 'MARÇO'
	WHEN @MONTH=4 THEN 'ABRIL'
	WHEN @MONTH=5 THEN 'MAIO'
	WHEN @MONTH=6 THEN 'JUNHO'
	WHEN @MONTH=7 THEN 'JULHO'
	WHEN @MONTH =8 THEN 'AGOSTO'
	WHEN @MONTH=9 THEN 'SETEMBRO'
	WHEN @MONTH=10 THEN 'OUTUBRO'
	WHEN @MONTH=11 THEN 'NOVEMBRO'
	WHEN @MONTH=12 THEN 'DEZEMBRO'
	
   end
	
end

GO

