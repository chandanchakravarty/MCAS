IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetWatercraftRule_Operators]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetWatercraftRule_Operators]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                                                                      
Proc Name                : Dbo.Proc_GetWatercraftRule_Operators                                                                                  
Created by               : Praveen Kasana                                                                                  
Date                     : 7 Dec.,2005                                                        
Purpose                  : To get the Watercraft Operators info for Rules                                                                                    
Revison History          :                                                                                      
Used In                  : Wolverine                                                                                      
------------------------------------------------------------                                                                                      
Date     Review By          Comments                                                                                      
------   ------------       -------------------------*/         
-- drop proc dbo.Proc_GetWatercraftRule_Operators 1707,88,1,1,''        
CREATE  proc [dbo].[Proc_GetWatercraftRule_Operators]                                                                                                           
(                                                                                                            
@CUSTOMERID    int,                                                                                                            
@APPID    int,                                                                                                            
@APPVERSIONID   int,                                                                                                  
@DRIVERID int,                                                                   
@DESC varchar(10)                                                   
)                                                                                                            
AS                                                                                             
BEGIN                                                                                             
--    Operator Detail                                                                                  
--APP_WATERCRAFT_DRIVER_DETAILS                                                     
declare @DRIVER_NAME nvarchar(225)                                                                                  
declare @DRIVER_FNAME nvarchar(75)                                                                            
declare @DRIVER_LNAME nvarchar(75)                                                                                 
declare @DRIVER_CODE nvarchar(20)                                                                                  
declare @DRIVER_ADD1 nvarchar(70)                                                   
declare @DRIVER_STATE nvarchar(5)                                        
declare @DRIVER_CITY nvarchar(20)                                        
declare @DRIVER_ZIP varchar(11)                                                         
--declare @DRIVER_DOB datetime                                          
declare @DRIVER_DOB varchar(20)                                          
declare @DRIVER_SEX nchar(8)                          
-- declare @INT_DRIVER_SEX int                                                                                 
declare @DRIVER_LIC_STATE nvarchar(5)                                                                                
declare @DRIVER_DRIV_LIC nvarchar(30)                                                                                
--declare @VEHICLE_ID int                              
--declare @VEHICLE_ID varchar(20)              
--declare @PRIN_OCC_ID int                         
declare @PRIN_OCC_ID varchar(20)        
declare @VIOLATIONS varchar(20)        
declare @DRIVER_COST_GAURAD_AUX varchar(20)        
DECLARE @DRIVER_DRIV_TYPE VARCHAR(10)            
DECLARE @APP_EFFECTIVE_DATE DATETIME       
DECLARE @APP_LOB NVARCHAR(10) --Added by Charles on 20-Nov-09 for Itrack 6743        
    
SELECT @APP_EFFECTIVE_DATE=APP_EFFECTIVE_DATE,@APP_LOB=APP_LOB FROM APP_LIST WITH(NOLOCK) --Added APP_LOB by Charles on 20-Nov-09 for Itrack 6743       
where CUSTOMER_ID=@CUSTOMERID and  APP_ID=@APPID  and  APP_VERSION_ID=@APPVERSIONID                                          
                       
IF EXISTS (select CUSTOMER_ID from APP_WATERCRAFT_DRIVER_DETAILS                                             
where CUSTOMER_ID=@CUSTOMERID and  APP_ID=@APPID  and  APP_VERSION_ID=@APPVERSIONID   and DRIVER_ID=@DRIVERID)                                           
BEGIN                                 
 SELECT @DRIVER_NAME=(isnull(DRIVER_FNAME,'') + '  ' + isnull(DRIVER_MNAME,'') + '  ' + isnull(DRIVER_LNAME,'')),                              
 @DRIVER_FNAME=isnull(DRIVER_FNAME,''),@DRIVER_LNAME=isnull(DRIVER_LNAME,''),@DRIVER_DRIV_LIC=isnull(DRIVER_DRIV_LIC,''),                          
 @DRIVER_CODE=isnull(DRIVER_CODE,''),@DRIVER_SEX=isnull(upper(DRIVER_SEX),'') ,@DRIVER_STATE=isnull(DRIVER_STATE,''),@DRIVER_CITY=isnull(DRIVER_CITY,''),                                                              
 @DRIVER_ZIP=isnull(DRIVER_ZIP,''),@DRIVER_LIC_STATE=isnull(DRIVER_LIC_STATE,''),                                                                                
 @DRIVER_DOB=isnull(convert(varchar(25),DRIVER_DOB),''),@DRIVER_ADD1=isnull(DRIVER_ADD1,''),                                                                                
 --@VEHICLE_ID=isnull(convert(varchar(20),VEHICLE_ID),''),        
 @PRIN_OCC_ID=isnull(convert(varchar(20),APP_VEHICLE_PRIN_OCC_ID),''),        
 @VIOLATIONS =isnull(convert(varchar(20),VIOLATIONS),'') ,                                                                        
 @DRIVER_COST_GAURAD_AUX =isnull(convert(varchar(20),DRIVER_COST_GAURAD_AUX),'') ,        
 @DRIVER_DRIV_TYPE = isnull(convert(varchar(10),DRIVER_DRIV_TYPE),'')                                                                             
 from APP_WATERCRAFT_DRIVER_DETAILS                                                                                  
 where CUSTOMER_ID=@CUSTOMERID and  APP_ID=@APPID  and  APP_VERSION_ID=@APPVERSIONID   and DRIVER_ID=@DRIVERID                                                                                  
END                                                  
ELSE                                                  
BEGIN                                                         
 SET @DRIVER_NAME =''                                                                                  
 SET @DRIVER_FNAME=''                                                  
 SET @DRIVER_LNAME=''                                                     
 SET @DRIVER_CODE=''                                                                                  
 SET @DRIVER_DRIV_LIC=''                                                                                  
 SET @DRIVER_SEX=''                                                                                 
 SET @DRIVER_STATE =''                                         
 SET @DRIVER_CITY=''                                        
 SET @DRIVER_ADD1 =''                                        
 SET @DRIVER_ZIP =''         
                                                                                
 SET @VIOLATIONS =''        
 SET @DRIVER_COST_GAURAD_AUX=''        
 SET @DRIVER_DRIV_TYPE=''                
 SET @DRIVER_DOB =''                                                  
 SET @DRIVER_LIC_STATE =''                                                                                 
 --SET @VEHICLE_ID =''                            
 SET @PRIN_OCC_ID =''                                                                          
END                                 
                                                                                
---Gender check--                                          
IF (@DRIVER_SEX='M')                                           
BEGIN                                          
 SET @DRIVER_SEX='MALE'                                    
END                                          
                                          
IF (@DRIVER_SEX='F')                                           
BEGIN                
 SET @DRIVER_SEX='FEMALE'                                                                        
END                            
               
--LIC Check        
--<option value="3478">Not licensed</option>              
--If Driver type is Un Licensed then Drivers license number is not mandatory Itrack #3983        
--EXCLUDED Driver Rule: Itrack 4710 Note part        
if (@DRIVER_DRIV_TYPE = '3478' or @DRIVER_DRIV_TYPE ='3477')           
begin        
 SET @DRIVER_DRIV_LIC='N'          
end             
                                         
---         
DECLARE  @INTCOUNTISACTIVE INT                                                
DECLARE  @DEACTIVATEBOAT CHAR         
SELECT @INTCOUNTISACTIVE=COUNT(DRIVER_ID) FROM APP_WATERCRAFT_DRIVER_DETAILS D                                                        
WHERE D.DRIVER_ID=@DRIVERID AND D.CUSTOMER_ID=@CUSTOMERID AND  D.APP_ID=@APPID  AND  D.APP_VERSION_ID=@APPVERSIONID                        
AND D.VEHICLE_ID IN                                                         
( SELECT V.BOAT_ID FROM APP_WATERCRAFT_INFO V                                                        
WHERE V.CUSTOMER_ID=@CUSTOMERID AND  V.APP_ID=@APPID  AND  V.APP_VERSION_ID=@APPVERSIONID AND V.IS_ACTIVE='N')                                                        
  --                                                  
 IF (@INTCOUNTISACTIVE>0)                                                            
  BEGIN                                    
   SET @DEACTIVATEBOAT='Y'                         
  END                                                             
 ELSE                               
  BEGIN                                                             
   SET @DEACTIVATEBOAT='N'                           
  END                                                            
 ---  MVR points   -----     -----------------                                      
                                             
--Rule for ineligible automobile driving record.                                           
--declare @IS_CONVICTED_ACCIDENT char                                                        
DECLARE @INT_CONVICTED_ACCIDENT INT                                           
IF EXISTS(SELECT APP_ID  FROM APP_WATERCRAFT_GEN_INFO                                                          
WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID)                                                     
BEGIN                                                         
 SELECT @INT_CONVICTED_ACCIDENT=ISNULL(IS_CONVICTED_ACCIDENT,0)                                              
 FROM APP_WATERCRAFT_GEN_INFO                                 
 WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID                                                       
END                                                        
ELSE                                                        
 BEGIN                                                         
 SET @INT_CONVICTED_ACCIDENT =''                                                
 END                                          
                                
        
                              
--------------------         
DECLARE @INTVEHICLE_ID INT                                                     
DECLARE @ISDRVASSIGNEDVEH CHAR                      
 SELECT @INTVEHICLE_ID=COUNT(VEHICLE_ID )                                                   
 FROM  APP_WATERCRAFT_DRIVER_DETAILS                      
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND DRIVER_ID=@DRIVERID                                              
 AND VEHICLE_ID IS NULL                             
 IF(@INTVEHICLE_ID>0 )                                                
  BEGIN                                                     
   SET @ISDRVASSIGNEDVEH='Y'                       
  END                                                     
 ELSE                      
  BEGIN                                                     
   SET @ISDRVASSIGNEDVEH='N'                     
  END        
                         
                  
/*        
Operators/Household Member         
MVR Info can be supplied by the agency or  can be done electronically  -         
Major Violation are all those that fall in the Violation Type Field          
 - Accident and Violations after Accident         
 - Serious Offences         
All other Violation Types are considered Minor         
On New business look at all the Points total per operator - for all Major violations in the last 5 years  on the        
Effective date of the policy and the year of the violation -        
IF the number of Points is 6 or Higher - Submit or if any of the points are showing as N/A        
--Total Point Major violation + Minar Violation greater than 6 refer to under writer*/         
        
----SD Points ..                          
 DECLARE @INTSD_PONITS INT            
 DECLARE @SD_POINTS CHAR          
 --Minor Violations                                 
 SELECT @INTSD_PONITS=ISNULL(SUM(ISNULL(POINTS_ASSIGNED,0)) + SUM(ISNULL(ADJUST_VIOLATION_POINTS,0)),0)                                                                                       
 FROM APP_WATER_MVR_INFORMATION  P        
INNER JOIN MNT_VIOLATIONS M ON M.VIOLATION_ID=P.VIOLATION_TYPE        
 WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND DRIVER_ID=@DRIVERID                                                                          
-- AND (YEAR(GETDATE())- YEAR(OCCURENCE_DATE))<= 3         
 AND DATEDIFF(DAY,MVR_DATE,@APP_EFFECTIVE_DATE)<= 3*365.25         
 AND DATEDIFF(DAY,MVR_DATE,@APP_EFFECTIVE_DATE)>= 0        
 --AND (VIOLATION_TYPE<>2099 AND VIOLATION_TYPE<>1830)         
AND M.VIOLATION_CODE NOT IN ('40000','10000','SUSPN')        
AND P.IS_ACTIVE='Y'--  AND  @INT_CONVICTED_ACCIDENT=1                                          
 /* Commented by Manoj Rathore        
  SELECT @INTSD_PONITS=ISNULL(SUM(ISNULL(WOLVERINE_VIOLATIONS,0)) + SUM(ISNULL(ADJUST_VIOLATION_POINTS,0)),0)                                                                                       
  FROM APP_WATER_MVR_INFORMATION                                        
  INNER JOIN  VIW_DRIVER_VIOLATIONS  ON APP_WATER_MVR_INFORMATION.VIOLATION_ID = VIW_DRIVER_VIOLATIONS.VIOLATION_ID                                        
  WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND DRIVER_ID=@DRIVERID                                                                          
  AND (YEAR(GETDATE())- YEAR(MVR_DATE))<= 3 AND (VIOLATION_TYPE<>2099 AND VIOLATION_TYPE<>1830) AND APP_WATER_MVR_INFORMATION.IS_ACTIVE='Y'--  AND  @INT_CONVICTED_ACCIDENT=1                                          
 */          
--Major Violations        
 DECLARE @WATER_MAJOR_VIOLATION VARCHAR        
 DECLARE @intMAJOR_VIOLATION INT        
 SET  @WATER_MAJOR_VIOLATION='N'        
 SELECT  @intMAJOR_VIOLATION=ISNULL((SUM(ISNULL(POINTS_ASSIGNED,0))+ SUM(ISNULL(ADJUST_VIOLATION_POINTS,0))),0)                                       
 FROM APP_WATER_MVR_INFORMATION  P        
INNER JOIN MNT_VIOLATIONS M ON M.VIOLATION_ID=P.VIOLATION_TYPE        
 WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND DRIVER_ID=@DRIVERID                                                                          
 --AND (VIOLATION_TYPE=2099 or VIOLATION_TYPE=1830)         
AND M.VIOLATION_CODE IN ('40000','10000','SUSPN')        
--AND (YEAR(GETDATE())- YEAR(OCCURENCE_DATE))<= 5         
AND DATEDIFF(DAY,MVR_DATE,@APP_EFFECTIVE_DATE)<= 5*365.5         
 AND DATEDIFF(DAY,MVR_DATE,@APP_EFFECTIVE_DATE)>= 0        
AND P.IS_ACTIVE='Y'        
/* Commented by Manoj Rathore        
  SELECT  @intMAJOR_VIOLATION=ISNULL((SUM(ISNULL(POINTS_ASSIGNED,0))+ SUM(ISNULL(ADJUST_VIOLATION_POINTS,0))),0)                                       
  FROM APP_WATER_MVR_INFORMATION                                        
  INNER JOIN  VIW_DRIVER_VIOLATIONS  ON APP_WATER_MVR_INFORMATION.VIOLATION_ID = VIW_DRIVER_VIOLATIONS.VIOLATION_ID                                        
  WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND DRIVER_ID=@DRIVERID                                                  
  AND (VIOLATION_TYPE=2099 or VIOLATION_TYPE=1830) AND (YEAR(GETDATE())- YEAR(MVR_DATE))<= 5 AND APP_WATER_MVR_INFORMATION.IS_ACTIVE='Y'        
*/        
--Total Point Major violation(with in 5 years) + Minar Violation(with in 3 years) greater than 6 refer to under writer        
 --ADDED BY PRAVESH ON 13 OCT 08 iTRACK 4716        
 DECLARE @ACCIDENT_POINTS INT        
 CREATE TABLE #TEMP_VIOLATION        
 (SUM_MVR_POINTS INT,ACCIDENT_POINTS INT,COUNT_ACCIDENTS INT,MVR_POINTS INT)        
 INSERT INTO #TEMP_VIOLATION exec GetMVRViolationPoints @CUSTOMERID,@APPID,@APPVERSIONID,@DRIVERID,3,3,3,1000        
 SELECT @ACCIDENT_POINTS =ACCIDENT_POINTS FROM #TEMP_VIOLATION        
 DROP TABLE #TEMP_VIOLATION        
        
 IF(@INTSD_PONITS + @intMAJOR_VIOLATION + @ACCIDENT_POINTS >= 6)              
  BEGIN                                       
   SET @SD_POINTS='Y'                                                            
  END                                                             
 ELSE                                                            
  BEGIN                                                             
   SET @SD_POINTS='N'                                                             
  END         
--added by Pravesh on 11 sep08 Itrack 4719        
--If violations are entered with a negative – number in the Points assigned field         
--This must be a referral to the underwriter – Message – Violations with Negative values        
DECLARE @NEGATIVE_VIOLATION CHAR(1)        
 SET  @NEGATIVE_VIOLATION='N'        
IF EXISTS(        
  SELECT  CUSTOMER_ID                                       
  FROM APP_WATER_MVR_INFORMATION  WMVR         
     inner join VIW_DRIVER_VIOLATIONS VWVIOL ON VWVIOL.VIOLATION_ID=WMVR.VIOLATION_ID        
  WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND DRIVER_ID=@DRIVERID                                                                          
  --AND (YEAR(GETDATE())- YEAR(OCCURENCE_DATE))<= 5         
  AND WMVR.IS_ACTIVE='Y'        
  --AND ISNULL(VWVIOL.WOLVERINE_VIOLATIONS,0)<=0        
  AND ISNULL(POINTS_ASSIGNED,0)<0        
  )        
  SET  @NEGATIVE_VIOLATION='Y'         
--end here        
        
--================================================================================================        
DECLARE @VEHICLE_ID CHAR        
DECLARE @INTCOUNT INT         
        
 SELECT @INTCOUNT=COUNT(APP_VEHICLE_PRIN_OCC_ID) FROM APP_OPERATOR_ASSIGNED_BOAT with(nolock)        
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID = @APPVERSIONID  --AND BOAT_ID = @DRIVERID         
 IF(@INTCOUNT= 0)        
 BEGIN        
  SET @VEHICLE_ID=''        
 END        
 ELSE         
 BEGIN        
  SET @VEHICLE_ID=@INTCOUNT        
 END        
        
SELECT @PRIN_OCC_ID=isnull(convert(varchar(20),APP_VEHICLE_PRIN_OCC_ID),'')        
FROM APP_OPERATOR_ASSIGNED_BOAT with(nolock)        
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID = @APPVERSIONID and DRIVER_ID=@DRIVERID         
 --================================================================================================       
      
-- Added by Charles on 18-Nov-09 for Itrack 6737  
-- DECLARE @REC_VEHICLE_PRIN_OCC_ID CHAR(2)      
-- SET @REC_VEHICLE_PRIN_OCC_ID = '-1' 
--
--IF @APP_LOB = '1'    
--BEGIN              
-- IF EXISTS(SELECT CUSTOMER_ID FROM APP_OPERATOR_ASSIGNED_RECREATIONAL_VEHICLE WITH(NOLOCK)      
-- WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID = @APPVERSIONID and DRIVER_ID=@DRIVERID      
-- AND APP_REC_VEHICLE_PRIN_OCC_ID = 0)      
-- BEGIN      
--  SET @REC_VEHICLE_PRIN_OCC_ID=''      
-- END      
--END  
--- Added till here       
      
-- Added by Charles on 20-Nov-09 for Itrack 6743       
IF @APP_LOB = '1'    
BEGIN      
 IF NOT EXISTS(SELECT BOAT_ID FROM APP_WATERCRAFT_INFO WITH(NOLOCK) WHERE               
     APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND CUSTOMER_ID=@CUSTOMERID AND IS_ACTIVE = 'Y')      
  SET @DRIVER_COST_GAURAD_AUX = '-1'     
END    
--- Added till here       
    
SELECT              
 @DRIVER_NAME  as DRIVER_NAME ,                                                                               
 @DRIVER_FNAME as DRIVER_FNAME ,                                                
 @DRIVER_LNAME as DRIVER_LNAME ,                                                    
 @DRIVER_CODE  as DRIVER_CODE,                                                                       
 @DRIVER_DRIV_LIC as DRIVER_DRIV_LIC,                                                                                 
 @DRIVER_SEX  as   DRIVER_SEX,                                        
 @DRIVER_ADD1 as DRIVER_ADD1,                                     
 @DRIVER_STATE as DRIVER_STATE,                                             
 @DRIVER_ZIP as DRIVER_ZIP,                                                                               
 @DRIVER_DOB as  DRIVER_DOB,                                                
 @DRIVER_LIC_STATE as DRIVER_LIC_STATE,        
 @DRIVER_DRIV_TYPE AS DRIVER_DRIV_TYPE,                                                
 @VEHICLE_ID as VEHICLE_ID,                              
 @PRIN_OCC_ID as PRIN_OCC_ID,                                                
 @DEACTIVATEBOAT as DEACTIVATEBOAT ,        
 @DRIVER_COST_GAURAD_AUX as DRIVER_COST_GAURAD_AUX,        
 @VIOLATIONS as VIOLATIONS ,                                                       
 -- Rule only                                                       
 @SD_POINTS as SD_POINTS,                            
 @DRIVER_CITY as DRIVER_CITY ,        
 @WATER_MAJOR_VIOLATION AS WATER_MAJOR_VIOLATION ,        
@NEGATIVE_VIOLATION AS NEGATIVE_VIOLATION      
--@REC_VEHICLE_PRIN_OCC_ID AS REC_VEHICLE_PRIN_OCC_ID -- Added by Charles on 18-Nov-09 for Itrack 6737          
----------------         
END        
        
GO

