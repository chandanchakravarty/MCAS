IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchListData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchListData]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name      : dbo.Proc_FetchListData
Created by      	: Anurag Verma
Date            	: 5/31/2005
Purpose    	  : retrieving data from questionListMaster
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_FetchListData
@LISTID INT
AS
BEGIN
SELECT NAME  FROM QUESTIONLISTMASTER WHERE LISTID = @LISTID AND ISACTIVE='Y' ORDER BY UPPER(NAME)
END


GO

