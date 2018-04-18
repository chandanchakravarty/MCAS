IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetWorkflowScreens]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetWorkflowScreens]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE procedure Proc_GetWorkflowScreens
(
	@SCREEN_ID	varchar(20)
)
AS
declare @WORKFLOW_SCREENS	varchar(255);
set 	@WORKFLOW_SCREENS 	=	null;
BEGIN
	select @WORKFLOW_SCREENS = WORKFLOW_SCREENS from MNT_WORKFLOW_LIST where SCREEN_ID = @SCREEN_ID
	
	if(@WORKFLOW_SCREENS is null or @WORKFLOW_SCREENS = '')
	begin
		select  @WORKFLOW_SCREENS = WORKFLOW_SCREENS from MNT_WORKFLOW_GROUP where GROUP_ID = 
		(select GROUP_ID from MNT_WORKFLOW_LIST where SCREEN_ID = @SCREEN_ID)
	end
	select @WORKFLOW_SCREENS as WORKFLOW_SCREENS
END



GO

