IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyGeneralInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyGeneralInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
PROC NAME        : DBO.PROC_GETPOLICYGENERALINFORMATION       
CREATED BY        : VIJAY ARORA    
DATE              : 08-11-2005    
PURPOSE         : GET THE INFORMATION OF POLICY FROM POL_AUTO_GEN_INFO    
REVISON HISTORY  :        
USED IN          : WOLVERINE        
  
Modified By : Shafee   
Modified On : 17-01-2006  
Purpose : Add Column for  YEARS_INSU_WOL,YEARS_INSU  
        
------------------------------------------------------------        
DATE     REVIEW BY          COMMENTS        
------   ------------       -------------------------*/   
--  drop proc Proc_GetPolicyGeneralInformation       
CREATE PROC dbo.Proc_GetPolicyGeneralInformation        
@CUSTOMER_ID INT,        
@POLICY_ID INT,        
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
  ANY_NON_OWNED_VEH_PP_DESC,        
  CAR_MODIFIED_DESC,        
  EXISTING_DMG_PP_DESC,        
  ANY_CAR_AT_SCH_DESC,        
  ANY_OTH_AUTO_INSU_DESC,        
  ANY_OTH_INSU_COMP_PP_DESC,        
  H_MEM_IN_MILITARY_DESC,        
  DRIVER_SUS_REVOKED_PP_DESC,        
  PHY_MENTL_CHALLENGED_PP_DESC,        
  ANY_FINANCIAL_RESPONSIBILITY_PP_DESC,        
  INS_AGENCY_TRANSFER_PP_DESC,        
  COVERAGE_DECLINED_PP_DESC,        
  AGENCY_VEH_INSPECTED_PP_DESC,        
  USE_AS_TRANSPORT_FEE_DESC,        
  SALVAGE_TITLE_PP_DESC,        
  ANY_ANTIQUE_AUTO_DESC,        
  MULTI_POLICY_DISC_APPLIED_PP_DESC,        
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
  FULLNAME,        
  CONVERT(VARCHAR,DATE_OF_BIRTH,101) AS DATE_OF_BIRTH,        
  DRIVINGLISENCE,        
  INSUREDELSEWHERE,        
  COMPANYNAME,        
  POLICYNUMBER,        
  IS_OTHER_THAN_INSURED,        
  CURR_RES_TYPE,        
  WHICHCYCLE,      
  COST_EQUIPMENT_DESC,  
  YEARS_INSU_WOL,  
  YEARS_INSU,
  ANY_PRIOR_LOSSES,
  ANY_PRIOR_LOSSES_DESC  
     
 FROM     
  POL_AUTO_GEN_INFO         
 WHERE      
  POLICY_ID=@POLICY_ID AND CUSTOMER_ID=@CUSTOMER_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID        
END      
    
  



GO

