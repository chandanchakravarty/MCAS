IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRentalDwellingRule_temp]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRentalDwellingRule_temp]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--Proc_GetRentalDwellingRule_temp 99,1,1,1        
/*----------------------------------------------------------                          
        
Proc Name                : Dbo.Proc_GetRentalDwellingRule_temp                        
Created by               : Ashwani                          
Date                     : 30 Sep.,2005                        
Purpose                  : To get the Rule Information for Rental Dwelling          
Revison History          :                          
Used In                  : Wolverine                          
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                          
CREATE proc Dbo.Proc_GetRentalDwellingRule_temp                          
(                          
@CUSTOMER_ID int,                          
@APP_ID int,                          
@APPVERSION_ID int,          
@DWELLING_ID int          
)                          
as                            
begin                     
--CLT_CUSTOMER_LIST          
declare @intCUSTOMER_INSURANCE_SCORE int          
declare @CUSTOMER_INSURANCE_SCORE char      
declare @CUSTOMER_INSURANCE_SCORE_HOME char      
if exists(select Customer_ID from  CLT_CUSTOMER_LIST   where Customer_ID=@CUSTOMER_ID )        
begin         
select @intCUSTOMER_INSURANCE_SCORE=isnull(CUSTOMER_INSURANCE_SCORE,0)     
from  CLT_CUSTOMER_LIST           
where Customer_ID=@CUSTOMER_ID          
end        
else        
begin         
set @CUSTOMER_INSURANCE_SCORE=''        
set @intCUSTOMER_INSURANCE_SCORE=0    
set @CUSTOMER_INSURANCE_SCORE_HOME=''    
end        
--    
if(@intCUSTOMER_INSURANCE_SCORE<550)          
begin           
set @CUSTOMER_INSURANCE_SCORE='Y'          
end   
else if(@intCUSTOMER_INSURANCE_SCORE<>0)          
begin           
set @CUSTOMER_INSURANCE_SCORE='N'          
end if(@intCUSTOMER_INSURANCE_SCORE=0)      
begin   
set @CUSTOMER_INSURANCE_SCORE=''          
end  
--      
if(@intCUSTOMER_INSURANCE_SCORE<600)          
begin           
set @CUSTOMER_INSURANCE_SCORE_HOME='Y'          
end   
else if(@intCUSTOMER_INSURANCE_SCORE<>0)          
begin           
set @CUSTOMER_INSURANCE_SCORE_HOME='N'          
end         
else if(@intCUSTOMER_INSURANCE_SCORE=0)   
begin         
set @CUSTOMER_INSURANCE_SCORE_HOME=''          
end         
--APP_DWELLINGS_INFO          
declare @intOCCUPANCY int          
declare @OCCUPANCY char          
declare @intYEAR_BUILT int          
declare @YEAR_BUILT char -- for Rule 1.A.20          
if exists (select APP_ID from APP_DWELLINGS_INFO  where CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APPVERSION_ID and DWELLING_ID=@DWELLING_ID)        
begin           
select @intOCCUPANCY=isnull(OCCUPANCY,0),  @intYEAR_BUILT =isnull(YEAR_BUILT,0)          
from APP_DWELLINGS_INFO           
where CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APPVERSION_ID and DWELLING_ID=@DWELLING_ID          
end         
else        
begin         
set @OCCUPANCY=''        
set @YEAR_BUILT=''     
set @intOCCUPANCY=0       
set @intYEAR_BUILT=0    
end    
--          
if(@intOCCUPANCY=8962)          
begin           
set @OCCUPANCY='Y'          
end   
else if(@intOCCUPANCY=0)          
begin           
set @OCCUPANCY=''          
end          
else if(@intOCCUPANCY<>0)          
begin           
set @OCCUPANCY='N'          
end  
--          
if(@intYEAR_BUILT<1950 and @intCUSTOMER_INSURANCE_SCORE<600)           
begin           
set @YEAR_BUILT='Y'          
end           
if(@intYEAR_BUILT=0 or @intCUSTOMER_INSURANCE_SCORE=0)           
begin           
set @YEAR_BUILT=''          
end           
else          
begin           
set @YEAR_BUILT='N'          
end           
--APP_HOME_OWNER_GEN_INFO          
declare @ANY_FARMING_BUSINESS_COND char          
declare @IS_VACENT_OCCUPY char          
declare @IS_SWIMPOLL_HOTTUB char          
declare @CONVICTION_DEGREE_IN_PAST char          
declare @ANY_HEATING_SOURCE char          
declare @RENTERS char          
declare @ANY_COV_DECLINED_CANCELED char        
if exists(select APP_ID from  APP_HOME_OWNER_GEN_INFO where CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID           
and APP_VERSION_ID=@APPVERSION_ID)         
begin         
select @ANY_FARMING_BUSINESS_COND =isnull(ANY_FARMING_BUSINESS_COND,0) ,          
@IS_VACENT_OCCUPY=isnull(IS_VACENT_OCCUPY,0),@IS_SWIMPOLL_HOTTUB=isnull(IS_SWIMPOLL_HOTTUB,0),          
@CONVICTION_DEGREE_IN_PAST=isnull(CONVICTION_DEGREE_IN_PAST,0),          
@ANY_HEATING_SOURCE=isnull(ANY_HEATING_SOURCE,0),@RENTERS =isnull(RENTERS,0)  ,        
@ANY_COV_DECLINED_CANCELED=isnull(ANY_COV_DECLINED_CANCELED,0)        
from  APP_HOME_OWNER_GEN_INFO          
where CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APPVERSION_ID           
end        
else        
begin         
set @ANY_FARMING_BUSINESS_COND=''        
set @IS_VACENT_OCCUPY=''        
set @IS_SWIMPOLL_HOTTUB=''        
set @CONVICTION_DEGREE_IN_PAST=''        
set @ANY_HEATING_SOURCE=''        
set @RENTERS=''        
set @ANY_COV_DECLINED_CANCELED=''        
end        
--          
if(@ANY_FARMING_BUSINESS_COND='1')          
begin           
set @ANY_FARMING_BUSINESS_COND='Y'          
end   
else if(@ANY_FARMING_BUSINESS_COND='')          
begin           
set @ANY_FARMING_BUSINESS_COND=''          
end          
else if(@ANY_FARMING_BUSINESS_COND<>'')          
begin           
set @ANY_FARMING_BUSINESS_COND='N'          
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
if(@IS_SWIMPOLL_HOTTUB='1')          
begin           
set @IS_SWIMPOLL_HOTTUB='Y'          
end  
else if(@IS_SWIMPOLL_HOTTUB='0')          
begin           
set @IS_SWIMPOLL_HOTTUB='N'          
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
--   
if(@ANY_HEATING_SOURCE='1')          
begin           
set @ANY_HEATING_SOURCE='Y'          
end  
else if(@ANY_HEATING_SOURCE='0')          
begin           
set @ANY_HEATING_SOURCE='N'          
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
if(@ANY_COV_DECLINED_CANCELED='1')          
begin           
set @ANY_COV_DECLINED_CANCELED='Y'          
end  
else if(@ANY_COV_DECLINED_CANCELED='0')          
begin           
set @ANY_COV_DECLINED_CANCELED='N'          
end       
--APP_HOME_RATING_INFO          
declare @NO_OF_FAMILIES char          
declare @intNO_OF_FAMILIES int           
declare @NO_OF_AMPS char          
declare @intNO_OF_AMPS int          
if exists( select APP_ID from  APP_HOME_RATING_INFO where CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID           
and APP_VERSION_ID=@APPVERSION_ID and DWELLING_ID=@DWELLING_ID)        
begin           
select @intNO_OF_FAMILIES=isnull(NO_OF_FAMILIES,0),@intNO_OF_AMPS =isnull(NO_OF_AMPS ,0)          
from  APP_HOME_RATING_INFO           
where CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID           
and APP_VERSION_ID=@APPVERSION_ID and DWELLING_ID=@DWELLING_ID          
end         
else        
begin         
set @NO_OF_FAMILIES=''        
set @NO_OF_AMPS=''     
set @intNO_OF_FAMILIES=0    
set @intNO_OF_AMPS  =0    
end         
--  
if(@intNO_OF_FAMILIES>2)          
begin           
set @NO_OF_FAMILIES='Y'          
end  
else if(@intNO_OF_FAMILIES<>0)          
begin           
set @NO_OF_FAMILIES='N'          
end          
--  
if(@intNO_OF_AMPS>100)          
begin           
set @NO_OF_AMPS='Y'          
end  
else if(@intNO_OF_AMPS<>0)          
begin           
set @NO_OF_AMPS='N'          
end        
select           
@CUSTOMER_INSURANCE_SCORE as CUSTOMER_INSURANCE_SCORE,          
@OCCUPANCY as OWNER_OCCUPIED,          
@YEAR_BUILT as HOME_BUILD,          
@ANY_FARMING_BUSINESS_COND as ANY_FARMING_BUSINESS_COND,          
@IS_VACENT_OCCUPY as IS_VACENT_OCCUPY,          
@IS_SWIMPOLL_HOTTUB as  IS_SWIMPOLL_HOTTUB ,          
@NO_OF_FAMILIES as NO_OF_FAMILIES,          
@ANY_HEATING_SOURCE as ANY_HEATING_SOURCE,          
@RENTERS as RENTERS,          
@CONVICTION_DEGREE_IN_PAST as CONVICTION_DEGREE_IN_PAST,          
@NO_OF_AMPS as NO_OF_AMPS,        
@ANY_COV_DECLINED_CANCELED as ANY_COV_DECLINED_CANCELED,      
@CUSTOMER_INSURANCE_SCORE_HOME  as CUSTOMER_INSURANCE_SCORE_HOME      
end          
    
    
    
    
  
  



GO

