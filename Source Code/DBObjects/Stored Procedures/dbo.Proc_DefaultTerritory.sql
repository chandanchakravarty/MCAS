IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DefaultTerritory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DefaultTerritory]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


/*----------------------------------------------------------
Proc Name          : Dbo.Proc_GetBodyTypeFromVINMASTER
Created by           : Mohit Gupta
Date                    : 19/05/2005
Purpose               : 
Revison History :
Used In                :   Wolverine  
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE   PROCEDURE Proc_DefaultTerritory
(
              @CUSTOMER_ID int,
	@APP_ID int,
	@APP_VERSION_ID int,
	@DWELLING_ID int

)
AS
BEGIN
SELECT A.terr TERRITORY FROM 
MNT_TERRITORY_CODES A ,
APP_DWELLINGS_INFO B ,
APP_LOCATIONS C
WHERE 
B.LOCATION_ID=C.LOCATION_ID AND
A.ZIP = C.LOC_ZIP AND 
B.CUSTOMER_ID=@CUSTOMER_ID  AND 
B.APP_ID=@APP_ID AND
B.APP_VERSION_ID=@APP_VERSION_ID AND
B.DWELLING_ID=@DWELLING_ID and A.LOBID=1
END

GO

