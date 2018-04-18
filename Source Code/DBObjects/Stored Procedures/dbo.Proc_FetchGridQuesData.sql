IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchGridQuesData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchGridQuesData]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name      : dbo.Proc_FetchGridQuesData
Created by      	: Anurag Verma
Date            	: 6/15/2005
Purpose    	  : retrieving data from QUESTIONGRID
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC dbo.Proc_FetchGridQuesData
@QID INT,
@QGRIDOPTIONID int
AS
BEGIN
SELECT QID,OPTIONTEXT,OPTIONTYPEID,ISREQUIRED,ISACTIVE,GRIDOPTIONLAYOUT,LISTID,ANSWERTYPEID FROM QUESTIONGRID WHERE QGRIDOPTIONID = @QGRIDOPTIONID  AND QID=@QID
END


GO

