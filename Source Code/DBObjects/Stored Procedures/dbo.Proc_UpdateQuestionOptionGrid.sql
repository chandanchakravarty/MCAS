IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateQuestionOptionGrid]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateQuestionOptionGrid]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------
Proc Name       : dbo.Proc_UpdateQuestionOptionGrid
Created by      : Anurag Verma
Date            : 	6/16/2005
Purpose    	  :updating user defined questions option
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE  PROC Dbo.Proc_UpdateQuestionOptionGrid
(
@QGRIDOPTIONID int,
@QID int,
@OPTIONTEXT nvarchar(400),
@OPTIONTYPEID int,
@ISREQUIRED char(1),

@GRIDOPTIONLAYOUT char(1),
@ANSWERTYPEID int,
@LISTID int,

@LASTMODIFIEDBY int,
@LASTMODIFIEDDATE datetime,
@GROUPID INT,
@TABID INT,
@SCREENID INT
)
AS

BEGIN
UPDATE QUESTIONGRID
SET
OPTIONTEXT=@OPTIONTEXT,
OPTIONTYPEID=@OPTIONTYPEID,
ISREQUIRED=@ISREQUIRED,
GRIDOPTIONLAYOUT=@GRIDOPTIONLAYOUT,
ANSWERTYPEID=@ANSWERTYPEID,
LISTID=@LISTID,
LASTMODIFIEDBY=@LASTMODIFIEDBY,
LASTMODIFIEDDATE=@LASTMODIFIEDDATE,
TABID=@TABID,
SCREENID=@SCREENID
WHERE 
GROUPID=@GROUPID AND 
QGRIDOPTIONID=@QGRIDOPTIONID AND
QID=@QID
END



GO

