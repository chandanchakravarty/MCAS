IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_fnGetTabDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_fnGetTabDetails]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_fnGetTabDetails
Created by      : Nidhi
Date            : 01/06/2005
Purpose         : To get tab details
Revison History :
Used In         :   Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_fnGetTabDetails
(
@SCREENID 	int
 
)
AS
BEGIN
		 SELECT DISTINCT TABID, TABNAME,SEQNO
				 FROM QUESTIONTABMASTER				  
				 WHERE SCREENID = @SCREENID  		


END


GO

