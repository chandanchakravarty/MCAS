IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateTabSequence]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateTabSequence]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_UpdateTabSequence
Created by      : Nidhi
Date            : 01/06/2005
Purpose         : To update the tab sequence
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_UpdateTabSequence
(
@SCREENID 	int,
@TABID int,
@SEQNO int
 
)
AS
BEGIN
 UPDATE QUESTIONTABMASTER
SET 
	SEQNO = @SEQNO 
WHERE 
	TABID = @TABID  AND 
	SCREENID = @SCREENID  		
	


END


GO

