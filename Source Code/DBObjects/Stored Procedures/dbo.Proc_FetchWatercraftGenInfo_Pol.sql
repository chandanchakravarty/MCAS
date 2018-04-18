IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchWatercraftGenInfo_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchWatercraftGenInfo_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/* ----------------------------------------------------------                
Proc Name       : dbo.Proc_updatePOL_WATERCRAFT_GEN_INFO_Pol       
Modified By 	: Ashwani          
Modified On 	: 02 Mar 2006    
Purpose  	: Add Feild for Inure          
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/   
-- drop proc dbo.Proc_FetchWatercraftGenInfo_Pol      
CREATE    PROC dbo.Proc_FetchWatercraftGenInfo_Pol              
@CUSTOMER_ID INT,              
@POLICY_ID INT,              
@POLICY_VERSION_ID INT              
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
IS_BOAT_COOWNED_DESC    
       
              
FROM POL_WATERCRAFT_GEN_INFO               
WHERE POLICY_ID=@POLICY_ID AND               
POLICY_VERSION_ID=@POLICY_VERSION_ID              
AND CUSTOMER_ID=@CUSTOMER_ID               
END            
          
          
        
      
    
  





GO

