IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchPolicyMotorCycleGenInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchPolicyMotorCycleGenInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------------------------------------------        
Proc Name       : dbo.Proc_FetchPolicyMotorCycleGenInfo        
Created by      : Ashwani        
Date            : 10 Nov.,2005        
Purpose      : retrieving data from pol_auto_gen_info        
Revison History :        
Used In  : Wolverine        
-------------------------------------------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       ---------------------------------------------------------------------*/        
--drop proc Proc_FetchPolicyMotorCycleGenInfo        
CREATE PROC dbo.Proc_FetchPolicyMotorCycleGenInfo        
@POLICY_ID INT,        
@CUSTOMER_ID INT,        
@POLICY_VERSION_ID INT        
AS        
BEGIN        
SELECT         
ANY_NON_OWNED_VEH,        
CAR_MODIFIED,        
EXISTING_DMG,        
ANY_CAR_AT_SCH,        
SCHOOL_CARS_LIST,        
ANY_OTH_AUTO_INSU,        
ANY_OTH_INSU_COMP,        
OTHER_POLICY_NUMBER_LIST,        
H_MEM_IN_MILITARY,        
H_MEM_IN_MILITARY_LIST,        
DRIVER_SUS_REVOKED,        
PHY_MENTL_CHALLENGED,        
ANY_FINANCIAL_RESPONSIBILITY,        
INS_AGENCY_TRANSFER,        
COVERAGE_DECLINED,        
AGENCY_VEH_INSPECTED,        
USE_AS_TRANSPORT_FEE,        
SALVAGE_TITLE,        
ANY_ANTIQUE_AUTO,        
ANTIQUE_AUTO_LIST,        
REMARKS,        
IS_ACTIVE,        
CREATED_BY,        
CREATED_DATETIME,        
MODIFIED_BY,        
LAST_UPDATED_DATETIME,        
IS_COMMERCIAL_USE,        
IS_USEDFOR_RACING,        
IS_COST_OVER_DEFINED_LIMIT,        
IS_MORE_WHEELS,        
IS_EXTENDED_FORKS,        
IS_LICENSED_FOR_ROAD,        
IS_MODIFIED_INCREASE_SPEED,        
IS_MODIFIED_KIT,        
IS_TAKEN_OUT,        
IS_CONVICTED_CARELESS_DRIVE,        
IS_CONVICTED_ACCIDENT,        
MULTI_POLICY_DISC_APPLIED,        
ANY_NON_OWNED_VEH_MC_DESC,        
EXISTING_DMG_MC_DESC,        
ANY_OTH_INSU_COMP_MC_DESC,        
DRIVER_SUS_REVOKED_MC_DESC,        
PHY_MENTL_CHALLENGED_MC_DESC,        
ANY_FINANCIAL_RESPONSIBILITY_MC_DESC,        
INS_AGENCY_TRANSFER_MC_DESC,        
COVERAGE_DECLINED_MC_DESC,        
AGENCY_VEH_INSPECTED_MC_DESC,        
SALVAGE_TITLE_MC_DESC,        
IS_COMMERCIAL_USE_DESC,        
IS_USEDFOR_RACING_DESC,        
IS_COST_OVER_DEFINED_LIMIT_DESC,        
IS_MORE_WHEELS_DESC,        
IS_EXTENDED_FORKS_DESC,        
IS_LICENSED_FOR_ROAD_DESC,        
IS_MODIFIED_INCREASE_SPEED_DESC,        
IS_MODIFIED_KIT_DESC,        
IS_TAKEN_OUT_DESC,        
IS_CONVICTED_CARELESS_DRIVE_DESC,        
IS_CONVICTED_ACCIDENT_DESC,        
MULTI_POLICY_DISC_APPLIED_MC_DESC,        
FullName,        
convert(varchar,DATE_OF_BIRTH,101) as  DATE_OF_BIRTH,        
DrivingLisence,        
InsuredElseWhere,        
CompanyName,        
PolicyNumber,        
IS_OTHER_THAN_INSURED,        
CURR_RES_TYPE,        
WhichCycle,        
COST_EQUIPMENT_DESC,    
YEARS_INSU_WOL,    
YEARS_INSU,  
ANY_PRIOR_LOSSES,  
ANY_PRIOR_LOSSES_DESC,  
APPLY_PERS_UMB_POL,  
APPLY_PERS_UMB_POL_DESC  
      
FROM POL_AUTO_GEN_INFO         
WHERE  POLICY_ID=@POLICY_ID AND CUSTOMER_ID=@CUSTOMER_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID        
END        
        
        
        
      
    
  



GO

