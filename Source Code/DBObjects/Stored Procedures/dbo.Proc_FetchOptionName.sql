IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchOptionName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchOptionName]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name      : dbo.Proc_FetchOptionName
Created by      	: Anurag Verma
Date            	: 5/31/2005
Purpose    	  : retrieving optionName from questionListOptionMaster
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_FetchOptionName
@LISTID INT,
@LISTOPTIONID INT
AS
BEGIN
SELECT OPTIONNAME FROM QUESTIONLISTOPTIONMASTER 
WHERE ISACTIVE='Y' AND LISTID =@LISTID AND OPTIONID =@LISTOPTIONID
END


GO

