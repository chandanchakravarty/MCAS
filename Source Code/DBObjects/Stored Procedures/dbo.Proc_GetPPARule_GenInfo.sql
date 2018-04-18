IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPPARule_GenInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPPARule_GenInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                        
Proc Name                : Dbo.Proc_GetPPARule                                      
Created by               : Ashwani                                        
Date                     : 14 Nov.,2005          
Purpose                  : To get the Auto Gen Information for Private Passenger Auto                                      
Revison History          :                                        
Used In                  : Wolverine                                        
------------------------------------------------------------                                        
Date     Review By          Comments                                        
------   ------------       -------------------------*/                                        
CREATE proc dbo.Proc_GetPPARule_GenInfo                                        
(                                        
@CUSTOMERID    int,                                        
@APPID    int,                                        
@APPVERSIONID   int                          
)                                        
as                                            
begin         
--1        
declare @COVERAGE_DECLINED char             
declare @COVERAGE_DECLINED_PP_DESC varchar(50)        
--2         
declare @H_MEM_IN_MILITARY char                              
declare @H_MEM_IN_MILITARY_DESC varchar(50)        
--3        
declare @ANY_FINANCIAL_RESPONSIBILITY char      -- if 'y'  should be referred to underwriter          
declare @ANY_FINANCIAL_RESPONSIBILITY_PP_DESC varchar(50)        
--4        
declare @CAR_MODIFIED char                              
declare @CAR_MODIFIED_DESC varchar(50)        
--5        
declare @SALVAGE_TITLE char              
declare @SALVAGE_TITLE_PP_DESC varchar(50)        
--6         
declare @ANY_NON_OWNED_VEH char              
declare @ANY_NON_OWNED_VEH_PP_DESC varchar(50)        
--7        
declare @EXISTING_DMG char                              
declare @EXISTING_DMG_PP_DESC varchar(50)        
--8         
declare @ANY_CAR_AT_SCH char                              
declare @ANY_CAR_AT_SCH_DESC varchar(50)        
--9        
declare @ANY_OTH_AUTO_INSU nchar(1)          
declare @ANY_OTH_AUTO_INSU_DESC varchar(50)        
--10         
declare @DRIVER_SUS_REVOKED char           
declare @DRIVER_SUS_REVOKED_PP_DESC varchar(50)        
--11        
declare @PHY_MENTL_CHALLENGED char                              
declare @PHY_MENTL_CHALLENGED_PP_DESC varchar(50)        
--Manadatory Fields         
--12        
declare @ANY_OTH_INSU_COMP char                              
declare @ANY_OTH_INSU_COMP_PP_DESC varchar(50)        
--13        
declare @INS_AGENCY_TRANSFER nchar(1)          
declare @INS_AGENCY_TRANSFER_PP_DESC varchar(50)        
--14        
declare @AGENCY_VEH_INSPECTED nchar(1)          
declare @AGENCY_VEH_INSPECTED_PP_DESC varchar(50)        
--15        
declare @USE_AS_TRANSPORT_FEE char          
--DECLARE @USE_AS_TRANSPORT_FEE_DESC varchar(50)        
--16        
declare @ANY_ANTIQUE_AUTO nchar(1)          
declare @ANY_ANTIQUE_AUTO_DESC varchar(50)        
--17        
declare @MULTI_POLICY_DISC_APPLIED nchar(1)         
declare @MULTI_POLICY_DISC_APPLIED_PP_DESC varchar(50)        
--18        
declare @IS_OTHER_THAN_INSURED char -- if 'Y' then         
declare @FULLNAME varchar(50)        
declare @DATE_OF_BIRTH varchar(20)        
--19        
declare @INSUREDELSEWHERE nchar(1) -- if 'Y' then         
declare @COMPANYNAME varchar(50)        
declare @POLICYNUMBER varchar(50)     
--     
declare @IS_RECORD_EXISTS char       
declare @ANY_PRIOR_LOSSES char    
declare @ANY_PRIOR_LOSSES_DESC varchar(50)   
declare @COST_EQUIPMENT_DESC varchar(50)  
   
        
if exists(select CUSTOMER_ID from APP_AUTO_GEN_INFO where                               
 CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID)                      
begin      
 set @IS_RECORD_EXISTS='N'    
select        
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
@ANY_PRIOR_LOSSES=isnull(ANY_PRIOR_LOSSES,''),@ANY_PRIOR_LOSSES_DESC=isnull(ANY_PRIOR_LOSSES_DESC,''),  
@COST_EQUIPMENT_DESC      =isnull(convert(varchar(50),COST_EQUIPMENT_DESC),'')  
from APP_AUTO_GEN_INFO        
where CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID  and isnull(IS_ACTIVE,'N')='Y'                            
end                       
else  
begin
 set @COVERAGE_DECLINED='' 
 set @COVERAGE_DECLINED_PP_DESC= '' 
 set @H_MEM_IN_MILITARY=''
 set @H_MEM_IN_MILITARY_DESC=''
 set @IS_RECORD_EXISTS='Y'  
end  
  
------------------------------------------------------------------------------------------    
/*Underwriting question:  
 "Any prior losses?", if not saved, means it is blank or null in database, prompt for answering to this question.  
  If Yes to "Any prior losses?", then look at prior losses for Auto LOB. If there is none then refer to underwriter  
  If No, and there are prior losses refer UWR.*/  
  
declare @PRIOR_LOSS_Y char  
declare @PRIOR_LOSS_N char  
  
set @PRIOR_LOSS_Y='N'  
set @PRIOR_LOSS_Y='N'  
  
if(@ANY_PRIOR_LOSSES='1')  
begin   
 -- No prior loss   
 if not exists(select CUSTOMER_ID from APP_PRIOR_LOSS_INFO where CUSTOMER_ID=@CUSTOMERID and LOB =2)  
  set @PRIOR_LOSS_Y ='Y'  
end  
else if(@ANY_PRIOR_LOSSES='0')  
begin   
 if exists(select CUSTOMER_ID from APP_PRIOR_LOSS_INFO where CUSTOMER_ID=@CUSTOMERID and LOB =2)  
  set @PRIOR_LOSS_N='Y'  
end    
------------------------------------------------------------------------------------------    
if(@IS_OTHER_THAN_INSURED='1')                              
begin                               
 set @IS_OTHER_THAN_INSURED='Y'                              
end                    
else if(@IS_OTHER_THAN_INSURED='0')                              
begin           
 set @IS_OTHER_THAN_INSURED='N'                              
end                     
    
        
-- 1                               
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
 /*    
 Underwriting Question    
 Has any car been modified, assembled or kit vehicle or have special equipment? (include customized vans and pickups indicate cost) is  Yes     
     
 Then look at the Vehicle Info Tab     
 If Vehicle Type is not customized truck or van     
 Then Refer to Underwriters */    
    
declare @intCount int    
select @intCount=count(isnull(VEHICLE_TYPE_PER,'0')) from APP_VEHICLES                                        
 where CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID and VEHICLE_TYPE_PER='11335' and isnull(IS_ACTIVE,'N')='Y'    
    
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
---------------------------                       
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
------------------      
--Any vehicle considered an antique car? Mandatory question If yes On new business - Risk is Declined     
-- Grandfathered, for existing policies only where the inception date is prior to 01/01/2003    
 declare @APP_INCEPTION_DATE datetime    
 declare @APP_INCEPTION_DATE_FIX datetime    
 declare @STATE_ID int    
 set @APP_INCEPTION_DATE_FIX = '2003-01-01'    
     
     
 select  @APP_INCEPTION_DATE= APP_INCEPTION_DATE,@STATE_ID=STATE_ID from APP_LIST     
 where CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID                              
    
 -- select @APP_INCEPTION_DATE    
    
 if(@APP_INCEPTION_DATE < @APP_INCEPTION_DATE_FIX and @ANY_ANTIQUE_AUTO='1' )    
  set @ANY_ANTIQUE_AUTO ='Y'    
 else    
  set @ANY_ANTIQUE_AUTO = 'N'    
    
if(@POLICYNUMBER is null)      
begin       
 set @POLICYNUMBER=''      
end       
------------------------------------------------------------------------------------    
    
    
      
select          
--1        
@COVERAGE_DECLINED as COVERAGE_DECLINED,        
case @COVERAGE_DECLINED        
when 'Y'then  @COVERAGE_DECLINED_PP_DESC         
end as COVERAGE_DECLINED_PP_DESC,        
--2         
@H_MEM_IN_MILITARY as H_MEM_IN_MILITARY,        
case @H_MEM_IN_MILITARY        
when 'Y'then  @H_MEM_IN_MILITARY_DESC         
end as H_MEM_IN_MILITARY_DESC,        
--3        
@ANY_FINANCIAL_RESPONSIBILITY  as ANY_FINANCIAL_RESPONSIBILITY,        
case @ANY_FINANCIAL_RESPONSIBILITY        
when 'Y'then  @ANY_FINANCIAL_RESPONSIBILITY_PP_DESC         
end as ANY_FINANCIAL_RESPONSIBILITY_PP_DESC,        
--4        
@CAR_MODIFIED as CAR_MODIFIED  ,        
case @CAR_MODIFIED        
when 'Y'then  @CAR_MODIFIED_DESC         
end as CAR_MODIFIED_DESC,       
--  
@CAR_MODIFIED as CAR_MODIFIED  ,        
case @CAR_MODIFIED        
when 'Y'then  @COST_EQUIPMENT_DESC         
end as COST_EQUIPMENT_DESC,  
   
--5        
@SALVAGE_TITLE as SALVAGE_TITLE ,        
case @SALVAGE_TITLE       
when 'Y'then  @SALVAGE_TITLE_PP_DESC         
end as SALVAGE_TITLE_PP_DESC,        
--6         
@ANY_NON_OWNED_VEH as ANY_NON_OWNED_VEH,        
case @ANY_NON_OWNED_VEH        
when 'Y'then  @ANY_NON_OWNED_VEH_PP_DESC         
end as ANY_NON_OWNED_VEH_PP_DESC,        
--7        
@EXISTING_DMG as EXISTING_DMG,       
case @EXISTING_DMG        
when 'Y'then  @EXISTING_DMG_PP_DESC         
end as EXISTING_DMG_PP_DESC,        
--8         
@ANY_CAR_AT_SCH as ANY_CAR_AT_SCH,        
case @ANY_CAR_AT_SCH        
when 'Y'then  @ANY_CAR_AT_SCH_DESC         
end as ANY_CAR_AT_SCH_DESC,        
--9        
--@ANY_OTH_INSU_COMP as ANY_OTH_INSU_COMP,         
--case @ANY_OTH_INSU_COMP        
--when 'Y'then  @ANY_OTH_INSU_COMP_PP_DESC         
--end as ANY_OTH_INSU_COMP_PP_DESC,        
--10         
@DRIVER_SUS_REVOKED as DRIVER_SUS_REVOKED ,        
case @DRIVER_SUS_REVOKED        
when 'Y'then  @DRIVER_SUS_REVOKED_PP_DESC         
end as DRIVER_SUS_REVOKED_PP_DESC,        
--11        
@PHY_MENTL_CHALLENGED as PHY_MENTL_CHALLENGED,        
case @PHY_MENTL_CHALLENGED        
when 'Y'then  @PHY_MENTL_CHALLENGED_PP_DESC         
end as PHY_MENTL_CHALLENGED_PP_DESC,        
--Manadatory Fields         
--12        
@ANY_OTH_AUTO_INSU as ANY_OTH_AUTO_INSU ,        
case @ANY_OTH_AUTO_INSU        
when 'Y'then  @ANY_OTH_AUTO_INSU_DESC         
end as ANY_OTH_AUTO_INSU_DESC,        
--13        
@INS_AGENCY_TRANSFER as INS_AGENCY_TRANSFER,         
case @INS_AGENCY_TRANSFER        
when '1'then  @INS_AGENCY_TRANSFER_PP_DESC         
end as INS_AGENCY_TRANSFER_PP_DESC,        
--14        
@AGENCY_VEH_INSPECTED as AGENCY_VEH_INSPECTED ,        
case @AGENCY_VEH_INSPECTED        
when '1'then  @AGENCY_VEH_INSPECTED_PP_DESC         
end as AGENCY_VEH_INSPECTED_PP_DESC,        
--15        
@USE_AS_TRANSPORT_FEE as USE_AS_TRANSPORT_FEE  ,        
--case @USE_AS_TRANSPORT_FEE        
--when '1'then  @USE_AS_TRANSPORT_FEE_DESC         
--end as USE_AS_TRANSPORT_FEE_DESC,        
--16        
@ANY_ANTIQUE_AUTO as ANY_ANTIQUE_AUTO,        
case @ANY_ANTIQUE_AUTO        
when '1'then  @ANY_ANTIQUE_AUTO_DESC         
end as ANY_ANTIQUE_AUTO_DESC,        
--17        
@MULTI_POLICY_DISC_APPLIED as MULTI_POLICY_DISC_APPLIED,        
case @MULTI_POLICY_DISC_APPLIED        
when '1'then  @MULTI_POLICY_DISC_APPLIED_PP_DESC         
end as MULTI_POLICY_DISC_APPLIED_PP_DESC,        
--18        
case @IS_OTHER_THAN_INSURED when 'Y'        
 then  @IS_OTHER_THAN_INSURED end as IS_OTHER_THAN_INSURED,        
           
        
--@IS_OTHER_THAN_INSURED as IS_OTHER_THAN_INSURED,  -- if 'Y' then         
        
case @IS_OTHER_THAN_INSURED        
when 'Y'then  @FULLNAME         
end as FULLNAME,        
--- 18.1        
--@IS_OTHER_THAN_INSURED as IS_OTHER_THAN_INSURED,  -- if 'Y' then         
case @IS_OTHER_THAN_INSURED        
when 'Y'then  @DATE_OF_BIRTH         
end as DATE_OF_BIRTH,        
        
        
case @IS_OTHER_THAN_INSURED        
   when 'Y' then         
 case @INSUREDELSEWHERE        
    when '1' then @POLICYNUMBER         
 end        
end as POLICYNUMBER,          
case @IS_OTHER_THAN_INSURED        
 when 'Y' then         
 case @INSUREDELSEWHERE        
  when '1' then @COMPANYNAME         
 end        
end as COMPANYNAME,    
@IS_RECORD_EXISTS as IS_RECORD_EXISTS,    
@ANY_PRIOR_LOSSES as ANY_PRIOR_LOSSES,    
    
case @ANY_PRIOR_LOSSES     
when 'Y' then @ANY_PRIOR_LOSSES_DESC    
end as ANY_PRIOR_LOSSES_DESC,  
@PRIOR_LOSS_Y as PRIOR_LOSS_Y,  
@PRIOR_LOSS_N as PRIOR_LOSS_N      
end    




GO

