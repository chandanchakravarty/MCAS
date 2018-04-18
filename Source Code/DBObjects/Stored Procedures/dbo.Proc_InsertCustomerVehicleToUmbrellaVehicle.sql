IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCustomerVehicleToUmbrellaVehicle]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCustomerVehicleToUmbrellaVehicle]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
    
       
/*----------------------------------------------------------          
Proc Name       : dbo.Proc_InsertCustomerVehicleToUmbrellaVehicle          
Created by      : Mohit          
Date            : 09/05/2005          
Purpose         : To copy from  APP_UMBRELLA_VEHICLE_INFO to APP_UMBRELLA_VEHICLE_INFO and to APP_UMBRELLA_VEHICLE_COV_IFNO.          
Revison History :          
Used In         :   Wolverine          
        
Modified By : Vijay Arora        
Modified Date : 13-10-2005        
Purpose  : To also copy the vehicle use, class and type fields.       
      
Modified By : Mohit Gupta      
Modified Date : 24-10-2005        
Purpose  : Changing fields names to (USE_VEHICLE,CLASS_PER,CLASS_COM,VEHICLE_TYPE_PER,VEHICLE_TYPE_COM )      
          from(APP_USE_VEHICLE_ID,APP_VEHICLE_PERCLASS_ID,APP_VEHICLE_COMCLASS_ID,      
   APP_VEHICLE_PERTYPE_ID,APP_VEHICLE_COMTYPE_ID)      
    
Modified By  : Ravindra Gupta     
Modified Date  : 04-17-2006    
Purpose   : To remove code for copying coverages as Vechicle Coverages screen is removed from     
    umbrella vehicle    
    
       
------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       -------------------------*/          
--drop proc Proc_InsertCustomerVehicleToUmbrellaVehicle          
CREATE   PROCEDURE dbo.Proc_InsertCustomerVehicleToUmbrellaVehicle          
(          
                       
 @FROM_CUSTOMER_ID int,          
 @TO_CUSTOMER_ID int,          
 @FROM_APP_ID int,          
 @TO_APP_ID int,          
 @FROM_APP_VERSION_ID int,          
 @TO_APP_VERSION_ID int,          
 @FROM_VEHICLE_ID int,           
 @COVERAGE_TO_BE_COPY  Char(1) ='N',  --DEFAULT VALUE 'N' MEANS COVERAGE DETAILS NOT TO BE COPY.            
             @CREATED_BY_USER_ID  int 
)          
AS          
BEGIN          
Declare @To_Vehicle_Id int     -- CONTAINS THE RUNING NUMBER FOR VEHICLE_ID          
Declare @To_Vehicle_Num int    -- CONTAINS THE RUNING NUMBER FOR VEHICLE number          
    
DECLARE @IS_ACTIVE CHAR(1)                        
 SET @IS_ACTIVE='Y'       
          
SELECT  @To_Vehicle_Id = ISNULL(MAX(VEHICLE_ID),0) + 1 ,          
@To_Vehicle_Num =  ISNULL(MAX(INSURED_VEH_NUMBER),0) + 1           
                               FROM           APP_UMBRELLA_VEHICLE_INFO           
                                WHERE       CUSTOMER_ID=@TO_CUSTOMER_ID          
                   AND              APP_ID=@TO_APP_ID          
                                AND             APP_VERSION_ID=@TO_APP_VERSION_ID             
          
          
          
INSERT INTO APP_UMBRELLA_VEHICLE_INFO          
(          
CUSTOMER_ID,APP_ID,APP_VERSION_ID,VEHICLE_ID,          
INSURED_VEH_NUMBER,VEHICLE_YEAR,MAKE,MODEL,VIN,BODY_TYPE,GRG_ADD1,GRG_ADD2,GRG_CITY,          
GRG_COUNTRY,GRG_STATE,GRG_ZIP,REGISTERED_STATE,TERRITORY,CLASS,REGN_PLATE_NUMBER,          
ST_AMT_TYPE,AMOUNT,SYMBOL,VEHICLE_AGE,IS_OWN_LEASE,PURCHASE_DATE,IS_NEW_USED,AMOUNT_COST_NEW,          
MILES_TO_WORK,VEHICLE_USE,VEH_PERFORMANCE,MULTI_CAR,ANNUAL_MILEAGE,PASSIVE_SEAT_BELT,          
AIR_BAG,ANTI_LOCK_BRAKES,P_SURCHARGES,DEACTIVATE_REACTIVATE_DATE,IS_ACTIVE,CREATED_BY,          
CREATED_DATETIME,USE_VEHICLE,CLASS_PER,CLASS_COM,VEHICLE_TYPE_PER,VEHICLE_TYPE_COM,UNINS_MOTOR_INJURY_COVE,      
UNINS_PROPERTY_DAMAGE_COVE,UNDERINS_MOTOR_INJURY_COVE,MOTORCYCLE_TYPE      
)          
SELECT          
@TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@To_Vehicle_Id,          
@To_Vehicle_Num, VEHICLE_YEAR,MAKE,MODEL,VIN,BODY_TYPE,GRG_ADD1,GRG_ADD2,          
GRG_CITY,GRG_COUNTRY,GRG_STATE,GRG_ZIP,REGISTERED_STATE,TERRITORY,CLASS,          
REGN_PLATE_NUMBER,ST_AMT_TYPE,AMOUNT,SYMBOL,VEHICLE_AGE,IS_OWN_LEASE,PURCHASE_DATE,          
IS_NEW_USED,AMOUNT_COST_NEW,MILES_TO_WORK,VEHICLE_USE,VEH_PERFORMANCE,MULTI_CAR,          
ANNUAL_MILEAGE,PASSIVE_SEAT_BELT,AIR_BAG,ANTI_LOCK_BRAKES,P_SURCHARGES,DEACTIVATE_REACTIVATE_DATE,          
@IS_ACTIVE,@CREATED_BY_USER_ID,GETDATE(),        
USE_VEHICLE,CLASS_PER,CLASS_COM,VEHICLE_TYPE_PER,VEHICLE_TYPE_COM,UNINS_MOTOR_INJURY_COVE,      
UNINS_PROPERTY_DAMAGE_COVE,UNDERINS_MOTOR_INJURY_COVE,MOTORCYCLE_TYPE  
         
FROM       APP_UMBRELLA_VEHICLE_INFO          
WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID          
AND         APP_ID=@FROM_APP_ID          
AND         APP_VERSION_ID=@FROM_APP_VERSION_ID          
AND         VEHICLE_ID=@FROM_VEHICLE_ID          
        
    
----- Commented By Ravindra (04-17-2006)    
/*          
 IF ( @COVERAGE_TO_BE_COPY = 'Y')  -- COPY ONLY IF  'Y' IS PASSED AS PARAMETER.          
  BEGIN          
   INSERT INTO APP_UMBRELLA_VEHICLE_COV_IFNO          
   (          
   CUSTOMER_ID,APP_ID,APP_VERSION_ID,VEHICLE_ID,COVERAGE_ID,          
   COVERAGE_CODE_ID,LIMIT_1,LIMIT_2,          
   DEDUCTIBLE_1,WRITTEN_PREMIUM,FULL_TERM_PREMIUM          
   )          
   SELECT          
   @TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@To_Vehicle_Id,COVERAGE_ID,          
   COVERAGE_CODE_ID,LIMIT_1,LIMIT_2,          
   DEDUCTIBLE_1,WRITTEN_PREMIUM,FULL_TERM_PREMIUM          
   FROM       APP_UMBRELLA_VEHICLE_COV_IFNO          
   WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID          
   AND         APP_ID=@FROM_APP_ID          
   AND         APP_VERSION_ID=@FROM_APP_VERSION_ID          
   AND         VEHICLE_ID=@FROM_VEHICLE_ID          
            
  END      */    
END          
        
      
      
      
    
  



GO

