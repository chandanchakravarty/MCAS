IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyVehicleInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyVehicleInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*                
----------------------------------------------------------                            
Proc Name       : dbo.Proc_UpdatePolicyVehicleInformation                            
Created by      : Vijay Arora                
Date            : 03-11-2005                
Purpose         : To update the record in POL_VEHICLES table                            
Revison History :                            
Used In         :                            
Modified by      : Sumit Chhabra              
Date            : 03-11-2005                
Purpose         : Change to Annual_Mileage parameter has been made to allow for (18,0) instead of (18,2)      
      
Modified BY     : Shafi                
Date            : 16-03-2006      
Purpose         : Add the Field For Seat Belt               
Used In         : Wolverine   
Modified BY     : pRAVESH
Date            : 4 NOV 08
Purpose         : Add the Field IS SUSPENDED               
Used In         : Wolverine                      
                  
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/        
--  drop proc Proc_UpdatePolicyVehicleInformation                          
CREATE  proc dbo.Proc_UpdatePolicyVehicleInformation                            
(                            
 @CUSTOMER_ID    int,                            
 @POLICY_ID          int,                            
 @POLICY_VERSION_ID  int,                            
 @VEHICLE_ID     smallint,                            
 @INSURED_VEH_NUMBER  int,                            
 @VEHICLE_YEAR int,                            
 @MAKE nvarchar(150),                            
 @MODEL  nvarchar(150),                            
 @VIN  nvarchar(150),                            
 @BODY_TYPE  nvarchar(150),                            
 @GRG_ADD1  nvarchar(70),                            
 @GRG_ADD2  nvarchar(70),                            
 @GRG_CITY  nvarchar(80),                            
 @GRG_COUNTRY      nvarchar(10),                            
 @GRG_STATE  nvarchar(10),                            
 @GRG_ZIP  nvarchar(20),                            
 @REGISTERED_STATE  nvarchar(20),                            
 @TERRITORY  nvarchar(20),                            
 @CLASS  nvarchar(150),                            
 @REGN_PLATE_NUMBER nvarchar(150),                            
 @ST_AMT_TYPE   nvarchar(10),                            
 @VEHICLE_TYPE int,                            
 @VEHICLE_TYPE_CODE NVarChar(10)=null,                            
 @AMOUNT decimal=null,                            
 @SYMBOL int=null,                            
 @VEHICLE_AGE decimal=null,                            
 @VEHICLE_CC int =0,                            
 @MOTORCYCLE_TYPE int =0,                            
 @IS_ACTIVE  nchar(1)=null,                            
 @MODIFIED_BY     int,                            
 @LAST_UPDATED_DATETIME  datetime,                            
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
 @ANTI_LOCK_BRAKES nvarchar(5)=null,                            
-- @UNINS_MOTOR_INJURY_COVE  nchar(5)=null,                            
-- @UNINS_PROPERTY_DAMAGE_COVE  nchar(5)=null,                            
-- @UNDERINS_MOTOR_INJURY_COVE  nchar(5)=null,                             
 @APP_USE_VEHICLE_ID INT,                          
 @APP_VEHICLE_PERCLASS_ID INT,                          
 @APP_VEHICLE_COMCLASS_ID INT,           
 @APP_VEHICLE_PERTYPE_ID INT,                          
 @APP_VEHICLE_COMTYPE_ID INT,      
 --@SAFETY_BELT smallint=null,      
 @BUSS_PERM_RESI INT  = null,    
 @SNOWPLOW_CONDS INT = null,    
 @CAR_POOL INT = null,    
 @AUTO_POL_NO VARCHAR(10) = NULL,  
 @RADIUS_OF_USE INT = NULL,    
 @TRANSPORT_CHEMICAL varchar(10)=null,    
 @COVERED_BY_WC_INSU varchar(10)=null,    
 @CLASS_DESCRIPTION varchar(10)=null,
 @IS_SUSPENDED      INT=NULL 
)                            
AS                            
BEGIN 
DECLARE @EBDORSEMENTPOROCESS_ID INT   
SET @EBDORSEMENTPOROCESS_ID=3                        
 DECLARE @VEHTYPE_ID Int    
DECLARE @PROCESS_ID INT
	SELECT   @PROCESS_ID=PROCESS_ID FROM POL_POLICY_PROCESS   WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND NEW_POLICY_VERSION_ID=@POLICY_VERSION_ID                     
 UPDATE POL_VEHICLES             
 SET                               
  VEHICLE_YEAR=@VEHICLE_YEAR,                            
  MAKE=@MAKE,                            
  MODEL=@MODEL,                            
  VIN=@VIN,                            
  BODY_TYPE=@BODY_TYPE,                            
  GRG_ADD1=@GRG_ADD1,                            
  GRG_ADD2=@GRG_ADD2,                            
  GRG_CITY=@GRG_CITY,                            
  GRG_COUNTRY=@GRG_COUNTRY,                            
  GRG_STATE=@GRG_STATE,                            
  GRG_ZIP=@GRG_ZIP,                            
  REGISTERED_STATE=@REGISTERED_STATE,                    
  TERRITORY=@TERRITORY,                            
  CLASS=@CLASS,                            
  REGN_PLATE_NUMBER=@REGN_PLATE_NUMBER,                            
  ST_AMT_TYPE=@ST_AMT_TYPE,                     
  VEHICLE_TYPE = @VEHICLE_TYPE,                            
  AMOUNT=@AMOUNT,                            
  SYMBOL=@SYMBOL,                            
  VEHICLE_AGE=@VEHICLE_AGE,                
  VEHICLE_CC=@VEHICLE_CC,                            
  MOTORCYCLE_TYPE=@MOTORCYCLE_TYPE,                            
--  IS_ACTIVE  =@IS_ACTIVE,                            
  MODIFIED_BY  =@MODIFIED_BY,                            
  LAST_UPDATED_DATETIME =@LAST_UPDATED_DATETIME,                            
  IS_OWN_LEASE=@IS_OWN_LEASE,                            
  PURCHASE_DATE=@PURCHASE_DATE,                            
  IS_NEW_USED=@IS_NEW_USED,                          
  MILES_TO_WORK=@MILES_TO_WORK,                            
  VEHICLE_USE=@VEHICLE_USE,                            
  VEH_PERFORMANCE=@VEH_PERFORMANCE,                            
  MULTI_CAR=@MULTI_CAR,                            
  ANNUAL_MILEAGE=@ANNUAL_MILEAGE,                            
  PASSIVE_SEAT_BELT=@PASSIVE_SEAT_BELT,                            
  AIR_BAG=@AIR_BAG,                            
  ANTI_LOCK_BRAKES=@ANTI_LOCK_BRAKES,                            
--  UNINS_MOTOR_INJURY_COVE=@UNINS_MOTOR_INJURY_COVE,                            
--  UNINS_PROPERTY_DAMAGE_COVE=@UNINS_PROPERTY_DAMAGE_COVE,                            
--  UNDERINS_MOTOR_INJURY_COVE=@UNDERINS_MOTOR_INJURY_COVE,                            
  APP_USE_VEHICLE_ID = @APP_USE_VEHICLE_ID,                          
  APP_VEHICLE_PERCLASS_ID = @APP_VEHICLE_PERCLASS_ID,                          
  APP_VEHICLE_COMCLASS_ID = @APP_VEHICLE_COMCLASS_ID,                          
  APP_VEHICLE_PERTYPE_ID = @APP_VEHICLE_PERTYPE_ID,                          
  APP_VEHICLE_COMTYPE_ID = @APP_VEHICLE_COMTYPE_ID,      
  --SAFETY_BELT            = @SAFETY_BELT,      
 BUSS_PERM_RESI = @BUSS_PERM_RESI,    
 SNOWPLOW_CONDS=@SNOWPLOW_CONDS,    
 CAR_POOL = @CAR_POOL,    
 AUTO_POL_NO = @AUTO_POL_NO,  
 RADIUS_OF_USE = @RADIUS_OF_USE,  
 TRANSPORT_CHEMICAL = @TRANSPORT_CHEMICAL,  
 COVERED_BY_WC_INSU = @COVERED_BY_WC_INSU,  
 CLASS_DESCRIPTION = @CLASS_DESCRIPTION  ,
 IS_SUSPENDED	=@IS_SUSPENDED,
 IS_UPDATED =10964
  WHERE                            
  CUSTOMER_ID  =@CUSTOMER_ID AND                            
  POLICY_ID   =@POLICY_ID AND                            
  POLICY_VERSION_ID =@POLICY_VERSION_ID AND                 
  VEHICLE_ID  =@VEHICLE_ID         
IF(@PROCESS_ID=@EBDORSEMENTPOROCESS_ID)
	BEGIN
		UPDATE POL_VEHICLES
			SET IS_UPDATED=10963
		WHERE	CUSTOMER_ID  =@CUSTOMER_ID AND                            
				POLICY_ID   =@POLICY_ID AND                            
				POLICY_VERSION_ID =@POLICY_VERSION_ID AND                 
				VEHICLE_ID  =@VEHICLE_ID   
	END
                   
END                          
              
            
          
        
      
      
      
    
  








GO

