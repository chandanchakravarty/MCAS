IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMappingFields]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMappingFields]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER ON
GO




/*----------------------------------------------------------
Proc Name      : dbo.Proc_GetMappingFields
Created by      	: Manabendra Roy
Date            	: 07/01/2005
Purpose    	  	: Retrieving Mapped Fields
Revison History :
Used In 	        : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROC dbo.Proc_GetMappingFields
AS
BEGIN
SELECT QUESMAPPINGID,UPPER(MAPPINGNAME) MAPPINGNAME 
FROM QUESTIONENTITYMAPPING WHERE ISACTIVE='Y' ORDER BY UPPER(MAPPINGNAME)
END



GO

