IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchQuesData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchQuesData]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO





/*----------------------------------------------------------
Proc Name      : dbo.Proc_FetchQuesData
Created by      	: Anurag Verma
Date            	: 5/27/2005
Purpose    	  : retrieving data from QUESTIONTYPE
Revison History :
Modified BY	 	: Manab
Modified Date		: 29 June 2005
Description 		: Added 4 more columns in the select clause
Used In 	        : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC dbo.Proc_FetchQuesData
@QID INT,
@screenID INT,
@tabID INT,
@grpID INT
AS
BEGIN
SELECT QDESC,SEQNO,ISREQ,TABID,ISACTIVE,PREFIX,SUFFIX,QNOTES,GROUPID,QUESTIONTYPEID,
ANSWERTYPEID,QUESTIONLISTID,FLGQUESDESC,FLGCOMPVALUE, 
--added by manab
REQTOTAL,STYLE,ISNULL(MAXLENGTH,0) MAXLENGTH,VALIDATIONTYPE,ISNULL(COLSPAN,1) COLSPAN,
DEPQUESTIONTEXT,DEPQUESTYPE,DEPANSTYPE,DEPQUESTIONLIST

--
FROM USERQUESTIONS WHERE QID =@QID and screenID=@screenid and tabid=@tabid and groupid=@grpid
END




GO

