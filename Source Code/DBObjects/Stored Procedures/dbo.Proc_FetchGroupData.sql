IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchGroupData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchGroupData]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name      : dbo.Proc_FetchGroupData
Created by      	: Anurag Verma
Date            	: 5/26/2005
Purpose    	  : retrieving data from QUESTIONGROUPMASTER
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC dbo.Proc_FetchGroupData
@TABID INT,
@GROUPID INT,
@SCREENID int
AS
BEGIN
SELECT GROUPNAME,isnull(ISACTIVE,'Y') as isactive FROM QUESTIONGROUPMASTER WHERE GROUPID=@GROUPID  AND TABID =@TABID AND SCREENID=@SCREENID
END


GO

