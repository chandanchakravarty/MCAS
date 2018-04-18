IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRatingInformationFormMotorcycle_DriverComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRatingInformationFormMotorcycle_DriverComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                            
Proc Name               : Dbo.Proc_GetRatingInformationForAuto_DriverComponent                            
Created by              : Shrikant Bhatt.                            
Date                    : 16/12/2005                            
Purpose                 : To get the information for creating the input xml                              
Revison History         :                            
Used In                 :   Creating InputXML for vehicle                   
                        
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/              
 --         
 --  drop proc Proc_GetRatingInformationFormMotorcycle_DriverComponent 547,212,1,1     
CREATE  PROC dbo.Proc_GetRatingInformationFormMotorcycle_DriverComponent        
@CUSTOMERID      INT,                            
@APPID      INT,                            
@APPVERSIONID    INT,                            
@DRIVERID      INT                            
                         
AS                            
                            
BEGIN                            
 SET QUOTED_IDENTIFIER OFF                            
                            
DECLARE    @DRIVERFNAME         nvarchar(100)                            
DECLARE    @DRIVERMNAME         nvarchar(100)                            
DECLARE    @DRIVERLNAME         nvarchar(100)                          
DECLARE    @BIRTHDATE          nvarchar(100)                            
DECLARE    @GENDER          nvarchar(100)                            
DECLARE    @DRIVERCLASSCOMPONENT1   nvarchar(100)                            
DECLARE    @DRIVERCLASSCOMPONENT2    nvarchar(100)                             
DECLARE    @MARITALSTATUS        nvarchar(100)                            
DECLARE    @NOCYCLENDMT         nvarchar(100)                            
DECLARE    @LICUNDER3YRS         nvarchar(100)                            
DECLARE    @VEHICLEASSIGNEDASOPERATOR   nvarchar(100)                            
DECLARE    @VEHICLEDRIVEDAS        nvarchar(100)                            
DECLARE    @AGEOFDRIVER         nvarchar(100)                             
DECLARE    @SUMOFMISCPOINTS     nvarchar(100)                     
DECLARE    @NOCYCLENDM     nvarchar(20)                     
DECLARE    @DRIVERCODE     nvarchar(20)                    
DECLARE    @DATED    int             
DECLARE    @VEHICLEDRIVEDASCODE NVARCHAR(20)  
DECLARE	   @DRIVER_DRIV_TYPE 	NVARCHAR(10)  
DECLARE	   @VIOLATION_APPLICABLE   VARCHAR(4)        
-- APP EFFECTIVE DATE                          
DECLARE @APPEFFECTIVEDATE DATETIME              
DECLARE @DATELICENSED DATETIME                              
                            
SELECT @APPEFFECTIVEDATE = CONVERT(CHAR(10),APP_EFFECTIVE_DATE,101)                               
FROM  APP_LIST WITH (NOLOCK)                             
WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                              
set @SUMOFMISCPOINTS  ='0' 
--Added by manoj rathore on 22th Jun. 2009 Itrack # 5947
DECLARE @DRIVER_TYPE VARCHAR(20)
SELECT @DRIVER_TYPE=DRIVER_DRIV_TYPE FROM APP_DRIVER_DETAILS
WHERE CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND DRIVER_ID=@DRIVERID                  

	IF(@DRIVER_TYPE='11941') 
	BEGIN                          
	------------------------                          
	--DRIVER DETAILS START                            
	------------------------ 
		SELECT                            
		@DRIVERFNAME      =  ISNULL(ADDS.DRIVER_FNAME,''),                            
		@DRIVERMNAME     =  ISNULL(ADDS.DRIVER_MNAME,''),                            
		@DRIVERLNAME      =  ISNULL(ADDS.DRIVER_LNAME,''),                   
		@DRIVERCODE       =  ISNULL(ADDS.DRIVER_CODE,''),             
		@BIRTHDATE           =  CONVERT(CHAR(10),ADDS.DRIVER_DOB,101) ,           
		@GENDER         =  ISNULL(ADDS.DRIVER_SEX,'')  ,                          
		@MARITALSTATUS     =  ISNULL(ADDS.DRIVER_MART_STAT,'')   ,       
		--ITRACK 5974     
		/*@NOCYCLENDMT    =  CASE ISNULL(ADDS.NO_CYCLE_ENDMT,'0')                          
		WHEN '1' THEN 'Y'                   
		ELSE 'N'                          
		END*/
		@NOCYCLENDMT    =  CASE ISNULL(ADDS.NO_CYCLE_ENDMT,'1')                          
		WHEN '0' THEN 'Y'                   
		ELSE 'N'                          
		END,                     
		@DATELICENSED  = ADDS.DATE_LICENSED,                      
		@LICUNDER3YRS  = CASE            
		WHEN DATEDIFF(DAY, CONVERT(CHAR(10),ADDS.DATE_LICENSED,101), @APPEFFECTIVEDATE)/365 >= 3 THEN 'N'                          
		ELSE 'Y'              
		END,                           
		--@VEHICLEASSIGNEDASOPERATOR     = ISNULL(ADAV.VEHICLE_ID,'0'),                               
		--@VEHICLEDRIVEDAS    = CAST(ISNULL(ADDS.APP_VEHICLE_PRIN_OCC_ID,0)AS VARCHAR),                             
		@DRIVERCLASSCOMPONENT1  = '',  -- NOT REQUIRED THROUGH APP                          
		@DRIVERCLASSCOMPONENT2   = ''  -- NOT REQUIRED THROUGH APP                          
		--@SUMOFMISCPOINTS   =  CASE WHEN DATEDIFF(DAY, CONVERT(CHAR(10),ADDS.DATE_LICENSED,101), @APPEFFECTIVEDATE)/365 >= 3 THEN '3' ELSE '' END  --ITRACK # 5081                    
		--@NOCYCLENDM = CASE ISNULL(NO_CYCLE_ENDMT,'0') WHEN '1' THEN 'Y' ELSE '0' END                    
		FROM                       
		APP_DRIVER_DETAILS ADDS WITH (NOLOCK) INNER JOIN APP_DRIVER_ASSIGNED_VEHICLE ADAV WITH (NOLOCK) ON   ADDS.CUSTOMER_ID = ADAV.CUSTOMER_ID          
		AND ADDS.APP_ID = ADAV.APP_ID           
		AND ADDS.APP_VERSION_ID = ADAV.APP_VERSION_ID          
		AND ADDS.DRIVER_ID = ADAV.DRIVER_ID          
		INNER JOIN APP_VEHICLES  ON           		
		ADAV.CUSTOMER_ID = APP_VEHICLES.CUSTOMER_ID          
		AND ADAV.APP_ID = APP_VEHICLES.APP_ID           
		AND ADAV.APP_VERSION_ID = APP_VEHICLES.APP_VERSION_ID          
		AND ADAV.VEHICLE_ID = APP_VEHICLES.VEHICLE_ID            
		WHERE                                     		
		ADDS.CUSTOMER_ID=@CUSTOMERID AND ADDS.APP_ID=@APPID AND ADDS.APP_VERSION_ID=@APPVERSIONID AND ADDS.DRIVER_ID=@DRIVERID           
		--AND ADAV.APP_VEHICLE_PRIN_OCC_ID <> 11931 
	END
	ELSE --Added by Manoj Rathore on 19 Jun. 2009 Itrack # 5847 When Driver type is "Does not operate cycle"
	BEGIN
		SELECT                            
		@DRIVERFNAME      =  ISNULL(ADDS.DRIVER_FNAME,''),                            
		@DRIVERMNAME     =  ISNULL(ADDS.DRIVER_MNAME,''),                            
		@DRIVERLNAME      =  ISNULL(ADDS.DRIVER_LNAME,''),                   
		@DRIVERCODE       =  ISNULL(ADDS.DRIVER_CODE,''),                    
		@BIRTHDATE           =  CONVERT(CHAR(10),ADDS.DRIVER_DOB,101) ,           
		@GENDER         =  ISNULL(ADDS.DRIVER_SEX,'')  ,                          
		@MARITALSTATUS     =  ISNULL(ADDS.DRIVER_MART_STAT,'')   ,     
		@NOCYCLENDMT    =  CASE ISNULL(ADDS.NO_CYCLE_ENDMT,'1')                          
		WHEN '0' THEN 'Y'                   
		ELSE 'N'                          
		END,                     
		@DATELICENSED  = ADDS.DATE_LICENSED,                      
		@LICUNDER3YRS  = CASE            
		WHEN DATEDIFF(DAY, CONVERT(CHAR(10),ADDS.DATE_LICENSED,101), @APPEFFECTIVEDATE)/365 >= 3 THEN 'N'                          
		ELSE 'Y'              
		END,                             
		@DRIVERCLASSCOMPONENT1  = '',  -- NOT REQUIRED THROUGH APP                          
		@DRIVERCLASSCOMPONENT2   = ''  -- NOT REQUIRED THROUGH APP 
		FROM                       
		APP_DRIVER_DETAILS ADDS WITH (NOLOCK)          
		WHERE                                     		
		ADDS.CUSTOMER_ID=@CUSTOMERID AND ADDS.APP_ID=@APPID AND ADDS.APP_VERSION_ID=@APPVERSIONID AND 
		ADDS.DRIVER_ID=@DRIVERID 
	
	END      

IF(@LICUNDER3YRS = 'Y' )
SET @SUMOFMISCPOINTS = '3'
-------------------------------------------------------------------------------------------------------------------      
if Object_id('tempdb..#tempvehicle') IS NOT NULL                
DROP TABLE #tempvehicle             
            
create table #tempvehicle            
(            
VEHICLE_ID int,            
Assigned_type int            
)            
INSERT INTO #tempvehicle            
(            
VEHICLE_ID,            
Assigned_type            
)            
--nolock added by Sumit Chhabra on Jun 08, 2007          
select vehicle_id,app_vehicle_prin_occ_id from APP_DRIVER_ASSIGNED_VEHICLE   with (nolock)          
where  CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND DRIVER_ID=@DRIVERID             
           
select @VEHICLEASSIGNEDASOPERATOR = VEHICLE_ID from #tempvehicle where Assigned_type = 11399            
SET @VEHICLEDRIVEDAS = 'Principal'           
SET @VEHICLEDRIVEDASCODE = 'PNPA^PRINCIPAL'  
        
if (@VEHICLEASSIGNEDASOPERATOR is null)            
BEGIN            
select @VEHICLEASSIGNEDASOPERATOR = VEHICLE_ID from #tempvehicle where Assigned_type = 11398            
SET @VEHICLEDRIVEDAS = 'Principal'  
SET @VEHICLEDRIVEDASCODE = 'PPA^PRINCIPAL'            
END            
if (@VEHICLEASSIGNEDASOPERATOR is null)            
BEGIN            
 select @VEHICLEASSIGNEDASOPERATOR = VEHICLE_ID from #tempvehicle where Assigned_type = 11930            
SET @VEHICLEDRIVEDAS = 'Youthful Principal'            
END             
            
if (@VEHICLEASSIGNEDASOPERATOR is null)            
BEGIN         
 select @VEHICLEASSIGNEDASOPERATOR = VEHICLE_ID from #tempvehicle where Assigned_type = 11929            
SET @VEHICLEDRIVEDAS = 'Youthful Principal'            
END             
            
if (@VEHICLEASSIGNEDASOPERATOR is null)            
BEGIN            
 select @VEHICLEASSIGNEDASOPERATOR = VEHICLE_ID from #tempvehicle where Assigned_type = 11928            
SET @VEHICLEDRIVEDAS = 'Youthful Occasional'            
END                              
if (@VEHICLEASSIGNEDASOPERATOR is null)            
BEGIN            
 select @VEHICLEASSIGNEDASOPERATOR = VEHICLE_ID from #tempvehicle where Assigned_type = 11927            
SET @VEHICLEDRIVEDAS = 'Youthful Occasional'            
END            
if (@VEHICLEASSIGNEDASOPERATOR is null)            
BEGIN           
 select @VEHICLEASSIGNEDASOPERATOR = VEHICLE_ID from #tempvehicle where Assigned_type = 11926            
SET @VEHICLEDRIVEDAS = 'Occasional'  
SET @VEHICLEDRIVEDASCODE= 'OMPA^OCCASIONAL'             
END            
if (@VEHICLEASSIGNEDASOPERATOR is null)            
BEGIN            
 select @VEHICLEASSIGNEDASOPERATOR = VEHICLE_ID from #tempvehicle where Assigned_type = 11925            
SET @VEHICLEDRIVEDAS = 'Occasional'  
SET @VEHICLEDRIVEDASCODE= 'OPA^OCCASIONAL'         
            
END            
if (@VEHICLEASSIGNEDASOPERATOR is null)            
BEGIN            
 select @VEHICLEASSIGNEDASOPERATOR = VEHICLE_ID from #tempvehicle where Assigned_type = 11931            
SET @VEHICLEDRIVEDAS = 'Not Rated'   
SET @VEHICLEDRIVEDASCODE ='NR^NR'           
END            
            
DROP TABLE #tempvehicle            
             
-------------------------------------------------------------------------------------------------------------------             
IF ISNULL(@NOCYCLENDMT,'')=''                    
 SET @NOCYCLENDMT='N'                   
                  
IF ISNULL(@DRIVERCODE,'')=''                  
 SET @DRIVERCODE= ' '                         
                          
--MARITAL STATUS                          
  SELECT @MARITALSTATUS = isnull(MLV.LOOKUP_VALUE_DESC,'') FROM MNT_LOOKUP_VALUES MLV  WITH (NOLOCK)                                
       INNER JOIN MNT_LOOKUP_TABLES MLT  WITH (NOLOCK) ON   MLV.LOOKUP_ID = MLT.LOOKUP_ID                           
       WHERE MLT.LOOKUP_NAME = 'MARST' AND MLV.LOOKUP_VALUE_CODE= @MARITALSTATUS                               
                            
--GENDER                          
      SELECT @GENDER = isnull(MLV.LOOKUP_VALUE_DESC,'') FROM MNT_LOOKUP_VALUES MLV WITH (NOLOCK)                                 
      INNER JOIN MNT_LOOKUP_TABLES MLT  WITH (NOLOCK) ON   MLV.LOOKUP_ID = MLT.LOOKUP_ID                                  
      WHERE MLT.LOOKUP_NAME = 'SEXCD' AND MLV.LOOKUP_VALUE_CODE= @GENDER                               
                            
----------------------                            
--DRIVER DETAILS END                            
--------------------                          
-------------------------                          
-- AGE OF DRIVER  START                            
-------------------------                    
SET @AGEOFDRIVER=DATEDIFF(DAY, @BIRTHDATE, @APPEFFECTIVEDATE)/365.2425    
SET @AGEOFDRIVER=dbo.piece(@AGEOFDRIVER,'.',1)   

SET @VIOLATION_APPLICABLE='Y'
SELECT @DRIVER_DRIV_TYPE=DRIVER_DRIV_TYPE FROM APP_DRIVER_DETAILS
WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND DRIVER_ID=@DRIVERID 

IF(@DRIVER_DRIV_TYPE='11942')
 BEGIN
 SET @VIOLATION_APPLICABLE='N'
 END                 
---------------------                            
 SELECT                             
      @DRIVERFNAME           AS DRIVERFNAME,                            
      @DRIVERMNAME          AS DRIVERMNAME,                            
      @DRIVERLNAME          AS DRIVERLNAME,                            
      @BIRTHDATE          AS BIRTHDATE,                            
      @GENDER           AS GENDER,          
      @MARITALSTATUS         AS MARITALSTATUS,                            
      @NOCYCLENDMT          AS NOCYCLENDMT,                                 
      @LICUNDER3YRS          AS LICUNDER3YRS,              
      @VEHICLEASSIGNEDASOPERATOR   AS  VEHICLEASSIGNEDASOPERATOR,                            
      @VEHICLEDRIVEDAS          AS  VEHICLEDRIVEDAS,                            
      @AGEOFDRIVER          AS  AGEOFDRIVER,             
      @DRIVERCODE           AS DRIVERCODE,     
      @VEHICLEDRIVEDASCODE AS VEHICLEDRIVEDASCODE,
      @SUMOFMISCPOINTS AS SUMOFMISCPOINTS,
	  ISNULL(@VIOLATION_APPLICABLE,'Y')		AS VIOLATION_APPLICABLE 
                           
 END     


GO

