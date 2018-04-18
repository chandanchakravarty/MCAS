IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchMainListData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchMainListData]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name      : dbo.Proc_FetchMainListData
Created by      	: Anurag Verma
Date            	: 5/31/2005
Purpose    	  : retrieving data from QUESTIONLISTOPTIONMASTER 
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC dbo.Proc_FetchMainListData
@LISTID INT
AS
BEGIN
SELECT LISTID,OPTIONID,OPTIONNAME FROM QUESTIONLISTOPTIONMASTER WHERE ISACTIVE='Y' AND LISTID = @LISTID ORDER BY UPPER(OPTIONNAME)
END


GO

