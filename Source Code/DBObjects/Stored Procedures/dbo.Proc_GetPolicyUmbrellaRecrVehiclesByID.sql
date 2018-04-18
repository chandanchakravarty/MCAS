IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyUmbrellaRecrVehiclesByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyUmbrellaRecrVehiclesByID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
 
/*----------------------------------------------------------              
Proc Name   : dbo.Proc_GetPolicyUmbrellaRecrVehiclesByID             
Created by  : Sumit Chhabra              
Date        : 27 March,2006            
Purpose     :               
Revison History  :                    
 ------------------------------------------------------------                          
Date     Review By          Comments                        
               
------   ------------       -------------------------*/        
      
CREATE     PROCEDURE dbo.Proc_GetPolicyUmbrellaRecrVehiclesByID      
(      
       
 @CUSTOMER_ID Int,      
 @POLICY_ID Int,      
 @POLICY_VERSION_ID SmallInt,      
 @REC_VEH_ID SmallInt      
)      
      
As      
      
SELECT       
 RV.CUSTOMER_ID,      
 RV.POLICY_ID,      
 RV.POLICY_VERSION_ID,      
 RV.REC_VEH_ID,      
 RV.COMPANY_ID_NUMBER,      
 RV.YEAR,      
 RV.MAKE,      
 RV.MODEL,      
 RV.SERIAL,      
 RV.STATE_REGISTERED,      
 MLV.LOOKUP_UNIQUE_ID as VEHICLE_TYPE,      
 MLV.LOOKUP_VALUE_DESC as VEHICLE_TYPE_NAME,      
 RV.MANUFACTURER_DESC,      
 RV.HORSE_POWER,      
 RV.DISPLACEMENT,      
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
 RV.LAST_UPDATED_DATETIME,    
 RV.VEHICLE_MODIFIED_DETAILS,  
 RV.VEH_LIC_ROAD,  
 RV.REC_VEH_TYPE,  
 RV.REC_VEH_TYPE_DESC,  
 RV.USED_IN_RACE_SPEED_CONTEST,
 ISNULL(RV.OTHER_POLICY,'') AS OTHER_POLICY,
 ISNULL(RV.C44,'') AS C44,
 ISNULL(RV.IS_BOAT_EXCLUDED,'') AS IS_BOAT_EXCLUDED      
FROM POL_UMBRELLA_RECREATIONAL_VEHICLES RV      
LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON      
 MLV.LOOKUP_UNIQUE_ID = RV.VEHICLE_TYPE      
WHERE CUSTOMER_ID = @CUSTOMER_ID AND      
      POLICY_ID = @POLICY_ID AND       
      POLICY_VERSION_ID = @POLICY_VERSION_ID AND      
      REC_VEH_ID = @REC_VEH_ID          
       
         
      
      
      
      
      
      
      
      
      
      
    
    
    
  







GO

