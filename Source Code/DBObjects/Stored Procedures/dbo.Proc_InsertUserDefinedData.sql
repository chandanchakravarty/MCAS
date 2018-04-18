IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertUserDefinedData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertUserDefinedData]
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
CREATE   PROC dbo.Proc_InsertUserDefinedData
@QID INT,
@HQGRIDID INT,
@VQGRIDID INT,
@ANSWER VARCHAR(1000),
@OPTIONVALUE VARCHAR(1000),
@CLIENTID INT,
@LISTID INT,
@OPTIONID VARCHAR(1000),
@DESCANSWER VARCHAR(1000),
@TABID INT,
@APPLICATIONID INT,
@APPVERSIONID INT,
@CARRIERID INT,
@SCREENID INT
AS
BEGIN
INSERT INTO QUESTIONPREPARE(
	QID,
	HQUESGRIDOPTID,
	VQUESGRIDOPTID,
	ANSWER,
	OPTIONVALUE,
	CLIENTID,
	LISTID,
	OPTIONID,
	DESCANSWER,
	TABID,
	APPLICATIONID,
	APPVERSIONID,
	CARRIERID,
	SCREENID)
Values(
	@QID,
	@HQGRIDID,
	@VQGRIDID,
	@ANSWER,
	@OPTIONVALUE,
	@CLIENTID,
	@LISTID,
	@OPTIONID,
	@DESCANSWER,
	@TABID,
	@APPLICATIONID,
	@APPVERSIONID,
	@CARRIERID,
	@SCREENID)		
END



GO

