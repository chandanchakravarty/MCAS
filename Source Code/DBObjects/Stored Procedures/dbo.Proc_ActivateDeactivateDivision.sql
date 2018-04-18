IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateDivision]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateDivision]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_ActivateDeactivateDivision
Created by      : Vijay
Date            : 3/4/2005
Purpose         : To update the record in division table
Revison History :
Used In         :   wolvorine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC dbo.Proc_ActivateDeactivateDivision
(
@CODE	numeric(9),
@IS_ACTIVE 	Char(1)
)
AS
BEGIN

	/*If assigned then return -1*/
	IF @IS_ACTIVE = 'N'
	BEGIN
		/*Check whether division assigned or not*/	
		IF EXISTS(SELECT DIV_ID FROM MNT_DIV_DEPT_MAPPING WHERE DIV_ID = @CODE)
			RETURN -1
	END

	UPDATE MNT_DIV_LIST
	SET 
		Is_Active	= @IS_ACTIVE
	WHERE
		DIV_ID 		= @CODE
	
	return 1
END




GO

