IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetGridVerticalQuestion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetGridVerticalQuestion]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetGridVerticalQuestion
Created by      : Nidhi
Date            : 31/05/2005
Purpose         : To get vertical question
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC Dbo.Proc_GetGridVerticalQuestion
(
@QID 	int,
@CARRIERID int ,
@TABID INT,
@SCREENID INT

)
AS
BEGIN
	Select QGRIDOPTIONID, QID, OPTIONTEXT, OPTIONTYPEID, ISREQUIRED, ISACTIVE, GRIDOPTIONLAYOUT, LISTID
	 from QuestionGrid where QID = @QID and SCREENID=@SCREENID and TABID=@TABID order by QGRIDOPTIONID
		
END




GO

