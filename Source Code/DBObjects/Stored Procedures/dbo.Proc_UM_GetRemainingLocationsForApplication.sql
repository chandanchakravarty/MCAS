IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UM_GetRemainingLocationsForApplication]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UM_GetRemainingLocationsForApplication]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE      PROCEDURE Proc_UM_GetRemainingLocationsForApplication        
(        
 @CUSTOMER_ID Int,        
 @APP_ID Int,        
 @APP_VERSION_ID smallint        
)        
        
As        
        
        
 SELECT AL.LOCATION_ID,        
  CONVERT(VARCHAR(20),AL.CLIENT_LOCATION_NUMBER) + ' - ' +            
  ISNULL(AL.ADDRESS_1,'') + ' ' + ISNULL(AL.ADDRESS_2,'') + ', ' +           
  ISNULL(AL.CITY,'') + ', ' +           
         CONVERT(VARCHAR(20),ISNULL(AL.STATE,'')) + ' ' +           
         ISNULL(AL.ZIPCODE,'')    
       
   as Address--,           
 FROM APP_UMBRELLA_REAL_ESTATE_LOCATION AL        
 LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST SL ON        
    AL.STATE = SL.STATE_ID        
 WHERE AL.CUSTOMER_ID = @CUSTOMER_ID AND        
       AL.APP_ID = @APP_ID AND        
       AL.APP_VERSION_ID = @APP_VERSION_ID AND        
       AL.IS_ACTIVE = 'Y' AND           
   AL.LOCATION_ID NOT  IN    
  (        
  SELECT        
   LOCATION_ID FROM APP_UMBRELLA_DWELLINGS_INFO  
   WHERE CUSTOMER_ID =  @CUSTOMER_ID AND  
 APP_ID = @APP_ID AND  
 APP_VERSION_ID = @APP_VERSION_ID AND
 IS_ACTIVE='Y'
  
)         
        



GO

