IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRDRule_GenInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRDRule_GenInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* ----------------------------------------------------------                                                                                              
Proc Name                : Dbo.Proc_GetRDRule_GenInfo  935,280,1,'d'                                                                                          
Created by               : Ashwani                                                                                              
Date                     : 12 Dec.,2005                                              
Purpose                  : To get the Underwriting Info for RD rules                                              
Revison History          :                                                                                              
Used In                  : Wolverine                                                                                              
------------------------------------------------------------                                                                                              
Date     Review By          Comments                                                                                              
------   ------------       -------------------------*/    
--drop proc dbo.Proc_GetRDRule_GenInfo                                                                                            
CREATE proc dbo.Proc_GetRDRule_GenInfo                                              
(                                                                                              
@CUSTOMERID    int,                                                                                              
@APPID    int,                                                                                              
@APPVERSIONID   int,                                              
@DESC varchar(10)                                                                                
)                                                                                              
as                                                                                                  
begin                                                 
 -- Mandatory                                              
 declare @IS_VACENT_OCCUPY nchar(1)                                             
 declare @DESC_VACENT_OCCUPY nvarchar(150)                                              
                                            
 declare @IS_RENTED_IN_PART nchar(1)                                              
 declare @DESC_RENTED_IN_PART nvarchar(150)                                              
                                            
 declare @IS_DWELLING_OWNED_BY_OTHER nchar(1)                                           
 declare @DESC_DWELLING_OWNED_BY_OTHER nvarchar(150)                                              
                                            
 declare @ANY_FARMING_BUSINESS_COND nchar(1)    --NA                                              
                                            
 declare @IS_PROP_NEXT_COMMERICAL nchar(1)                                              
 declare @DESC_PROPERTY nvarchar(150)                                              
                                            
 declare @ARE_STAIRWAYS_PRESENT nchar(1)                                            
 declare @DESC_STAIRWAYS nvarchar(150)                                              
                                            
 declare @INTANIMALS_EXO_PETS_HISTORY int                                            
 declare @ANIMALS_EXO_PETS_HISTORY nchar(1)                                            
 declare @BREED nvarchar(100)                                              
                                            
 declare @INTNO_OF_PETS int                                             
 declare @NO_OF_PETS char                                            
 declare @OTHER_DESCRIPTION nvarchar(100)                                              
                   
 declare @IS_SWIMPOLL_HOTTUB char --NA                                              
                   
 declare @HAS_INSU_TRANSFERED_AGENCY nchar(1)               
 declare @DESC_INSU_TRANSFERED_AGENCY nvarchar(150)                                              
                                    
 declare @IS_OWNERS_DWELLING_CHANGED nchar(1)                     
 declare @DESC_OWNER nvarchar(150)                                              
                        
 declare @ANY_COV_DECLINED_CANCELED nchar(1)                                               
 declare @DESC_COV_DECLINED_CANCELED nvarchar(150)                                             
                                             
 declare @CONVICTION_DEGREE_IN_PAST nchar(1)                                              
 declare @DESC_CONVICTION_DEGREE_IN_PAST nvarchar(150)                                              
                                            
 declare @LEAD_PAINT_HAZARD nchar(1)                             
 declare @DESC_LEAD_PAINT_HAZARD nvarchar(150)                                             
                                             
 declare @MULTI_POLICY_DISC_APPLIED nchar(1) --na    
 declare @DESC_MULTI_POLICY_DISC_APPLIED nvarchar(150)                                           
                         
 declare @ANY_RESIDENCE_EMPLOYEE nchar(1)                                              
 declare @DESC_RESIDENCE_EMPLOYEE nvarchar(150)                            
                                            
 declare @ANY_OTHER_RESI_OWNED nchar(1)                                               
 declare @DESC_OTHER_RESIDENCE nvarchar(150)                                              
                                            
 declare @ANY_OTH_INSU_COMP nchar(1)                                             
 declare @DESC_OTHER_INSURANCE nvarchar(150)                                              
                                            
 declare @ANY_RENOVATION nchar(1)                                               
 declare @DESC_RENOVATION nvarchar(150)                                              
                                            
 declare @TRAMPOLINE nchar(1)                                              
 declare @DESC_TRAMPOLINE nvarchar(150)                                              
                                            
 declare @RENTERS nchar(1)                                               
  declare @DESC_RENTERS nvarchar(150)                                             
                                             
 declare @ANY_HEATING_SOURCE nchar(1)                                               
 declare @DESC_ANY_HEATING_SOURCE nvarchar(150)      
      
--PROPERTY_USED_WHOLE_PART,PROPERTY_USED_WHOLE_PART_DESC      
 DECLARE @PROPERTY_USED_WHOLE_PART NCHAR(1)      
 DECLARE @PROPERTY_USED_WHOLE_PART_DESC NVARCHAR(150)      
      
 declare @DWELLING_MOBILE_HOME NCHAR(1)      
 declare @DWELLING_MOBILE_HOME_DESC NVARCHAR(150)      
      
 declare @MODULAR_MANUFACTURED_HOME nchar(1)      
 declare @BUILT_ON_CONTINUOUS_FOUNDATION  nchar(1)      
    
 declare @PROPERTY_ON_MORE_THAN nchar(1)    
 declare @PROPERTY_ON_MORE_THAN_DESC NVARCHAR(150)    
    
declare @VALUED_CUSTOMER_DISCOUNT_OVERRIDE nchar(1)            
declare @VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC nvarchar(150)           
declare @ANY_PRIOR_LOSSES NVARCHAR(5)  
declare @ANY_PRIOR_LOSSES_DESC VARCHAR(50)         
 --                                     
 declare @BUILD_UNDER_CON_GEN_CONT nchar(1)    --na                                               
 declare @NON_SMOKER_CREDIT nchar(1)  --NA                                              
 declare @SWIMMING_POOL nchar(1)                                              
 declare @DESC_BUSINESS nvarchar(150)   
 declare @IS_RECORD_EXISTS char                                          
                                               
                                               
 if exists (select CUSTOMER_ID from APP_HOME_OWNER_GEN_INFO                                  
  where CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID)                                              
 begin                                 
 set @IS_RECORD_EXISTS='N'             
  select @IS_VACENT_OCCUPY=isnull(IS_VACENT_OCCUPY,''), @DESC_VACENT_OCCUPY=isnull(DESC_VACENT_OCCUPY,''),                                              
 @IS_RENTED_IN_PART=isnull(IS_RENTED_IN_PART,''),@DESC_RENTED_IN_PART=isnull(DESC_RENTED_IN_PART,''),                                              
 @IS_DWELLING_OWNED_BY_OTHER=isnull(IS_DWELLING_OWNED_BY_OTHER,''),@DESC_DWELLING_OWNED_BY_OTHER =isnull(DESC_DWELLING_OWNED_BY_OTHER,''),                                              
 @ANY_FARMING_BUSINESS_COND=isnull(ANY_FARMING_BUSINESS_COND,''),                                              
 @IS_PROP_NEXT_COMMERICAL=isnull(IS_PROP_NEXT_COMMERICAL,''),@DESC_PROPERTY=isnull(DESC_PROPERTY,''),                                              
 @ARE_STAIRWAYS_PRESENT=isnull(ARE_STAIRWAYS_PRESENT,''),@DESC_STAIRWAYS=isnull(DESC_STAIRWAYS,''),                                              
 @INTANIMALS_EXO_PETS_HISTORY=isnull(ANIMALS_EXO_PETS_HISTORY,'-1'), @MULTI_POLICY_DISC_APPLIED=              
 isnull(MULTI_POLICY_DISC_APPLIED,''),@DESC_MULTI_POLICY_DISC_APPLIED=isnull(DESC_MULTI_POLICY_DISC_APPLIED,''),              
 @BREED=isnull(BREED,''),                                              
 @INTNO_OF_PETS=isnull(NO_OF_PETS,-1),@OTHER_DESCRIPTION=isnull(OTHER_DESCRIPTION,''),                                              
 @IS_SWIMPOLL_HOTTUB=isnull(IS_SWIMPOLL_HOTTUB,''),                                              
 @HAS_INSU_TRANSFERED_AGENCY=isnull(HAS_INSU_TRANSFERED_AGENCY,''),@DESC_INSU_TRANSFERED_AGENCY=isnull(DESC_INSU_TRANSFERED_AGENCY,''),                                              
 @IS_OWNERS_DWELLING_CHANGED=isnull(IS_OWNERS_DWELLING_CHANGED,''),@DESC_OWNER=isnull(DESC_OWNER,''),                                         
 @ANY_COV_DECLINED_CANCELED=isnull(ANY_COV_DECLINED_CANCELED,''),@DESC_COV_DECLINED_CANCELED=isnull(DESC_COV_DECLINED_CANCELED,''),                                              
 @CONVICTION_DEGREE_IN_PAST=isnull(CONVICTION_DEGREE_IN_PAST,''),@DESC_CONVICTION_DEGREE_IN_PAST=isnull(DESC_CONVICTION_DEGREE_IN_PAST,''),                                              
 @LEAD_PAINT_HAZARD=isnull(LEAD_PAINT_HAZARD,''),@DESC_LEAD_PAINT_HAZARD=isnull(DESC_LEAD_PAINT_HAZARD,''),                                  
 @ANY_RESIDENCE_EMPLOYEE=isnull(ANY_RESIDENCE_EMPLOYEE,'N'),@DESC_RESIDENCE_EMPLOYEE=isnull(DESC_RESIDENCE_EMPLOYEE,''),                                              
 @ANY_OTHER_RESI_OWNED=isnull(ANY_OTHER_RESI_OWNED,'N'),@DESC_OTHER_RESIDENCE=isnull(DESC_OTHER_RESIDENCE,''),                                              
 @ANY_OTH_INSU_COMP=isnull(ANY_OTH_INSU_COMP,''),@DESC_OTHER_INSURANCE=isnull(DESC_OTHER_INSURANCE,''),                                              
 @ANY_RENOVATION=isnull(ANY_RENOVATION,''),@DESC_RENOVATION=isnull(DESC_RENOVATION,''),                                              
 @TRAMPOLINE=isnull(TRAMPOLINE,''),@DESC_TRAMPOLINE=isnull(DESC_TRAMPOLINE,''),                                              
 @RENTERS=isnull(RENTERS,''),@DESC_RENTERS=isnull(DESC_RENTERS,''),                                              
        @ANY_HEATING_SOURCE=isnull(ANY_HEATING_SOURCE,''),@DESC_ANY_HEATING_SOURCE=isnull(DESC_ANY_HEATING_SOURCE,''),                                              
 @BUILD_UNDER_CON_GEN_CONT=isnull(BUILD_UNDER_CON_GEN_CONT,''),                                              
 @NON_SMOKER_CREDIT=isnull(NON_SMOKER_CREDIT,''),@SWIMMING_POOL=isnull(SWIMMING_POOL,''),@DESC_BUSINESS=isnull(DESC_BUSINESS,''),      
 @PROPERTY_USED_WHOLE_PART=isnull(PROPERTY_USED_WHOLE_PART,''),@PROPERTY_USED_WHOLE_PART_DESC=isnull(PROPERTY_USED_WHOLE_PART_DESC,''),                                       
 @DWELLING_MOBILE_HOME=isnull(DWELLING_MOBILE_HOME,''),@DWELLING_MOBILE_HOME_DESC=isnull(DWELLING_MOBILE_HOME_DESC,''),      
 @MODULAR_MANUFACTURED_HOME=isnull(MODULAR_MANUFACTURED_HOME,''),@BUILT_ON_CONTINUOUS_FOUNDATION=isnull(BUILT_ON_CONTINUOUS_FOUNDATION,''),    
 @PROPERTY_ON_MORE_THAN=isnull(PROPERTY_ON_MORE_THAN,''),@PROPERTY_ON_MORE_THAN_DESC=isnull(PROPERTY_ON_MORE_THAN_DESC,''),    
 @MODULAR_MANUFACTURED_HOME=isnull(MODULAR_MANUFACTURED_HOME,''),    
    
 @VALUED_CUSTOMER_DISCOUNT_OVERRIDE = isnull(VALUED_CUSTOMER_DISCOUNT_OVERRIDE,''),            
 @VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC = isnull(VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC,''),  
 @ANY_PRIOR_LOSSES=ISNULL(ANY_PRIOR_LOSSES,''),  
 @ANY_PRIOR_LOSSES_DESC=ISNULL(ANY_PRIOR_LOSSES_DESC,'')     
    
 from APP_HOME_OWNER_GEN_INFO                                              
 where CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID                                              
end                                              
else                                              
begin                                               
-- Mandatory                                              
 set @IS_VACENT_OCCUPY =''   set @DESC_VACENT_OCCUPY =''                                               
 set @IS_RENTED_IN_PART ='' set @DESC_RENTED_IN_PART =''                                               
 set @IS_DWELLING_OWNED_BY_OTHER =''  set @DESC_DWELLING_OWNED_BY_OTHER =''                                            
 set @ANY_FARMING_BUSINESS_COND='' --na                                              
 set @IS_PROP_NEXT_COMMERICAL =''  set @DESC_PROPERTY =''                                               
 set @ARE_STAIRWAYS_PRESENT =''   set @DESC_STAIRWAYS =''                                               
 set @ANIMALS_EXO_PETS_HISTORY =''   set @BREED =''                                              
 set @INTANIMALS_EXO_PETS_HISTORY ='-1'                                
 set @INTNO_OF_PETS =-1      set @OTHER_DESCRIPTION =''                                              
 set @NO_OF_PETS=''                                              
 set @IS_SWIMPOLL_HOTTUB =''  --NA                                              
 set @HAS_INSU_TRANSFERED_AGENCY =''   set @DESC_INSU_TRANSFERED_AGENCY =''                                               
 set @IS_OWNERS_DWELLING_CHANGED =''     set @DESC_OWNER =''                                               
 set @ANY_COV_DECLINED_CANCELED =''  set @DESC_COV_DECLINED_CANCELED =''                         
 set @CONVICTION_DEGREE_IN_PAST =''   set @DESC_CONVICTION_DEGREE_IN_PAST =''                                               
 set @LEAD_PAINT_HAZARD =''   set @DESC_LEAD_PAINT_HAZARD =''                                               
 set @ANY_RESIDENCE_EMPLOYEE ='N'   set @DESC_RESIDENCE_EMPLOYEE =''                                               
 set @ANY_OTHER_RESI_OWNED ='N'  set @DESC_OTHER_RESIDENCE =''                                               
 set @ANY_OTH_INSU_COMP =''   set @DESC_OTHER_INSURANCE =''                   
 set @ANY_RENOVATION =''   set @DESC_RENOVATION =''                                               
 set @TRAMPOLINE =''    set @DESC_TRAMPOLINE =''                                               
 set @RENTERS =''    set @DESC_RENTERS =''                                               
 set @ANY_HEATING_SOURCE =''    set @DESC_ANY_HEATING_SOURCE =''                                               
 set @BUILD_UNDER_CON_GEN_CONT =''  --na                                               
 set @NON_SMOKER_CREDIT=''   --NA                                              
 set @SWIMMING_POOL =''                                               
 set @DESC_BUSINESS =''               
 set @MULTI_POLICY_DISC_APPLIED=''   
 set @DESC_MULTI_POLICY_DISC_APPLIED=''                      
 set @IS_RECORD_EXISTS ='Y'       
 --      
 set @PROPERTY_USED_WHOLE_PART=''  set @PROPERTY_USED_WHOLE_PART_DESC=''       
 set @DWELLING_MOBILE_HOME='' set @DWELLING_MOBILE_HOME_DESC=''      
 --Mod home      
 set @MODULAR_MANUFACTURED_HOME='' set @BUILT_ON_CONTINUOUS_FOUNDATION=''    
 set @PROPERTY_ON_MORE_THAN  ='' set @PROPERTY_ON_MORE_THAN_DESC=''    
 --Valued Home    
set @VALUED_CUSTOMER_DISCOUNT_OVERRIDE=''            
set @VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC='' 
SET @ANY_PRIOR_LOSSES=''    
     
      
end                                               
                                      
--              
              
              
--                                          
 if(@IS_SWIMPOLL_HOTTUB='1')                                  
 begin                                           
 set @IS_SWIMPOLL_HOTTUB ='Y'                                          
 end                   
 else if(@IS_SWIMPOLL_HOTTUB='')                                          
  begin                                           
  set @IS_SWIMPOLL_HOTTUB =''                                           
 end                                           
   else if(@IS_SWIMPOLL_HOTTUB='0')                                          
  begin                                           
  set @IS_SWIMPOLL_HOTTUB ='N'                                           
 end                                            
--                                          
 if(@ANY_COV_DECLINED_CANCELED='1')                                          
 begin                                           
 set @ANY_COV_DECLINED_CANCELED ='Y'                                          
 end                                           
 else if(@ANY_COV_DECLINED_CANCELED='')                                          
  begin                                           
  set @ANY_COV_DECLINED_CANCELED =''                                           
 end                                           
        else if(@ANY_COV_DECLINED_CANCELED='0')                                          
  begin                         
  set @ANY_COV_DECLINED_CANCELED ='N'                                         
 end                                            
                                          
--                                           
  if(@ANY_HEATING_SOURCE='1')                                          
 begin                                           
 set @ANY_HEATING_SOURCE ='Y'                                          
 end                                           
 else if(@ANY_HEATING_SOURCE='')                                          
  begin                                           
  set @ANY_HEATING_SOURCE =''                                           
 end                                           
   else if(@ANY_HEATING_SOURCE='0')                                          
  begin                                           
  set @ANY_HEATING_SOURCE ='N'                                           
 end                                           
--                                           
 if(@CONVICTION_DEGREE_IN_PAST='1')                                          
 begin                                           
 set @CONVICTION_DEGREE_IN_PAST ='Y'                                          
 end                                           
 else if(@CONVICTION_DEGREE_IN_PAST='')                                          
  begin                                           
  set @CONVICTION_DEGREE_IN_PAST =''                                           
 end                                           
     else if(@CONVICTION_DEGREE_IN_PAST='0')                                          
  begin                                           
  set @CONVICTION_DEGREE_IN_PAST ='N'        
 end                                           
                                          
--                                            
 if(@INTANIMALS_EXO_PETS_HISTORY='1')                                          
 begin                            
  set @ANIMALS_EXO_PETS_HISTORY ='Y'                                          
 end                                           
 else if(@INTANIMALS_EXO_PETS_HISTORY='-1')                                          
 begin                       
  set @ANIMALS_EXO_PETS_HISTORY =''                                           
 end                
 else if(@INTANIMALS_EXO_PETS_HISTORY='0')                                        
 begin                                           
  set @ANIMALS_EXO_PETS_HISTORY ='N'                                           
 end                                           
--                                            
if(@IS_VACENT_OCCUPY='1')                                                               
begin                                                                       
set @IS_VACENT_OCCUPY='Y'                                                       
end                  
else if(@IS_VACENT_OCCUPY='0')                                                
begin                                                                       
set @IS_VACENT_OCCUPY='N'                                                                      
end                                                                 
--                                            
-- Only for Michigan states                                  
 declare @STATE_ID int                                     
 select @STATE_ID=STATE_ID from APP_LIST  where CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID                                                
----------------------------------                                  
if(@STATE_ID=22 or @STATE_ID=14 )              
begin               
/* if(@MULTI_POLICY_DISC_APPLIED='' or @MULTI_POLICY_DISC_APPLIED is null)              
 begin               
  set @MULTI_POLICY_DISC_APPLIED=''              
 end               
 else            
 begin             
  set @MULTI_POLICY_DISC_APPLIED='0'            
 end            
end               
else            
begin             
 set @MULTI_POLICY_DISC_APPLIED='0' */       
if(@MULTI_POLICY_DISC_APPLIED='1')                                                               
begin                                                                       
set @MULTI_POLICY_DISC_APPLIED='Y'                                                       
end                  
else if(@MULTI_POLICY_DISC_APPLIED='0')                                                
begin                                                                       
set @MULTI_POLICY_DISC_APPLIED='N'                                                                      
end     
      
end            
--------------------------------------              
 IF(@STATE_ID=22 OR @STATE_ID=14)                                  
 BEGIN                                   
 IF(@IS_RENTED_IN_PART='1')                                                               
  BEGIN                                                                       
   SET @IS_RENTED_IN_PART='Y'                             
  END                                                            
 ELSE IF(@IS_RENTED_IN_PART='0')                                                                      
  BEGIN                                                                       
   SET @IS_RENTED_IN_PART='N'                                                                      
  END                                              
 END                                   
 ELSE                                 
 BEGIN                                   
 IF(@IS_RENTED_IN_PART='')                       
  BEGIN                    
   SET @IS_RENTED_IN_PART=''                                                       
  END                                                            
 ELSE                                   
  BEGIN                                                                       
   SET @IS_RENTED_IN_PART='N'                                                                      
  END                                              
 END                                   
                                  
--                                            
IF(@IS_DWELLING_OWNED_BY_OTHER='1')                                                               
 BEGIN                                  
  SET @IS_DWELLING_OWNED_BY_OTHER='Y'                                                       
 END                                
ELSE IF(@IS_DWELLING_OWNED_BY_OTHER='0')                                                                      
 BEGIN                                                                       
  SET @IS_DWELLING_OWNED_BY_OTHER='N'                                                                      
 END                                             
--                                
                                
if(@RENTERS='1')                                                      
begin                                                                       
set @RENTERS='Y'                                                   
end                                                            
else if(@RENTERS='0')                                                                      
begin                                                                       
set @RENTERS='N'                                                                      
end                                 
--                                            
if(@ANY_FARMING_BUSINESS_COND='1')                                                               
begin                                             
set @ANY_FARMING_BUSINESS_COND='Y'                                                       
end                                            
else if(@ANY_FARMING_BUSINESS_COND='0')                                                                      
begin                                                                       
set @ANY_FARMING_BUSINESS_COND='N'                                                                      
end                                             
--                                       
if(@IS_PROP_NEXT_COMMERICAL='1')                                                               
begin                                                                       
set @IS_PROP_NEXT_COMMERICAL='Y'                                                       
end                          
else if(@IS_PROP_NEXT_COMMERICAL='0')                                                                      
begin                                                                       
set @IS_PROP_NEXT_COMMERICAL='N'                                                                  
end                                             
--                                            
if(@ARE_STAIRWAYS_PRESENT='1')                                                               
begin                                                                       
set @ARE_STAIRWAYS_PRESENT='Y'                                                       
end                                            
else if(@ARE_STAIRWAYS_PRESENT='0')                                                                      
begin                                                                       
set @ARE_STAIRWAYS_PRESENT='N'                                                                      
end                                             
                                          
--                         
IF(@INTNO_OF_PETS=-1)                                          
 BEGIN              
  SET  @NO_OF_PETS=''                                            
 END                                             
ELSE                                         
 BEGIN                                             
  SET  @NO_OF_PETS='N'                                              
 END        
-----      
--                                            
if(@PROPERTY_USED_WHOLE_PART='1')                                                               
begin                                                                       
set @PROPERTY_USED_WHOLE_PART='Y'                                                       
end                                                           
else if(@PROPERTY_USED_WHOLE_PART='0')                                                                      
begin                                                                       
set @PROPERTY_USED_WHOLE_PART='N'                                                                      
end       
      
if(@DWELLING_MOBILE_HOME='1')                                                               
begin                                                                       
set @DWELLING_MOBILE_HOME='Y'                                                       
end                                                            
else if(@DWELLING_MOBILE_HOME='0')                                                                      
begin                                                                       
set @DWELLING_MOBILE_HOME='N'                                                                      
end     
---    
if(@PROPERTY_ON_MORE_THAN='1')                                                               
begin                                                                       
set @PROPERTY_ON_MORE_THAN='Y'                                                       
end                                                            
else if(@PROPERTY_ON_MORE_THAN='0')                                                                      
begin                                                                       
set @PROPERTY_ON_MORE_THAN='N'                                                                      
end     
      
-------------Modular/Manufactured Home---------------      
if(@MODULAR_MANUFACTURED_HOME='1')                                                               
begin                                                                       
set @MODULAR_MANUFACTURED_HOME='Y'                                                       
end                                                            
else if(@MODULAR_MANUFACTURED_HOME='0')                                                                      
begin                                                                       
set @MODULAR_MANUFACTURED_HOME='N'                                                                      
end       
      
if(@BUILT_ON_CONTINUOUS_FOUNDATION='1')                                                               
begin                                                                       
set @BUILT_ON_CONTINUOUS_FOUNDATION='Y'                                                       
end                                                            
else if(@BUILT_ON_CONTINUOUS_FOUNDATION='0')                                                                      
begin                                                                       
set @BUILT_ON_CONTINUOUS_FOUNDATION='N'                                                                      
end       
      
Declare @MODULAR_MANU_HOME char      
if(@MODULAR_MANUFACTURED_HOME='Y')      
begin      
 if(@BUILT_ON_CONTINUOUS_FOUNDATION='N')       
        begin      
  set @MODULAR_MANU_HOME='Y' --Risk decline      
        end      
        else      
        begin       
  set @MODULAR_MANU_HOME='N'      
        end       
end      
else      
begin      
 set @MODULAR_MANU_HOME='N'      
end      
      
/*Underwriting question:      
"Any prior losses?", if not saved, means it is blank or null in database, prompt for answering to this question.      
If Yes to "Any prior losses?", then look at prior losses for Auto LOB.If there is none then refer to underwriter      
 If No, and there are prior losses refer UWR. */    
IF(@ANY_PRIOR_LOSSES='1')                                  
	BEGIN                                   
		SET @ANY_PRIOR_LOSSES='Y'                                  
	END                        
ELSE IF(@ANY_PRIOR_LOSSES='0')                                  
	BEGIN                                   
		SET @ANY_PRIOR_LOSSES='N'                                  
	END 
ELSE IF (@ANY_PRIOR_LOSSES='')
        BEGIN                                   
		SET @ANY_PRIOR_LOSSES=''                                  
	END         
  
DECLARE @PRIOR_LOSS_INFO_EXISTS CHAR      
SET @PRIOR_LOSS_INFO_EXISTS='P'      
      
IF EXISTS ( SELECT CUSTOMER_ID FROM APP_PRIOR_LOSS_INFO WHERE CUSTOMER_ID=@CUSTOMERID AND LOB=6)      
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
----------------------------------------------------                                           
                                            
                                            
select                                              
-- Mandatory                                          
@IS_VACENT_OCCUPY as IS_VACENT_OCCUPY,                                             
case @IS_VACENT_OCCUPY                                                
when 'Y'then  @DESC_VACENT_OCCUPY                                                 
end as DESC_VACENT_OCCUPY,                                                
                                            
@IS_RENTED_IN_PART as IS_RENTED_IN_PART,                                            
case @IS_RENTED_IN_PART                          
when 'Y'then  @DESC_RENTED_IN_PART                                                 
end as DESC_RENTED_IN_PART,                  
                                            
@IS_DWELLING_OWNED_BY_OTHER as IS_DWELLING_OWNED_BY_OTHER,                                              
case @IS_DWELLING_OWNED_BY_OTHER                                                
when 'Y'then  @DESC_DWELLING_OWNED_BY_OTHER                                                 
end as DESC_DWELLING_OWNED_BY_OTHER,                                                 
                                            
@ANY_FARMING_BUSINESS_COND as  ANY_FARMING_BUSINESS_COND, --na                                               
                                            
@IS_PROP_NEXT_COMMERICAL as IS_PROP_NEXT_COMMERICAL,                                              
case @IS_PROP_NEXT_COMMERICAL                                            
when 'Y'then  @DESC_PROPERTY                                                 
end as DESC_PROPERTY,                                                 
                           
@ARE_STAIRWAYS_PRESENT as ARE_STAIRWAYS_PRESENT,                                              
case @ARE_STAIRWAYS_PRESENT                                            
when 'Y'then  @DESC_STAIRWAYS                                                 
end as DESC_STAIRWAYS,                                            
                                            
@ANIMALS_EXO_PETS_HISTORY as  ANIMALS_EXO_PETS_HISTORY,               
case @ANIMALS_EXO_PETS_HISTORY                                            
when 'Y'then  @BREED         
end as BREED,                                            
                                
@NO_OF_PETS as NO_OF_PETS,                                             
                                        
case                                             
when @INTNO_OF_PETS > 0 then @OTHER_DESCRIPTION                                              
end as OTHER_DESCRIPTION,                                            
                                            
@IS_SWIMPOLL_HOTTUB as  IS_SWIMPOLL_HOTTUB, --NA                                              
                                            
@HAS_INSU_TRANSFERED_AGENCY as HAS_INSU_TRANSFERED_AGENCY,                                              
case @HAS_INSU_TRANSFERED_AGENCY                                            
when '1' then @DESC_INSU_TRANSFERED_AGENCY                                            
end  as DESC_INSU_TRANSFERED_AGENCY,                                              
                                            
@IS_OWNERS_DWELLING_CHANGED as IS_OWNERS_DWELLING_CHANGED,                    
case @IS_OWNERS_DWELLING_CHANGED                                             
when '1' then @DESC_OWNER                                             
end as DESC_OWNER,                                              
                                            
                                            
@ANY_COV_DECLINED_CANCELED as ANY_COV_DECLINED_CANCELED,                                            
case @ANY_COV_DECLINED_CANCELED                                             
when 'Y' then @DESC_COV_DECLINED_CANCELED                                             
end as DESC_COV_DECLINED_CANCELED,                                              
                                            
@CONVICTION_DEGREE_IN_PAST as CONVICTION_DEGREE_IN_PAST,                                              
case @CONVICTION_DEGREE_IN_PAST                                             
when 'Y' then @DESC_CONVICTION_DEGREE_IN_PAST                                             
end as DESC_CONVICTION_DEGREE_IN_PAST,                                              
                                            
@LEAD_PAINT_HAZARD as LEAD_PAINT_HAZARD,                                    
case @LEAD_PAINT_HAZARD          
when '1' then @DESC_LEAD_PAINT_HAZARD                            
end as DESC_LEAD_PAINT_HAZARD,                                      
                                            
              
                                         
@ANY_RESIDENCE_EMPLOYEE as  ANY_RESIDENCE_EMPLOYEE,                                              
case @ANY_RESIDENCE_EMPLOYEE                                             
when '1' then @DESC_RESIDENCE_EMPLOYEE                 
end as DESC_RESIDENCE_EMPLOYEE,                                              
                                            
@ANY_OTHER_RESI_OWNED as ANY_OTHER_RESI_OWNED,                                                
case @ANY_OTHER_RESI_OWNED                                             
when '1' then @DESC_OTHER_RESIDENCE                                             
end as DESC_OTHER_RESIDENCE,                                              
                                            
@ANY_OTH_INSU_COMP as ANY_OTH_INSU_COMP,                                              
case @ANY_OTH_INSU_COMP                                             
when '1' then @DESC_OTHER_INSURANCE                                             
end as DESC_OTHER_INSURANCE,                                              
                                            
@ANY_RENOVATION as ANY_RENOVATION,                     
case @ANY_RENOVATION                                
when '1' then @DESC_RENOVATION                                             
end as DESC_RENOVATION,                                             
              
@TRAMPOLINE as TRAMPOLINE,                          
case @TRAMPOLINE             
when '1' then @DESC_TRAMPOLINE                                             
end as DESC_TRAMPOLINE,                                             
                                            
@RENTERS as RENTERS,                                             
case @RENTERS                                             
when 'Y' then @DESC_RENTERS                                             
end as DESC_RENTERS,                                             
                                            
@ANY_HEATING_SOURCE as ANY_HEATING_SOURCE,                            
case @ANY_HEATING_SOURCE                                             
when 'Y' then @DESC_ANY_HEATING_SOURCE                                             
end as DESC_ANY_HEATING_SOURCE,      
      
---      
@PROPERTY_USED_WHOLE_PART as PROPERTY_USED_WHOLE_PART,                            
case @PROPERTY_USED_WHOLE_PART      
when 'Y' then @PROPERTY_USED_WHOLE_PART_DESC      
end as PROPERTY_USED_WHOLE_PART_DESC,      
      
      
@DWELLING_MOBILE_HOME as DWELLING_MOBILE_HOME,                            
case @DWELLING_MOBILE_HOME    
when 'Y' then @DWELLING_MOBILE_HOME_DESC      
end as DWELLING_MOBILE_HOME_DESC,      
    
@MODULAR_MANUFACTURED_HOME as MODULAR_MANUFACTURED_HOME ,--Mandatory     
@MODULAR_MANU_HOME as MODULAR_MANU_HOME,--Rule    
    
--    
      
@PROPERTY_ON_MORE_THAN as PROPERTY_ON_MORE_THAN,                            
case @PROPERTY_ON_MORE_THAN      
when 'Y' then @PROPERTY_ON_MORE_THAN_DESC      
end as PROPERTY_ON_MORE_THAN_DESC,     
    
    
------            
@VALUED_CUSTOMER_DISCOUNT_OVERRIDE as VALUED_CUSTOMER_DISCOUNT_OVERRIDE,                                                                    
case @VALUED_CUSTOMER_DISCOUNT_OVERRIDE                      
when 'Y' then @VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC                                                  
end as VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC,      
---                                             
                                            
@BUILD_UNDER_CON_GEN_CONT as BUILD_UNDER_CON_GEN_CONT, --na                                               
--@NON_SMOKER_CREDIT as NON_SMOKER_CREDIT, --NA                                              
@SWIMMING_POOL as SWIMMING_POOL,                                            
--@DESC_BUSINESS as DESC_BUSINESS               
@MULTI_POLICY_DISC_APPLIED as MULTI_POLICY_DISC_APPLIED ,  
case @MULTI_POLICY_DISC_APPLIED                      
when 'Y' then @DESC_MULTI_POLICY_DISC_APPLIED                                                  
end as DESC_MULTI_POLICY_DISC_APPLIED,   
                                            
@IS_RECORD_EXISTS as IS_RECORD_EXISTS ,  
  
@ANY_PRIOR_LOSSES AS ANY_PRIOR_LOSSES,   
CASE @ANY_PRIOR_LOSSES         
WHEN 'Y' THEN @ANY_PRIOR_LOSSES_DESC        
END AS ANY_PRIOR_LOSSES_DESC ,  
@PRIOR_LOSS_INFO_EXISTS AS PRIOR_LOSS_INFO_EXISTS                         
end 






GO

