IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetGroupCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetGroupCount]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetGroupCount
Created by      : Nidhi
Date            : 01/06/2005
Purpose         : To get tab count
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetGroupCount
(
@SCREENID 	int,
@TABID int
 
)
AS
BEGIN
SELECT DISTINCT GROUPID
FROM USERQUESTIONS WHERE  TABID = @TABID and SCREENID=@SCREENID


END


GO

