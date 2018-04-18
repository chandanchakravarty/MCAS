IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyVehicleInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyVehicleInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ============================================================================================  
Proc Name       : dbo.Proc_InsertPolicyVehicleInfo                    
Created by      : Vijay Arora                    
Date            : 03-11-2005                    
Purpose         : To Insert the Information of Policy Vehicle                    
Revison History :                            
Created by      : Sumit Chhabra                  
Date            : 08-11-2005                    
Purpose         : Modified the parameter value Annual Mileage from (18,2) to (18,0)                  
Used In        : Wolverine               
        
Modified BY     : Shafi                  
Date            : 16-03-2006        
Purpose         : Add the Field For Seat Belt                 
Used In         : Wolverine             
                     
============================================================                           
Date     Review By          Comments                            
======   =========       ===================================             
*/    
--Drop PROC dbo.Proc_InsertPolicyVehicleInfo                           
CREATE PROC [dbo].[Proc_InsertPolicyVehicleInfo]                            
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
 @VEHICLE_TYPE int,                            
 @AMOUNT     decimal =null,                            
 @SYMBOL     int =null,                            
 @VEHICLE_AGE     decimal(9)=null,                            
 @CREATED_BY     int,                            
 @CREATED_DATETIME     datetime,                            
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
--@SAFETY_BELT smallint=null,        
 @BUSS_PERM_RESI INT  =null,      
 @SNOWPLOW_CONDS INT = null,      
 @CAR_POOL INT = null,      
 @AUTO_POL_NO VARCHAR(10) = NULL,    
 @RADIUS_OF_USE INT = NULL,      
 @TRANSPORT_CHEMICAL varchar(10)=null,      
 @COVERED_BY_WC_INSU varchar(10)=null,      
 @CLASS_DESCRIPTION varchar(10)=null,  
 @IS_SUSPENDED INT  = null            
)                            
AS                            
BEGIN                            
           
DECLARE @TEMP_INSURED_VEHICLE_NUMBER INT                          
 DECLARE @EBDORSEMENTPOROCESS_ID INT     
SET @EBDORSEMENTPOROCESS_ID=3                          
 DECLARE @VEHTYPE_ID Int      
DECLARE @PROCESS_ID INT  
 SELECT   @PROCESS_ID=PROCESS_ID FROM POL_POLICY_PROCESS   WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND NEW_POLICY_VERSION_ID=@POLICY_VERSION_ID                       
                         
SELECT @VEHICLE_ID=isnull(max(VEHICLE_ID),0)+1 FROM POL_VEHICLES  WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and POLICY_ID=@POLICY_ID                            
               
SELECT  @TEMP_INSURED_VEHICLE_NUMBER = (isnull(MAX(INSURED_VEH_NUMBER),0)) + 1 FROM POL_VEHICLES WITH(NOLOCK)  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                            
                            
INSERT INTO POL_VEHICLES                             
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
VEHICLE_TYPE,                            
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
APP_USE_VEHICLE_ID,                              
APP_VEHICLE_PERCLASS_ID,                              
APP_VEHICLE_COMCLASS_ID,                              
APP_VEHICLE_PERTYPE_ID,                              
APP_VEHICLE_COMTYPE_ID,        
--SAFETY_BELT,        
BUSS_PERM_RESI,      
SNOWPLOW_CONDS,      
CAR_POOL,      
AUTO_POL_NO,    
RADIUS_OF_USE,    
TRANSPORT_CHEMICAL,    
COVERED_BY_WC_INSU,    
CLASS_DESCRIPTION ,  
IS_SUSPENDED,  
IS_UPDATED   
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
@VEHICLE_TYPE,                            
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
--@UNINS_PROPERTY_DAMAGE_COVE,                            
--@UNDERINS_MOTOR_INJURY_COVE,                            
@APP_USE_VEHICLE_ID,                              
@APP_VEHICLE_PERCLASS_ID,                              
@APP_VEHICLE_COMCLASS_ID,                              
@APP_VEHICLE_PERTYPE_ID,                              
@APP_VEHICLE_COMTYPE_ID,        
--@SAFETY_BELT,        
@BUSS_PERM_RESI,      
@SNOWPLOW_CONDS,      
@CAR_POOL,      
@AUTO_POL_NO,    
@RADIUS_OF_USE,    
@TRANSPORT_CHEMICAL,    
@COVERED_BY_WC_INSU,    
@CLASS_DESCRIPTION  ,  
@IS_SUSPENDED,  
CASE WHEN @PROCESS_ID=@EBDORSEMENTPOROCESS_ID  
 THEN 10963  
 ELSE  
   10964  
 END  
)             
--Copy Policy level vehicles from any other vehicle if it exists--    
IF (ISNULL(@APP_VEHICLE_PERTYPE_ID,'0') NOT IN ('11870','11337','11618')    
 AND isnull(@APP_VEHICLE_COMTYPE_ID,'0') NOT IN('11341')  
 AND isnull(@IS_SUSPENDED,0) !=10963  
 ) -- CONDITION ADDED BY pRAVESH ON 12 SEP08 iTRACK 4536  
BEGIN          
 EXEC Proc_COPY_POLICY_LEVEL_COVERAGES                 
  @CUSTOMER_ID,--@CUSTOMER_ID     int,                      
  @POLICY_ID,--@POLICY_ID     int,                      
  @POLICY_VERSION_ID,--@POLICY_VERSION_ID     smallint,                      
  @VEHICLE_ID--@VEHICLE_ID     smallint   
 ------------------   End of policy level   
 
END 
 -- ADDED BY SONAL 12=08-2010 TO UPDATE RISKID FOR RENUMERATION>TO SHOW RENUMERATION ON THE BASIS OF RISKID
  EXEC Proc_UpdateRisk_Renumeration  
    @CUSTOMER_ID,     
	@POLICY_ID   ,    
	@POLICY_VERSION_ID,  
	@VEHICLE_ID,
	@CREATED_BY 
END                            
  
  
  
  
  
  
  
  
GO

