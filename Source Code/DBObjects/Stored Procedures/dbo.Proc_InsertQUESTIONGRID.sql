IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertQUESTIONGRID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertQUESTIONGRID]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





/*----------------------------------------------------------
Proc Name       : dbo.Proc_InsertQUESTIONGRID
Created by      : Anurag Verma
Date            : 	6/15/2005
Purpose    	  :Inserting user defined questions OPTIONS
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC Dbo.Proc_InsertQUESTIONGRID
(
@QGRIDOPTIONID int output,
@QID int,
@OPTIONTEXT nvarchar(400),
@OPTIONTYPEID int,
@ISREQUIRED char(1),
@ISACTIVE nchar(2),
@GRIDOPTIONLAYOUT char(1),
@ANSWERTYPEID int,
@LISTID int,
@CARRIERID int,
@LASTMODIFIEDBY int,
@LASTMODIFIEDDATE datetime,
@GROUPID INT,
@TABID INT,
@SCREENID INT
)
AS
Declare @Count numeric    
SELECT @Count = ISNULL(MAX(QGRIDOPTIONID),0)+1  FROM QUESTIONGRID where QID=@QID

BEGIN
INSERT INTO QUESTIONGRID
(
QGRIDOPTIONID,
QID,
OPTIONTEXT,
OPTIONTYPEID,
ISREQUIRED,
ISACTIVE,
GRIDOPTIONLAYOUT,
ANSWERTYPEID,
LISTID,
CARRIERID,
LASTMODIFIEDBY,
LASTMODIFIEDDATE,
GROUPID,
TABID,
SCREENID
)
VALUES
(
@Count,
@QID,
@OPTIONTEXT,
@OPTIONTYPEID,
@ISREQUIRED,
'Y' ,
@GRIDOPTIONLAYOUT ,
@ANSWERTYPEID,
@LISTID,
@CARRIERID,
@LASTMODIFIEDBY,
@LASTMODIFIEDDATE,
@GROUPID,
@TABID,
@SCREENID
)

SELECT @QGRIDOPTIONID =@Count
END




GO

