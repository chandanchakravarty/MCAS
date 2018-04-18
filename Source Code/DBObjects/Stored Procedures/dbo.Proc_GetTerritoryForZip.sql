IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTerritoryForZip]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTerritoryForZip]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name          : Dbo.Proc_GetTerritoryForZip    
Created by           : Pradeep
Date                    : 11/10/2005    
Purpose               :     
Revison History :    
Used In                :   Wolverine      

------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE   PROCEDURE Proc_GetTerritoryForZip    
(    
 	@ZIP_ID VARCHAR(10),    
 	@CUSTOMER_ID     int,                    
  	@APP_ID     int ,                    
  	@APP_VERSION_ID     smallint    
)    
AS    
BEGIN

DECLARE @TERR NVarChar(50)
DECLARE @STATE Int
DECLARE @LOB Int
DECLARE @APP_EFFECTIVE_DATE DATETIME
SELECT @STATE = STATE_ID,
	@LOB = APP_LOB,
	@APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE
	FROM APP_LIST
WHERE CUSTOMER_ID = @CUSTOMER_ID AND
	APP_ID = @APP_ID AND
	APP_VERSION_ID = @APP_VERSION_ID
    
SELECT @TERR = TERR FROM MNT_TERRITORY_CODES 
	WHERE ZIP = @ZIP_ID 
	AND LOBID = @LOB    AND
	STATE =  @STATE AND @APP_EFFECTIVE_DATE BETWEEN ISNULL(EFFECTIVE_FROM_DATE,'01/01/2000') AND ISNULL(EFFECTIVE_TO_DATE,'12/31/3000')

RETURN @TERR

END    

GO

