IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ActivateDeactivateCLM_EXPERT_SERVICE_PROVIDERS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ActivateDeactivateCLM_EXPERT_SERVICE_PROVIDERS]
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
CREATE PROC dbo.Proc_ActivateDeactivateCLM_EXPERT_SERVICE_PROVIDERS
(
	@CODE   		numeric(9),
	@IS_ACTIVE 		Char(1)
)
AS
BEGIN
	

	UPDATE CLM_EXPERT_SERVICE_PROVIDERS
	SET 
		IS_ACTIVE	= @IS_ACTIVE
	WHERE
		EXPERT_SERVICE_ID	= @CODE
	
	RETURN 1
END







GO

