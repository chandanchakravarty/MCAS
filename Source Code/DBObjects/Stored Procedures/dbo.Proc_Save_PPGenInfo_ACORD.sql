IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Save_PPGenInfo_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Save_PPGenInfo_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------              
Proc Name       : dbo.Proc_Save_PPGenInfo_ACORD              
Created by      : Pradeep               
Date            : 10/11/2005              
Purpose     :  Inserts/Upadtes record in PP Gen Info table          
Revison History :              
Used In  : Wolverine              
  --DROP PROC dbo.Proc_Save_PPGenInfo_ACORD          
------------------------------------------------------------              
Date     Review By          Comments              
------   ------------       -------------------------*/    
  
create PROC Dbo.Proc_Save_PPGenInfo_ACORD              
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
@IS_COMMERCIAL_USE Char(1),        
@IS_USEDFOR_RACING Char(1),        
@IS_COST_OVER_DEFINED_LIMIT Char(1),        
@IS_MORE_WHEELS Char(1),        
@IS_EXTENDED_FORKS Char(1),        
@IS_LICENSED_FOR_ROAD Char(1),        
@IS_MODIFIED_INCREASE_SPEED Char(1),        
@IS_MODIFIED_KIT Char(1),        
@IS_TAKEN_OUT Char(1),        
@IS_CONVICTED_CARELESS_DRIVE  Char(1),        
@IS_CONVICTED_ACCIDENT    Char(1) ,      
@YEARS_INSU int,      
@YEARS_INSU_WOL int  ,    
@SEAT_BELT_CREDIT char(1)=null,
@APPLY_PERS_UMB_POL int,
@ANY_PRIOR_LOSSES nvarchar(5) = null
    
    
      
      
    
)              
AS 
BEGIN     
              
if NOT EXISTS          
(          
 SELECT 1 FROM APP_AUTO_GEN_INFO          
 WHERE              
  CUSTOMER_ID   = @CUSTOMER_ID AND              
  APP_ID = @APP_ID AND              
  APP_VERSION_ID = @APP_VERSION_ID              
)           
BEGIN              
 --Inserting the record              
  --SELECT @CUSTOMER_ID = IsNull(Max(CUSTOMER_ID),0) + 1   FROM APP_AUTO_GEN_INFO         
        
--Default values-------------------------------------------------------------------        
SET @ANY_NON_OWNED_VEH = 0                    
        
SET   @CAR_MODIFIED     = 0        
        
SET  @EXISTING_DMG   = 0       
        
 SET @ANY_CAR_AT_SCH    = 0            
        
  SET @ANY_OTH_AUTO_INSU   = 0         
        
SET  @ANY_OTH_INSU_COMP   = 0           
        
SET  @H_MEM_IN_MILITARY   = 0           
        
SET  @DRIVER_SUS_REVOKED   = 0            
        
SET  @PHY_MENTL_CHALLENGED      = 0         
        
SET  @ANY_FINANCIAL_RESPONSIBILITY  = 0           
        
SET  @INS_AGENCY_TRANSFER   = 0           
        
SET  @COVERAGE_DECLINED    = 0           
        
            
        
SET  @AGENCY_VEH_INSPECTED   = 0          
SET  @USE_AS_TRANSPORT_FEE   = 0        
        
SET  @SALVAGE_TITLE    = 0          
        
SET  @ANY_ANTIQUE_AUTO  = 0        
        
SET @IS_COMMERCIAL_USE = 0        
SET @IS_USEDFOR_RACING = 0        
--SET @IS_COST_OVER_DEFINED_LIMIT = 0        
SET @IS_MORE_WHEELS = 0        
SET @IS_EXTENDED_FORKS = 0        
SET @IS_LICENSED_FOR_ROAD = 0        
SET @IS_MODIFIED_INCREASE_SPEED = 0        
SET @IS_MODIFIED_KIT = 0        
SET @IS_TAKEN_OUT = 0        
SET @IS_CONVICTED_CARELESS_DRIVE = 0        
SET @IS_CONVICTED_ACCIDENT     = 0      

  -----------added by Pravesh on 20 march FOR IF MULTI POLICY DISCOUNT THEN PUT POLICY NUMBER IN DESC FIELD

--if (LEN(ISNULL(@MULTI_POLICY_DISC_APPLIED_PP_DESC,''))<5 AND ISNULL(@MULTI_POLICY_DISC_APPLIED,'0')='1')
--BEGIN
	DECLARE @AGENCY_ID					SMALLINT,
				@LOB_ID					SMALLINT,
				@MULTI_POLICY_NUMBER	varchar(20),
				@APP_POLICY_NUMBER		varchar(20),
				@MULTI_POLICY_COUNT		SMALLINT

	----Itrack 5267 @AGENCY_ID ..No use of @AGENCY_ID 
	SELECT @AGENCY_ID=APP_AGENCY_ID,@LOB_ID=APP_LOB, @APP_POLICY_NUMBER=APP_NUMBER FROM APP_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID 

	create table ##TMP_MULTIPOLICY
		(APP_POL_NUMBER varchar(20))
	INSERT INTO ##TMP_MULTIPOLICY 
		EXECUTE Proc_GetEligiblePolicies   @CUSTOMER_ID ,  @AGENCY_ID , @LOB_ID , @APP_POLICY_NUMBER 

--IF ANY ELIGIBLE DRIVER EXISTS THEN SET MULTIDISCOUNT AND MULTIDISCOUNT DESCRIPTION
--ITRACK 5267
IF EXISTS(SELECT 1 FROM ##TMP_MULTIPOLICY)
BEGIN

	SELECT @MULTI_POLICY_COUNT = COUNT(*)  FROM ##TMP_MULTIPOLICY
	SELECT TOP 1 @MULTI_POLICY_NUMBER=APP_POL_NUMBER FROM ##TMP_MULTIPOLICY   
	DROP TABLE ##TMP_MULTIPOLICY  
	IF (ISNULL(@MULTI_POLICY_NUMBER,'')!='N.A.' AND ISNULL(@MULTI_POLICY_NUMBER,'')!='' AND @MULTI_POLICY_COUNT>0)  
	BEGIN  
		SET @MULTI_POLICY_DISC_APPLIED = '1'
		SET @MULTI_POLICY_DISC_APPLIED_PP_DESC=@MULTI_POLICY_NUMBER  
	END  
END

--END
--------END HERE        
-----------------------------------------------------------------------------------        
        
             
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
  DRIVER_SUS_REVOKED_PP_DESC              ,              
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
	IS_CONVICTED_ACCIDENT ,      
	YEARS_INSU,      
	YEARS_INSU_WOL ,    
	SEAT_BELT_CREDIT  ,
	APPLY_PERS_UMB_POL,
	ANY_PRIOR_LOSSES        
               
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
  'Y'    ,              
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
@COST_EQUIPMENT_DESC ,        
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
@IS_CONVICTED_ACCIDENT ,      
@YEARS_INSU,      
@YEARS_INSU_WOL    ,    
@SEAT_BELT_CREDIT,
@APPLY_PERS_UMB_POL,
@ANY_PRIOR_LOSSES          
 )              
END              
ELSE              
BEGIN              
 --Updating the record              
 UPDATE APP_AUTO_GEN_INFO              
 SET               
--APP_VERSION_ID     =  @APP_VERSION_ID ,              
 ANY_NON_OWNED_VEH         = @ANY_NON_OWNED_VEH ,              
 ANY_NON_OWNED_VEH_PP_DESC  = @ANY_NON_OWNED_VEH_PP_DESC ,              
 CAR_MODIFIED         = @CAR_MODIFIED  ,              
 CAR_MODIFIED_DESC          = @CAR_MODIFIED_DESC     ,              
 EXISTING_DMG         = @EXISTING_DMG  ,              
 EXISTING_DMG_PP_DESC       = @EXISTING_DMG_PP_DESC  ,              
 ANY_CAR_AT_SCH         = @ANY_CAR_AT_SCH ,              
 ANY_CAR_AT_SCH_DESC        = @ANY_CAR_AT_SCH_DESC , 
 ANY_OTH_AUTO_INSU        = @ANY_OTH_AUTO_INSU ,     
 ANY_OTH_AUTO_INSU_DESC    =  @ANY_OTH_AUTO_INSU_DESC,              
 ANY_OTH_INSU_COMP        =  @ANY_OTH_INSU_COMP ,              
 ANY_OTH_INSU_COMP_PP_DESC  = @ANY_OTH_INSU_COMP_PP_DESC     ,              
 H_MEM_IN_MILITARY         = @H_MEM_IN_MILITARY ,              
 H_MEM_IN_MILITARY_DESC     = @H_MEM_IN_MILITARY_DESC,              
 DRIVER_SUS_REVOKED         = @DRIVER_SUS_REVOKED ,              
 DRIVER_SUS_REVOKED_PP_DESC = @DRIVER_SUS_REVOKED_PP_DESC,              
 PHY_MENTL_CHALLENGED       = @PHY_MENTL_CHALLENGED ,              
 PHY_MENTL_CHALLENGED_PP_DESC = @PHY_MENTL_CHALLENGED_PP_DESC,              
 ANY_FINANCIAL_RESPONSIBILITY = @ANY_FINANCIAL_RESPONSIBILITY    ,              
 ANY_FINANCIAL_RESPONSIBILITY_PP_DESC = @ANY_FINANCIAL_RESPONSIBILITY_PP_DESC,              
 INS_AGENCY_TRANSFER         = @INS_AGENCY_TRANSFER ,              
 INS_AGENCY_TRANSFER_PP_DESC = @INS_AGENCY_TRANSFER_PP_DESC,              
 COVERAGE_DECLINED          = @COVERAGE_DECLINED ,              
 COVERAGE_DECLINED_PP_DESC   = @COVERAGE_DECLINED_PP_DESC,              
 AGENCY_VEH_INSPECTED        = @AGENCY_VEH_INSPECTED    ,              
 AGENCY_VEH_INSPECTED_PP_DESC = @AGENCY_VEH_INSPECTED_PP_DESC,              
 USE_AS_TRANSPORT_FEE  = @USE_AS_TRANSPORT_FEE   ,              
 USE_AS_TRANSPORT_FEE_DESC  = @USE_AS_TRANSPORT_FEE_DESC,              
 SALVAGE_TITLE        = @SALVAGE_TITLE   ,              
 SALVAGE_TITLE_PP_DESC       =@SALVAGE_TITLE_PP_DESC,              
 ANY_ANTIQUE_AUTO   = @ANY_ANTIQUE_AUTO  ,              
 ANY_ANTIQUE_AUTO_DESC      =@ANY_ANTIQUE_AUTO_DESC,              
 REMARKS     = @REMARKS ,              
 IS_ACTIVE    = @IS_ACTIVE  ,              
 MODIFIED_BY        = @MODIFIED_BY ,              
 LAST_UPDATED_DATETIME    = @LAST_UPDATED_DATETIME,              
 MULTI_POLICY_DISC_APPLIED = @MULTI_POLICY_DISC_APPLIED ,              
 MULTI_POLICY_DISC_APPLIED_PP_DESC =@MULTI_POLICY_DISC_APPLIED_PP_DESC,            
FullName=@FullName,            
DATE_OF_BIRTH=@DATE_OF_BIRTH,            
DrivingLisence=@DrivingLisence,            
InsuredElseWhere=@InsuredElseWhere,            
CompanyName=@CompanyName,            
PolicyNumber=@PolicyNumber,            
IS_OTHER_THAN_INSURED=@IS_OTHER_THAN_INSURED,            
CURR_RES_TYPE=@CURR_RES_TYPE,            
WhichCycle     =@WhichCycle,            
COST_EQUIPMENT_DESC=@COST_EQUIPMENT_DESC      ,      
YEARS_INSU=@YEARS_INSU,      
YEARS_INSU_WOL=@YEARS_INSU_WOL ,    
SEAT_BELT_CREDIT=@SEAT_BELT_CREDIT ,
APPLY_PERS_UMB_POL = @APPLY_PERS_UMB_POL,
ANY_PRIOR_LOSSES = @ANY_PRIOR_LOSSES   
              
 WHERE              
  CUSTOMER_ID   = @CUSTOMER_ID AND              
  APP_ID = @APP_ID AND              
  APP_VERSION_ID = @APP_VERSION_ID              
              
END              
              
END            
            
            
            
            
            
            
   
        
      
    
  












GO

