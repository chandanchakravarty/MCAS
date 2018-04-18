IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPOL_WATERCRAFT_GEN_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPOL_WATERCRAFT_GEN_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_InsertPOL_WATERCRAFT_GEN_INFO                  
Created by      : Vijay Arora          
Date            : 01-12-2005          
Purpose        : Insert the General Informatino in table named  POL_WATERCRAFT_GEN_INFO                  
Revison History :                  
Used In         : Wolverine              
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
--drop proc dbo.Proc_InsertPOL_WATERCRAFT_GEN_INFO    
CREATE  PROC dbo.Proc_InsertPOL_WATERCRAFT_GEN_INFO              
(              
@CUSTOMER_ID     int,              
@POLICY_ID     int,              
@POLICY_VERSION_ID     smallint,              
--@HAS_CURR_ADD_THREE_YEARS     nchar(1),              
@PHY_MENTL_CHALLENGED     nchar(1),              
@DRIVER_SUS_REVOKED     nchar(1),              
@IS_CONVICTED_ACCIDENT     char(1),              
@ANY_OTH_INSU_COMP     nchar(1),              
@OTHER_POLICY_NUMBER_LIST     nvarchar(50),              
@ANY_LOSS_THREE_YEARS     nchar(1),              
@COVERAGE_DECLINED     nchar(1),              
--@IS_CREDIT     nchar(1),              
--@CREDIT_DETAILS     nvarchar(50),              
@IS_RENTED_OTHERS     nchar(1),              
@IS_REGISTERED_OTHERS     nchar(1),              
@IS_ACTIVE     nchar(1),              
@CREATED_BY     int,              
@CREATED_DATETIME     datetime,              
--@HAS_CURR_ADD_THREE_YEARS_DESC varchar(50),              
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
@IS_PRIOR_INSURANCE_CARRIER nchar(1) ,      
@IS_BOAT_COOWNED     nchar(1),      
@IS_BOAT_COOWNED_DESC nvarchar(50),
@MULTI_POLICY_DISC_APPLIED  NCHAR(2),      
@MULTI_POLICY_DISC_APPLIED_PP_DESC     varchar(50),
@ANY_BOAT_AMPHIBIOUS NCHAR(1),      
@ANY_BOAT_AMPHIBIOUS_DESC VARCHAR(50),
@ANY_BOAT_RESIDENCE NCHAR(1),      
@ANY_BOAT_RESIDENCE_DESC VARCHAR(50) ,
@IS_BOAT_USED_IN_ANY_WATER NCHAR(1) = null,      
@IS_BOAT_USED_IN_ANY_WATER_DESC VARCHAR(50)=null 
  
)              
AS              
BEGIN              
INSERT INTO POL_WATERCRAFT_GEN_INFO              
(              
CUSTOMER_ID,              
POLICY_ID,              
POLICY_VERSION_ID,              
--HAS_CURR_ADD_THREE_YEARS,              
PHY_MENTL_CHALLENGED,              
DRIVER_SUS_REVOKED,              
IS_CONVICTED_ACCIDENT,              
ANY_OTH_INSU_COMP,              
OTHER_POLICY_NUMBER_LIST,              
ANY_LOSS_THREE_YEARS,              
COVERAGE_DECLINED,              
--IS_CREDIT,              
--CREDIT_DETAILS,              
IS_RENTED_OTHERS,              
IS_REGISTERED_OTHERS,              
IS_ACTIVE,              
CREATED_BY,              
CREATED_DATETIME,              
--HAS_CURR_ADD_THREE_YEARS_DESC ,              
PHY_MENTL_CHALLENGED_DESC,              
DRIVER_SUS_REVOKED_DESC,              
IS_CONVICTED_ACCIDENT_DESC,              
ANY_LOSS_THREE_YEARS_DESC,              
COVERAGE_DECLINED_DESC,              
IS_RENTED_OTHERS_DESC,              
IS_REGISTERED_OTHERS_DESC,              
MINOR_VIOLATION,              
DRINK_DRUG_VOILATION,              
PARTICIPATE_RACE,              
CARRY_PASSENGER_FOR_CHARGE,              
PARTICIPATE_RACE_DESC,CARRY_PASSENGER_FOR_CHARGE_DESC,            
IS_PRIOR_INSURANCE_CARRIER,PRIOR_INSURANCE_CARRIER_DESC  ,          
IS_BOAT_COOWNED,IS_BOAT_COOWNED_DESC,
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
@POLICY_ID,              
@POLICY_VERSION_ID,              
--@HAS_CURR_ADD_THREE_YEARS,    
@PHY_MENTL_CHALLENGED,              
@DRIVER_SUS_REVOKED,              
@IS_CONVICTED_ACCIDENT,              
@ANY_OTH_INSU_COMP,              
@OTHER_POLICY_NUMBER_LIST,              
@ANY_LOSS_THREE_YEARS,              
@COVERAGE_DECLINED,              
--@IS_CREDIT,              
--@CREDIT_DETAILS,              
@IS_RENTED_OTHERS,              
@IS_REGISTERED_OTHERS,              
@IS_ACTIVE,              
@CREATED_BY,              
@CREATED_DATETIME,              
--@HAS_CURR_ADD_THREE_YEARS_DESC ,              
@PHY_MENTL_CHALLENGED_DESC,              
@DRIVER_SUS_REVOKED_DESC,              
@IS_CONVICTED_ACCIDENT_DESC,              
@ANY_LOSS_THREE_YEARS_DESC,              
@COVERAGE_DECLINED_DESC,              
@IS_RENTED_OTHERS_DESC,              
@IS_REGISTERED_OTHERS_DESC,              
@MINOR_VIOLATION,              
@DRINK_DRUG_VOILATION,        @PARTICIPATE_RACE,              
@CARRY_PASSENGER_FOR_CHARGE,              
@PARTICIPATE_RACE_DESC,@CARRY_PASSENGER_FOR_CHARGE_DESC,             
@IS_PRIOR_INSURANCE_CARRIER,@PRIOR_INSURANCE_CARRIER_DESC ,      
@IS_BOAT_COOWNED,@IS_BOAT_COOWNED_DESC,
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





GO

