IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_POL_VEHICLES_FOR_POL_COVERAGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_POL_VEHICLES_FOR_POL_COVERAGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                  
Proc Name   : dbo.Proc_Get_POL_VEHICLES_FOR_POL_COVERAGES        
Created by  : Pradeep        
Date        : Feb 21, 2005    
Purpose     : Get all vehicles for App level coverages  

modified by		:Pravesh k Chandel
Modified Date	:29 Dec 2008
Purpose			: get only those vehicle where PL level Covg Applicable  
Revison History  :                        
 ------------------------------------------------------------                              
Date     Review By          Comments                            
DROP PROCEDURE dbo.Proc_Get_POL_VEHICLES_FOR_POL_COVERAGES                          
------   ------------       -------------------------*/                  
CREATE PROCEDURE dbo.Proc_Get_POL_VEHICLES_FOR_POL_COVERAGES       
(        
  @VEHICLE_ID smallint,        
  @CUSTOMER_ID int,        
  @POLICY_ID int,        
  @POLICY_VERSION_ID int  
        
)            
AS                 
BEGIN                  
-- ADDED BY pRAVESH ON 24 APRIL 09 iTRACK 5731       
DECLARE @APP_USE_VEHICLE_ID INT
 SELECT @APP_USE_VEHICLE_ID=APP_USE_VEHICLE_ID  
 FROM POL_VEHICLES  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  POLICY_ID = @POLICY_ID AND  
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
  VEHICLE_ID = @VEHICLE_ID 
-- END HERE

 SELECT VEHICLE_ID  
 FROM POL_VEHICLES  
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  
  POLICY_ID = @POLICY_ID AND  
  POLICY_VERSION_ID = @POLICY_VERSION_ID AND  
  VEHICLE_ID <> @VEHICLE_ID AND  
  ISNULL(APP_VEHICLE_PERTYPE_ID,0) NOT IN (11870,11618,11337)  AND ISNULL(APP_VEHICLE_COMTYPE_ID,0) NOT IN (11341)
  AND ISNULL(IS_SUSPENDED,0)!=10963	 
  AND ISNULL(APP_USE_VEHICLE_ID,'0')=ISNULL(@APP_USE_VEHICLE_ID ,'0')
  AND ISNULL(COMPRH_ONLY,'0')!=10963
      
END        
        
        
      
    
  
  







GO

