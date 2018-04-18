IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchScreenData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchScreenData]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name      : dbo.Proc_FetchScreenData
Created by      	: Anurag Verma
Date            	: 5/25/2005
Purpose    	  : retrieving data from onlinescreenmaster
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_FetchScreenData
@SCREENID INT
AS
BEGIN
SELECT SCREENNAME,CLASSID,SUBCLASSID,PROFESSIONID,ISACTIVE,DISPLAYNAME FROM ONLINESCREENMASTER WHERE SCREENID =@SCREENID 
END


GO

