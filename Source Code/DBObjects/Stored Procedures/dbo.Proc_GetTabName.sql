IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTabName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTabName]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetTabName
Created by      : Nidhi
Date            : 01/06/2005
Purpose         : To get tab name
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetTabName
(
@SCREENID 	int,
@CARRIERID int,
@TABID int
)
AS
BEGIN

SELECT TABNAME FROM QUESTIONTABMASTER WHERE TABID = @TABID and  SCREENID = @SCREENID

END


GO

