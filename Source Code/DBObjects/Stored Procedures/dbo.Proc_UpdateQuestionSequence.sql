IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateQuestionSequence]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateQuestionSequence]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_UpdateQuestionSequence
Created by      : Nidhi
Date            : 01/06/2005
Purpose         : To update the tab sequence
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_UpdateQuestionSequence
(
@SCREENID 	int,
@TABID int,
@GROUPID int,
@SEQNO int
 
)
AS
BEGIN
UPDATE QUESTIONGROUPMASTER 
SET SEQNO = @SEQNO  WHERE  SCREENID=@SCREENID and TABID =@TABID AND GROUPID =@GROUPID
END


GO

