IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteMotorCycle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteMotorCycle]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------              
              
Proc Name       : Dbo.Proc_DeleteMotorCycle        
Created by      : Sumit Chhabra              
Date            : 11/10/2005              
Purpose         : Deletion of Motorcyle Vehicle Information        
      
Created by      : Sumit Chhabra              
Date            : 10/11/2005              
Purpose         : De-assign of Motorcyle information from the drivers table also      
Revison History :              
Used In                   : Wolverine              
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/ 
-- drop PROC dbo.Proc_DeleteMotorCycle               
CREATE PROC dbo.Proc_DeleteMotorCycle        
(              
 @CUSTOMER_ID  INT,        
 @APP_ID  INT,        
 @APP_VERSION_ID INT,        
 @VEHICLE_ID  INT        
)              
AS             
BEGIN           
 IF EXISTS(SELECT VEHICLE_ID FROM APP_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND         
  APP_VERSION_ID=@APP_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID)        
 BEGIN        
  --DELETE RELATED DATA FROM OTHER(FOREIGN) TABLES        
    
 --DEELTE DATA FROM ADDITIONAL INTEREST TABLE    
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_ADD_OTHER_INT  WHERE  CUSTOMER_ID = @CUSTOMER_ID and APP_ID = @APP_ID and        
        APP_VERSION_ID = @APP_VERSION_ID and  VEHICLE_ID  = @VEHICLE_ID  )    
  DELETE FROM APP_ADD_OTHER_INT  WHERE  CUSTOMER_ID  = @CUSTOMER_ID and  APP_ID   = @APP_ID and        
         APP_VERSION_ID = @APP_VERSION_ID and  VEHICLE_ID  = @VEHICLE_ID       
        
  --DELETE DATA FROM APP_AUTO_ID_CARD_INFO TABLE        
   IF EXISTS(SELECT CUSTOMER_ID FROM APP_AUTO_ID_CARD_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND         
    APP_VERSION_ID=@APP_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID)        
        
   DELETE FROM APP_AUTO_ID_CARD_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND         
    APP_VERSION_ID=@APP_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID        
        
  --DELETE DATA FROM APP_VEHICLE_COVERAGES        
  IF EXISTS(SELECT CUSTOMER_ID FROM APP_VEHICLE_COVERAGES WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND         
    APP_VERSION_ID=@APP_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID)        
        
   DELETE FROM APP_VEHICLE_COVERAGES WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND         
    APP_VERSION_ID=@APP_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID        
        
  --DELETE DATA FROM APP_VEHICLE_ENDORSEMENTS        
  IF EXISTS(SELECT CUSTOMER_ID FROM APP_VEHICLE_ENDORSEMENTS WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND         
    APP_VERSION_ID=@APP_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID)        
        
   DELETE FROM APP_VEHICLE_ENDORSEMENTS WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND         
    APP_VERSION_ID=@APP_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID  

--DELETE DATA FROM APP_MISCELLANEOUS_EQUIPMENT_VALUES        
  IF EXISTS(SELECT CUSTOMER_ID FROM APP_MISCELLANEOUS_EQUIPMENT_VALUES WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND         
    APP_VERSION_ID=@APP_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID)        
        
   DELETE FROM APP_MISCELLANEOUS_EQUIPMENT_VALUES WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND         
    APP_VERSION_ID=@APP_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID         
        
  --FINALLY DELETE DATA FROM THE PRIMARY TABLE        
        
   DELETE FROM APP_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND         
   APP_VERSION_ID=@APP_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID 
 
   DELETE FROM APP_DRIVER_ASSIGNED_VEHICLE 
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID  AND    
   APP_VERSION_ID = @APP_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID   
  --when the vehicle has been delete, de-assign it to from driver's table also      
   UPDATE APP_DRIVER_DETAILS SET VEHICLE_ID=NULL      
     WHERE   CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND         
    APP_VERSION_ID=@APP_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID   
  
--Call to proc to set the value at gen info table when there are vehicles having amount>30000  
--The following rule has been commented as the rule will be called from the underwritting question page itself  
--exec  Proc_MotorGreaterAmountRule @customeR_id,@app_id,@app_version_id       
 END        
END       
      
    






GO

