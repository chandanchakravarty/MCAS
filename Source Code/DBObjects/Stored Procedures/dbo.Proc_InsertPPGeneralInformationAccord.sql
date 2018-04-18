IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPPGeneralInformationAccord]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPPGeneralInformationAccord]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
--drop proc Proc_InsertPPGeneralInformationAccord 
CREATE PROC dbo.Proc_InsertPPGeneralInformationAccord          
(          
 @CUSTOMER_ID        int,          
 @APP_ID         int,          
 @APP_VERSION_ID       smallint,          
 @ANY_NON_OWNED_VEH        nchar(1),          
 @ANY_NON_OWNED_VEH_PP_DESC              varchar(50),          
 @CAR_MODIFIED        nchar(1),          
 @CAR_MODIFIED_DESC     varchar(50),          
 @EXISTING_DMG        nchar(1),          
 @EXISTING_DMG_PP_DESC                   varchar(50),          
 @ANY_CAR_AT_SCH        nchar(1),          
 @ANY_CAR_AT_SCH_DESC             varchar(50),          
 @ANY_OTH_AUTO_INSU        nchar(1),          
 @ANY_OTH_AUTO_INSU_DESC   varchar(50),          
 @ANY_OTH_INSU_COMP        nchar(1),          
 @ANY_OTH_INSU_COMP_PP_DESC  varchar(50),          
 @H_MEM_IN_MILITARY        nchar(1),          
 @H_MEM_IN_MILITARY_DESC   varchar(50),          
 @DRIVER_SUS_REVOKED       nchar(1),          
 @DRIVER_SUS_REVOKED_PP_DESC  varchar(50),          
 @PHY_MENTL_CHALLENGED       nchar(1),          
 @PHY_MENTL_CHALLENGED_PP_DESC  varchar(50),          
 @ANY_FINANCIAL_RESPONSIBILITY      nchar(1),          
 @ANY_FINANCIAL_RESPONSIBILITY_PP_DESC varchar(50),          
 @INS_AGENCY_TRANSFER       nchar(1),          
 @INS_AGENCY_TRANSFER_PP_DESC  varchar(50),          
 @COVERAGE_DECLINED        nchar(1),          
 @COVERAGE_DECLINED_PP_DESC  varchar(50),          
 @AGENCY_VEH_INSPECTED       nchar(1),          
 @AGENCY_VEH_INSPECTED_PP_DESC  varchar(50),          
 @USE_AS_TRANSPORT_FEE       nchar(20),          
 @USE_AS_TRANSPORT_FEE_DESC  varchar(50),          
 @SALVAGE_TITLE       nvarchar(5),          
 @SALVAGE_TITLE_PP_DESC   varchar(50),          
 @ANY_ANTIQUE_AUTO   nchar(1),          
 @ANY_ANTIQUE_AUTO_DESC   varchar(50),          
 @REMARKS    nvarchar(255),          
 @IS_ACTIVE    nchar(1),          
 @CREATED_BY        int,          
 @CREATED_DATETIME        datetime,          
 @MODIFIED_BY    int,          
 @LAST_UPDATED_DATETIME   datetime,          
 @INSERTUPDATE    char(1),          
 @MULTI_POLICY_DISC_APPLIED  NCHAR(2),          
 @MULTI_POLICY_DISC_APPLIED_PP_DESC     varchar(50) ,        
@FullName varchar(50),        
@DATE_OF_BIRTH datetime,        
@DrivingLisence varchar(20),        
@InsuredElseWhere nchar(1),        
@CompanyName varchar(50),       
@PolicyNumber varchar(50),        
@IS_OTHER_THAN_INSURED nchar(10),        
@CURR_RES_TYPE nvarchar(10),        
@WhichCycle nvarchar(10) = null,        
@COST_EQUIPMENT_DESC decimal(18),      
@YEARS_INSU_WOL smallint,      
@YEARS_INSU smallint,  
@ANY_PRIOR_LOSSES       nvarchar(5)=null,          
@ANY_PRIOR_LOSSES_DESC   varchar(50)=null         
      
      
)          
AS          
BEGIN          
     
  --Inserting the record          
  --SELECT @CUSTOMER_ID = IsNull(Max(CUSTOMER_ID),0) + 1   FROM APP_AUTO_GEN_INFO          
 INSERT INTO APP_AUTO_GEN_INFO          
 (          
  CUSTOMER_ID        ,          
  APP_ID         ,          
  APP_VERSION_ID       ,          
  ANY_NON_OWNED_VEH        ,          
  ANY_NON_OWNED_VEH_PP_DESC  ,          
  CAR_MODIFIED        ,          
  CAR_MODIFIED_DESC             ,          
  EXISTING_DMG        ,          
  EXISTING_DMG_PP_DESC   ,          
  ANY_CAR_AT_SCH        ,          
  ANY_CAR_AT_SCH_DESC   ,          
  ANY_OTH_AUTO_INSU        ,          
  ANY_OTH_AUTO_INSU_DESC   ,          
  ANY_OTH_INSU_COMP        ,          
  ANY_OTH_INSU_COMP_PP_DESC  ,            
  H_MEM_IN_MILITARY        ,          
  H_MEM_IN_MILITARY_DESC   ,          
  DRIVER_SUS_REVOKED               ,    
  DRIVER_SUS_REVOKED_PP_DESC    ,          
  PHY_MENTL_CHALLENGED       ,          
                PHY_MENTL_CHALLENGED_PP_DESC            ,          
  ANY_FINANCIAL_RESPONSIBILITY      ,          
  ANY_FINANCIAL_RESPONSIBILITY_PP_DESC ,          
  INS_AGENCY_TRANSFER       ,          
  INS_AGENCY_TRANSFER_PP_DESC  ,          
  COVERAGE_DECLINED        ,          
  COVERAGE_DECLINED_PP_DESC  ,     
 AGENCY_VEH_INSPECTED_PP_DESC            ,          
  AGENCY_VEH_INSPECTED       ,          
  USE_AS_TRANSPORT_FEE       ,          
  USE_AS_TRANSPORT_FEE_DESC  ,          
  SALVAGE_TITLE       ,          
  SALVAGE_TITLE_PP_DESC   ,          
  ANY_ANTIQUE_AUTO   ,          
  ANY_ANTIQUE_AUTO_DESC                   ,          
  REMARKS             ,          
  IS_ACTIVE    ,          
  CREATED_BY                ,          
  CREATED_DATETIME               ,          
  MULTI_POLICY_DISC_APPLIED         ,          
  MULTI_POLICY_DISC_APPLIED_PP_DESC,        
FullName,        
DATE_OF_BIRTH,        
DrivingLisence,        
InsuredElseWhere,        
CompanyName,        
PolicyNumber,        
IS_OTHER_THAN_INSURED,        
CURR_RES_TYPE,        
WhichCycle   ,        
COST_EQUIPMENT_DESC,      
YEARS_INSU_WOL,      
YEARS_INSU,  
ANY_PRIOR_LOSSES,  
ANY_PRIOR_LOSSES_DESC       
          
 )          
 VALUES          
 (          
  @CUSTOMER_ID        ,          
  @APP_ID         ,          
  @APP_VERSION_ID       ,          
  @ANY_NON_OWNED_VEH        ,          
  @ANY_NON_OWNED_VEH_PP_DESC  ,          
  @CAR_MODIFIED        ,          
  @CAR_MODIFIED_DESC   ,          
  @EXISTING_DMG        ,          
  @EXISTING_DMG_PP_DESC   ,          
  @ANY_CAR_AT_SCH        ,          
  @ANY_CAR_AT_SCH_DESC   ,          
  @ANY_OTH_AUTO_INSU     ,          
  @ANY_OTH_AUTO_INSU_DESC   ,          
  @ANY_OTH_INSU_COMP        ,          
  @ANY_OTH_INSU_COMP_PP_DESC  ,          
  @H_MEM_IN_MILITARY        ,          
  @H_MEM_IN_MILITARY_DESC   ,          
  @DRIVER_SUS_REVOKED       ,          
  @DRIVER_SUS_REVOKED_PP_DESC  ,          
  @PHY_MENTL_CHALLENGED       ,          
  @PHY_MENTL_CHALLENGED_PP_DESC  ,          
  @ANY_FINANCIAL_RESPONSIBILITY      ,          
  @ANY_FINANCIAL_RESPONSIBILITY_PP_DESC    ,          
  @INS_AGENCY_TRANSFER       ,          
  @INS_AGENCY_TRANSFER_PP_DESC  ,          
  @COVERAGE_DECLINED        ,          
  @COVERAGE_DECLINED_PP_DESC  ,          
        
  @AGENCY_VEH_INSPECTED_PP_DESC  ,          
  @AGENCY_VEH_INSPECTED       ,          
  @USE_AS_TRANSPORT_FEE       ,          
  @USE_AS_TRANSPORT_FEE_DESC  ,          
  @SALVAGE_TITLE       ,          
  @SALVAGE_TITLE_PP_DESC   ,          
  @ANY_ANTIQUE_AUTO   ,          
  @ANY_ANTIQUE_AUTO_DESC   ,          
  @REMARKS    ,          
  @IS_ACTIVE    ,          
  @CREATED_BY        ,          
  @CREATED_DATETIME        ,          
  @MULTI_POLICY_DISC_APPLIED  ,          
  @MULTI_POLICY_DISC_APPLIED_PP_DESC  ,        
  @FullName,        
  @DATE_OF_BIRTH,        
  @DrivingLisence,        
  @InsuredElseWhere,        
  @CompanyName,        
  @PolicyNumber,        
  @IS_OTHER_THAN_INSURED,        
  @CURR_RES_TYPE,        
  @WhichCycle,        
  @COST_EQUIPMENT_DESC,      
  @YEARS_INSU_WOL,      
  @YEARS_INSU,  
  @ANY_PRIOR_LOSSES,  
  @ANY_PRIOR_LOSSES_DESC       
      
 )  
           
END        
        
        
        
  
  
  
  



GO

