IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Get_PolRecrVehiclesByID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Get_PolRecrVehiclesByID]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                      
Proc Name   : dbo.Proc_Get_PolRecrVehiclesByID                     
Created by  : Pradeep                      
Date        : 11/10/2005                    
Purpose     :                       
Revison History  :                             
 ------------------------------------------------------------                                  
Date     Review By          Comments                                
                       
------   ------------       -------------------------*/                
 -- drop proc Proc_Get_PolRecrVehiclesByID              
CREATE PROCEDURE dbo.Proc_Get_PolRecrVehiclesByID              
(              
               
 @CUSTOMER_ID Int,              
 @POLICY_ID Int,              
 @POLICY_VERSION_ID SmallInt,              
 @REC_VEH_ID SmallInt              
)              
              
As              
begin              
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
 --RV.DISPLACEMENT,              
 RV.REMARKS,              
 RV.USED_IN_RACE_SPEED,              
 RV.PRIOR_LOSSES,              
 RV.IS_UNIT_REG_IN_OTHER_STATE,              
 RV.RISK_DECL_BY_OTHER_COMP,              
 RV.DESC_RISK_DECL_BY_OTHER_COMP,              
 RV.VEHICLE_MODIFIED,              
 RV.ACTIVE,              
 RV.INSURING_VALUE ,    
 RV.DEDUCTIBLE,
 RV.UNIT_RENTED, 
 RV.UNIT_OWNED_DEALERS,
 RV.YOUTHFUL_OPERATOR_UNDER_25,
 --Added LIABILITY , MEDICAL_PAYMENTS , PHYSICAL_DAMAGE For Itrack Issue #6710 
 RV.LIABILITY ,  
 RV.MEDICAL_PAYMENTS ,
 RV.PHYSICAL_DAMAGE ,
 case 
 when P.POLICY_STATUS='UISSUE' or P.POLICY_STATUS='SUSPENDED' then CONVERT(CHAR,P.APP_INCEPTION_DATE,101) 
 else '' 
 end as APP_INCEPTION_DATE

FROM POL_HOME_OWNER_RECREATIONAL_VEHICLES RV 
LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV ON              
 MLV.LOOKUP_UNIQUE_ID = RV.VEHICLE_TYPE 
LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST P ON
P.CUSTOMER_ID = RV.CUSTOMER_ID AND
P.POLICY_ID = RV.POLICY_ID AND
P.POLICY_VERSION_ID = RV.POLICY_VERSION_ID             
WHERE RV.CUSTOMER_ID = @CUSTOMER_ID AND              
      RV.POLICY_ID = @POLICY_ID AND               
      RV.POLICY_VERSION_ID = @POLICY_VERSION_ID AND              
      RV.REC_VEH_ID = @REC_VEH_ID
--	 AND P.POLICY_STATUS IN ('UISSUE','SUSPENDED')
end







GO

