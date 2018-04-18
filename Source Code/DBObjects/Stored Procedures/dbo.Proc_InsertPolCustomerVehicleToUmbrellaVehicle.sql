IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolCustomerVehicleToUmbrellaVehicle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolCustomerVehicleToUmbrellaVehicle]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
      
        
           
/*----------------------------------------------------------              
Proc Name       : dbo.Proc_InsertPolCustomerVehicleToUmbrellaVehicle              
Created by      : Mohit              
Date            : 09/05/2005              
Purpose         : To copy from  POL_UMBRELLA_VEHICLE_INFO to POL_UMBRELLA_VEHICLE_INFO and to APP_UMBRELLA_VEHICLE_COV_IFNO.              
Revison History :              
Used In         :   Wolverine              
            
    
           
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/              
--drop proc Proc_InsertPolCustomerVehicleToUmbrellaVehicle              
CREATE   PROCEDURE dbo.Proc_InsertPolCustomerVehicleToUmbrellaVehicle              
(              
                           
 @FROM_CUSTOMER_ID int,              
 @TO_CUSTOMER_ID int,              
 @FROM_POLICY_ID int,              
 @TO_POLICY_ID int,              
 @FROM_POLICY_VERSION_ID int,              
 @TO_POLICY_VERSION_ID int,              
 @FROM_VEHICLE_ID int,               
 @CREATED_BY_USER_ID  int,  
 @NEW_VEH_ID int output     
)              
AS              
BEGIN              
Declare @To_Vehicle_Id int     -- CONTAINS THE RUNING NUMBER FOR VEHICLE_ID              
Declare @To_Vehicle_Num int    -- CONTAINS THE RUNING NUMBER FOR VEHICLE number              
        
DECLARE @IS_ACTIVE CHAR(1)                            
 SET @IS_ACTIVE='Y'           
              
SELECT  @To_Vehicle_Id = ISNULL(MAX(VEHICLE_ID),0) + 1 ,@To_Vehicle_Num =  ISNULL(MAX(INSURED_VEH_NUMBER),0) + 1               
 FROM           POL_UMBRELLA_VEHICLE_INFO               
 WHERE       CUSTOMER_ID=@TO_CUSTOMER_ID AND POLICY_ID=@TO_POLICY_ID AND POLICY_VERSION_ID=@TO_POLICY_VERSION_ID                 
              
            
INSERT INTO POL_UMBRELLA_VEHICLE_INFO              
(              
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,VEHICLE_ID,              
 INSURED_VEH_NUMBER,VEHICLE_YEAR,MAKE,MODEL,VIN,BODY_TYPE,GRG_ADD1,GRG_ADD2,GRG_CITY,              
 GRG_COUNTRY,GRG_STATE,GRG_ZIP,REGISTERED_STATE,TERRITORY,CLASS,REGN_PLATE_NUMBER,              
 ST_AMT_TYPE,AMOUNT,SYMBOL,VEHICLE_AGE,IS_OWN_LEASE,PURCHASE_DATE,IS_NEW_USED,AMOUNT_COST_NEW,              
 MILES_TO_WORK,VEHICLE_USE,VEH_PERFORMANCE,MULTI_CAR,ANNUAL_MILEAGE,PASSIVE_SEAT_BELT,              
 AIR_BAG,ANTI_LOCK_BRAKES,P_SURCHARGES,DEACTIVATE_REACTIVATE_DATE,IS_ACTIVE,CREATED_BY,              
 CREATED_DATETIME,USE_VEHICLE,CLASS_PER,CLASS_COM,VEHICLE_TYPE_PER,VEHICLE_TYPE_COM,UNINS_MOTOR_INJURY_COVE,          
 UNINS_PROPERTY_DAMAGE_COVE,UNDERINS_MOTOR_INJURY_COVE,MOTORCYCLE_TYPE          
)              
SELECT              
 @TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@To_Vehicle_Id,              
 @To_Vehicle_Num, VEHICLE_YEAR,MAKE,MODEL,VIN,BODY_TYPE,GRG_ADD1,GRG_ADD2,              
 GRG_CITY,GRG_COUNTRY,GRG_STATE,GRG_ZIP,REGISTERED_STATE,TERRITORY,CLASS,              
 REGN_PLATE_NUMBER,ST_AMT_TYPE,AMOUNT,SYMBOL,VEHICLE_AGE,IS_OWN_LEASE,PURCHASE_DATE,              
 IS_NEW_USED,AMOUNT_COST_NEW,MILES_TO_WORK,VEHICLE_USE,VEH_PERFORMANCE,MULTI_CAR,              
 ANNUAL_MILEAGE,PASSIVE_SEAT_BELT,AIR_BAG,ANTI_LOCK_BRAKES,P_SURCHARGES,DEACTIVATE_REACTIVATE_DATE,              
 @IS_ACTIVE,@CREATED_BY_USER_ID,GETDATE(),            
 USE_VEHICLE,CLASS_PER,CLASS_COM,VEHICLE_TYPE_PER,VEHICLE_TYPE_COM,UNINS_MOTOR_INJURY_COVE,          
 UNINS_PROPERTY_DAMAGE_COVE,UNDERINS_MOTOR_INJURY_COVE,MOTORCYCLE_TYPE               
FROM           
 POL_UMBRELLA_VEHICLE_INFO              
WHERE       
 CUSTOMER_ID=@FROM_CUSTOMER_ID AND POLICY_ID=@FROM_POLICY_ID AND POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID              
 AND VEHICLE_ID=@FROM_VEHICLE_ID              
    
END              
            
          
          
          
        
      
    
  



GO

