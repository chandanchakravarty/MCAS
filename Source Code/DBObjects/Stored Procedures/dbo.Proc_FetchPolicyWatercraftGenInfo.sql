IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchPolicyWatercraftGenInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchPolicyWatercraftGenInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/****** Object:  Stored Procedure dbo.Proc_FetchPolicyWatercraftGenInfo    Script Date: 5/15/2006 4:48:28 PM ******/

/*----------------------------------------------------------                      
Proc Name           :  Dbo.Proc_FetchPolicyWatercraftGenInfo                      
Created by            :  Vijay Arora          
Date                    :  01-12-2005          
Purpose                :                       
Revison History  :                      
Used In                 :    Wolverine                        
------------------------------------------------------------*/          
--      drop proc  dbo.Proc_FetchPolicyWatercraftGenInfo                
CREATE  PROC dbo.Proc_FetchPolicyWatercraftGenInfo                
(          
@CUSTOMER_ID INT,                
@POLICY_ID INT,                
@POLICY_VERSION_ID INT                
)          
AS                
              
BEGIN                
SELECT                 
 HAS_CURR_ADD_THREE_YEARS,                
 PHY_MENTL_CHALLENGED,                
 DRIVER_SUS_REVOKED,                
 IS_CONVICTED_ACCIDENT,                
 ANY_OTH_INSU_COMP,                
 OTHER_POLICY_NUMBER_LIST,                
 ANY_LOSS_THREE_YEARS,                
 COVERAGE_DECLINED,                
 IS_CREDIT,                
 CREDIT_DETAILS,                
 IS_RENTED_OTHERS,                
 IS_REGISTERED_OTHERS ,              
 HAS_CURR_ADD_THREE_YEARS_DESC ,              
 PHY_MENTL_CHALLENGED_DESC,              
 DRIVER_SUS_REVOKED_DESC,              
 IS_CONVICTED_ACCIDENT_DESC,              
 ANY_LOSS_THREE_YEARS_DESC,              
 COVERAGE_DECLINED_DESC,              
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
 MULTI_POLICY_DISC_APPLIED,      
 MULTI_POLICY_DISC_APPLIED_PP_DESC,
 ANY_BOAT_AMPHIBIOUS,      
 ANY_BOAT_AMPHIBIOUS_DESC,
 ANY_BOAT_RESIDENCE, ANY_BOAT_RESIDENCE_DESC,
 IS_BOAT_USED_IN_ANY_WATER, IS_BOAT_USED_IN_ANY_WATER_DESC
                                     
FROM           
POL_WATERCRAFT_GEN_INFO                 
WHERE           
 CUSTOMER_ID=@CUSTOMER_ID                 
 AND POLICY_ID=@POLICY_ID           
 AND POLICY_VERSION_ID=@POLICY_VERSION_ID                
END         






GO

