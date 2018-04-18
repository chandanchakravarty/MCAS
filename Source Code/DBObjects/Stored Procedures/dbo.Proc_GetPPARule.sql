IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPPARule]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPPARule]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                              
Proc Name                : Dbo.Proc_GetPPARule                            
Created by               : Ashwani                              
Date                     : 25Aug.,2005                            
Purpose                  : To get the Rule Information for Private Passenger Auto                            
Revison History          :                              
Used In                  : Wolverine                              
------------------------------------------------------------                              
Date     Review By          Comments                              
DROP PROC Dbo.Proc_GetPPARule
------   ------------       -------------------------*/                              
CREATE proc Dbo.Proc_GetPPARule                              
(                              
@CUSTOMERID    int,                              
@APPID    int,                              
@APPVERSIONID   int,                    
@VEHICLEID int                           
)                              
as                                  
begin                     
--APP_AUTO_GEN_INFO                       
declare @COVERAGE_DECLINED char                    
declare @H_MEM_IN_MILITARY char                    
declare @PHY_MENTL_CHALLENGED char                    
declare @CAR_MODIFIED char                    
declare @SALVAGE_TITLE char                    
declare @ANY_NON_OWNED_VEH char                    
declare @EXISTING_DMG char                    
declare @ANY_CAR_AT_SCH char                    
declare @ANY_OTH_INSU_COMP char                    
declare @ANY_FINANCIAL_RESPONSIBILITY char     
declare @DRIVER_SUS_REVOKED char               
            
if exists(select CUSTOMER_ID from APP_AUTO_GEN_INFO where                     
CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID)            
begin             
select   @DRIVER_SUS_REVOKED=isnull(DRIVER_SUS_REVOKED,'') ,@COVERAGE_DECLINED=isnull(COVERAGE_DECLINED,''),@H_MEM_IN_MILITARY=isnull(H_MEM_IN_MILITARY,''),                    
@PHY_MENTL_CHALLENGED=isnull(PHY_MENTL_CHALLENGED,''),@CAR_MODIFIED=isnull(CAR_MODIFIED,''),                    
@SALVAGE_TITLE=isnull(SALVAGE_TITLE,''),@ANY_NON_OWNED_VEH=isnull(ANY_NON_OWNED_VEH,''),                    
@EXISTING_DMG=isnull(EXISTING_DMG,''),@ANY_CAR_AT_SCH=isnull(ANY_CAR_AT_SCH,''),@ANY_OTH_INSU_COMP=isnull(ANY_OTH_INSU_COMP,''),                    
@ANY_FINANCIAL_RESPONSIBILITY=isnull(ANY_FINANCIAL_RESPONSIBILITY,''),@COVERAGE_DECLINED=isnull(COVERAGE_DECLINED,'')                    
from APP_AUTO_GEN_INFO where                     
CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID                    
end             
else            
begin             
set @COVERAGE_DECLINED =''            
set @H_MEM_IN_MILITARY =''            
set @PHY_MENTL_CHALLENGED =''            
set @CAR_MODIFIED =''                    
set @SALVAGE_TITLE =''                    
set @ANY_NON_OWNED_VEH =''                    
set @EXISTING_DMG =''                    
set @ANY_CAR_AT_SCH =''                    
set @ANY_OTH_INSU_COMP =''                    
set @ANY_FINANCIAL_RESPONSIBILITY =''     
set @DRIVER_SUS_REVOKED  =''    
end                     
--                  
if(@DRIVER_SUS_REVOKED='1')                    
begin                     
set @DRIVER_SUS_REVOKED='Y'                    
end          
else if(@DRIVER_SUS_REVOKED='0')                    
begin                     
set @DRIVER_SUS_REVOKED='N'                    
end     
--                    
if(@COVERAGE_DECLINED='1')                    
begin                     
set @COVERAGE_DECLINED='Y'                    
end          
else if(@COVERAGE_DECLINED='0')                    
begin                     
set @COVERAGE_DECLINED='N'                    
end     
--                   
if(@PHY_MENTL_CHALLENGED='1')             
begin                     
set @PHY_MENTL_CHALLENGED='Y'     
end          
else if(@PHY_MENTL_CHALLENGED='0')                    
begin                     
set @PHY_MENTL_CHALLENGED='N'                    
end               
--          
if(@CAR_MODIFIED='1')                    
begin                     
set @CAR_MODIFIED='Y'                    
end          
else if(@CAR_MODIFIED='0')                    
begin                     
set @CAR_MODIFIED='N'                    
end          
--         
if(@SALVAGE_TITLE='1')                    
begin                     
set @SALVAGE_TITLE='Y'                    
end          
else if(@SALVAGE_TITLE='0')                    
begin                     
set @SALVAGE_TITLE='N'                    
end            
--         
if(@ANY_NON_OWNED_VEH='1')                    
begin                     
set @ANY_NON_OWNED_VEH='Y'                    
end          
else if(@ANY_NON_OWNED_VEH='0')                    
begin                     
set @ANY_NON_OWNED_VEH='N'                    
end              
--         
if(@EXISTING_DMG='1')                    
begin                     
set @EXISTING_DMG='Y'                    
end          
else if(@EXISTING_DMG='0')                    
begin                     
set @EXISTING_DMG='N'                    
end        
--         
if(@ANY_CAR_AT_SCH='1')                    
begin                     
set @ANY_CAR_AT_SCH='Y'                    
end          
else if(@ANY_CAR_AT_SCH='0')                    
begin                     
set @ANY_CAR_AT_SCH='N'                    
end        
--         
if(@ANY_OTH_INSU_COMP='1')                    
begin                     
set @ANY_OTH_INSU_COMP='Y'                    
end          
else if(@ANY_OTH_INSU_COMP='0')                    
begin                     
set @ANY_OTH_INSU_COMP='N'                    
end           
--         
if(@ANY_FINANCIAL_RESPONSIBILITY='1')                    
begin                     
set @ANY_FINANCIAL_RESPONSIBILITY='Y'                    
end          
else if(@ANY_FINANCIAL_RESPONSIBILITY='0')                    
begin                     
set @ANY_FINANCIAL_RESPONSIBILITY='N'                    
end        
--         
if(@H_MEM_IN_MILITARY='1')                    
begin                     
set @H_MEM_IN_MILITARY='Y'                    
end          
else if(@H_MEM_IN_MILITARY='0')                    
begin                     
set @H_MEM_IN_MILITARY='N'                    
end        
-- Vehicle Info       
declare @VEHICLE_YEAR varchar(4)      
declare @MAKE nvarchar(75)      
declare @MODEL nvarchar(75)      
declare @VIN nvarchar(75)
declare @VEHICLE_USE	nvarchar(5)
declare @INSURED_VEH_NUMBER smallint
declare @GRG_ADD1 nvarchar(70)
declare @GRG_CITY  nvarchar(40)
declare @GRG_STATE nvarchar(5)
declare @GRG_ZIP varchar(11)
declare @TERRITORY varchar(5)
declare @AMOUNT  decimal

      
if exists (select CUSTOMER_ID from APP_VEHICLES where CUSTOMER_ID=@CUSTOMERID and  APP_ID=@APPID  and  APP_VERSION_ID=@APPVERSIONID   and VEHICLE_ID=@VEHICLEID)      
begin       

select @VEHICLE_YEAR=isnull(VEHICLE_YEAR,''),@MAKE=isnull(MAKE,''),@MODEL=isnull(MODEL,''),@VIN=isnull(VIN,''),
@VEHICLE_USE=isnull(VEHICLE_USE,''),@INSURED_VEH_NUMBER=isnull(INSURED_VEH_NUMBER,''),
@GRG_ADD1=isnull(GRG_ADD1,''),@GRG_CITY=isnull(GRG_CITY,''),@GRG_STATE=isnull(GRG_STATE,''),@GRG_ZIP=isnull(GRG_ZIP,''),
@TERRITORY=isnull(TERRITORY,''),@AMOUNT=isnull(AMOUNT,0)
      

from APP_VEHICLES        
where CUSTOMER_ID=@CUSTOMERID and  APP_ID=@APPID  and  APP_VERSION_ID=@APPVERSIONID   and VEHICLE_ID=@VEHICLEID      
end       
else      
begin       
set @VEHICLE_YEAR =''      
set @MAKE =''      
set @MODEL =''      
set @VIN =''
set @VEHICLE_USE=''
set @INSURED_VEH_NUMBER =''
set @GRG_ADD1 =''
set @GRG_CITY ='' 
set @GRG_STATE =''

set @GRG_ZIP='' 
set @TERRITORY =''
set @AMOUNT =''
end       
--   
-- Remove this field  
if(@VIN='')  
begin   
set @VIN='9999999'  
end   
         
select                     
@COVERAGE_DECLINED as COVERAGE_DECLINED,                    
@H_MEM_IN_MILITARY as MEM_IN_MILITRY,                    
@PHY_MENTL_CHALLENGED as  PHY_MENTAL_CHALLENGED,                    
@CAR_MODIFIED as CAR_MODIFIED,                    
@SALVAGE_TITLE as SALVAGE_TITLE,                    
@ANY_NON_OWNED_VEH as ANY_NON_OWNED_VEH,                    
@EXISTING_DMG as EXISTING_DMG,                    
@ANY_CAR_AT_SCH as  ANY_CAR_AT_SCH,                    
@ANY_OTH_INSU_COMP as ANY_OTH_INSU_COMP,                    
@ANY_FINANCIAL_RESPONSIBILITY  as ANY_FINANCIAL_RESPONSIBILITY,      
@VEHICLE_YEAR as VEHICLE_YEAR,      
@MAKE as MAKE,      
@MODEL as MODEL,      
@VIN as VIN,    
@DRIVER_SUS_REVOKED as DRIVER_SUS_REVOKED ,     
-- Mandatory 
 @VEHICLE_USE	as VEHICLE_USE,
 @INSURED_VEH_NUMBER as INSURED_VEH_NUMBER,
 @GRG_ADD1 as GRG_ADD1,
 @GRG_CITY  as GRG_CITY,
 @GRG_STATE as GRG_STATE,
 @GRG_ZIP as GRG_ZIP,
 @TERRITORY as TERRITORY,
 @AMOUNT as  AMOUNT
end                                       
              
              
            
          
          
    
  








GO

