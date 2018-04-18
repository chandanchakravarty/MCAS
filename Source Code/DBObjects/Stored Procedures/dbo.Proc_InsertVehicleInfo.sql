IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertVehicleInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertVehicleInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*========================================================================      
Proc Name       : dbo.VehicleInfo                                
Created by      : NIDHI                                
Date            : 4/28/2005                                
Purpose       :Insert                                
Revison History :                                
Used In        : Wolverine                                
                                
Modified By : Anurag Verma                                
Modidied On : 20/09/2005                                
Purpose : Personal vehicle info screen is merged with vehicle info                                
                                
Modified By : Vijay Arora                                
Modidied On : 10-10-2005                                
Purpose : To add the vehicle use personal or commerical and depending on it                                
          class and vehicle type.                                
                              
Modified By : Vijay Arora                                
Modidied On : 13-10-2005                                
Purpose : To update the insured vehicle number when record saved.                              
                            
Modified By : Mohit                             
Modified On :21/10/2005                            
Purpose     :Changing parameter names as field names are changes.                            
                        
Modified By : Sumit Chhabra                             
Modified On :08/11/2005                            
Purpose     :Modified the parameter value Annual Mileage from (18,2) to (18,0)                        
                      
Modified By : Sumit Chhabra                             
Modified On :21/11/2005                            
Purpose     :Modified the parameter value is_active to take default value of 'Y'                      
Modified By : Sumit Chhabra                             
Modified On :29/12/2005                            
Purpose     :Modified the parameters              
            
Modified By : Pradeep                             
Modified On :06/02/2006                            
Purpose     :Copied Policy level coverages from other vehicle           
Modified By : Pravesh k chandel
Modified On :12 sep 2008
Purpose     :do not Copy Policy level coverages from other vehicle    if vehicle tyrp is trailer
                 
------------------------------------------------------------                                
Date     Review By          Comments                                
------   ------------       -------------------------*/  
-- DROP proc dbo.Proc_InsertVehicleInfo                               
CREATE PROC dbo.Proc_InsertVehicleInfo                                
(                                
                                 
 @CUSTOMER_ID     int,                                
 @APP_ID     int ,                                
 @APP_VERSION_ID     smallint,                                
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
 @TERRITORY nvarchar(10),                                
 @CLASS     nvarchar(150),                                
-- @ANTI_LCK_BRAKES NVARCHAR(5)=null,     
 @ANTI_LOCK_BRAKES NVARCHAR(5)=null,                                
 @REGN_PLATE_NUMBER     nvarchar(100),                                
 @MOTORCYCLE_TYPE int,                                 
 @ST_AMT_TYPE     nvarchar(10),                                
 @VEHICLE_TYPE int,                                
 @AMOUNT     decimal =null,                                
 @SYMBOL     int =null,                                
 @VEHICLE_AGE     decimal(9)=null,        
 @CREATED_BY     int,                                
 @CREATED_DATETIME     datetime,                                
                                
 -- Modified by Anurag Verma 20/09/2005                                
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
 @IS_ACTIVE varchar(2)='Y',                      
                                 
-- @UNINS_MOTOR_INJURY_COVE  nchar(5)=null,                                
-- @UNINS_PROPERTY_DAMAGE_COVE  nchar(5)=null,                                
-- @UNDERINS_MOTOR_INJURY_COVE  nchar(5)=null,                                
                             
-- field name changed by mohit                               
 -- Modified by Vijay Arora 10-10-2005                                
                             
-- old field names                             
-- @APP_USE_VEHICLE_ID INT,                                  
 @USE_VEHICLE INT,                                  
-- @APP_VEHICLE_PERCLASS_ID INT,                                  
 @CLASS_PER INT,                                  
-- @APP_VEHICLE_COMCLASS_ID INT,                                  
 @CLASS_COM INT,                                  
-- @APP_VEHICLE_PERTYPE_ID INT,                                  
 @VEHICLE_TYPE_PER INT,                                  
-- @APP_VEHICLE_COMTYPE_ID INT                                   
 @VEHICLE_TYPE_COM INT,      
 @BUSS_PERM_RESI INT  =null,      
@SNOWPLOW_CONDS int = null,      
@CAR_POOL int = null,    
--@SAFETY_BELT INT = NULL,    
@AUTO_POL_NO VARCHAR(10) = NULL,  
@RADIUS_OF_USE INT = NULL,  
@TRANSPORT_CHEMICAL varchar(10)=null,  
@COVERED_BY_WC_INSU varchar(10)=null,  
@CLASS_DESCRIPTION varchar(10)=null ,                         
-- new field names                               
-- @USE_VEHICLE   int = NULL,                                
-- @CLASS_PER int = NULL,                                
-- @CLASS_COM int = NULL,                                
-- @VEHICLE_TYPE_PER  int = NULL,                                
-- @VEHICLE_TYPE_COM  int = NULL                               
                            
--end                              
@IS_SUSPENDED int =null
)                          
AS                                
BEGIN                                
                              
DECLARE @TEMP_INSURED_VEHICLE_NUMBER INT                              
DECLARE @STATE_ID Int          
          
SELECT  @STATE_ID = STATE_ID          
FROM APP_LIST          
WHERE CUSTOMER_ID = @CUSTOMER_ID AND          
 APP_ID = @APP_ID AND          
 APP_VERSION_ID = @APP_VERSION_ID          
                            
SELECT @VEHICLE_ID=isnull(max(VEHICLE_ID),0)+1 from APP_VEHICLES WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_VERSION_ID=@APP_VERSION_ID and APP_ID=@APP_ID                                
                              
-- modified by vj on 13-10-2005                              
SELECT  @TEMP_INSURED_VEHICLE_NUMBER = (isnull(MAX(INSURED_VEH_NUMBER),0)) + 1 FROM APP_VEHICLES WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID              
                                
-- Get the mapping unique id for the anti_lck_brakes                                
--declare @LookUpUniqueID nvarchar(10)                                 
                                
/*if (@ANTI_LCK_BRAKES is not null)                                
 begin                                
  SELECT   @LookUpUniqueID =  MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID                                
  FROM         MNT_LOOKUP_TABLES INNER JOIN                          
                        MNT_LOOKUP_VALUES ON MNT_LOOKUP_TABLES.LOOKUP_ID = MNT_LOOKUP_VALUES.LOOKUP_ID                                
  WHERE     (MNT_LOOKUP_TABLES.LOOKUP_NAME ='%ALB') AND (MNT_LOOKUP_VALUES.LOOKUP_VALUE_ID = @ANTI_LCK_BRAKES)                                
 end          
else              
 set @LookUpUniqueID=@ANTI_LCK_BRAKES                                 
*/                                
INSERT INTO APP_VEHICLES                                 
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
                                
-- modified by Vijay Arora on 10-10-2005                                
                                
USE_VEHICLE,                                
CLASS_PER,                                
CLASS_COM,                                
VEHICLE_TYPE_PER,                                
VEHICLE_TYPE_COM,      
BUSS_PERM_RESI,      
SNOWPLOW_CONDS,      
CAR_POOL,    
--SAFETY_BELT,   
AUTO_POL_NO,  
RADIUS_OF_USE,  
TRANSPORT_CHEMICAL,  
COVERED_BY_WC_INSU,  
CLASS_DESCRIPTION  ,
IS_SUSPENDED,
IS_UPDATED                
)                                
VALUES                                
(                                
@CUSTOMER_ID,                                
@APP_ID,                                
@APP_VERSION_ID,                                
@VEHICLE_ID,                                
-- modified by vj on 13-10-2005                              
-- @INSURED_VEH_NUMBER                              
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
--@ANTI_LCK_BRAKES,                                
@ANTI_LOCK_BRAKES,                                
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
                                
                                
-- Modified by Vijay Arora on 10-10-2005             
                                
--@USE_VEHICLE,                                
--@CLASS_PER,                                
--@CLASS_COM,                                
--@VEHICLE_TYPE_PER,                                
--@VEHICLE_TYPE_COM                             
                          
                          
--@APP_USE_VEHICLE_ID,                                  
@USE_VEHICLE,                  
                  
--@APP_VEHICLE_PERCLASS_ID,                                  
@CLASS_PER,                  
--@APP_VEHICLE_COMCLASS_ID,                                  
@CLASS_COM,                  
--@APP_VEHICLE_PERTYPE_ID,                                  
@VEHICLE_TYPE_PER,                  
--@APP_VEHICLE_COMTYPE_ID                             
@VEHICLE_TYPE_COM,      
@BUSS_PERM_RESI,      
@SNOWPLOW_CONDS,      
@CAR_POOL,    
--@SAFETY_BELT,    
@AUTO_POL_NO,  
@RADIUS_OF_USE,  
@TRANSPORT_CHEMICAL,  
@COVERED_BY_WC_INSU,  
@CLASS_DESCRIPTION ,   
@IS_SUSPENDED,
10964                               
)                
		
--Copy Policy level vehicles from any other vehicle if it exists--          
IF (ISNULL(@VEHICLE_TYPE_PER,'0') NOT IN ('11870','11337','11618')  
	AND isnull(@VEHICLE_TYPE_COM,0) NOT IN('11341')
	AND isnull(@IS_SUSPENDED,0) !=10963
	) 
BEGIN        
	EXEC Proc_COPY_POLICY_LEVEL_COVERAGES_APP               
	 @CUSTOMER_ID,
	 @APP_ID,
	 @APP_VERSION_ID,
	 @VEHICLE_ID
	------------------   End of policy level 
END
/*
IF((SELECT COUNT(VEHICLE_ID) FROM APP_VEHICLES WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_VERSION_ID=@APP_VERSION_ID and APP_ID=@APP_ID) > 1)
	BEGIN
		UPDATE APP_VEHICLES 
			SET MULTI_CAR='11919'
		WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_VERSION_ID=@APP_VERSION_ID and APP_ID=@APP_ID AND VEHICLE_ID NOT IN (@VEHICLE_ID)
	END
*/
/* Code moved to Proc   
IF ( EXISTS             
	(            
	SELECT TOP 1 * FROM             
	APP_VEHICLE_COVERAGES  WITH(NOLOCK)          
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
	APP_ID = @APP_ID AND            
	APP_VERSION_ID = @APP_VERSION_ID AND         
	VEHICLE_ID <> @VEHICLE_ID            
	)            
  AND  ISNULL(@VEHICLE_TYPE_PER,0) NOT IN ('11870','11337','11618')  AND ISNULL(@VEHICLE_TYPE_COM,0) NOT IN ('11341') -- CONDITION ADDED BY PRAVESH 12 SEP08 iTRACK 4536
  AND isnull(@IS_SUSPENDED,0)!=10963 
)
BEGIN            
  DECLARE @TEMP_ERROR Int            
  SET @TEMP_ERROR = 0            
           
   SELECT  AVC.* INTO #APP_VEHICLE_COVERAGES             
  FROM APP_VEHICLE_COVERAGES AVC   WITH(NOLOCK)         
  INNER JOIN MNT_COVERAGE C ON            
   AVC.COVERAGE_CODE_ID = C.COV_ID             
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
   APP_ID = @APP_ID AND            
   APP_VERSION_ID = @APP_VERSION_ID AND            
   C.COVERAGE_TYPE = 'PL' AND            
   VEHICLE_ID =             
   (            
    SELECT TOP 1 VEHICLE_ID FROM             
    APP_VEHICLES    WITH(NOLOCK)        
    WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
     APP_ID = @APP_ID AND            
     APP_VERSION_ID = @APP_VERSION_ID AND            
     VEHICLE_ID <> @VEHICLE_ID
	AND ISNULL(VEHICLE_TYPE_PER,'0') NOT IN ('11870','11337','11618') 
	AND ISNULL(VEHICLE_TYPE_COM,'0')  NOT IN ('11341')
	AND isnull(IS_SUSPENDED,0)!=10963    
   )             
    SET @TEMP_ERROR = @@ERROR            
      UPDATE #APP_VEHICLE_COVERAGES             
     SET VEHICLE_ID = @VEHICLE_ID            
        SET @TEMP_ERROR = @@ERROR            
              
       SELECT * FROM #APP_VEHICLE_COVERAGES            
      print(@@rowcount)            
                         
      INSERT INTO APP_VEHICLE_COVERAGES             
      SELECT * FROM #APP_VEHICLE_COVERAGES                                  
       SET @TEMP_ERROR = @@ERROR      
--INSERT ASSOCIATED ENDORSEMENT --ADDED BY pravesh on 26 aug 2008
 SELECT PVE.* INTO  #APP_VEHICLE_ENDORSEMENTS 
	 FROM APP_VEHICLE_ENDORSEMENTS  PVE
	 INNER JOIN  MNT_ENDORSMENT_DETAILS MED ON MED.ENDORSMENT_ID=PVE.ENDORSEMENT_ID
	 WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
	  APP_ID = @APP_ID AND        
	  APP_VERSION_ID = @APP_VERSION_ID AND        
	  VEHICLE_ID <> @VEHICLE_ID   
	AND SELECT_COVERAGE IN (SELECT COVERAGE_CODE_ID FROM #APP_VEHICLE_COVERAGES)
	AND ISNULL(MED.ENDORS_ASSOC_COVERAGE,'')='Y'
	AND VEHICLE_ID =         
	(        
		SELECT TOP 1 VEHICLE_ID FROM         
		APP_VEHICLES        
		WHERE CUSTOMER_ID = @CUSTOMER_ID AND        
        APP_ID = @APP_ID AND        
	    APP_VERSION_ID = @APP_VERSION_ID AND        
		VEHICLE_ID <> @VEHICLE_ID  
		AND ISNULL(VEHICLE_TYPE_PER,'0') NOT IN ('11870','11337','11618') 
		AND ISNULL(VEHICLE_TYPE_COM,'0')  NOT IN ('11341') 
		AND isnull(IS_SUSPENDED,0)!=10963      
	)         
	UPDATE #APP_VEHICLE_ENDORSEMENTS SET VEHICLE_ID = @VEHICLE_ID  

	INSERT INTO APP_VEHICLE_ENDORSEMENTS
	 SELECT * FROM #APP_VEHICLE_ENDORSEMENTS 
    SET @TEMP_ERROR = @@ERROR 
	DROP TABLE #APP_VEHICLE_ENDORSEMENTS                             
--END HERE 
     SET @TEMP_ERROR = @@ERROR            
      DROP TABLE #APP_VEHICLE_COVERAGES  
     SET @TEMP_ERROR = @@ERROR            
             
  IF (   @TEMP_ERROR  <> 0 )            
  BEGIN            
    RAISERROR ('Unable to copy Policy Level Coverages', 16, 1)            
    RETURN            
  END            
  
END            
  */      
------------------                 
                                
END                        











GO

