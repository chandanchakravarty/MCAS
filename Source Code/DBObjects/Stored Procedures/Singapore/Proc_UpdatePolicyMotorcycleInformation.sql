IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyMotorcycleInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyMotorcycleInformation]
GO
CREATE PROC dbo.Proc_UpdatePolicyMotorcycleInformation                  
(                  
@CUSTOMER_ID    int,                  
@POLICY_ID     int,                    
@POLICY_VERSION_ID     smallint,                    
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
@IS_ACTIVE  nchar(1),                  
@MODIFIED_BY     int,                  
@LAST_UPDATED_DATETIME  datetime,              
@APP_VEHICLE_CLASS   INT ,            
@SYMBOL int,        
@CYCL_REGD_ROAD_USE int = null,      
@COMPRH_ONLY int=null  ,  
-- Added by Agniswar for Singapore Implementation  
@CHASIS_NUMBER nvarchar(150)= null,  
@TRANSMISSION_TYPE nvarchar(150)= null,  
@FUEL_TYPE nvarchar(150)= null,  
@TOTAL_DRIVERS int= null,  
@BODY_TYPE nvarchar(150)= null,  
@REGN_PLATE_NUMBER   nvarchar(100)= null,  
@RISK_CURRENCY int= null,    
@VEHICLE_COVERAGE nvarchar(50)= null         
)                  
AS                  
BEGIN      
    
DECLARE @EBDORSEMENTPOROCESS_ID INT       
SET @EBDORSEMENTPOROCESS_ID=3                            
 DECLARE @VEHTYPE_ID Int        
 DECLARE @PROCESS_ID INT    
 SELECT   @PROCESS_ID=PROCESS_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND NEW_POLICY_VERSION_ID=@POLICY_VERSION_ID                         
                
 UPDATE POL_VEHICLES                  
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
  IS_ACTIVE  =@IS_ACTIVE,                  
  MODIFIED_BY  =@MODIFIED_BY,                  
  LAST_UPDATED_DATETIME =@LAST_UPDATED_DATETIME,              
  APP_VEHICLE_CLASS = @APP_VEHICLE_CLASS ,            
  SYMBOL =     @SYMBOL,        
  CYCL_REGD_ROAD_USE = @CYCL_REGD_ROAD_USE,      
  COMPRH_ONLY=@COMPRH_ONLY   ,  
    
  CHASIS_NUMBER = @CHASIS_NUMBER,  
  TRANSMISSION_TYPE = @TRANSMISSION_TYPE,  
  FUEL_TYPE = @FUEL_TYPE,  
  TOTAL_DRIVERS = @TOTAL_DRIVERS,  
  BODY_TYPE = @BODY_TYPE,  
  REGN_PLATE_NUMBER = @REGN_PLATE_NUMBER,             
  RISK_CURRENCY = @RISK_CURRENCY,  
  VEHICLE_COVERAGE = @VEHICLE_COVERAGE  
           
 WHERE                  
  CUSTOMER_ID  =@CUSTOMER_ID AND                  
  POLICY_ID =@POLICY_ID  AND                       
  POLICY_VERSION_ID     =@POLICY_VERSION_ID AND              
  VEHICLE_ID  =@VEHICLE_ID     
                 
 IF(@PROCESS_ID=@EBDORSEMENTPOROCESS_ID)    
 BEGIN    
  UPDATE POL_VEHICLES    
   SET IS_UPDATED=10963    
  WHERE CUSTOMER_ID  =@CUSTOMER_ID AND                                
    POLICY_ID   =@POLICY_ID AND                                
    POLICY_VERSION_ID =@POLICY_VERSION_ID AND                     
    VEHICLE_ID  =@VEHICLE_ID       
 END   
 
 UPDATE POL_PRODUCT_COVERAGES SET 
 COVERAGE_CODE_ID = M.COV_ID,
 LIMIT_1 = POL_VEHICLES.AMOUNT
 FROM POL_PRODUCT_COVERAGES,POL_VEHICLES WITH(NOLOCK) INNER JOIN            
				MNT_COVERAGE M WITH(NOLOCK) ON POL_VEHICLES.VEHICLE_COVERAGE =M.COV_ID            
			   WHERE   POL_VEHICLES.CUSTOMER_ID=@CUSTOMER_ID         
		AND POL_VEHICLES.POLICY_ID=@POLICY_ID         
		AND POL_VEHICLES.POLICY_VERSION_ID=@POLICY_VERSION_ID        
		AND POL_VEHICLES.VEHICLE_ID=@VEHICLE_ID      
--Call to proc to set the value at gen info table when there are vehicles having amount>30000          
--The following rule is being commented as it will be implemented at underwriting Q page itself    
--exec  Proc_MotorGreaterAmountRulePolicy @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID          
          
END                  
                
              
              
            
          
        
      
    
    
    