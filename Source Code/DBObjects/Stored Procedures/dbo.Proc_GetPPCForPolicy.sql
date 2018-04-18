IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPPCForPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPPCForPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------              
Proc Name          : Dbo.Proc_GetPPCForPolicy              
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
-- drop procedure dbo.Proc_GetPPCForPolicy              
CREATE   PROCEDURE dbo.Proc_GetPPCForPolicy            
(              
 @CUSTOMER_ID  int,                
 @POL_ID  int,                
 @POL_VERSION_ID  int,              
 @DWELLING_ID int                
)              
AS              
BEGIN              
DECLARE @ADD1 AS NVARCHAR(100)            
DECLARE @CITY AS NVARCHAR(50)            
DECLARE @STATE AS NVARCHAR(10)            
DECLARE @ZIP AS NVARCHAR(10)             
             
SELECT @ADD1=ISNULL(PL.LOC_ADD1,'') ,                  
       @CITY=ISNULL(PL.LOC_CITY,'') ,            
       @STATE=ISNULL(SL.STATE_CODE,'') ,            
       @ZIP=ISNULL(PL.LOC_ZIP,'')                     
                       
FROM POL_LOCATIONS PL                        
LEFT OUTER JOIN MNT_COUNTRY_STATE_LIST SL ON                        
 PL.LOC_COUNTRY = SL.COUNTRY_ID AND                        
 PL.LOC_STATE = SL.STATE_ID                        
WHERE PL.CUSTOMER_ID = @CUSTOMER_ID AND                        
      PL.POLICY_ID = @POL_ID AND                        
      PL.POLICY_VERSION_ID = @POL_VERSION_ID AND                        
      PL.IS_ACTIVE = 'Y'                   
AND                  
    PL.LOCATION_ID IN (SELECT DISTINCT LOCATION_ID                   
                              FROM POL_DWELLINGS_INFO                   
                              WHERE CUSTOMER_ID=@CUSTOMER_ID AND                   
                                    POLICY_ID = @POL_ID AND                        
							POLICY_VERSION_ID = @POL_VERSION_ID AND DWELLING_ID=@DWELLING_ID 
						)                 
            
--modify by Pravesh Call a common Proc for App and Pol
exec Proc_GetPPCForAppPol @ADD1,@CITY,@STATE,@ZIP 
          
END            
            
          
        
      
      
      
    
  
  
  








GO

