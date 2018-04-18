IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetGridQuestion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetGridQuestion]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetGridQuestion
Created by      : Nidhi
Date            : 31/05/2005
Purpose         : To get list 
Revison History :
Modified By	: Manab
Modified Date	: 30 june 2005
Description 	: Removing Grid Id from where clause
Used In         : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC Dbo.Proc_GetGridQuestion
(
@QID 	int,
@CARRIERID int,
@TABID int,
@SCREENID int
 
)
AS
BEGIN
Select QGRIDOPTIONID, QID, OPTIONTEXT, OPTIONTYPEID, ISREQUIRED, ISACTIVE, GRIDOPTIONLAYOUT, LISTID 
from QuestionGrid
where QID =@QID and  TABID=@TABID and SCREENID=@SCREENID order by QGRIDOPTIONID
--where QID =@QID and groupid=@groupid order by QGRIDOPTIONID

END




GO

