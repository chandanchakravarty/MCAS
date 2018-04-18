IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolVehicleCount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolVehicleCount]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                            
Proc Name       : dbo.Proc_GetPolVehicleCount                            
Created by      : swastika                            
Date            : 23rd Aug'06                            
Purpose         :                            
Revison History :                            
Used In  : Wolverine                            
--Modified :  Praveen kasana
Date :  22 jan 2009
Itrack 5310
            
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/        
-- drop proc dbo.Proc_GetPolVehicleCount                        
CREATE PROC dbo.Proc_GetPolVehicleCount        
(                            
@CUSTOMER_ID     int,                            
@POLICY_ID     int ,                            
@POLICY_VERSION_ID     smallint,  
@VEHICLE_ID smallint=null--,  
--@Mode varchar(5) = null -- 'ADD', 'EDIT', 'GET'  
)                            
AS          
BEGIN                        
		DECLARE @COUNT INT                 
		DECLARE @COUNT_PERSONAL INT	
		DECLARE @COUNT_COMM INT	
		SET @COUNT_PERSONAL = 0
		SET @COUNT_COMM = 0
   
  SELECT @COUNT_PERSONAL = COUNT(*) FROM POL_VEHICLES (NOLOCK)    
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
  POLICY_ID = @POLICY_ID AND    
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
  ISNULL(IS_ACTIVE,'N') = 'Y' AND  
  ISNULL(APP_USE_VEHICLE_ID,0) =   11332  --personal
  AND ISNULL(APP_VEHICLE_PERTYPE_ID,0) !=11870 AND ISNULL(APP_VEHICLE_PERTYPE_ID,0) != 11337 


 SELECT @COUNT_COMM = COUNT(*) FROM POL_VEHICLES (NOLOCK)    
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
  POLICY_ID = @POLICY_ID AND    
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
  ISNULL(IS_ACTIVE,'N') = 'Y' AND  
  ISNULL(APP_USE_VEHICLE_ID,0) =   11333 --comm
  AND ISNULL(APP_VEHICLE_COMCLASS_ID,0) !=11341 AND ISNULL(APP_VEHICLE_COMCLASS_ID,0) !=11340  	
  
  
  SET @COUNT = @COUNT_PERSONAL + @COUNT_COMM
  

   
  /* IF @CNT = 1  
     
   UPDATE POL_VEHICLES  
   SET MULTI_CAR = '11918' -- NOT APPLICABLE  
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
    POLICY_ID = @POLICY_ID AND    
    POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
    APP_VEHICLE_PERTYPE_ID =   '11334'   -- PPA  
   AND  VEHICLE_ID = @VEHICLE_ID  
     
   ELSE  
   UPDATE POL_VEHICLES  
   SET MULTI_CAR = '11919' -- OTHER CAR ON THIS POLICY  
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND    
    POLICY_ID = @POLICY_ID AND    
    POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
    APP_VEHICLE_PERTYPE_ID =   '11334' -- PPA  
   AND  VEHICLE_ID = @VEHICLE_ID  
 --    END  */
   
      RETURN @COUNT  
  
END      
  





GO

