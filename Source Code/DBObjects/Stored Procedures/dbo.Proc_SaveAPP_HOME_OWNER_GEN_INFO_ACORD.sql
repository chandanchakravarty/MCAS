IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SaveAPP_HOME_OWNER_GEN_INFO_ACORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SaveAPP_HOME_OWNER_GEN_INFO_ACORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--drop proc dbo.Proc_SaveAPP_HOME_OWNER_GEN_INFO_ACORD    
CREATE PROC dbo.Proc_SaveAPP_HOME_OWNER_GEN_INFO_ACORD                                      
(                                      
 @CUSTOMER_ID     int,                                      
 @APP_ID     int,                                      
 @APP_VERSION_ID     smallint,                                      
 @ANY_FARMING_BUSINESS_COND     nchar(1)= null,                                       
 @ANY_RESIDENCE_EMPLOYEE     nchar(1),                                       
 @DESC_RESIDENCE_EMPLOYEE     nvarchar(150),                                        
 @ANY_OTHER_RESI_OWNED     nchar(1),                              
 @DESC_OTHER_RESIDENCE     nvarchar(150),                                       
 @ANY_OTH_INSU_COMP     nchar(1),                                
 @DESC_OTHER_INSURANCE     nvarchar(150),                                            
 @HAS_INSU_TRANSFERED_AGENCY     nchar(1),                                        
 @DESC_INSU_TRANSFERED_AGENCY     nvarchar(150),                                      
 @ANY_COV_DECLINED_CANCELED     nchar(1),                              
 @DESC_COV_DECLINED_CANCELED     nvarchar(150),                               
 @ANIMALS_EXO_PETS_HISTORY     int,                                     
 @BREED     nvarchar(100),                                         
 @OTHER_DESCRIPTION     nvarchar(100),                                         
 @CONVICTION_DEGREE_IN_PAST     nchar(1),                                       
 @DESC_CONVICTION_DEGREE_IN_PAST     nvarchar(150),                                         
 @ANY_RENOVATION     nchar(1),                                       
 @DESC_RENOVATION     nvarchar(150),                                       
 @TRAMPOLINE     nchar(1),                               
 @DESC_TRAMPOLINE     nvarchar(150),                              
 @LEAD_PAINT_HAZARD     nchar(1),                                       
 @DESC_LEAD_PAINT_HAZARD     nvarchar(150),                                      
 @RENTERS     nchar(1),                               
 @DESC_RENTERS     nvarchar(150),                    
 @BUILD_UNDER_CON_GEN_CONT     nchar(1),                              
 @REMARKS     nvarchar(1000),                               
 @MULTI_POLICY_DISC_APPLIED NCHAR(2),             
 @CREATED_BY     int,                               
 @MODIFIED_BY     int,                                      
 @CREATED_DATETIME     datetime,                                      
 @NO_OF_PETS INT ,                                    
 @IS_SWIMPOLL_HOTTUB char(1) ,                                    
 @LAST_INSPECTED_DATE datetime,                                
 @NON_SMOKER_CREDIT NChar(1),                                
 @ANY_HEATING_SOURCE NChar(1)    ,                            
 @EXP_AGE_CREDIT int,                            
 @IS_LOSS_FREE_12_MONTHS nchar(1)  =null   ,                    
 @NO_OF_YEARS_INSURED int ,                   
 @IS_VACENT_OCCUPY nchar(1),                        
 @IS_RENTED_IN_PART nchar(1),                        
 @IS_DWELLING_OWNED_BY_OTHER nchar(1),                        
 @IS_PROP_NEXT_COMMERICAL nchar(1),                        
 @ARE_STAIRWAYS_PRESENT nchar(1),                        
 @IS_OWNERS_DWELLING_CHANGED nchar(1),                        
 @SWIMMING_POOL nchar(1),                        
 @ANY_FORMING NCHAR(1)          ,       
 @Location   nvarchar(100)=null,       
 @PREMISES INT = NULL,                    
 @ISANY_HORSE NCHAR(1)= NULL,                    
 @PROPERTY_ON_MORE_THAN  nchar(1)          ,            
 @PROPERTY_USED_WHOLE_PART  nchar(1),                      
 @DWELLING_MOBILE_HOME  nchar(1),                               
 @VALUED_CUSTOMER_DISCOUNT_OVERRIDE nchar(1),          
 @MODULAR_MANUFACTURED_HOME nchar(1) ,         
 @YEARS_INSU_WOL nchar(10) ,  
 @ANY_PRIOR_LOSSES  varchar (10) = null,
 @NON_WEATHER_CLAIMS smallint = null,     
 @WEATHER_CLAIMS smallint = null  
) 
AS                                      
BEGIN                  
       
--commented Praveen Kasana 9 Dec 2009
--@OTHER_DESCRIPTION contains list of dogs and No.
/*          
 DECLARE @DOG_BREED Int            
 EXECUTE @DOG_BREED = Proc_GetLookupID 'HBDOG',@OTHER_DESCRIPTION                                                          
 IF ( @@ERROR <> 0 )      
 BEGIN                        
  RETURN                        
 END                          

*/
  -----------added by Pravesh on 20 march FOR IF MULTI POLICY DISCOUNT THEN PUT POLICY NUMBER IN DESC FIELD  
declare @DESC_MULTI_POLICY_DISC_APPLIED varchar(20) -- as this para is not is not being passed  

DECLARE @AGENCY_ID     SMALLINT,  
    @LOB_ID     SMALLINT,  
    @MULTI_POLICY_NUMBER varchar(20),  
    @APP_POLICY_NUMBER  varchar(20),  
    @MULTI_POLICY_COUNT  SMALLINT  
  
-- Itrack 5267 --@AGENCY_ID  no longer use in Proc Proc_GetEligiblePolicies
 SELECT @AGENCY_ID=APP_AGENCY_ID,@LOB_ID=APP_LOB, @APP_POLICY_NUMBER=APP_NUMBER FROM APP_LIST WITH(NOLOCK) WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID   
  


--Itrack 5267.
--Convert to an application and the Application information tab is showing that there are eligible policies. 
--Program should change the underwriting tab. 
-- Multi Policy Discount from No to yes and put in an eligible policy number in the Multi Policy Description Field.

--
--IF (ISNULL(@MULTI_POLICY_DISC_APPLIED,'0')='1')  
--BEGIN

 
 create table ##TMP_MULTIPOLICY  
  (APP_POL_NUMBER varchar(20))  
 INSERT INTO ##TMP_MULTIPOLICY   
  EXECUTE Proc_GetEligiblePolicies   @CUSTOMER_ID ,  @AGENCY_ID , @LOB_ID , @APP_POLICY_NUMBER   

--Check if Any Eligible Policies exists or not
IF EXISTS(SELECT 1 FROM ##TMP_MULTIPOLICY)
BEGIN

 SELECT @MULTI_POLICY_COUNT=COUNT(*)  FROM ##TMP_MULTIPOLICY 
    
  SELECT TOP 1 @MULTI_POLICY_NUMBER=APP_POL_NUMBER FROM ##TMP_MULTIPOLICY     
  DROP TABLE ##TMP_MULTIPOLICY    
  IF (ISNULL(@MULTI_POLICY_NUMBER,'')!='N.A.' AND ISNULL(@MULTI_POLICY_NUMBER,'')!='' AND @MULTI_POLICY_COUNT>0)    
  BEGIN    
    SET @MULTI_POLICY_DISC_APPLIED = '1'
	SET @DESC_MULTI_POLICY_DISC_APPLIED=@MULTI_POLICY_NUMBER    
  END   


--END

  


END  
--------END HERE  

if(@LOB_ID = 1)
Begin
	if(@YEARS_INSU_WOL = -1)
		set @YEARS_INSU_WOL = null
end

--Rental lob
If(@LOB_ID = 6)
Begin
 SET @ANY_FARMING_BUSINESS_COND	 = '0'
End
              
                                    
 IF EXISTS                                 (                                    
 SELECT 1 FROM APP_HOME_OWNER_GEN_INFO                                    
 WHERE                                        
 CUSTOMER_ID   = @CUSTOMER_ID AND                                        
 APP_ID    = @APP_ID AND                                        
 APP_VERSION_ID   = @APP_VERSION_ID                                        
 )                                    
 BEGIN                                    
 UPDATE APP_HOME_OWNER_GEN_INFO                                     
 SET                                        
  ANY_FARMING_BUSINESS_COND = @ANY_FARMING_BUSINESS_COND,                                        
  ANY_RESIDENCE_EMPLOYEE  = @ANY_RESIDENCE_EMPLOYEE,                                        
  DESC_RESIDENCE_EMPLOYEE  = @DESC_RESIDENCE_EMPLOYEE,                                        
  ANY_OTHER_RESI_OWNED  = @ANY_OTHER_RESI_OWNED,                                        
  DESC_OTHER_RESIDENCE  = @DESC_OTHER_RESIDENCE,                                        
  ANY_OTH_INSU_COMP  = @ANY_OTH_INSU_COMP,                                        
  DESC_OTHER_INSURANCE  = @DESC_OTHER_INSURANCE,                                        
  HAS_INSU_TRANSFERED_AGENCY = @HAS_INSU_TRANSFERED_AGENCY,                                        
  DESC_INSU_TRANSFERED_AGENCY = @DESC_INSU_TRANSFERED_AGENCY,                                        
  ANY_COV_DECLINED_CANCELED = @ANY_COV_DECLINED_CANCELED,                                     
  DESC_COV_DECLINED_CANCELED = @DESC_COV_DECLINED_CANCELED,                                     
  ANIMALS_EXO_PETS_HISTORY = @ANIMALS_EXO_PETS_HISTORY,                                        
  BREED    = @BREED,                                        
  OTHER_DESCRIPTION  = @OTHER_DESCRIPTION,--@DOG_BREED,                                         
  CONVICTION_DEGREE_IN_PAST = @CONVICTION_DEGREE_IN_PAST,                                        
  DESC_CONVICTION_DEGREE_IN_PAST = @DESC_CONVICTION_DEGREE_IN_PAST,                                        
  ANY_RENOVATION       = @ANY_RENOVATION,         
  DESC_RENOVATION       = @DESC_RENOVATION,                                        
  TRAMPOLINE        = @TRAMPOLINE,                                        
  DESC_TRAMPOLINE       = @DESC_TRAMPOLINE,                                        
  LEAD_PAINT_HAZARD  = @LEAD_PAINT_HAZARD,                            
  DESC_LEAD_PAINT_HAZARD = @DESC_LEAD_PAINT_HAZARD,                                        
  RENTERS    = @RENTERS,                    
  DESC_RENTERS   = @DESC_RENTERS,                                        
  BUILD_UNDER_CON_GEN_CONT = @BUILD_UNDER_CON_GEN_CONT,                                        
  REMARKS    = @REMARKS,                                        
  IS_ACTIVE   = 'Y',                                        
  MODIFIED_BY   = @MODIFIED_BY,                                        
  LAST_UPDATED_DATETIME  = GetDate(),                                        
  MULTI_POLICY_DISC_APPLIED = @MULTI_POLICY_DISC_APPLIED,   
  DESC_MULTI_POLICY_DISC_APPLIED=@DESC_MULTI_POLICY_DISC_APPLIED,                                       
  NO_OF_PETS   = @NO_OF_PETS ,                                    
  IS_SWIMPOLL_HOTTUB   =@IS_SWIMPOLL_HOTTUB,                                 
  NON_SMOKER_CREDIT =    @NON_SMOKER_CREDIT,                                
  ANY_HEATING_SOURCE = @ANY_HEATING_SOURCE,                                
  LAST_INSPECTED_DATE=@LAST_INSPECTED_DATE ,                            
  --added for exp credit and loss free                            
  EXP_AGE_CREDIT=@EXP_AGE_CREDIT ,                            
  IS_LOSS_FREE_12_MONTHS=@IS_LOSS_FREE_12_MONTHS  ,                    
  NO_OF_YEARS_INSURED=@NO_OF_YEARS_INSURED ,                          
  /* New field added on 06 mar   */                  
  IS_VACENT_OCCUPY =@IS_VACENT_OCCUPY,                        
  IS_RENTED_IN_PART = @IS_RENTED_IN_PART,                        
  IS_DWELLING_OWNED_BY_OTHER = @IS_DWELLING_OWNED_BY_OTHER,                        
  IS_PROP_NEXT_COMMERICAL = @IS_PROP_NEXT_COMMERICAL,              
  ARE_STAIRWAYS_PRESENT = @ARE_STAIRWAYS_PRESENT,                        
  IS_OWNERS_DWELLING_CHANGED = @IS_OWNERS_DWELLING_CHANGED,                        
  SWIMMING_POOL = @SWIMMING_POOL,              
  ANY_FORMING = @ANY_FORMING,    
  Location  =  @Location,    
  PREMISES =@PREMISES ,        
  ISANY_HORSE=@ISANY_HORSE ,        
  PROPERTY_ON_MORE_THAN   = @PROPERTY_ON_MORE_THAN,            
  PROPERTY_USED_WHOLE_PART   = @PROPERTY_USED_WHOLE_PART,            
  DWELLING_MOBILE_HOME   = @DWELLING_MOBILE_HOME ,           
  VALUED_CUSTOMER_DISCOUNT_OVERRIDE = @VALUED_CUSTOMER_DISCOUNT_OVERRIDE,          
  MODULAR_MANUFACTURED_HOME  = @MODULAR_MANUFACTURED_HOME   ,        
  YEARS_INSU_WOL = @YEARS_INSU_WOL,    
  ANY_PRIOR_LOSSES = @ANY_PRIOR_LOSSES,
  NON_WEATHER_CLAIMS = @NON_WEATHER_CLAIMS,
  WEATHER_CLAIMS = @WEATHER_CLAIMS

  WHERE                                        
  CUSTOMER_ID   = @CUSTOMER_ID AND                                        
  APP_ID    = @APP_ID AND                                        
  APP_VERSION_ID   = @APP_VERSION_ID                                        
 END                                    
ELSE                                    
 BEGIN                                     
  INSERT INTO APP_HOME_OWNER_GEN_INFO                    
  (                                      
  CUSTOMER_ID,                                      
  APP_ID,                                      
  APP_VERSION_ID,                                      
  ANY_FARMING_BUSINESS_COND,                                      
  ANY_RESIDENCE_EMPLOYEE,                                      
  DESC_RESIDENCE_EMPLOYEE,                                      
  ANY_OTHER_RESI_OWNED,                
  DESC_OTHER_RESIDENCE,                                      
  ANY_OTH_INSU_COMP,                                      
  DESC_OTHER_INSURANCE,      
  HAS_INSU_TRANSFERED_AGENCY,                  
  DESC_INSU_TRANSFERED_AGENCY,                                      
  ANY_COV_DECLINED_CANCELED,                                      
  DESC_COV_DECLINED_CANCELED,                                      
  ANIMALS_EXO_PETS_HISTORY,           
  BREED,                                      
  OTHER_DESCRIPTION,                         
  CONVICTION_DEGREE_IN_PAST,                                      
  DESC_CONVICTION_DEGREE_IN_PAST,                                      
  ANY_RENOVATION,                                      
  DESC_RENOVATION,                                      
  TRAMPOLINE,                                      
  DESC_TRAMPOLINE,                                      
  LEAD_PAINT_HAZARD,                                      
  DESC_LEAD_PAINT_HAZARD,                                      
  RENTERS,                                      
  DESC_RENTERS,                                      
  BUILD_UNDER_CON_GEN_CONT,                                      
  REMARKS,      
  IS_ACTIVE,                                       
  MULTI_POLICY_DISC_APPLIED,                                      
  CREATED_BY,                                      
  CREATED_DATETIME,                                      
  NO_OF_PETS  ,                                    
  IS_SWIMPOLL_HOTTUB,                                    
  LAST_INSPECTED_DATE,                                
  NON_SMOKER_CREDIT,                                
  ANY_HEATING_SOURCE ,                            
  --added                            
  IS_LOSS_FREE_12_MONTHS ,                            
  EXP_AGE_CREDIT  ,                    
  NO_OF_YEARS_INSURED ,              
  /* New field added on 06 mar   */                  
  IS_VACENT_OCCUPY,                        
  IS_RENTED_IN_PART,                        
  IS_DWELLING_OWNED_BY_OTHER,                        
  IS_PROP_NEXT_COMMERICAL,                        
  ARE_STAIRWAYS_PRESENT,                        
  IS_OWNERS_DWELLING_CHANGED,                        
  SWIMMING_POOL,                         
  ANY_FORMING,    
  Location,            
  PREMISES ,        
  ISANY_HORSE,        
  PROPERTY_ON_MORE_THAN,            
  PROPERTY_USED_WHOLE_PART,            
  DWELLING_MOBILE_HOME ,          
  VALUED_CUSTOMER_DISCOUNT_OVERRIDE,          
  MODULAR_MANUFACTURED_HOME  ,        
  YEARS_INSU_WOL ,  
  ANY_PRIOR_LOSSES,
  DESC_MULTI_POLICY_DISC_APPLIED,
  NON_WEATHER_CLAIMS,
  WEATHER_CLAIMS          
          
  )                                      
  VALUES                                      
  (                                      
  @CUSTOMER_ID,                                      
  @APP_ID,                                      
  @APP_VERSION_ID,          
  @ANY_FARMING_BUSINESS_COND,                                            
  @ANY_RESIDENCE_EMPLOYEE,                                      
  @DESC_RESIDENCE_EMPLOYEE,                                      
  @ANY_OTHER_RESI_OWNED,                                      
  @DESC_OTHER_RESIDENCE,                                      
  @ANY_OTH_INSU_COMP,            @DESC_OTHER_INSURANCE,        
  @HAS_INSU_TRANSFERED_AGENCY,                                      
  @DESC_INSU_TRANSFERED_AGENCY,                                      
  @ANY_COV_DECLINED_CANCELED,                                      
  @DESC_COV_DECLINED_CANCELED,                                      
  @ANIMALS_EXO_PETS_HISTORY,                                      
  @BREED,                                      
  @OTHER_DESCRIPTION,--@DOG_BREED,                                      
          
  @CONVICTION_DEGREE_IN_PAST, 
  @DESC_CONVICTION_DEGREE_IN_PAST,                                      
          
  @ANY_RENOVATION,                                      
  @DESC_RENOVATION,                                      
          
  @TRAMPOLINE,                                     
  @DESC_TRAMPOLINE,             
          
  @LEAD_PAINT_HAZARD,                                      
  @DESC_LEAD_PAINT_HAZARD,                                      
          
  @RENTERS,                                      
  @DESC_RENTERS,                                      
  @BUILD_UNDER_CON_GEN_CONT,                                      
  @REMARKS,      
  'Y',                                       
  @MULTI_POLICY_DISC_APPLIED,                                       
  @CREATED_BY,                                      
  @CREATED_DATETIME,                                      
  @NO_OF_PETS,                                    
  @IS_SWIMPOLL_HOTTUB,                                    
  @LAST_INSPECTED_DATE  ,                                
  @NON_SMOKER_CREDIT,                                
  @ANY_HEATING_SOURCE  ,                            
  @IS_LOSS_FREE_12_MONTHS ,                            
  @EXP_AGE_CREDIT ,                    
  @NO_OF_YEARS_INSURED ,                    
  /* New field added on 06 mar   */                  
  @IS_VACENT_OCCUPY,                        
  @IS_RENTED_IN_PART,                        
  @IS_DWELLING_OWNED_BY_OTHER,                        
  @IS_PROP_NEXT_COMMERICAL,                        
  @ARE_STAIRWAYS_PRESENT,                        
  @IS_OWNERS_DWELLING_CHANGED,                        
  @SWIMMING_POOL,                        
  @ANY_FORMING ,     
  @Location       ,    
  @PREMISES ,        
  @ISANY_HORSE ,        
  @PROPERTY_ON_MORE_THAN,            
  @PROPERTY_USED_WHOLE_PART,            
  @DWELLING_MOBILE_HOME ,           
  @VALUED_CUSTOMER_DISCOUNT_OVERRIDE,          
  @MODULAR_MANUFACTURED_HOME  ,        
  @YEARS_INSU_WOL ,  
  @ANY_PRIOR_LOSSES ,
  @DESC_MULTI_POLICY_DISC_APPLIED,
  @NON_WEATHER_CLAIMS,
  @WEATHER_CLAIMS
  )                                      
 END             
        
END        













GO

