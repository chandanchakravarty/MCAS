IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetWatercraftRule_Operators_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetWatercraftRule_Operators_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------                                                                                  
Proc Name                : Dbo.Proc_GetWatercraftRule_Operators_Pol                                                                              
Created by               : Ashwnai                                                                            
Date                     : 02 Mar 2006                                                   
Purpose                  : To get the Watercraft Operators info for policy Rules                                                                                
Revison History          :                                                                                  
Used In                  : Wolverine                                                                                  
------------------------------------------------------------                                                                                  
Date     Review By          Comments                                                                                  
------   ------------       -------------------------*/                                     
-- drop proc dbo.Proc_GetWatercraftRule_Operators_Pol 547,83,2,1                     
CREATE  proc [dbo].[Proc_GetWatercraftRule_Operators_Pol]                                                                                                       
(                                                                                                        
@CUSTOMER_ID    int,                                                                                                        
@POLICY_ID    int,                                                                                                        
@POLICY_VERSION_ID   int,                                                                                              
@DRIVER_ID int                                                               
)                                                                                                        
AS                                                                                         
BEGIN                                                                                         
--    Operator Detail                                                                              
--POL_WATERCRAFT_DRIVER_DETAILS                                                 
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
--declare @INT_DRIVER_SEX int                                                                             
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
DECLARE @POLICY_LOB NVARCHAR(10) --Added by Charles on 23-Nov-09 for Itrack 6743              
              
SELECT @APP_EFFECTIVE_DATE=APP_EFFECTIVE_DATE,@POLICY_LOB=POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WITH(NOLOCK) --POLICY_LOB added by Charles on 23-Nov-09 for Itrack 6743              
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                                                       
   IF EXISTS (SELECT CUSTOMER_ID FROM POL_WATERCRAFT_DRIVER_DETAILS                                              
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND DRIVER_ID=@DRIVER_ID)                                                                              
BEGIN                                         
SELECT  @DRIVER_NAME=(isnull(DRIVER_FNAME,'') + '  ' + isnull(DRIVER_MNAME,'') + '  ' + isnull(DRIVER_LNAME,'')),                                                                            
 @DRIVER_FNAME=isnull(DRIVER_FNAME,''),@DRIVER_LNAME=isnull(DRIVER_LNAME,''),@DRIVER_DRIV_LIC=isnull(DRIVER_DRIV_LIC,''),                                                                            
 @DRIVER_CODE=isnull(DRIVER_CODE,''),@DRIVER_SEX=isnull(upper(DRIVER_SEX),'') ,@DRIVER_STATE=isnull(DRIVER_STATE,''),@DRIVER_CITY=isnull(DRIVER_CITY,''),                                                          
 @DRIVER_ZIP=isnull(DRIVER_ZIP,''),@DRIVER_LIC_STATE=isnull(DRIVER_LIC_STATE,''),                                                                            
 @DRIVER_DOB=isnull(convert(varchar(25),DRIVER_DOB),''),@DRIVER_ADD1=isnull(DRIVER_ADD1,''),                                                                            
 --@VEHICLE_ID=isnull(convert(varchar(20),VEHICLE_ID),''),              
 @PRIN_OCC_ID=isnull(convert(varchar(20),APP_VEHICLE_PRIN_OCC_ID),''),              
 @VIOLATIONS =isnull(convert(varchar(20),VIOLATIONS),'') ,                                                                              
 @DRIVER_COST_GAURAD_AUX =isnull(convert(varchar(20),DRIVER_COST_GAURAD_AUX),''),              
 @DRIVER_DRIV_TYPE = isnull(convert(varchar(10),DRIVER_DRIV_TYPE),'')                                                                         
                                                                             
FROM POL_WATERCRAFT_DRIVER_DETAILS                                                                              
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND DRIVER_ID=@DRIVER_ID                                                                              
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
 SET @DRIVER_SEX =''                                       
 SET @DRIVER_DOB =''                
 SET @DRIVER_LIC_STATE =''                                                                             
 --SET @VEHICLE_ID =''     
 SET @PRIN_OCC_ID =''                                                              
 SET @VIOLATIONS =''              
 SET @DRIVER_COST_GAURAD_AUX=''               
 set @DRIVER_DRIV_TYPE=''                                                                       
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
IF (@DRIVER_DRIV_TYPE = '3478' or @DRIVER_DRIV_TYPE ='3477')                 
BEGIN              
 SET @DRIVER_DRIV_LIC='N'                
END                            
                          
                                     
---                                            
DECLARE  @INTCOUNTISACTIVE INT                                                        
DECLARE  @DEACTIVATEBOAT CHAR                                                        
                          
SELECT @INTCOUNTISACTIVE=COUNT(DRIVER_ID) FROM POL_WATERCRAFT_DRIVER_DETAILS D                                                    
WHERE D.DRIVER_ID=@DRIVER_ID AND D.CUSTOMER_ID=@CUSTOMER_ID AND  D.POLICY_ID=@POLICY_ID  AND  D.POLICY_VERSION_ID=@POLICY_VERSION_ID                                                     
AND D.VEHICLE_ID IN                  
( SELECT V.BOAT_ID FROM POL_WATERCRAFT_INFO V                  
WHERE V.CUSTOMER_ID=@CUSTOMER_ID AND  V.POLICY_ID=@POLICY_ID  AND  V.POLICY_VERSION_ID=@POLICY_VERSION_ID AND V.IS_ACTIVE='N')                                                    
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
                                         
--RULE FOR INELIGIBLE AUTOMOBILE DRIVING RECORD.                                       
--DECLARE @IS_CONVICTED_ACCIDENT CHAR                                                    
DECLARE @INT_CONVICTED_ACCIDENT INT                                      
IF EXISTS(SELECT POLICY_ID  FROM POL_WATERCRAFT_GEN_INFO                                                      
WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID)                                                 
BEGIN                                                     
 SELECT @INT_CONVICTED_ACCIDENT=ISNULL(IS_CONVICTED_ACCIDENT,-1)                                          
 FROM POL_WATERCRAFT_GEN_INFO                             
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                                                   
END                                                    
ELSE                            
BEGIN                                                     
 SET @INT_CONVICTED_ACCIDENT =''                                            
END                                      
                    
--=======================                             
 DECLARE @INTVEHICLE_ID INT                                                 
 DECLARE @ISDRVASSIGNEDVEH CHAR                                                 
                                                
 SELECT @INTVEHICLE_ID=COUNT(VEHICLE_ID )                                                
 FROM  POL_WATERCRAFT_DRIVER_DETAILS                                       
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND DRIVER_ID=@DRIVER_ID                                                           
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
ALL other Violation Types are considered Minor               
ON NEW BUSINESS LOOK AT ALL THE POINTS TOTAL PER OPERATOR - for all Major violations in the last 5 years  on the effective date of the policy and the year of the violation and the points total for all Minor violations               
in the last 3 years based on the effective date of the policy and the year of the violation - if the number of points is 6 or higher - submit or if any of the points are showing as N/A              
ON RENEWAL LOOK AT ALL THE POINTS TOTAL PER OPERATOR FOR ALL Major violations in the last 5 years  on the effective date of the policy and the year of the violation and the points total              
for all Minor violations in the last 3 years based on the effective date of the policy and the year of the violation - if the number of points is 9 or higher - submit or               
if any of the points are showing as N/A              
Mid term updates on violations will be controlled by the underwriters - manually               
Operators/Members - MVR tab               
*/              
 DECLARE @WATER_MAJOR_VIOLATION VARCHAR              
 DECLARE @RENEW_WATER_MAJOR_VIOLATION VARCHAR              
 DECLARE @IS_RENEW INT              
 DECLARE @MVR_MAJOR_POINTS INT              
 DECLARE @MVR_MAJOR_POINT INT              
 DECLARE @POINT_ASSIGNED INT              
 DECLARE @ACCIDENT_POINTS INT              
 SET  @WATER_MAJOR_VIOLATION='N'              
 SET  @RENEW_WATER_MAJOR_VIOLATION='N'              
 SET  @IS_RENEW=0              
 -- Condition for Renewal               
 SELECT @IS_RENEW=COUNT(*) FROM POL_POLICY_PROCESS WITH(NOLOCK)              
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID                
 AND  NEW_POLICY_VERSION_ID=@POLICY_VERSION_ID  AND PROCESS_ID IN (5,18)             
             
--ITRACK # 5464 AND 5950 AND ADDED BY MANOJ RATHORE ON 9TH JUN. 2009              
 /*IF (@IS_RENEW=0)              
 BEGIN               
 SET @MVR_MAJOR_POINT=6 END              
 ELSE              
 --BEGIN SET @MVR_MAJOR_POINTS=9 END              
 BEGIN SET @MVR_MAJOR_POINTS=6 END*/         
        
 SET @MVR_MAJOR_POINT=6           
 SET @MVR_MAJOR_POINTS=6             
--ITRACK # 5464 AND 5950 AND ADDED BY MANOJ RATHORE ON 9TH JUN. 2009              
               
 ----SD Points Rule.. (Minor Violation)         
   DECLARE @INTSD_PONITS INT                                                        
  DECLARE @SD_POINTS CHAR ,              
      @RENEW_SD_POINTS CHAR                                                       
  SET @SD_POINTS='N'               
  SET @RENEW_SD_POINTS='N'                
                                                   
  SELECT @INTSD_PONITS=ISNULL((SUM(ISNULL(POINTS_ASSIGNED,0))+ SUM(ISNULL(ADJUST_VIOLATION_POINTS,0))),0)               
  FROM POL_WATERCRAFT_MVR_INFORMATION                                    
   INNER JOIN  VIW_DRIVER_VIOLATIONS  ON POL_WATERCRAFT_MVR_INFORMATION.VIOLATION_TYPE = VIW_DRIVER_VIOLATIONS.VIOLATION_ID                                    
   WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND DRIVER_ID=@DRIVER_ID                                                                      
   --AND (YEAR(GETDATE())- YEAR(MVR_DATE))<= 3               
  AND DATEDIFF(DD,MVR_DATE,@APP_EFFECTIVE_DATE)<= 3*365.25                
  AND DATEDIFF(DD,MVR_DATE,@APP_EFFECTIVE_DATE)>= 0              
  --AND (VIOLATION_TYPE<>2099 AND VIOLATION_TYPE<>1830)               
  AND VIW_DRIVER_VIOLATIONS.VIOLATION_CODE NOT IN ('40000','10000','SUSPN')              
  AND POL_WATERCRAFT_MVR_INFORMATION.IS_ACTIVE='Y'               
  -- AND  @INT_CONVICTED_ACCIDENT=1                                                        
                
 --== Major Violation              
  SELECT @POINT_ASSIGNED=ISNULL((SUM(ISNULL(POINTS_ASSIGNED,0))+ SUM(ISNULL(ADJUST_VIOLATION_POINTS,0))),0) FROM POL_WATERCRAFT_MVR_INFORMATION                                              
   INNER JOIN  VIW_DRIVER_VIOLATIONS  ON POL_WATERCRAFT_MVR_INFORMATION.VIOLATION_TYPE = VIW_DRIVER_VIOLATIONS.VIOLATION_ID                                              
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND DRIVER_ID=@DRIVER_ID                                                                    
   --AND (VIOLATION_TYPE=2099 or VIOLATION_TYPE=1830)                
  AND VIW_DRIVER_VIOLATIONS.VIOLATION_CODE IN ('40000','10000','SUSPN')              
  --AND (YEAR(GETDATE())- YEAR(MVR_DATE))<= 5               
  AND DATEDIFF(DD,MVR_DATE,@APP_EFFECTIVE_DATE)<= 5*365.5              
  AND DATEDIFF(DD,MVR_DATE,@APP_EFFECTIVE_DATE)>= 0              
  AND POL_WATERCRAFT_MVR_INFORMATION.IS_ACTIVE='Y'              
  /*IF(@POINT_ASSIGNED >=@MVR_MAJOR_POINT)  -- At New Business(Major Violation) Points 6 or Higher than 6              
   BEGIN              
    SET @WATER_MAJOR_VIOLATION='Y'              
   END                       
  ELSE IF(@POINT_ASSIGNED >=@MVR_MAJOR_POINTS)  -- At Renewal Points 9 or higher than 9              
   BEGIN               
   SET @RENEW_WATER_MAJOR_VIOLATION='Y'              
   END              
  */               
--ADDED BY pRAVESH ON 13 OCT 08 iTRACK 4716                
 CREATE TABLE #TEMP_VIOLATION              
 (SUM_MVR_POINTS INT,ACCIDENT_POINTS INT,COUNT_ACCIDENTS INT,MVR_POINTS INT)              
 INSERT INTO #TEMP_VIOLATION exec GetMVRViolationPointsPol @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@DRIVER_ID,3,3,3,1000              
 SELECT @ACCIDENT_POINTS =ACCIDENT_POINTS FROM #TEMP_VIOLATION              
 DROP TABLE #TEMP_VIOLATION              
              
                                              
  IF(@INTSD_PONITS + @POINT_ASSIGNED + @ACCIDENT_POINTS >= @MVR_MAJOR_POINT)      -- At New Business(Minor Violation) Points 6 or Higher than 6                                                     
  BEGIN                                   
   SET @SD_POINTS='Y'                                                        
  END                     
  ELSE IF(@INTSD_PONITS + @POINT_ASSIGNED + @ACCIDENT_POINTS >= @MVR_MAJOR_POINT)--@MVR_MAJOR_POINTS -- At Renewal Points 9 or higher than 9                                                      
  BEGIN                                                         
   SET @RENEW_SD_POINTS='Y'                                                    
  END               
--===============================================================              
              
--added by Pravesh on 11 sep08 Itrack 4719              
--If violations are entered with a negative – number in the Points assigned field               
--This must be a referral to the underwriter – Message – Violations with Negative values              
DECLARE @NEGATIVE_VIOLATION CHAR(1)              
 SET  @NEGATIVE_VIOLATION='N'              
IF EXISTS(              
  SELECT  CUSTOMER_ID                     
  FROM POL_WATERCRAFT_MVR_INFORMATION  WMVR               
     inner join VIW_DRIVER_VIOLATIONS VWVIOL ON VWVIOL.VIOLATION_ID=WMVR.VIOLATION_ID              
  WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND DRIVER_ID=@DRIVER_ID                                                                                 
  --AND (YEAR(GETDATE())- YEAR(OCCURENCE_DATE))<= 5               
  AND WMVR.IS_ACTIVE='Y'              
  --AND ISNULL(VWVIOL.WOLVERINE_VIOLATIONS,0)<0              
  AND ISNULL(POINTS_ASSIGNED,0)<0              
  )              
  SET  @NEGATIVE_VIOLATION='Y'               
--end here              
              
DECLARE @VEHICLE_ID CHAR              
DECLARE @INTCOUNT INT               
              
 SELECT @INTCOUNT=COUNT(APP_VEHICLE_PRIN_OCC_ID) FROM POL_OPERATOR_ASSIGNED_BOAT with(nolock)              
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID              
 IF(@INTCOUNT= 0)              
 BEGIN              
  SET @VEHICLE_ID=''              
 END              
 ELSE               
 BEGIN              
  SET @VEHICLE_ID=@INTCOUNT              
 END               
              
SELECT @PRIN_OCC_ID=isnull(convert(varchar(20),APP_VEHICLE_PRIN_OCC_ID),'')         
FROM POL_OPERATOR_ASSIGNED_BOAT with(nolock)              
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID AND DRIVER_ID=@DRIVER_ID               
       
-- Added by Charles on 18-Nov-09 for Itrack 6737  
-- DECLARE @REC_VEHICLE_PRIN_OCC_ID CHAR(2)      
-- SET @REC_VEHICLE_PRIN_OCC_ID = '-1'
-- 
--IF @POLICY_LOB = '1'    
--BEGIN             
-- IF EXISTS(SELECT CUSTOMER_ID FROM POL_OPERATOR_ASSIGNED_RECREATIONAL_VEHICLE WITH(NOLOCK)      
-- WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID and DRIVER_ID=@DRIVER_ID      
-- AND POL_REC_VEHICLE_PRIN_OCC_ID = 0)      
-- BEGIN      
--  SET @REC_VEHICLE_PRIN_OCC_ID=''      
-- END      
--END  
--- Added till here       
    
-- Added by Charles on 23-Nov-09 for Itrack 6743       
IF @POLICY_LOB = '1'    
BEGIN      
 IF NOT EXISTS(SELECT BOAT_ID FROM POL_WATERCRAFT_INFO WITH(NOLOCK) WHERE               
     POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND CUSTOMER_ID=@CUSTOMER_ID AND IS_ACTIVE = 'Y')      
  SET @DRIVER_COST_GAURAD_AUX = '-1'     
END    
--- Added till here     
             
--===============================================================                     
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
 @DRIVER_DRIV_TYPE as DRIVER_DRIV_TYPE,                           
 @VEHICLE_ID as VEHICLE_ID,                      
 @PRIN_OCC_ID as PRIN_OCC_ID,                                            
 @DEACTIVATEBOAT as DEACTIVATEBOAT ,              
 @DRIVER_COST_GAURAD_AUX as DRIVER_COST_GAURAD_AUX,              
 @VIOLATIONS as VIOLATIONS ,                                                         
 -- Rule only                                                   
 @SD_POINTS as SD_POINTS,                        
 @DRIVER_CITY as DRIVER_CITY ,              
 @WATER_MAJOR_VIOLATION AS WATER_MAJOR_VIOLATION,                                            
 @RENEW_WATER_MAJOR_VIOLATION as RENEW_WATER_MAJOR_VIOLATION ,              
 @RENEW_SD_POINTS AS RENEW_SD_POINTS ,              
 @NEGATIVE_VIOLATION as NEGATIVE_VIOLATION      
 --@REC_VEHICLE_PRIN_OCC_ID AS REC_VEHICLE_PRIN_OCC_ID -- Added by Charles on 18-Nov-09 for Itrack 6737                 
END               
        
              
              
              
            
GO

