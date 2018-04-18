IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertUSERQUESTIONS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertUSERQUESTIONS]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO






/*----------------------------------------------------------
Proc Name       : dbo.USERQUESTIONS
Created by      : Anurag Verma
Date            : 5/30/2005
Purpose    	  :Inserting user defined questions
Revison History :
Modified By		: Manab
Modified Date		: 28 June 2005
Description		: Adding insert script for 4 more columns 
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE    PROC Dbo.Proc_InsertUSERQUESTIONS
(
@QID     int output,
@CARRIERID     int,
@QUESTIONLISTID     int,
@TABID     int,
@QDESC     nvarchar(1000),
@ISREQ     nchar(2),
@SCREENID     int,
@PREFIX     nvarchar(200),
@SUFFIX     nvarchar(200),
@QNOTES     nvarchar(1000),
@FLGQUESDESC     nchar(2),
@QUESTIONTYPEID     numeric(13),
@GROUPID     numeric(13),

@ANSWERTYPEID     numeric(13),
@FLGCOMPVALUE     nvarchar(400),
@ISACTIVE     nchar(4),
@LASTMODIFIEDDATE     datetime,
@LASTMODIFIEDBY     int,
@REQTOTAL     nchar(2),
--manab added for new params
@STYLE varchar(500),
@MAXLENGTH int,
@VALIDATIONTYPE varchar(500),
@COLSPAN int,
@DEPQUESTIONTEXT varchar(200),
@DEPQUESTYPE int,
@DEPANSTYPE int,
@DEPQUESTIONLIST int

)
AS
 Declare @Count numeric    
 DECLARE @CNTSEQNO NUMERIC
 SELECT @Count = ISNULL(MAX(QID),0)+1,@CNTSEQNO = ISNULL(MAX(SEQNO),0)+1   FROM USERQUESTIONS where  SCREENID=@SCREENID and TABID=@TABID

BEGIN
INSERT INTO USERQUESTIONS
(
QID,
CARRIERID,
QUESTIONLISTID,
TABID,
QDESC,
SEQNO,
ISREQ,
SCREENID,
PREFIX,
SUFFIX,
QNOTES,
FLGQUESDESC,
QUESTIONTYPEID,
GROUPID,
ANSWERTYPEID,
FLGCOMPVALUE,
ISACTIVE,
LASTMODIFIEDDATE,
LASTMODIFIEDBY,
REQTOTAL,
STYLE,
MAXLENGTH,
VALIDATIONTYPE,
COLSPAN,
DEPQUESTIONTEXT,
DEPQUESTYPE,
DEPANSTYPE,
DEPQUESTIONLIST
)
VALUES
(
@Count,
@CARRIERID,
@QUESTIONLISTID,
@TABID,
@QDESC,
@CNTSEQNO,
@ISREQ,
@SCREENID,
@PREFIX,
@SUFFIX,
@QNOTES,
@FLGQUESDESC,
@QUESTIONTYPEID,
@GROUPID,
@ANSWERTYPEID,
@FLGCOMPVALUE,
@ISACTIVE,
@LASTMODIFIEDDATE,
@LASTMODIFIEDBY,
@REQTOTAL,
@STYLE,
@MAXLENGTH,
@VALIDATIONTYPE,
@COLSPAN,
@DEPQUESTIONTEXT,
@DEPQUESTYPE,
@DEPANSTYPE,
@DEPQUESTIONLIST
)

--SELECT @QID=ISNULL(MAX(QID),0) FROM USERQUESTIONS where  SCREENID=@SCREENID and TABID=@TABID AND GROUPID=@GROUPID
select @QID=@Count
END





GO

