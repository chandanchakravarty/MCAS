IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetQuestionDetail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetQuestionDetail]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetQuestionDetail
Created by      : Nidhi
Date            : 31/05/2005
Purpose         : To get QUESTIONS   
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetQuestionDetail
(
@SCREENID 	int,
@CARRIERID int,
@TABID int,
@QID int,
@GROUPID INT
)
AS
BEGIN
	 select QID, QDESC, QUESTIONTYPEID, QDESC, PREFIX, SUFFIX, userquestions.TABID,
	 ISREQ, userquestions.GROUPID, QNOTES, QUESTIONLISTID,
	 FLGQUESDESC, DESCTEXT, FLGCOMPVALUE, QG.GroupType, QLM.TYPE, QLM.TABLENAME 
	 from userquestions
	 Inner Join QUESTIONGROUPMASTER QG on
	 QG.GROUPID = userquestions.GROUPID AND QG.TABID = USERQUESTIONS.TABID  
	 Inner Join QUESTIONTABMASTER TB on
	 TB.TABID = userquestions.TABID AND TB.CARRIERID = @CARRIERID 
	 Left Outer Join QUESTIONLISTMASTER QLM on QLM.LISTID = userquestions.QUESTIONLISTID 
	 where userquestions.TABID= @TABID AND TB.SCREENID = @SCREENID  AND userquestions.CARRIERID =@CARRIERID  AND userquestions.QID =@QID AND  userquestions.GROUPID=@GROUPID
	 Order by QG.SEQNO,userquestions.SEQNO 

END


GO

