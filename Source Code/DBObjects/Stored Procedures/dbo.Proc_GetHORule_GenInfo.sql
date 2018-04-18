IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHORule_GenInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHORule_GenInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------                                                                                                                                                                      
Proc Name                : Dbo.Proc_GetHORule_GenInfo 935,141,1,'d'                                                                                                                                                        
Created by               : Ashwani                                                                                                                                                                      
Date                     : 01 Dec.,2005                                                                                                                      
Purpose                  : To get the General Info for HO rules                                                                                                                      
Revison History          :                                                                                                                                                                      
Used In                  : Wolverine                                                                                                                                                                      
------------------------------------------------------------                                                                                                                                                                      
Date     Review By          Comments                                                                                                                                                                      
------   ------------       -------------------------*/                
-- DROP PROC dbo.Proc_GetHORule_GenInfo 1692,76,1,''               
CREATE proc dbo.Proc_GetHORule_GenInfo                                                                                             
(                                                                                                                                                
@CUSTOMERID    int,                                                                                                                                                
@APPID    int,                                                                                                                                                
@APPVERSIONID   int,                                                                                                
@DESC varchar(10)                                                                                                                                  
)                                                                                                                                                
AS                                                                                                                                                    
BEGIN                                                                                                   
  -- Mandatory                                                                                                
  declare @IS_VACENT_OCCUPY nchar(1)                                                                                               
  declare @DESC_VACENT_OCCUPY nvarchar(150)                                                    
  declare @IS_RENTED_IN_PART nchar(1)                                                                                                
  declare @DESC_RENTED_IN_PART nvarchar(150)                                                  
  declare @IS_DWELLING_OWNED_BY_OTHER nchar(1)                                                            
  declare @DESC_DWELLING_OWNED_BY_OTHER nvarchar(150)      
  declare @ANY_FARMING_BUSINESS_COND nchar(1)                        
  declare @DESC_FARMING_BUSINESS_COND nvarchar(150)       
  declare @OF_ACRES_FIELD int           
  declare @FARMING_FIELD nchar(1)                                                             
  declare @IS_PROP_NEXT_COMMERICAL nchar(1)                                                                                              
  declare @DESC_PROPERTY nvarchar(150)                                                       
  declare @ARE_STAIRWAYS_PRESENT nchar(2)                                                                                              
  declare @DESC_STAIRWAYS nvarchar(150)           
  declare @INTANIMALS_EXO_PETS_HISTORY int                                 
  declare @ANIMALS_EXO_PETS_HISTORY char                                                   
  declare @BREED nvarchar(100)                                                               
  declare @INTNO_OF_PETS int                                                                                               
  declare @NO_OF_PETS char                                                                                              
  declare @OTHER_DESCRIPTION nvarchar(100)                           
  declare @IS_SWIMPOLL_HOTTUB char(1) --NA                                                     
  declare @HAS_INSU_TRANSFERED_AGENCY nchar(1)                                                                                             
  declare @DESC_INSU_TRANSFERED_AGENCY nvarchar(150)       
  declare @IS_OWNERS_DWELLING_CHANGED nchar(1)                                    
  declare @DESC_OWNER nvarchar(150)                                                      
  declare @ANY_COV_DECLINED_CANCELED nchar(1)                                      
  declare @DESC_COV_DECLINED_CANCELED nvarchar(150)                                                                           
                                                                                                
  declare @CONVICTION_DEGREE_IN_PAST nchar(1)                                                                                                
  declare @DESC_CONVICTION_DEGREE_IN_PAST nvarchar(150)                                                                                                
                                                                                               
  declare @LEAD_PAINT_HAZARD nchar(2)                                                                                               
  declare @DESC_LEAD_PAINT_HAZARD nvarchar(150)                                                                                  
                                                                                                
  declare @MULTI_POLICY_DISC_APPLIED nchar(2) --na                                            
  declare @DESC_MULTI_POLICY_DISC_APPLIED nvarchar(150)                                               
                      
  declare @ANY_RESIDENCE_EMPLOYEE nchar(1)               
  declare @DESC_RESIDENCE_EMPLOYEE nvarchar(150)                                
                                                                                      
  declare @ANY_OTHER_RESI_OWNED nchar(1)                                                                                               
  declare @DESC_OTHER_RESIDENCE nvarchar(150)                                                                                         
                                               
  declare @ANY_OTH_INSU_COMP nchar(10)                                                                                               
  declare @DESC_OTHER_INSURANCE nvarchar(150)                                                                                                
                                                                       
  declare @ANY_RENOVATION nchar(1)                         
  declare @DESC_RENOVATION nvarchar(150)            
                                                                                               
  declare @TRAMPOLINE nchar(2)                                                                                             
  declare @DESC_TRAMPOLINE nvarchar(150)                              
                                                                                               
  declare @RENTERS nchar(2)                                                                                               
 -- declare @INTDESC_RENTERS int                                                                    
  declare @DESC_RENTERS nvarchar(150)                                                                    
                                                                                                
  declare @ANY_HEATING_SOURCE nchar(1)                                                                                                
  declare @DESC_ANY_HEATING_SOURCE nvarchar(150)                                                                                               
                                                                                                
 declare @BUILD_UNDER_CON_GEN_CONT nchar(1) --na                                                                                                 
 declare @NON_SMOKER_CREDIT nchar(1)  --NA                                                                                                
 declare @SWIMMING_POOL nchar(1)                                                                                                
 declare @DESC_BUSINESS nvarchar(150)                                                             
 --Home care                                                       
 declare @PROVIDE_HOME_DAY_CARE nvarchar(2)        
 --Modular Home : 19 June                                                    
 declare @MODULAR_MANUFACTURED_HOME char                                                    
 declare @INTBUILT_ON_CONTINUOUS_FOUNDATION int                                                    
 declare @BUILT_ON_CONTINUOUS_FOUNDATION char       
 --Valued Customer : 7 July 2006                                        
 declare @VALUED_CUSTOMER_DISCOUNT_OVERRIDE nchar(1)                                       
 declare @VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC nvarchar(150)                        
 --                                            
 declare @IS_RECORD_EXISTS char                                                                 
 --                                                                 
 declare @ANY_FORMING char                                                                
 declare @PREMISES varchar(15)                  
 declare @OF_ACRES varchar(30)             
 declare @ISANY_HORSE char                                                  
 -- declare @OF_ACRES_P varchar(30)                                            
 declare @NO_HORSES varchar(15)                                           
 declare @FLOCATION nvarchar(50) --  # of Locations*                                                                 
 declare @DESC_LOCATION nvarchar(500)  --Description of Locations(Max 255 chars)*          
 declare @ANY_PRIOR_LOSSES NVARCHAR(5)        
 declare @ANY_PRIOR_LOSSES_DESC VARCHAR(50)                                                              
        declare @BOAT_WITH_HOMEOWNER   VARCHAR(50)                                                                        
IF EXISTS (SELECT CUSTOMER_ID FROM APP_HOME_OWNER_GEN_INFO                                                                 
WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID)                                                       
BEGIN                                                                                 
 SET @IS_RECORD_EXISTS='N'                                                                                              
 SELECT       
  @IS_VACENT_OCCUPY=isnull(IS_VACENT_OCCUPY,''), @DESC_VACENT_OCCUPY=isnull(DESC_VACENT_OCCUPY,''),                                                                                                
  @IS_RENTED_IN_PART=isnull(IS_RENTED_IN_PART,''),@DESC_RENTED_IN_PART=isnull(DESC_RENTED_IN_PART,''),                  
  @IS_DWELLING_OWNED_BY_OTHER=isnull(IS_DWELLING_OWNED_BY_OTHER,''),@DESC_DWELLING_OWNED_BY_OTHER =isnull(DESC_DWELLING_OWNED_BY_OTHER,''),                                                                               
  @ANY_FARMING_BUSINESS_COND=isnull(ANY_FARMING_BUSINESS_COND,''),@DESC_FARMING_BUSINESS_COND=isnull(DESC_FARMING_BUSINESS_COND,''),                                                                
  @OF_ACRES_FIELD=ISNULL(OF_ACRES,0),                                                                                         
  @IS_PROP_NEXT_COMMERICAL=isnull(IS_PROP_NEXT_COMMERICAL,''),@DESC_PROPERTY=isnull(DESC_PROPERTY,''),                                                                                                
  @ARE_STAIRWAYS_PRESENT=isnull(ARE_STAIRWAYS_PRESENT,''),@DESC_STAIRWAYS=isnull(DESC_STAIRWAYS,''),                                                                 
  @INTANIMALS_EXO_PETS_HISTORY=isnull(ANIMALS_EXO_PETS_HISTORY,-1),@BREED=isnull(BREED,''),                                                                                                
  @INTNO_OF_PETS=isnull(NO_OF_PETS,-1),@OTHER_DESCRIPTION=isnull(OTHER_DESCRIPTION,''),                                                                                                
  @IS_SWIMPOLL_HOTTUB=isnull(IS_SWIMPOLL_HOTTUB,''),                                                                                                
  @HAS_INSU_TRANSFERED_AGENCY=isnull(HAS_INSU_TRANSFERED_AGENCY,''),@DESC_INSU_TRANSFERED_AGENCY=isnull(DESC_INSU_TRANSFERED_AGENCY,''),                                                                                                
  @IS_OWNERS_DWELLING_CHANGED=isnull(IS_OWNERS_DWELLING_CHANGED,''),@DESC_OWNER=isnull(DESC_OWNER,''),                                                                                                
  @ANY_COV_DECLINED_CANCELED=isnull(ANY_COV_DECLINED_CANCELED,''),@DESC_COV_DECLINED_CANCELED=isnull(DESC_COV_DECLINED_CANCELED,''),                                                                                        
  @CONVICTION_DEGREE_IN_PAST=isnull(CONVICTION_DEGREE_IN_PAST,''),@DESC_CONVICTION_DEGREE_IN_PAST=isnull(DESC_CONVICTION_DEGREE_IN_PAST,''),                                                                                                
  @LEAD_PAINT_HAZARD=isnull(LEAD_PAINT_HAZARD,''),@DESC_LEAD_PAINT_HAZARD=isnull(DESC_LEAD_PAINT_HAZARD,''),                                                                  
  @MULTI_POLICY_DISC_APPLIED=isnull(MULTI_POLICY_DISC_APPLIED,''),@DESC_MULTI_POLICY_DISC_APPLIED=isnull(DESC_MULTI_POLICY_DISC_APPLIED,''),                                         
  @ANY_RESIDENCE_EMPLOYEE=isnull(ANY_RESIDENCE_EMPLOYEE,''),@DESC_RESIDENCE_EMPLOYEE=isnull(DESC_RESIDENCE_EMPLOYEE,''),                 
  @ANY_OTHER_RESI_OWNED=isnull(ANY_OTHER_RESI_OWNED,''),@DESC_OTHER_RESIDENCE=isnull(DESC_OTHER_RESIDENCE,''),                                                                                                
  @ANY_OTH_INSU_COMP=isnull(ANY_OTH_INSU_COMP,''),@DESC_OTHER_INSURANCE=isnull(DESC_OTHER_INSURANCE,''),                                                                                                
  @ANY_RENOVATION=isnull(ANY_RENOVATION,''),@DESC_RENOVATION=isnull(DESC_RENOVATION,''),                  
  @TRAMPOLINE=isnull(TRAMPOLINE,''),@DESC_TRAMPOLINE=isnull(DESC_TRAMPOLINE,''),                                                                                
  @RENTERS=isnull(RENTERS,''),@DESC_RENTERS=isnull(DESC_RENTERS,''),                                                                                                
  @ANY_HEATING_SOURCE=isnull(ANY_HEATING_SOURCE,''),@DESC_ANY_HEATING_SOURCE=isnull(DESC_ANY_HEATING_SOURCE,''),                     
  @BUILD_UNDER_CON_GEN_CONT=isnull(BUILD_UNDER_CON_GEN_CONT,''),                                                                                                
  @NON_SMOKER_CREDIT=isnull(NON_SMOKER_CREDIT,''),                                                           
  @SWIMMING_POOL=isnull(SWIMMING_POOL,''),                                                                
  @DESC_BUSINESS=isnull(DESC_BUSINESS,''),                               
  @ANY_FORMING=isnull(ANY_FORMING,''),                                                    
  @PREMISES=isnull(convert(varchar(15),PREMISES),''),                                                                
  @OF_ACRES=isnull(convert(varchar(30),OF_ACRES),''),                                                                
  @ISANY_HORSE=isnull(ISANY_HORSE,''),                                                                
  --@OF_ACRES_P=isnull(convert(varchar(30),OF_ACRES_P),''),                                                                
  @NO_HORSES=isnull(convert(varchar(15),NO_HORSES),''),                                                                                   
  @FLOCATION=isnull(LOCATION,''),                                                                
  @DESC_LOCATION=isnull(DESC_LOCATION,''),                                                      
  @PROVIDE_HOME_DAY_CARE =isnull(PROVIDE_HOME_DAY_CARE,''),       
  @MODULAR_MANUFACTURED_HOME = isnull(MODULAR_MANUFACTURED_HOME,''),                                                    
  @INTBUILT_ON_CONTINUOUS_FOUNDATION = isnull(BUILT_ON_CONTINUOUS_FOUNDATION,0),       
 @VALUED_CUSTOMER_DISCOUNT_OVERRIDE = isnull(VALUED_CUSTOMER_DISCOUNT_OVERRIDE,''),                                        
 @VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC = isnull(VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC,'') ,        
 @ANY_PRIOR_LOSSES=ISNULL(ANY_PRIOR_LOSSES,''),        
 @ANY_PRIOR_LOSSES_DESC=ISNULL(ANY_PRIOR_LOSSES_DESC,'') ,      
 @BOAT_WITH_HOMEOWNER =ISNULL(BOAT_WITH_HOMEOWNER,'2')       
 FROM APP_HOME_OWNER_GEN_INFO                                                                                                
 WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID       
                                                                                              
 END                                                                                                
ELSE                                                                                                
BEGIN                                                                                                 
-- Mandatory                       
  set @IS_VACENT_OCCUPY =''   set @DESC_VACENT_OCCUPY =''                                                              
  set @IS_RENTED_IN_PART ='' set @DESC_RENTED_IN_PART =''                                                                                                 
  set @IS_DWELLING_OWNED_BY_OTHER =''  set @DESC_DWELLING_OWNED_BY_OTHER =''                                                    
  set @ANY_FARMING_BUSINESS_COND='' set @DESC_FARMING_BUSINESS_COND=''        ---Modified Desc                                                                                       
  set @IS_PROP_NEXT_COMMERICAL =''  set @DESC_PROPERTY =''                                                                                                 
  set @ARE_STAIRWAYS_PRESENT =''   set @DESC_STAIRWAYS =''                                                                                       
  set @INTANIMALS_EXO_PETS_HISTORY =-1   set @BREED =''                                                                                                
  set @ANIMALS_EXO_PETS_HISTORY =''                                                                      
  set @INTNO_OF_PETS =-1      set @OTHER_DESCRIPTION =''                                                                                                
  set @NO_OF_PETS=''                                      
  set @IS_SWIMPOLL_HOTTUB =''  --NA                                                               
  set @HAS_INSU_TRANSFERED_AGENCY =''   set @DESC_INSU_TRANSFERED_AGENCY =''                                                                                                 
  set @IS_OWNERS_DWELLING_CHANGED =''     set @DESC_OWNER =''                          
  set @ANY_COV_DECLINED_CANCELED =''  set @DESC_COV_DECLINED_CANCELED =''                                                              
  set @CONVICTION_DEGREE_IN_PAST =''   set @DESC_CONVICTION_DEGREE_IN_PAST =''                                    
  set @LEAD_PAINT_HAZARD =''   set @DESC_LEAD_PAINT_HAZARD =''                                                                                                 
  set @MULTI_POLICY_DISC_APPLIED ='' set @DESC_MULTI_POLICY_DISC_APPLIED=''                                            
  set @ANY_RESIDENCE_EMPLOYEE =''   set @DESC_RESIDENCE_EMPLOYEE =''                                                                                                 
  set @ANY_OTHER_RESI_OWNED =''  set @DESC_OTHER_RESIDENCE =''                                                                                                 
  set @ANY_OTH_INSU_COMP =''   set @DESC_OTHER_INSURANCE =''                                                                                                 
  set @ANY_RENOVATION =''   set @DESC_RENOVATION =''                                                                                                 
  set @TRAMPOLINE =''    set @DESC_TRAMPOLINE =''                                                                                                 
  set @RENTERS =''    set @DESC_RENTERS =''                                                                                                  
  set @ANY_HEATING_SOURCE =''    set @DESC_ANY_HEATING_SOURCE =''                                                                                                 
  set @BUILD_UNDER_CON_GEN_CONT =''  --na                                                                                    
  set @NON_SMOKER_CREDIT='' --NA                                                                                                
  set @SWIMMING_POOL =''                                                                                                 
  set @DESC_BUSINESS =''                                                                               
  set @IS_RECORD_EXISTS='Y'                                                                
  set @ANY_FORMING =''                                                                
  set @PREMISES =''                                                                
  set @OF_ACRES =''                                                                
  set @ISANY_HORSE =''                                   
  --set @OF_ACRES_P =''                                       
  set @NO_HORSES =''                                                                
  set @DESC_LOCATION=''                            
  set @FLOCATION=''                                     
  set @PROVIDE_HOME_DAY_CARE=''               
  set @MODULAR_MANUFACTURED_HOME=''                                        
  set @BUILT_ON_CONTINUOUS_FOUNDATION =''                                        
  set @VALUED_CUSTOMER_DISCOUNT_OVERRIDE=''                                        
  set @VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC=''                
END                                                                                                 
                               
--HO-2 , HO-3 & HO-5  if no of renters are > 2                        
 DECLARE @POLICY_TYPE INT                                                                                                                                   
 SELECT @POLICY_TYPE=POLICY_TYPE FROM APP_LIST WITH(NOLOCK)                                       
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID        
 --if(@POLICY_TYPE=11402 or @POLICY_TYPE=11400 or @POLICY_TYPE=11401 or @POLICY_TYPE=11192 or @POLICY_TYPE=11148 or @POLICY_TYPE=11149)                                                               
 --begin                                                                                             
  IF(@DESC_RENTERS>2)             
 BEGIN                                                                       
 SET @DESC_RENTERS='Y'                                                   
 END                                                                      
 --end            
        
------Home Underwriting Question        
--RULE If # of Horses Field is greater than 3 then refer        
IF(@NO_HORSES > 3)        
BEGIN        
SET @NO_HORSES ='Y'        
END                                                          
-----------------------------------------------------------------------------------------           --                                                                                            
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
                                                                                            
------------------------------------------------------------------------------------------        
DECLARE @FUEL_ID smallint        
SELECT @FUEL_ID=FUEL_ID FROM  APP_HOME_OWNER_SOLID_FUEL WITH(NOLOCK)                   
WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID AND IS_ACTIVE='Y'                                              
 IF(@ANY_HEATING_SOURCE='1' and (@FUEL_ID IS NULL OR @FUEL_ID=0))                                                                
 BEGIN                                                                                             
 SET @ANY_HEATING_SOURCE ='Y'                                                                                            
 END                                                                                      
 ELSE IF(@ANY_HEATING_SOURCE='')                                                                                            
 BEGIN                                                                                             
 SET @ANY_HEATING_SOURCE =''                                                                     
 END                                                                                             
 ELSE IF(@ANY_HEATING_SOURCE='0')                                                                                            
 BEGIN                                                                                             
 SET @ANY_HEATING_SOURCE ='N'                                                                       
 END                                                                                  
--                                                                 
 IF(@CONVICTION_DEGREE_IN_PAST='1')                                 
 BEGIN                                                                                             
 SET @CONVICTION_DEGREE_IN_PAST ='Y'                                                                                            
 END                                                                       
 ELSE IF(@CONVICTION_DEGREE_IN_PAST='')                                                                                            
 BEGIN        
 SET @CONVICTION_DEGREE_IN_PAST =''                  
 END                                                   
 ELSE IF(@CONVICTION_DEGREE_IN_PAST='0')                                                                                            
 BEGIN                                        
 SET @CONVICTION_DEGREE_IN_PAST ='N'                                                                       
 END                                                                                             
                              
 DECLARE @STATE_ID INT                                                                                     
 SELECT @STATE_ID=STATE_ID FROM APP_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID                                                     
                                                                                            
-- only for Michingan                                                                                              
IF(@STATE_ID=22)                                                        
BEGIN                                                                                     
 IF(@INTANIMALS_EXO_PETS_HISTORY=1)                                                                                            
 BEGIN                                                            
 SET @ANIMALS_EXO_PETS_HISTORY ='Y'                                                         
 END                                                                                             
 ELSE IF(@INTANIMALS_EXO_PETS_HISTORY=-1)                                                                                           
 BEGIN                                                                                             
 SET @ANIMALS_EXO_PETS_HISTORY =''                                                                                             
 END                                                                                             
 ELSE IF(@INTANIMALS_EXO_PETS_HISTORY=0)                                                                       
 BEGIN                                                                             
 SET @ANIMALS_EXO_PETS_HISTORY ='N'                                                   
 END                                                                                  
END                                                                                  
ELSE                                                                                   
BEGIN                                                              
 IF(@INTANIMALS_EXO_PETS_HISTORY=-1)                                                                                            
 BEGIN                                                                                             
 SET @ANIMALS_EXO_PETS_HISTORY =''                                                                                             
 END                                                                                   
 ELSE                                                                                       
 BEGIN                                                                                             
 SET @ANIMALS_EXO_PETS_HISTORY ='N'                                                                                             
 END                                                      
 END                                  
                                                                                   
-- If Is there an enclosed swimming pool or hot tub ? is Yes than ,application should get rejected                                                                              
 IF(@IS_SWIMPOLL_HOTTUB='1')                                                                                            
 BEGIN                       
 SET @IS_SWIMPOLL_HOTTUB ='Y'                                                                                            
 END                                                                                             
 ELSE IF(@IS_SWIMPOLL_HOTTUB='')                                               
 BEGIN                                                                                             
 SET @IS_SWIMPOLL_HOTTUB =''                                         
 END                            
 ELSE IF(@IS_SWIMPOLL_HOTTUB='0')                                               
 BEGIN                                                                                     
 SET @IS_SWIMPOLL_HOTTUB ='N'                                                                                             
 END                   
                                                                     
--If Is dwelling currently vacant or unoccupied ? is Yes and Location Is 'Seasonal' than   Risk Decline                             
DECLARE @LOC_TYPE int                             
SELECT @LOC_TYPE=LOCATION_TYPE FROM APP_LOCATIONS   WITH(NOLOCK)                          
WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID                            
                            
IF(@IS_VACENT_OCCUPY='1')                            
BEGIN                            
 IF(@LOC_TYPE!=11814)                            
 BEGIN                                                                                            
 SET @IS_VACENT_OCCUPY='Y'                                                                            
 END                            
 ELSE             
 BEGIN                                                                                            
 SET @IS_VACENT_OCCUPY='N'                                                                            
 END                            
END                             
ELSE IF(@IS_VACENT_OCCUPY='0')                                                                                                                        
 BEGIN                                            
 SET @IS_VACENT_OCCUPY='N'                                       
 END                                                  
--                    
                                                      
IF(@IS_RENTED_IN_PART='1')                                                                         
BEGIN                                                                                                                         
SET @IS_RENTED_IN_PART='Y'                                                                                                         
END                                                                                                              
ELSE IF(@IS_RENTED_IN_PART='0')                                                                                                                        
BEGIN                                                                               
SET @IS_RENTED_IN_PART='N'                                                                                                         
END                                                                                                
--                                                                                              
IF(@IS_DWELLING_OWNED_BY_OTHER='1')                                                                                                    BEGIN                                                                                                                  
       
SET @IS_DWELLING_OWNED_BY_OTHER='Y'                                  
END                                                                                                              
ELSE IF(@IS_DWELLING_OWNED_BY_OTHER='0')                                                                                                                        
BEGIN                                                                                    
SET @IS_DWELLING_OWNED_BY_OTHER='N'                    
END                                                                                               
--                   
--"Any business conducted on Premises  - If YEs then Reject --If No check the following:              
--If yes to - Provide Home Day Care for Monetary or other compensation?* then Reject               
--If no to - Provide Home Day Care for Monetary or other compensation?* then Refer"          
          
IF(@ANY_FARMING_BUSINESS_COND='1' OR  @ANY_FARMING_BUSINESS_COND='Y')                                 
BEGIN                                              
 IF(@PROVIDE_HOME_DAY_CARE='1'OR @PROVIDE_HOME_DAY_CARE='Y')                                                      
  BEGIN                                                      
  --SET @ANY_FARMING_BUSINESS_COND='Y'  --Reject  --Commented by Charles on 22-Sep-09 for Itrack 6440      
  SET @ANY_FARMING_BUSINESS_COND='N' --Added by Charles on 22-Sep-09 for Itrack 6440        
  SET @PROVIDE_HOME_DAY_CARE='Y'  --Changed from 'N' by Charles on 22-Sep-09 for Itrack 6440                                                                                                      
  END             
 ELSE             
  BEGIN                                                      
  SET @PROVIDE_HOME_DAY_CARE='N'  --Changed from 'Y' by Charles on 22-Sep-09 for Itrack 6440           
  SET @ANY_FARMING_BUSINESS_COND='N'                                                                                                       
  END             
END            
ELSE          
BEGIN           
SET @ANY_FARMING_BUSINESS_COND='N'          
SET @PROVIDE_HOME_DAY_CARE='N'          
END           
          
----ACRES                                                          
IF (@OF_ACRES_FIELD > 500)                                                          
BEGIN                                                          
SET @FARMING_FIELD='Y'                                
END                                                          
ELSE                                                          
BEGIN                                                          
SET @FARMING_FIELD='N'                                                          
END                                                          
---                                                                                            
--                    
IF(@IS_PROP_NEXT_COMMERICAL='1')                                                                                                            
BEGIN             
SET @IS_PROP_NEXT_COMMERICAL='Y'                                                          
END                       
ELSE IF(@IS_PROP_NEXT_COMMERICAL='0')                                                                    
BEGIN                                                                                                                         
SET @IS_PROP_NEXT_COMMERICAL='N'                                                                                        
END                                                                                               
--                                                                                     
IF(@ARE_STAIRWAYS_PRESENT='1')                                                                     
BEGIN                                                                              
SET @ARE_STAIRWAYS_PRESENT='Y'                                                                                                         
END                                                                                                              
ELSE IF(@ARE_STAIRWAYS_PRESENT='0')                                                                                                                        
BEGIN                                                                                
SET @ARE_STAIRWAYS_PRESENT='N'                                                                                                                        
END                         
                                                                                     
--                                                                                              
IF(@INTNO_OF_PETS=-1)                                                                                              
BEGIN                             
SET  @NO_OF_PETS=''                                                                                              
END                                                                                               
ELSE                                                                                           
BEGIN                                                                                               
SET  @NO_OF_PETS='N'                                                                    
END                                 
----------------------------------------------------------------------------------------                
-- FOR MIXED BREED =11657 REF IN CASE OF MICHIGAN                                               
 DECLARE @MI_MIXED_BREED CHAR                                                                  
                                                                  
IF(@STATE_ID=22)                                                                  
BEGIN                                                                   
 IF(@OTHER_DESCRIPTION='11657')                                                                  
 BEGIN                                                                   
 SET @MI_MIXED_BREED='Y'                                                                  
 END                                                                   
 ELSE                                                                  
 BEGIN                                                                
 SET @MI_MIXED_BREED='N'                                                                  
 END                                                                   
END                               
ELSE                                                         
BEGIN                                                       
SET @MI_MIXED_BREED='N'                                                                  
END                       
----------------------------------------------------------------------------------       
 DECLARE @IN_MIXED_BREED CHAR                                     
IF(@STATE_ID=14)                                                                  
BEGIN                                                                   
 IF(@OTHER_DESCRIPTION='11657')                                                                  
 BEGIN                                                                   
 SET @IN_MIXED_BREED='Y'                                                                  
 END                                                                   
 ELSE                                                      
 BEGIN                                                                   
 SET @IN_MIXED_BREED='N'                                                                  
 END                                                           
END                                                                   
ELSE                                                                  
BEGIN                                                     
SET @IN_MIXED_BREED='N'                                                                  
END                                                                    
--@IN_REJ_MIXED_BREED                                                                  
 DECLARE @IN_REJ_MIXED_BREED CHAR                                                                  
                                                                  
IF(@STATE_ID=14)                                                                  
BEGIN                                                                   
  IF(@OTHER_DESCRIPTION IS NOT NULL AND @OTHER_DESCRIPTION <> '' AND @INTNO_OF_PETS > 0 )                                                        
  BEGIN                                                                   
 IF(@OTHER_DESCRIPTION !='11617' AND @OTHER_DESCRIPTION<>'11657' )                                                                  
 BEGIN                                              
 SET @IN_REJ_MIXED_BREED='Y'                                                                  
 END                                                                   
 ELSE                                                                  
 BEGIN                                                                   
 SET @IN_REJ_MIXED_BREED='N'                                                                  
 END                                                           
 END                                                                   
 ELSE                                                           
 BEGIN                                                          
 SET @IN_REJ_MIXED_BREED='N'                                                                  
 END                                                       
END                                                           
ELSE                                                                  
BEGIN                                                                   
SET @IN_REJ_MIXED_BREED='N'                                                                  
END                                                         
                                                    
--------------IS THIS A MODULAR/MANUFACTURED HOME? * FIELD                                                     
--IF YES -CHECK THE FOLLOWING FIELDS                                                      
--BUILT ON A CONTINUOUS FOUNDATION IF NO - refer  
  
 SET @BUILT_ON_CONTINUOUS_FOUNDATION ='N'                                                    
  
--IF(@POLICY_TYPE!=11409 AND @POLICY_TYPE!=11410) --Not applicable for Premier Policies --ITRACK 6679, CHARLES, 26-NOV-09  
--BEGIN                                                     
IF(@MODULAR_MANUFACTURED_HOME = '1' AND @INTBUILT_ON_CONTINUOUS_FOUNDATION = 0)                                                    
 BEGIN                                                
 SET @BUILT_ON_CONTINUOUS_FOUNDATION ='Y'                                                    
 END               
--END      
  
--Added by Charles on 26-Nov-09 for Itrack 6679  
DECLARE @BUILT_ON_CONTINUOUS_FOUNDATION_PREMIER CHAR  
 SET @BUILT_ON_CONTINUOUS_FOUNDATION_PREMIER ='N'                                                    
--  
--IF(@POLICY_TYPE = 11409 OR @POLICY_TYPE = 11410) --Premier Policies   
--BEGIN  
--IF(@MODULAR_MANUFACTURED_HOME = '1')                                                    
-- BEGIN                                                
-- SET @BUILT_ON_CONTINUOUS_FOUNDATION_PREMIER ='Y'                                                    
-- END   
--END                                  
--Added till here  
                      
-------------------------------------                
                                        
--MODULAR HOME                                         
IF(@MODULAR_MANUFACTURED_HOME='1')                                                                                                                 
BEGIN                                                                         
SET @MODULAR_MANUFACTURED_HOME='Y'                                          
END                                                                                 
ELSE IF(@MODULAR_MANUFACTURED_HOME='0')                                                                                                                        
BEGIN                                                                     
SET @MODULAR_MANUFACTURED_HOME='N'                                                                                                                        
END                                     
                                  
                                   
                                        
---Valued Customer                                        
IF(@VALUED_CUSTOMER_DISCOUNT_OVERRIDE='1')                                                                                                                 
BEGIN                                                                              
SET @VALUED_CUSTOMER_DISCOUNT_OVERRIDE='Y'                                                                                                         
END                                                                                                              
ELSE IF(@VALUED_CUSTOMER_DISCOUNT_OVERRIDE='0')                                                                                                            
BEGIN                                                                                
SET @VALUED_CUSTOMER_DISCOUNT_OVERRIDE='N'               
END               
                                        
                                        
                                        
-----"HOMEOWNERS UNDERWRITING QUESTION ANY SUPPLEMENTAL HEAT SOURCE*?         
--IF YES - THEN SURCHARGE APPLIES AND THE USER MUST COMPLETE THE SOLID FUEL DETAILS TABS         
 --IF NO SOLID FUEL DETAILS COMPLETED RISK IS SUBMIT": 19 JUNE 2006         
                                                  
Declare @SUPP_HEATING_SOURCE char                                                  
IF  (NOT EXISTS (SELECT FUEL_ID FROM APP_HOME_OWNER_SOLID_FUEL WITH(NOLOCK)                                                     
WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND IS_ACTIVE='Y') AND @ANY_HEATING_SOURCE='Y')                                                  
 BEGIN                                                    
 SET @SUPP_HEATING_SOURCE='Y'                                  
 END                                                  
ELSE                                                  
 BEGIN                                                  
 SET @SUPP_HEATING_SOURCE='N'                                                  
 END            
        
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
ELSE IF(@ANY_PRIOR_LOSSES='')                                        
BEGIN                                         
SET @ANY_PRIOR_LOSSES=''                                        
END              
            
DECLARE @PRIOR_LOSS_INFO_EXISTS CHAR            
SET @PRIOR_LOSS_INFO_EXISTS='P'            
            
IF EXISTS ( SELECT CUSTOMER_ID FROM APP_PRIOR_LOSS_INFO WHERE CUSTOMER_ID=@CUSTOMERID AND LOB=1)        
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
/*      
IF "Do you want to add a boat with this Homeowner policy"? is 'Yes' and No boat added with policy , reject it.      
*/      
IF(@BOAT_WITH_HOMEOWNER='1')                                        
 BEGIN                                         
 SET @BOAT_WITH_HOMEOWNER='Y'                                        
 END                              
ELSE IF(@BOAT_WITH_HOMEOWNER='0')                                        
 BEGIN                                         
 SET @BOAT_WITH_HOMEOWNER='N'                                        
 END           
ELSE IF(@BOAT_WITH_HOMEOWNER='')                                        
 BEGIN                                         
 SET @BOAT_WITH_HOMEOWNER=''                                        
 END       
DECLARE @BOAT_ID INT,      
 @BOAT_WITH_HOMEOWNER_POLICY CHAR      
SET @BOAT_WITH_HOMEOWNER_POLICY='N'       
      
      
 SELECT @BOAT_ID=ISNULL(BOAT_ID,0) FROM APP_WATERCRAFT_INFO WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID       
 AND APP_VERSION_ID = @APPVERSIONID  AND IS_ACTIVE='Y'      
       
 IF(@BOAT_WITH_HOMEOWNER='Y')      
 BEGIN      
  IF (@BOAT_ID IS NULL OR @BOAT_ID=0)       
   BEGIN       
   SET @BOAT_WITH_HOMEOWNER_POLICY='Y'       
   END        
 END       
 ELSE IF(@BOAT_WITH_HOMEOWNER='N')       
  IF (@BOAT_ID IS NOT NULL OR @BOAT_ID!=0)      
   BEGIN      
   SET @BOAT_WITH_HOMEOWNER_POLICY='Y'      
   END      
      
      
------------------------------------------------------------------------------------          
                                         
SELECT                                                                                                
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
 case @DESC_FARMING_BUSINESS_COND                                              
 when 'Y' then @DESC_FARMING_BUSINESS_COND                                              
 end as DESC_FARMING_BUSINESS_COND,                                       
                                                                                              
 @IS_PROP_NEXT_COMMERICAL as IS_PROP_NEXT_COMMERICAL,                                                                                                
 case @IS_PROP_NEXT_COMMERICAL                                                                                        
 when 'Y'then  @DESC_PROPERTY                                                                                       
 end as DESC_PROPERTY,                                                                                            
                                                                            
 @ARE_STAIRWAYS_PRESENT as ARE_STAIRWAYS_PRESENT,                                                                                                
 case @IS_PROP_NEXT_COMMERICAL                                                                                              
 when 'Y'then  @DESC_STAIRWAYS                                                                                             
 end as DESC_STAIRWAYS,                                                                                              
                      
 @ANIMALS_EXO_PETS_HISTORY as  ANIMALS_EXO_PETS_HISTORY,                                                                                 
 case @IS_PROP_NEXT_COMMERICAL                    
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
                                                                                     
 @MULTI_POLICY_DISC_APPLIED as  MULTI_POLICY_DISC_APPLIED, --na                                             
 case @MULTI_POLICY_DISC_APPLIED                                                    
 when '1' then @DESC_MULTI_POLICY_DISC_APPLIED          
 end as DESC_MULTI_POLICY_DISC_APPLIED,                                            
                                                                                  
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
 when '1' then @DESC_RENTERS                                                                                               
 end as DESC_RENTERS,                                                               
             
 @ANY_HEATING_SOURCE as ANY_HEATING_SOURCE,                                                                                                
 case @ANY_HEATING_SOURCE                                                                                               
 when 'Y' then @DESC_ANY_HEATING_SOURCE                                                                              
 end as DESC_ANY_HEATING_SOURCE,                                                                                               
 @BUILD_UNDER_CON_GEN_CONT as BUILD_UNDER_CON_GEN_CONT, --na                                                                                                 
 --@NON_SMOKER_CREDIT as NON_SMOKER_CREDIT, --NA                                                                                                
 @SWIMMING_POOL as SWIMMING_POOL ,                                                             
 --@DESC_BUSINESS as DESC_BUSINESS                                                     
  @MI_MIXED_BREED as MI_MIXED_BREED,               
  @IN_MIXED_BREED as IN_MIXED_BREED,                                                                  
  @IN_REJ_MIXED_BREED as IN_REJ_MIXED_BREED,                                                                  
  @IS_RECORD_EXISTS as   IS_RECORD_EXISTS,                                                                
                                                                 
  @ANY_FORMING as ANY_FORMING,                                                             
 case @ANY_FORMING when '1'                                                                
  then @PREMISES end as PREMISES,                                                                
  case @ANY_FORMING when '1'                                                                
  then @OF_ACRES end as OF_ACRES,                              
  case @ANY_FORMING when '1'                                                                
  then @DESC_LOCATION end as DESC_LOCATION,                              
                                                                  
 -- 11558, 11559                                                                
  case  when @PREMISES=11558 or @PREMISES=11559                                                                
  then @FLOCATION end as FLOCATION,            
  case  when @PREMISES=11558 or @PREMISES=11559                                                                
  then @DESC_LOCATION end as DESC_LOCATION,                               
  case  when @PREMISES=11558 or @PREMISES=11559 -----add a new field at 4th dec                                    
  then @OF_ACRES end as OF_ACRES,                                                                
 --                                                                 
  case when @PREMISES=11557                                                                 
  then @ISANY_HORSE end as ISANY_HORSE,                                                                
 --  case when @PREMISES=11557                                          
 --  then @OF_ACRES_P end  as OF_ACRES_P,                                                 
 --                                                                
  case when @ISANY_HORSE='1'                                                                 
  then @NO_HORSES end as NO_HORSES,                                              
 --                                                                
 @FARMING_FIELD as FARMING_FIELD ,                    
 @PROVIDE_HOME_DAY_CARE as PROVIDE_HOME_DAY_CARE,                                                    
 --                                                    
 @BUILT_ON_CONTINUOUS_FOUNDATION as BUILT_ON_CONTINUOUS_FOUNDATION,                                                  
 @SUPP_HEATING_SOURCE as SUPP_HEATING_SOURCE,                                        
 --                                        
 @MODULAR_MANUFACTURED_HOME as MODULAR_MANUFACTURED_HOME,                                        
                                         
 ------                                        
 @VALUED_CUSTOMER_DISCOUNT_OVERRIDE as VALUED_CUSTOMER_DISCOUNT_OVERRIDE,                                                                                                
 case @VALUED_CUSTOMER_DISCOUNT_OVERRIDE                                                  
 when 'Y' then @VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC                                                                              
 end as VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC,        
         
 @ANY_PRIOR_LOSSES AS ANY_PRIOR_LOSSES,         
 CASE @ANY_PRIOR_LOSSES               
 WHEN 'Y' THEN @ANY_PRIOR_LOSSES_DESC              
 END AS ANY_PRIOR_LOSSES_DESC ,        
 @PRIOR_LOSS_INFO_EXISTS AS PRIOR_LOSS_INFO_EXISTS ,      
 @BOAT_WITH_HOMEOWNER_POLICY AS BOAT_WITH_HOMEOWNER_POLICY,      
 @BOAT_WITH_HOMEOWNER as BOAT_WITH_HOMEOWNER,  
 @BUILT_ON_CONTINUOUS_FOUNDATION_PREMIER AS BUILT_ON_CONTINUOUS_FOUNDATION_PREMIER --Added by Charles on 26-Nov-09 for Itrack 6679       
      
END                                                                                                   
 /*set @ANY_FORMING =''-- Any Farming?*                                                                 
 set @PREMISES ='' --Farming Details*                                                                
 set @FLocation ='' --  # of Locations*                                                                 
 set @DESC_Location=''  --Description of Locations(Max 255 chars)*                           
 set @OF_ACRES ='' --# of Farming acres*                                                                
 set @ISANY_HORSE ='' --Any Horses?*                                                            
 set @OF_ACRES_P ='' --# of incidental income acres*                                                                 
 set @NO_HORSES =''  --# of Horses* */         
      
      
      
      
      
      
       
GO

