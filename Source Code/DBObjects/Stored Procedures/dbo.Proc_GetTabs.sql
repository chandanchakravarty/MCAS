IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTabs]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTabs]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_GetTabs
Created by      : Nidhi
Date            : 30/05/2005
Purpose         : To get tabs 
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_GetTabs
(
@SCREENID 	int,
@CARRIERID int
)
AS
BEGIN

SELECT TABID, TABNAME FROM QUESTIONTABMASTER WHERE SCREENID =@SCREENID  ORDER BY SEQNO

END


GO

