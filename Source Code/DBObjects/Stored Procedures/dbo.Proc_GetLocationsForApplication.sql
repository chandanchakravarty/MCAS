IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLocationsForApplication]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLocationsForApplication]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 
/*          
----------------------------------------------------------              
Proc Name       : dbo.Proc_GetLocationsForApplication          
Created by      : Pradeep            
Date            : 13 May,2005              
Purpose         : Selects the location for an application          
Revison History :              
Used In         : Wolverine     
    
Modified By     : Mohit Gupta    
Modified On     : 25/10/2005    
Purpose         : Selecting only those locations which are not used & added new parameter @DWELLING_ID. 
                
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------             
*/          
          
CREATE     PROCEDURE Proc_GetLocationsForApplication          
(          
 @CUSTOMER_ID Int,          
 @APP_ID Int,          
 @APP_VERSION_ID smallint,  
 @DWELLING_ID  int=null          
)          
          
As          
          
SELECT AL.LOCATION_ID,
       convert(varchar(10),AL.LOCATION_ID) + '^' + convert(varchar(10),isnull(AL.LOCATION_TYPE,'0')) as LOCATIONID,      
       CONVERT(VARCHAR(20), AL.LOC_NUM) + ' - ' +            
       ISNULL(AL.LOC_ADD1,'') + ' ' + ISNULL(AL.LOC_ADD2,'') + ', ' +           
       ISNULL(AL.LOC_CITY,'') + ', ' +           
       ISNULL(SL.STATE_NAME,'') + ' ' +           
       ISNULL(AL.LOC_ZIP,'')       
       
 as Address,          
/* Added by nidhi. we need this field for subject of insurance screen */          
       CONVERT(VARCHAR(20), AL.LOC_NUM) + ' - ' +            
       ISNULL(AL.LOC_ADD1,'') + ' ' + ISNULL(AL.LOC_ADD2,'') + ', ' +           
       ISNULL(AL.LOC_CITY,'') + ', ' +           
       ISNULL(SL.STATE_NAME,'') + ' ' +           
       ISNULL(AL.LOC_ZIP,'') as Address1        
/* ---- */          
         
FROM APP_LOCATIONS AL          
LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST SL ON          
 AL.LOC_COUNTRY = SL.COUNTRY_ID AND          
 AL.LOC_STATE = SL.STATE_ID          
WHERE AL.CUSTOMER_ID = @CUSTOMER_ID AND          
      AL.APP_ID = @APP_ID AND          
      AL.APP_VERSION_ID = @APP_VERSION_ID AND          
      AL.IS_ACTIVE = 'Y' AND    
      AL.LOCATION_ID NOT IN (SELECT DISTINCT LOCATION_ID     
                              FROM APP_DWELLINGS_INFO     
                              WHERE CUSTOMER_ID=@CUSTOMER_ID AND     
                                    APP_ID=@APP_ID AND                              
        APP_VERSION_ID=@APP_VERSION_ID)    
UNION  
SELECT AL.LOCATION_ID,
       convert(varchar(10),AL.LOCATION_ID) + '^' + convert(varchar(10),isnull(AL.LOCATION_TYPE,'0')) as LOCATIONID,           
       CONVERT(VARCHAR(20), AL.LOC_NUM) + ' - ' +            
       ISNULL(AL.LOC_ADD1,'') + ' ' + ISNULL(AL.LOC_ADD2,'') + ', ' +           
       ISNULL(AL.LOC_CITY,'') + ', ' +           
       ISNULL(SL.STATE_NAME,'') + ' ' +           
       ISNULL(AL.LOC_ZIP,'')       
       
 as Address,          
/* Added by nidhi. we need this field for subject of insurance screen */          
       CONVERT(VARCHAR(20), AL.LOC_NUM) + ' - ' +            
       ISNULL(AL.LOC_ADD1,'') + ' ' + ISNULL(AL.LOC_ADD2,'') + ', ' +           
       ISNULL(AL.LOC_CITY,'') + ', ' +           
       ISNULL(SL.STATE_NAME,'') + ' ' +           
       ISNULL(AL.LOC_ZIP,'') as Address1        
/* ---- */          
         
FROM APP_LOCATIONS AL          
LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST SL ON          
 AL.LOC_COUNTRY = SL.COUNTRY_ID AND          
 AL.LOC_STATE = SL.STATE_ID          
WHERE AL.CUSTOMER_ID = @CUSTOMER_ID AND          
      AL.APP_ID = @APP_ID AND          
      AL.APP_VERSION_ID = @APP_VERSION_ID AND          
      AL.IS_ACTIVE = 'Y'     
AND    
    AL.LOCATION_ID IN (SELECT DISTINCT LOCATION_ID     
                              FROM APP_DWELLINGS_INFO     
                              WHERE CUSTOMER_ID=@CUSTOMER_ID AND     
                                    APP_ID=@APP_ID AND                              
        APP_VERSION_ID=@APP_VERSION_ID AND DWELLING_ID=@DWELLING_ID )  
  
  
  
  
  
 
  
  
      
        
        
      
    
    
    
  







GO

