IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateProfitCenter]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateProfitCenter]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*-------------------------------------------------------------
Created by      : Priya
Date            : 3/18/2005
Purpose         : To update the record in profit center table
Revison History :
Used In         :   wolvorine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC dbo.Proc_ActivateDeactivateProfitCenter
(
	@CODE   		numeric(9),
	@IS_ACTIVE 		Char(1)
)
AS
BEGIN
	
	IF @IS_ACTIVE = 'N'
	BEGIN
		/*Check whether profit center assigned or not*/	
		IF EXISTS(SELECT PC_ID FROM MNT_DEPT_PC_MAPPING WHERE PC_ID = @CODE)
			RETURN -1
	END

	UPDATE MNT_PROFIT_CENTER_LIST
	SET 
		IS_ACTIVE	= @IS_ACTIVE
	WHERE
		PC_ID 		= @CODE
	
	RETURN 1
END








GO

