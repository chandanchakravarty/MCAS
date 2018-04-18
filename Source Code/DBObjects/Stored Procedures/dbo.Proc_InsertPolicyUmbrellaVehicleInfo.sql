IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyUmbrellaVehicleInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyUmbrellaVehicleInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
     
/*                    
-----------------------------------------------------------                    
Proc Name       : dbo.Proc_InsertPolicyVehicleInfo                    
Created by      : Vijay Arora                    
Date            : 03-11-2005                    
Purpose         : To Insert the Information of Policy Vehicle                    
      
                     
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------              
*/    
--drop PROC dbo.Proc_InsertPolicyUmbrellaVehicleInfo                           
CREATE PROC dbo.Proc_InsertPolicyUmbrellaVehicleInfo                            
(                            
 @CUSTOMER_ID     int,                            
 @POLICY_ID     int ,                            
 @POLICY_VERSION_ID     smallint,                            
 @VEHICLE_ID     smallint output,                            
 @INSURED_VEH_NUMBER     smallint,                            
 @VEHICLE_YEAR     int,                            
 @MAKE     nvarchar(150),                            
 @MODEL     nvarchar(150),                            
 @VIN     nvarchar(150),                            
 @BODY_TYPE     nvarchar(150),                            
 @GRG_ADD1     nvarchar(70),                            
 @GRG_ADD2     nvarchar(70),                            
 @GRG_CITY     nvarchar(80),                            
 @GRG_COUNTRY     nvarchar(10),                            
 @GRG_STATE     nvarchar(10),                            
 @GRG_ZIP     nvarchar(20),                            
 @REGISTERED_STATE     nvarchar(10),                            
 @TERRITORY     nvarchar(10),                            
 @CLASS     nvarchar(150),                            
 @ANTI_LCK_BRAKES NVARCHAR(5)=null,                            
 @REGN_PLATE_NUMBER     nvarchar(100),                            
 @MOTORCYCLE_TYPE int,                              
 @ST_AMT_TYPE     nvarchar(10),                            
-- @VEHICLE_TYPE int,                            
 @AMOUNT     decimal =null,                            
 @SYMBOL     int =null,                            
 @VEHICLE_AGE     decimal(9)=null,                            
 @CREATED_BY     int,                            
 @CREATED_DATETIME     datetime=NULL,                            
 @IS_OWN_LEASE      nvarchar(10)=null,                            
 @PURCHASE_DATE      datetime=null,                            
 @IS_NEW_USED      nchar(1)=null,                            
 @MILES_TO_WORK      nvarchar(5)=null,                            
 @VEHICLE_USE      nvarchar(5)=null,                            
 @VEH_PERFORMANCE      nvarchar(5)=null,                            
 @MULTI_CAR       nvarchar(5)=null,                            
 @ANNUAL_MILEAGE      decimal (18, 0)=null,                            
 @PASSIVE_SEAT_BELT      nvarchar(5)=null,                            
 @AIR_BAG  nvarchar(5)=null,                             
-- @UNINS_MOTOR_INJURY_COVE  nchar(5)=null,                            
-- @UNINS_PROPERTY_DAMAGE_COVE  nchar(5)=null,                            
-- @UNDERINS_MOTOR_INJURY_COVE  nchar(5)=null,                            
 @APP_USE_VEHICLE_ID INT,                              
 @APP_VEHICLE_PERCLASS_ID INT,                              
 @APP_VEHICLE_COMCLASS_ID INT,                              
 @APP_VEHICLE_PERTYPE_ID INT,                              
 @APP_VEHICLE_COMTYPE_ID INT ,        
 @SAFETY_BELT smallint=null,    
 @USE_VEHICLE int = null,
 @CALLED_FOR  varchar(20),     
 @IS_EXCLUDED int = null,  
 @OTHER_POLICY nvarchar (150) = null
                 
)                            
AS                            
BEGIN                            
                          
DECLARE @TEMP_INSURED_VEHICLE_NUMBER INT                          
                          
select @VEHICLE_ID=isnull(max(VEHICLE_ID),0)+1 from POL_UMBRELLA_VEHICLE_INFO where CUSTOMER_ID=@CUSTOMER_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and POLICY_ID=@POLICY_ID                            
               
SELECT  @TEMP_INSURED_VEHICLE_NUMBER = (isnull(MAX(INSURED_VEH_NUMBER),0)) + 1 FROM POL_UMBRELLA_VEHICLE_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                            
 if(@CALLED_FOR!='Motor Home')   
    BEGIN                            
INSERT INTO POL_UMBRELLA_VEHICLE_INFO                             
(                            
CUSTOMER_ID,               
POLICY_ID,                            
POLICY_VERSION_ID,                            
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
ANTI_LOCK_BRAKES,                            
MOTORCYCLE_TYPE,                            
REGN_PLATE_NUMBER,                            
ST_AMT_TYPE,                            
AMOUNT,                            
SYMBOL,                            
VEHICLE_AGE,                            
IS_ACTIVE,                            
CREATED_BY,                            
CREATED_DATETIME,                            
IS_OWN_LEASE,                            
PURCHASE_DATE,                            
IS_NEW_USED,                            
MILES_TO_WORK,                            
VEHICLE_USE,                            
VEH_PERFORMANCE,                            
MULTI_CAR,                            
ANNUAL_MILEAGE,                            
PASSIVE_SEAT_BELT,                            
AIR_BAG,                            
--UNINS_MOTOR_INJURY_COVE,                            
--UNINS_PROPERTY_DAMAGE_COVE,                            
--UNDERINS_MOTOR_INJURY_COVE,                            
USE_VEHICLE,                              
CLASS_PER,                              
CLASS_COM,                              
VEHICLE_TYPE_PER,                              
VEHICLE_TYPE_COM,    
IS_EXCLUDED,  
OTHER_POLICY        
)                            
VALUES                            
(                            
@CUSTOMER_ID,                            
@POLICY_ID,                            
@POLICY_VERSION_ID,                            
@VEHICLE_ID,                            
@TEMP_INSURED_VEHICLE_NUMBER,                            
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
@ANTI_LCK_BRAKES,                            
@MOTORCYCLE_TYPE,                            
@REGN_PLATE_NUMBER,                            
@ST_AMT_TYPE,                            
@AMOUNT,                            
@SYMBOL,                            
@VEHICLE_AGE,                            
'Y',                            
@CREATED_BY,                            
@CREATED_DATETIME,                            
@IS_OWN_LEASE,                            
@PURCHASE_DATE,                            
@IS_NEW_USED,                            
@MILES_TO_WORK,                            
@VEHICLE_USE,                            
@VEH_PERFORMANCE,                            
@MULTI_CAR,                            
@ANNUAL_MILEAGE,                            
@PASSIVE_SEAT_BELT,                            
@AIR_BAG,                            
--@UNINS_MOTOR_INJURY_COVE,                            
--@UNINS_PROPERTY_DAMAGE_COVE,                          --@UNDERINS_MOTOR_INJURY_COVE,                            
@USE_VEHICLE,    
@APP_VEHICLE_PERCLASS_ID,                              
@APP_VEHICLE_COMCLASS_ID,                              
@APP_VEHICLE_PERTYPE_ID,                              
@APP_VEHICLE_COMTYPE_ID,    
@IS_EXCLUDED,  
@OTHER_POLICY      
)      
  END        
ELSE      
    BEGIN      
             
  
INSERT INTO POL_UMBRELLA_VEHICLE_INFO                             
(                            
CUSTOMER_ID,               
POLICY_ID,                            
POLICY_VERSION_ID,                            
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
ANTI_LOCK_BRAKES,                            
MOTORCYCLE_TYPE,                            
REGN_PLATE_NUMBER,                            
ST_AMT_TYPE,                            
AMOUNT,                            
SYMBOL,                            
VEHICLE_AGE,                            
IS_ACTIVE,                            
CREATED_BY,                            
CREATED_DATETIME,                            
IS_OWN_LEASE,                            
PURCHASE_DATE,                            
IS_NEW_USED,                            
MILES_TO_WORK,                            
VEHICLE_USE,                            
VEH_PERFORMANCE,                            
MULTI_CAR,                            
ANNUAL_MILEAGE,                            
PASSIVE_SEAT_BELT,                            
AIR_BAG,                            
--UNINS_MOTOR_INJURY_COVE,                            
--UNINS_PROPERTY_DAMAGE_COVE,                            
--UNDERINS_MOTOR_INJURY_COVE,                            
USE_VEHICLE,                              
CLASS_PER,                              
CLASS_COM,                              
VEHICLE_TYPE_PER,                              
VEHICLE_TYPE_COM,    
IS_EXCLUDED,  
OTHER_POLICY        
)                            
VALUES                            
(                            
@CUSTOMER_ID,                            
@POLICY_ID,                            
@POLICY_VERSION_ID,                            
@VEHICLE_ID,                            
@TEMP_INSURED_VEHICLE_NUMBER,                            
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
@ANTI_LCK_BRAKES,                            
@APP_VEHICLE_PERTYPE_ID,                            
@REGN_PLATE_NUMBER,                            
@ST_AMT_TYPE,                            
@AMOUNT,                            
@SYMBOL,                            
@VEHICLE_AGE,                            
'Y',                            
@CREATED_BY,                            
@CREATED_DATETIME,                            
@IS_OWN_LEASE,                            
@PURCHASE_DATE,                            
@IS_NEW_USED,                            
@MILES_TO_WORK,                            
@VEHICLE_USE,                            
@VEH_PERFORMANCE,                            
@MULTI_CAR,                            
@ANNUAL_MILEAGE,                            
@PASSIVE_SEAT_BELT,                            
@AIR_BAG,                            
--@UNINS_MOTOR_INJURY_COVE,                            
--@UNINS_PROPERTY_DAMAGE_COVE,                          --@UNDERINS_MOTOR_INJURY_COVE,                            
@USE_VEHICLE,    
@APP_VEHICLE_PERCLASS_ID,                              
@APP_VEHICLE_COMCLASS_ID,
@MOTORCYCLE_TYPE,                               
--@APP_VEHICLE_PERTYPE_ID,                              
@APP_VEHICLE_COMTYPE_ID,    
@IS_EXCLUDED,  
@OTHER_POLICY      
)
END             
                            
END                            
                  
                
              
            
          
        
      
    
    
    
  



GO

