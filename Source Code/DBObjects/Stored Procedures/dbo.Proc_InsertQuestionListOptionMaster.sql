IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertQuestionListOptionMaster]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertQuestionListOptionMaster]
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
CREATE PROC Dbo.Proc_InsertQuestionListOptionMaster
(
@OPTIONID     int output,
@CARRIERID     numeric(5),
@LISTID     int,
@OPTIONNAME     nvarchar(200),
@ISACTIVE     nchar(4)
)
AS
DECLARE @COUNT INT

SELECT @COUNT=ISNULL(MAX(OPTIONID),0)+1 FROM   QUESTIONLISTOPTIONMASTER WHERE LISTID=@LISTID

BEGIN
INSERT INTO QUESTIONLISTOPTIONMASTER
(
OPTIONID,
CARRIERID,
LISTID,
OPTIONNAME,
ISACTIVE
)
VALUES
(
@COUNT,
@CARRIERID,
@LISTID,
@OPTIONNAME,
@ISACTIVE
)
END
select @OPTIONID=isnull(max(optionid),0) +1 from QUESTIONLISTOPTIONMASTER where  LISTID=@LISTID


GO

