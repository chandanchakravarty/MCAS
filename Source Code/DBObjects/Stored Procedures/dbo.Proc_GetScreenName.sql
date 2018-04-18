IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetScreenName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetScreenName]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetScreenName
Created by      : Nidhi
Date            : 30/05/2005
Purpose         : To get screen name
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetScreenName
(
@SCREENID 	int,
@CARRIERID int
)
AS
BEGIN

SELECT SCREENNAME FROM ONLINESCREENMASTER  WHERE  SCREENID = @SCREENID

END


GO

