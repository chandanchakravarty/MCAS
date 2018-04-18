IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPOLICY_AUTO_GEN_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPOLICY_AUTO_GEN_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  /*----------------------------------------------------------                
Proc Name        : dbo.Proc_InsertPOLICY_AUTO_GEN_INFO                
Created by       : Ashwini            
Date             : 10 Nov.,2005            
Purpose         : Insert the record into POL_AUTO_GEN_INFo            
Revison History  :                
Used In          : Wolverine                   
            
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
--drop proc Proc_InsertPOLICY_AUTO_GEN_INFO          
CREATE PROC Dbo.Proc_InsertPOLICY_AUTO_GEN_INFO                
(                
@CUSTOMER_ID     int,                
@POLICY_ID     int,                
@POLICY_VERSION_ID     smallint,                
@ANY_NON_OWNED_VEH     nchar(2),                
@EXISTING_DMG     nchar(2),                
@ANY_OTH_INSU_COMP     nchar(2),                
@DRIVER_SUS_REVOKED     nchar(2),                
@PHY_MENTL_CHALLENGED     nchar(2),                
@ANY_FINANCIAL_RESPONSIBILITY     nchar(2),                
@INS_AGENCY_TRANSFER     nchar(2),                
@COVERAGE_DECLINED     nchar(2),                
@AGENCY_VEH_INSPECTED     nchar(2),                
@USE_AS_TRANSPORT_FEE     nchar(2),                
@SALVAGE_TITLE     nvarchar(10),                
@REMARKS     nvarchar(255),                
@IS_ACTIVE     nchar(2),                
@CREATED_BY     int,                
@CREATED_DATETIME     datetime,                
@IS_COMMERCIAL_USE     char(1),                
@IS_USEDFOR_RACING     char(1),                
@IS_COST_OVER_DEFINED_LIMIT     char(1),                
@IS_MORE_WHEELS     char(1),                
@IS_EXTENDED_FORKS     char(1),                
@IS_LICENSED_FOR_ROAD     char(1),                
@IS_MODIFIED_INCREASE_SPEED     char(1),                
@IS_MODIFIED_KIT     char(1),                
@IS_TAKEN_OUT     char(1),                
@IS_CONVICTED_CARELESS_DRIVE     char(1),                
@IS_CONVICTED_ACCIDENT     char(1),                
@MULTI_POLICY_DISC_APPLIED NCHAR(2) ,              
              
@ANY_NON_OWNED_VEH_MC_DESC varchar(50),              
@EXISTING_DMG_MC_DESC varchar(50),              
@ANY_OTH_INSU_COMP_MC_DESC varchar(50),              
@DRIVER_SUS_REVOKED_MC_DESC varchar(50),              
@PHY_MENTL_CHALLENGED_MC_DESC varchar(50),              
@ANY_FINANCIAL_RESPONSIBILITY_MC_DESC varchar(50),              
@INS_AGENCY_TRANSFER_MC_DESC varchar(50),              
@COVERAGE_DECLINED_MC_DESC  varchar(50),              
@AGENCY_VEH_INSPECTED_MC_DESC varchar(50),              
@SALVAGE_TITLE_MC_DESC varchar(50),              
@IS_COMMERCIAL_USE_DESC varchar(50),              
@IS_USEDFOR_RACING_DESC varchar(50),              
@IS_COST_OVER_DEFINED_LIMIT_DESC varchar(50),              
@IS_MORE_WHEELS_DESC varchar(50),              
@IS_EXTENDED_FORKS_DESC varchar(50),              
@IS_LICENSED_FOR_ROAD_DESC varchar(50),              
@IS_MODIFIED_INCREASE_SPEED_DESC varchar(50),              
@IS_MODIFIED_KIT_DESC varchar(50),              
@IS_TAKEN_OUT_DESC varchar(50),              
@IS_CONVICTED_CARELESS_DRIVE_DESC varchar(50),              
@IS_CONVICTED_ACCIDENT_DESC varchar(50),              
@MULTI_POLICY_DISC_APPLIED_MC_DESC varchar(50)  ,            
@FullName varchar(50)=null,            
@DATE_OF_BIRTH datetime=null,            
@DrivingLisence varchar(20)=null,            
@InsuredElseWhere nchar(1)=null,            
@CompanyName varchar(50)=null,            
@PolicyNumber varchar(50)=null,            
@IS_OTHER_THAN_INSURED nchar(10)=null,            
@CURR_RES_TYPE nvarchar(10)=null,            
@WhichCycle nvarchar(10) = null,          
@YEARS_INSU_WOL smallint,          
@YEARS_INSU smallint,        
@ANY_PRIOR_LOSSES nvarchar(10)=null,        
@ANY_PRIOR_LOSSES_DESC varchar(50)=null,        
@APPLY_PERS_UMB_POL int = null,        
@APPLY_PERS_UMB_POL_DESC varchar(50) = null        
           
)      
AS                
BEGIN                
INSERT INTO POL_AUTO_GEN_INFO                
(                
CUSTOMER_ID,                
POLICY_ID,                
POLICY_VERSION_ID,               
ANY_NON_OWNED_VEH,                
EXISTING_DMG,                
ANY_OTH_INSU_COMP,                
DRIVER_SUS_REVOKED,                
PHY_MENTL_CHALLENGED,                
ANY_FINANCIAL_RESPONSIBILITY,                
INS_AGENCY_TRANSFER,                
COVERAGE_DECLINED,                
AGENCY_VEH_INSPECTED,                
USE_AS_TRANSPORT_FEE,                
SALVAGE_TITLE,                
REMARKS,                
IS_ACTIVE,               
CREATED_BY,                
CREATED_DATETIME,                
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
MULTI_POLICY_DISC_APPLIED ,               
              
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
MULTI_POLICY_DISC_APPLIED_MC_DESC  ,            
FullName,            
DATE_OF_BIRTH,            
DrivingLisence,            
InsuredElseWhere,            
CompanyName,            
PolicyNumber,            
IS_OTHER_THAN_INSURED,            
CURR_RES_TYPE,             
WhichCycle ,          
YEARS_INSU_WOL,          
YEARS_INSU,        
ANY_PRIOR_LOSSES,        
ANY_PRIOR_LOSSES_DESC,        
APPLY_PERS_UMB_POL,        
APPLY_PERS_UMB_POL_DESC          
          
)                
VALUES                
(                
@CUSTOMER_ID,                
@POLICY_ID,                
@POLICY_VERSION_ID,                
@ANY_NON_OWNED_VEH,                
@EXISTING_DMG,                
@ANY_OTH_INSU_COMP,                
@DRIVER_SUS_REVOKED,                
@PHY_MENTL_CHALLENGED,                
@ANY_FINANCIAL_RESPONSIBILITY,                
@INS_AGENCY_TRANSFER,                
@COVERAGE_DECLINED,                
@AGENCY_VEH_INSPECTED,                
@USE_AS_TRANSPORT_FEE,                
@SALVAGE_TITLE,                
@REMARKS,                
@IS_ACTIVE,                
@CREATED_BY,                
@CREATED_DATETIME,                
@IS_COMMERCIAL_USE,                
@IS_USEDFOR_RACING,                
@IS_COST_OVER_DEFINED_LIMIT,                
@IS_MORE_WHEELS,                
@IS_EXTENDED_FORKS,                
@IS_LICENSED_FOR_ROAD,                
@IS_MODIFIED_INCREASE_SPEED,                
@IS_MODIFIED_KIT,                
@IS_TAKEN_OUT,                
@IS_CONVICTED_CARELESS_DRIVE,                
@IS_CONVICTED_ACCIDENT,                
@MULTI_POLICY_DISC_APPLIED ,              
              
@ANY_NON_OWNED_VEH_MC_DESC ,              
@EXISTING_DMG_MC_DESC ,              
@ANY_OTH_INSU_COMP_MC_DESC ,              
@DRIVER_SUS_REVOKED_MC_DESC ,              
@PHY_MENTL_CHALLENGED_MC_DESC ,              
@ANY_FINANCIAL_RESPONSIBILITY_MC_DESC ,              
@INS_AGENCY_TRANSFER_MC_DESC ,              
@COVERAGE_DECLINED_MC_DESC  ,              
@AGENCY_VEH_INSPECTED_MC_DESC ,              
@SALVAGE_TITLE_MC_DESC ,              
@IS_COMMERCIAL_USE_DESC ,              
@IS_USEDFOR_RACING_DESC ,              
@IS_COST_OVER_DEFINED_LIMIT_DESC ,              
@IS_MORE_WHEELS_DESC ,              
@IS_EXTENDED_FORKS_DESC ,              
@IS_LICENSED_FOR_ROAD_DESC ,              
@IS_MODIFIED_INCREASE_SPEED_DESC ,              
@IS_MODIFIED_KIT_DESC ,              
@IS_TAKEN_OUT_DESC ,              
@IS_CONVICTED_CARELESS_DRIVE_DESC ,              
@IS_CONVICTED_ACCIDENT_DESC ,              
@MULTI_POLICY_DISC_APPLIED_MC_DESC,            
@FullName,            
@DATE_OF_BIRTH,            
@DrivingLisence,            
@InsuredElseWhere,            
@CompanyName,            
@PolicyNumber,            
@IS_OTHER_THAN_INSURED,            
@CURR_RES_TYPE,             
@WhichCycle  ,          
@YEARS_INSU_WOL,          
@YEARS_INSU,        
@ANY_PRIOR_LOSSES,        
@ANY_PRIOR_LOSSES_DESC,        
@APPLY_PERS_UMB_POL,        
@APPLY_PERS_UMB_POL_DESC        
          
)         
      
 
/* Commented by Charles on 2-Jul-09 for Itrack issue 6012     
/*             
Added by Charles on 24-Jun-2009 for Itrack 6003,            
to make changes to - transfer renewal discount combo box on driver page            
when - the discount condition satifies at the underwriting rules page            
*/            
          
IF EXISTS (SELECT TRANSFEREXPERIENCE_RENEWALCREDIT FROM POL_DRIVER_DETAILS   
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID   
AND ISNULL(IS_ACTIVE,'N') = 'Y')          
BEGIN           
          
DECLARE @POL_STATE SMALLINT, @POLICY_EFFECTIVE_DATE DATETIME;          
SET @POL_STATE=0; SET @POLICY_EFFECTIVE_DATE=NULL;         
          
SELECT @POL_STATE=ISNULL(STATE_ID,'0'), @POLICY_EFFECTIVE_DATE=ISNULL(POLICY_EFFECTIVE_DATE,'')   
FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID   
AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND ISNULL(IS_ACTIVE,'N') = 'Y' ;         
          
UPDATE POL_DRIVER_DETAILS SET TRANSFEREXPERIENCE_RENEWALCREDIT =  
CASE   
WHEN @POL_STATE=14 AND (@YEARS_INSU >=1 OR @YEARS_INSU_WOL>=1) AND DATEDIFF(dd,DATE_LICENSED,@POLICY_EFFECTIVE_DATE)>365     
     THEN 1   
WHEN @POL_STATE=22 AND @YEARS_INSU >=1 OR @YEARS_INSU_WOL>=1   
 THEN 1     
ELSE NULL  
END  
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID             
  AND ISNULL(IS_ACTIVE,'N') = 'Y' ;    
          
END  
          
-- Added till here  
*/           
             
END            
GO

