IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateACT_CURRENT_DEPOSITS_TAPETOTAL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateACT_CURRENT_DEPOSITS_TAPETOTAL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*
Created By		: Vijay Joshi
Created Date	: 23 Aug 2005
Purpose			: To update the tape total of deposit
*/
CREATE PROC Proc_UpdateACT_CURRENT_DEPOSITS_TAPETOTAL
(
	@DEPOSIT_ID int,
	@TAPE_TOTAL decimal(20,2)
)
AS
BEGIN
	
	--IF(@TAPE_TOTAL<>0.0 OR @TAPE_TOTAL<>0.00 OR @TAPE_TOTAL<>0)
	--BEGIN 	
		
		UPDATE ACT_CURRENT_DEPOSITS
		SET TAPE_TOTAL = @TAPE_TOTAL
		WHERE DEPOSIT_ID = @DEPOSIT_ID
	--END 
END







GO

