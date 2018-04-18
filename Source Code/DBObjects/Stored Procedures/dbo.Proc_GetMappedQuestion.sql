IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMappedQuestion]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMappedQuestion]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------
Proc Name      : dbo.Proc_GetMappedQuestion
Created by      	: Manabendra Roy
Date            	: 07/01/2005
Purpose    	  	: Retrieving Mapped Questions
Revison History :
Used In 	        : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC dbo.Proc_GetMappedQuestion
@CARRIERID INT,
@SCREENID INT
AS
BEGIN

SELECT CAST(UQ.QID as VARCHAR) + '^' + CAST(QEM.QUESMAPPINGID as VARCHAR) QID,UPPER(QEM.MAPPINGNAME + '--' + SUBSTRING(UQ.QDESC,0,50)) MAPPING FROM  USERQUESTIONS  UQ 
INNER JOIN QUESTIONENTITYMAPPING QEM ON QEM.QUESMAPPINGID=UQ.QUESMAPPING 
WHERE QUESMAPPING IS NOT NULL AND UQ.ISACTIVE='Y' AND  QEM.ISACTIVE='Y'  AND UQ.CARRIERID=@CARRIERID AND UQ.SCREENID=@SCREENID  AND TABID IN (SELECT TABID FROM QUESTIONTABMASTER WHERE SCREENID=@SCREENID AND ISACTIVE='Y')

END



GO

