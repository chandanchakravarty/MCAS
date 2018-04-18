IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTabGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTabGroup]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetTabGroup
Created by      : Nidhi
Date            : 31/05/2005
Purpose         : To get tabs 
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetTabGroup
(
@SCREENID 	int,
@CARRIERID int,
@TABID int,
@GROUPID int
)
AS
BEGIN

SELECT GROUPID, GROUPNAME from QUESTIONGROUPMASTER where GROUPID= @GROUPID  AND TABID =@TABID  AND SCREENID=@SCREENID;

END


GO

