IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateCLM_ADJUSTER_AUTHORITY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateCLM_ADJUSTER_AUTHORITY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*-------------------------------------------------------------
Created by      : Vijay Joshi
Date            : 16-June-2006
Purpose         : To activate or deactivate claim adjuster authority
Revison History :
Used In         :   wolvorine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC dbo.Proc_ActivateDeactivateCLM_ADJUSTER_AUTHORITY
(
	@CODE   		numeric(9),
	@IS_ACTIVE 		Char(1)
)
AS
BEGIN
	

	UPDATE CLM_ADJUSTER_AUTHORITY
	SET 
		IS_ACTIVE	= @IS_ACTIVE
	WHERE
		ADJUSTER_AUTHORITY_ID	= @CODE
	
	RETURN 1
END





GO

