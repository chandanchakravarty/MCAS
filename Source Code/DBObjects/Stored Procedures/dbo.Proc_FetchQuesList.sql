IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchQuesList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchQuesList]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO




/*----------------------------------------------------------
Proc Name      : dbo.Proc_FetchQuesList
Created by      	: Anurag Verma
Date            	: 5/27/2005
Purpose    	  : retrieving data from QUESTIONLISTMASTER
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE  PROC dbo.Proc_FetchQuesList
@LTYPE CHAR(1)
AS
BEGIN
IF @LTYPE <> ''
	SELECT LISTID,NAME FROM QUESTIONLISTMASTER WHERE  TYPE=@LTYPE ORDER BY UPPER(NAME)
ELSE
	SELECT LISTID,NAME FROM QUESTIONLISTMASTER ORDER BY UPPER(NAME)


END



GO

