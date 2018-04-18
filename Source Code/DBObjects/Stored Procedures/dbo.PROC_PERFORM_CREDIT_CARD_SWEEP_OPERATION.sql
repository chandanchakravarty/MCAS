IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_PERFORM_CREDIT_CARD_SWEEP_OPERATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_PERFORM_CREDIT_CARD_SWEEP_OPERATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc dbo.PROC_PERFORM_CREDIT_CARD_SWEEP_OPERATION 

CREATE PROC dbo.PROC_PERFORM_CREDIT_CARD_SWEEP_OPERATION 
	@IDEN_ROW_ID VARCHAR(2000),
	@OPERATION VARCHAR(50)
AS
BEGIN

	DECLARE @SQLQUERY AS VARCHAR(3000)

	IF (@OPERATION = 'HOLD')
		BEGIN
			-- PRINT 'HOLD'
			SELECT @SQLQUERY = 'UPDATE EOD_CREDIT_CARD_SPOOL SET PROCESSED=''H'' WHERE IDEN_ROW_ID IN (' + @IDEN_ROW_ID + ')'
		END

	ELSE IF (@OPERATION = 'UNHOLD')
		BEGIN
			-- PRINT 'UNHOLD'
			SELECT @SQLQUERY = 'UPDATE EOD_CREDIT_CARD_SPOOL SET PROCESSED=''F'' WHERE IDEN_ROW_ID IN (' + @IDEN_ROW_ID + ')'
		END

	
-- PRINT @SQLQUERY 
EXEC (@SQLQUERY)

END





GO

