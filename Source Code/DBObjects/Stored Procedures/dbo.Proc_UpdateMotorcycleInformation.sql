IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateMotorcycleInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateMotorcycleInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
--  drop proc Proc_UpdateMotorcycleInformation  
CREATE PROC dbo.Proc_UpdateMotorcycleInformation              
(              
@CUSTOMER_ID    int,              
@APP_ID          int,              
@APP_VERSION_ID  int,              
@VEHICLE_ID     smallint,              
@INSURED_VEH_NUMBER  int,              
@VEHICLE_YEAR int,              
@MAKE nvarchar(150),              
@MODEL  nvarchar(150),              
@VIN  nvarchar(150),              
@GRG_ADD1  nvarchar(150),              
@GRG_ADD2  nvarchar(150),              
@GRG_CITY  nvarchar(80),              
@GRG_COUNTRY      nvarchar(10),              
@GRG_STATE  nvarchar(10),              
@GRG_ZIP  nvarchar(20),              
@REGISTERED_STATE  nvarchar(20),              
@TERRITORY  nvarchar(20),              
@AMOUNT decimal,              
@VEHICLE_AGE decimal,              
@VEHICLE_CC int =0,              
@MOTORCYCLE_TYPE int =0,              
--@IS_ACTIVE  nchar(1),              
@MODIFIED_BY     int,              
@LAST_UPDATED_DATETIME  datetime,          
@APP_VEHICLE_CLASS   INT,      
@SYMBOL INT,  
@CYCL_REGD_ROAD_USE int = null,
@COMPRH_ONLY int =null         
)              
AS              
BEGIN              
 UPDATE APP_VEHICLES              
 SET                 
  --INSURED_VEH_NUMBER=@INSURED_VEH_NUMBER,              
  VEHICLE_YEAR=@VEHICLE_YEAR,              
  MAKE=@MAKE,              
  MODEL=@MODEL,              
  VIN=@VIN,                
  GRG_ADD1=@GRG_ADD1,              
  GRG_ADD2=@GRG_ADD2,              
  GRG_CITY=@GRG_CITY,              
  GRG_COUNTRY=@GRG_COUNTRY,              
  GRG_STATE=@GRG_STATE,              
  GRG_ZIP=@GRG_ZIP,              
  REGISTERED_STATE=@REGISTERED_STATE,              
  TERRITORY=@TERRITORY,                
  AMOUNT=@AMOUNT,                
  VEHICLE_AGE=@VEHICLE_AGE,              
  VEHICLE_CC=@VEHICLE_CC,              
  MOTORCYCLE_TYPE=@MOTORCYCLE_TYPE,              
--  IS_ACTIVE  =@IS_ACTIVE,              
  MODIFIED_BY  =@MODIFIED_BY,              
  LAST_UPDATED_DATETIME =@LAST_UPDATED_DATETIME,          
  APP_VEHICLE_CLASS = @APP_VEHICLE_CLASS,      
  SYMBOL = @SYMBOL,  
  CYCL_REGD_ROAD_USE = @CYCL_REGD_ROAD_USE,
  COMPRH_ONLY=@COMPRH_ONLY              
 WHERE              
  CUSTOMER_ID  =@CUSTOMER_ID AND              
  APP_ID   =@APP_ID AND              
  APP_VERSION_ID =@APP_VERSION_ID AND              
  VEHICLE_ID  =@VEHICLE_ID      
    
--Call to proc to set the value at gen info table when there are vehicles having amount>30000    
--The following rule has been commented as the rule will be implemented at Underwritting question page itself
--exec  Proc_MotorGreaterAmountRule @customeR_id,@app_id,@app_version_id            
END              
            
          
        
      
    
  





GO

