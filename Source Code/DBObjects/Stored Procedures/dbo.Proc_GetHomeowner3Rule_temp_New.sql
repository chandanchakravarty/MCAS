IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHomeowner3Rule_temp_New]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHomeowner3Rule_temp_New]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--Proc_GetHomeowner3Rule_temp_New 435,7,1,20  
                        
/*----------------------------------------------------------                                
Proc Name                : Dbo.Proc_GetHomeowner3Rule_temp_New                              
Created by               : Ashwani                                
Date                     : 07 Oct.,2005                              
Purpose                  : To get the Rule Information for Homeowners                              
Revison History          :                                
Used In                  : Wolverine                                
------------------------------------------------------------                                
Date     Review By          Comments                                
--   ------------       -------------------------*/                                
CREATE proc Dbo.Proc_GetHomeowner3Rule_temp_New                                
(                                
@CUSTOMERID    int,                                
@APPID    int,                                
@APPVERSIONID   int,                                
@DWELLINGID    int                                
)                                
as                                  
begin                              
set quoted_identifier off                                
        
                             
--APP_DWELLING_COVERAGE                        
 declare  @intCOVERAGEA int  --1.A, 2.A.2.b ,2.F.2.a for HO-2,HO-3 and HO-5          
 declare  @COVERAGEA char  --1.A          
 declare @intPERSONAL_PROP_LIMIT int  --2.C          
 declare @intMINCOVC int  --2.C          
 declare @COVAC_HO6 char  --2.C      
if exists( select Customer_ID from  APP_DWELLING_COVERAGE             
where CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID and  DWELLING_ID =@DWELLINGID)          
begin         
select  @intCOVERAGEA =isnull(DWELLING_LIMIT,''),@intPERSONAL_PROP_LIMIT=isnull(PERSONAL_PROP_LIMIT,0)             
from  APP_DWELLING_COVERAGE             
where CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID and  DWELLING_ID =@DWELLINGID           
end          
else          
begin           
set @intCOVERAGEA =0      
set @COVERAGEA =''      
set @intPERSONAL_PROP_LIMIT =0      
set @intMINCOVC =0      
set @COVAC_HO6 =''      
end                               
--            
if(@intCOVERAGEA >400000)            
begin             
set @COVERAGEA='Y'            
end    
else if(@intCOVERAGEA <> 0)            
begin            
set @COVERAGEA='N'            
end if(@intCOVERAGEA = 0)
begin     
set @COVERAGEA=''      
end               
  
-- For HO6             
set @intMINCOVC=(0.1 *@intPERSONAL_PROP_LIMIT)            
if((@intMINCOVC=@intCOVERAGEA) or (@intCOVERAGEA=2000 ))            
begin             
 set @COVAC_HO6='Y'            
end             
else            
begin             
 set @COVAC_HO6='N'            
end        
            
--APP_DWELLINGS_INFO            
declare @intYEAR_BUILT int   --2.A.2.c          
declare @YEAR_BUILT char  --2.A.2.c          
declare @intBUILDING_TYPE int  --2.F.14          
declare @BUILDING_TYPE char  --2.F.14          
declare @intREPLACEMENT_COST int  --2.A.2.b, 2.F.2.a for HO-2 ,Ho-3 and Ho-5          
declare @REPLACEMENT_COSTHO5 char  --2.A.2.b          
declare @REPLACEMENT_COST_POLICY_HO2_HO3 char --2.F.2.a for HO-2, HO-3           
declare @intCOVERAGE_A_HO2_HO3 int  --2.F.2.a for HO-2 , HO-3          
declare @REPLACEMENT_COST_POLICY_HO5 char          
            
if exists(select CUSTOMER_ID from APP_DWELLINGS_INFO             
where CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID =@DWELLINGID )      
begin             
select @intYEAR_BUILT=isnull(YEAR_BUILT,0),@intBUILDING_TYPE =isnull(BUILDING_TYPE,''),            
@intREPLACEMENT_COST=isnull(REPLACEMENT_COST,0)            
from APP_DWELLINGS_INFO             
where CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID AND DWELLING_ID =@DWELLINGID             
end       
else      
begin       
set @intYEAR_BUILT =0      
set @YEAR_BUILT =''      
set @intBUILDING_TYPE =0      
set @BUILDING_TYPE =''      
set @intREPLACEMENT_COST =0      
set @REPLACEMENT_COSTHO5 =''      
set @REPLACEMENT_COST_POLICY_HO2_HO3 =''      
set @intCOVERAGE_A_HO2_HO3 =0      
set @REPLACEMENT_COST_POLICY_HO5 =''      
end      
            
--          
if (@intCOVERAGEA < @intREPLACEMENT_COST )                            
begin                             
set @REPLACEMENT_COST_POLICY_HO5='Y'                            
end                             
else                            
begin                             
set @REPLACEMENT_COST_POLICY_HO5='N'                            
end                 
--            
set @intCOVERAGE_A_HO2_HO3 =(0.8*@intREPLACEMENT_COST)          
      
if (@intCOVERAGEA < @intCOVERAGE_A_HO2_HO3)                            
begin                             
set @REPLACEMENT_COST_POLICY_HO2_HO3='Y'                            
end                             
else                            
begin                             
set @REPLACEMENT_COST_POLICY_HO2_HO3='N'                            
end                      
--            
if (@intREPLACEMENT_COST =@intCOVERAGEA and @intCOVERAGEA >= 125000)                            
begin                             
set @REPLACEMENT_COSTHO5='Y'                            
end                             
else                  
begin                             
set @REPLACEMENT_COSTHO5='N'                            
end                          
--            
if (@intBUILDING_TYPE =10571)                            
begin                             
set @BUILDING_TYPE='Y'                            
end                
      
if (@intBUILDING_TYPE =0)                            
begin                             
set @BUILDING_TYPE=''                            
end                             
else                            
begin                             
set @BUILDING_TYPE='N'                            
end                     
--            
if(@intYEAR_BUILT >1950)            
begin             
set @YEAR_BUILT='Y'            
end                             
      
if(@intYEAR_BUILT=0)            
begin             
set @YEAR_BUILT=''            
end       
else            
begin            
set @YEAR_BUILT='N'            
end               
            
 --APP_HOME_OWNER_GEN_INFO            
declare @ANY_COV_DECLINED_CANCELED char  --2.F.3          
declare @ANY_FARMING_BUSINESS_COND char  --2.F.6          
declare @CONVICTION_DEGREE_IN_PAST char  --2.F.8          
declare @ANIMALS_EXO_PETS_HISTORY char  --2.F.18          
declare @IS_SWIMPOLL_HOTTUB char   --2.F.23          
declare @IS_VACENT_OCCUPY char       
      
if exists(select CUSTOMER_ID from APP_HOME_OWNER_GEN_INFO            
where CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID )           
begin            
select @ANY_COV_DECLINED_CANCELED=isnull(ANY_COV_DECLINED_CANCELED,''),@ANY_FARMING_BUSINESS_COND=isnull(ANY_FARMING_BUSINESS_COND,''),            
@CONVICTION_DEGREE_IN_PAST=isnull(CONVICTION_DEGREE_IN_PAST,''),@ANIMALS_EXO_PETS_HISTORY=isnull(ANIMALS_EXO_PETS_HISTORY,''),            
@IS_SWIMPOLL_HOTTUB=isnull(IS_SWIMPOLL_HOTTUB,''),@IS_VACENT_OCCUPY=isnull(IS_VACENT_OCCUPY,'')          
from APP_HOME_OWNER_GEN_INFO            
where CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID                               
end       
else      
begin       
set @ANY_COV_DECLINED_CANCELED=''      
set @ANY_FARMING_BUSINESS_COND =''      
set @CONVICTION_DEGREE_IN_PAST=''      
set @ANIMALS_EXO_PETS_HISTORY =''      
set @IS_SWIMPOLL_HOTTUB=''      
set @IS_VACENT_OCCUPY =''      
end       
--            
if(@IS_VACENT_OCCUPY='1')            
begin             
set @IS_VACENT_OCCUPY='Y'            
end   
else if(@IS_VACENT_OCCUPY='0')   
set @IS_VACENT_OCCUPY='N'            
--            
if(@IS_SWIMPOLL_HOTTUB='1')            
begin             
set @IS_SWIMPOLL_HOTTUB='Y'         
end  
else if(@IS_SWIMPOLL_HOTTUB='0')            
begin             
set @IS_SWIMPOLL_HOTTUB='N'            
end                      
--            
if(@ANIMALS_EXO_PETS_HISTORY='1')            
begin             
set @ANIMALS_EXO_PETS_HISTORY='Y'        
end             
else if(@ANIMALS_EXO_PETS_HISTORY='0')            
begin             
set @ANIMALS_EXO_PETS_HISTORY='N'            
end        
--            
if(@ANY_COV_DECLINED_CANCELED='1')            
begin             
set @ANY_COV_DECLINED_CANCELED='Y'            
end  
else if(@ANY_COV_DECLINED_CANCELED='0')            
begin             
set @ANY_COV_DECLINED_CANCELED='N'            
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
if(@CONVICTION_DEGREE_IN_PAST='1')            
begin             
set @CONVICTION_DEGREE_IN_PAST='Y'            
end             
else if(@CONVICTION_DEGREE_IN_PAST='0')            
begin             
set @CONVICTION_DEGREE_IN_PAST='N'            
end                    
--APP_HOME_RATING_INFO         
declare @IS_UNDER_CONSTRUCTION  char  --2.G          
declare @intNO_OF_FAMILIES int  --2.A.1&2.a          
declare @NO_OF_FAMILIES char  --2.A.1&2.a          
declare @intROOF_TYPE int  --2.F.21          
declare @ROOF_TYPE char  --2.F.21         
declare @intWIRING_UPDATE_YEAR int   --2.A.2.c          
declare @WIRING_UPDATE_YEAR char  --2.A.2.c          
declare @intPLUMBING_UPDATE_YEAR int   --2.A.2.c          
declare @PLUMBING_UPDATE_YEAR char   --2.A.2.c          
declare @intHEATING_UPDATE_YEAR int   --2.A.2.c          
declare @HEATING_UPDATE_YEAR char  --2.A.2.c          
declare @intROOFING_UPDATE_YEAR int  --2.A.2.c           
declare @ROOFING_UPDATE_YEAR char  --2.A.2.c           
declare @intNO_OF_AMPS int--2.F.20        
declare @NO_OF_AMPS char --2.F.20        
            
if exists(select CUSTOMER_ID from APP_HOME_RATING_INFO            
where CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID and  DWELLING_ID =@DWELLINGID)      
begin      
select @IS_UNDER_CONSTRUCTION =isnull(IS_UNDER_CONSTRUCTION,''),        
@NO_OF_FAMILIES=isnull(NO_OF_FAMILIES,0),@intROOF_TYPE=isnull(ROOF_TYPE,0),        
@intWIRING_UPDATE_YEAR=isnull(WIRING_UPDATE_YEAR,0),@intPLUMBING_UPDATE_YEAR=isnull(PLUMBING_UPDATE_YEAR,0),                              
@intHEATING_UPDATE_YEAR=isnull(HEATING_UPDATE_YEAR,0), @intROOFING_UPDATE_YEAR = isnull(ROOFING_UPDATE_YEAR,0),        
@intNO_OF_AMPS=isnull(NO_OF_AMPS,0)            
from APP_HOME_RATING_INFO            
where CUSTOMER_ID = @CUSTOMERID AND APP_ID = @APPID AND APP_VERSION_ID = @APPVERSIONID and  DWELLING_ID =@DWELLINGID                                
end      
else      
begin       
set @IS_UNDER_CONSTRUCTION =''      
set @intNO_OF_FAMILIES =0      
set @NO_OF_FAMILIES =''      
set @intROOF_TYPE =0      
set @ROOF_TYPE =''      
set @intWIRING_UPDATE_YEAR =0      
set @WIRING_UPDATE_YEAR =''      
set @intPLUMBING_UPDATE_YEAR =0      
set @PLUMBING_UPDATE_YEAR =''      
set @intHEATING_UPDATE_YEAR =0      
set @HEATING_UPDATE_YEAR =''      
set @intROOFING_UPDATE_YEAR =0      
set @ROOFING_UPDATE_YEAR =''      
set @intNO_OF_AMPS =0      
set @NO_OF_AMPS =''      
end             
--        
if(@intNO_OF_AMPS<100)            
begin             
set @NO_OF_AMPS='Y'            
end       
if(@intNO_OF_AMPS=0)            
begin             
set @NO_OF_AMPS=''            
end             
else            
begin             
set @NO_OF_AMPS='N'            
end           
--        
if(@IS_UNDER_CONSTRUCTION='1')            
begin             
set @IS_UNDER_CONSTRUCTION='Y'            
end             
else if(@IS_UNDER_CONSTRUCTION='0')            
begin             
set @IS_UNDER_CONSTRUCTION='N'            
end             
--            
if (@intROOF_TYPE =9964)                            
begin           
set @ROOF_TYPE='Y'                            
end       
if (@intROOF_TYPE =0)                            
begin                             
set @ROOF_TYPE=''                            
end             
else                          
begin                             
set @ROOF_TYPE='N'                            
end                             
--            
if(@intNO_OF_FAMILIES >2)            
begin             
set @NO_OF_FAMILIES='Y'            
end                             
if(@intNO_OF_FAMILIES =0)            
begin             
set @NO_OF_FAMILIES=''            
end                             
else            
begin            
set @NO_OF_FAMILIES='N'            
end         
set @intWIRING_UPDATE_YEAR=(YEAR(GETDATE()) - @intWIRING_UPDATE_YEAR)             
set @intPLUMBING_UPDATE_YEAR=(YEAR(GETDATE()) - @intPLUMBING_UPDATE_YEAR)             
set @intHEATING_UPDATE_YEAR=(YEAR(GETDATE()) - @intHEATING_UPDATE_YEAR)             
set @intROOFING_UPDATE_YEAR=(YEAR(GETDATE()) - @intROOFING_UPDATE_YEAR)             
--            
if(@intWIRING_UPDATE_YEAR>10)            
begin            
set @WIRING_UPDATE_YEAR='Y'            
end             
else            
begin             
set @WIRING_UPDATE_YEAR='N'             
end            
--            
if(@intPLUMBING_UPDATE_YEAR>10)            
begin            
set @PLUMBING_UPDATE_YEAR='Y'            
end             
else            
begin             
set @PLUMBING_UPDATE_YEAR='N'             
end            
--            
if(@intHEATING_UPDATE_YEAR>10)            
begin            
set @HEATING_UPDATE_YEAR='Y'            
end             
else            
begin             
set @HEATING_UPDATE_YEAR='N'             
end            
--            
if(@intROOFING_UPDATE_YEAR>10)            
begin            
set @ROOFING_UPDATE_YEAR='Y'            
end             
else            
begin             
set @ROOFING_UPDATE_YEAR='N'             
end               
      
---clt_customer_list          
declare @intCUSTOMER_INSURANCE_SCORE int           
declare @CUSTOMER_INSURANCE_SCORE char           
      
if exists(select CUSTOMER_ID from clt_customer_list where CUSTOMER_ID = @CUSTOMERID)          
begin       
select @intCUSTOMER_INSURANCE_SCORE = isnull(CUSTOMER_INSURANCE_SCORE,0)          
from clt_customer_list where CUSTOMER_ID = @CUSTOMERID                              
end      
else      
begin      
set @intCUSTOMER_INSURANCE_SCORE =0      
set @CUSTOMER_INSURANCE_SCORE =''      
end      
--          
if(@intCUSTOMER_INSURANCE_SCORE <600)            
begin             
set @CUSTOMER_INSURANCE_SCORE='Y'            
end             
if(@intCUSTOMER_INSURANCE_SCORE=0)            
begin             
set @CUSTOMER_INSURANCE_SCORE=''            
end             
else            
begin             
set @CUSTOMER_INSURANCE_SCORE='N'            
end              
      
--APP_LIST        
declare @STATE_ID int           
declare @intPOLICY_TYPE int    
declare @POLICY_TYPE nvarchar(20)                                
    
if exists(select CUSTOMER_ID from APP_LIST         
where CUSTOMER_ID = @CUSTOMERID  AND APP_ID = @APPID AND APP_VERSION_ID=@APPVERSIONID)      
begin       
select @STATE_ID = isnull(STATE_ID,-1),@intPOLICY_TYPE=isnull(POLICY_TYPE,0)    
from APP_LIST         
where CUSTOMER_ID = @CUSTOMERID  AND APP_ID = @APPID AND APP_VERSION_ID=@APPVERSIONID      
end       
else      
begin       
set @STATE_ID=-1      
set @intPOLICY_TYPE=0   
set @POLICY_TYPE='HO'  
end     
    
if(@intPOLICY_TYPE=11148)    
begin     
set @POLICY_TYPE='HO-3'    
end     
else  
begin   
set @POLICY_TYPE='HO'  
end   
    
if(@intPOLICY_TYPE=11149)    
begin     
set @POLICY_TYPE='HO-5'    
end     
else  
begin   
set @POLICY_TYPE='HO'  
end   
    
if(@intPOLICY_TYPE=11192)    
begin     
set @POLICY_TYPE='HO-2'    
end     
else  
begin   
set @POLICY_TYPE='HO'  
end   
    
if(@intPOLICY_TYPE=11195)    
begin     
set @POLICY_TYPE='HO-4'    
end     
else  
begin   
set @POLICY_TYPE='HO'  
end   
    
if(@intPOLICY_TYPE=11196)    
begin     
set @POLICY_TYPE='HO-6'    
end     
else  
begin   
set @POLICY_TYPE='HO'  
end   
    
if(@intPOLICY_TYPE=0)    
begin     
set @POLICY_TYPE=''    
end     
  
    
          
select             
@NO_OF_FAMILIES as NO_OF_FAMILIES,            
@ROOF_TYPE as ROOF_TYPE,            
@YEAR_BUILT as YEAR_BUILT ,             
@BUILDING_TYPE as BUILDING_TYPE,            
@WIRING_UPDATE_YEAR as WIRING_UPDATE_YEAR,            
@PLUMBING_UPDATE_YEAR as PLUMBING_UPDATE_YEAR ,            
@HEATING_UPDATE_YEAR as HEATING_UPDATE_YEAR ,            
@ROOFING_UPDATE_YEAR as ROOFING_UPDATE_YEAR,            
@COVERAGEA as COVERAGE_A ,            
@COVAC_HO6 as  COVAC_HO6,  --HO-6          
@ANY_COV_DECLINED_CANCELED as ANY_COV_DECLINED_CANCELED ,            
@ANY_FARMING_BUSINESS_COND as ANY_FARMING_BUSINESS_COND,             
@CONVICTION_DEGREE_IN_PAST as CONVICTION_DEGREE_IN_PAST,       
@ANIMALS_EXO_PETS_HISTORY as ANIMALS_EXO_PETS_HISTORY ,             
@IS_SWIMPOLL_HOTTUB as IS_SWIMPOLL_HOTTUB,             
@IS_UNDER_CONSTRUCTION as IS_UNDER_CONSTRUCTION,            
@REPLACEMENT_COSTHO5 as REPLACEMENT_COST_HO5 ,          
@REPLACEMENT_COST_POLICY_HO2_HO3 as REPLACEMENT_COST_POLICY_HO2_HO3, -- 2.F.2.a for HO-2 ,HO-3          
@REPLACEMENT_COST_POLICY_HO5 as REPLACEMENT_COST_POLICY_HO5 ,--HO-5           
@intPERSONAL_PROP_LIMIT as COVERAGE_C,          
@intREPLACEMENT_COST as REPLACEMENT_COST,          
@CUSTOMER_INSURANCE_SCORE as CUSTOMER_INSURANCE_SCORE ,        
@NO_OF_AMPS as NO_OF_AMPS,        
@IS_VACENT_OCCUPY as IS_VACENT_OCCUPY,        
@STATE_ID as STATE_ID,    
@POLICY_TYPE as POLICY_TYPE     
            
set quoted_identifier on                              
END                  
                
                
                
                
                
              
              
           
          
        
      
      
    
    
  
  



GO

