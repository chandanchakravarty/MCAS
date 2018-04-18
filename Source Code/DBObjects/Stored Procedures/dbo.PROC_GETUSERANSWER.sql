IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_GETUSERANSWER]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_GETUSERANSWER]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO





/*----------------------------------------------------------
Proc Name      		: dbo.PROC_GETUSERANSWER
Created by      	: Manabendra Roy
Date            	: 07/01/2005
Purpose    	  	: Updated Mapped Questions
Revison History :
Used In 	        : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE    PROC DBO.PROC_GETUSERANSWER
@CARRIERID INT,
@SCREENID INT,
@TABID INT,
@CLIENTID INT,
@APPID INT,
@APPVERID INT,
@QID INT,
@HQOPTIONID INT,
@VQOPTIONID INT
AS
BEGIN

	Select OPTIONVALUE from QUESTIONPREPARE 			
	Where QID =@QID AND HQUESGRIDOPTID = @HQOPTIONID AND VQUESGRIDOPTID = @VQOPTIONID AND CLIENTID= @CLIENTID
	AND TABID =@TABID AND APPLICATIONID = @APPID AND APPVERSIONID =@APPVERID 
	AND CARRIERID = @CARRIERID AND SCREENID=@SCREENID

END




GO

