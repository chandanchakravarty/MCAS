IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPPCForApplication]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPPCForApplication]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                  
Proc Name          : Dbo.Proc_GetPPCForApplication                  
Created by           : Mohit Agarwal                  
Date                    : 17-Jan-2007                
Purpose               :                   
Revison History :                  
Modified by			:P K Chandel
Modified Date		:25 Sep08
Purpose				: Move Common Code to new Proc and Modify Logic

Used In                :   Wolverine                    
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
-- drop procedure dbo.Proc_GetPPCForApplication                
CREATE   PROCEDURE dbo.Proc_GetPPCForApplication              
(                  
 @CUSTOMER_ID  int,                    
 @APP_ID  int,                    
 @APP_VERSION_ID  int,                  
 @DWELLING_ID int                    
)                  
AS                  
BEGIN                  
            
DECLARE @ADD1 AS NVARCHAR(100)                
DECLARE @CITY AS NVARCHAR(50)                
DECLARE @STATE AS NVARCHAR(10)                
DECLARE @ZIP AS NVARCHAR(10)                 
                 
SELECT @ADD1=ISNULL(AL.LOC_ADD1,'') ,                      
       @CITY=ISNULL(AL.LOC_CITY,'') ,                
       @STATE=ISNULL(SL.STATE_CODE,'') ,                
       @ZIP=ISNULL(AL.LOC_ZIP,'')                         
                           
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
--modify by Pravesh Call a common Proc for App and Pol
exec Proc_GetPPCForAppPol @ADD1,@CITY,@STATE,@ZIP 

END                
                
      
    
  
  
  









GO

