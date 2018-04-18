IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyMotorcycleInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyMotorcycleInfo]
GO                  
CREATE PROC dbo.Proc_InsertPolicyMotorcycleInfo                      
(                      
                       
@CUSTOMER_ID     int,                      
@POLICY_ID     int,                      
@POLICY_VERSION_ID     smallint,                      
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
@IS_ACTIVE     nchar(2),                      
@CREATED_BY     int,                      
@CREATED_DATETIME     datetime,                      
@MODIFIED_BY     int,                      
@LAST_UPDATED_DATETIME     datetime,                
@APP_VEHICLE_CLASS  INT,              
@SYMBOL int,        
@CYCL_REGD_ROAD_USE int = null,      
@COMPRH_ONLY int=null ,  
  
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
DECLARE @TEMP_INSURED_VEH_NUMBER INT                  
  DECLARE @PROCESS_ID INT    
 SELECT   @PROCESS_ID=PROCESS_ID FROM POL_POLICY_PROCESS   WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND NEW_POLICY_VERSION_ID=@POLICY_VERSION_ID                         
                                  
                 
select @VEHICLE_ID=isnull(max(VEHICLE_ID),0)+1 from POL_VEHICLES where CUSTOMER_ID=@CUSTOMER_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and POLICY_ID=@POLICY_ID;                      
                  
                  
SELECT   @TEMP_INSURED_VEH_NUMBER =  (isnull(MAX(INSURED_VEH_NUMBER),0)) +1                   
  FROM         POL_VEHICLES                     
  WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and POLICY_ID=@POLICY_ID                   
                  
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
COMPRH_ONLY,        
IS_UPDATED ,  
  
CHASIS_NUMBER,  
TRANSMISSION_TYPE,  
FUEL_TYPE,  
TOTAL_DRIVERS,  
BODY_TYPE,  
REGN_PLATE_NUMBER,             
RISK_CURRENCY,  
VEHICLE_COVERAGE  
              
)                      
VALUES                      
(                      
@CUSTOMER_ID,                      
@POLICY_ID,                      
@POLICY_VERSION_ID,                      
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
                
@APP_VEHICLE_CLASS,              
@SYMBOL,        
@CYCL_REGD_ROAD_USE,      
@COMPRH_ONLY,    
CASE WHEN @PROCESS_ID=@EBDORSEMENTPOROCESS_ID    
 THEN 10963    
 ELSE    
   10964    
 END  ,  
   
@CHASIS_NUMBER,  
@TRANSMISSION_TYPE,  
@FUEL_TYPE,  
@TOTAL_DRIVERS,  
@BODY_TYPE,  
@REGN_PLATE_NUMBER,  
@RISK_CURRENCY   ,  
@VEHICLE_COVERAGE                
)         

IF NOT EXISTS(SELECT * FROM POL_PRODUCT_COVERAGES WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID)
BEGIN
	INSERT INTO POL_PRODUCT_COVERAGES (CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,RISK_ID,COVERAGE_ID,COVERAGE_CODE_ID,LIMIT_1)
	SELECT @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@VEHICLE_ID,1,M.COV_ID,AMOUNT FROM POL_VEHICLES P WITH(NOLOCK) INNER JOIN            
				MNT_COVERAGE M WITH(NOLOCK) ON P.VEHICLE_COVERAGE =M.COV_ID            
			   WHERE   CUSTOMER_ID=@CUSTOMER_ID         
		AND POLICY_ID=@POLICY_ID         
		AND POLICY_VERSION_ID=@POLICY_VERSION_ID        
		AND VEHICLE_ID=@VEHICLE_ID
END             
                      
--Copy Policy level vehicles from any other vehicle if it exists--            
EXEC Proc_COPY_CYCLE_POLICY_LEVEL_COVERAGES_POL                 
 @CUSTOMER_ID,--@CUSTOMER_ID     int,                      
 @POLICY_ID,--@POLICY_ID     int,                      
 @POLICY_VERSION_ID,--@POLICY_VERSION_ID     smallint,                      
 @VEHICLE_ID--@VEHICLE_ID     smallint                          
            
                
------------------   End of policy level             
          
--Call to proc to set the value at gen info table when there are vehicles having amount>30000          
--The following rule is being commented as it will be implemented at underwriting Q info page itself    
--exec  Proc_MotorGreaterAmountRulePolicy @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID     


                                   
                
END                    
                
    
    
    