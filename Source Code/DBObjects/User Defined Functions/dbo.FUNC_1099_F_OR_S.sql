IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[FUNC_1099_F_OR_S]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[FUNC_1099_F_OR_S]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* Procedure Name: DBO.FUNC_1099_F_OR_S
*	Created By: Sibin Philip	
*	Date: 24-Sep-08
*	Purpose: To return Federal or SSN value depending on 1099 new option list
*	
*	Revision History	
*	Modified By:
*	Date:
*	Purpose:
***********************************************/
CREATE FUNCTION dbo.FUNC_1099_F_OR_S(@PROCESS_1099_OPT NVARCHAR(20))
RETURNS VARCHAR(1)
AS 
BEGIN
	DECLARE @F_OR_S VARCHAR(1)
	
	IF (@PROCESS_1099_OPT = '11733' OR @PROCESS_1099_OPT = '11734' OR @PROCESS_1099_OPT = '11735')
		SET @F_OR_S = 'F'
	ELSE 
		SET @F_OR_S = 'S'
	
	RETURN @F_OR_S
END 

GO

