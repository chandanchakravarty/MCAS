IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateUSERQUESTIONS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateUSERQUESTIONS]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





/*----------------------------------------------------------
Proc Name       : dbo.USERQUESTIONS
Created by      : Anurag Verma
Date            : 5/31/2005
Purpose    	  :updating user defined questions
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC Dbo.Proc_UpdateUSERQUESTIONS
(
@QID     int,
@QUESTIONLISTID     int,
@TABID     int,
@QDESC     nvarchar(1000),
@ISREQ     nchar(2),
@SCREENID     int,
@PREFIX     nvarchar(200),
@SUFFIX     nvarchar(200),
@QNOTES     nvarchar(1000),
@FLGQUESDESC     nchar(2),
@ISACTIVE nchar(2),
@QUESTIONTYPEID     numeric(13),
@GROUPID     numeric(13),
@ANSWERTYPEID     numeric(13),

@FLGCOMPVALUE     nvarchar(400),
@LASTMODIFIEDDATE     datetime,
@LASTMODIFIEDBY     int,
@REQTOTAL     nchar(2),
@STYLE 	  varchar(500),	
@MAXLENGTH int,
@VALIDATIONTYPE varchar(500),
@COLSPAN int,
@DEPQUESTIONTEXT varchar(200),
@DEPQUESTYPE int,
@DEPANSTYPE int,
@DEPQUESTIONLIST int
)
AS

BEGIN
UPDATE USERQUESTIONS
SET 
QUESTIONLISTID=@QUESTIONLISTID,
QDESC=@QDESC,
ISREQ=@ISREQ,
PREFIX=@PREFIX,
SUFFIX=@SUFFIX,
QNOTES=@QNOTES,
FLGQUESDESC=@FLGQUESDESC,
QUESTIONTYPEID=@QUESTIONTYPEID,
ANSWERTYPEID=@ANSWERTYPEID,
FLGCOMPVALUE=@FLGCOMPVALUE,
LASTMODIFIEDDATE=@LASTMODIFIEDDATE,
LASTMODIFIEDBY=@LASTMODIFIEDBY,
REQTOTAL=@REQTOTAL,
STYLE=@STYLE,
MAXLENGTH=@MAXLENGTH,
VALIDATIONTYPE=@VALIDATIONTYPE,
COLSPAN=@COLSPAN,
DEPQUESTIONTEXT=@DEPQUESTIONTEXT,
DEPQUESTYPE=@DEPQUESTYPE,
DEPANSTYPE=@DEPANSTYPE,
DEPQUESTIONLIST = @DEPQUESTIONLIST
where  SCREENID=@SCREENID and TABID=@TABID AND GROUPID=@GROUPID AND QID=@QID
END




GO

