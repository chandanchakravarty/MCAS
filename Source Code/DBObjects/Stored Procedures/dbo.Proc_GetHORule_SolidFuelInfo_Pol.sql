IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetHORule_SolidFuelInfo_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetHORule_SolidFuelInfo_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                                                      
Proc Name                : Dbo.Proc_GetHORule_SolidFuelInfo_Pol                                                    
Created by               : Ashwani                                                      
Date                     : 02 Mar 2006   
Purpose                  : To get solid fuel info for HO policy rules      
Revison History          :                                                      
Used In                  : Wolverine                                                      
------------------------------------------------------------                                                      
Date     Review By          Comments                                                      
------   ------------       -------------------------*/ 
--drop proc dbo.Proc_GetHORule_SolidFuelInfo_Pol                                                     
CREATE proc dbo.Proc_GetHORule_SolidFuelInfo_Pol    
(                                                      
@CUSTOMER_ID    int,                                                      
@POLICY_ID    int,                                                      
@POLICY_VERSION_ID   int,      
@FUEL_ID int                  
)                                                      
AS                                                          
BEGIN         
	 -- Mandatory       
	 declare @MANUFACTURER nvarchar(100)    
	 -- declare @BRAND_NAME nvarchar(75)    
	 declare @MODEL_NUMBER nvarchar(35)    
	 --declare @FUEL nvarchar(75)    
	 declare @STOVE_TYPE nvarchar(5)    
	 declare @HAVE_LABORATORY_LABEL nchar(1)    
	 --declare @IS_UNIT nvarchar(5)    
	 declare @LOCATION nvarchar(5)    
	 --declare @CONSTRUCTION nvarchar(5)    
	 declare @YEAR_DEVICE_INSTALLED int    
	 declare @INSTALL_INSPECTED_BY nvarchar(5)    
	 declare @WAS_PROF_INSTALL_DONE  nchar(1)    
	 declare @HEATING_USE nvarchar(10)    
	 declare @HEATING_SOURCE nvarchar(5)
	 declare @STOVE_INSTALLATION_CONFORM_SPECIFICATIONS int     
       
IF EXISTS (SELECT CUSTOMER_ID FROM POL_HOME_OWNER_SOLID_FUEL                         
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND FUEL_ID=@FUEL_ID)      
BEGIN       
	SELECT @MANUFACTURER=ISNULL(MANUFACTURER,''),--@BRAND_NAME=ISNULL(BRAND_NAME,''),
	@MODEL_NUMBER=ISNULL(MODEL_NUMBER,''),    
	--@FUEL=ISNULL(FUEL,''),
	@STOVE_TYPE=ISNULL(STOVE_TYPE,''),@HAVE_LABORATORY_LABEL=ISNULL(HAVE_LABORATORY_LABEL,''),    
	--@IS_UNIT=ISNULL(IS_UNIT,''),
	@LOCATION=ISNULL(LOCATION,''),--@CONSTRUCTION=ISNULL(CONSTRUCTION,''),    
	@YEAR_DEVICE_INSTALLED=ISNULL(YEAR_DEVICE_INSTALLED,0),@INSTALL_INSPECTED_BY=ISNULL(INSTALL_INSPECTED_BY,''),    
	@WAS_PROF_INSTALL_DONE=ISNULL(WAS_PROF_INSTALL_DONE,''),@HEATING_USE=ISNULL(HEATING_USE,''),@HEATING_SOURCE=ISNULL(HEATING_SOURCE,'')    ,
	@STOVE_INSTALLATION_CONFORM_SPECIFICATIONS = ISNULL(STOVE_INSTALLATION_CONFORM_SPECIFICATIONS,0)
	FROM POL_HOME_OWNER_SOLID_FUEL      
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND FUEL_ID=@FUEL_ID        
END      
ELSE      
BEGIN     
	--Mandatory    
	set @MANUFACTURER =''    
	--set @BRAND_NAME =''    
	set @MODEL_NUMBER =''    
	--set @FUEL =''    
	set @STOVE_TYPE =''    
	--set @HAVE_LABORATORY_LABEL =''    
	--set @IS_UNIT =''    
	set @LOCATION =''    
	--set @CONSTRUCTION =''    
	set @YEAR_DEVICE_INSTALLED =0    
	set @INSTALL_INSPECTED_BY =''    
	set @WAS_PROF_INSTALL_DONE  =''    
	set @HEATING_USE =''    
	set @HEATING_SOURCE =''      
END       
      
  -- Primary and Secondary Heat Type                          
DECLARE @SECONDARY_HEATING_SOURCE CHAR                                              
DECLARE @SECONDARY_HEAT_TYPE CHAR              
                                       
 IF EXISTS (SELECT  SECONDARY_HEAT_TYPE FROM  POL_HOME_RATING_INFO                   
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                     
      AND (SECONDARY_HEAT_TYPE=6223 OR SECONDARY_HEAT_TYPE=6224        
      OR SECONDARY_HEAT_TYPE=6212 OR SECONDARY_HEAT_TYPE=6213 OR SECONDARY_HEAT_TYPE=11806         
      OR SECONDARY_HEAT_TYPE=11806 OR SECONDARY_HEAT_TYPE=11807 OR SECONDARY_HEAT_TYPE=11808))                                               
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
-------------------"Soild fuel : Heating Use:-------------- --Programming - If Primary  --Give message the risk is declined as this is the primary heat source"    
--9019 Primary (main heat source)    
DECLARE @MAIN_HEATING_USE CHAR    
IF(@HEATING_USE='9019')    
BEGIN    
SET @MAIN_HEATING_USE='Y'    
END    
ELSE    
BEGIN    
SET @MAIN_HEATING_USE='N'    
END    
    
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
	@HEATING_SOURCE as HEATING_SOURCE  ,
	@SECONDARY_HEATING_SOURCE AS SECONDARY_HEATING_SOURCE ,
	@MAIN_HEATING_USE as MAIN_HEATING_USE   ,    
	@STOVE_INSTALLATION as STOVE_INSTALLATION  ,
	@LABORATORY_LABEL AS LABORATORY_LABEL ,
	@PROF_INSTALL_DONE AS PROF_INSTALL_DONE 
END






GO

