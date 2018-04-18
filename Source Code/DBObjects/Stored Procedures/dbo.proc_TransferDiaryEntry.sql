IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[proc_TransferDiaryEntry]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[proc_TransferDiaryEntry]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------
Proc Name       : proc_TransferDiaryEntry
Created by      : Anurag Verma
Date            : 4/13/2005
Purpose         : To update listopen field of todolist table 
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/


CREATE  PROCEDURE dbo.proc_TransferDiaryEntry
(
@LISTID INT,
@USERID INT,
@NOTES NVARCHAR(4000),
@FOLLOWUPDATE DATETIME
)
AS


--SET NOCOUNT ON
BEGIN 
	UPDATE TODOLIST 
		SET 
			TOUSERID=@USERID,
			NOTE=@NOTES,
			FOLLOWUPDATE=@FOLLOWUPDATE	
			 

		WHERE LISTID=@LISTID
END 
--SET NOCOUNT OFF



GO

