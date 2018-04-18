IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetGridVerticalControls]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetGridVerticalControls]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetGridVerticalControls
Created by      : Nidhi
Date            : 31/05/2005
Purpose         : To get vertical controls  
Revison History :
Used In         :   Wolverine
Modified By 	: Anurag Verma
Modified ON	: 16/03/2005
Purpose	: To add groupid clause
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetGridVerticalControls
(
@QID 	int,
@CARRIERID int ,
@groupid int
)
AS
BEGIN
	Select QGRIDOPTIONID, QID, OPTIONTEXT, OPTIONTYPEID, ISREQUIRED, ISACTIVE, GRIDOPTIONLAYOUT, LISTID 
	from QuestionGrid 
	where QID = @QID  And GRIDOPTIONLAYOUT = 'V'  and groupid=@groupid order by QGRIDOPTIONID
		
END


GO

