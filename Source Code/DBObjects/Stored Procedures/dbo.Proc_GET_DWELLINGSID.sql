IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GET_DWELLINGSID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GET_DWELLINGSID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO

/*----------------------------------------------------------                    
Proc Name   : dbo.Proc_GET_DWELLINGSID 936,169,1                  
Created by  : Ashwini                    
Date        : 13 July,2005          
Purpose     : GET THE DWELLINGS's ID                     
Revison History  :                          
 ------------------------------------------------------------                                
Date     Review By          Comments                              
                     
------   ------------       -------------------------*/       
--drop proc Proc_GET_DWELLINGSID                   
CREATE PROCEDURE dbo.Proc_GET_DWELLINGSID         
(          
 @CUSTOMER_ID int,          
 @APP_ID int,          
 @APP_VERSION_ID int          
)              
AS                   
BEGIN     
  
          
DECLARE @POLICY_TYPE VARCHAR(100)  
  
SELECT @POLICY_TYPE= MNT.LOOKUP_VALUE_DESC FROM   
MNT_LOOKUP_VALUES MNT INNER JOIN APP_LIST APP ON  
APP.POLICY_TYPE = MNT.LOOKUP_UNIQUE_ID WHERE APP.CUSTOMER_ID=@CUSTOMER_ID AND APP.APP_ID=@APP_ID AND   
APP.APP_VERSION_ID=APP_VERSION_ID  
          
SELECT     APP_DWELLINGS_INFO.DWELLING_ID,      
     ISNULL(APP_LOCATIONS.LOC_ADD1,'') + ' ' +ISNULL(APP_LOCATIONS.LOC_ADD2,'') + ' ' + ISNULL(APP_LOCATIONS.LOC_CITY,'') + ' ' + ISNULL(MNT_COUNTRY_STATE_LIST.STATE_NAME,'')+ ' ' + ISNULL(APP_LOCATIONS.LOC_ZIP,'')  
    ,@POLICY_TYPE         
FROM         APP_DWELLINGS_INFO with (nolock) INNER JOIN          
                      APP_LOCATIONS with (nolock) ON APP_DWELLINGS_INFO.CUSTOMER_ID = APP_LOCATIONS.CUSTOMER_ID AND           
                      APP_DWELLINGS_INFO.APP_ID = APP_LOCATIONS.APP_ID AND           
                      APP_DWELLINGS_INFO.APP_VERSION_ID = APP_LOCATIONS.APP_VERSION_ID AND           
                      APP_DWELLINGS_INFO.LOCATION_ID = APP_LOCATIONS.LOCATION_ID         
                      LEFT OUTER  JOIN MNT_COUNTRY_STATE_LIST with (nolock) ON APP_LOCATIONS.LOC_STATE=MNT_COUNTRY_STATE_LIST.STATE_ID      
      
   WHERE APP_DWELLINGS_INFO.CUSTOMER_ID = @CUSTOMER_ID AND APP_DWELLINGS_INFO.APP_ID=@APP_ID AND APP_DWELLINGS_INFO.APP_VERSION_ID=@APP_VERSION_ID AND APP_DWELLINGS_INFO.IS_ACTIVE='Y'    
          
   ORDER BY   APP_DWELLINGS_INFO.DWELLING_ID   
  
  
------------------  
  
 
  
  
  
End          
      
      
          
        
      
    
  


GO

