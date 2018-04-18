IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetWatercraftRule_GenInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetWatercraftRule_GenInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/* ----------------------------------------------------------                                                      
Proc Name                : Dbo.Proc_GetWatercraftRule_GenInfo  920,56,1                                                 
Created by               : Ashwani                                                  
Date                     : 02 Mar 2006                  
Purpose                  : To get the Watercraft Gen Information for application rules                                                 
Revison History          :                                                      
Used In                  : Wolverine                                                      
------------------------------------------------------------                                                      
Date     Review By          Comments                                                      
------   ------------       -------------------------*/  
--drop proc dbo.Proc_GetWatercraftRule_GenInfo           
CREATE PROCEDURE dbo.Proc_GetWatercraftRule_GenInfo                    
(                                                                
@CUSTOMERID    int,                                                                
@APPID    int,                                                                
@APPVERSIONID   int                                                  
)                                                                
as                                                                    
begin                                 
--1                                
--declare @HAS_CURR_ADD_THREE_YEARS char                                     
--declare @HAS_CURR_ADD_THREE_YEARS_DESC varchar(50)                                
--2                                 
declare @PHY_MENTL_CHALLENGED char                                                      
declare @PHY_MENTL_CHALLENGED_DESC varchar(50)                                
--3                                
declare @DRIVER_SUS_REVOKED char                                 
declare @DRIVER_SUS_REVOKED_DESC varchar(50)                                
--4                                
declare @IS_CONVICTED_ACCIDENT char                                                      
declare @IS_CONVICTED_ACCIDENT_DESC varchar(50)                                
--5                               
declare @DRINK_DRUG_VOILATION char                                      
--6                                 
declare @MINOR_VIOLATION char                                                      
--7                                
/*declare @ANY_OTH_INSU_COMP char                                                      
declare @OTHER_POLICY_NUMBER_LIST varchar(50)*/      
--8                                 
declare @ANY_LOSS_THREE_YEARS char                                                      
declare @ANY_LOSS_THREE_YEARS_DESC varchar(50)                                
--9                                
declare @COVERAGE_DECLINED char                            
declare @COVERAGE_DECLINED_DESC varchar(50)                             
--10                                 
--declare @IS_CREDIT char                                   
--declare @CREDIT_DETAILS varchar(50)                                
--11                                
declare @IS_RENTED_OTHERS char                                                      
declare  @IS_RENTED_OTHERS_DESC varchar(50)                                
--12                                
declare @IS_REGISTERED_OTHERS char                                                      
declare @IS_REGISTERED_OTHERS_DESC varchar(50)                                
--13                                
declare @PARTICIPATE_RACE nchar(1)                                  
declare @PARTICIPATE_RACE_DESC varchar(50)                   
--14                                
declare @CARRY_PASSENGER_FOR_CHARGE char                            
declare @CARRY_PASSENGER_FOR_CHARGE_DESC varchar(50)                                
--15                              
declare @IS_PRIOR_INSURANCE_CARRIER char               
declare @PRIOR_INSURANCE_CARRIER_DESC varchar(50)                           
                
                      
-- 17                   
declare @IS_BOAT_COOWNED nchar(1)                  
declare @IS_BOAT_COOWNED_DESC nvarchar(100)                  
                
--18                 
declare @ANY_BOAT_AMPHIBIOUS nchar(1)                  
declare @ANY_BOAT_AMPHIBIOUS_DESC nvarchar(100)                  
              
--19              
declare @MULTI_POLICY_DISC_APPLIED nchar(1)                  
declare @MULTI_POLICY_DISC_APPLIED_DESC nvarchar(100)              
          
--20          
declare @ANY_BOAT_RESIDENCE nchar(1)          
declare @ANY_BOAT_RESIDENCE_DESC nvarchar(100)          
--21 
declare @IS_BOAT_USED_IN_ANY_WATER nchar(1)
declare @IS_BOAT_USED_IN_ANY_WATER_DESC nvarchar(100)             
                  
                      
                  
declare @IS_RECORD_EXISTS char                       
                            
                               
if exists(select CUSTOMER_ID from APP_WATERCRAFT_GEN_INFO where                                                       
 CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID)                                              
begin                                   
set @IS_RECORD_EXISTS='N'                      
select                      
--mANDATORY FIELDS                             
                                  
@PHY_MENTL_CHALLENGED=isnull(PHY_MENTL_CHALLENGED,''),@PHY_MENTL_CHALLENGED_DESC=isnull(PHY_MENTL_CHALLENGED_DESC,''),                                
@DRIVER_SUS_REVOKED =isnull(DRIVER_SUS_REVOKED,''),@DRIVER_SUS_REVOKED_DESC =isnull(DRIVER_SUS_REVOKED_DESC,''),                                
@IS_CONVICTED_ACCIDENT =isnull(IS_CONVICTED_ACCIDENT,''),@IS_CONVICTED_ACCIDENT_DESC=isnull(IS_CONVICTED_ACCIDENT_DESC,''),                            
@DRINK_DRUG_VOILATION =isnull(DRINK_DRUG_VOILATION,''),@MINOR_VIOLATION =isnull(MINOR_VIOLATION,''),                                
--@ANY_OTH_INSU_COMP =isnull(ANY_OTH_INSU_COMP,''),@OTHER_POLICY_NUMBER_LIST =isnull(OTHER_POLICY_NUMBER_LIST,''),                                
@ANY_LOSS_THREE_YEARS =isnull(ANY_LOSS_THREE_YEARS ,''),@ANY_LOSS_THREE_YEARS_DESC =isnull(ANY_LOSS_THREE_YEARS_DESC,''),                                
@COVERAGE_DECLINED =isnull(COVERAGE_DECLINED,''),@COVERAGE_DECLINED_DESC =isnull(COVERAGE_DECLINED_DESC,''),                                
--@IS_CREDIT =isnull(IS_CREDIT,''),@CREDIT_DETAILS =isnull(CREDIT_DETAILS,''),                                
@IS_RENTED_OTHERS =isnull(IS_RENTED_OTHERS,''),@IS_RENTED_OTHERS_DESC =isnull(IS_RENTED_OTHERS_DESC,''),                                
@IS_REGISTERED_OTHERS =isnull(IS_REGISTERED_OTHERS,''),@IS_REGISTERED_OTHERS_DESC =isnull(IS_REGISTERED_OTHERS_DESC,''),                                
@PARTICIPATE_RACE =isnull(PARTICIPATE_RACE,''),@PARTICIPATE_RACE_DESC =isnull(PARTICIPATE_RACE_DESC ,''),                                
@CARRY_PASSENGER_FOR_CHARGE =isnull(CARRY_PASSENGER_FOR_CHARGE,''),@CARRY_PASSENGER_FOR_CHARGE_DESC =isnull(CARRY_PASSENGER_FOR_CHARGE_DESC ,''),                                
@IS_PRIOR_INSURANCE_CARRIER=isnull(IS_PRIOR_INSURANCE_CARRIER,''),@PRIOR_INSURANCE_CARRIER_DESC=isnull(PRIOR_INSURANCE_CARRIER_DESC,''),                          
@IS_BOAT_COOWNED=isnull(IS_BOAT_COOWNED,''),@IS_BOAT_COOWNED_DESC=isnull(IS_BOAT_COOWNED_DESC,'') ,              
@ANY_BOAT_AMPHIBIOUS=isnull(ANY_BOAT_AMPHIBIOUS,''),@ANY_BOAT_AMPHIBIOUS_DESC=isnull(ANY_BOAT_AMPHIBIOUS_DESC,''),              
@MULTI_POLICY_DISC_APPLIED=isnull(MULTI_POLICY_DISC_APPLIED,''),@MULTI_POLICY_DISC_APPLIED_DESC=isnull(MULTI_POLICY_DISC_APPLIED_PP_DESC,''),          
@ANY_BOAT_RESIDENCE=isnull(ANY_BOAT_RESIDENCE,''),@ANY_BOAT_RESIDENCE_DESC=isnull(ANY_BOAT_RESIDENCE_DESC,''),
@IS_BOAT_USED_IN_ANY_WATER=isnull(IS_BOAT_USED_IN_ANY_WATER,''),
@IS_BOAT_USED_IN_ANY_WATER_DESC=isnull(IS_BOAT_USED_IN_ANY_WATER_DESC,'')                          
 
from APP_WATERCRAFT_GEN_INFO                                
where CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID                            
end   
else                                              
begin              
set @IS_RECORD_EXISTS='Y'                      
--1                                
--set @HAS_CURR_ADD_THREE_YEARS  =''                                     
--set @HAS_CURR_ADD_THREE_YEARS_DESC  =''                                 
--2                                 
set @PHY_MENTL_CHALLENGED  =''                                 
set @PHY_MENTL_CHALLENGED_DESC  =''                                 
--3                                
set @DRIVER_SUS_REVOKED  =''       -- if 'y'  should be referred to underwriter                                  
set @DRIVER_SUS_REVOKED_DESC  =''                                 
--4                                
set @IS_CONVICTED_ACCIDENT  =''                                 
set @IS_CONVICTED_ACCIDENT_DESC  =''                                 
--5                    
set @DRINK_DRUG_VOILATION =''           
--6                                
set @MINOR_VIOLATION  =''                                 
--7                                 
--set @ANY_OTH_INSU_COMP  =''                                 
--set @OTHER_POLICY_NUMBER_LIST  =''                                 
--8                                
set @ANY_LOSS_THREE_YEARS  =''                                 
set @ANY_LOSS_THREE_YEARS_DESC  =''                                 
--9                                 
set @COVERAGE_DECLINED  =''                             set @COVERAGE_DECLINED_DESC  =''                                 
--10                                
--set @IS_CREDIT  =''                                 
--set @CREDIT_DETAILS  =''                                 
--11                                
set @IS_RENTED_OTHERS  =''                                 
set @IS_RENTED_OTHERS_DESC  =''                                 
--12                                
set @IS_REGISTERED_OTHERS  =''                                 
set @IS_REGISTERED_OTHERS_DESC  =''                                 
--13                                
set @PARTICIPATE_RACE  =''                                 
set @PARTICIPATE_RACE_DESC  =''                                 
--14                                
set @CARRY_PASSENGER_FOR_CHARGE  =''                                 
set @CARRY_PASSENGER_FOR_CHARGE_DESC  =''                               
--15                              
set @IS_PRIOR_INSURANCE_CARRIER =''                          
set @PRIOR_INSURANCE_CARRIER_DESC =''                          
--16                    
set @IS_BOAT_COOWNED_DESC =''                  
set @IS_BOAT_COOWNED=''                  
                
--17                
set @ANY_BOAT_AMPHIBIOUS=''                
set @ANY_BOAT_AMPHIBIOUS_DESC=''                            
              
--18                              
set @MULTI_POLICY_DISC_APPLIED=''              
set @MULTI_POLICY_DISC_APPLIED_DESC=''              
          
--19          
set @ANY_BOAT_RESIDENCE=''          
set @ANY_BOAT_RESIDENCE_DESC=''  
--20
set @IS_BOAT_USED_IN_ANY_WATER_DESC=''
set @IS_BOAT_USED_IN_ANY_WATER=''       
                            
end                                     
                             
-- 1                                                       
--if(@HAS_CURR_ADD_THREE_YEARS='1')                            
--begin                                                       
--set @HAS_CURR_ADD_THREE_YEARS='Y'                            
--end                                            
--else if(@HAS_CURR_ADD_THREE_YEARS='0')                                                      
--begin                                                       
--set @HAS_CURR_ADD_THREE_YEARS='N'                                                      
--end            
-- 2            
if(@PHY_MENTL_CHALLENGED='1')                                                      
begin                                                       
set @PHY_MENTL_CHALLENGED ='Y'                                       
end                                            
else if(@PHY_MENTL_CHALLENGED ='0')                                   
begin                                                       
set @PHY_MENTL_CHALLENGED ='N'                                                      
end                                          
-- 3                                
if(@DRIVER_SUS_REVOKED='1')                                                      
begin               
set @DRIVER_SUS_REVOKED='Y'                                                      
end                                            
else if(@DRIVER_SUS_REVOKED='0')                                                      
begin                                                       
set @DRIVER_SUS_REVOKED='N'                                                      
end                                          
-- 4                                
if(@IS_CONVICTED_ACCIDENT ='1')                                                      
begin                                                       
set @IS_CONVICTED_ACCIDENT ='Y'                                             
end                                            
else if(@IS_CONVICTED_ACCIDENT ='0')                                                      
begin                                                       
set @IS_CONVICTED_ACCIDENT ='N'                  
end                
-- 5                                
if(@DRINK_DRUG_VOILATION ='1')                                                      
begin                                                       
set @DRINK_DRUG_VOILATION ='Y'                                                      
end                                            
else if(@DRINK_DRUG_VOILATION ='0')                                   
begin                                           
set @DRINK_DRUG_VOILATION ='N'                                                      
end                                 
-- 6                                              
if(@MINOR_VIOLATION ='1')                                                      
begin                                                       
set @MINOR_VIOLATION ='Y'                                                      
end                                            
else if(@MINOR_VIOLATION ='0')                                                      
begin                                            
set @MINOR_VIOLATION ='N'                                               
end                                                
-- 7                                
/*if(@ANY_OTH_INSU_COMP ='1')                                                      
begin                                                       
set @ANY_OTH_INSU_COMP='Y'                                                      
end                                            
else if(@ANY_OTH_INSU_COMP='0')                                                      
begin                                                       
set @ANY_OTH_INSU_COMP='N'                                                      
end */                               
-- 8                                
if(@ANY_LOSS_THREE_YEARS ='1')                                                      
begin                                                       
set @ANY_LOSS_THREE_YEARS ='Y'                      
end                                            
else if(@ANY_LOSS_THREE_YEARS ='0')                                                      
begin                                                       
set @ANY_LOSS_THREE_YEARS ='N'                                                      
end                                          
--  9                  
if(@COVERAGE_DECLINED ='1')                                                   
begin                                                       
set @COVERAGE_DECLINED ='Y'                                                      
end                                            
else if(@COVERAGE_DECLINED ='0')                                                      
begin                                                       
set @COVERAGE_DECLINED ='N'                                                      
end                                  
--10                                
--if(@IS_CREDIT ='1')                         
--begin                                                       
--set @IS_CREDIT ='Y'                                                      
--end                                            
--else if(@IS_CREDIT ='0')                                                  
--begin                           
--set @IS_CREDIT ='N'                                                      
--end                                       
-- 11                                                      
if(@IS_RENTED_OTHERS ='1')                                               
begin                                                       
set @IS_RENTED_OTHERS ='Y'                   
end                                            
else if(@IS_RENTED_OTHERS ='0')                                                      
begin                                                       
set @IS_RENTED_OTHERS ='N'                                                      
end                                   
--12                                      
if(@IS_REGISTERED_OTHERS  ='1')                                               
begin                                                       
set @IS_REGISTERED_OTHERS  ='Y'                                    
end               
else if(@IS_REGISTERED_OTHERS  ='0')                                                      
begin                                                       
set @IS_REGISTERED_OTHERS  ='N'                                                      
end                                                 
--13       
---------------------                         
--Type of watercraft is Sailboat (11372)        
   
if exists(select TYPE_OF_WATERCRAFT from APP_WATERCRAFT_INFO WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID    
and  APP_VERSION_ID=@APPVERSIONID and (TYPE_OF_WATERCRAFT<>11372 and TYPE_OF_WATERCRAFT<>11672 ) and Length<26)     
BEGIN    
	IF(@PARTICIPATE_RACE='1')                              
		BEGIN                                                       
		SET @PARTICIPATE_RACE ='Y'                                       
		END                                            
	ELSE IF(@PARTICIPATE_RACE='0')                                                      
		BEGIN                                                       
		SET @PARTICIPATE_RACE='N'                                                      
		END      
END 
ELSE 
	BEGIN 
		SET @PARTICIPATE_RACE='N'  
	END                 
--------------------                               
--14                            
if(@CARRY_PASSENGER_FOR_CHARGE ='1')                                               
begin                                                       
set @CARRY_PASSENGER_FOR_CHARGE  ='Y'                                       
end                                            
else if(@CARRY_PASSENGER_FOR_CHARGE ='0')                                                      
begin                                        
set @CARRY_PASSENGER_FOR_CHARGE ='N'                             
end                          
--15                          
if(@IS_PRIOR_INSURANCE_CARRIER ='1')                                               
begin                                                       
set @IS_PRIOR_INSURANCE_CARRIER  ='Y'                                       
end                                            
else if(@IS_PRIOR_INSURANCE_CARRIER ='0')                                  
begin                                                       
set @IS_PRIOR_INSURANCE_CARRIER ='N'                                                      
end                  
                
--16                
IF(@ANY_BOAT_AMPHIBIOUS='1')                              
	BEGIN                                                       
	SET @ANY_BOAT_AMPHIBIOUS ='Y'                                       
	END                                            
ELSE IF(@ANY_BOAT_AMPHIBIOUS='0')                                                      
	BEGIN                                                       
	SET @ANY_BOAT_AMPHIBIOUS='N'                                                      
	END                                  
               
--17              
                 
IF(@MULTI_POLICY_DISC_APPLIED='1')                              
	BEGIN                                                       
	SET @MULTI_POLICY_DISC_APPLIED ='Y'                                       
	END                                            
ELSE IF(@MULTI_POLICY_DISC_APPLIED='0')                                                      
	BEGIN                      
	SET @MULTI_POLICY_DISC_APPLIED='N'                                                      
	END              
          
--18 For COowned Boats          
          
IF(@IS_BOAT_COOWNED='1')                              
	BEGIN                                                       
	SET @IS_BOAT_COOWNED ='Y'                                       
	END                                            
ELSE IF(@IS_BOAT_COOWNED='0')                                                      
	BEGIN                                                       
	SET @IS_BOAT_COOWNED='N'                                                      
	END           
    
--19 BOAT RESIDENCE      
          
IF(@ANY_BOAT_RESIDENCE='1')                              
	BEGIN                                                       
	SET @ANY_BOAT_RESIDENCE ='Y'                                       
	END                                         
ELSE IF(@ANY_BOAT_RESIDENCE='0')                                                      
	BEGIN                                                      
	SET @ANY_BOAT_RESIDENCE='N'                                                      
	END           

--20 Boat used in any water Description      
          
IF(@IS_BOAT_USED_IN_ANY_WATER='1')                              
	BEGIN                                                       
	SET @IS_BOAT_USED_IN_ANY_WATER  ='Y'                                       
	END                                         
ELSE IF(@IS_BOAT_USED_IN_ANY_WATER ='0')                                                      
	BEGIN                                                      
	SET @IS_BOAT_USED_IN_ANY_WATER ='N'                                                      
	END              
--End                          
                    
SELECT                                  
--1                                
--@HAS_CURR_ADD_THREE_YEARS  as HAS_CURR_ADD_THREE_YEARS ,                            
--case @HAS_CURR_ADD_THREE_YEARS                            
--when 'Y'then  @HAS_CURR_ADD_THREE_YEARS_DESC                            
--end as HAS_CURR_ADD_THREE_YEARS_DESC,                                
--2                      
@PHY_MENTL_CHALLENGED  as PHY_MENTL_CHALLENGED ,                                
case @PHY_MENTL_CHALLENGED                                 
when 'Y'then  @PHY_MENTL_CHALLENGED_DESC              
end as PHY_MENTL_CHALLENGED_DESC ,                                
--3                                
@DRIVER_SUS_REVOKED as DRIVER_SUS_REVOKED ,                                
case @DRIVER_SUS_REVOKED                            
when 'Y'then  @DRIVER_SUS_REVOKED_DESC                                  
end as DRIVER_SUS_REVOKED_DESC,                                
--4                                
@IS_CONVICTED_ACCIDENT  as IS_CONVICTED_ACCIDENT,                                
case @IS_CONVICTED_ACCIDENT                                 
when 'Y'then  @IS_CONVICTED_ACCIDENT_DESC                                  
end as IS_CONVICTED_ACCIDENT_DESC ,                                
--5                                
@DRINK_DRUG_VOILATION  as DRINK_DRUG_VOILATION ,                                
--6                                 
@MINOR_VIOLATION  as MINOR_VIOLATION ,           
--7           
/*@ANY_OTH_INSU_COMP  as ANY_OTH_INSU_COMP ,                                
case @ANY_OTH_INSU_COMP                                 
when 'Y'then  @OTHER_POLICY_NUMBER_LIST                                  
end as OTHER_POLICY_NUMBER_LIST ,*/                                
--8                                 
@ANY_LOSS_THREE_YEARS  as ANY_LOSS_THREE_YEARS ,                                
case @ANY_LOSS_THREE_YEARS                                
when 'Y'then  @ANY_LOSS_THREE_YEARS_DESC                                  
end as ANY_LOSS_THREE_YEARS_DESC ,                                
--9                                
@COVERAGE_DECLINED  as COVERAGE_DECLINED ,                                 
case @COVERAGE_DECLINED                                 
when 'Y'then  @COVERAGE_DECLINED_DESC                                  
end as COVERAGE_DECLINED_DESC ,                                
--10                                 
--@IS_CREDIT  as IS_CREDIT  ,                                
--case @IS_CREDIT                                 
--when 'Y'then  @CREDIT_DETAILS                                  
--end as CREDIT_DETAILS ,                                
--11                                
@IS_RENTED_OTHERS as IS_RENTED_OTHERS ,                                
case @IS_RENTED_OTHERS                                 
when 'Y'then  @IS_RENTED_OTHERS_DESC                                  
end as IS_RENTED_OTHERS_DESC ,                        
--Manadatory Fields                                 
--12                                
@IS_REGISTERED_OTHERS  as IS_REGISTERED_OTHERS  ,                                
case @IS_REGISTERED_OTHERS                                 
when '1'then  @IS_REGISTERED_OTHERS_DESC                                  
end as IS_REGISTERED_OTHERS_DESC,                                
--13                                
@PARTICIPATE_RACE  as PARTICIPATE_RACE ,                                 
case @PARTICIPATE_RACE                                 
when '1'then  @PARTICIPATE_RACE_DESC                                  
end as PARTICIPATE_RACE_DESC ,                                
--14                          
@CARRY_PASSENGER_FOR_CHARGE  as CARRY_PASSENGER_FOR_CHARGE ,                                
case @CARRY_PASSENGER_FOR_CHARGE                     
when '1'then  @CARRY_PASSENGER_FOR_CHARGE_DESC                                  
end as CARRY_PASSENGER_FOR_CHARGE_DESC ,                                
--15                                
@IS_PRIOR_INSURANCE_CARRIER  as IS_PRIOR_INSURANCE_CARRIER ,                                
case @IS_PRIOR_INSURANCE_CARRIER                                 
when '1'then  @PRIOR_INSURANCE_CARRIER_DESC                          
end as PRIOR_INSURANCE_CARRIER_DESC ,                             
                  
--  16                
@IS_BOAT_COOWNED as IS_BOAT_COOWNED,                  
case @IS_BOAT_COOWNED                                 
when 'Y'then  @IS_BOAT_COOWNED_DESC                          
end as IS_BOAT_COOWNED_DESC,                  
--                  
                
--17                
@ANY_BOAT_AMPHIBIOUS  as ANY_BOAT_AMPHIBIOUS,                                 
case @ANY_BOAT_AMPHIBIOUS                
when '1'then  @ANY_BOAT_AMPHIBIOUS_DESC                                  
end as ANY_BOAT_AMPHIBIOUS_DESC ,                
                
              
--18              
@MULTI_POLICY_DISC_APPLIED  as MULTI_POLICY_DISC_APPLIED,                                 
case @MULTI_POLICY_DISC_APPLIED                
when 'Y'then  @MULTI_POLICY_DISC_APPLIED_DESC                                  
end as MULTI_POLICY_DISC_APPLIED_DESC ,                
              
--19          
@ANY_BOAT_RESIDENCE as ANY_BOAT_RESIDENCE,          
case @ANY_BOAT_RESIDENCE                
when '1'then  @ANY_BOAT_RESIDENCE_DESC                                  
end as ANY_BOAT_RESIDENCE_DESC ,          
--20
@IS_BOAT_USED_IN_ANY_WATER  as IS_BOAT_USED_IN_ANY_WATER ,          
case @IS_BOAT_USED_IN_ANY_WATER                 
when '1'then  @IS_BOAT_USED_IN_ANY_WATER_DESC                                  
end as IS_BOAT_USED_IN_ANY_WATER_DESC ,                
                
@IS_RECORD_EXISTS as IS_RECORD_EXISTS  
----------------------------------------------------------  
End  





GO

