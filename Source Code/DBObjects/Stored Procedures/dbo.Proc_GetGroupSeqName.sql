IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetGroupSeqName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetGroupSeqName]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetGroupSeqName
Created by      : Nidhi
Date            : 01/06/2005
Purpose         : To get group seq name
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetGroupSeqName
(
@SCREENID 	int,
@TABID int,
@GROUPID int
 
)
AS
BEGIN
 SELECT GROUPNAME FROM QUESTIONGROUPMASTER 
WHERE SCREENID=@SCREENID and GROUPID = @GROUPID  AND TABID = @TABID
 order by GROUPNAME


END


GO

