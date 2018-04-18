IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetScreensAssociated]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetScreensAssociated]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO





/*----------------------------------------------------------
Proc Name      : dbo.Proc_GetScreensAssociated
Created by      	: Manabendra Roy
Date            	: 07/01/2005
Purpose    	  	: Retrieving UnMapped Questions
Revison History :
Used In 	        : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE    PROC dbo.Proc_GetScreensAssociated
@CUSTOMERID INT,
@APPID INT,
@APPVERID INT
AS
BEGIN

SELECT DISTINCT OS.SCREENID,OS.SCREENNAME FROM APP_LIST AL
INNER JOIN ONLINESCREENMASTER OS ON AL.APP_LOB = OS.CLASSID
INNER JOIN QUESTIONTABMASTER TB ON OS.SCREENID=TB.SCREENID 
WHERE AL.CUSTOMER_ID=@CUSTOMERID AND AL.APP_ID=@APPID AND AL.APP_VERSION_ID=@APPVERID
AND OS.ISACTIVE ='Y'

END




GO

