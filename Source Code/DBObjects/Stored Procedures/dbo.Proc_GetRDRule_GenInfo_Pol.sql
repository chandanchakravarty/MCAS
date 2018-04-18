IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRDRule_GenInfo_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRDRule_GenInfo_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                                                  
Proc Name                : Dbo.Proc_GetRDRule_GenInfo_Pol                                                                                                
Created by               : Ashwani                                                                                                  
Date                     : 02 Mar 2006                                         
Purpose                  : To get the Underwriting Info for RD policy rules                                                  
Revison History          :                                                                                                  
Used In                  : Wolverine                                                                                                  
------------------------------------------------------------                                                                                                  
Date     Review By          Comments                                                                                                  
------   ------------       -------------------------*/      
--drop proc dbo.Proc_GetRDRule_GenInfo_Pol   2107,1,4                                                                                               
CREATE proc [dbo].[Proc_GetRDRule_GenInfo_Pol]                                                  
(                                                                                                  
	@CUSTOMER_ID    int,                                                                                                  
	@POLICY_ID    int,                                                                                                  
	@POLICY_VERSION_ID   int                                                  
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
	        
	 declare @PROPERTY_ON_MORE_THAN nchar(1)        
	 declare @PROPERTY_ON_MORE_THAN_DESC NVARCHAR(150)          
	        
	 declare @VALUED_CUSTOMER_DISCOUNT_OVERRIDE nchar(1)                
	 declare @VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC nvarchar(150)            
	        
	 declare @MODULAR_MANUFACTURED_HOME nchar(1)          
	 declare @BUILT_ON_CONTINUOUS_FOUNDATION  nchar(1)          
	--                                                  
	                                                 
	 declare @BUILD_UNDER_CON_GEN_CONT nchar(1)    --na                                                   
	 declare @NON_SMOKER_CREDIT nchar(1)  --NA                                                  
	 declare @SWIMMING_POOL nchar(1)                                              
	 declare @DESC_BUSINESS nvarchar(150)                
	 declare @IS_RECORD_EXISTS char                                              
	 declare @ANY_PRIOR_LOSSES NVARCHAR(5)  
	 declare @ANY_PRIOR_LOSSES_DESC VARCHAR(50)         
 --                                                                                      
                                             
 if exists (select CUSTOMER_ID from POL_HOME_OWNER_GEN_INFO                                                                     
  where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID)                                 
 begin                                     
 set @IS_RECORD_EXISTS='N'                                          
  SELECT 
	 @IS_VACENT_OCCUPY=isnull(IS_VACENT_OCCUPY,''), @DESC_VACENT_OCCUPY=isnull(DESC_VACENT_OCCUPY,''),                                      
	 @IS_RENTED_IN_PART=isnull(IS_RENTED_IN_PART,''),@DESC_RENTED_IN_PART=isnull(DESC_RENTED_IN_PART,''),                                                  
	 @IS_DWELLING_OWNED_BY_OTHER=isnull(IS_DWELLING_OWNED_BY_OTHER,''),@DESC_DWELLING_OWNED_BY_OTHER =isnull(DESC_DWELLING_OWNED_BY_OTHER,''),                                                  
	 @ANY_FARMING_BUSINESS_COND=isnull(ANY_FARMING_BUSINESS_COND,''),                   
	 @IS_PROP_NEXT_COMMERICAL=isnull(IS_PROP_NEXT_COMMERICAL,''),@DESC_PROPERTY=isnull(DESC_PROPERTY,''),                                                  
	 @ARE_STAIRWAYS_PRESENT=isnull(ARE_STAIRWAYS_PRESENT,''),@DESC_STAIRWAYS=isnull(DESC_STAIRWAYS,''),                                                  
	 @INTANIMALS_EXO_PETS_HISTORY=isnull(ANIMALS_EXO_PETS_HISTORY,'-1'), @MULTI_POLICY_DISC_APPLIED=                  
	 isnull(MULTI_POLICY_DISC_APPLIED,''), @DESC_MULTI_POLICY_DISC_APPLIED=isnull(DESC_MULTI_POLICY_DISC_APPLIED,''),                 
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
	 @PROPERTY_ON_MORE_THAN=isnull(PROPERTY_ON_MORE_THAN,''),@PROPERTY_ON_MORE_THAN_DESC=isnull(PROPERTY_ON_MORE_THAN_DESC,''),        
	 @MODULAR_MANUFACTURED_HOME =isnull(MODULAR_MANUFACTURED_HOME,'') ,        
	        
	  @BUILT_ON_CONTINUOUS_FOUNDATION=isnull(BUILT_ON_CONTINUOUS_FOUNDATION,''),      
	 @VALUED_CUSTOMER_DISCOUNT_OVERRIDE = isnull(VALUED_CUSTOMER_DISCOUNT_OVERRIDE,''),                
	 @VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC = isnull(VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC,''),  
	 @ANY_PRIOR_LOSSES=ISNULL(ANY_PRIOR_LOSSES,''),  
	 @ANY_PRIOR_LOSSES_DESC=ISNULL(ANY_PRIOR_LOSSES_DESC,'')             
	          
	 from POL_HOME_OWNER_GEN_INFO                                                  
	 where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID                                                  
END                                                  
ELSE                                                  
BEGIN                                                   
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
	set @NON_SMOKER_CREDIT='' --NA                  
	set @SWIMMING_POOL =''                                                   
	set @DESC_BUSINESS =''                     
	set @MULTI_POLICY_DISC_APPLIED=''      
	set @DESC_MULTI_POLICY_DISC_APPLIED=''                           
	set @IS_RECORD_EXISTS ='Y'            
	--            
	set @PROPERTY_USED_WHOLE_PART=''  set @PROPERTY_USED_WHOLE_PART_DESC=''          
	set @DWELLING_MOBILE_HOME='' set @DWELLING_MOBILE_HOME_DESC=''        
	set @PROPERTY_ON_MORE_THAN  ='' set @PROPERTY_ON_MORE_THAN_DESC=''        
	set @MODULAR_MANUFACTURED_HOME=''        
	--Valued Home        
	set @VALUED_CUSTOMER_DISCOUNT_OVERRIDE=''                
	set @VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC=''          
                                                    
end          
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
 select @STATE_ID=STATE_ID from POL_CUSTOMER_POLICY_LIST with(nolock)  
 where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID= @POLICY_ID and POLICY_VERSION_ID = @POLICY_VERSION_ID                                                    
----------------------------------                                      
if(@STATE_ID=22 or @STATE_ID=14)                  
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
 set @MULTI_POLICY_DISC_APPLIED='0'      */      
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
 if(@STATE_ID=22 or @STATE_ID=14)                                      
 begin                                       
  if(@IS_RENTED_IN_PART='1')                                                                   
  begin                                                                           
   set @IS_RENTED_IN_PART='Y'                                 
  end                                                                
  else if(@IS_RENTED_IN_PART='0')                                                                          
   begin       
    set @IS_RENTED_IN_PART='N'                       
   end                                     
 end                            
 else                                     
 begin                
if(@IS_RENTED_IN_PART='')                           
  begin                                                                           
   set @IS_RENTED_IN_PART=''                                                           
  end                                                                
  else                                       
  begin                                                                           
  set @IS_RENTED_IN_PART='N'                                                                          
  end                                 
 end                                       
                                      
--                                                
if(@IS_DWELLING_OWNED_BY_OTHER='1')                                                                   
begin                                                                     
set @IS_DWELLING_OWNED_BY_OTHER='Y'                                                           
end                                                                
else if(@IS_DWELLING_OWNED_BY_OTHER='0')                                                                  
begin                                                                           
set @IS_DWELLING_OWNED_BY_OTHER='N'                                                                          
end                                                 
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
if(@INTNO_OF_PETS=-1)                                                
begin                 set  @NO_OF_PETS=''                                                
end                                                 
else                                             
begin                                    
 set  @NO_OF_PETS='N'                                                  
end                     
            
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
IF(@MODULAR_MANUFACTURED_HOME='1')                                                                   
 BEGIN                                                                           
 SET @MODULAR_MANUFACTURED_HOME='Y'                                                           
 END                                                                
ELSE IF(@MODULAR_MANUFACTURED_HOME='0')                                                                          
 BEGIN                                                                           
 SET @MODULAR_MANUFACTURED_HOME='N'       
 END           
          
IF(@BUILT_ON_CONTINUOUS_FOUNDATION='1')                                                                   
 BEGIN                                                                           
 SET @BUILT_ON_CONTINUOUS_FOUNDATION='Y'                                                           
 END                                                                
ELSE IF(@BUILT_ON_CONTINUOUS_FOUNDATION='0')                       
 BEGIN                                      
 SET @BUILT_ON_CONTINUOUS_FOUNDATION='N'                                                                          
 END        
--------------------------------       
DECLARE @MODULAR_MANU_HOME CHAR      
IF(@MODULAR_MANUFACTURED_HOME='Y')          
BEGIN          
   IF(@BUILT_ON_CONTINUOUS_FOUNDATION='N')       
   BEGIN          
   SET @MODULAR_MANU_HOME='Y' --RISK DECLINE          
   END          
         ELSE          
   BEGIN           
   SET @MODULAR_MANU_HOME='N'          
   END           
END           
ELSE          
  BEGIN          
  SET @MODULAR_MANU_HOME='N'          
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
      
IF EXISTS ( SELECT CUSTOMER_ID FROM APP_PRIOR_LOSS_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND LOB=6)      
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
  
--=============================== Itrack No. 3593 ===========================  
  
   DECLARE @MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS VARCHAR  
   SET @MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='N'  
   
 IF(@MULTI_POLICY_DISC_APPLIED='1' OR @MULTI_POLICY_DISC_APPLIED='Y')  
 BEGIN  
	DECLARE 
		@POLICY_LOB INT,  
		@POLICY_NUMBER VARCHAR(100),  
		@COUNT INT,  
	      --@MULTI_POLICY_DISC_APPLIED_PP_DESC VARCHAR(100),  
		@BASE_POLICY_VERSION_ID INT,    
		@NEW_POLICY_VERSION_ID INT ,  
		@PROCESS_ID  INT,  
		@COUNT_POL_STATUS INT,  
	   	@POLICY_STATUS varchar(20),  
		@COUNT_POLICY_NUMBER INT ,  
		@MULTI_POLICY_DISC_APPLIED_PP_DESC VARCHAR(50)  
  
  SELECT @POLICY_LOB=POLICY_LOB,  
         @POLICY_NUMBER=POLICY_NUMBER,  
         @MULTI_POLICY_DISC_APPLIED_PP_DESC=SUBSTRING(DESC_MULTI_POLICY_DISC_APPLIED,0,9),  
         @POLICY_STATUS=POLICY_STATUS  FROM POL_CUSTOMER_POLICY_LIST POL  
  INNER JOIN POL_HOME_OWNER_GEN_INFO  POL_WATER ON    
  POL.CUSTOMER_ID=POL_WATER.CUSTOMER_ID AND POL.POLICY_ID=POL_WATER.POLICY_ID AND POL.POLICY_VERSION_ID=POL_WATER.POLICY_VERSION_ID  
  WHERE POL.CUSTOMER_ID=@CUSTOMER_ID AND POL.POLICY_ID=@POLICY_ID AND POL.POLICY_VERSION_ID=@POLICY_VERSION_ID   
 --------------- 
  SELECT @BASE_POLICY_VERSION_ID = MAX(POLICY_VERSION_ID),    
                @NEW_POLICY_VERSION_ID  = MAX(NEW_POLICY_VERSION_ID)  
         FROM POL_POLICY_PROCESS WITH(NOLOCK)    
         WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID   
    
  SELECT  @PROCESS_ID=PROCESS_ID  
  FROM POL_POLICY_PROCESS WITH(NOLOCK)    
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@BASE_POLICY_VERSION_ID  
    
  SELECT @COUNT_POL_STATUS=COUNT(POLICY_NUMBER)  FROM POL_CUSTOMER_POLICY_LIST   
  WHERE POLICY_NUMBER = @MULTI_POLICY_DISC_APPLIED_PP_DESC AND --POLICY_STATUS = 'INACTIVE'  
  POLICY_STATUS IN('INACTIVE','CANCEL','SUSPENDED')
 
  --SELECT @COUNT_POLICY_NUMBER=COUNT(POLICY_NUMBER)  FROM POL_CUSTOMER_POLICY_LIST   
  --WHERE POLICY_NUMBER = @MULTI_POLICY_DISC_APPLIED_PP_DESC AND  POLICY_STATUS NOT IN('INACTIVE','CANCEL','SUSPENDED')  
    
   --SET @COUNT=@COUNT_POLICY_NUMBER 
 
	IF (@POLICY_LOB = 6)                 
	BEGIN                
		SELECT   @COUNT=COUNT(POLICY_NUMBER) FROM POL_CUSTOMER_POLICY_LIST, MNT_AGENCY_LIST            
		WHERE CUSTOMER_ID = @CUSTOMER_ID     
		AND POL_CUSTOMER_POLICY_LIST.AGENCY_ID = MNT_AGENCY_LIST.AGENCY_ID            
		AND POLICY_NUMBER <>  @POLICY_NUMBER          
		AND POL_CUSTOMER_POLICY_LIST.IS_ACTIVE = 'Y' AND APP_EXPIRATION_DATE > GETDATE() AND POLICY_STATUS = 'NORMAL'           
		--AND POLICY_NUMBER!=@MULTI_POLICY_DISC_APPLIED_PP_DESC
	END	  
  -- Umbrella  
	IF (@POLICY_LOB=5 OR @POLICY_LOB=7)                  
	BEGIN    
		SET @COUNT = -1 
	END 
 -->>1: ==>>>NEW BUSINESS AND REWRITE   
  --If There is a yes in the Field Is multi-policy discount applied?*   
  --and there are no Eligible policies - make sure that   
  --there are details in the field Multi-policy discount description   
  --If there are detail allow the discount 
    
	IF(@COUNT >0 AND ( @PROCESS_ID IN(24,25,31,32)) )  
	BEGIN   
		SET @MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y'  
	END
--SELECT * FROM POL_PROCESS_MASTER
  
 -->>2: ==>>>RENEWAL   
  --If there are no Eligible Policies and there is Yes in the Field multi-policy discount applied?*  
  --program to see if the policy number in the Field Multi-policy discount description is active   
  --If policy is not active or does not exist on the database  
  --This will goes as a Refer to Underwriters  
  --Note for the referral - Multi Policy Discount Eligibility  
  --If referral is accepted - allow discount    
    
	--ELSE IF(@COUNT=0  AND (@COUNT_POL_STATUS>0 OR @COUNT_POLICY_NUMBER=0)  AND (@PROCESS_ID IN(5,18)))  
	ELSE IF(@COUNT=0  AND (@COUNT_POL_STATUS>0 )  AND (@PROCESS_ID IN(5,18)))  
	BEGIN   
		SET @MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y'  
	END  
	ELSE IF(@COUNT=0  AND (@PROCESS_ID IN(5,18)))  
	BEGIN   
		SET @MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS='Y'  
	END
END                          
-------------------------------------------      
SELECT                                                  
	 -- MANDATORY                                   
	 @IS_VACENT_OCCUPY AS IS_VACENT_OCCUPY,                                                 
	 CASE @IS_VACENT_OCCUPY                                                    
	 WHEN 'Y'THEN  @DESC_VACENT_OCCUPY                                                     
	 END AS DESC_VACENT_OCCUPY,                      
	 @IS_RENTED_IN_PART AS IS_RENTED_IN_PART,                                                
	 CASE @IS_RENTED_IN_PART                                                    
	 WHEN 'Y'THEN  @DESC_RENTED_IN_PART                                                     
	 END AS DESC_RENTED_IN_PART,                     
	 @IS_DWELLING_OWNED_BY_OTHER AS IS_DWELLING_OWNED_BY_OTHER,                                                  
	 CASE @IS_DWELLING_OWNED_BY_OTHER                                                    
	 WHEN 'Y'THEN  @DESC_DWELLING_OWNED_BY_OTHER                                                     
	 END AS DESC_DWELLING_OWNED_BY_OTHER,                                                     
	                                                 
	 @ANY_FARMING_BUSINESS_COND AS  ANY_FARMING_BUSINESS_COND, --NA                            
	                                                 
	 @IS_PROP_NEXT_COMMERICAL AS IS_PROP_NEXT_COMMERICAL,                                                  
	 CASE @IS_PROP_NEXT_COMMERICAL                                  
	 WHEN 'Y'THEN  @DESC_PROPERTY                             
	 END AS DESC_PROPERTY,                                                     
	                                
	 @ARE_STAIRWAYS_PRESENT AS ARE_STAIRWAYS_PRESENT,                                                  
	 CASE @ARE_STAIRWAYS_PRESENT                                                
	 WHEN 'Y'THEN  @DESC_STAIRWAYS                                                     
	 END AS DESC_STAIRWAYS,    
	                                                 
	 @ANIMALS_EXO_PETS_HISTORY AS  ANIMALS_EXO_PETS_HISTORY,                   
	 CASE @ANIMALS_EXO_PETS_HISTORY                  
	 WHEN 'Y'THEN  @BREED                                      
	 END AS BREED,                                                
	                                       
	 @NO_OF_PETS AS NO_OF_PETS,                  
	 CASE                                                 
	 WHEN @INTNO_OF_PETS > 0 THEN @OTHER_DESCRIPTION                                            
	 END AS OTHER_DESCRIPTION,                       
	 @IS_SWIMPOLL_HOTTUB AS  IS_SWIMPOLL_HOTTUB, --NA 
	 @HAS_INSU_TRANSFERED_AGENCY AS HAS_INSU_TRANSFERED_AGENCY,                                                  
	 CASE @HAS_INSU_TRANSFERED_AGENCY                                                
	 WHEN '1' THEN @DESC_INSU_TRANSFERED_AGENCY                                                
	 END  AS DESC_INSU_TRANSFERED_AGENCY,                                                  
	                                                 
	 @IS_OWNERS_DWELLING_CHANGED AS IS_OWNERS_DWELLING_CHANGED,                                                  
	 CASE @IS_OWNERS_DWELLING_CHANGED                                                 
	 WHEN '1' THEN @DESC_OWNER                                                 
	 END AS DESC_OWNER,                                                  
	                                                 
	                                                 
	 @ANY_COV_DECLINED_CANCELED AS ANY_COV_DECLINED_CANCELED,                                                
	 CASE @ANY_COV_DECLINED_CANCELED                                                 
	 WHEN 'Y' THEN @DESC_COV_DECLINED_CANCELED                                 
	 END AS DESC_COV_DECLINED_CANCELED,                
	                                                 
	 @CONVICTION_DEGREE_IN_PAST AS CONVICTION_DEGREE_IN_PAST,                                                  
	 CASE @CONVICTION_DEGREE_IN_PAST                    
	 WHEN 'Y' THEN @DESC_CONVICTION_DEGREE_IN_PAST                                                 
	 END AS DESC_CONVICTION_DEGREE_IN_PAST,                                                  
	                                                 
	 @LEAD_PAINT_HAZARD AS LEAD_PAINT_HAZARD,                                                   
	 CASE @LEAD_PAINT_HAZARD                                                 
	 WHEN '1' THEN @DESC_LEAD_PAINT_HAZARD                                                 
	 END AS DESC_LEAD_PAINT_HAZARD,                                          
	                                                 
	                   
	                                              
	 @ANY_RESIDENCE_EMPLOYEE AS  ANY_RESIDENCE_EMPLOYEE,                                                  
	 CASE @ANY_RESIDENCE_EMPLOYEE                                                 
	 WHEN '1' THEN @DESC_RESIDENCE_EMPLOYEE                                                 
	 END AS DESC_RESIDENCE_EMPLOYEE,                                                  
	                                                 
	 @ANY_OTHER_RESI_OWNED AS ANY_OTHER_RESI_OWNED,                      
	 CASE @ANY_OTHER_RESI_OWNED                  
	 WHEN '1' THEN @DESC_OTHER_RESIDENCE                                                 
	 END AS DESC_OTHER_RESIDENCE,                                                  
	                                                 
	 @ANY_OTH_INSU_COMP AS ANY_OTH_INSU_COMP,                                                  
	 CASE @ANY_OTH_INSU_COMP                                                 
	 WHEN '1' THEN @DESC_OTHER_INSURANCE                                                 
	 END AS DESC_OTHER_INSURANCE,                                        
	                                                 
	 @ANY_RENOVATION AS ANY_RENOVATION,                         
	 CASE @ANY_RENOVATION                                    
	 WHEN '1' THEN @DESC_RENOVATION              
	 END AS DESC_RENOVATION,                                                 
	                                                 
	 @TRAMPOLINE AS TRAMPOLINE,                                                  
	 CASE @TRAMPOLINE                                                 
	 WHEN '1' THEN @DESC_TRAMPOLINE                                                 
	 END AS DESC_TRAMPOLINE,                                                 
	                                                 
	 @RENTERS AS RENTERS,               
	 CASE @RENTERS                                   
	 WHEN 'Y' THEN @DESC_RENTERS                                                 
	 END AS DESC_RENTERS,                                                 
	                                                 
	 @ANY_HEATING_SOURCE AS ANY_HEATING_SOURCE,                           
	 CASE @ANY_HEATING_SOURCE                    
	 WHEN 'Y' THEN @DESC_ANY_HEATING_SOURCE                                                 
	 END AS DESC_ANY_HEATING_SOURCE,             
	             
	 ---            
	 @PROPERTY_USED_WHOLE_PART AS PROPERTY_USED_WHOLE_PART,                                  
	 CASE @PROPERTY_USED_WHOLE_PART            
	 WHEN 'Y' THEN @PROPERTY_USED_WHOLE_PART_DESC            
	 END AS PROPERTY_USED_WHOLE_PART_DESC,            
	           
	           
	 @DWELLING_MOBILE_HOME AS DWELLING_MOBILE_HOME,                                
	 CASE @DWELLING_MOBILE_HOME          
	 WHEN 'Y' THEN @DWELLING_MOBILE_HOME_DESC          
	 END AS DWELLING_MOBILE_HOME_DESC,         
	         
	 @PROPERTY_ON_MORE_THAN AS PROPERTY_ON_MORE_THAN,                                
	 CASE @PROPERTY_ON_MORE_THAN          
	 WHEN 'Y' THEN @PROPERTY_ON_MORE_THAN_DESC          
	 END AS PROPERTY_ON_MORE_THAN_DESC,        
	         
	 ------                
	 @VALUED_CUSTOMER_DISCOUNT_OVERRIDE AS VALUED_CUSTOMER_DISCOUNT_OVERRIDE,                                                   
	 CASE @VALUED_CUSTOMER_DISCOUNT_OVERRIDE                          
	 WHEN 'Y' THEN @VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC                                                      
	 END AS VALUED_CUSTOMER_DISCOUNT_OVERRIDE_DESC,          
	 ---                                                 
	         
	 @MODULAR_MANUFACTURED_HOME AS MODULAR_MANUFACTURED_HOME,                                              
	                                                 
	 @BUILD_UNDER_CON_GEN_CONT AS BUILD_UNDER_CON_GEN_CONT, --NA                                                   
	 --@NON_SMOKER_CREDIT AS NON_SMOKER_CREDIT, --NA                                                  
	 @SWIMMING_POOL AS SWIMMING_POOL,                                                
	 --@DESC_BUSINESS AS DESC_BUSINESS                   
	 @MULTI_POLICY_DISC_APPLIED AS MULTI_POLICY_DISC_APPLIED,       
	 CASE @MULTI_POLICY_DISC_APPLIED                          
	 WHEN 'Y' THEN @DESC_MULTI_POLICY_DISC_APPLIED                                                      
	 END AS DESC_MULTI_POLICY_DISC_APPLIED,    
	 @MODULAR_MANU_HOME AS MODULAR_MANU_HOME,     
	 @IS_RECORD_EXISTS AS IS_RECORD_EXISTS  ,  
	 @ANY_PRIOR_LOSSES AS ANY_PRIOR_LOSSES,   
	 CASE @ANY_PRIOR_LOSSES         
	 WHEN 'Y' THEN @ANY_PRIOR_LOSSES_DESC        
	 END AS ANY_PRIOR_LOSSES_DESC ,  
	 @PRIOR_LOSS_INFO_EXISTS AS PRIOR_LOSS_INFO_EXISTS,  
	 @MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS AS MULTIPOLICY_DISC_APPLIED_AT_NEW_BUSINESS                           
END     


GO

