IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_GetMNT_SCREEN_LIST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_GetMNT_SCREEN_LIST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------
Proc Name       : dbo.proc_GetMNT_SCREEN_LIST
Created by      : Anshuman
Date            : 5/25/2005
Purpose         : To fetch record from MNT_SCREEN_LIST table
Revison History :
Used In         : brics
------------------------------------------------------------
Date     Review By          Comments
Modified By	: Anshuman Sharan
Modified On	: 05/26/2005
Purpose		: Remove top_menu_id column from select
------   ------------       -------------------------*/
CREATE  procedure proc_GetMNT_SCREEN_LIST
(
	@SCREEN_ID	varchar(20)
)
AS
BEGIN
	SELECT 
		SCREEN_ID, 
		SCREEN_DESC,
		SCREEN_PATH,
		SCREEN_READ,
		SCREEN_WRITE,
		SCREEN_EXECUTE,
		SCREEN_DELETE
	FROM
		MNT_SCREEN_LIST
	WHERE
		SCREEN_ID = @SCREEN_ID	
END




GO

