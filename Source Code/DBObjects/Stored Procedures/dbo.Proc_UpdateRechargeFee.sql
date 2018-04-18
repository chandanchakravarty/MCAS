IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateRechargeFee]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateRechargeFee]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC dbo.Proc_UpdateRechargeFee
(
	@IDEN_ROW_ID int,
	@INS_FEE decimal(18,2),
	@RESULT INT OUT   
)
AS
 BEGIN

UPDATE ACT_CUSTOMER_OPEN_ITEMS  SET TOTAL_DUE = @INS_FEE
 WHERE IDEN_ROW_ID  = @IDEN_ROW_ID

DELETE FROM ACT_FEE_REVERSAL WHERE 
CUSTOMER_OPEN_ITEM_ID = @IDEN_ROW_ID

SET  @RESULT = 1

END






GO

