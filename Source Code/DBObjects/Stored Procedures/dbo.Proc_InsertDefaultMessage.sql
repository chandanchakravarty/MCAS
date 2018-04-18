IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertDefaultMessage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertDefaultMessage]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_InsertDefaultMessage
Created by      : Anurag Verma
Date            : 4/19/2005
Purpose    	  :Evaluation
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_InsertDefaultMessage
(
@MSG_TYPE     varchar(2),
@MSG_CODE     varchar(50),
@MSG_DESC     varchar(200),
@MSG_TEXT     varchar(200),
@MSG_POSITION     varchar(2),
@MSG_APPLY_TO     varchar(30),
@IS_ACTIVE     nchar(2),
@CREATED_BY     int,
@CREATED_DATETIME     datetime,
@MODIFIED_BY	INT,
@LAST_UPDATED_DATETIME DATETIME,	
@MSG_ID     int  OUTPUT 
)
AS
BEGIN
declare @MSGID int 
SET @MSGID=-1
if EXISTS (SELECT MSG_CODE FROM MNT_MESSAGE_LIST WHERE MSG_CODE=@MSG_CODE)
	SET @MSGID=1
END

IF @MSGID<>1
BEGIN
select @MSGID=ISNULL(max(msg_id),0)+1 from mnt_message_list

INSERT INTO MNT_MESSAGE_LIST
(
MSG_ID,
MSG_TYPE,
MSG_CODE,
MSG_DESC,
MSG_TEXT,
MSG_POSITION,
MSG_APPLY_TO,
IS_ACTIVE,
CREATED_BY,
CREATED_DATETIME,
MODIFIED_BY,
LAST_UPDATED_DATETIME
)
VALUES
(
@MSGID,
@MSG_TYPE,
@MSG_CODE,
@MSG_DESC,
@MSG_TEXT,
@MSG_POSITION,
@MSG_APPLY_TO,
@IS_ACTIVE,
@CREATED_BY,
@CREATED_DATETIME,
@MODIFIED_BY,
@LAST_UPDATED_DATETIME
)

SELECT @MSG_ID=ISNULL(MAX(MSG_ID),0) FROM MNT_MESSAGE_LIST
END
ELSE
SET @MSG_ID=@MSGID


GO

