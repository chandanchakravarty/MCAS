IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetUMRule_LocationInfo_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetUMRule_LocationInfo_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* ====================================================================================    
Proc Name                : dbo.Proc_GetUMRule_LocationInfo_Pol                                                          
Created by               : Ashwani                                                              
Date                     : 25 Oct.,2006      
Purpose                  : To get the location detail for UM rules              
Revison History          :                                                              
Used In                  : Wolverine                                                              
=====================================================================================    
Date     Review By          Comments              
    
DROP PROC Proc_GetUMRule_LocationInfo_POL 943,60,1,178                                                  
    
=====    ===========    ============================================================= */    
CREATE proc dbo.Proc_GetUMRule_LocationInfo_Pol              
(                                                              
 @CUSTOMERID    int,                                                              
 @POLICYID    int,                                                              
 @POLICYVERSIONID   int,              
 @LOCATIONID int    
)                                                              
as                                                                  
begin                               
    
 declare  @BUSS_FARM_PURSUITS varchar(12)    
 declare  @BUSS_FARM_PURSUITS_OTHER char    
 declare  @BUSS_FARM_PURSUITS_SCH_OFF char    
 declare  @PERS_INJ_COV_82 varchar(12)    
 declare  @NUM_FAMILIES varchar(12)    
 declare  @LOCATION_NUMBER varchar(20)
 DECLARE  @LOC_EXCLUDED varchar(12)    
 DECLARE  @LOCATION_ID varchar(12)
 DECLARE  @COVERAGE_ID varchar(12)
        
    
 set @BUSS_FARM_PURSUITS_OTHER ='N'    
 set @BUSS_FARM_PURSUITS_SCH_OFF ='N'    
    
if exists (select CUSTOMER_ID from POL_UMBRELLA_REAL_ESTATE_LOCATION                                 
 where CUSTOMER_ID=@CUSTOMERID and POLICY_ID= @POLICYID and POLICY_VERSION_ID = @POLICYVERSIONID and LOCATION_ID=@LOCATIONID)              
begin               
 select @BUSS_FARM_PURSUITS =isnull(BUSS_FARM_PURSUITS,''),@PERS_INJ_COV_82=isnull(convert(varchar(12),PERS_INJ_COV_82),''),    
 @LOCATION_NUMBER =isnull(convert(varchar(20),LOCATION_NUMBER),'')              
  from POL_UMBRELLA_REAL_ESTATE_LOCATION              
   where CUSTOMER_ID=@CUSTOMERID and POLICY_ID= @POLICYID and POLICY_VERSION_ID = @POLICYVERSIONID and LOCATION_ID=@LOCATIONID              
              
end     
    
if(@PERS_INJ_COV_82=0)    
 set @PERS_INJ_COV_82=''    
--Location Details Business/Farming Pursuits? If Other Business - then refer to underwriters    
if(@BUSS_FARM_PURSUITS='11955')    
 set @BUSS_FARM_PURSUITS_OTHER='Y'    
    
-- Location Details if the number of locations that have  in the Farming/Business Pursuits Field     
-- Office/School or Studio Then refer to underwriters      
if(@BUSS_FARM_PURSUITS='11953')    
 set @BUSS_FARM_PURSUITS_SCH_OFF='Y'    
    
-- Location Details  Do you carry Personal Injury Coverage - HO-82 If no then refer to Underwriters     
if(@PERS_INJ_COV_82='10964')    
 set @PERS_INJ_COV_82='Y'    
    
-- Location Details  Look at the Description field of all locations with Rented Total up the # of Families     
-- If greater then 4 Refer to underwriters    
 IF EXISTS(SELECT SUM(NUM_FAMILIES)  FROM POL_UMBRELLA_REAL_ESTATE_LOCATION      
  WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID= @POLICYID and POLICY_VERSION_ID = @POLICYVERSIONID AND OCCUPIED_BY=11951  
  HAVING SUM(NUM_FAMILIES)> 4  )       
 BEGIN        
    SET @NUM_FAMILIES='Y'  
 END

-- Location Details Is Excluded

SET @LOC_EXCLUDED='N'
SET @LOCATION_ID = 'N'
SET @COVERAGE_ID ='N'
IF EXISTS(SELECT LOCATION_ID FROM POL_UMBRELLA_REAL_ESTATE_LOCATION 
WHERE CUSTOMER_ID=@CUSTOMERID and POLICY_ID= @POLICYID and POLICY_VERSION_ID = @POLICYVERSIONID AND LOC_EXCLUDED = 10963) 
BEGIN 
SET @LOCATION_ID = 'Y'
END

IF EXISTS(SELECT COVERAGE_ID FROM POL_UMBRELLA_COVERAGES 
WHERE CUSTOMER_ID=@CUSTOMERID and POL_ID= @POLICYID and POL_VERSION_ID = @POLICYVERSIONID AND COVERAGE_CODE_ID IN (1050,1039))

BEGIN 
SET @COVERAGE_ID ='Y' 
END 
IF(@LOCATION_ID = 'Y' AND @COVERAGE_ID ='Y' )
BEGIN 
SET @LOC_EXCLUDED ='N'
END 
ELSE IF (@LOCATION_ID = 'Y' AND @COVERAGE_ID ='N' )
BEGIN
SET @LOC_EXCLUDED ='Y'
END
ELSE IF (@LOCATION_ID = 'N' AND @COVERAGE_ID ='Y' )
BEGIN
SET @LOC_EXCLUDED ='Y'
END 

    
 select @BUSS_FARM_PURSUITS as BUSS_FARM_PURSUITS ,    
 @BUSS_FARM_PURSUITS_OTHER as BUSS_FARM_PURSUITS_OTHER,    
 @BUSS_FARM_PURSUITS_SCH_OFF as BUSS_FARM_PURSUITS_SCH_OFF,    
 @PERS_INJ_COV_82 as PERS_INJ_COV_82,    
 @NUM_FAMILIES as NUM_FAMILIES,    
 @LOCATION_NUMBER as LOC_NUM,
 @LOC_EXCLUDED AS LOC_EXCLUDED    
                         
end   




GO

