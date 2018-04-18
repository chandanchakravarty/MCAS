IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyGeneralInformation]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyGeneralInformation]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------            
Proc Name       : dbo.Proc_InsertPolicyGeneralInformation            
Created by      : Vijay Arora        
Date            : 09-11-2005        
Purpose      : To add the General Information of Policy in table named POL_AUTO_GEN_INFO.        
Revison History :            
Used In   : Wolverine            
      
Modified By : Shafee       
Modified On : 17-01-2006      
Purpose : Add Column for  YEARS_INSU_WOL,YEARS_INSU      
      
------------------------------------------------------------            
Date     Review By          Comments            
------   ------------       -------------------------*/           
-- drop proc Proc_InsertPolicyGeneralInformation       
CREATE PROC [dbo].[Proc_InsertPolicyGeneralInformation]            
(            
 @CUSTOMER_ID          INT,            
 @POLICY_ID           INT,            
 @POLICY_VERSION_ID      SMALLINT,            
 @ANY_NON_OWNED_VEH         NCHAR(1),            
 @ANY_NON_OWNED_VEH_PP_DESC     VARCHAR(50),            
 @CAR_MODIFIED          NCHAR(1),            
 @CAR_MODIFIED_DESC      VARCHAR(50),            
 @EXISTING_DMG          NCHAR(1),            
 @EXISTING_DMG_PP_DESC          VARCHAR(50),            
 @ANY_CAR_AT_SCH         NCHAR(1),            
 @ANY_CAR_AT_SCH_DESC           VARCHAR(50),            
 @ANY_OTH_AUTO_INSU         NCHAR(1),            
 @ANY_OTH_AUTO_INSU_DESC    VARCHAR(50),            
 @ANY_OTH_INSU_COMP         NCHAR(1),            
 @ANY_OTH_INSU_COMP_PP_DESC   VARCHAR(50),            
 @H_MEM_IN_MILITARY         NCHAR(1),            
 @H_MEM_IN_MILITARY_DESC    VARCHAR(50),            
 @DRIVER_SUS_REVOKED        NCHAR(1),            
 @DRIVER_SUS_REVOKED_PP_DESC   VARCHAR(50),            
 @PHY_MENTL_CHALLENGED        NCHAR(1),            
 @PHY_MENTL_CHALLENGED_PP_DESC  VARCHAR(50),            
 @ANY_FINANCIAL_RESPONSIBILITY  NCHAR(1),            
 @ANY_FINANCIAL_RESPONSIBILITY_PP_DESC VARCHAR(50),            
 @INS_AGENCY_TRANSFER        NCHAR(1),            
 @INS_AGENCY_TRANSFER_PP_DESC   VARCHAR(50),            
 @COVERAGE_DECLINED         NCHAR(1),            
 @COVERAGE_DECLINED_PP_DESC   VARCHAR(50),            
 @AGENCY_VEH_INSPECTED        NCHAR(1),            
 @AGENCY_VEH_INSPECTED_PP_DESC  VARCHAR(50),            
 @USE_AS_TRANSPORT_FEE        NCHAR(20),            
 @USE_AS_TRANSPORT_FEE_DESC   VARCHAR(50),            
 @SALVAGE_TITLE         NVARCHAR(5),            
 @SALVAGE_TITLE_PP_DESC  VARCHAR(50),            
 @ANY_ANTIQUE_AUTO     NCHAR(1),            
 @ANY_ANTIQUE_AUTO_DESC    VARCHAR(50),            
 @REMARKS       NVARCHAR(255),            
 @IS_ACTIVE       NCHAR(1),            
 @CREATED_BY          INT,            
 @CREATED_DATETIME         DATETIME,            
 @MULTI_POLICY_DISC_APPLIED   NCHAR(2),            
 @MULTI_POLICY_DISC_APPLIED_PP_DESC  VARCHAR(50) ,          
 @COST_EQUIPMENT_DESC   VARCHAR(50),          
 @FULLNAME    VARCHAR(50),          
 @DATE_OF_BIRTH   DATETIME,          
 @DRIVINGLISENCE   VARCHAR(20),          
 @POLICYNUMBER    VARCHAR(50),           
 @COMPANYNAME    VARCHAR(50),          
 @INSUREDELSEWHERE   NCHAR(1),          
 @IS_OTHER_THAN_INSURED  NCHAR(10),          
 @CURR_RES_TYPE   NVARCHAR(10),          
 @WHICHCYCLE    NVARCHAR(10) = NULL ,      
 @YEARS_INSU_WOL smallint,      
 @YEARS_INSU smallint,    
 @ANY_PRIOR_LOSSES NVARCHAR(5) = NULL,    
 @ANY_PRIOR_LOSSES_DESC VARCHAR(50)=NULL      
      
        
)            
AS            
BEGIN   
  
  
           
 INSERT INTO POL_AUTO_GEN_INFO            
 (            
  CUSTOMER_ID,        
  POLICY_ID,        
  POLICY_VERSION_ID,        
  ANY_NON_OWNED_VEH,        
  ANY_NON_OWNED_VEH_PP_DESC,        
  CAR_MODIFIED,        
  CAR_MODIFIED_DESC,        
  EXISTING_DMG,        
  EXISTING_DMG_PP_DESC,        
  ANY_CAR_AT_SCH,        
  ANY_CAR_AT_SCH_DESC,        
  ANY_OTH_AUTO_INSU,        
  ANY_OTH_AUTO_INSU_DESC,        
  ANY_OTH_INSU_COMP,        
  ANY_OTH_INSU_COMP_PP_DESC,        
  H_MEM_IN_MILITARY,        
  H_MEM_IN_MILITARY_DESC,        
  DRIVER_SUS_REVOKED,        
  DRIVER_SUS_REVOKED_PP_DESC,        
  PHY_MENTL_CHALLENGED,        
  PHY_MENTL_CHALLENGED_PP_DESC,        
  ANY_FINANCIAL_RESPONSIBILITY,        
  ANY_FINANCIAL_RESPONSIBILITY_PP_DESC,        
  INS_AGENCY_TRANSFER,        
  INS_AGENCY_TRANSFER_PP_DESC,        
  COVERAGE_DECLINED,        
  COVERAGE_DECLINED_PP_DESC,        
  AGENCY_VEH_INSPECTED,        
  AGENCY_VEH_INSPECTED_PP_DESC,        
  USE_AS_TRANSPORT_FEE,        
  USE_AS_TRANSPORT_FEE_DESC,        
  SALVAGE_TITLE,      SALVAGE_TITLE_PP_DESC,        
  ANY_ANTIQUE_AUTO,        
  ANY_ANTIQUE_AUTO_DESC,        
  REMARKS,        
  IS_ACTIVE,        
  CREATED_BY,        
  CREATED_DATETIME,        
  MULTI_POLICY_DISC_APPLIED,        
  MULTI_POLICY_DISC_APPLIED_PP_DESC,        
  COST_EQUIPMENT_DESC,        
  FULLNAME,        
  DATE_OF_BIRTH,        
  DRIVINGLISENCE,        
  POLICYNUMBER,        
  COMPANYNAME,        
  INSUREDELSEWHERE,        
  IS_OTHER_THAN_INSURED,        
  CURR_RES_TYPE,        
  WHICHCYCLE ,      
  YEARS_INSU_WOL,      
  YEARS_INSU,    
  ANY_PRIOR_LOSSES,    
  ANY_PRIOR_LOSSES_DESC      
      
 )            
 VALUES       (            
  @CUSTOMER_ID,        
  @POLICY_ID,        
  @POLICY_VERSION_ID,        
  @ANY_NON_OWNED_VEH,        
  @ANY_NON_OWNED_VEH_PP_DESC,        
  @CAR_MODIFIED,        
  @CAR_MODIFIED_DESC,        
  @EXISTING_DMG,        
  @EXISTING_DMG_PP_DESC,        
  @ANY_CAR_AT_SCH,        
  @ANY_CAR_AT_SCH_DESC,        
  @ANY_OTH_AUTO_INSU,        
  @ANY_OTH_AUTO_INSU_DESC,        
  @ANY_OTH_INSU_COMP,        
  @ANY_OTH_INSU_COMP_PP_DESC,        
  @H_MEM_IN_MILITARY,        
  @H_MEM_IN_MILITARY_DESC,        
  @DRIVER_SUS_REVOKED,        
  @DRIVER_SUS_REVOKED_PP_DESC,        
  @PHY_MENTL_CHALLENGED,        
  @PHY_MENTL_CHALLENGED_PP_DESC,        
  @ANY_FINANCIAL_RESPONSIBILITY,        
  @ANY_FINANCIAL_RESPONSIBILITY_PP_DESC,        
  @INS_AGENCY_TRANSFER,        
  @INS_AGENCY_TRANSFER_PP_DESC,        
  @COVERAGE_DECLINED,        
  @COVERAGE_DECLINED_PP_DESC,        
  @AGENCY_VEH_INSPECTED,        
  @AGENCY_VEH_INSPECTED_PP_DESC,        
  @USE_AS_TRANSPORT_FEE,        
  @USE_AS_TRANSPORT_FEE_DESC,        
  @SALVAGE_TITLE,        
  @SALVAGE_TITLE_PP_DESC,        
  @ANY_ANTIQUE_AUTO,        
  @ANY_ANTIQUE_AUTO_DESC,        
  @REMARKS,        
  @IS_ACTIVE,        
  @CREATED_BY,        
  @CREATED_DATETIME,        
  @MULTI_POLICY_DISC_APPLIED,        
  @MULTI_POLICY_DISC_APPLIED_PP_DESC,        
  @COST_EQUIPMENT_DESC,        
  @FULLNAME,        
  @DATE_OF_BIRTH,        
  @DRIVINGLISENCE,        
  @POLICYNUMBER,        
  @COMPANYNAME,        
  @INSUREDELSEWHERE,        
  @IS_OTHER_THAN_INSURED,        
  @CURR_RES_TYPE,        
  @WHICHCYCLE ,      
  @YEARS_INSU_WOL,      
  @YEARS_INSU,    
  @ANY_PRIOR_LOSSES,    
  @ANY_PRIOR_LOSSES_DESC    
        
 )     
  
/*DONE BY SIBIN ON NOV 19 '08  
fetch insured with wolverine values from app_auto_gen_info                                                          
 set value of safe driver detail and premier driver detail on the basis of years with wolverine          
 if years with wolverine <3 then   
set premier driver to yes  
and safe driver to no  
 if years with wolverine >3 then  
 set safe driver to yes  
and premier driver to no  
*/  
 IF EXISTS(SELECT 1 FROM POL_DRIVER_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND   
POLICY_VERSION_ID=@POLICY_VERSION_ID AND ISNULL(IS_ACTIVE,'N') = 'Y')  
 BEGIN  
    IF(@YEARS_INSU_WOL>2)      
      BEGIN  
       UPDATE POL_DRIVER_DETAILS SET SAFE_DRIVER_RENEWAL_DISCOUNT=1,DRIVER_PREF_RISK=0 WHERE   
    CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  
      END  
   ELSE    
   BEGIN    
      UPDATE POL_DRIVER_DETAILS SET SAFE_DRIVER_RENEWAL_DISCOUNT=0,DRIVER_PREF_RISK=1 WHERE   
      CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID    
   END  
 END      
END            
      
GO

