IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHORule_SolidFuelInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHORule_SolidFuelInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* ----------------------------------------------------------                                                          
Proc Name                : Dbo.Proc_GetHORule_SolidFuelInfo   
Created by               : Ashwani                                                          
Date                     : 01 Dec.,2005          
Purpose                  : To get solid fuel info for HO rules          
Revison History          :                                                          
Used In                  : Wolverine                                                          
------------------------------------------------------------                                                          
Date     Review By          Comments                                                          
------   ------------       -------------------------*/  
-- drop proc Proc_GetHORule_SolidFuelInfo                                                        
CREATE proc dbo.Proc_GetHORule_SolidFuelInfo      
(                                                          
@CUSTOMERID    int,                                                          
@APPID    int,                                                          
@APPVERSIONID   int,          
@FUELID int,                      
@DESC varchar(10)                                            
)                                                          
as                                                              
begin             
 -- MANDATORY           
 DECLARE @MANUFACTURER NVARCHAR(100)        
 --DECLARE @BRAND_NAME NVARCHAR(75)        
 DECLARE @MODEL_NUMBER NVARCHAR(35)        
 --DECLARE @FUEL NVARCHAR(75)        
 DECLARE @STOVE_TYPE NVARCHAR(5)        
 DECLARE @HAVE_LABORATORY_LABEL NCHAR(1)        
 --DECLARE @IS_UNIT NVARCHAR(5)        
 DECLARE @LOCATION NVARCHAR(5)        
 --DECLARE @CONSTRUCTION NVARCHAR(5)        
 DECLARE @YEAR_DEVICE_INSTALLED INT        
 DECLARE @INSTALL_INSPECTED_BY NVARCHAR(5)        
 DECLARE @WAS_PROF_INSTALL_DONE  NCHAR(1)        
 DECLARE @HEATING_USE NVARCHAR(10)        
 DECLARE @HEATING_SOURCE NVARCHAR(5) 
 declare @STOVE_INSTALLATION_CONFORM_SPECIFICATIONS int   
           
 IF EXISTS (SELECT CUSTOMER_ID FROM APP_HOME_OWNER_SOLID_FUEL                             
  WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND FUEL_ID=@FUELID)          
  BEGIN           
	        SELECT 
		@MANUFACTURER=ISNULL(MANUFACTURER,''),  
		--@BRAND_NAME=ISNULL(BRAND_NAME,''),  
		@MODEL_NUMBER=ISNULL(MODEL_NUMBER,''),        
		--@FUEL=ISNULL(FUEL,''),  
		@STOVE_TYPE=ISNULL(STOVE_TYPE,''),  
		@HAVE_LABORATORY_LABEL=ISNULL(HAVE_LABORATORY_LABEL,''),        
		--@IS_UNIT=ISNULL(IS_UNIT,''),  
		@LOCATION=ISNULL(LOCATION,''),  
		--@CONSTRUCTION=ISNULL(CONSTRUCTION,''),        
		@YEAR_DEVICE_INSTALLED=ISNULL(YEAR_DEVICE_INSTALLED,0),@INSTALL_INSPECTED_BY=ISNULL(INSTALL_INSPECTED_BY,''),        
		@WAS_PROF_INSTALL_DONE=ISNULL(WAS_PROF_INSTALL_DONE,''),@HEATING_USE=ISNULL(HEATING_USE,''),  
		@HEATING_SOURCE=ISNULL(HEATING_SOURCE,'')      ,  
		@STOVE_INSTALLATION_CONFORM_SPECIFICATIONS = ISNULL(STOVE_INSTALLATION_CONFORM_SPECIFICATIONS,0)  
	 	FROM APP_HOME_OWNER_SOLID_FUEL          
		WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID AND FUEL_ID=@FUELID            
 END          
 ELSE          
 BEGIN         
		--MANDATORY        
		SET @MANUFACTURER =''        
		--SET @BRAND_NAME =''        
		SET @MODEL_NUMBER =''        
		--SET @FUEL =''        
		SET @STOVE_TYPE =''        
		--SET @HAVE_LABORATORY_LABEL =''        
		--SET @IS_UNIT =''        
		SET @LOCATION =''        
		--SET @CONSTRUCTION =''        
		SET @YEAR_DEVICE_INSTALLED =0        
		SET @INSTALL_INSPECTED_BY =''        
		SET @WAS_PROF_INSTALL_DONE  =''        
		SET @HEATING_USE =''        
		SET @HEATING_SOURCE =''          
END           

----------------------      
 -- Primary and Secondary Heat Type                        
DECLARE @SECONDARY_HEATING_SOURCE CHAR                                            
DECLARE @SECONDARY_HEAT_TYPE CHAR            
                                     
 IF EXISTS (SELECT  SECONDARY_HEAT_TYPE FROM  APP_HOME_RATING_INFO                                                   
	WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID                   
	AND (SECONDARY_HEAT_TYPE=6223 OR SECONDARY_HEAT_TYPE=6224      
	OR SECONDARY_HEAT_TYPE=6212   OR SECONDARY_HEAT_TYPE=6213  OR SECONDARY_HEAT_TYPE=11806       
	OR SECONDARY_HEAT_TYPE=11806  OR SECONDARY_HEAT_TYPE=11807 OR SECONDARY_HEAT_TYPE=11808))                                             
	BEGIN                                                   
		SET  @SECONDARY_HEATING_SOURCE='Y'                                                  
	END                                                   
 ELSE                                                  
	BEGIN                                                   
		SET @SECONDARY_HEATING_SOURCE='N'                                                  
	END  
 -- IF No to any of these Fields -Risk is declined 
	 -- Was installation does by a professional installer such as a contractor ? 
 	 -- Does the unit have a testing laboratory label (UL, other)
DECLARE @LABORATORY_LABEL CHAR 
DECLARE	@PROF_INSTALL_DONE CHAR
SET @LABORATORY_LABEL='N'  
SET @PROF_INSTALL_DONE='N'    
IF (@HAVE_LABORATORY_LABEL='N')
	BEGIN 
		SET @LABORATORY_LABEL='Y'
	END

IF(@WAS_PROF_INSTALL_DONE='N')
	BEGIN 
		SET @PROF_INSTALL_DONE='Y'
	END
	


IF((@WAS_PROF_INSTALL_DONE='N' OR @HAVE_LABORATORY_LABEL='N')AND @SECONDARY_HEATING_SOURCE='Y')          
	BEGIN      
		SET  @SECONDARY_HEATING_SOURCE='Y'      
	END      
ELSE      
	BEGIN      
		SET  @SECONDARY_HEATING_SOURCE='N'      
	END      
  
---------------------      
-------------------"Soild fuel : Heating Use:--------------
--Programming - If Primary  --Give message the risk is declined as this is the primary heat source"  
--9019 Primary (main heat source)  
print @HEATING_USE
DECLARE @MAIN_HEATING_USE CHAR  
	IF(@HEATING_USE='9019')  
		BEGIN  
			SET @MAIN_HEATING_USE='Y'  
		END  
	ELSE  
		BEGIN  
			SET @MAIN_HEATING_USE='N'  
		END  
  print @MAIN_HEATING_USE
--------------------END -Soild fuel : Heating Use:---------------  
--Does the stove installation and use conform to all of its manufacturers  
--specifications and local fire codes IF NO  Reject the Application :   
DECLARE @STOVE_INSTALLATION CHAR  
	IF(@STOVE_INSTALLATION_CONFORM_SPECIFICATIONS=10964)  
		BEGIN  
			SET @STOVE_INSTALLATION='Y'  
		END  
	ELSE  
		BEGIN  
			SET @STOVE_INSTALLATION='N'  
		END  
  
---------END stove installation------  
-- Mandatory          
SELECT         
@MANUFACTURER as MANUFACTURER,        
--@BRAND_NAME as BRAND_NAME,        
@MODEL_NUMBER as MODEL_NUMBER,        
--@FUEL as FUEL,        
@STOVE_TYPE as STOVE_TYPE,        
--@HAVE_LABORATORY_LABEL as  HAVE_LABORATORY_LABEL,        
--@IS_UNIT as IS_UNIT,        
@LOCATION as LOCATION,        
--@CONSTRUCTION as CONSTRUCTION,        
@YEAR_DEVICE_INSTALLED as YEAR_DEVICE_INSTALLED,        
@INSTALL_INSPECTED_BY as INSTALL_INSPECTED_BY,        
@WAS_PROF_INSTALL_DONE  as  WAS_PROF_INSTALL_DONE,        
@HEATING_USE as HEATING_USE,        
@HEATING_SOURCE as HEATING_SOURCE ,      
@SECONDARY_HEATING_SOURCE as SECONDARY_HEATING_SOURCE,  
--  
@MAIN_HEATING_USE as MAIN_HEATING_USE,  
@STOVE_INSTALLATION as STOVE_INSTALLATION,
@LABORATORY_LABEL AS LABORATORY_LABEL ,
@PROF_INSTALL_DONE AS PROF_INSTALL_DONE    
end





GO

