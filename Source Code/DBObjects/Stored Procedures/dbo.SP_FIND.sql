IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SP_FIND]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[SP_FIND]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO




CREATE PROCEDURE SP_FIND
	@lstrTABLE_NAME		NVARCHAR(100) = '',
	@lstrXType		NVARCHAR(5) = 'U'
AS
	DECLARE @XTYPE NVARCHAR(5)
BEGIN
	/*
		Stored Procedure To Find the name of the Tables/Views
		or Stored Procedures.
	*/
	
	if upper( @lstrXType) = 'H'
	BEGIN
		SELECT @XTYPE = XTYPE FROM SYSOBJECTS WHERE NAME LIKE @lstrTABLE_NAME 
		IF @XTYPE = 'U'
			EXEC ( 'SP_HELP ' + @lstrTABLE_NAME)
		ELSE IF @XTYPE = 'P'
			EXEC ( 'SP_HELPTEXT ' + @lstrTABLE_NAME)
	END
	ELSE
		SELECT * FROM SYSOBJECTS WHERE XTYPE = @lstrXTYPE AND  NAME LIKE '%' + @lstrTABLE_NAME + '%'
		ORDER BY CRDATE desc
END

/*
	sp_Find 'POLICY_VEHICLES', 'H'
*/

GO

