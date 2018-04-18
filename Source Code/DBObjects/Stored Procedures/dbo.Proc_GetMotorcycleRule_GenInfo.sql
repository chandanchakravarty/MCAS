IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMotorcycleRule_GenInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMotorcycleRule_GenInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create proc dbo.Proc_GetMotorcycleRule_GenInfo
(                                                              
@CUSTOMERID    int,                                                              
@APPID    int,                                                              
@APPVERSIONID   int,                          
@DESC varchar(10)                                                
)                                                              
as                                                                  
begin                               
--APP_AUTO_GEN_INFO                                    
-- Mandatory                          
declare @DRIVER_SUS_REVOKED char                          
declare @DRIVER_SUS_REVOKED_PP_DESC nvarchar(75)                          
declare @PHY_MENTL_CHALLENGED char                          
declare @PHY_MENTL_CHALLENGED_PP_DESC nvarchar(75)                          
declare @ANY_FINANCIAL_RESPONSIBILITY  char                           
declare @ANY_FINANCIAL_RESPONSIBILITY_PP_DESC nvarchar(75)                          
declare @MULTI_POLICY_DISC_APPLIED   nchar(1)                          
declare @MULTI_POLICY_DISC_APPLIED_PP_DESC varchar(25)                          
declare @IS_OTHER_THAN_INSURED nchar(1) -- if 'Y' then                               
declare @FULLNAME varchar(50)                              
declare @DATE_OF_BIRTH varchar(20)                              
--Rules                           
 declare @IS_EXTENDED_FORKS  char                           
 declare @IS_COMMERCIAL_USE  char                                    
 declare @ANY_NON_OWNED_VEH  char                                    
 declare @IS_USEDFOR_RACING  char                                    
 declare @IS_TAKEN_OUT  char                                     
 declare @IS_MORE_WHEELS  char                                    
 declare @IS_CONVICTED_CARELESS_DRIVE  char                                    
 declare @IS_COST_OVER_DEFINED_LIMIT  char                                     
 declare @EXISTING_DMG  char                                    
 declare @COVERAGE_DECLINED  char                                    
 declare @SALVAGE_TITLE  char                                     
 declare @IS_LICENSED_FOR_ROAD char                       
 declare @IS_MODIFIED_INCREASE_SPEED char                      
 declare @IS_MODIFIED_KIT char                      
 declare @IS_MODIFIED_KIT_INCSPEED char --          
 declare @IS_RECORD_EXISTS char   
 declare @ANY_PRIOR_LOSSES char    
 declare @ANY_PRIOR_LOSSES_DESC varchar(50)  
 declare  @IS_CONVICTED_ACCIDENT char  
 declare @CURR_RES_TYPE varchar(20)
--30 jan 2008
 declare @APPLY_PERS_UMB_POL char
 declare @APPLY_PERS_UMB_POL_DESC varchar(20) 
 --27 NOV 2008
declare @STATE_ID smallint

select @STATE_ID=STATE_ID from APP_LIST where CUSTOMER_ID=1505 and APP_ID=7 and APP_VERSION_ID=1

  
  
                            
                     
                      
                           
     
if exists (select IS_COST_OVER_DEFINED_LIMIT from APP_AUTO_GEN_INFO                                     
 where CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID )                                    
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
  @IS_TAKEN_OUT=isnull(IS_TAKEN_OUT,''),@IS_MORE_WHEELS=isnull(IS_MORE_WHEELS,''),@IS_CONVICTED_CARELESS_DRIVE=isnull(IS_CONVICTED_CARELESS_DRIVE,''),                                    
  @IS_COST_OVER_DEFINED_LIMIT=isnull(IS_COST_OVER_DEFINED_LIMIT,''),@EXISTING_DMG=isnull(EXISTING_DMG,0),                          
  @COVERAGE_DECLINED=isnull(COVERAGE_DECLINED,''),@SALVAGE_TITLE=isnull(SALVAGE_TITLE,''),@IS_LICENSED_FOR_ROAD=isnull(IS_LICENSED_FOR_ROAD,''),                      
  @IS_MODIFIED_INCREASE_SPEED=isnull(IS_MODIFIED_INCREASE_SPEED,''),@IS_MODIFIED_KIT=isnull(IS_MODIFIED_KIT,''),  
  @ANY_PRIOR_LOSSES=isnull(ANY_PRIOR_LOSSES,''),@ANY_PRIOR_LOSSES_DESC=isnull(ANY_PRIOR_LOSSES_DESC,''),  
  @IS_CONVICTED_ACCIDENT=isnull(IS_CONVICTED_ACCIDENT,''),@CURR_RES_TYPE=isnull(CURR_RES_TYPE,'') ,
 --30 jan 2008
  @APPLY_PERS_UMB_POL = isnull(APPLY_PERS_UMB_POL ,''),
  @APPLY_PERS_UMB_POL_DESC = isnull(APPLY_PERS_UMB_POL_DESC ,'')
  from APP_AUTO_GEN_INFO with(nolock)                                     
  where CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID                                    
end                                     
else                                     
begin                              
set @DRIVER_SUS_REVOKED=''                           
 set @DRIVER_SUS_REVOKED_PP_DESC=''                           
 set @PHY_MENTL_CHALLENGED=''                           
 set @PHY_MENTL_CHALLENGED_PP_DESC=''                           
 set @ANY_FINANCIAL_RESPONSIBILITY=''                           
 set @ANY_FINANCIAL_RESPONSIBILITY_PP_DESC=''                           
 set @MULTI_POLICY_DISC_APPLIED=''                           
 set @MULTI_POLICY_DISC_APPLIED_PP_DESC=''                           
 set @IS_OTHER_THAN_INSURED=''                           
 set @FULLNAME=''                           
 set @DATE_OF_BIRTH=''                           
 set  @IS_EXTENDED_FORKS =''                                    
 set  @IS_COMMERCIAL_USE =''                                      
 set  @ANY_NON_OWNED_VEH =''                                     
 set  @IS_USEDFOR_RACING =''                                      
 set  @IS_TAKEN_OUT =''                                      
 set  @IS_MORE_WHEELS =''                                      
 set  @DRIVER_SUS_REVOKED =''                                     
 set  @IS_CONVICTED_CARELESS_DRIVE =''                                    
 set  @IS_COST_OVER_DEFINED_LIMIT=''                                    
 set  @EXISTING_DMG=''                        
 set  @ANY_FINANCIAL_RESPONSIBILITY=''                                    
 set  @COVERAGE_DECLINED=''                                    
 set  @SALVAGE_TITLE=''                                   
 set  @IS_LICENSED_FOR_ROAD=''                        
 set  @IS_MODIFIED_INCREASE_SPEED=''                      
 set  @IS_MODIFIED_KIT=''                      
 set  @IS_MODIFIED_KIT_INCSPEED=''           
 set  @IS_RECORD_EXISTS='Y'   
 set  @ANY_PRIOR_LOSSES=''  
 set  @ANY_PRIOR_LOSSES_DESC=''   
 set  @IS_CONVICTED_ACCIDENT=''   
 set  @CURR_RES_TYPE=''
--30 jan 2008                    
 set @APPLY_PERS_UMB_POL = ''
 set @APPLY_PERS_UMB_POL_DESC =''
                   
end                                    
  
if(@IS_CONVICTED_ACCIDENT='1')                              
begin                               
 set @IS_CONVICTED_ACCIDENT='Y'                              
end                    
else if(@IS_CONVICTED_ACCIDENT='0')                              
begin                               
 set @IS_CONVICTED_ACCIDENT='N'                              
end    
  
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
--if(@IS_TAKEN_OUT='1')                                    
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
------  
/*Underwriting question:  
"Any prior losses?", if not saved, means it is blank or null in database, prompt for answering to this question.  
If Yes to "Any prior losses?", then look at prior losses for Auto LOB.If there is none then refer to underwriter  
 If No, and there are prior losses refer UWR. */  
  
declare @PRIOR_LOSS_INFO_EXISTS char  
set @PRIOR_LOSS_INFO_EXISTS='P'  
  
if exists ( select CUSTOMER_ID from APP_PRIOR_LOSS_INFO where CUSTOMER_ID=@CUSTOMERID and LOB=3)  
begin   
 set @PRIOR_LOSS_INFO_EXISTS='T'  
end  
else  
begin   
 set @PRIOR_LOSS_INFO_EXISTS='F'  
end       
  
if( (@ANY_PRIOR_LOSSES='Y' and @PRIOR_LOSS_INFO_EXISTS='F') or (@ANY_PRIOR_LOSSES='N' and @PRIOR_LOSS_INFO_EXISTS='T') )  
begin   
 set @PRIOR_LOSS_INFO_EXISTS='Y'  
end   


--16 --30 jan 2008                                    
if(@APPLY_PERS_UMB_POL='1')                                    
begin                                    
 set @APPLY_PERS_UMB_POL ='Y'                                    
end                            
else if(@APPLY_PERS_UMB_POL='0')                                    
begin                                    
 set @APPLY_PERS_UMB_POL ='N'                                    
end    
---------------------------    
                         
                          
select                              
 @DRIVER_SUS_REVOKED as DRIVER_SUS_REVOKED,                          
 case @DRIVER_SUS_REVOKED                              
 when '1'then  @DRIVER_SUS_REVOKED_PP_DESC                               
 end as DRIVER_SUS_REVOKED_PP_DESC,                              
                           
 @PHY_MENTL_CHALLENGED as PHY_MENTL_CHALLENGED,                          
 case @PHY_MENTL_CHALLENGED                           
 when '1' then @PHY_MENTL_CHALLENGED_PP_DESC                           
 end as PHY_MENTL_CHALLENGED_MC_DESC ,                          
                           
 @ANY_FINANCIAL_RESPONSIBILITY as ANY_FINANCIAL_RESPONSIBILITY,                          
 case @ANY_FINANCIAL_RESPONSIBILITY                          
 when '1' then  @ANY_FINANCIAL_RESPONSIBILITY_PP_DESC                           
 end as ANY_FINANCIAL_RESPONSIBILITY_PP_DESC ,                    
                           
 @MULTI_POLICY_DISC_APPLIED as MULTI_POLICY_DISC_APPLIED,                          
 case @MULTI_POLICY_DISC_APPLIED                           
 when '1' then @MULTI_POLICY_DISC_APPLIED_PP_DESC                          
 end as MULTI_POLICY_DISC_APPLIED_PP_DESC,                          
                           
 case @IS_OTHER_THAN_INSURED                           
 when '1' then @FULLNAME                            
 end as FULLNAME,                          
                           
 case @IS_OTHER_THAN_INSURED                           
 when '1' then  @DATE_OF_BIRTH                            
 end as DATE_OF_BIRTH,                          
 @IS_EXTENDED_FORKS as IS_EXTENDED_FORKS,                          
 @IS_COMMERCIAL_USE as IS_COMMERCIAL_USE,                          
 @ANY_NON_OWNED_VEH as ANY_NON_OWNED_VEH,                          
 @IS_USEDFOR_RACING as IS_USEDFOR_RACING,                          
 @IS_TAKEN_OUT as IS_TAKEN_OUT,                          
 @IS_MORE_WHEELS as IS_MORE_WHEELS,                         
-- @DRIVER_SUS_REVOKED as DRIVER_SUS_REVOKED,                          
 @IS_CONVICTED_CARELESS_DRIVE as IS_CONVICTED_CARELESS_DRIVE,                          
 @IS_COST_OVER_DEFINED_LIMIT as IS_COST_OVER_DEFINED_LIMIT,                                    
 @EXISTING_DMG as EXISTING_DMG,                                    
-- @ANY_FINANCIAL_RESPONSIBILITY as ANY_FINANCIAL_RESPONSIBILITY,                                    
 @COVERAGE_DECLINED as COVERAGE_DECLINED,                          
 @SALVAGE_TITLE as SALVAGE_TITLE,                                    
 @IS_LICENSED_FOR_ROAD as IS_LICENSED_FOR_ROAD ,             @IS_MODIFIED_INCREASE_SPEED as IS_MODIFIED_INCREASE_SPEED,                      
 @IS_MODIFIED_KIT as IS_MODIFIED_KIT,                      
 @IS_MODIFIED_KIT_INCSPEED as IS_MODIFIED_KIT_INCSPEED,        
 @IS_RECORD_EXISTS as IS_RECORD_EXISTS ,  
   
 @ANY_PRIOR_LOSSES as ANY_PRIOR_LOSSES,    
 case @ANY_PRIOR_LOSSES     
 when 'Y' then @ANY_PRIOR_LOSSES_DESC    
 end as ANY_PRIOR_LOSSES_DESC, 

 @IS_CONVICTED_ACCIDENT as IS_CONVICTED_ACCIDENT,  
 @PRIOR_LOSS_INFO_EXISTS as PRIOR_LOSS_INFO_EXISTS,  
 @CURR_RES_TYPE as CURR_RES_TYPE           ,

--30 jan 2008
 case @APPLY_PERS_UMB_POL 
 when 'Y' then @APPLY_PERS_UMB_POL_DESC
 end as APPLY_PERS_UMB_POL_DESC 
          
                          
end      


                             
                              
  
  
  

GO

