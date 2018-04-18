IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchOptionList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchOptionList]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name      : dbo.Proc_FetchOptionList
Created by      	: Anurag Verma
Date            	: 5/27/2005
Purpose    	  : retrieving data from QUESTIONLISTOPTIONMASTER 
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC dbo.Proc_FetchOptionList
@LISTID INT
AS
BEGIN
SELECT OPTIONID,OPTIONNAME FROM QUESTIONLISTOPTIONMASTER WHERE LISTID =@LISTID ORDER BY OPTIONNAME
END


GO

