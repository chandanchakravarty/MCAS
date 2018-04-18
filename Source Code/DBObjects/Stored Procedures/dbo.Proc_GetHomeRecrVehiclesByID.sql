IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHomeRecrVehiclesByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHomeRecrVehiclesByID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                    
Proc Name   : dbo.Proc_GetHomeRecrVehiclesByID                   
Created by  : Pradeep                    
Date        : 23 May,2005                  
Purpose     :                     
Revison History  :          
    
Modified By   : Mohit Gupta    
Modified On   : 7/11/2005    
Purpose       : Commenting displacement         
                   
 ------------------------------------------------------------                                
Date     Review By          Comments                              
                     
------   ------------       -------------------------*/              
--drop proc Proc_GetHomeRecrVehiclesByID                      
CREATE PROCEDURE dbo.Proc_GetHomeRecrVehiclesByID            
(            
             
 @CUSTOMER_ID Int,            
 @APP_ID Int,            
 @APP_VERSION_ID SmallInt,            
 @REC_VEH_ID SmallInt            
)            
            
As            
BEGIN            
SELECT             
 RV.CUSTOMER_ID,            
 RV.APP_ID,            
 RV.APP_VERSION_ID,            
 RV.REC_VEH_ID,            
 RV.COMPANY_ID_NUMBER,            
 RV.YEAR,            
 RV.MAKE,            
 RV.MODEL,            
 RV.SERIAL,            
 RV.STATE_REGISTERED,            
 MLV.LOOKUP_UNIQUE_ID as VEHICLE_TYPE,            
 MLV.LOOKUP_UNIQUE_ID as VEHICLE_TYPE_NAME,            
 RV.MANUFACTURER_DESC,            
 RV.HORSE_POWER,            
 RV.REMARKS,            
 RV.USED_IN_RACE_SPEED,            
 RV.PRIOR_LOSSES,            
 RV.IS_UNIT_REG_IN_OTHER_STATE,            
 RV.RISK_DECL_BY_OTHER_COMP,            
 RV.DESC_RISK_DECL_BY_OTHER_COMP,            
 RV.VEHICLE_MODIFIED,            
 RV.ACTIVE,            
 RV.CREATED_BY,            
 RV.CREATED_DATETIME,            
 RV.MODIFIED_BY,            
 RV.LAST_UPDATED_DATETIME  ,          
 RV.INSURING_VALUE ,  
 RV.DEDUCTIBLE,  
 convert(char,AL.APP_EFFECTIVE_DATE,101) AS APP_EFFECTIVE_DATE,
 RV.UNIT_RENTED, 
 RV.UNIT_OWNED_DEALERS,
 RV.YOUTHFUL_OPERATOR_UNDER_25 ,
 --Added LIABILITY , MEDICAL_PAYMENTS , PHYSICAL_DAMAGE For Itrack Issue #6710    
 RV.LIABILITY ,  
 RV.MEDICAL_PAYMENTS ,
 RV.PHYSICAL_DAMAGE  
 
  
FROM APP_HOME_OWNER_RECREATIONAL_VEHICLES RV   
LEFT OUTER JOIN APP_LIST AL
ON 
AL.CUSTOMER_ID= RV.CUSTOMER_ID AND
AL.APP_ID = RV.APP_ID AND
AL.APP_VERSION_ID = RV.APP_VERSION_ID	         
LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON            
 MLV.LOOKUP_UNIQUE_ID = RV.VEHICLE_TYPE            
WHERE RV.CUSTOMER_ID = @CUSTOMER_ID AND            
      RV.APP_ID = @APP_ID AND             
      RV.APP_VERSION_ID = @APP_VERSION_ID AND            
      RV.REC_VEH_ID = @REC_VEH_ID                

END








GO

