IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUnMappedQuestionList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUnMappedQuestionList]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------
Proc Name      : dbo.Proc_GetUnMappedQuestionList
Created by      	: Manabendra Roy
Date            	: 07/01/2005
Purpose    	  	: Retrieving UnMapped Questions
Revison History :
Used In 	        : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC dbo.Proc_GetUnMappedQuestionList
@CARRIERID INT,
@SCREENID INT
AS
BEGIN

SELECT QID,QDESC QSHORTDESC FROM USERQUESTIONS 
WHERE ISACTIVE='Y' AND CARRIERID=@CARRIERID AND SCREENID=@SCREENID AND TABID IN (SELECT TABID FROM QUESTIONTABMASTER WHERE SCREENID=@SCREENID AND ISACTIVE='Y') ORDER BY UPPER(QDESC)

END



GO

