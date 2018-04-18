IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCountyFromZip]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCountyFromZip]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name          : Dbo.Proc_GetCountyFromZip      
Created by           : Pradeep  
Date                    : 11/10/2005      
Purpose               :  Gets the county name from Zip code     
Revison History :      
Used In                :   Wolverine        
  
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROCEDURE Proc_GetCountyFromZip      
(      
  @ZIP_ID VARCHAR(10),      
  @CUSTOMER_ID     int,                      
   @APP_ID     int ,                      
   @APP_VERSION_ID     smallint,
   @TERR NVarChar(75) OUTPUT
)      
AS      
BEGIN  


DECLARE @STATE smallint
DECLARE @LOB Int  
  
SELECT @STATE = STATE_ID,  
 @LOB = APP_LOB  
 FROM APP_LIST  
WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
 APP_ID = @APP_ID AND  
 APP_VERSION_ID = @APP_VERSION_ID  

    
SELECT @TERR = COUNTY FROM MNT_TERRITORY_CODES   
 WHERE ZIP = @ZIP_ID   
 AND LOBID = @LOB    AND  
 STATE =  @STATE   
  

  
END


GO

