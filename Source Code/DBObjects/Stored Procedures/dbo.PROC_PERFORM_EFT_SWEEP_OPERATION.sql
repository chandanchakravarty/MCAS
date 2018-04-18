IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_PERFORM_EFT_SWEEP_OPERATION]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_PERFORM_EFT_SWEEP_OPERATION]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc dbo.PROC_PERFORM_EFT_SWEEP_OPERATION 

CREATE PROC [dbo].[PROC_PERFORM_EFT_SWEEP_OPERATION] 
	@IDEN_ROW_ID VARCHAR(2000),
	@OPERATION VARCHAR(50)
AS
BEGIN

	DECLARE @SQLQUERY AS VARCHAR(3000)

	DECLARE @CURRENT_CHECK_ID VARCHAR(10)              
	DECLARE @COUNT INT              
	DECLARE @VOID_ACTIVITY_ID int

	IF (@OPERATION = 'HOLD')
		BEGIN
			-- PRINT 'HOLD'
			SELECT @SQLQUERY = 'UPDATE EOD_EFT_SPOOL SET PROCESSED=''H'' WHERE IDEN_ROW_ID IN (' + @IDEN_ROW_ID + ')'
		END

	ELSE IF (@OPERATION = 'UNHOLD')
		BEGIN
			-- PRINT 'UNHOLD'
			SELECT @SQLQUERY = 'UPDATE EOD_EFT_SPOOL SET PROCESSED=NULL WHERE IDEN_ROW_ID IN (' + @IDEN_ROW_ID + ')'
		END

	
-- PRINT @SQLQUERY 
EXEC (@SQLQUERY)

END








GO

