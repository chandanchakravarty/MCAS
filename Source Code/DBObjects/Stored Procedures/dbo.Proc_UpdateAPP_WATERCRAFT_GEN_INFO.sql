IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateAPP_WATERCRAFT_GEN_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateAPP_WATERCRAFT_GEN_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_updateAPP_WATERCRAFT_GEN_INFO                  
Created by      : Anurag Verma                  
Date            : 5/16/2005                  
Purpose       :Insert data in APP_WATERCRAFT_GEN_INFO                  
Revison History :                  
Used In        : Wolverine                  
                
Modified By : Anurag verma                
Modified On : 5/10/2005                
Purpose  : added fields in the query and as parameter(PARTICIPATE_RACE,CARRY_PASSENGER_FOR_CHARGE,TOT_YRS_OPERATORS_EXP)                
                
Modified By : Anurag Verma                
Modified On : 06/10/2005                
Purpose  : PARTICIPATE_RACE_DESC,CARRY_PASSENGER_FOR_CHARGE_DESC                
              
Modified By : Vijay Arora              
Modified On : 14/10/2005                
Purpose  : Correct the total years experience parameter.              
            
Modified By : Sumit Chhabra            
Modified On : 14/11/2005                
Purpose  : Commented the fields DEGREE_CONVICTION and             

Modified By :Shafi         
Modified On : 20/03/2006           
Purpose     : Add New Field MULTI_POLICY_DISC_APPLIED_PP_DESC,MULTI_POLICY_DISC_APPLIED    

-----------------------------------------------------------

-                  
Date     Review By          Comments                  
------   ------------       -------------------------*/         
--       drop proc dbo.Proc_UpdateAPP_WATERCRAFT_GEN_INFO                  
CREATE     PROC dbo.Proc_UpdateAPP_WATERCRAFT_GEN_INFO                  
(                  
@CUSTOMER_ID     int,                  
@APP_ID     int,                  
@APP_VERSION_ID     smallint,                  
--@HAS_CURR_ADD_THREE_YEARS     nchar(1),                  
@PHY_MENTL_CHALLENGED     nchar(1),                  
@DRIVER_SUS_REVOKED     nchar(1),                  
@IS_CONVICTED_ACCIDENT     nchar(1),                  
@ANY_OTH_INSU_COMP     nchar(1),                  
@OTHER_POLICY_NUMBER_LIST     nvarchar(50),                  
@ANY_LOSS_THREE_YEARS     nchar(1),                  
@COVERAGE_DECLINED     nchar(1),                  
--@DEGREE_CONVICTION     nchar(1),                  
--@IS_CREDIT     nchar(1),                  
--@CREDIT_DETAILS     nvarchar(50),                  
@IS_RENTED_OTHERS     nchar(1),                  
@IS_REGISTERED_OTHERS     nchar(1),                  
@MODIFIED_BY int,                  
@LAST_UPDATED_DATETIME datetime  ,                
                
--@HAS_CURR_ADD_THREE_YEARS_DESC varchar(50),                
@PHY_MENTL_CHALLENGED_DESC varchar(50),                
@DRIVER_SUS_REVOKED_DESC varchar(50),                
@IS_CONVICTED_ACCIDENT_DESC varchar(50),                
                
@ANY_LOSS_THREE_YEARS_DESC varchar(50),                
@COVERAGE_DECLINED_DESC varchar(50),                
--@DEGREE_CONVICTION_DESC varchar(50),                
            
@IS_RENTED_OTHERS_DESC varchar(50),                
@IS_REGISTERED_OTHERS_DESC varchar(50),                
@MINOR_VIOLATION nchar(1),                
@DRINK_DRUG_VOILATION nchar(1),                
@PARTICIPATE_RACE nchar(2),                
@CARRY_PASSENGER_FOR_CHARGE nchar(2),                
--@TOT_YRS_OPERATORS_EXP int,                
@PARTICIPATE_RACE_DESC nvarchar(50),                
@CARRY_PASSENGER_FOR_CHARGE_DESC nvarchar(50),                
@IS_PRIOR_INSURANCE_CARRIER nchar(1),            
@PRIOR_INSURANCE_CARRIER_DESC nvarchar(50),            
        
@IS_BOAT_COOWNED nchar(1),            
@IS_BOAT_COOWNED_DESC nvarchar(50) ,     
--Added By Shafi 20-03-2006  
 @MULTI_POLICY_DISC_APPLIED  NCHAR(2),      
 @MULTI_POLICY_DISC_APPLIED_PP_DESC     varchar(50),
 @ANY_BOAT_AMPHIBIOUS  NCHAR(1),      
 @ANY_BOAT_AMPHIBIOUS_DESC varchar(50),
 @ANY_BOAT_RESIDENCE  NCHAR(1),     
 @ANY_BOAT_RESIDENCE_DESC varchar(50),
@IS_BOAT_USED_IN_ANY_WATER  NCHAR(1) =null,
@IS_BOAT_USED_IN_ANY_WATER_DESC  varchar(50)=null
       
)                  
        
AS                  
BEGIN                  
UPDATE  APP_WATERCRAFT_GEN_INFO              
SET                  
--HAS_CURR_ADD_THREE_YEARS=@HAS_CURR_ADD_THREE_YEARS,                  
PHY_MENTL_CHALLENGED=@PHY_MENTL_CHALLENGED,                  
DRIVER_SUS_REVOKED=@DRIVER_SUS_REVOKED,                  
IS_CONVICTED_ACCIDENT=@IS_CONVICTED_ACCIDENT,                  
ANY_OTH_INSU_COMP=@ANY_OTH_INSU_COMP,                  
OTHER_POLICY_NUMBER_LIST=@OTHER_POLICY_NUMBER_LIST,                  
ANY_LOSS_THREE_YEARS=@ANY_LOSS_THREE_YEARS,                  
COVERAGE_DECLINED=@COVERAGE_DECLINED,                  
--DEGREE_CONVICTION=@DEGREE_CONVICTION,                  
--IS_CREDIT=@IS_CREDIT,                  
--CREDIT_DETAILS=@CREDIT_DETAILS,                  
IS_RENTED_OTHERS=@IS_RENTED_OTHERS,                  
IS_REGISTERED_OTHERS=@IS_REGISTERED_OTHERS,                  
MODIFIED_BY=@MODIFIED_BY,                  
LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME   ,                
                
--HAS_CURR_ADD_THREE_YEARS_DESC=@HAS_CURR_ADD_THREE_YEARS_DESC ,                
PHY_MENTL_CHALLENGED_DESC=@PHY_MENTL_CHALLENGED_DESC,                
DRIVER_SUS_REVOKED_DESC=@DRIVER_SUS_REVOKED_DESC,                
IS_CONVICTED_ACCIDENT_DESC=@IS_CONVICTED_ACCIDENT_DESC,                
                
ANY_LOSS_THREE_YEARS_DESC=@ANY_LOSS_THREE_YEARS_DESC,                
COVERAGE_DECLINED_DESC=@COVERAGE_DECLINED_DESC,                
--DEGREE_CONVICTION_DESC= @DEGREE_CONVICTION_DESC,                
                
IS_RENTED_OTHERS_DESC=@IS_RENTED_OTHERS_DESC,                
IS_REGISTERED_OTHERS_DESC=@IS_REGISTERED_OTHERS_DESC ,                
MINOR_VIOLATION=@MINOR_VIOLATION,                
DRINK_DRUG_VOILATION=@DRINK_DRUG_VOILATION,                
PARTICIPATE_RACE=@PARTICIPATE_RACE,                
CARRY_PASSENGER_FOR_CHARGE=@CARRY_PASSENGER_FOR_CHARGE,                
--TOT_YRS_OPERATORS_EXP=@TOT_YRS_OPERATORS_EXP,                
PARTICIPATE_RACE_DESC=@PARTICIPATE_RACE_DESC,                
CARRY_PASSENGER_FOR_CHARGE_DESC=@CARRY_PASSENGER_FOR_CHARGE_DESC,            
IS_PRIOR_INSURANCE_CARRIER=@IS_PRIOR_INSURANCE_CARRIER,            
PRIOR_INSURANCE_CARRIER_DESC=@PRIOR_INSURANCE_CARRIER_DESC,           
IS_BOAT_COOWNED=@IS_BOAT_COOWNED,  
IS_BOAT_COOWNED_DESC=@IS_BOAT_COOWNED_DESC , 
MULTI_POLICY_DISC_APPLIED= @MULTI_POLICY_DISC_APPLIED,      
MULTI_POLICY_DISC_APPLIED_PP_DESC=  @MULTI_POLICY_DISC_APPLIED_PP_DESC,
ANY_BOAT_AMPHIBIOUS= @ANY_BOAT_AMPHIBIOUS,      
ANY_BOAT_AMPHIBIOUS_DESC=  @ANY_BOAT_AMPHIBIOUS_DESC,
ANY_BOAT_RESIDENCE= @ANY_BOAT_RESIDENCE,      
ANY_BOAT_RESIDENCE_DESC =  @ANY_BOAT_RESIDENCE_DESC,
IS_BOAT_USED_IN_ANY_WATER = @IS_BOAT_USED_IN_ANY_WATER,
IS_BOAT_USED_IN_ANY_WATER_DESC = @IS_BOAT_USED_IN_ANY_WATER_DESC
WHERE                  
CUSTOMER_ID=@CUSTOMER_ID AND                  
APP_ID=@APP_ID AND                  
APP_VERSION_ID=@APP_VERSION_ID                  
END  





GO

