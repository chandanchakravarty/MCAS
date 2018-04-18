IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTabGroupQuest]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTabGroupQuest]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetTabGroupQuest
Created by      : Nidhi
Date            : 01/06/2005
Purpose         : To get tab count
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetTabGroupQuest
(
@SCREENID 	int,
@TABID int
 
)
AS
BEGIN
 SELECT DISTINCT QID, QG.GROUPID, QDESC, GROUPTYPE, QG.SEQNO 
				 FROM USERQUESTIONS 
				 INNER JOIN QUESTIONGROUPMASTER QG ON QG.GROUPID = USERQUESTIONS.GROUPID AND QG.CARRIERID=USERQUESTIONS.CARRIERID AND QG.TABID = USERQUESTIONS.TABID 
				 WHERE USERQUESTIONS.TABID =@TABID  AND USERQUESTIONS.SCREENID=@SCREENID
				 ORDER BY QDESC
 


END


GO

