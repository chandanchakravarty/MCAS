IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetList]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetList
Created by      : Nidhi
Date            : 31/05/2005
Purpose         : To get list 
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetList
(
@LISTID 	int
 
)
AS
BEGIN

Select LISTID,OPTIONID,OPTIONNAME,
CAST(LISTID AS varchar(20))+ '#' +CAST(OPTIONID AS varchar(20))as  COMBINEID From QUESTIONLISTOPTIONMASTER WHERE LISTID = @LISTID order by OPTIONID ;

END


GO

