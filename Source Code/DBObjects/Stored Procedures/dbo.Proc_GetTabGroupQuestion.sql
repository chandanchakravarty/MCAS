IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTabGroupQuestion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTabGroupQuestion]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetTabGroupQuestion
Created by      : Nidhi
Date            : 01/06/2005
Purpose         : To get tab group questions
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetTabGroupQuestion
(
@SCREENID 	int,
@TABID int,
@GROUPID int
 
)
AS
BEGIN
 SELECT QID, QDESC, GROUPID, SEQNO AS COUNT FROM  USERQUESTIONS WHERE SCREENID=@SCREENID and   GROUPID = @GROUPID  AND TABID = @TABID
ORDER BY QID
END


GO

