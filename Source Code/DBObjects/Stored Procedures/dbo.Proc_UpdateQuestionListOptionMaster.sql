IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateQuestionListOptionMaster]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateQuestionListOptionMaster]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.QuestionListOptionMaster
Created by      : Anurag Verma
Date            : 5/31/2005
Purpose    	  :Inserting data in QuestionListOptionMaster
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_UpdateQuestionListOptionMaster
(
@OPTIONID     int,
@LISTID     int,
@OPTIONNAME     nvarchar(200)
)
AS

BEGIN
UPDATE QUESTIONLISTOPTIONMASTER
SET
OPTIONNAME=@OPTIONNAME
WHERE
LISTID=@LISTID AND OPTIONID=@OPTIONID
END


GO

