IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPPARule_GenInfo_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPPARule_GenInfo_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                            
Proc Name                : Dbo.Proc_GetPPARule_GenInfo_Pol                                          
Created by               : Ashwani                                            
Date                     : 01 Mar 2006      
Purpose                  : To get the Auto Gen Information for PPA policy mandatory info                                       
Revison History          :                                            
Used In                  : Wolverine                                            
------------------------------------------------------------                                            
Date     Review By          Comments                                            
------   ------------       -------------------------    
drop proc dbo.Proc_GetPPARule_GenInfo_Pol     
*/                                            
CREATE proc dbo.Proc_GetPPARule_GenInfo_Pol                                            
(                                            
@CUSTOMER_ID    int,                                            
@POLICY_ID    int,                                            
@POLICY_VERSION_ID   int                              
)                                            
AS                                                
BEGIN             
	--1            
	DECLARE @COVERAGE_DECLINED CHAR                 
	DECLARE @COVERAGE_DECLINED_PP_DESC VARCHAR(50)            
	--2             
	DECLARE @H_MEM_IN_MILITARY CHAR                                  
	DECLARE @H_MEM_IN_MILITARY_DESC VARCHAR(50)            
	--3            
	DECLARE @ANY_FINANCIAL_RESPONSIBILITY CHAR      -- IF 'Y'  SHOULD BE REFERRED TO UNDERWRITER              
	DECLARE @ANY_FINANCIAL_RESPONSIBILITY_PP_DESC VARCHAR(50)            
	--4            
	DECLARE @CAR_MODIFIED CHAR                                  
	DECLARE @CAR_MODIFIED_DESC VARCHAR(50)            
	--5            
	DECLARE @SALVAGE_TITLE CHAR                  
	DECLARE @SALVAGE_TITLE_PP_DESC VARCHAR(50)            
	--6             
	DECLARE @ANY_NON_OWNED_VEH CHAR                  
	DECLARE @ANY_NON_OWNED_VEH_PP_DESC VARCHAR(50)            
	--7            
	DECLARE @EXISTING_DMG CHAR                                  
	DECLARE @EXISTING_DMG_PP_DESC VARCHAR(50)            
	--8             
	DECLARE @ANY_CAR_AT_SCH CHAR                                  
	DECLARE @ANY_CAR_AT_SCH_DESC VARCHAR(50)            
	--9            
	DECLARE @ANY_OTH_AUTO_INSU NCHAR(1)              
	DECLARE @ANY_OTH_AUTO_INSU_DESC VARCHAR(50)            
	--10             
	DECLARE @DRIVER_SUS_REVOKED CHAR               
	DECLARE @DRIVER_SUS_REVOKED_PP_DESC VARCHAR(50)            
	--11            
	DECLARE @PHY_MENTL_CHALLENGED CHAR                                  
	DECLARE @PHY_MENTL_CHALLENGED_PP_DESC VARCHAR(50)            
	--MANADATORY FIELDS             
	--12            
	DECLARE @ANY_OTH_INSU_COMP CHAR                                  
	DECLARE @ANY_OTH_INSU_COMP_PP_DESC VARCHAR(50)            
	--13            
	DECLARE @INS_AGENCY_TRANSFER NCHAR(1)              
	DECLARE @INS_AGENCY_TRANSFER_PP_DESC VARCHAR(50)            
	--14            
	DECLARE @AGENCY_VEH_INSPECTED NCHAR(1)              
	DECLARE @AGENCY_VEH_INSPECTED_PP_DESC VARCHAR(50)            
	--15            
	DECLARE @USE_AS_TRANSPORT_FEE CHAR              
	--DECLARE @USE_AS_TRANSPORT_FEE_DESC VARCHAR(50)            
	--16            
	DECLARE @ANY_ANTIQUE_AUTO NCHAR(1)              
	DECLARE @ANY_ANTIQUE_AUTO_DESC VARCHAR(50)            
	--17            
	DECLARE @MULTI_POLICY_DISC_APPLIED NCHAR(1)             
	DECLARE @MULTI_POLICY_DISC_APPLIED_PP_DESC VARCHAR(50)            
	--18            
	DECLARE @IS_OTHER_THAN_INSURED NCHAR(1) -- IF 'Y' THEN             
	DECLARE @FULLNAME VARCHAR(50)            
	DECLARE @DATE_OF_BIRTH VARCHAR(20) 
	--19            
	DECLARE @INSUREDELSEWHERE NCHAR(1) -- IF 'Y' THEN             
	DECLARE @COMPANYNAME VARCHAR(50)            
	DECLARE @POLICYNUMBER VARCHAR(50)         
	--         
	DECLARE @IS_RECORD_EXISTS CHAR         
	DECLARE @ANY_PRIOR_LOSSES CHAR      
	DECLARE @ANY_PRIOR_LOSSES_DESC VARCHAR(50)      
	    
	DECLARE @COST_EQUIPMENT_DESC VARCHAR(50)    
       
            
if exists(select CUSTOMER_ID from POL_AUTO_GEN_INFO where                          
 CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID)                          
BEGIN          
SET @IS_RECORD_EXISTS='N'        
SELECT            
	@COVERAGE_DECLINED=isnull(COVERAGE_DECLINED,''),@COVERAGE_DECLINED_PP_DESC=isnull(COVERAGE_DECLINED_PP_DESC,''),              
	@H_MEM_IN_MILITARY=isnull(H_MEM_IN_MILITARY,''),@H_MEM_IN_MILITARY_DESC=isnull(H_MEM_IN_MILITARY_DESC,''),            
	@ANY_FINANCIAL_RESPONSIBILITY=isnull(ANY_FINANCIAL_RESPONSIBILITY,''),@ANY_FINANCIAL_RESPONSIBILITY_PP_DESC=isnull(ANY_FINANCIAL_RESPONSIBILITY_PP_DESC,''),            
	@CAR_MODIFIED=isnull(CAR_MODIFIED,''),@CAR_MODIFIED_DESC=isnull(CAR_MODIFIED_DESC,''),    @SALVAGE_TITLE=isnull(SALVAGE_TITLE,''),@SALVAGE_TITLE_PP_DESC=isnull(SALVAGE_TITLE_PP_DESC,''),            
	@ANY_NON_OWNED_VEH=isnull(ANY_NON_OWNED_VEH,''),@ANY_NON_OWNED_VEH_PP_DESC=isnull(ANY_NON_OWNED_VEH_PP_DESC,''),            
	@EXISTING_DMG=isnull(EXISTING_DMG,''),@EXISTING_DMG_PP_DESC=isnull(EXISTING_DMG_PP_DESC,''),            
	@ANY_CAR_AT_SCH=isnull(ANY_CAR_AT_SCH,''),@ANY_CAR_AT_SCH_DESC=isnull(ANY_CAR_AT_SCH_DESC,''),            
	@ANY_OTH_AUTO_INSU=isnull(ANY_OTH_AUTO_INSU,''),@ANY_OTH_AUTO_INSU_DESC=isnull(ANY_OTH_AUTO_INSU_DESC,''),            
	@DRIVER_SUS_REVOKED=isnull(DRIVER_SUS_REVOKED,''),@DRIVER_SUS_REVOKED_PP_DESC=isnull(DRIVER_SUS_REVOKED_PP_DESC,''),            
	@PHY_MENTL_CHALLENGED=isnull(PHY_MENTL_CHALLENGED,''),@PHY_MENTL_CHALLENGED_PP_DESC=isnull(PHY_MENTL_CHALLENGED_PP_DESC,''),            
	--Mandatory fields             
	@ANY_OTH_INSU_COMP=isnull(ANY_OTH_INSU_COMP,''),@ANY_OTH_INSU_COMP_PP_DESC=isnull(ANY_OTH_INSU_COMP_PP_DESC,''),            
	@INS_AGENCY_TRANSFER=isnull(INS_AGENCY_TRANSFER,''),@INS_AGENCY_TRANSFER_PP_DESC=isnull(INS_AGENCY_TRANSFER_PP_DESC,''),            
	@AGENCY_VEH_INSPECTED=isnull(AGENCY_VEH_INSPECTED,''),@AGENCY_VEH_INSPECTED_PP_DESC=isnull(AGENCY_VEH_INSPECTED_PP_DESC,''),            
	@USE_AS_TRANSPORT_FEE=isnull(USE_AS_TRANSPORT_FEE,''),--@USE_AS_TRANSPORT_FEE_DESC=isnull(USE_AS_TRANSPORT_FEE_DESC,''),            
	@ANY_ANTIQUE_AUTO=isnull(ANY_ANTIQUE_AUTO,''),@ANY_ANTIQUE_AUTO_DESC=isnull(ANY_ANTIQUE_AUTO_DESC,''),            
	@MULTI_POLICY_DISC_APPLIED=isnull(MULTI_POLICY_DISC_APPLIED,''),@MULTI_POLICY_DISC_APPLIED_PP_DESC=isnull(MULTI_POLICY_DISC_APPLIED_PP_DESC,''),            
	@IS_OTHER_THAN_INSURED=isnull(IS_OTHER_THAN_INSURED,''),@FULLNAME=isnull(FULLNAME,''),            
	@DATE_OF_BIRTH=isnull(convert(varchar(20),DATE_OF_BIRTH),''),            
	@INSUREDELSEWHERE=isnull(INSUREDELSEWHERE,''),@COMPANYNAME=isnull(COMPANYNAME,''),@POLICYNUMBER=isnull(POLICYNUMBER,''),      
	@ANY_PRIOR_LOSSES=isnull(ANY_PRIOR_LOSSES,'') ,@ANY_PRIOR_LOSSES_DESC = isnull(ANY_PRIOR_LOSSES_DESC,''),    
	@COST_EQUIPMENT_DESC =isnull(convert(varchar(50),COST_EQUIPMENT_DESC),'')           
FROM POL_AUTO_GEN_INFO            
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                  
END                           
ELSE                          
BEGIN                           
SET @IS_RECORD_EXISTS='Y'        
END                 
   
/*Underwriting question:      
 "Any prior losses?", if not saved, means it is blank or null in database, prompt for answering to this question.      
  If Yes to "Any prior losses?", then look at prior losses for Auto LOB. If there is none then refer to underwriter      
  If No, and there are prior losses refer UWR.*/      
      
DECLARE @PRIOR_LOSS_Y CHAR      
DECLARE @PRIOR_LOSS_N CHAR      
      
SET @PRIOR_LOSS_Y='N'      
SET @PRIOR_LOSS_Y='N'      
      
IF(@ANY_PRIOR_LOSSES='1')      
BEGIN       
 -- NO PRIOR LOSS       
	IF NOT EXISTS(SELECT CUSTOMER_ID FROM APP_PRIOR_LOSS_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND LOB =2)      
	SET @PRIOR_LOSS_Y ='Y'      
END      
ELSE IF(@ANY_PRIOR_LOSSES='0')      
BEGIN       
	IF EXISTS(SELECT CUSTOMER_ID FROM APP_PRIOR_LOSS_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND LOB =2)      
	SET @PRIOR_LOSS_N='Y'      
END        
--Any other licensed drivers in the household that are not listed or rated on this policy
--If Yes - refer to underwriters      
IF(@IS_OTHER_THAN_INSURED='1')                                
BEGIN                                 
 SET @IS_OTHER_THAN_INSURED='Y'                                
END                      
ELSE IF(@IS_OTHER_THAN_INSURED='0')                                
BEGIN             
 SET @IS_OTHER_THAN_INSURED='N'                                
END                       
---      
                                
if(@COVERAGE_DECLINED='1')                                  
begin                                   
set @COVERAGE_DECLINED='Y'                                  
end                        
else if(@COVERAGE_DECLINED='0')                                  
begin                                   
set @COVERAGE_DECLINED='N'                                  
end                         
-- 2           
if(@H_MEM_IN_MILITARY='1')                                  
begin                           
set @H_MEM_IN_MILITARY='Y'                                  
end                        
else if(@H_MEM_IN_MILITARY='0')                                  
begin                                   
set @H_MEM_IN_MILITARY='N'                                  
end                      
-- 3            
if(@ANY_FINANCIAL_RESPONSIBILITY='1')                                  
begin                                   
set @ANY_FINANCIAL_RESPONSIBILITY='Y'                                  
end                        
else if(@ANY_FINANCIAL_RESPONSIBILITY='0')                                  
begin                                   
set @ANY_FINANCIAL_RESPONSIBILITY='N'                                  
end                      
-- 4       
-- 4          
 /*      
 Underwriting Question      
 Has any car been modified, assembled or kit vehicle or have special equipment? (include customized vans and pickups indicate cost) is  Yes       
       
 Then look at the Vehicle Info Tab       
 If Vehicle Type is not customized truck or van       
 Then Refer to Underwriters */      
      
declare @intCount int      
select @intCount=count(isnull(APP_VEHICLE_PERTYPE_ID,'0')) from POL_VEHICLES                                          
  where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID  and APP_VEHICLE_PERTYPE_ID='11335'        
      
if(@CAR_MODIFIED='1')                                
begin                                 
 if(@intCount >0)                      
   set @CAR_MODIFIED='N'                                  
  else      
   set @CAR_MODIFIED='Y'                        
end                      
else if(@CAR_MODIFIED='0')                                
begin                                 
set @CAR_MODIFIED='N'                                
end                
-- 5            
if(@SALVAGE_TITLE='1')                                  
begin                                   
set @SALVAGE_TITLE='Y'                                  
end                        
else if(@SALVAGE_TITLE='0')                                  
begin                                   
set @SALVAGE_TITLE='N'                                  
end             
-- 6                          
if(@ANY_NON_OWNED_VEH='1')                                  
begin                                   
set @ANY_NON_OWNED_VEH='Y'                                  
end                        
else if(@ANY_NON_OWNED_VEH='0')                           
begin                                   
set @ANY_NON_OWNED_VEH='N'                                  
end                            
-- 7            
if(@EXISTING_DMG='1')                                  
begin                                   
set @EXISTING_DMG='Y'                                  
end                        
else if(@EXISTING_DMG='0')                                  
begin                                   
set @EXISTING_DMG='N'                                  
end            
-- 8            
if(@ANY_CAR_AT_SCH='1')                                  
begin            
set @ANY_CAR_AT_SCH='Y'                                  
end                        
else if(@ANY_CAR_AT_SCH='0')                                  
begin                                
set @ANY_CAR_AT_SCH='N'                                  
end                      
--  9               
if(@ANY_OTH_INSU_COMP='1')                                  
begin                                   
set @ANY_OTH_INSU_COMP='Y'                                  
end                        
else if(@ANY_OTH_INSU_COMP='0')                                  
begin                                   
set @ANY_OTH_INSU_COMP='N'                                  
end              
--10            
if(@DRIVER_SUS_REVOKED='1')                                  
begin                                   
set @DRIVER_SUS_REVOKED='Y'                                  
end                        
else if(@DRIVER_SUS_REVOKED='0')                                  
begin                                   
set @DRIVER_SUS_REVOKED='N'                                  
end                   
-- 11                        
if(@PHY_MENTL_CHALLENGED='1')                           
begin                                   
set @PHY_MENTL_CHALLENGED='Y'                   
end                        
else if(@PHY_MENTL_CHALLENGED='0')                                  
begin                                   
set @PHY_MENTL_CHALLENGED='N'                                  
end                             
      
--   Is any vehicle used for Livery, rental, passenger hire, or to transport persons to work for a fee?       
--   If Yes the Refer to underwriters                       
      
if(@USE_AS_TRANSPORT_FEE='1')                         
begin                                 
set @USE_AS_TRANSPORT_FEE='Y'                 
end                      
else if(@USE_AS_TRANSPORT_FEE='0')                                
begin                                 
set @USE_AS_TRANSPORT_FEE='N'                                
end       
---------------------------------------------------------------------------------------      
--If any other auto insurance in the household If Yes - Refer to underwriters       
      
if(@ANY_OTH_AUTO_INSU='1')                         
begin                                 
set @ANY_OTH_AUTO_INSU='Y'                 
end                      
else if(@ANY_OTH_AUTO_INSU='0')                                
begin                                 
set @ANY_OTH_AUTO_INSU='N'                                
end       
------------------  ------------      
--Any vehicle considered an antique car? Mandatory question If yes On new business - Risk is Declined       
-- Grandfathered, for existing policies only where the inception date is prior to 01/01/2003      
 declare @APP_INCEPTION_DATE datetime      
 declare @APP_INCEPTION_DATE_FIX datetime      
 set @APP_INCEPTION_DATE_FIX = '2003-01-01'    
       
 select  @APP_INCEPTION_DATE= APP_INCEPTION_DATE from POL_CUSTOMER_POLICY_LIST   with(nolock) --by pravesh    
 where  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID      
      
 -- select @APP_INCEPTION_DATE      
      
 if(@APP_INCEPTION_DATE < @APP_INCEPTION_DATE_FIX and @ANY_ANTIQUE_AUTO='1')      
  set @ANY_ANTIQUE_AUTO ='Y'      
 else      
  set @ANY_ANTIQUE_AUTO = 'N'      
      
                            
          
if(@POLICYNUMBER is null)          
begin           
 set @POLICYNUMBER=''          
end           
------------------------------------------------------------------------------------      
-- If there is a loss for Automobile LOB with Driver Field Empty At Fault is Blank       
-- Refer to underwriters       
      
DECLARE @DRIVER_ID VARCHAR(15)       
DECLARE @AT_FAULT VARCHAR(15)      
DECLARE @AUTO_DRIVER_FAULT CHAR      

SELECT @DRIVER_ID=ISNULL(CONVERT(VARCHAR(15),DRIVER_ID),''),@AT_FAULT = ISNULL(CONVERT(VARCHAR(15),AT_FAULT),'')       
FROM APP_PRIOR_LOSS_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND  LOB=2      
      
IF(@DRIVER_ID='' OR @AT_FAULT='')      
BEGIN       
SET @AUTO_DRIVER_FAULT='Y'      
END       
ELSE       
BEGIN       
SET @AUTO_DRIVER_FAULT='N'      
END 
--=============================== Itrack No. 3593 ===========================

   DECLARE @MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS VARCHAR
   SET @MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='N'
	
   IF(@MULTI_POLICY_DISC_APPLIED='1')
   BEGIN
		DECLARE @POLICY_LOB INT,
			@POLICY_NUMBER VARCHAR(100),
			@COUNT INT,
		        --@MULTI_POLICY_DISC_APPLIED_PP_DESC VARCHAR(100),
			@BASE_POLICY_VERSION_ID INT,  
	  		@NEW_POLICY_VERSION_ID INT ,
			@PROCESS_ID  INT,
			@COUNT_POL_STATUS INT,
			@POLICY_STATUS varchar(20),
			@COUNT_POLICY_NUMBER INT 

		SELECT @POLICY_LOB=POLICY_LOB,
		       @POLICY_NUMBER=POLICY_NUMBER,
		       @MULTI_POLICY_DISC_APPLIED_PP_DESC=SUBSTRING(MULTI_POLICY_DISC_APPLIED_PP_DESC,0,9),
		       @POLICY_STATUS=POLICY_STATUS  FROM POL_CUSTOMER_POLICY_LIST POL
		INNER JOIN POL_AUTO_GEN_INFO  POL_WATER ON 	
		POL.CUSTOMER_ID=POL_WATER.CUSTOMER_ID AND POL.POLICY_ID=POL_WATER.POLICY_ID AND POL.POLICY_VERSION_ID=POL_WATER.POLICY_VERSION_ID
		WHERE POL.CUSTOMER_ID=@CUSTOMER_ID AND POL.POLICY_ID=@POLICY_ID AND POL.POLICY_VERSION_ID=@POLICY_VERSION_ID 

		SELECT @BASE_POLICY_VERSION_ID = MAX(POLICY_VERSION_ID),  
	       	       @NEW_POLICY_VERSION_ID  = MAX(NEW_POLICY_VERSION_ID)
		       FROM POL_POLICY_PROCESS WITH(NOLOCK)  
	        WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID 
		
		SELECT  @PROCESS_ID=PROCESS_ID
		FROM POL_POLICY_PROCESS WITH(NOLOCK)  
		WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@BASE_POLICY_VERSION_ID
		
		SELECT @COUNT_POL_STATUS=COUNT(POLICY_STATUS)  FROM POL_CUSTOMER_POLICY_LIST 
		WHERE POLICY_NUMBER = @MULTI_POLICY_DISC_APPLIED_PP_DESC AND POLICY_STATUS = 'INACTIVE'
		SELECT @COUNT_POLICY_NUMBER=COUNT(POLICY_NUMBER)  FROM POL_CUSTOMER_POLICY_LIST 
		WHERE POLICY_NUMBER = @MULTI_POLICY_DISC_APPLIED_PP_DESC 

		-- To Select All Eligible Policy 
		--Homeowners - 1 Automobile - 2 Motorcycle - 3	Watercraft - 4	Umbrella   - 5	Rental     - 6	General Liability - 7
                
		  IF (@POLICY_LOB IN (2,3,4))                 
		  BEGIN                
			SELECT   @COUNT=COUNT(POLICY_NUMBER) FROM POL_CUSTOMER_POLICY_LIST, MNT_AGENCY_LIST                
			WHERE CUSTOMER_ID = @CUSTOMER_ID     
			AND POL_CUSTOMER_POLICY_LIST.AGENCY_ID = MNT_AGENCY_LIST.AGENCY_ID             
			AND POLICY_NUMBER <>  @POLICY_NUMBER            
			AND POL_CUSTOMER_POLICY_LIST.IS_ACTIVE = 'Y' AND APP_EXPIRATION_DATE > GETDATE() AND POLICY_STATUS = 'NORMAL'          
			AND POLICY_LOB = 1 -- ORDER BY POLICY_NUMBER              
			AND POLICY_NUMBER!=@MULTI_POLICY_DISC_APPLIED_PP_DESC
		  END      
		 
		-- Umbrella and Rental Dwelling
		  ELSE IF (@POLICY_LOB=5 OR @POLICY_LOB=7)                
		  BEGIN
			
			SET @COUNT = -1
			
		  END 

	-->>1: ==>>>NEW BUSINESS AND REWRITE 
		--If there is a yes in the Field Is multi-policy discount applied?* 
		--and there are no Eligible policies - make sure that 
		--there are details in the field Multi-policy discount description 
		--if there are detail allow the discount 
		
		IF(@COUNT >0 AND ( @PROCESS_ID IN(24,25,31,32)) )
		BEGIN 
			SET @MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y'
		END
	-->>2: ==>>>RENEWAL 
		--If there are no Eligible Policies and there is Yes in the Field multi-policy discount applied?*
		--program to see if the policy number in the Field Multi-policy discount description is active 
		--If policy is not active or does not exist on the database
		--This will goes as a Refer to Underwriters
		--	Note for the referral - Multi Policy Discount Eligibility
		--	If referral is accepted - allow discount		
		
		ELSE IF(@COUNT=0  AND (@COUNT_POL_STATUS>0 OR @COUNT_POLICY_NUMBER=0)  AND (@PROCESS_ID IN(5,18)))
		BEGIN 
			SET @MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y'
		END
    END

--================ End =======================================     
----------------------------------------------------------------------------------      
      
          
          
SELECT              
	--1            
	@COVERAGE_DECLINED AS COVERAGE_DECLINED,            
	CASE @COVERAGE_DECLINED            
	WHEN 'Y'THEN  @COVERAGE_DECLINED_PP_DESC             
	END AS COVERAGE_DECLINED_PP_DESC,            
	--2             
	@H_MEM_IN_MILITARY AS H_MEM_IN_MILITARY,            
	CASE @H_MEM_IN_MILITARY            
	WHEN 'Y'THEN  @H_MEM_IN_MILITARY_DESC             
	END AS H_MEM_IN_MILITARY_DESC,            
	--3            
	@ANY_FINANCIAL_RESPONSIBILITY  AS ANY_FINANCIAL_RESPONSIBILITY,            
	CASE @ANY_FINANCIAL_RESPONSIBILITY            
	WHEN 'Y'THEN  @ANY_FINANCIAL_RESPONSIBILITY_PP_DESC             
	END AS ANY_FINANCIAL_RESPONSIBILITY_PP_DESC,            
	--4            
	@CAR_MODIFIED AS CAR_MODIFIED  ,            
	CASE @CAR_MODIFIED            
	WHEN 'Y'THEN  @CAR_MODIFIED_DESC             
	END AS CAR_MODIFIED_DESC,        
	
	--    
	
	@CAR_MODIFIED AS CAR_MODIFIED  ,          
	CASE @CAR_MODIFIED          
	WHEN 'Y'THEN  @COST_EQUIPMENT_DESC           
	END AS COST_EQUIPMENT_DESC,        
	--5            
	@SALVAGE_TITLE AS SALVAGE_TITLE ,            
	CASE @SALVAGE_TITLE            
	WHEN 'Y'THEN  @SALVAGE_TITLE_PP_DESC             
	END AS SALVAGE_TITLE_PP_DESC,            
	--6             
	@ANY_NON_OWNED_VEH AS ANY_NON_OWNED_VEH,            
	CASE @ANY_NON_OWNED_VEH            
	WHEN 'Y'THEN  @ANY_NON_OWNED_VEH_PP_DESC             
	END AS ANY_NON_OWNED_VEH_PP_DESC,            
	--7            
	@EXISTING_DMG AS EXISTING_DMG,            
	CASE @EXISTING_DMG            
	WHEN 'Y'THEN  @EXISTING_DMG_PP_DESC             
	END AS EXISTING_DMG_PP_DESC,            
	--8             
	@ANY_CAR_AT_SCH AS ANY_CAR_AT_SCH,            
	CASE @ANY_CAR_AT_SCH            
	WHEN 'Y'THEN  @ANY_CAR_AT_SCH_DESC             
	END AS ANY_CAR_AT_SCH_DESC,            
	--9            
	--@ANY_OTH_INSU_COMP AS ANY_OTH_INSU_COMP,             
	--CASE @ANY_OTH_INSU_COMP            
	--WHEN 'Y'THEN  @ANY_OTH_INSU_COMP_PP_DESC             
	--END AS ANY_OTH_INSU_COMP_PP_DESC,            
	--10             
	@DRIVER_SUS_REVOKED AS DRIVER_SUS_REVOKED ,            
	CASE @DRIVER_SUS_REVOKED            
	WHEN 'Y'THEN  @DRIVER_SUS_REVOKED_PP_DESC             
	END AS DRIVER_SUS_REVOKED_PP_DESC,            
	--11            
	@PHY_MENTL_CHALLENGED AS PHY_MENTL_CHALLENGED,            
	CASE @PHY_MENTL_CHALLENGED            
	WHEN 'Y'THEN  @PHY_MENTL_CHALLENGED_PP_DESC             
	END AS PHY_MENTL_CHALLENGED_PP_DESC,            
	--MANADATORY FIELDS             
	--12            
	@ANY_OTH_AUTO_INSU AS ANY_OTH_AUTO_INSU ,            
	CASE @ANY_OTH_AUTO_INSU            
	WHEN 'Y'THEN  @ANY_OTH_AUTO_INSU_DESC             
	END AS ANY_OTH_AUTO_INSU_DESC,            
	--13            
	@INS_AGENCY_TRANSFER AS INS_AGENCY_TRANSFER,            
	CASE @INS_AGENCY_TRANSFER            
	WHEN '1'THEN  @INS_AGENCY_TRANSFER_PP_DESC             
	END AS INS_AGENCY_TRANSFER_PP_DESC,            
	--14            
	@AGENCY_VEH_INSPECTED AS AGENCY_VEH_INSPECTED ,            
	CASE @AGENCY_VEH_INSPECTED            
	WHEN '1'THEN  @AGENCY_VEH_INSPECTED_PP_DESC             
	END AS AGENCY_VEH_INSPECTED_PP_DESC,            
	--15            
	@USE_AS_TRANSPORT_FEE AS USE_AS_TRANSPORT_FEE  ,            
	--16            
	@ANY_ANTIQUE_AUTO AS ANY_ANTIQUE_AUTO,            
	CASE @ANY_ANTIQUE_AUTO            
	WHEN '1'THEN  @ANY_ANTIQUE_AUTO_DESC             
	END AS ANY_ANTIQUE_AUTO_DESC,            
	--17            
	@MULTI_POLICY_DISC_APPLIED AS MULTI_POLICY_DISC_APPLIED,            
	CASE @MULTI_POLICY_DISC_APPLIED            
	WHEN '1'THEN  @MULTI_POLICY_DISC_APPLIED_PP_DESC             
	END AS MULTI_POLICY_DISC_APPLIED_PP_DESC,            
	--18            
	CASE @IS_OTHER_THAN_INSURED WHEN 'Y'            
	THEN  @IS_OTHER_THAN_INSURED END AS IS_OTHER_THAN_INSURED,            
	       
	    
	--@IS_OTHER_THAN_INSURED AS IS_OTHER_THAN_INSURED,  -- IF 'Y' THEN             
	    
	CASE @IS_OTHER_THAN_INSURED            
	WHEN 'Y'THEN  @FULLNAME             
	END AS FULLNAME,            
	--- 18.1            
	--@IS_OTHER_THAN_INSURED AS IS_OTHER_THAN_INSURED,  -- IF 'Y' THEN             
	CASE @IS_OTHER_THAN_INSURED            
	WHEN 'Y'THEN  @DATE_OF_BIRTH             
	END AS DATE_OF_BIRTH,        
	    
	    
	CASE @IS_OTHER_THAN_INSURED            
	WHEN 'Y' THEN             
	CASE @INSUREDELSEWHERE            
	WHEN '1' THEN @POLICYNUMBER             
	END            
	END AS POLICYNUMBER,              
	CASE @IS_OTHER_THAN_INSURED            
	WHEN 'Y' THEN             
	CASE @INSUREDELSEWHERE            
	WHEN '1' THEN @COMPANYNAME             
	END            
	END AS COMPANYNAME,        
	@IS_RECORD_EXISTS AS IS_RECORD_EXISTS  ,      
	@AUTO_DRIVER_FAULT AS AUTO_DRIVER_FAULT,      
	@ANY_PRIOR_LOSSES AS ANY_PRIOR_LOSSES,      
	
	CASE @ANY_PRIOR_LOSSES       
	WHEN 'Y' THEN @ANY_PRIOR_LOSSES_DESC      
	END AS ANY_PRIOR_LOSSES_DESC,   
	@PRIOR_LOSS_Y AS PRIOR_LOSS_Y,      
	@PRIOR_LOSS_N AS PRIOR_LOSS_N ,
 	@MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS AS MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS        
END 






GO

