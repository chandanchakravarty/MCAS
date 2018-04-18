IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertUmbrellaVehicleInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertUmbrellaVehicleInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                              
Proc Name       : dbo.Proc_InsertUmbrellaVehicleInfo                              
Created by      : Mohit                              
Date            : 5/27/2005                              
Purpose     :Insert                              
Revison History :                              
Used In  : Wolverine                              
                              
Modified By : Vijay Arora                              
Modified Date : 11-10-2005                              
Purpose  : To set the vehicle use, class and type.                              
                            
Modified By : Vijay Arora                              
Modified Date : 13-10-2005                              
Purpose  : To set the insured vehicle number when the record get saved.                           
                          
Modified By : Mohit                          
Modified On : 21-10-2005                          
Purpose     : Changing field names.                           
Modified By : Sumit Chhabra                  
Modified On : 10-11-2005                          
Purpose     : Modified the parameter Annual_Mileage to take decimal(24,0) instead of real values                  
                
Modified By : Sumit Chhabra                  
Modified On : 21-11-2005                          
Purpose     : Modified the parameter is_active to take default value of 'Y'                
Modified By : Sumit Chhabra                  
Modified On : 29-12-2005                          
Purpose     : Modified the parameter is_active to take default value of 'Y'                
APP_USE_VEHICLE_ID, ---------------------->>>>>>>>USE_VEHICLE                             
ANTI_LCK_BRAKES ---------->>>>>>>>>>>>>>>>>      ANTI_LOCK_BRAKES              
APP_VEHICLE_PERCLASS_ID---------->>>>>>>>>>>>>>CLASS_PER              
APP_VEHICLE_COMCLASS_ID ------------>>>>>>>>>>> CLASS_COM               
APP_VEHICLE_PERTYPE_ID------->>>>>>>>>>VEHICLE_TYPE_PER              
APP_VEHICLE_COMTYPE_ID-------->>>>>>>>>>>VEHICLE_TYPE_COM              
              
-- Drop PROC  Proc_InsertUmbrellaVehicleInfo  
------------------------------------------------------------                              
Date     Review By          Comments                              
------   ------------       -------------------------*/                              
CREATE PROC Dbo.Proc_InsertUmbrellaVehicleInfo                              
(                              
@CUSTOMER_ID     int,                              
@APP_ID     int ,                              
@APP_VERSION_ID     smallint,                              
@VEHICLE_ID     smallint output,                              
@INSURED_VEH_NUMBER     smallint,                              
@VEHICLE_YEAR     int,                              
@MAKE     nvarchar(70),                              
@MODEL     nvarchar(70),                              
@VIN     nvarchar(17),                              
@BODY_TYPE     nvarchar(70),                              
@GRG_ADD1     nvarchar(70),                              
@GRG_ADD2     nvarchar(70),                              
@GRG_CITY     nvarchar(40),                              
@GRG_COUNTRY     nvarchar(10),                              
@GRG_STATE     nvarchar(10),                              
@GRG_ZIP     nvarchar(20),                              
@REGISTERED_STATE     nvarchar(10),                              
@TERRITORY     nvarchar(10),                              
@CLASS     nvarchar(150),                              
@REGN_PLATE_NUMBER     nvarchar(20),                              
@ST_AMT_TYPE     nvarchar(5),       
@AMOUNT     decimal(18,0) =null,                              
@SYMBOL     int=null,                              
@VEHICLE_AGE    decimal(9),                        
@IS_ACTIVE     nchar(2)='Y',             
@CREATED_BY     int,                              
@CREATED_DATETIME     datetime=null,               
@MODIFIED_BY     int,                              
@LAST_UPDATED_DATETIME     datetime,                              
@VEHICLE_TYPE int,                              
                          
-- Commented by mohit on 21-10-2005.                          
-- field names are changed in respective table                          
-- added by vj                              
@USE_VEHICLE int,                              
@CLASS_PER int,                              
@CLASS_COM int,                              
@VEHICLE_TYPE_PER int,                              
@VEHICLE_TYPE_COM int,                           
                           
-- @USE_VEHICLE   int ,                              
-- @CLASS_PER int,                              
-- @CLASS_COM int,                              
-- @VEHICLE_TYPE_PER  int,                              
-- @VEHICLE_TYPE_COM  int                             
--new parameters being added by Sumit on Oct 24,2005                      
@IS_OWN_LEASE nvarchar(20),                      
@PURCHASE_DATE datetime,                      
@IS_NEW_USED nchar(2),                      
@VEHICLE_USE nvarchar(10),                      
@MULTI_CAR nvarchar(10),                      
@SAFETY_BELT nvarchar(5) = null,          
@ANNUAL_MILEAGE decimal(24,0),                      
@PASSIVE_SEAT_BELT nvarchar(10),                      
@AIR_BAG nvarchar(10),                      
@ANTI_LOCK_BRAKES nvarchar(10),                    
--@UNINS_MOTOR_INJURY_COVE nchar(10),                    
--@UNINS_PROPERTY_DAMAGE_COVE  nchar(10),                    
--@UNDERINS_MOTOR_INJURY_COVE nchar(10),            
@MILES_TO_WORK varchar(5),    
@CALLED_FOR  varchar(20),     
@IS_EXCLUDED int,                   
@OTHER_POLICY nvarchar(150)                          
--end                            
                          
                          
                             
)                              
AS                              
BEGIN                              
                            
 DECLARE @TEMP_INSURED_VEH_NUMBER INT                            
                             
 SELECT @VEHICLE_ID=isnull(max(VEHICLE_ID),0)+1 FROM APP_UMBRELLA_VEHICLE_INFO  WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_VERSION_ID=@APP_VERSION_ID and APP_ID=@APP_ID;     
                           
 SELECT  @TEMP_INSURED_VEH_NUMBER =  (isnull(MAX(INSURED_VEH_NUMBER),0)) +1                             
 FROM    APP_UMBRELLA_VEHICLE_INFO    WITH(NOLOCK)                           
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID     
      
  if(@CALLED_FOR!='Motor Home')                            
    BEGIN                             
 INSERT INTO APP_UMBRELLA_VEHICLE_INFO                              
 (                              
 CUSTOMER_ID,                              
 APP_ID,                              
 APP_VERSION_ID,                              
 VEHICLE_ID,                              
 INSURED_VEH_NUMBER,                              
 VEHICLE_YEAR,                              
 MAKE,                              
 MODEL,                              
 VIN,                              
 BODY_TYPE,                              
 GRG_ADD1,                              
 GRG_ADD2,                              
 GRG_CITY,                              
 GRG_COUNTRY,                              
 GRG_STATE,                              
 GRG_ZIP,                              
 REGISTERED_STATE,                              
 TERRITORY,                              
 CLASS,                              
 REGN_PLATE_NUMBER,                              
 ST_AMT_TYPE,                              
 AMOUNT,                              
 SYMBOL,       
 VEHICLE_AGE,                              
                               
 IS_ACTIVE,                              
 CREATED_BY,                              
 CREATED_DATETIME,                              
 MODIFIED_BY,                              
 LAST_UPDATED_DATETIME,    
  MOTORCYCLE_TYPE,             
 -- added by vj                              
 --USE_VEHICLE,                              
 --CLASS_PER,                              
 --CLASS_COM,                              
 --VEHICLE_TYPE_PER,                              
 --VEHICLE_TYPE_COM                             
 USE_VEHICLE,                              
 CLASS_PER,                              
 CLASS_COM,                              
 VEHICLE_TYPE_PER,                              
 VEHICLE_TYPE_COM,                      
 --new parameters being added by Sumit on Oct 24,2005                      
 IS_OWN_LEASE,                      
 PURCHASE_DATE,                      
 IS_NEW_USED,                      
 VEHICLE_USE,                      
 MULTI_CAR,          
 SAFETY_BELT,                      
 ANNUAL_MILEAGE,                      
 PASSIVE_SEAT_BELT,                      
 AIR_BAG,                      
 ANTI_LOCK_BRAKES,                    
 --UNINS_MOTOR_INJURY_COVE,                 
 --UNINS_PROPERTY_DAMAGE_COVE,                    
 --UNDERINS_MOTOR_INJURY_COVE,            
 MILES_TO_WORK,                    
 IS_EXCLUDED,
 OTHER_POLICY                              
 )                              
 VALUES                              
 (                              
 @CUSTOMER_ID,                              
 @APP_ID,                              
 @APP_VERSION_ID,                              
 @VEHICLE_ID,                              
 @TEMP_INSURED_VEH_NUMBER,                              
 @VEHICLE_YEAR,                              
 @MAKE,                              
 @MODEL,                              
 @VIN,                              
 @BODY_TYPE,                              
 @GRG_ADD1,                              
 @GRG_ADD2,                     
 @GRG_CITY,                              
 @GRG_COUNTRY,                              
 @GRG_STATE,                              
 @GRG_ZIP,                              
 @REGISTERED_STATE,                              
 @TERRITORY,                              
 @CLASS,                              
 @REGN_PLATE_NUMBER,                              
 @ST_AMT_TYPE,                              
 @AMOUNT,                              
 @SYMBOL,                              
 @VEHICLE_AGE,                              
                             
                               
 @IS_ACTIVE,                              
 @CREATED_BY,                              
 @CREATED_DATETIME,                              
 @MODIFIED_BY,                              
 @LAST_UPDATED_DATETIME, @VEHICLE_TYPE,                              
                               
 -- added by vj                              
                               
 @USE_VEHICLE,                              
 @CLASS_PER,                              
 @CLASS_COM,                              
 @VEHICLE_TYPE_PER,                              
 @VEHICLE_TYPE_COM,                              
 --@USE_VEHICLE,                              
 --@CLASS_PER,                              
 --@CLASS_COM,                              
 --@VEHICLE_TYPE_PER,                              
 --@VEHICLE_TYPE_COM                           
 --added by sumit on oct 24,2005                      
 --new parameters being added by Sumit on Oct 24,2005                      
 @IS_OWN_LEASE,                      
 @PURCHASE_DATE,                      
 @IS_NEW_USED,                      
 @VEHICLE_USE,                      
 @MULTI_CAR,                      
 @SAFETY_BELT,          
 @ANNUAL_MILEAGE,                      
 @PASSIVE_SEAT_BELT,                      
 @AIR_BAG,                      
 @ANTI_LOCK_BRAKES,                    
 --@UNINS_MOTOR_INJURY_COVE,                    
 --@UNINS_PROPERTY_DAMAGE_COVE,                    
 --@UNDERINS_MOTOR_INJURY_COVE,            
 @MILES_TO_WORK,                    
 @IS_EXCLUDED,
 @OTHER_POLICY                              
 )    
    END      
ELSE    
    BEGIN    
  INSERT INTO APP_UMBRELLA_VEHICLE_INFO                              
 (                              
 CUSTOMER_ID,           
 APP_ID,                              
 APP_VERSION_ID,                              
 VEHICLE_ID,                              
 INSURED_VEH_NUMBER,                              
 VEHICLE_YEAR,                              
 MAKE,                              
 MODEL,                              
 VIN,                             BODY_TYPE,                              
 GRG_ADD1,                              
 GRG_ADD2,                              
 GRG_CITY,                              
 GRG_COUNTRY,                              
 GRG_STATE,                              
 GRG_ZIP,                              
 REGISTERED_STATE,                              
 TERRITORY,                              
 CLASS,                              
 REGN_PLATE_NUMBER,                              
 ST_AMT_TYPE,                              
 AMOUNT,                              
 SYMBOL,                              
 VEHICLE_AGE,                              
                               
 IS_ACTIVE,                              
 CREATED_BY,                              
 CREATED_DATETIME,                              
 MODIFIED_BY,                              
 LAST_UPDATED_DATETIME,    
 MOTORCYCLE_TYPE,                              
 -- added by vj                              
 --USE_VEHICLE,                              
 --CLASS_PER,                              
 --CLASS_COM,                              
 --VEHICLE_TYPE_PER,                              
 --VEHICLE_TYPE_COM                             
 USE_VEHICLE,                              
 CLASS_PER,                              
 CLASS_COM,                              
 VEHICLE_TYPE_PER,                              
 VEHICLE_TYPE_COM,                      
 --new parameters being added by Sumit on Oct 24,2005                      
 IS_OWN_LEASE,                      
 PURCHASE_DATE,                      
 IS_NEW_USED,                      
 VEHICLE_USE,                      
 MULTI_CAR,          
 SAFETY_BELT,                      
 ANNUAL_MILEAGE,                      
 PASSIVE_SEAT_BELT,                      
 AIR_BAG,                      
 ANTI_LOCK_BRAKES,                    
 --UNINS_MOTOR_INJURY_COVE,                 
 --UNINS_PROPERTY_DAMAGE_COVE,                    
 --UNDERINS_MOTOR_INJURY_COVE,            
 MILES_TO_WORK,  
 IS_EXCLUDED,
 OTHER_POLICY                    
                               
 )                              
 VALUES                              
 (                              
 @CUSTOMER_ID,                              
 @APP_ID,                              
 @APP_VERSION_ID,                              
 @VEHICLE_ID,                              
 @TEMP_INSURED_VEH_NUMBER,                              
 @VEHICLE_YEAR,                              
 @MAKE,                              
 @MODEL,                              
 @VIN,                              
 @BODY_TYPE,                              
 @GRG_ADD1,                              
 @GRG_ADD2,                     
 @GRG_CITY,                              
 @GRG_COUNTRY,                              
 @GRG_STATE,                              
 @GRG_ZIP,                              
 @REGISTERED_STATE,                              
 @TERRITORY,                              
 @CLASS,                              
 @REGN_PLATE_NUMBER,           
 @ST_AMT_TYPE,                              
 @AMOUNT,                              
 @SYMBOL,                              
 @VEHICLE_AGE,                              
                             
                               
 @IS_ACTIVE,                              
 @CREATED_BY,                              
 @CREATED_DATETIME,                              
 @MODIFIED_BY,                              
 @LAST_UPDATED_DATETIME, @VEHICLE_TYPE_PER,                              
                               
 -- added by vj                              
                               
 @USE_VEHICLE,                              
 @CLASS_PER,                              
 @CLASS_COM,                     
 @VEHICLE_TYPE,                              
 @VEHICLE_TYPE_COM,                              
 --@USE_VEHICLE,                              
 --@CLASS_PER,                              
 --@CLASS_COM,                              
 --@VEHICLE_TYPE_PER,                              
 --@VEHICLE_TYPE_COM                           
 --added by sumit on oct 24,2005                      
 --new parameters being added by Sumit on Oct 24,2005                      
 @IS_OWN_LEASE,                      
 @PURCHASE_DATE,                      
 @IS_NEW_USED,                      
 @VEHICLE_USE,                      
 @MULTI_CAR,                      
 @SAFETY_BELT,          
 @ANNUAL_MILEAGE,                      
 @PASSIVE_SEAT_BELT,                      
 @AIR_BAG,                      
 @ANTI_LOCK_BRAKES,                    
 --@UNINS_MOTOR_INJURY_COVE,                    
 --@UNINS_PROPERTY_DAMAGE_COVE,                    
 --@UNDERINS_MOTOR_INJURY_COVE,            
 @MILES_TO_WORK,  
 @IS_EXCLUDED,
 @OTHER_POLICY                    
                               
 )     
 END                            
END  


GO

