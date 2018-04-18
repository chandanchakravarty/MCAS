IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SAVE_APP_WATERCRAFT_GEN_INFO_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SAVE_APP_WATERCRAFT_GEN_INFO_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                      
Proc Name       : dbo.Proc_SAVE_APP_WATERCRAFT_GEN_INFO_ACORD                      
Created by      : Praveen kasana                       
Date            : 15/feb/2006                      
Purpose			: Inserts/Upadtes record in Watercrat Gen Info table                  
Revison History :                      
Used In  : Wolverine

Date Modified   : 07 Oct 2009
Purpose :	When Boat is attached to HOME .
			Update Logic will call.
			So intilaise the Variable.                      
                  
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------*/          
-- drop Proc dbo.Proc_SAVE_APP_WATERCRAFT_GEN_INFO_ACORD            
CREATE   PROC dbo.Proc_SAVE_APP_WATERCRAFT_GEN_INFO_ACORD                      
(                      
@CUSTOMER_ID     int,                      
@APP_ID     int,                      
@APP_VERSION_ID     smallint,                      
@PHY_MENTL_CHALLENGED     nchar(1),                      
@DRIVER_SUS_REVOKED     nchar(1),                      
@IS_CONVICTED_ACCIDENT     char(1),                      
@ANY_OTH_INSU_COMP     nchar(1),                      
@OTHER_POLICY_NUMBER_LIST     nvarchar(50),                      
@ANY_LOSS_THREE_YEARS     nchar(1),                      
@COVERAGE_DECLINED     nchar(1),                      
@IS_RENTED_OTHERS     nchar(1),                      
@IS_REGISTERED_OTHERS     nchar(1),                      
@IS_ACTIVE     nchar(1),                      
@CREATED_BY     int,                      
@CREATED_DATETIME     datetime,        
@MODIFIED_BY               int,        
@PHY_MENTL_CHALLENGED_DESC varchar(50),                      
@DRIVER_SUS_REVOKED_DESC varchar(50),                      
@IS_CONVICTED_ACCIDENT_DESC varchar(50),                      
@ANY_LOSS_THREE_YEARS_DESC varchar(50),                      
@COVERAGE_DECLINED_DESC varchar(50),                      
@IS_RENTED_OTHERS_DESC varchar(50),                      
@IS_REGISTERED_OTHERS_DESC varchar(50),                      
@MINOR_VIOLATION nchar(1),                      
@DRINK_DRUG_VOILATION nchar(1),                      
@PARTICIPATE_RACE nchar(2),                      
@CARRY_PASSENGER_FOR_CHARGE nchar(2),                      
@PARTICIPATE_RACE_DESC nvarchar(50),                      
@CARRY_PASSENGER_FOR_CHARGE_DESC nvarchar(50),                     
@PRIOR_INSURANCE_CARRIER_DESC nvarchar(50),                    
@IS_PRIOR_INSURANCE_CARRIER nchar(1)  ,              
@IS_BOAT_COOWNED nchar(1),              
@IS_BOAT_COOWNED_DESC    nvarchar(50),        
@BOAT_HOME_DISCOUNT nchar(1),  
--Added on 11 may 2006  
@MULTI_POLICY_DISC_APPLIED nchar(1),  
@MULTI_POLICY_DISC_APPLIED_PP_DESC   nvarchar(50),  
@ANY_BOAT_AMPHIBIOUS nchar(1),  
@ANY_BOAT_AMPHIBIOUS_DESC   nvarchar(50),  
@ANY_BOAT_RESIDENCE  nchar(1),  
@ANY_BOAT_RESIDENCE_DESC   nvarchar(50),  
@IS_BOAT_USED_IN_ANY_WATER NCHAR(1) = null,        
@IS_BOAT_USED_IN_ANY_WATER_DESC VARCHAR(50)=null   
        
          
)                      
AS                      
BEGIN             
        
if NOT EXISTS(        
        
 SELECT * FROM APP_WATERCRAFT_GEN_INFO                  
 WHERE                      
 CUSTOMER_ID   = @CUSTOMER_ID AND                      
 APP_ID = @APP_ID AND                      
 APP_VERSION_ID = @APP_VERSION_ID                      
        
)                   
BEGIN         

       
set @PHY_MENTL_CHALLENGED   =0         
set @DRIVER_SUS_REVOKED     =0        
set @IS_CONVICTED_ACCIDENT   =0           
set @ANY_OTH_INSU_COMP     =0        
set @OTHER_POLICY_NUMBER_LIST =0        
set @ANY_LOSS_THREE_YEARS      =0        
set @COVERAGE_DECLINED      =0        
set @IS_RENTED_OTHERS      =0        
set @IS_REGISTERED_OTHERS     =0 
set @CREATED_BY      =0        
set @PARTICIPATE_RACE =0        
set @CARRY_PASSENGER_FOR_CHARGE  =0        
set @IS_PRIOR_INSURANCE_CARRIER  =0        
set @ANY_BOAT_AMPHIBIOUS =0   
set @ANY_BOAT_RESIDENCE =0  
set @IS_BOAT_USED_IN_ANY_WATER =0  
   
-----------added by Pravesh on 20 march FOR IF MULTI POLICY DISCOUNT THEN PUT POLICY NUMBER IN DESC FIELD
if (LEN(ISNULL(@MULTI_POLICY_DISC_APPLIED_PP_DESC,''))<5 AND ISNULL(@MULTI_POLICY_DISC_APPLIED,'0')='1')
BEGIN
	DECLARE @AGENCY_ID					SMALLINT,
				@LOB_ID					SMALLINT,
				@MULTI_POLICY_NUMBER	varchar(20),
				@APP_POLICY_NUMBER		varchar(20),
				@MULTI_POLICY_COUNT		SMALLINT

	SELECT @AGENCY_ID=APP_AGENCY_ID,@LOB_ID=APP_LOB, @APP_POLICY_NUMBER=APP_NUMBER FROM APP_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID 

	create table ##TMP_MULTIPOLICY
		(APP_POL_NUMBER varchar(20))
	INSERT INTO ##TMP_MULTIPOLICY 
		EXECUTE Proc_GetEligiblePolicies   @CUSTOMER_ID ,  @AGENCY_ID , @LOB_ID , @APP_POLICY_NUMBER 
	 SELECT @MULTI_POLICY_COUNT=COUNT(*)  FROM ##TMP_MULTIPOLICY   
	 SELECT TOP 1 @MULTI_POLICY_NUMBER=APP_POL_NUMBER FROM ##TMP_MULTIPOLICY   
	DROP TABLE ##TMP_MULTIPOLICY  
	 IF (ISNULL(@MULTI_POLICY_NUMBER,'')!='N.A.' AND ISNULL(@MULTI_POLICY_NUMBER,'')!='' AND @MULTI_POLICY_COUNT>0)  
	 BEGIN  
		set @MULTI_POLICY_DISC_APPLIED_PP_DESC=@MULTI_POLICY_NUMBER  
	 END  
END
--------END HERE
        
--==================                 
INSERT INTO APP_WATERCRAFT_GEN_INFO                      
(                      
CUSTOMER_ID,                      
APP_ID,                      
APP_VERSION_ID,                      
--HAS_CURR_ADD_THREE_YEARS,                      
PHY_MENTL_CHALLENGED,                      
DRIVER_SUS_REVOKED,                      
IS_CONVICTED_ACCIDENT,           
ANY_OTH_INSU_COMP,              
OTHER_POLICY_NUMBER_LIST,                      
ANY_LOSS_THREE_YEARS,                      
COVERAGE_DECLINED,                      
IS_RENTED_OTHERS,                      
IS_REGISTERED_OTHERS,                   
IS_ACTIVE,                      
CREATED_BY,                      
CREATED_DATETIME,        
MODIFIED_BY,        
--HAS_CURR_ADD_THREE_YEARS_DESC ,                      
PHY_MENTL_CHALLENGED_DESC,                      
DRIVER_SUS_REVOKED_DESC,                      
IS_CONVICTED_ACCIDENT_DESC,                      
                      
ANY_LOSS_THREE_YEARS_DESC,                      
COVERAGE_DECLINED_DESC,                      
--DEGREE_CONVICTION_DESC,                      
                      
IS_RENTED_OTHERS_DESC,                      
IS_REGISTERED_OTHERS_DESC,                      
                   
DRINK_DRUG_VOILATION,           
MINOR_VIOLATION,                    
PARTICIPATE_RACE,                      
CARRY_PASSENGER_FOR_CHARGE,                      
PARTICIPATE_RACE_DESC,        
CARRY_PASSENGER_FOR_CHARGE_DESC,                    
IS_PRIOR_INSURANCE_CARRIER,        
PRIOR_INSURANCE_CARRIER_DESC  ,              
IS_BOAT_COOWNED,        
IS_BOAT_COOWNED_DESC  ,        
BOAT_HOME_DISCOUNT ,  
--Added on 11 May 2006  
MULTI_POLICY_DISC_APPLIED,  
MULTI_POLICY_DISC_APPLIED_PP_DESC,  
ANY_BOAT_AMPHIBIOUS,  
ANY_BOAT_AMPHIBIOUS_DESC,  
ANY_BOAT_RESIDENCE,  
ANY_BOAT_RESIDENCE_DESC,  
IS_BOAT_USED_IN_ANY_WATER,  
IS_BOAT_USED_IN_ANY_WATER_DESC  
  
       
)                      
VALUES                      
(                      
@CUSTOMER_ID,                      
@APP_ID,                      
@APP_VERSION_ID,                      
--@HAS_CURR_ADD_THREE_YEARS,                      
@PHY_MENTL_CHALLENGED,                      
@DRIVER_SUS_REVOKED,                      
@IS_CONVICTED_ACCIDENT,                      
@ANY_OTH_INSU_COMP,                      
@OTHER_POLICY_NUMBER_LIST,                      
@ANY_LOSS_THREE_YEARS,                      
@COVERAGE_DECLINED,                      
--@DEGREE_CONVICTION,                      
--Is_CREDIT,                      
--@CREDIT_DETAILS,                      
@IS_RENTED_OTHERS,                      
@IS_REGISTERED_OTHERS,                      
'Y',                      
@CREATED_BY,                      
@CREATED_DATETIME,        
@MODIFIED_BY,                        
                    
--@HAS_CURR_ADD_THREE_YEARS_DESC ,                      
@PHY_MENTL_CHALLENGED_DESC,                      
@DRIVER_SUS_REVOKED_DESC,                      
@IS_CONVICTED_ACCIDENT_DESC,                      
                      
@ANY_LOSS_THREE_YEARS_DESC,                      
@COVERAGE_DECLINED_DESC,                      
--@DEGREE_CONVICTION_DESC,                      
                      
@IS_RENTED_OTHERS_DESC,                      
@IS_REGISTERED_OTHERS_DESC,        
@DRINK_DRUG_VOILATION,                      
@MINOR_VIOLATION,             
@PARTICIPATE_RACE,                      
@CARRY_PASSENGER_FOR_CHARGE,                      
--@TOT_YRS_OPERATORS_EXP,                      
@PARTICIPATE_RACE_DESC,        
@CARRY_PASSENGER_FOR_CHARGE_DESC,                     
@IS_PRIOR_INSURANCE_CARRIER,        
@PRIOR_INSURANCE_CARRIER_DESC ,                  
@IS_BOAT_COOWNED,@IS_BOAT_COOWNED_DESC  ,        
@BOAT_HOME_DISCOUNT,  
--Added on may 11  
@MULTI_POLICY_DISC_APPLIED,  
@MULTI_POLICY_DISC_APPLIED_PP_DESC,  
@ANY_BOAT_AMPHIBIOUS,  
@ANY_BOAT_AMPHIBIOUS_DESC,  
@ANY_BOAT_RESIDENCE,  
@ANY_BOAT_RESIDENCE_DESC,  
@IS_BOAT_USED_IN_ANY_WATER,  
@IS_BOAT_USED_IN_ANY_WATER_DESC  
        
)                      
END         
else        
BEGIN   

--Added on Itrack 6515
set @PHY_MENTL_CHALLENGED   =0         
set @DRIVER_SUS_REVOKED     =0        
set @IS_CONVICTED_ACCIDENT   =0           
set @ANY_OTH_INSU_COMP     =0        
set @OTHER_POLICY_NUMBER_LIST =0        
set @ANY_LOSS_THREE_YEARS      =0        
set @COVERAGE_DECLINED      =0        
set @IS_RENTED_OTHERS      =0        
set @IS_REGISTERED_OTHERS     =0 
set @CREATED_BY      =0        
set @PARTICIPATE_RACE =0        
set @CARRY_PASSENGER_FOR_CHARGE  =0        
set @IS_PRIOR_INSURANCE_CARRIER  =0        
set @ANY_BOAT_AMPHIBIOUS =0   
set @ANY_BOAT_RESIDENCE =0  
set @IS_BOAT_USED_IN_ANY_WATER =0  
                       
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
IS_RENTED_OTHERS=@IS_RENTED_OTHERS,                          
IS_REGISTERED_OTHERS=@IS_REGISTERED_OTHERS,                          
--MODIFIED_BY=@MODIFIED_BY,                          
--LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME   ,                        
PHY_MENTL_CHALLENGED_DESC=@PHY_MENTL_CHALLENGED_DESC,                        
DRIVER_SUS_REVOKED_DESC=@DRIVER_SUS_REVOKED_DESC,                        
IS_CONVICTED_ACCIDENT_DESC=@IS_CONVICTED_ACCIDENT_DESC,                        
ANY_LOSS_THREE_YEARS_DESC=@ANY_LOSS_THREE_YEARS_DESC,                        
COVERAGE_DECLINED_DESC=@COVERAGE_DECLINED_DESC,                        
IS_RENTED_OTHERS_DESC=@IS_RENTED_OTHERS_DESC,                        
MODIFIED_BY=@MODIFIED_BY,           
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
IS_BOAT_COOWNED_DESC=@IS_BOAT_COOWNED_DESC  ,        
BOAT_HOME_DISCOUNT = @BOAT_HOME_DISCOUNT,  
--Added on 11 may  
MULTI_POLICY_DISC_APPLIED = @MULTI_POLICY_DISC_APPLIED,  
MULTI_POLICY_DISC_APPLIED_PP_DESC = @MULTI_POLICY_DISC_APPLIED_PP_DESC,  
ANY_BOAT_AMPHIBIOUS = @ANY_BOAT_AMPHIBIOUS,  
ANY_BOAT_AMPHIBIOUS_DESC = @ANY_BOAT_AMPHIBIOUS_DESC,  
ANY_BOAT_RESIDENCE = @ANY_BOAT_RESIDENCE,  
ANY_BOAT_RESIDENCE_DESC = @ANY_BOAT_RESIDENCE_DESC,  
  
IS_BOAT_USED_IN_ANY_WATER = @IS_BOAT_USED_IN_ANY_WATER,  
IS_BOAT_USED_IN_ANY_WATER_DESC = @IS_BOAT_USED_IN_ANY_WATER_DESC  
  
            
WHERE                          
CUSTOMER_ID=@CUSTOMER_ID AND                          
APP_ID=@APP_ID AND                          
APP_VERSION_ID=@APP_VERSION_ID                          
END        
  
---MAjor VIOLATION CHECK//  
/*<VIOLATIONID>1830</VIOLATIONID>  
<VIOLATIONID>2288</VIOLATIONID>  
<VIOLATIONID>2745</VIOLATIONID>*/  
  
DECLARE @SERIOUS_VIOLATION_IN VARCHAR(20)  
DECLARE @SERIOUS_VIOLATION_MI VARCHAR(20)  
DECLARE @SERIOUS_VIOLATION_WI VARCHAR(20)  
SET @SERIOUS_VIOLATION_IN = 1830  
SET @SERIOUS_VIOLATION_MI = 2288  
SET @SERIOUS_VIOLATION_WI = 2745  
DECLARE @APP_EFFECTIVE_DATE DATETIME  
DECLARE @MVR_DATE DATETIME  
  
  
SELECT @APP_EFFECTIVE_DATE = APP_EFFECTIVE_DATE  
FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND                          
 APP_ID=@APP_ID AND                          
 APP_VERSION_ID=@APP_VERSION_ID    
  
SELECT @MVR_DATE = MVR_DATE FROM APP_WATER_MVR_INFORMATION WITH (NOLOCK)  
WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND   
APP_VERSION_ID = @APP_VERSION_ID AND VIOLATION_TYPE IN (@SERIOUS_VIOLATION_IN,@SERIOUS_VIOLATION_MI,@SERIOUS_VIOLATION_WI)  
  
IF(DATEDIFF(YEAR,@MVR_DATE,@APP_EFFECTIVE_DATE)<=5)  
BEGIN  
  
  
 IF EXISTS (SELECT APP_WATER_MVR_ID FROM APP_WATER_MVR_INFORMATION WITH (NOLOCK)  
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND   
  APP_VERSION_ID = @APP_VERSION_ID AND VIOLATION_TYPE IN (@SERIOUS_VIOLATION_IN,@SERIOUS_VIOLATION_MI,@SERIOUS_VIOLATION_WI))  
 BEGIN  
  UPDATE APP_WATERCRAFT_GEN_INFO SET IS_CONVICTED_ACCIDENT = 1   
  WHERE                          
  CUSTOMER_ID=@CUSTOMER_ID AND                          
  APP_ID=@APP_ID AND                          
  APP_VERSION_ID=@APP_VERSION_ID    
 END  
END  
--         
end          
  
  
  
  
  
  
  
  
  
  
  


GO

