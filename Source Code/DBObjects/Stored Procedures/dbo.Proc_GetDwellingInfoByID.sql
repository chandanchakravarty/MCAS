IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDwellingInfoByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDwellingInfoByID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop proc dbo.Proc_GetDwellingInfoByID       
 CREATE       PROCEDURE dbo.Proc_GetDwellingInfoByID      
(            
             
 @CUSTOMER_ID Int,            
 @APP_ID Int,            
 @APP_VERSION_ID SmallInt,            
 @DWELLING_ID SmallInt            
)            
            
As            
            
SELECT             
 ADI.CUSTOMER_ID,            
 ADI.APP_ID,            
 ADI.APP_VERSION_ID,            
 ADI.DWELLING_ID,            
 ADI.DWELLING_NUMBER,            
 ADI.LOCATION_ID,            
 ADI.SUB_LOC_ID,            
 ADI.YEAR_BUILT,            
 ADI.PURCHASE_YEAR,            
 floor(ADI.PURCHASE_PRICE) PURCHASE_PRICE,            
 floor(ADI.MARKET_VALUE) MARKET_VALUE,            
 floor(ADI.REPLACEMENT_COST) REPLACEMENT_COST,            
 ADI.BUILDING_TYPE,            
 ADI.OCCUPANCY,            
 ADI.NEED_OF_UNITS,            
 ADI.USAGE,            
 ADI.NEIGHBOURS_VISIBLE,            
 ADI.OCCUPIED_DAILY,            
 ADI.NO_WEEKS_RENTED,            
 ADI.IS_ACTIVE,          
 ADI.CREATED_BY,            
 ADI.CREATED_DATETIME,            
 ADI.MODIFIED_BY,            
 ADI.LAST_UPDATED_DATETIME,            
 CONVERT(VARCHAR(10),ADI.LOCATION_ID) + ',' +             
 CONVERT(VARCHAR(10),ISNULL(ADI.SUB_LOC_ID,0)) as LOC_SUBLOC    ,        
 ADI.DETACHED_OTHER_STRUCTURES,        
 ADI.MONTHS_RENTED,      
 ADI.REPLACEMENTCOST_COVA,    
CONVERT(VARCHAR(20), AL.LOC_NUM)+ '-' + ISNULL(AL.LOC_ADD1,'')+ ' ' + ISNULL(AL.LOC_ADD2,'')+ ',' + ISNULL(AL.LOC_CITY,'')+' ' +isnull((select STATE_NAME from MNT_COUNTRY_STATE_LIST where STATE_ID = AL.LOC_STATE) ,'') + ' ' + ISNULL(AL.LOC_ZIP,'') AS LOCATION,
AL.LOC_NUM
FROM   
APP_DWELLINGS_INFO  ADI    
LEFT OUTER JOIN APP_LOCATIONS AL on    
ADI.LOCATION_ID = AL.LOCATION_ID   and  
ADI.CUSTOMER_ID = AL.CUSTOMER_ID AND            
ADI.APP_ID = AL.APP_ID AND             
ADI.APP_VERSION_ID = AL.APP_VERSION_ID             
     
            
WHERE ADI.CUSTOMER_ID = @CUSTOMER_ID AND            
      ADI.APP_ID = @APP_ID AND             
      ADI.APP_VERSION_ID = @APP_VERSION_ID AND            
      ADI.DWELLING_ID = @DWELLING_ID            
            
            


GO

