IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTabCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTabCount]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetTabCount
Created by      : Nidhi
Date            : 01/06/2005
Purpose         : To get tab count
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetTabCount
(
@SCREENID 	int
 
)
AS
BEGIN
 SELECT COUNT(*) AS TABCOUNT 
				 FROM QUESTIONTABMASTER
				 WHERE SCREENID = @SCREENID  		


END


GO

