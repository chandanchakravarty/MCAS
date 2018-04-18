IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteUserData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteUserData]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------
Proc Name      		: dbo.Proc_GetScreensAssociated
Created by      	: Manabendra Roy
Date            	: 07/01/2005
Purpose    	  	: Delete User Defined Data
Revison History :
Used In 	        : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC dbo.Proc_DeleteUserData
@CUSTOMERID INT,
@APPID INT,
@APPVERID INT,
@TABID   INT,
@SCREENID INT
AS
BEGIN

DELETE from QUESTIONPREPARE where CLIENTID =@CUSTOMERID  AND TABID =  @TABID AND APPLICATIONID=@APPID
 AND APPVERSIONID=@APPVERID AND SCREENID=@SCREENID
END



GO

