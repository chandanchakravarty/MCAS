  
  /*----------------------------------------------------------                        
Proc Name       : dbo.Proc_DeletePolicyVehicle        
Created by      :   
Date            :   
Purpose         : Delete vehicle    
Revison History :                        
Used In         : Ebix Advantage Web  
        
  
Modified By  : Lalit Chauhan   
Modified Date : Oct 05,2010  
Purpose   : Delete Vehicle Id from remuneration when vehicle deleted   
   
                       
------------------------------------------------------------                        
Date     Review By          Comments          
DROP PROC Proc_DeletePolicyVehicle  
                
------   ------------       -------------------------*/        
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePolicyVehicle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePolicyVehicle]
GO   
CREATE  PROCEDURE [dbo].[Proc_DeletePolicyVehicle]                  
(                  
 @CUSTOMER_ID  INT,                  
 @POLICY_ID  INT,                  
 @POLICY_VERSION_ID INT,                  
 @VEHICLE_ID INT              
)                  
AS                  
BEGIN                  
              
DELETE FROM POL_VEHICLE_COVERAGES                   
WHERE               
CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID                  
                  
DELETE FROM POL_VEHICLE_ENDORSEMENTS                   
WHERE               
CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID                  
                  
DELETE FROM POL_ADD_OTHER_INT                   
WHERE               
CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID                      
                  
DELETE FROM POL_MISCELLANEOUS_EQUIPMENT_VALUES   WHERE        
CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID                      
        
DELETE FROM POL_VEHICLES                   
WHERE               
CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID                      
          
DELETE FROM POL_DRIVER_ASSIGNED_VEHICLE                   
WHERE               
CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID                      
         
--set vehicle_id of driver to null wherever the vehicle is assigned            
 UPDATE POL_DRIVER_DETAILS SET VEHICLE_ID=NULL                  
 WHERE            
 CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND VEHICLE_ID = @VEHICLE_ID                      
    
--Modified By Lalit on Oct 05,2010    
--Delete Vehicle id from remuneration    
DELETE FROM POL_REMUNERATION   
WHERE               
CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND RISK_ID = @VEHICLE_ID      

DELETE FROM POL_PRODUCT_COVERAGES   
WHERE               
CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND RISK_ID = @VEHICLE_ID               
    
  /* Praveen Kasana : Itrack 5964 :If there are no other cars on the policy that are eligible for Multi Car    
        Discount then change this field to Not Applicable and     
        remove/do not apply the Multi Car Discount     
  11918 - Not applicable - NOT IN    
  11919 - Other car on this policy    
  11920 - Other Policy with Wolverine NOT IN     
    
  If “Other Car on Policy”: Discount will not be applicable on single Vehicle:    
  Then we have to check number of Private passenger in this policy if more than one then we will give Multi car discount,      
  (except for Camper and Travel Trailer & Utility Trailer) ,otherwise no.*/    
 DECLARE @UTILITY_TRAILER INT     
 SET @UTILITY_TRAILER = 11337    
    
 DECLARE @CAMPER_TRAVEL_TRAILER INT    
 SET @CAMPER_TRAVEL_TRAILER  = 11870    
    
 DECLARE @TRAILER INT    
    SET @TRAILER = 11341    
    
    
    
    
    DECLARE @COUNT int    
     
 SELECT @COUNT = COUNT(*) from POL_VEHICLES with(nolock)    
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID    
    AND MULTI_CAR ='11919'     
    AND (    
  ISNULL(APP_VEHICLE_PERTYPE_ID,0) NOT IN (@UTILITY_TRAILER,@CAMPER_TRAVEL_TRAILER)     
  AND     
  ISNULL(APP_VEHICLE_COMTYPE_ID,0) NOT IN (@TRAILER))    
     AND ISNULL(IS_ACTIVE,'') = 'Y'    
    
    
      IF(@COUNT =1)    
    
  BEGIN    
   IF(SELECT ISNULL(MULTI_CAR,'') FROM POL_VEHICLES WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID    
    and MULTI_CAR ='11919'     
      AND (    
    ISNULL(APP_VEHICLE_PERTYPE_ID,0) NOT IN (@UTILITY_TRAILER,@CAMPER_TRAVEL_TRAILER)     
    AND     
    ISNULL(APP_VEHICLE_COMTYPE_ID,0) NOT IN (@TRAILER)    
    )    
    AND ISNULL(IS_ACTIVE,'') = 'Y') = '11919'    
   BEGIN    
    
    UPDATE POL_VEHICLES SET MULTI_CAR = '11918' WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID    
    and MULTI_CAR ='11919'     
    AND (    
     ISNULL(APP_VEHICLE_PERTYPE_ID,0) NOT IN (@UTILITY_TRAILER,@CAMPER_TRAVEL_TRAILER)     
     AND     
     ISNULL(APP_VEHICLE_COMTYPE_ID,0) NOT IN (@TRAILER)    
     )    
    AND ISNULL(IS_ACTIVE,'') = 'Y'    
   END    
  END    
     
          
--Call to proc to set the value at gen info table when there are vehicles having amount>30000          
--exec  Proc_MotorGreaterAmountRulePolicy @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID          
END                  
                  
                              
    
    
              
            
          
        
      
    
    
    