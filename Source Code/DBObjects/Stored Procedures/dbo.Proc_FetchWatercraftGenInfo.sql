IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchWatercraftGenInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchWatercraftGenInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_FetchWatercraftGenInfo         
Modified By : Shafi            
Modified On : 16/01/2006         
Purpose  : Add Feild for Inusre   
  
Modified By :Shafi           
Modified On : 20/03/2006             
Purpose     : Add New Field MULTI_POLICY_DISC_APPLIED_PP_DESC,MULTI_POLICY_DISC_APPLIED             
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/           
--drop proc dbo.Proc_FetchWatercraftGenInfo       
CREATE    PROC dbo.Proc_FetchWatercraftGenInfo           
@CUSTOMER_ID INT,                
@APP_ID INT,                
@APP_VERSION_ID INT                
AS                
                
BEGIN                
SELECT                 
--HAS_CURR_ADD_THREE_YEARS,                
PHY_MENTL_CHALLENGED,                
DRIVER_SUS_REVOKED,                
IS_CONVICTED_ACCIDENT,                
ANY_OTH_INSU_COMP,                
OTHER_POLICY_NUMBER_LIST,                
ANY_LOSS_THREE_YEARS,                
COVERAGE_DECLINED,                
--DEGREE_CONVICTION,                
--IS_CREDIT,                
--CREDIT_DETAILS,                
IS_RENTED_OTHERS,                
IS_REGISTERED_OTHERS ,              
              
              
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
PARTICIPATE_RACE_DESC,CARRY_PASSENGER_FOR_CHARGE_DESC,            
IS_PRIOR_INSURANCE_CARRIER,            
PRIOR_INSURANCE_CARRIER_DESC ,        
IS_BOAT_COOWNED,        
IS_BOAT_COOWNED_DESC,  
LTRIM(RTRIM(ISNULL(MULTI_POLICY_DISC_APPLIED,''))) AS MULTI_POLICY_DISC_APPLIED,
MULTI_POLICY_DISC_APPLIED_PP_DESC,  
ANY_BOAT_AMPHIBIOUS,  
ANY_BOAT_AMPHIBIOUS_DESC,  
ANY_BOAT_RESIDENCE,  
ANY_BOAT_RESIDENCE_DESC,
IS_BOAT_USED_IN_ANY_WATER,
IS_BOAT_USED_IN_ANY_WATER_DESC,
     
AL.APP_NUMBER as APP_NUMBER                
FROM APP_WATERCRAFT_GEN_INFO AWGI with (nolock)
LEFT OUTER JOIN APP_LIST AL  with (nolock)               
on
AL.APP_ID = AWGI.APP_ID AND
AL.APP_VERSION_ID = AWGI.APP_VERSION_ID AND
AL.CUSTOMER_ID  = AWGI.CUSTOMER_ID
WHERE AWGI.APP_ID=@APP_ID AND                 
AWGI.APP_VERSION_ID=@APP_VERSION_ID                
AND AWGI.CUSTOMER_ID=@CUSTOMER_ID  

               
END      
  














GO

