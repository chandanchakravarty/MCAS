IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHORule_DwellingCoverageInfo_TEST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHORule_DwellingCoverageInfo_TEST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* ----------------------------------------------------------                                                                        
Proc Name                : Dbo.Proc_GetHORule_DwellingCoverageInfo                                                                      
Created by               : Ashwani                                                                        
Date                     : 01 Dec.,2005                        
Purpose                  : To get the dwelling coverage detail for HO rules                        
Revison History          :                                                                        
Used In                  : Wolverine                                                                        
------------------------------------------------------------                                                                        
Date     Review By          Comments                                                                        
------   ------------       -------------------------*/                                                                        
                 
create proc Dbo.Proc_GetHORule_DwellingCoverageInfo_TEST                        
(                                                                        
@CUSTOMERID    int,                                                                        
@APPID    int,                                                                        
@APPVERSIONID   int,                        
@DWELLINGID int,                                    
@DESC varchar(10)                                                          
)                                                                        
as                                                                            
begin                           
 -- Mandatory                          
 declare @DECDWELLING_LIMIT decimal                      
 declare @DWELLING_LIMIT char  -- HO-6                     
 declare @PERSONAL_LIAB_LIMIT varchar(50)                      
 declare @MED_PAY_EACH_PERSON varchar(50)                    
 --Rule                   
 declare @DECPERSONAL_PROP_LIMIT decimal -- HO-6                  
 declare @PERSONAL_PROP_LIMIT char                  
 declare @COVAC_HO6 char --HO-6         
 declare @INTREPLACEMENT_COST int                 
                      
                        
 if exists (select CUSTOMER_ID from APP_DWELLING_COVERAGE                                           
  where CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID and DWELLING_ID=@DWELLINGID)                        
 begin                         
  select @DECDWELLING_LIMIT= isnull(DWELLING_LIMIT,-1),@PERSONAL_LIAB_LIMIT=isnull(convert(varchar(50),PERSONAL_LIAB_LIMIT),''),                        
  @MED_PAY_EACH_PERSON=isnull(convert(varchar(50),MED_PAY_EACH_PERSON),''),@DECPERSONAL_PROP_LIMIT=isnull(PERSONAL_PROP_LIMIT,-1)                        
 from APP_DWELLING_COVERAGE                        
 where CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID and DWELLING_ID=@DWELLINGID                        
                        
 end                        
else                        
begin                         
 set @DWELLING_LIMIT =''                        
 set @PERSONAL_LIAB_LIMIT =''                        
 set @MED_PAY_EACH_PERSON =''                   
 set @DECDWELLING_LIMIT=-1                
end                         
                  
-- Policy Type                  
                  
 declare @INTPOLICY_TYPE int                  
                  
 select @INTPOLICY_TYPE=isnull(POLICY_TYPE,0)                  
 from APP_LIST                   
 where CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID                  
                  
---------------------------------------------------------------------------------      
      
 select @INTREPLACEMENT_COST=isnull(REPLACEMENT_COST,0)                  
 from APP_DWELLINGS_INFO                   
 where CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID and DWELLING_ID=@DWELLINGID       
      
 if(@INTPOLICY_TYPE=11193 or @INTPOLICY_TYPE=11194 or @INTPOLICY_TYPE=11403 or @INTPOLICY_TYPE=11404)-- HO-2 & HO-3                  
begin                   
 declare @INTCOVERAGE_A_HO2_HO3 int                  
 declare @REPLACEMENT_COST_POLICY_HO2_HO3 char              
                  
                       
                          
 set @INTCOVERAGE_A_HO2_HO3 =(0.8*@INTREPLACEMENT_COST)                              
                           
 if (@DECDWELLING_LIMIT < @intCOVERAGE_A_HO2_HO3)                                  
 begin                                                 
  set @REPLACEMENT_COST_POLICY_HO2_HO3='Y'                                                
 end                                                 
 else                                                
 begin                      
  set @REPLACEMENT_COST_POLICY_HO2_HO3='N'                                                
 end                                    
end                   
else                  
begin                   
 set @REPLACEMENT_COST_POLICY_HO2_HO3='N'                  
                   
end                   
--                  
 --HO -5 only                   
 declare @REPLACEMENT_COST_POLICY_HO5 char                  
                   
 if(@INTPOLICY_TYPE=11149 or @INTPOLICY_TYPE=11401)                  
 begin                  
 if (@DECDWELLING_LIMIT < @INTREPLACEMENT_COST )                                                
 begin                                                 
  set @REPLACEMENT_COST_POLICY_HO5='Y'                                                
 end                                                 
 else                                                
 begin                                                 
  set @REPLACEMENT_COST_POLICY_HO5='N'                                                
 end                   
 end                   
 else                  
 begin                   
 set @REPLACEMENT_COST_POLICY_HO5='N'                   
 end                         
                  
                  
                   
-- HO-6                  
if(@INTPOLICY_TYPE=11196 or @INTPOLICY_TYPE=11406)                   
 begin                   
 declare @INTMINCOVC decimal                  
 -- For HO6 (10% of Covg C)                                 
 set @INTMINCOVC=(0.1 * @DECPERSONAL_PROP_LIMIT)                
             
  if(@DECDWELLING_LIMIT< @INTMINCOVC OR @DECDWELLING_LIMIT<2000)                        
  begin                                 
   set @COVAC_HO6='Y'                                
  end                                 
  else                                
  begin                                 
   set @COVAC_HO6='N'                                
  end                      
end                  
else                   
begin                   
 set  @COVAC_HO6='N'                  
 set @PERSONAL_PROP_LIMIT='N'                  
end                      
                  
--                  
 if(@DECPERSONAL_PROP_LIMIT=-1)                        
 begin                   
 set @PERSONAL_PROP_LIMIT=''                  
 end                   
 else                  
 begin                   
 set @PERSONAL_PROP_LIMIT='N'                  
 end                   
--                      
 if(@DECDWELLING_LIMIT > 400000)                      
 begin                       
   set @DWELLING_LIMIT='Y'                      
 end                      
 else if(@DECDWELLING_LIMIT=-1)                     
  begin                       
     set @DWELLING_LIMIT=''                      
  end                      
 else if(@DECDWELLING_LIMIT<>0)                     
  begin                       
    set @DWELLING_LIMIT='N'                      
         end                      
-------------------------------------------------------------------------------------------------------------------                      
--Coverage A in case of HO2, 3, 5 regular and premier cannot be greater than 100% of Replacement Cost          
-- 11192,11402  -- HO-2, 11148,11400,11409 -- HO-3, 11149,11401,11410 -- HO-5          
declare @COVA_NOT_REPCOST char       
      
if(@INTPOLICY_TYPE=11192 or @INTPOLICY_TYPE=11402 or @INTPOLICY_TYPE=11148 or @INTPOLICY_TYPE=11400 or @INTPOLICY_TYPE=11149 or @INTPOLICY_TYPE=11401 or @INTPOLICY_TYPE=11409 or @INTPOLICY_TYPE=11410)                   
begin           
 if(@DECDWELLING_LIMIT>@INTREPLACEMENT_COST)          
 begin           
 set @COVA_NOT_REPCOST='Y'          
 end           
 else       
 begin           
  set @COVA_NOT_REPCOST='N'          
 end           
end           
else          
begin           
 set @COVA_NOT_REPCOST='N'          
end           
--          
if(@COVA_NOT_REPCOST='Y')          
begin           
 set @COVA_NOT_REPCOST=''           
end           
          
          
-------------------------------------------------------------------------------------------------------------------                      
--Coverage A in case of HO2, 3 Repair must be equal to 100% of Market Value          
          
 declare @COVA_NOT_MAKVALUE char          
          
 -- Market Value          
 declare @DECMARKET_VALUE decimal          
          
 select @DECMARKET_VALUE=isnull(MARKET_VALUE,0)          
 from APP_DWELLINGS_INFO                                  
 where CUSTOMER_ID=@CUSTOMERID and APP_ID= @APPID and APP_VERSION_ID = @APPVERSIONID and DWELLING_ID=@DWELLINGID            
          
-- HO-2 Repair Cost - 11193,11403,HO-3 Repair Cost - 11194,11404          
 if(@INTPOLICY_TYPE=11193 or @INTPOLICY_TYPE=11403 or @INTPOLICY_TYPE=11194 or @INTPOLICY_TYPE=11404)          
 begin           
 if(@DECDWELLING_LIMIT <> @DECMARKET_VALUE)          
 begin           
  set @COVA_NOT_MAKVALUE ='Y'          
 end           
 else          
 begin           
  set @COVA_NOT_MAKVALUE ='N'          
 end           
 end           
 else          
 begin           
 set @COVA_NOT_MAKVALUE ='N'          
 end           
---          
 if(@COVA_NOT_MAKVALUE='Y')          
 begin           
 set @COVA_NOT_MAKVALUE=''          
 end           
          
-------------------------------------------------------------------------------------------------------------------                      
--Coverage C in case of HO4, 6 Reg and Delux cannot be greater than 100% of Replacement Cost        
-- HO-4 11195,11405,HO-4 Deluxe 11245,11407, HO-6 Deluxe 11246,11408, HO-6 11196,11406        
        
 declare  @COVC_NOT_REPCOST char        
         
if(@INTPOLICY_TYPE=11195 or @INTPOLICY_TYPE=11405 or @INTPOLICY_TYPE=11245 or @INTPOLICY_TYPE=11407 or @INTPOLICY_TYPE=11246 or @INTPOLICY_TYPE=11408 or @INTPOLICY_TYPE=11196 or @INTPOLICY_TYPE=11406)          
begin         
 if(@DECPERSONAL_PROP_LIMIT > @INTREPLACEMENT_COST)        
  begin         
   set @COVC_NOT_REPCOST='Y'        
  end        
 else        
  begin         
   set @COVC_NOT_REPCOST='N'      
  end        
end        
else        
begin         
 set @COVC_NOT_REPCOST='N'        
end        
      
      
      
 ----------        
 if(@COVC_NOT_REPCOST='Y')        
 begin         
   set @COVC_NOT_REPCOST=''        
 end                    
    
-----------------------------------------------------------------------------------------------------    
-- Coverage A is not mandatory in case of HO-4=11195, Ho-4 Deluxe= 11245    
-- Modified on 27 Feb. 2006    
if(@INTPOLICY_TYPE=11195 or @INTPOLICY_TYPE=11245 or @INTPOLICY_TYPE=11405 or @INTPOLICY_TYPE=11407 )    
begin     
 if(@DWELLING_LIMIT='')    
 begin    
  set @DWELLING_LIMIT='N'    
  end      
end     
                        
select                        
-- Mandatory                        
 @DWELLING_LIMIT as  DWELLING_LIMIT,                        
 @PERSONAL_LIAB_LIMIT as PERSONAL_LIAB_LIMIT,                        
 @MED_PAY_EACH_PERSON as MED_PAY_EACH_PERSON ,                  
 -- Rule                  
 @PERSONAL_PROP_LIMIT as PERSONAL_PROP_LIMIT,    --HO-6                  
 @COVAC_HO6 as COVAC_HO6, -- HO-6                    
 @REPLACEMENT_COST_POLICY_HO2_HO3 as REPLACEMENT_COST_POLICY_HO2_HO3, -- HO-2 & HO-3                  
 @REPLACEMENT_COST_POLICY_HO5 as REPLACEMENT_COST_POLICY_HO5, -- HO-5                  
 @COVA_NOT_REPCOST as  COVA_NOT_REPCOST,          
 @COVA_NOT_MAKVALUE as COVA_NOT_MAKVALUE,        
 @COVC_NOT_REPCOST as COVC_NOT_REPCOST           
                   
                  
end                                              
                         
      
                                        
                                      
                                        
                                        
                                                        
                                        
                           
                                        
                                        
                                        
                                        
                                        
                                         
                                        
                                        
                                        
                                        
                                        
                     
                                        
                                        
                                        
                                        
                                     
                                        
            
                                        
                     
                                        
                                      
                                    
                                  
                                
                            
                            
                          
                        
                      
                    
                  
                  
                
              
              
              
            
          
          
          
          
        
      
      
    
  




GO

