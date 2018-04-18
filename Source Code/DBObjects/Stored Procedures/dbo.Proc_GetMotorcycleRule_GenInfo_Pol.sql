IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMotorcycleRule_GenInfo_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMotorcycleRule_GenInfo_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC dbo.Proc_GetMotorcycleRule_GenInfo_Pol                                                                        
(                                                                        
@CUSTOMER_ID    int,                                                                        
@POLICY_ID    int,                                                                        
@POLICY_VERSION_ID   int          
)                                                                        
as                                                                            
begin                                         
--POL_AUTO_GEN_INFO                                              
-- Mandatory                                    
 DECLARE @DRIVER_SUS_REVOKED CHAR                                    
 DECLARE @DRIVER_SUS_REVOKED_PP_DESC NVARCHAR(75)                                    
 DECLARE @PHY_MENTL_CHALLENGED CHAR                                    
 DECLARE @PHY_MENTL_CHALLENGED_PP_DESC NVARCHAR(75)                                    
 DECLARE @ANY_FINANCIAL_RESPONSIBILITY  CHAR                                     
 DECLARE @ANY_FINANCIAL_RESPONSIBILITY_PP_DESC NVARCHAR(75)                                    
 DECLARE @MULTI_POLICY_DISC_APPLIED   NCHAR(1)                                    
 DECLARE @MULTI_POLICY_DISC_APPLIED_PP_DESC VARCHAR(25)                                    
 DECLARE @IS_OTHER_THAN_INSURED NCHAR(1) -- IF 'Y' THEN                                         
 DECLARE @FULLNAME VARCHAR(50)                                        
 DECLARE @DATE_OF_BIRTH VARCHAR(20)                                        
 --RULES                                     
 DECLARE @IS_EXTENDED_FORKS  CHAR                                     
 DECLARE @IS_COMMERCIAL_USE  CHAR                                              
 DECLARE @ANY_NON_OWNED_VEH  CHAR                                              
 DECLARE @IS_USEDFOR_RACING  CHAR                                              
 DECLARE @IS_TAKEN_OUT  CHAR                                               
 DECLARE @IS_MORE_WHEELS  CHAR                                              
 DECLARE @IS_CONVICTED_CARELESS_DRIVE  CHAR                                              
 DECLARE @IS_COST_OVER_DEFINED_LIMIT  CHAR                                               
 DECLARE @EXISTING_DMG  CHAR                                              
 DECLARE @COVERAGE_DECLINED  CHAR                                              
 DECLARE @SALVAGE_TITLE  CHAR                                               
 DECLARE @IS_LICENSED_FOR_ROAD CHAR                                 
 DECLARE @IS_MODIFIED_INCREASE_SPEED CHAR                                
 DECLARE @IS_MODIFIED_KIT CHAR     
 DECLARE @IS_MODIFIED_KIT_INCSPEED CHAR --                    
 DECLARE @IS_RECORD_EXISTS CHAR    
 DECLARE @IS_CONVICTED_ACCIDENT CHAR   
 DECLARE @ANY_PRIOR_LOSSES CHAR        
 DECLARE @ANY_PRIOR_LOSSES_DESC VARCHAR(50)   
 DECLARE @CURR_RES_TYPE VARCHAR(20)  
 --30 JAN 2008  
 DECLARE @APPLY_PERS_UMB_POL CHAR  
 DECLARE @APPLY_PERS_UMB_POL_DESC VARCHAR(20)   
--27 NOV 2008
declare @STATE_ID smallint

select @STATE_ID=STATE_ID from POL_CUSTOMER_POLICY_LIST with(nolock)                                     
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                          
                                
                                    
                                           
if exists (select IS_COST_OVER_DEFINED_LIMIT from POL_AUTO_GEN_INFO                                               
 where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID )                                              
begin                     
    set @IS_RECORD_EXISTS='N'                           
                 
  select                                     
  @DRIVER_SUS_REVOKED=isnull(DRIVER_SUS_REVOKED,''),@DRIVER_SUS_REVOKED_PP_DESC=isnull(DRIVER_SUS_REVOKED_MC_DESC,''),                                    
  @PHY_MENTL_CHALLENGED=isnull(PHY_MENTL_CHALLENGED,''),            
  @PHY_MENTL_CHALLENGED_PP_DESC=isnull(PHY_MENTL_CHALLENGED_MC_DESC,''),                                    
  @ANY_FINANCIAL_RESPONSIBILITY=isnull(ANY_FINANCIAL_RESPONSIBILITY,''),@ANY_FINANCIAL_RESPONSIBILITY_PP_DESC=isnull(ANY_FINANCIAL_RESPONSIBILITY_MC_DESC,''),                                    
  @MULTI_POLICY_DISC_APPLIED=isnull(MULTI_POLICY_DISC_APPLIED,''),@MULTI_POLICY_DISC_APPLIED_PP_DESC=isnull(MULTI_POLICY_DISC_APPLIED_MC_DESC,''),                                    
  @IS_OTHER_THAN_INSURED=isnull(IS_OTHER_THAN_INSURED,''),@FULLNAME=isnull(FULLNAME,''),@DATE_OF_BIRTH=isnull(DATE_OF_BIRTH,''),                                    
  @IS_EXTENDED_FORKS=isnull(IS_EXTENDED_FORKS,''),@IS_COMMERCIAL_USE=isnull(IS_COMMERCIAL_USE,''),@ANY_NON_OWNED_VEH=isnull(ANY_NON_OWNED_VEH,''),@IS_USEDFOR_RACING=isnull(IS_USEDFOR_RACING,''),                                              
  @IS_TAKEN_OUT=isnull(IS_TAKEN_OUT,''),      
  @IS_MORE_WHEELS=isnull(IS_MORE_WHEELS,''),@IS_CONVICTED_CARELESS_DRIVE=isnull(IS_CONVICTED_CARELESS_DRIVE,''),                                              
  @IS_COST_OVER_DEFINED_LIMIT=isnull(IS_COST_OVER_DEFINED_LIMIT,''),@EXISTING_DMG=isnull(EXISTING_DMG,0),                                    
  @COVERAGE_DECLINED=isnull(COVERAGE_DECLINED,''),@SALVAGE_TITLE=isnull(SALVAGE_TITLE,''),@IS_LICENSED_FOR_ROAD=isnull(IS_LICENSED_FOR_ROAD,''),                                
  @IS_MODIFIED_INCREASE_SPEED=isnull(IS_MODIFIED_INCREASE_SPEED,''),@IS_MODIFIED_KIT=isnull(IS_MODIFIED_KIT,''),    
  @IS_CONVICTED_ACCIDENT=isnull(IS_CONVICTED_ACCIDENT,'') ,  
  @ANY_PRIOR_LOSSES=isnull(ANY_PRIOR_LOSSES,''),@ANY_PRIOR_LOSSES_DESC=isnull(ANY_PRIOR_LOSSES_DESC,''),  
  @CURR_RES_TYPE=isnull(CURR_RES_TYPE,'')  ,  
   --30 jan 2008  
  @APPLY_PERS_UMB_POL = isnull(APPLY_PERS_UMB_POL ,''),  
  @APPLY_PERS_UMB_POL_DESC = isnull(APPLY_PERS_UMB_POL_DESC ,'')                                             
  from POL_AUTO_GEN_INFO   with(nolock)                                            
  where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID                                              
end                                               
else                                               
begin                                        
  SET @DRIVER_SUS_REVOKED=''                                     
  SET @DRIVER_SUS_REVOKED_PP_DESC=''                                     
  SET @PHY_MENTL_CHALLENGED=''                                     
  SET @PHY_MENTL_CHALLENGED_PP_DESC=''                                     
  SET @ANY_FINANCIAL_RESPONSIBILITY=''                                     
  SET @ANY_FINANCIAL_RESPONSIBILITY_PP_DESC=''                                     
  SET @MULTI_POLICY_DISC_APPLIED=''                                     
  SET @MULTI_POLICY_DISC_APPLIED_PP_DESC=''                     
  SET @IS_OTHER_THAN_INSURED=''                           
  SET @FULLNAME=''                                     
  SET @DATE_OF_BIRTH=''                                     
  SET  @IS_EXTENDED_FORKS =''                                              
  SET  @IS_COMMERCIAL_USE =''                                    
  SET  @ANY_NON_OWNED_VEH =''                             
  SET  @IS_USEDFOR_RACING =''                                                
  SET  @IS_TAKEN_OUT =''                                                
  SET  @IS_MORE_WHEELS =''                                                
  SET  @DRIVER_SUS_REVOKED =''                                               
  SET  @IS_CONVICTED_CARELESS_DRIVE =''                                              
  SET  @IS_COST_OVER_DEFINED_LIMIT=''                                              
  SET  @EXISTING_DMG=''                                              
  SET  @ANY_FINANCIAL_RESPONSIBILITY=''                                              
  SET  @COVERAGE_DECLINED=''                                              
  SET  @SALVAGE_TITLE=''                                              
  SET  @IS_LICENSED_FOR_ROAD=''                                  
  SET  @IS_MODIFIED_INCREASE_SPEED=''                                
  SET  @IS_MODIFIED_KIT=''                     
  SET @IS_MODIFIED_KIT_INCSPEED=''                     
  SET @IS_RECORD_EXISTS='Y'     
  SET  @IS_CONVICTED_ACCIDENT=''  
  SET  @ANY_PRIOR_LOSSES=''      
  SET  @ANY_PRIOR_LOSSES_DESC=''  
  SET  @CURR_RES_TYPE=''  
  --30 JAN 2008                      
  SET @APPLY_PERS_UMB_POL = ''  
  SET @APPLY_PERS_UMB_POL_DESC =''                                      
                             
end                                              
--    
IF(@IS_CONVICTED_ACCIDENT='1')                                  
 BEGIN                                   
 SET @IS_CONVICTED_ACCIDENT='Y'                                  
 END                        
ELSE IF(@IS_CONVICTED_ACCIDENT='0')                                  
 BEGIN                                   
 SET @IS_CONVICTED_ACCIDENT='N'                                  
 END        
                                  
--  
if(@ANY_PRIOR_LOSSES='1')                                  
begin                                   
 set @ANY_PRIOR_LOSSES='Y'                                  
end                        
else if(@ANY_PRIOR_LOSSES='0')                                  
begin                                   
 set @ANY_PRIOR_LOSSES='N'                                  
end    
--                               
 if(@IS_MODIFIED_INCREASE_SPEED='1' or @IS_MODIFIED_KIT='1')                                
 begin                                 
  set @IS_MODIFIED_KIT_INCSPEED='Y'                                
 end                                 
 else                                
 begin                                 
  set @IS_MODIFIED_KIT_INCSPEED='N'                                
 end                                 
                                
                       
                                
--1.                                              
if(@IS_EXTENDED_FORKS='1')                                              
begin                                              
 set @IS_EXTENDED_FORKS ='Y'                                              
end                            
else if(@IS_EXTENDED_FORKS='0')                                              
begin                                              
 set @IS_EXTENDED_FORKS ='N'                                              
end                                              
--2.                                              
if(@IS_COMMERCIAL_USE='1')                                              
begin                                              
 set @IS_COMMERCIAL_USE ='Y'                                              
end                                      
else if(@IS_COMMERCIAL_USE='0')                
begin        
 set @IS_COMMERCIAL_USE ='N'                                 
end                                      
--3.                                       
if(@ANY_NON_OWNED_VEH='1')                                       
begin                                              
 set @ANY_NON_OWNED_VEH ='Y'                                              
end    
else if(@ANY_NON_OWNED_VEH='0')                                              
begin                                          
 set @ANY_NON_OWNED_VEH ='N'                         
end                                      
--4.                                               
if(@IS_USEDFOR_RACING='1')                                              
begin                                              
 set @IS_USEDFOR_RACING ='Y'                                              
end                                      
else if(@IS_USEDFOR_RACING='0')                                              
begin                                   
 set @IS_USEDFOR_RACING ='N'                                              
end                                      
--5.                                              
--if(@IS_TAKEN_OUT='1' )                                              
--begin                                              
-- set @IS_TAKEN_OUT ='Y'                                              
--end                                      
--else if(@IS_TAKEN_OUT='0')                                              
--begin                                             
-- set @IS_TAKEN_OUT ='N'                                              
--end                                     
--------------Added for Itrack 5121 by Praveen Kumar on 27-11-08---
if(@STATE_ID=14 or @STATE_ID=22)
 begin
	set @IS_TAKEN_OUT ='N'
 end
else
  begin
	if(@IS_TAKEN_OUT='1')                                              
	begin                                              
	 set @IS_TAKEN_OUT ='Y'                                              
	end                                      
	else if(@IS_TAKEN_OUT='0')                                              
	begin                                             
	 set @IS_TAKEN_OUT ='N'                                              
	end             
 end     
--6.                                               
if(@IS_MORE_WHEELS='1')                                              
begin                                              
 set @IS_MORE_WHEELS ='Y'                                              
end                                      
else if(@IS_MORE_WHEELS='0')                                              
begin                                              
 set @IS_MORE_WHEELS ='N'                                              
end                                      
--7.                                      
if(@DRIVER_SUS_REVOKED='1')                                              
begin                                              
 set @DRIVER_SUS_REVOKED ='Y'                                      
end                                      
else if(@DRIVER_SUS_REVOKED='0')                                              
begin                                              
 set @DRIVER_SUS_REVOKED ='N'                                              
end                                      
--8.                                              
if(@IS_CONVICTED_CARELESS_DRIVE='1')                                              
begin                                              
 set @IS_CONVICTED_CARELESS_DRIVE ='Y'                                              
end                                      
else if(@IS_CONVICTED_CARELESS_DRIVE='0')                                              
begin                                              
 set @IS_CONVICTED_CARELESS_DRIVE ='N'                                           
end                                      
--9.                                          
if(@IS_COST_OVER_DEFINED_LIMIT='1')                                              
begin                                              
 set @IS_COST_OVER_DEFINED_LIMIT ='Y'                                              
end                                      
else if(@IS_COST_OVER_DEFINED_LIMIT='0')                                              
begin                                              
 set @IS_COST_OVER_DEFINED_LIMIT ='N'                                              
end                                      
--10                                              
if(@EXISTING_DMG='1')                                              
begin                                              
set @EXISTING_DMG ='Y'                             
end                                 
else if(@EXISTING_DMG='0')                                              
begin                                              
 set @EXISTING_DMG ='N'                                              
end                                      
--11                                               
if(@ANY_FINANCIAL_RESPONSIBILITY='1')                                              
begin                          
 set @ANY_FINANCIAL_RESPONSIBILITY ='Y'                                              
end                                      
else if(@ANY_FINANCIAL_RESPONSIBILITY='0')                                              
begin                                              
 set @ANY_FINANCIAL_RESPONSIBILITY ='N'                                              
end                                      
--12                                              
if(@COVERAGE_DECLINED='1')                                              
begin                                              
 set @COVERAGE_DECLINED ='Y'                                        
end                                      
else if(@COVERAGE_DECLINED='0')                                              
begin                                              
 set @COVERAGE_DECLINED ='N'                                              
end                                      
--13                                              
if(@SALVAGE_TITLE='1')                                              
begin                                              
 set @SALVAGE_TITLE ='Y'                              
end                                      
else if(@SALVAGE_TITLE='0')                                              
begin                                              
 set @SALVAGE_TITLE ='N'                                              
end                          
--14                                              
if(@IS_LICENSED_FOR_ROAD='1')                                              
begin                                              
 set @IS_LICENSED_FOR_ROAD ='Y'                                              
end                                      
else if(@IS_LICENSED_FOR_ROAD='0')                                              
begin                                              
 set @IS_LICENSED_FOR_ROAD ='N'                                              
end                                      
                               
--14                                              
if(@PHY_MENTL_CHALLENGED='1')                                              
begin               
 set @PHY_MENTL_CHALLENGED ='Y'                                              
end                                      
else if(@PHY_MENTL_CHALLENGED='0')                                              
begin                                              
 set @PHY_MENTL_CHALLENGED ='N'                                              
end                                      
                     
---                          
              
if(@MULTI_POLICY_DISC_APPLIED='' or @MULTI_POLICY_DISC_APPLIED is NULL)              
begin               
 set @MULTI_POLICY_DISC_APPLIED='N'              
end               
                                   
  
  
/*Underwriting question:      
"Any prior losses?", if not saved, means it is blank or null in database, prompt for answering to this question.      
If Yes to "Any prior losses?", then look at prior losses for Auto LOB.If there is none then refer to underwriter      
 If No, and there are prior losses refer UWR. */      
      
DECLARE @PRIOR_LOSS_INFO_EXISTS CHAR      
SET @PRIOR_LOSS_INFO_EXISTS='P'     
      
 IF EXISTS ( SELECT CUSTOMER_ID FROM APP_PRIOR_LOSS_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND LOB=3)      
  BEGIN       
   SET @PRIOR_LOSS_INFO_EXISTS='T'      
  END      
 ELSE      
  BEGIN       
   SET @PRIOR_LOSS_INFO_EXISTS='F'      
  END           
      
IF( (@ANY_PRIOR_LOSSES='Y' AND @PRIOR_LOSS_INFO_EXISTS='F') OR (@ANY_PRIOR_LOSSES='N' AND @PRIOR_LOSS_INFO_EXISTS='T') )      
BEGIN       
 SET @PRIOR_LOSS_INFO_EXISTS='Y'      
END       
  
  
--16 --30 jan 2008                                      
 IF(@APPLY_PERS_UMB_POL='1')                                      
 BEGIN                                      
 SET @APPLY_PERS_UMB_POL ='Y'                                      
 END                              
 ELSE IF(@APPLY_PERS_UMB_POL='0')                                      
 BEGIN                                      
 SET @APPLY_PERS_UMB_POL ='N'                                      
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
  --Homeowners - 1 Automobile - 2 Motorcycle - 3 Watercraft - 4 Umbrella   - 5 Rental     - 6 General Liability - 7  
                  
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
  PRINT @PROCESS_ID  
  PRINT @COUNT  
  IF(@COUNT >0 AND ( @PROCESS_ID IN(24,25,31,32)) )  
  BEGIN   
   SET @MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y'  
  END  
 -->>2: ==>>>RENEWAL   
  --If there are no Eligible Policies and there is Yes in the Field multi-policy discount applied?*  
  --program to see if the policy number in the Field Multi-policy discount description is active   
  --If policy is not active or does not exist on the database  
  --This will goes as a Refer to Underwriters  
  -- Note for the referral - Multi Policy Discount Eligibility  
  -- If referral is accepted - allow discount    
    
  ELSE IF(@COUNT=0  AND (@COUNT_POL_STATUS>0 OR @COUNT_POLICY_NUMBER=0)  AND (@PROCESS_ID IN(5,18)))  
  BEGIN   
   SET @MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y'  
  END  
    END                        
---------------------------        
                                                              
SELECT                                        
  @DRIVER_SUS_REVOKED AS DRIVER_SUS_REVOKED,                                    
  CASE @DRIVER_SUS_REVOKED                   
  WHEN '1'THEN  @DRIVER_SUS_REVOKED_PP_DESC                                         
  END AS DRIVER_SUS_REVOKED_PP_DESC,                                        
                                      
  @PHY_MENTL_CHALLENGED AS PHY_MENTL_CHALLENGED,                                    
  CASE @PHY_MENTL_CHALLENGED                                     
  WHEN '1' THEN @PHY_MENTL_CHALLENGED_PP_DESC                                     
  END AS PHY_MENTL_CHALLENGED_MC_DESC ,                                    
                                      
  @ANY_FINANCIAL_RESPONSIBILITY AS ANY_FINANCIAL_RESPONSIBILITY,                                    
  CASE @ANY_FINANCIAL_RESPONSIBILITY                                    
  WHEN '1' THEN  @ANY_FINANCIAL_RESPONSIBILITY_PP_DESC                                     
  END AS ANY_FINANCIAL_RESPONSIBILITY_PP_DESC ,                              
                                      
  @MULTI_POLICY_DISC_APPLIED AS MULTI_POLICY_DISC_APPLIED,                                    
  CASE @MULTI_POLICY_DISC_APPLIED                                     
  WHEN '1' THEN @MULTI_POLICY_DISC_APPLIED_PP_DESC                                    
  END AS MULTI_POLICY_DISC_APPLIED_PP_DESC,                                    
                                      
  CASE @IS_OTHER_THAN_INSURED                                     
  WHEN '1' THEN @FULLNAME                                      
  END AS FULLNAME,                                    
                                      
  CASE @IS_OTHER_THAN_INSURED                                     
  WHEN '1' THEN  @DATE_OF_BIRTH                                      
  END AS DATE_OF_BIRTH,                                    
  @IS_EXTENDED_FORKS AS IS_EXTENDED_FORKS,                                    
  @IS_COMMERCIAL_USE AS IS_COMMERCIAL_USE,                                    
  @ANY_NON_OWNED_VEH AS ANY_NON_OWNED_VEH,                                    
  @IS_USEDFOR_RACING AS IS_USEDFOR_RACING,                                    
  @IS_TAKEN_OUT AS IS_TAKEN_OUT,                                    
  @IS_MORE_WHEELS AS IS_MORE_WHEELS,                                   
 -- @DRIVER_SUS_REVOKED AS DRIVER_SUS_REVOKED,                                    
  @IS_CONVICTED_CARELESS_DRIVE AS IS_CONVICTED_CARELESS_DRIVE,                                    
  @IS_COST_OVER_DEFINED_LIMIT AS IS_COST_OVER_DEFINED_LIMIT,                                              
  @EXISTING_DMG AS EXISTING_DMG,                                              
 -- @ANY_FINANCIAL_RESPONSIBILITY AS ANY_FINANCIAL_RESPONSIBILITY,                                              
  @COVERAGE_DECLINED AS COVERAGE_DECLINED,                                    
  @SALVAGE_TITLE AS SALVAGE_TITLE,                                              
  @IS_LICENSED_FOR_ROAD AS IS_LICENSED_FOR_ROAD ,            
  @IS_MODIFIED_INCREASE_SPEED AS IS_MODIFIED_INCREASE_SPEED,                                
  @IS_MODIFIED_KIT AS IS_MODIFIED_KIT,                                
  @IS_MODIFIED_KIT_INCSPEED AS IS_MODIFIED_KIT_INCSPEED,                  
  @IS_RECORD_EXISTS AS IS_RECORD_EXISTS,    
  @IS_CONVICTED_ACCIDENT  AS IS_CONVICTED_ACCIDENT ,  
  @PRIOR_LOSS_INFO_EXISTS AS PRIOR_LOSS_INFO_EXISTS,  
  @ANY_PRIOR_LOSSES AS ANY_PRIOR_LOSSES,        
  CASE @ANY_PRIOR_LOSSES     
  WHEN 'Y' THEN @ANY_PRIOR_LOSSES_DESC        
  END AS ANY_PRIOR_LOSSES_DESC,  
   @CURR_RES_TYPE AS CURR_RES_TYPE   ,  
  --30 JAN 2008  
  CASE @APPLY_PERS_UMB_POL   
  WHEN 'Y' THEN @APPLY_PERS_UMB_POL_DESC  
  END AS APPLY_PERS_UMB_POL_DESC,  
         @MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS AS MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS   
   
 END      
    

GO

