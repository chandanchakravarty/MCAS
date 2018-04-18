IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertMotorcycleInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertMotorcycleInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_InsertMotorcycleInfo                  
Created by      : Ebix                  
Date            : 4/28/2005                  
Purpose       :Insert                  
Revison History :                  
Used In        : Wolverine              
              
Modified By : Vijay Arora              
Modified Date : 13-10-2005              
Purpose  : To set the vehicle insured number when record get saved.              
            
Modified By : Vijay Arora              
Modified Date : 18-10-2005              
Purpose  : Added a field named APP_VEHICLE_CLASS.            
      
Modified By : Pravesh K chandel
Modified Date : 14 April 09
Purpose  : Copied policy level coverages      
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
--drop proc Proc_InsertMotorcycleInfo
CREATE PROC dbo.Proc_InsertMotorcycleInfo                  
(                  
                   
@CUSTOMER_ID     int,                  
@APP_ID     int,                  
@APP_VERSION_ID     smallint,                  
@VEHICLE_ID     smallint output,                  
@INSURED_VEH_NUMBER     smallint,                  
@VEHICLE_YEAR     int,                  
@MAKE     nvarchar(150),                  
@MODEL     nvarchar(150),                  
@VIN     nvarchar(150),                  
@GRG_ADD1     nvarchar(140),                  
@GRG_ADD2     nvarchar(140),                  
@GRG_CITY     nvarchar(80),                  
@GRG_COUNTRY     nvarchar(10),                  
@GRG_STATE     nvarchar(10),                  
@GRG_ZIP     nvarchar(20),                  
@REGISTERED_STATE     nvarchar(10),                  
@TERRITORY     nvarchar(10),                  
@AMOUNT     decimal(18,2),                  
@VEHICLE_AGE     decimal(9),                  
@VEHICLE_CC int,                  
@MOTORCYCLE_TYPE int,                  
@IS_ACTIVE     nchar(2)='Y',                  
@CREATED_BY     int,                  
@CREATED_DATETIME     datetime,                  
@MODIFIED_BY     int,                  
@LAST_UPDATED_DATETIME     datetime,            
@APP_VEHICLE_CLASS  INT,        
@SYMBOL INT,  
@CYCL_REGD_ROAD_USE int = null,
@COMPRH_ONLY int =null                
                  
)                  
AS                  
BEGIN              
              
DECLARE @TEMP_INSURED_VEH_NUMBER INT              
                  
select @VEHICLE_ID=isnull(max(VEHICLE_ID),0)+1 from APP_VEHICLES where CUSTOMER_ID=@CUSTOMER_ID and APP_VERSION_ID=@APP_VERSION_ID and APP_ID=@APP_ID;                  
              
              
SELECT   @TEMP_INSURED_VEH_NUMBER =  (isnull(MAX(INSURED_VEH_NUMBER),0)) +1               
  FROM         APP_VEHICLES                 
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID                
              
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
GRG_ADD1,                  
GRG_ADD2,                  
GRG_CITY,                  
GRG_COUNTRY,                  
GRG_STATE,                  
GRG_ZIP,                  
REGISTERED_STATE,                  
TERRITORY,                  
AMOUNT,                  
                  
VEHICLE_AGE,                  
VEHICLE_CC,                  
MOTORCYCLE_TYPE,                  
IS_ACTIVE,                  
CREATED_BY,                  
CREATED_DATETIME,                  
MODIFIED_BY,          
LAST_UPDATED_DATETIME,            
            
APP_VEHICLE_CLASS,        
SYMBOL,  
CYCL_REGD_ROAD_USE,
COMPRH_ONLY                  
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
                  
@GRG_ADD1,                  
@GRG_ADD2,                  
@GRG_CITY,                  
@GRG_COUNTRY,                  
@GRG_STATE,                  
@GRG_ZIP,                  
@REGISTERED_STATE,                  
@TERRITORY,                  
@AMOUNT,                  
@VEHICLE_AGE,                  
@VEHICLE_CC,                  
@MOTORCYCLE_TYPE,                  
                  
@IS_ACTIVE,                  
@CREATED_BY,                  
@CREATED_DATETIME,                  
@MODIFIED_BY,                  
@LAST_UPDATED_DATETIME,            
            
@APP_VEHICLE_CLASS, @SYMBOL,  
@CYCL_REGD_ROAD_USE,
@COMPRH_ONLY                  
)                  
                  
      
-- --Copy Policy level vehicles from any other vehicle if it exists--      
exec Proc_COPY_CYCLE_POLICY_LEVEL_COVERAGES  @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID ,@VEHICLE_ID    
-- IF EXISTS             
-- (            
--  SELECT TOP 1 * FROM             
--  APP_VEHICLE_COVERAGES            
--  WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
--   APP_ID = @APP_ID AND            
--   APP_VERSION_ID = @APP_VERSION_ID AND            
--   VEHICLE_ID <> @VEHICLE_ID            
-- )            
-- BEGIN            
--   DECLARE @TEMP_ERROR Int            
--   SET @TEMP_ERROR = 0            
--              
--    SELECT  AVC.* INTO #APP_VEHICLE_COVERAGES             
--   FROM APP_VEHICLE_COVERAGES AVC            
--   INNER JOIN MNT_COVERAGE C ON            
--    AVC.COVERAGE_CODE_ID = C.COV_ID             
--   WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
--    APP_ID = @APP_ID AND            
--    APP_VERSION_ID = @APP_VERSION_ID AND            
--    C.COVERAGE_TYPE = 'PL' AND            
--    VEHICLE_ID =             
--    (            
--     SELECT TOP 1 VEHICLE_ID FROM             
--     APP_VEHICLES            
--     WHERE CUSTOMER_ID = @CUSTOMER_ID AND            
--      APP_ID = @APP_ID AND            
--      APP_VERSION_ID = @APP_VERSION_ID AND            
--      VEHICLE_ID <> @VEHICLE_ID            
--    )             
--               
--     SET @TEMP_ERROR = @@ERROR            
--                                  
--       UPDATE #APP_VEHICLE_COVERAGES             
--      SET VEHICLE_ID = @VEHICLE_ID            
--                             
--         SET @TEMP_ERROR = @@ERROR            
--                   
--               
--        SELECT * FROM #APP_VEHICLE_COVERAGES            
--              
--   print(@@rowcount)            
--                          
--       INSERT INTO APP_VEHICLE_COVERAGES             
--       SELECT * FROM #APP_VEHICLE_COVERAGES                                  
--                   
--        SET @TEMP_ERROR = @@ERROR            
--                                   
--       DROP TABLE #APP_VEHICLE_COVERAGES                                  
--               
--      SET @TEMP_ERROR = @@ERROR            
--              
--   IF (  @TEMP_ERROR  <> 0 )            
--   BEGIN            
--     RAISERROR ('Unable to copy Policy Level Coverages', 16, 1)            
--     RETURN            
--   END            
-- END            
--Call to proc to set the value at gen info table when there are vehicles having amount>30000    
--The following rule is being commented as it will not be used now...the rule will be set at
--the underwriting question page itself
--exec  Proc_MotorGreaterAmountRule @customeR_id,@app_id,@app_version_id    
------------------                                        
                  
END                
                
    
  








GO

