IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPPARule_Drivers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPPARule_Drivers]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* --------------------------------------------------------------------------------------------------                                                                                                                                      
Proc Name                : Dbo.Proc_GetPPARule_Drivers           
Created by               : Ashwani                                                                                                                                      
Date                     : 13 Oct.,2005                                                                                                                                    
Purpose                  : To get the Driver's Information for Private Passenger Auto Rules                                                                                                                                    
Revison History          :                                                                                                                                      
Used In                  : Wolverine                                                                                                                                      
----------------------------------------------------------------------------------------------------                                                                                                                                      
Date     Review By          Comments                                                   
                                                
For Accident/MVR points plz follow   'GetMVRViolationPoints' sp.                                                
                                                
Proc_GetPPARule_Drivers 1692,39,1,1,''                                    
drop proc dbo.Proc_GetPPARule_Drivers                                                                                                                                
------   ------------       ------------------------------------------------------------------------*/                                                                                                                                      
CREATE  proc [dbo].[Proc_GetPPARule_Drivers]                                                                                                                                      
(                                                                                                                                      
@CUSTOMERID    int,                                                                                                                                      
@APPID    int,                                                                                                                                      
@APPVERSIONID   int,                                                                                                                            
@DRIVERID int     ,                                                                                                  
@DESC varchar(10)                                                                                                                              
)                                                                                                                                      
as                                                                                                                       
begin                                                                                                                       
--APP_DRIVER_DETAILS                                                                                                                         
 declare @DRIVER_DRINK_VIOLATION char                                                                                                                           
 declare @DRIVER_US_CITIZEN char            
 declare @DRIVER_STUD_DIST_OVER_HUNDRED char                
 declare @DRIVER_LIC_SUSPENDED char                  
declare @SAFE_DRIVER char          
 -- DOB                        
 declare @DATE_DRIVER_DOB datetime                                
 declare @INT_DIFFERENCE int                             
 declare @DRIVER_DOB char                                             
                                                 
 declare @IN_MILITARY int                                    
 declare @HAVE_CAR int                                                
 declare @STATIONED_IN_US_TERR int                                              
 declare @PARENTS_INSURANCE int                                                
 declare @DRIVER_GOOD_STUDENT char                                                
 declare @intDRIVER_LIC_STATE int                                                
 declare @WAIVER_WORK_LOSS_BENEFITS char                                                
                                               
 --    Driver Detail                                                                                      
 declare @DRIVER_NAME nvarchar(225)                                                                                                            
 declare @DRIVER_DRIV_LIC nvarchar(30) -- Lic. number                                                                     
 declare @DRIVER_CODE nvarchar(20)                                                                          
 declare @DRIVER_SEX nchar(12)                                                                                                 
 declare @DRIVER_FNAME nvarchar(75)                                                                                      
 declare @DRIVER_LNAME nvarchar(75)                                                                                                           
 declare @DRIVER_STATE nvarchar(5)                                                                                                          
 declare @DRIVER_ZIP varchar(11)                                                                                                          
 declare @DRIVER_LIC_STATE nvarchar(5)                                                                                                          
 declare @DRIVER_DRIV_TYPE nvarchar(15)  -- Driver Type                                                                                                        
 declare @DRIVER_VOLUNTEER_POLICE_FIRE char                                                                                                       
 declare @INTAPP_VEHICLE_PRIN_OCC_ID int                                                                                                         
 declare @APP_VEHICLE_PRIN_OCC_ID char                               
 declare @FULL_TIME_STUDENT char                               
 declare @SUPPORT_DOCUMENT char                                          
 DECLARE @intVIOLATIONS int                                                 
 declare @VIOLATIONS VARCHAR(5)                                                
 --MVR_POINTS YRS                                                
 declare @MVR_PNTS_YEARS int                                                 
 declare @ACC_MVR_POINTS_YRS int                             
 DECLARE @VIOLATIONREFERYEAR INT                          
 DECLARE @ACCIDENTREFERYEAR INT                          
 DECLARE @VIOLATIONNUMYEAR INT                          
 DECLARE @ACCIDENTCHARGES INT                          
 SET   @VIOLATIONREFERYEAR=3                          
 SET   @ACCIDENTREFERYEAR =3                          
 SET   @VIOLATIONNUMYEAR=2                             
 SET   @ACCIDENTCHARGES=1000                                       
 set @MVR_PNTS_YEARS = 5                                                   
 set @ACC_MVR_POINTS_YRS = 3                              
 DECLARE @FORM_F95 INT                      
                    
--ADDED BY PRAVEEN KUMAR(22-01-09):                    
                    
DECLARE   @IN_MILITARY_NEW CHAR                      
DECLARE   @STATIONED_IN_US_TERR_NEW CHAR                          
DECLARE   @HAVE_CAR_NEW  CHAR                                                                      
DECLARE   @DRIVERTURNEITTEEN NVARCHAR(5)              
SET   @DRIVERTURNEITTEEN='N'        
  
 --Added by Charles on 18-Nov-09 for Itrack 6725   
DECLARE @PARENTS_INSURANCE_NEW CHAR(2)  
DECLARE @DISTANT_STUDENT CHAR(2)   
  
SET @PARENTS_INSURANCE_NEW='-1'    
SET @DISTANT_STUDENT='-1'    
--Added till here  
  
IF EXISTS(SELECT CUSTOMER_ID FROM  APP_DRIVER_DETAILS WHERE                                                           
 CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND DRIVER_ID=@DRIVERID)                                                                                           
BEGIN                    
  SELECT  @DRIVER_US_CITIZEN=ISNULL(DRIVER_US_CITIZEN,''), @DRIVER_DRINK_VIOLATION=ISNULL(DRIVER_DRINK_VIOLATION,''),                                                 
  @DRIVER_STUD_DIST_OVER_HUNDRED=ISNULL(DRIVER_STUD_DIST_OVER_HUNDRED,''),@SAFE_DRIVER=ISNULL(SAFE_DRIVER,''),                                                                                       
  @DRIVER_DOB =DRIVER_DOB ,@DATE_DRIVER_DOB=DRIVER_DOB,@IN_MILITARY=ISNULL(IN_MILITARY,0),@HAVE_CAR=HAVE_CAR,                                                
  @STATIONED_IN_US_TERR =STATIONED_IN_US_TERR,@PARENTS_INSURANCE=ISNULL(PARENTS_INSURANCE,0),@DRIVER_GOOD_STUDENT=ISNULL(DRIVER_GOOD_STUDENT,''),                                                
  @INTDRIVER_LIC_STATE= DRIVER_LIC_STATE,@WAIVER_WORK_LOSS_BENEFITS=ISNULL(WAIVER_WORK_LOSS_BENEFITS,''),                           
  @DRIVER_NAME=(ISNULL(DRIVER_FNAME,'') + '  ' + ISNULL(DRIVER_MNAME,'') + '  ' + ISNULL(DRIVER_LNAME,'')),                                                                                                          
  @DRIVER_FNAME=ISNULL(DRIVER_FNAME,''),@DRIVER_LNAME=ISNULL(DRIVER_LNAME,''),@DRIVER_DRIV_LIC=ISNULL(DRIVER_DRIV_LIC,''),                                                                    
  @DRIVER_CODE=ISNULL(DRIVER_CODE,''),@DRIVER_SEX=ISNULL(DRIVER_SEX,'') ,@DRIVER_STATE=ISNULL(DRIVER_STATE,''),                                                                                    
  @DRIVER_ZIP=ISNULL(DRIVER_ZIP,''),@DRIVER_SEX=ISNULL(DRIVER_SEX,''),@DRIVER_LIC_STATE=ISNULL(DRIVER_LIC_STATE,''),                                                                    
  @DRIVER_DRIV_TYPE=ISNULL(DRIVER_DRIV_TYPE,''),@DRIVER_VOLUNTEER_POLICE_FIRE=ISNULL(DRIVER_VOLUNTEER_POLICE_FIRE,''),                                    
  @INTAPP_VEHICLE_PRIN_OCC_ID=ISNULL(APP_VEHICLE_PRIN_OCC_ID,-1)  , @INTVIOLATIONS=VIOLATIONS ,                                        
  @VIOLATIONS=ISNULL(VIOLATIONS,''), @FULL_TIME_STUDENT =ISNULL(FULL_TIME_STUDENT,''),                              
  @SUPPORT_DOCUMENT =ISNULL(SUPPORT_DOCUMENT,'')   ,@FORM_F95 = ISNULL(FORM_F95,0)                                                                                    
FROM  APP_DRIVER_DETAILS                                                                 
WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND DRIVER_ID=@DRIVERID                                                                               
END                              
ELSE                            
BEGIN                             
 SET @DRIVER_LIC_STATE=''                            
END                                       
                                                 
-- If State is Indiana Driver/Household Member  Date of Birth Take effective date of policy minus the date of birth                                                 
-- is under 21 Then look at Field  -Parents Insurance If  - Insured Elsewhere  Refer to underwriters                                                 
 DECLARE @DATEAPP_EFFECTIVE_DATE DATETIME                                                
 DECLARE  @STATE_ID INT                                                     
                                                          
 select @STATE_ID=STATE_ID,@DATEAPP_EFFECTIVE_DATE=APP_EFFECTIVE_DATE from  APP_LIST                                                                            
 where CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID and IS_ACTIVE='Y'                                                             
  -- D_O_B                   
 set @INT_DIFFERENCE = datediff(day,@DATE_DRIVER_DOB,@DATEAPP_EFFECTIVE_DATE)            
set @INT_DIFFERENCE = @INT_DIFFERENCE/365.2425                                           
                
IF(@STATE_ID=14 AND @PARENTS_INSURANCE=11934)                                                                  
 BEGIN                                         
  IF(@DATE_DRIVER_DOB IS NULL)                                                                                                      
   BEGIN                                                             
   SET @DRIVER_DOB=''                                                                                              
   END                                                                                
  ELSE IF(@INT_DIFFERENCE<21)                                                                                                  
   BEGIN                                                                                             
   SET @DRIVER_DOB='Y'                                                                                                      
   END                                                      
 ELSE                                                                                                      
  BEGIN                                                                                                      
  SET @DRIVER_DOB='N'                                                                             
  END                                                                     
END                                                                   
ELSE                                                                  
BEGIN                                                                   
SET @DRIVER_DOB='N'                                                                  
END          
  
 --Added by Charles on 18-Nov-09 for Itrack 6725   
IF (@INT_DIFFERENCE<25 AND @PARENTS_INSURANCE = 0)  
BEGIN  
 SET @PARENTS_INSURANCE_NEW=''    
END  
--Added till here  
------------------------------------------------------                                                
-- Commented by Manoj Rathore (Change)                            
                                             
/* MVR Tab ,Violation                                                 
 For each driver in the lister - look at the list of violations and should there be any                                                
 one violation where the number of points is 8                                                 
 Then take the effective date of the policy minus the conviction date and if less                                                 
 than 5 years , Refer to underwriters */                              
/*                            
 DECLARE @VIOLATION_POINT CHAR                                            
 SET  @VIOLATION_POINT='N'                              
                               
 IF EXISTS( SELECT MVR_POINTS FROM APP_MVR_INFORMATION                                          
 INNER JOIN  MNT_VIOLATIONS  ON APP_MVR_INFORMATION.VIOLATION_ID = MNT_VIOLATIONS.VIOLATION_ID                                                                          
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DRIVER_ID=@DRIVERID                                                 
 AND MVR_POINTS=8 AND ((ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(MVR_DATE),0))<=@MVR_PNTS_YEARS AND                                  
 (ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(MVR_DATE),0))>=0)                                                          
 AND APP_MVR_INFORMATION.IS_ACTIVE='Y')                                                
 BEGIN                                                 
 SET @VIOLATION_POINT='Y'                                       
 END                             
*/                           
                          
--added by Pravesh on 11 sep 2008 Itrack 4719             
--If violations are entered with a negative – number in the Points assigned field                           
--This must be a referral to the underwriter – Message – Violations with Negative values                          
 DECLARE @NEGATIVE_VIOLATION_POINT char                                  
  SET  @NEGATIVE_VIOLATION_POINT='N'                             
 IF EXISTS(                          
  SELECT CUSTOMER_ID                           
  FROM APP_MVR_INFORMATION                             
  INNER JOIN  VIW_DRIVER_VIOLATIONS  ON APP_MVR_INFORMATION.VIOLATION_ID = VIW_DRIVER_VIOLATIONS.VIOLATION_ID                              
  WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DRIVER_ID=@DRIVERID                            
  AND ISNULL(WOLVERINE_VIOLATIONS,0) < 0                             
  )                          
 SET  @NEGATIVE_VIOLATION_POINT='Y'                             
                          
-- added by Pravesh end here                             
-- If TOTAL violation Major(with in 5 year) and Minor(with in 3 years) greater than 5 refer to underwriter                            
                                                                          
  DECLARE @VIOLATION_POINT char                                  
  SET  @VIOLATION_POINT='N'                             
-- Check Major Violation With in 5 years                            
 DECLARE @intMAJOR_VIOLATION INT                             
 SELECT @intMAJOR_VIOLATION=ISNULL(SUM(ISNULL(POINTS_ASSIGNED,0))+ SUM(ISNULL(ADJUST_VIOLATION_POINTS,0)),0)                             
 FROM APP_MVR_INFORMATION                             
INNER JOIN  VIW_DRIVER_VIOLATIONS  ON APP_MVR_INFORMATION.VIOLATION_TYPE = VIW_DRIVER_VIOLATIONS.VIOLATION_ID                              
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DRIVER_ID=@DRIVERID                            
-- AND (VIOLATION_TYPE =270 OR VIOLATION_TYPE=1 )                            
AND VIOLATION_CODE IN('10000','40000','SUSPN')                           
 --AND ((ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(MVR_DATE),0)))<=5                             
 AND DATEDIFF(DAY,MVR_DATE,@DATEAPP_EFFECTIVE_DATE)<= 5*365.5                           
 AND DATEDIFF(DAY,MVR_DATE,@DATEAPP_EFFECTIVE_DATE)>= 0 AND APP_MVR_INFORMATION.IS_ACTIVE='Y'                          
                            
-- Check Minor Violation With in 3 years                            
 DECLARE @MINOR_VIOLATION INT                             
 SELECT @MINOR_VIOLATION=ISNULL(SUM(ISNULL(POINTS_ASSIGNED,0))+ SUM(ISNULL(ADJUST_VIOLATION_POINTS,0)),0)                             
 FROM APP_MVR_INFORMATION                           
INNER JOIN  VIW_DRIVER_VIOLATIONS  ON APP_MVR_INFORMATION.VIOLATION_TYPE = VIW_DRIVER_VIOLATIONS.VIOLATION_ID                              
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DRIVER_ID=@DRIVERID                            
 --AND (VIOLATION_TYPE !=270 AND VIOLATION_TYPE!=1 )                            
AND VIOLATION_CODE NOT IN('10000','40000','SUSPN')                           
--AND ((ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(MVR_DATE),0)))<=3                             
 AND DATEDIFF(DAY,MVR_DATE,@DATEAPP_EFFECTIVE_DATE)<= 3*365.25                           
 AND DATEDIFF(DAY,MVR_DATE,@DATEAPP_EFFECTIVE_DATE)>= 0 AND APP_MVR_INFORMATION.IS_ACTIVE='Y'                          
 -- Sum of Major and Minor violation greater than 5 and less than 0 refer to underwriter                            
 IF((@intMAJOR_VIOLATION + @MINOR_VIOLATION) > 5 or (@intMAJOR_VIOLATION + @MINOR_VIOLATION)< 0)                           
 BEGIN                                   
 SET @VIOLATION_POINT='Y'                                  
 END                             
DECLARE @ACCIDENT_POINTS INT                          
-- ACCIDENT POINTS ACCUMULATION                          
CREATE TABLE #DRIVER_VIOLATION_ACCIDENT                            
(                            
 [SUM_MVR_POINTS]  INT,                                                                        
 [ACCIDENT_POINTS]  INT,                                  
 [COUNT_ACCIDENTS]  INT,                            
 [MVR_POINTS]  INT                         
                                 
)                             
INSERT INTO #DRIVER_VIOLATION_ACCIDENT exec GetMVRViolationPoints @CUSTOMERID,@APPID,@APPVERSIONID ,@DRIVERID,@ACCIDENTREFERYEAR,@VIOLATIONNUMYEAR,@VIOLATIONREFERYEAR,@ACCIDENTCHARGES                                       
SELECT @ACCIDENT_POINTS = ACCIDENT_POINTS FROM #DRIVER_VIOLATION_ACCIDENT                          
IF(@ACCIDENT_POINTS IS NULL)                            
 SET @ACCIDENT_POINTS =0                           
DROP TABLE  #DRIVER_VIOLATION_ACCIDENT                           
                          
IF((@intMAJOR_VIOLATION + @MINOR_VIOLATION + @ACCIDENT_POINTS) > 5 or (@intMAJOR_VIOLATION + @MINOR_VIOLATION + @ACCIDENT_POINTS)< 0)                  
 BEGIN                                   
 SET @VIOLATION_POINT='Y'                                  
 END                            
--=================================================================================         
  
--Added till here                                                                       
IF(@INT_DIFFERENCE<25)                                                                    
BEGIN               
  IF(@DRIVER_STUD_DIST_OVER_HUNDRED='1')                                                                                                                          
  BEGIN                                               
  SET @DRIVER_STUD_DIST_OVER_HUNDRED='Y'                                                                     
  END                                                  
  ELSE IF(@DRIVER_STUD_DIST_OVER_HUNDRED='0')                                                               
  BEGIN                                                                                
  SET @DRIVER_STUD_DIST_OVER_HUNDRED='N'                                                                                                                         
  END    
  ELSE --Added by Charles on 18-Nov-09 for Itrack 6725    
  SET @DISTANT_STUDENT = ''                                   
END                                                                             
ELSE                              
BEGIN                                    
SET @DRIVER_STUD_DIST_OVER_HUNDRED ='N'                                                                      
 /* IF(@DRIVER_STUD_DIST_OVER_HUNDRED='1')                             
  BEGIN                                                
  SET @DRIVER_STUD_DIST_OVER_HUNDRED='Y'                                                                                                    
  END                                                                                                      
  ELSE IF(@DRIVER_STUD_DIST_OVER_HUNDRED='0')                                                                                                         
  BEGIN                             
  SET @DRIVER_STUD_DIST_OVER_HUNDRED='N'                                               
  END                                
  ELSE IF(@DRIVER_STUD_DIST_OVER_HUNDRED='')                                                                       
  BEGIN                                                                             
  SET @DRIVER_STUD_DIST_OVER_HUNDRED='N'                                                                            
  END */                                  
                                                                          
END                                                                                         
                                                     
                                                                                                                    
--                                                                                                                 
IF(@DRIVER_DRINK_VIOLATION='1')                                                                                                    
BEGIN                                                                                                               
SET @DRIVER_DRINK_VIOLATION='Y'                                   
END                                                                                                                          
ELSE IF(@DRIVER_DRINK_VIOLATION='0')                                                                                                                          
BEGIN                  
SET @DRIVER_DRINK_VIOLATION='N'                             
END                                     
--                              
IF(@DRIVER_US_CITIZEN='0')                                                                   
BEGIN                                                           
SET @DRIVER_US_CITIZEN='Y'                                                                                                                          
END                                                          
ELSE IF(@DRIVER_US_CITIZEN='1')                                               
BEGIN                                                             
SET @DRIVER_US_CITIZEN='N'                                                                               
END                                                                                                                 
--                                                                                                   
                                                     
IF(@SAFE_DRIVER='1')                                                                                               
BEGIN                                                                                                                           
SET @SAFE_DRIVER='Y'                                                                                                  
END                                          
ELSE IF(@SAFE_DRIVER='0')                                              
BEGIN                                                                   
SET @SAFE_DRIVER='N'                               
END                                                     
                                                                
                                                                      
                                            
------------------------------------------------------------------------------------                                                    
 DECLARE @VEHICLE_ID VARCHAR(20)                                                           
 -- IF DRIVER TYPE IS LICENSED & VALUE="11603"                                                    
 IF(@DRIVER_DRIV_TYPE=11603)                                       
BEGIN                                                     
  IF EXISTS (SELECT     APP_DRIVER_DETAILS.VEHICLE_ID                               
      FROM  APP_VEHICLES INNER JOIN                           
                       APP_DRIVER_DETAILS ON APP_VEHICLES.CUSTOMER_ID = APP_DRIVER_DETAILS.CUSTOMER_ID AND                                                     
                       APP_VEHICLES.APP_ID = APP_DRIVER_DETAILS.APP_ID AND                                                     
                       APP_VEHICLES.APP_VERSION_ID = APP_DRIVER_DETAILS.APP_VERSION_ID AND                                                     
                       APP_VEHICLES.VEHICLE_ID = APP_DRIVER_DETAILS.VEHICLE_ID                                                    
 WHERE     (APP_VEHICLES.IS_ACTIVE = 'Y')                                                     
  AND (APP_DRIVER_DETAILS.CUSTOMER_ID = @CUSTOMERID) AND (APP_DRIVER_DETAILS.APP_ID = @APPID) AND                                                     
                       (APP_DRIVER_DETAILS.APP_VERSION_ID = @APPVERSIONID))                                                    
  BEGIN                                                     
  SELECT     @VEHICLE_ID=ISNULL(CONVERT(VARCHAR(20),APP_DRIVER_DETAILS.VEHICLE_ID),'')                                                    
  FROM         APP_VEHICLES INNER JOIN                                       
                       APP_DRIVER_DETAILS ON APP_VEHICLES.CUSTOMER_ID = APP_DRIVER_DETAILS.CUSTOMER_ID AND                                                    
                       APP_VEHICLES.APP_ID = APP_DRIVER_DETAILS.APP_ID AND                       
                       APP_VEHICLES.APP_VERSION_ID = APP_DRIVER_DETAILS.APP_VERSION_ID AND                                                     
                       APP_VEHICLES.VEHICLE_ID = APP_DRIVER_DETAILS.VEHICLE_ID                               
  WHERE (APP_VEHICLES.IS_ACTIVE = 'Y')                           
        AND (APP_DRIVER_DETAILS.CUSTOMER_ID = @CUSTOMERID) AND (APP_DRIVER_DETAILS.APP_ID = @APPID) AND                                                     
               (APP_DRIVER_DETAILS.APP_VERSION_ID = @APPVERSIONID)                                                    
  END                                      
  ELSE                                                    
  BEGIN                                             
  SET @VEHICLE_ID=''                                                    
  END                                                  
END                                                     
ELSE                                                  
BEGIN                               
 SET @VEHICLE_ID='1'                                                    
                                                    
END                                         
                                                                                                        
-----                                                                    
                                                                                                        
-- declare @DATEAPP_EFFECTIVE_DATE datetime                                                                                                        
 DECLARE @APP_EFFECTIVE_DATE CHAR                                                    
 DECLARE @DATEDATE_LICENSED DATETIME                                                    
 DECLARE @DATE_LICENSED CHAR                                                                    
-- DECLARE @DATEDATECONT_DRIVER_LICENSE INT                                                     
 DECLARE @CONT_DRIVER_LICENSE  CHAR -- 2.B.6 2.B.7.B                             
 --SELECT @DATEAPP_EFFECTIVE_DATE=APP_EFFECTIVE_DATE                                                                                               
 --FROM APP_LIST                                                                              
-- WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                             
 SELECT @DATEDATE_LICENSED=DATE_LICENSED                                                      
 FROM APP_DRIVER_DETAILS                                                    
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND DRIVER_ID=@DRIVERID                                        
                                                         
                                                               
 IF(@DATEAPP_EFFECTIVE_DATE IS NOT NULL AND @DATEDATE_LICENSED IS NOT NULL)                                                                    
 BEGIN                                                                                                         
--  SET @DATEDATECONT_DRIVER_LICENSE = DATEDIFF(MM,@DATEDATE_LICENSED,@DATEAPP_EFFECTIVE_DATE)                                                                                      
  IF (SELECT DATEADD(MONTH, 12, CONVERT(DATETIME,@DATEDATE_LICENSED) )) >((SELECT CONVERT(DATETIME,@DATEAPP_EFFECTIVE_DATE)))                           
  BEGIN                                                             
  SET @CONT_DRIVER_LICENSE='Y'                                                              
  END                                                               
  ELSE                                                              
  BEGIN                         
  SET @CONT_DRIVER_LICENSE='N'                                                           
  END                                                                
                                                    
 END                                                                                                    
                                                                              
 /*if(@DATEDATECONT_DRIVER_LICENSE <12)                                                   
 begin                                                               
  set @CONT_DRIVER_LICENSE='Y'                                                            
 end                                                                                                        
 else                                
 begin                                                                                                         
  set @CONT_DRIVER_LICENSE='N'                                                                                
 end  */                                                                                                       
--=============================                                                                                                        
IF(@DATEAPP_EFFECTIVE_DATE IS NULL)                                                                     
BEGIN                        
SET @APP_EFFECTIVE_DATE=''                                     
END                                                                                              
ELSE                                                     
BEGIN                                                                                                         
SET @APP_EFFECTIVE_DATE='N'                                                                                                   
END                                                                                                         
--============================                                                                
IF(@DATEDATE_LICENSED IS NULL)                                                                                                        
BEGIN                                      
SET @DATE_LICENSED=''                                                                 
END                                            
ELSE                                                          
BEGIN                                                                                                         
SET @DATE_LICENSED='N'                         
END                                                                         
                                                            
--============================                            
DECLARE  @INTCOUNTISACTIVE INT                                             
DECLARE  @DEACTIVATEVEHICLE CHAR                             
                            
SELECT @INTCOUNTISACTIVE=COUNT(DRIVER_ID) FROM APP_DRIVER_DETAILS D      
WHERE DRIVER_ID = @DRIVERID AND D.CUSTOMER_ID=@CUSTOMERID AND  D.APP_ID=@APPID  AND  D.APP_VERSION_ID=@APPVERSIONID                                                       
AND D.VEHICLE_ID IN ( SELECT V.VEHICLE_ID FROM APP_VEHICLES V                                      
        WHERE V.CUSTOMER_ID=@CUSTOMERID AND  V.APP_ID=@APPID  AND  V.APP_VERSION_ID=@APPVERSIONID AND V.IS_ACTIVE='N')                                             
--===========================                                 
IF (@INTCOUNTISACTIVE>0)                                                                                   
BEGIN                                                                          
SET @DEACTIVATEVEHICLE='Y'                                                                              
END                                       
ELSE                                                                                       
BEGIN                                                                             
SET @DEACTIVATEVEHICLE='N'                                                  
END                                        
                                     
--============================          
/*  Accumulation of more than 5 eligibility points during the preceding 3 years.                                        
    Any violations and accidents (both prior loss and claims)  within the last 3 years (Entered through MVR tab) ,                                                
    Based on the effective date of the policy minus the conviction date                                                 
    If the number (sum of accident and violations)   is  greater then 5  Refer to Underwriter */                             
 --DECLARE @INTSD_PONITS INT                                                 
 --DECLARE @INTACCIDENT_POINTS INT                                    
 --DECLARE @SUMOFPOINTS INT                                                 
 DECLARE @SD_POINTS CHAR                             
 SET @SD_POINTS ='N'                           
   /*Commented by Manoj Rathore on 18 oct 2007(New implementation has been done)                            
 SELECT @INTACCIDENT_POINTS=COUNT(CUSTOMER_ID) FROM FETCH_ACCIDENT                                                         
 WHERE CUSTOMER_ID = @CUSTOMERID AND                                                                  
 (POLICY_ID IS NULL) AND (POLICY_VERSION_ID IS NULL) AND LOB=2 AND                                                    
 ((ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(OCCURENCE_DATE,0)),0))<=@ACC_MVR_POINTS_YRS AND                                          
 (ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(OCCURENCE_DATE,0)),0))>=0) AND DRIVER_ID=@DRIVERID                             
 IF (@INTACCIDENT_POINTS>0)                                                  
 SET @INTACCIDENT_POINTS = (ISNULL(@INTACCIDENT_POINTS,0) * 4 ) - 1                             
 SELECT  @INTSD_PONITS = SUM(ISNULL(MVR_POINTS,0))                                                
 FROM  APP_MVR_INFORMATION A  JOIN  MNT_VIOLATIONS M                                                  
 ON   A.VIOLATION_ID = M.VIOLATION_ID                                                 
 WHERE  A.CUSTOMER_ID = @CUSTOMERID AND  A.APP_ID = @APPID AND  A.APP_VERSION_ID = @APPVERSIONID AND                                                                  
 A.DRIVER_ID = @DRIVERID AND    ISNULL(M.MVR_POINTS,0)>0 AND                                 
 ((ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(MVR_DATE),0))<=3 AND                                                                
 (ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(MVR_DATE),0))>=0)                                                                
 AND A.IS_ACTIVE='Y'                             
 SET @SUMOFPOINTS = @INTSD_PONITS+@INTACCIDENT_POINTS                            
 IF(@SUMOFPOINTS > 5)                  
  BEGIN                                                                                       
  SET @SD_POINTS='Y'                                                  
  END                                                                                       
 ELSE                                                                    
  BEGIN                                                                                       
  SET @SD_POINTS='N'                                                                                    
  END                     
    */                                                                              
                                                                                  
                                                                                  
--==========================================================                                                                               
 declare @INTVEHICLE_ID int                                                  
 declare @ISDRVASSIGNEDVEH char                                                                               
                                                             
 select @INTVEHICLE_ID=count(VEHICLE_ID )                                                     
 from  APP_DRIVER_DETAILS                   
 where CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID and DRIVER_ID=@DRIVERID                                   
 and VEHICLE_ID is null                                                     
                                      
 IF(@INTVEHICLE_ID>0 )                                                                              
 BEGIN                                                           
 SET @ISDRVASSIGNEDVEH='Y'                                                                              
 END                                                                               
 ELSE                                                        
 BEGIN                                   
 SET @ISDRVASSIGNEDVEH='N'                                                                              
 END                                                             
--            
/*if(@DRIVER_LIC_STATE=0)                                                      
begin                                    
 set @DRIVER_LIC_STATE=''                                                            
end              */                          
                                              
IF(@DRIVER_LIC_STATE='0')                                                            
   BEGIN                                                             
   SET @DRIVER_LIC_STATE=''                                                            
   END                             
/*                                              
ELSE IF (@STATE_ID='22' AND @DRIVER_LIC_STATE<>'22')      removed by pravesh on 9sep08 Itrack 4569                          
   BEGIN                                                            
   SET @DRIVER_LIC_STATE='Y'                                                
   END  */                             
ELSE                                                
  BEGIN                                                
  SET  @DRIVER_LIC_STATE='N'                                                            
  END                              
--                                            
if(@DRIVER_DRINK_VIOLATION ='Y' and @DRIVER_DRIV_TYPE= '3477')                                                
 set @DRIVER_DRINK_VIOLATION='N'                                      
                                                
if(@DRIVER_DRIV_TYPE=0)                                                            
begin                                    
 set @DRIVER_DRIV_TYPE=''                                                            
end                          
/* commented by pravesh on 17 sep                           
--------------RAGHAV-----               
IF (@DRIVER_DRIV_TYPE = '3477' AND @FORM_F95 = 10963 ) -- Excluded   \                                                
 BEGIN                          
set @DRIVER_DRIV_TYPE='Y'                                                                                  
END                          
                          
ELSE IF (@DRIVER_DRIV_TYPE = '3477' AND @FORM_F95 = 10964 ) -- Excluded   \                                                 
BEGIN                          
set @DRIVER_DRIV_TYPE='N'                                     
END                          
--                                       
*/                          
                                        
if(@VEHICLE_ID=0)                               
begin                                                         
 set @VEHICLE_ID=''                                                          
end                                                           
-------------------------------------------------------------------------                                                      
if(@DRIVER_SEX='M')                      
begin                                                       
 set @DRIVER_SEX='Male'                                                      
end                                                      
else if(@DRIVER_SEX='F')                                                    
begin                                                       
 set @DRIVER_SEX='Female'                                                   
end          
-------------------------------------------------------------------------                                               
-------------------------------------------------------------------------------------                                                    
IF(@VEHICLE_ID='')                                           
BEGIN                                                                                      
 IF(@INTAPP_VEHICLE_PRIN_OCC_ID=11399 OR @INTAPP_VEHICLE_PRIN_OCC_ID=11398)                                                               
 BEGIN                                                            
 SET @APP_VEHICLE_PRIN_OCC_ID='N'                                                                
 END                                                                 
 ELSE                                                                
 BEGIN                                                                 
 SET @APP_VEHICLE_PRIN_OCC_ID=''                                      
 END                                   
 END                                                   
 ELSE                                                  
 BEGIN                                                   
 SET @APP_VEHICLE_PRIN_OCC_ID='N'                                                  
                                          
END                                                  
------------------------------------------------------------------------------------------                                   
-- Drivers/Household Members Tab If Yes to Waiver of Work Loss Coverage Then look at Field                                                 
-- Signed Waiver of Benefits Form If no refer to underwriters                                                 
                                 
DECLARE @WAIVER_WORK_LOSS CHAR                                                 
SET @WAIVER_WORK_LOSS='N'                                                
                            
IF EXISTS(SELECT CUSTOMER_ID FROM APP_DRIVER_DETAILS                                       
WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND DRIVER_ID=@DRIVERID                                       
AND SIGNED_WAIVER_BENEFITS_FORM='0' AND WAIVER_WORK_LOSS_BENEFITS='1')                                                
BEGIN                                                 
SET @WAIVER_WORK_LOSS='Y'                                
END                                                
------------------------------------------------------------------------------------------                                                     
-- Michigan Youthful driver                                                
-- We will apply class 5C to youthful drivers (under the age of 25) who are on their parent's policy and also have                                                
-- joined the Military Services.  He or she must be stationed in the "Policy Territory" (United States, its territories                                                
-- and possessions, Puerto Rico and Canada) and not have a car with them on base.  If stationed outside of the "Policy                                                 
-- Territory," they may be removed from the policy."                                                
                                                
-- MI ,age < 25 AND Country = USA or Canada AND Are you in the Military?* = yes AND Are you a college student?* = yes                                                 
-- and Do you have the car with you?* = No True then ok else Refer                                                
                              
DECLARE @YOUTH_DRIVER CHAR                               
SET @YOUTH_DRIVER = 'N'                           
                            
IF(@STATE_ID=22 AND @INT_DIFFERENCE<25 AND  @IN_MILITARY=10964 AND (@DRIVER_STUD_DIST_OVER_HUNDRED='0' OR @DRIVER_STUD_DIST_OVER_HUNDRED='N')                                               
AND @HAVE_CAR=10964 AND @STATIONED_IN_US_TERR<>10964)                                                
BEGIN                                                 
SET @YOUTH_DRIVER='Y'                                                
END                    
--------------------------------------------------------------------------------                                                         
-- If driver is assigned "with points" and No MVR information is provided OR driver is assigned with "no points" and                                                 
-- MVR info is provided Refere to underwriter                          
                                                
 declare @DRV_WITHPOINTS char                                                
 declare @DRV_WITHOUTPOINTS char                                                
 --set @DRV_WITHPOINTS='N'                                                
 SET @DRV_WITHOUTPOINTS ='N'                             
 SET @DRV_WITHPOINTS  ='N'                            
                                                
   /*Itrack No. 2933 Commented By Manoj rathore                            
                                             
 IF EXISTS (SELECT MVR_POINTS FROM APP_MVR_INFORMATION                                                  
 INNER JOIN  MNT_VIOLATIONS  ON APP_MVR_INFORMATION.VIOLATION_ID = MNT_VIOLATIONS.VIOLATION_ID                                                                          
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND DRIVER_ID=@DRIVERID                                                
 AND APP_MVR_INFORMATION.IS_ACTIVE='Y' AND MVR_POINTS>0)                                                
                                                 
 BEGIN                                          
   -- MVR PTS EXISTS & ANY DRIVER WITH NO POINTS- REFER                                                
  IF EXISTS (SELECT P.APP_VEHICLE_PRIN_OCC_ID FROM APP_DRIVER_DETAILS D                                                
  INNER JOIN APP_DRIVER_ASSIGNED_VEHICLE P                                                 
  ON D.CUSTOMER_ID=P.CUSTOMER_ID AND D.APP_ID=P.APP_ID AND D.APP_VERSION_ID=P.APP_VERSION_ID AND D.DRIVER_ID=P.DRIVER_ID                                                
  WHERE D.CUSTOMER_ID=@CUSTOMERID AND D.APP_ID=@APPID AND D.APP_VERSION_ID=@APPVERSIONID AND D.DRIVER_ID=@DRIVERID                                                 
  AND  P.APP_VEHICLE_PRIN_OCC_ID IN (11926,11399,11928,11930))                                             
  BEGIN                                                 
  SET @DRV_WITHOUTPOINTS='Y'                                                
  END                                                
  ELSE                                  
  BEGIN                                                 
  SET @DRV_WITHOUTPOINTS='N'                                                
  END                                  
                             
 END                                                 
 ELSE                                                
 BEGIN                                         
          -- MVR PTS NOT EXISTS & ANY DRIVER WITH POINTS - REFER                                      
  IF EXISTS (SELECT P.APP_VEHICLE_PRIN_OCC_ID FROM APP_DRIVER_DETAILS D              
  INNER JOIN APP_DRIVER_ASSIGNED_VEHICLE P                                                 
  ON D.CUSTOMER_ID=P.CUSTOMER_ID AND D.APP_ID=P.APP_ID AND D.APP_VERSION_ID=P.APP_VERSION_ID AND D.DRIVER_ID=P.DRIVER_ID                                                
  WHERE  D.CUSTOMER_ID=@CUSTOMERID AND D.APP_ID=@APPID AND D.APP_VERSION_ID=@APPVERSIONID AND D.DRIVER_ID=@DRIVERID                                                 
  AND  P.APP_VEHICLE_PRIN_OCC_ID IN (11929,11925,11398,11927) AND  D.MVR_ORDERED=0)                                   
                                         
  BEGIN                                                 
  SET @DRV_WITHPOINTS='Y'                                                
  END                                    
  ELSE                                   
  BEGIN                                                 
  SET @DRV_WITHPOINTS='N'                                                
  END                                                
                                                
 END                             
                             
 Itrack No.2933 end Comment*/                        
-- MVR information of 2 points & Put "NO" to the field "MVR Ordered" under Driver information, refer to underwriter                                          
                                       
DECLARE @APP_MVR_ID INT                                         
DECLARE @MVR_ORDERED INT                                         
DECLARE @DRIVER_MVR_ORDERED CHAR                    
DECLARE @MVR_STATUS CHAR(1) -- ITRACK 5529                    
SELECT @APP_MVR_ID=ISNULL(APP_MVR_ID,'0') FROM APP_MVR_INFORMATION                                     
 WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID and DRIVER_ID=@DRIVERID and IS_ACTIVE='Y'                                        
SELECT @MVR_ORDERED=ISNULL(MVR_ORDERED,'0'),@MVR_STATUS=MVR_STATUS FROM APP_DRIVER_DETAILS                                         
WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID and DRIVER_ID=@DRIVERID and IS_ACTIVE='Y'                 
                
-- ITRACK 5529                    
IF(@MVR_ORDERED=10964 OR @MVR_ORDERED IS NULL OR @MVR_ORDERED=0)                    
BEGIN                    
 SET @DRIVER_MVR_ORDERED='Y'                      
END                       
ELSE IF (@MVR_ORDERED=10963 AND @MVR_STATUS IN ('C','V'))                    
BEGIN                    
 SET @DRIVER_MVR_ORDERED='N'                      
END --END OF ITRACK 5529 CHANGES                    
ELSE IF((@APP_MVR_ID IS NULL AND @MVR_ORDERED=10963) or (@APP_MVR_ID IS NOT NULL AND @MVR_ORDERED=10964))                                        
 BEGIN                                        
 SET @DRIVER_MVR_ORDERED='Y'                                        
 END                                  
 ELSE                                           
 BEGIN                                        
 SET @DRIVER_MVR_ORDERED='N'                                        
 END                                        
                           
--------------------------------------------------------------------------------------------------------------------                                                
-- Driver screen Volunteer fireman or policeman* if yes then refer to underwriter                                                
if(@DRIVER_VOLUNTEER_POLICE_FIRE='1')                                                
begin                                                 
 set @DRIVER_VOLUNTEER_POLICE_FIRE='Y'                                                
end                          
                      
--Added By praveen Kumar(14-01-2009):Itrack :4513                      
                      
else if(@DRIVER_VOLUNTEER_POLICE_FIRE='' and @DRIVER_DRIV_TYPE='3477')                                            
begin                                                 
 set @DRIVER_VOLUNTEER_POLICE_FIRE='N'                                                
end                        
                     
---End Praveen Kumar                                             
--------------------------------------------------------------------------------------------------------------------                                                
-- Check the list If we have any drivers under 25 If Yes The look at Field Parents Insurance                                               
-- If Insured Elsewhere - then refer to underwriters                                                
                                                
declare @DRIVER_PARENT_ELSEWHERE char                                  
set @DRIVER_PARENT_ELSEWHERE='N'                                                
                                                
if(@PARENTS_INSURANCE=11934 and @INT_DIFFERENCE<21 and @STATE_ID=14 )                           
begin                                               
 set @DRIVER_PARENT_ELSEWHERE='Y'                                                
 if (@DRIVER_DOB='Y')                          
 set @DRIVER_DOB='N'                          
end                          
                                                
--------------------------------------------------------------------------------------------------------------------                                                
--------------------------------------------------------------------------------------------------------------------                                                  
DECLARE @DRIVER_SUPPORTING_DOCUMENT char                               
-- 1- If driver is under 25 years of age There is a field for Good Student If Yes Then look at field - Supporting Document*                                                
-- If No - then refer to underwriters                                 
 IF( @INT_DIFFERENCE < 25 AND @DRIVER_GOOD_STUDENT='1' AND @FULL_TIME_STUDENT ='1' AND @SUPPORT_DOCUMENT='0') 
 BEGIN                                                 
 SET @DRIVER_SUPPORTING_DOCUMENT='Y'                                         
 END                               
ELSE                              
 BEGIN                                                 
 SET @DRIVER_SUPPORTING_DOCUMENT='N'                                      
 END                               
                                
-- 2- If driver is under 25 years of age There is a field for Good Student If No - then refer to underwriters                                 
                                
                                 
IF(@INT_DIFFERENCE < 25 AND @DRIVER_GOOD_STUDENT='0')                                                  
  BEGIN                                 
  SET @DRIVER_GOOD_STUDENT='Y'                            
  END                               
ELSE                               
  BEGIN                                                   
  SET @DRIVER_GOOD_STUDENT='N'                                       
  END                              
--------------------------------------------------------------------------------------------------------------------                                                
/*                    
If college student is YES and field "Parents Insurance" is opted for "Insured Elsewhere" then refer to underwriter                                                
If college student is YES and If "yes" to "Do you keep the car with you?", then look at the Licensed State on the Driver/Household Member tab                                                 
                                                
If the Licensed State is not equal to the State on the Application/Policy details - State Field , then Refer to Underwriters                                                 
If equal then look at the Vehicle Info tab for the car assigned on the respective  Driver/Member Tab                                                 
If Registered State is not  equal to the  State on the Application/Policy details - State Field ,Refer to Underwriters                                                 
                                                
If Equal - do nothing                                                
Then make then the principal driver on the vehicle they drive - Assigned Vehicle Field                                                 
Class based on age - Date of Birth Field and Gender                                                 
Territory based on policy address  */                                                
                                                
                                                
declare @REGISTERED_STATE varchar(5)                                  
                                 
select @REGISTERED_STATE=REGISTERED_STATE                             
from APP_VEHICLES                                                 
where CUSTOMER_ID=@CUSTOMERID  and  APP_ID=@APPID  and  APP_VERSION_ID=@APPVERSIONID                                                 
 and VEHICLE_ID in (select VEHICLE_ID from APP_DRIVER_DETAILS                                                
    where CUSTOMER_ID=@CUSTOMERID  and  APP_ID=@APPID  and  APP_VERSION_ID=@APPVERSIONID                                                 
     and DRIVER_ID=@DRIVERID                                    
)                                                
                                                
                                                
                                                
               
declare @COLLEGE_INSELSE char          
set @COLLEGE_INSELSE='N'                                                
                                                
IF(@PARENTS_INSURANCE=11934 AND (@DRIVER_STUD_DIST_OVER_HUNDRED='1' OR @DRIVER_STUD_DIST_OVER_HUNDRED='Y'))                                                
BEGIN         
SET @COLLEGE_INSELSE='Y'                                                
END                                                 
                                                
-----------------                                               
-- If college student is YES and If "yes" to "Do you keep the car with you?", then look at the Licensed                                                 
-- State on the Driver/Household Member tab                     
-- If the Licensed State is not equal to the State on the Application/Policy details - State Field , then Refer to Underwriters                           
declare @COLLEGE_CAR_STATE char                                                
declare @COLLEGE_CAR_STATE_VEHCILE char                                                
                                                
set @COLLEGE_CAR_STATE='N'                                                
set @COLLEGE_CAR_STATE_VEHCILE='N'                                                
              
IF(@DRIVER_STUD_DIST_OVER_HUNDRED='Y' AND @HAVE_CAR=10963 AND (@STATE_ID<>@INTDRIVER_LIC_STATE))                                                
 BEGIN                                                 
 SET @COLLEGE_CAR_STATE='Y'                                                
 END                 
ELSE IF(@DRIVER_STUD_DIST_OVER_HUNDRED='Y' AND @HAVE_CAR=10963 AND (@STATE_ID=@INTDRIVER_LIC_STATE) AND (@REGISTERED_STATE<>@STATE_ID))                                                
 BEGIN                                                 
 --IF EQUAL THEN LOOK AT THE VEHICLE INFO TAB FOR THE CAR ASSIGNED ON THE RESPECTIVE  DRIVER/MEMBER TAB                                           
 --IF REGISTERED STATE IS NOT  EQUAL TO THE  STATE ON THE APPLICATION/POLICY DETAILS - STATE FIELD ,REFER TO UNDERWRITERS                                                 
 SET @COLLEGE_CAR_STATE_VEHCILE='Y'                                      
 END                                                 
                               
--------------------------------                                  
                                                
 /* Issue no 336                                   
Driver/Household Member If Driver is under 25 years of age the question Are you in the Military?* will                                         
 appear on this screen If Yes  The look at Field Parents Insurance If Insured Elsewhere - then refer to underwriters */                                     
                                                
DECLARE @DRV_MIL_INSELSE CHAR                                                 
SET @DRV_MIL_INSELSE='N'                                                
                                        
IF(@INT_DIFFERENCE<25 AND @IN_MILITARY=10963 AND @PARENTS_INSURANCE=11934)                                                
BEGIN                        
SET @DRV_MIL_INSELSE='Y'                                                
END                                                
---                                                
/*If Part of this Policy or Separate Policy with Wolverine                                                 
If yes                                                 
Then look at the Field Are you stationed in US, Canada, Puerto Rico or  other US Territories                                                 
If No - then no rating and no rates apply - and the option for Assigned Vehicle is not visible                                                
If yes then go the field Do you have the car with you"                                                
If No to   Do you keep the car with you*                                   
Then apply Class 5C to the vehicle they drive - Assigned Vehicle Field                                                 
                        
If yes to Do you keep the car with you?                                                
Then look at the Licensed State on the Driver/Household Member tab                                                 
If the Licensed State is not equal to the State on the Application/Policy details - State Field                                               
Refer to Underwriters*/                                       
                                                
DECLARE @PARNT_USTERR_CAR_STA CHAR                              
SET @PARNT_USTERR_CAR_STA='N'                                               
                                        
IF(@PARENTS_INSURANCE=11935 AND @STATIONED_IN_US_TERR=10963 AND @HAVE_CAR=10963 AND (@STATE_ID<>@INTDRIVER_LIC_STATE))                                                 
BEGIN                                                 
SET @PARNT_USTERR_CAR_STA='Y'                                               
END                                                 
------------------------------------------------------------------------------------------------------------                                                
/*If State is Michigan Driver/Household member If age is over 60                                                 
Endorsement A-94 is available By checking off Yes to the Waiver of Loss Income Coverage                              
 DECLARE @MI_OLDDRIVER CHAR                     
 SET @MI_OLDDRIVER='N'       
                                         
 IF(@STATE_ID=22 AND @INT_DIFFERENCE>60 )                                                
 BEGIN                                                 
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_VEHICLE_COVERAGES COV                                                
 WHERE  COV.CUSTOMER_ID=@CUSTOMERID AND  COV.APP_ID=@APPID  AND  COV.APP_VERSION_ID=@APPVERSIONID                                                   
 AND  COV.COVERAGE_CODE_ID IN (1006)                                                 
 AND  COV.VEHICLE_ID IN(SELECT VEHICLE_ID FROM APP_DRIVER_DETAILS D                           
 WHERE D.CUSTOMER_ID=@CUSTOMERID AND D.APP_ID=@APPID  AND  D.APP_VERSION_ID=@APPVERSIONID                                                   
 AND D.DRIVER_ID= @DRIVERID))                                                
 BEGIN                                                 
 SET @MI_OLDDRIVER='Y'                                                
 END                                         
                                         
 END                             
*/                                                
/*If State is Michigan Driver/Household member If age is over 60                                                 
Rule (Refer) should be implemented as follows:                            
If Waiver of Loss is Yes and A-94 not available OR A-94 available and Waiver of Loss is not Yes for any of driver */                                     
                                                
DECLARE @COV_A94_EXISTS CHAR                            
DECLARE @MI_OLDDRIVER CHAR                             
DECLARE @ELIGIBLE_VEHICLE CHAR                                          
SET @ELIGIBLE_VEHICLE='N'                                              
SET @MI_OLDDRIVER='N'                                                 
SET @COV_A94_EXISTS ='N'                                                
-- ADDED BY PRAVESH ON 25 JULY 11618->Suspended-Comp Only ;11337->Utility Trailer ; 11341->Trailer A94 IS NOT APPLICABLE TO THESE TYPE                                         
IF EXISTS(SELECT V.CUSTOMER_ID FROM APP_VEHICLES V WHERE V.CUSTOMER_ID=@CUSTOMERID AND V.APP_ID=@APPID                            
   AND V.APP_VERSION_ID=@APPVERSIONID                             
   AND  ISNULL(V.VEHICLE_TYPE_PER,0) NOT IN (11337,11618)                            
   AND ISNULL(V.VEHICLE_TYPE_COM,0) NOT IN (11341)                              
  )                            
BEGIN                            
 SET @ELIGIBLE_VEHICLE='Y'      
END                                           
--END HERE                                        
IF EXISTS(SELECT COV.CUSTOMER_ID FROM APP_VEHICLE_COVERAGES COV                  
 WHERE COV.CUSTOMER_ID=@CUSTOMERID AND  COV.APP_ID=@APPID  AND  COV.APP_VERSION_ID=@APPVERSIONID                                                 
 AND COV.COVERAGE_CODE_ID IN (1006)                                                 
 AND  COV.VEHICLE_ID IN(                            
   SELECT VEHICLE_ID FROM APP_DRIVER_ASSIGNED_VEHICLE D                                                
   WHERE D.CUSTOMER_ID=@CUSTOMERID AND  D.APP_ID=@APPID  AND  D.APP_VERSION_ID=@APPVERSIONID                  
   AND D.DRIVER_ID= @DRIVERID                                    
   )                            
 )                                               
BEGIN                                                 
SET @COV_A94_EXISTS='Y'                                                
END                                                 
ELSE                                                
BEGIN                                                 
SET @COV_A94_EXISTS='N'                                                
END                                                
                                        
---                                                 
--ADDED BY PRAVEEN KUMAR(02-03-2009):ITRACK 5503                    
IF  EXISTS(              
 SELECT APP.VEHICLE_ID FROM               
 APP_VEHICLES APP INNER JOIN APP_DRIVER_ASSIGNED_VEHICLE ASS ON                    
 APP.CUSTOMER_ID = ASS.CUSTOMER_ID AND APP.APP_ID = ASS.APP_ID AND APP.APP_VERSION_ID = ASS.APP_VERSION_ID         
 WHERE APP.IS_ACTIVE = 'Y' AND APP.CUSTOMER_ID = @CUSTOMERID AND APP.APP_ID = @APPID AND ASS.DRIVER_ID=@DRIVERID                    
 AND USE_VEHICLE ='11332'              
 AND APP.APP_VERSION_ID = @APPVERSIONID and ASS.APP_VEHICLE_PRIN_OCC_ID IN('11398','11399')            
 AND APP.IS_SUSPENDED <> 10963 -- ADDED CONDITION OF 'IS_SUSPENDED' for Itrack Issue 5609 on 8 April 2009          
 )              
-- END PRAVEEN KUMAR                      
--IF(@VEHICLEUSE <> '11333')                                           
BEGIN                    
 IF(@STATE_ID=22 AND @INT_DIFFERENCE>60 AND @COV_A94_EXISTS='Y' AND  @WAIVER_WORK_LOSS_BENEFITS='0' AND @ELIGIBLE_VEHICLE='Y')  --CHANGE BY PRAVESH  ADD CONDITION OF ELIGIBLE VEHICLE                                             
 BEGIN                                                     
 SET @MI_OLDDRIVER='Y'                                                    
 END                                      
 IF(@STATE_ID=22 AND @INT_DIFFERENCE>60 AND @COV_A94_EXISTS='N' AND @WAIVER_WORK_LOSS_BENEFITS='1' AND @ELIGIBLE_VEHICLE='Y')    --CHANGE BY PRAVESH  ADD CONDITION OF ELIGIBLE VEHICLE                                                 
 BEGIN                                                     
 SET @MI_OLDDRIVER='Y'                                                    
 END                    
END                                                            
-----------------------------                               
-----------------------------------------------------------------------------------------------------------                                                
/*                                                
If Extended Non- Owned Coverage for Named Insured is checked off, then when doing the verify make sure that                                                 
Drivers/Household members tab that the number of drivers in the limit field "Equal to" the number of drivers                                                 
that have a yes in the Field Extended Non Owned Coverages Required.                                   
If there is a yes in the Field Extended Non Owned Coverages Required on the                                                 
Drivers/Household members tab                                
Then make sure  Extended Non- Owned Coverage for Named Insured is checked off */                                   
declare @ENO  char                               
 set @ENO='N'                                             
 /* This Rule Moved To Vehicle Level as per Itrack 4771 on 29 Jan 09 By Pravesh                                   
declare @ADD_INFORMATION varchar(20)       
                                
select @ADD_INFORMATION=isnull(ADD_INFORMATION,0)                                                
  from APP_VEHICLE_COVERAGES COV                                                
   where  COV.CUSTOMER_ID=@CUSTOMERID AND  COV.APP_ID=@APPID  AND  COV.APP_VERSION_ID=@APPVERSIONID                            
   AND  COV.COVERAGE_CODE_ID IN (52,254)                   
--   AND  COV.VEHICLE_ID IN(select VEHICLE_ID from APP_DRIVER_DETAILS D                                                
--      where D.CUSTOMER_ID=@CUSTOMERID AND  D.APP_ID=@APPID  AND  D.APP_VERSION_ID=@APPVERSIONID AND D.DRIVER_ID= @DRIVERID)                             
 AND  COV.VEHICLE_ID IN                          
  (SELECT P.VEHICLE_ID FROM APP_DRIVER_DETAILS D   with(nolock)                            
   INNER JOIN APP_DRIVER_ASSIGNED_VEHICLE P with(nolock)                                               
   ON D.CUSTOMER_ID=P.CUSTOMER_ID AND D.APP_ID=P.APP_ID AND D.APP_VERSION_ID=P.APP_VERSION_ID AND D.DRIVER_ID=P.DRIVER_ID                              
   WHERE D.CUSTOMER_ID=@CUSTOMERID AND  D.APP_ID=@APPID  AND  D.APP_VERSION_ID=@APPVERSIONID AND D.DRIVER_ID= @DRIVERID                          
  )                        
                                                
-- Drivers/Household members tab that the number of drivers in the limit field "Equal to" the number of drivers                                        
-- that have a yes in the Field Extended Non Owned Coverages Required                                        
declare @intCount int                 
select @intCount= count(isnull(Driver_ID,0)) from APP_DRIVER_DETAILS                                              
where CUSTOMER_ID=@CUSTOMERID and  APP_ID=@APPID  and  APP_VERSION_ID=@APPVERSIONID  and  EXT_NON_OWN_COVG_INDIVI='10963'                                                
--                                                 
if(@ADD_INFORMATION<>@intCount)                                  
 set @ENO='Y'                                                
else                                                
 set @ENO='N'                              
                    
 -- End here Rule Moved To Vehicle Level as per Itrack 4771 on 29 Jan 09 By Pravesh                                   
*/                    
                    
--Commented on 18 SEP 2008  iTRACK 4771                             
/*                            
---NEW IMPlMTATION 04 JUNE 2008                            
--STEP 1                            
IF EXISTS(SELECT CUSTOMER_ID                                            
  FROM APP_VEHICLE_COVERAGES COV                                                
  WHERE  COV.CUSTOMER_ID=@CUSTOMERID AND  COV.APP_ID=@APPID  AND  COV.APP_VERSION_ID=@APPVERSIONID                                                   
  AND  COV.COVERAGE_CODE_ID IN (52,254)                                                 
  AND  COV.VEHICLE_ID IN(SELECT P.VEHICLE_ID FROM APP_DRIVER_DETAILS D                  
INNER JOIN APP_DRIVER_ASSIGNED_VEHICLE P                                                
ON D.CUSTOMER_ID=P.CUSTOMER_ID AND D.APP_ID=P.APP_ID AND D.APP_VERSION_ID=P.APP_VERSION_ID AND D.DRIVER_ID=P.DRIVER_ID                              
WHERE D.CUSTOMER_ID=@CUSTOMERID AND  D.APP_ID=@APPID  AND  D.APP_VERSION_ID=@APPVERSIONID                                                   
AND D.DRIVER_ID= @DRIVERID))                            
BEGIN                            
 IF EXISTS(SELECT CUSTOMER_ID FROM APP_DRIVER_DETAILS WHERE  CUSTOMER_ID=@CUSTOMERID AND                            
   APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID  AND  EXT_NON_OWN_COVG_INDIVI='10964' AND  DRIVER_ID = @DRIVERID)                            
 BEGIN                             
  SET @ENO='Y'               END                            
 ELSE                            
 BEGIN                            
  SET @ENO='N'                              
 END                            
END                            
--STEP 2 11332 Personal; 11618->Suspended-Comp Only ;11337->Utility Trailer ; 11341->Trailer ADDED BY pRAVESH ON 25 jULY 2008                            
IF (EXISTS(SELECT EXT_NON_OWN_COVG_INDIVI FROM APP_DRIVER_DETAILS WHERE  CUSTOMER_ID=@CUSTOMERID AND                            
   APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID  AND  EXT_NON_OWN_COVG_INDIVI='10963' AND  DRIVER_ID = @DRIVERID)                        
                             
 )                            
AND                            
(                            
 EXISTS(SELECT VEHICLE_ID FROM APP_VEHICLES WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                            
   AND ISNULL(VEHICLE_TYPE_PER,0) NOT IN (11337,11618)                            
   AND ISNULL(VEHICLE_TYPE_COM,0) NOT IN (11341)                             
    )                             
                            
)                            
BEGIN                            
IF NOT EXISTS(SELECT CUSTOMER_ID                                            
    FROM APP_VEHICLE_COVERAGES COV                                                
WHERE  COV.CUSTOMER_ID=@CUSTOMERID AND  COV.APP_ID=@APPID  AND  COV.APP_VERSION_ID=@APPVERSIONID                               
    AND  COV.COVERAGE_CODE_ID IN (52,254)                                                 
    AND  COV.VEHICLE_ID IN(                            
      SELECT P.VEHICLE_ID FROM APP_DRIVER_DETAILS D                               
      INNER JOIN APP_DRIVER_ASSIGNED_VEHICLE P                                                
      ON D.CUSTOMER_ID=P.CUSTOMER_ID AND D.APP_ID=P.APP_ID AND D.APP_VERSION_ID=P.APP_VERSION_ID AND D.DRIVER_ID=P.DRIVER_ID                              
      WHERE D.CUSTOMER_ID=@CUSTOMERID AND  D.APP_ID=@APPID  AND  D.APP_VERSION_ID=@APPVERSIONID                                                   
      AND D.DRIVER_ID= @DRIVERID                             
      )                            
 )                            
 BEGIN                             
  SET @ENO='Y'                                   
 END                            
 ELSE                            
 BEGIN                            
  SET @ENO='N'                              
 END                            
END                            
                            
--END NEW IMP                             
*/                                       
--------------------------------------------------------------------------------------------------------------------                                                
-- 3477- Excluded  if other then Licensed                           
--ADDED BY PRAVEEN KUMAR(11-12-08)                        
                                           
IF(@DRIVER_DRIV_TYPE<>'11603' AND @DRIVER_DRIV_LIC='')  ---ONLY THIS CONDITION PRAVEEN KUMAR END                                              
SET @DRIVER_DRIV_LIC='N'                            
                            
IF(@DRIVER_DRIV_TYPE<>'11603' AND @DATE_LICENSED='')                                               
SET @DATE_LICENSED='N'                              
                            
IF(@DRIVER_DRIV_TYPE <> '11603' AND @DRIVER_DRINK_VIOLATION='')  -- if other then Licensed                                              
SET @DRIVER_DRINK_VIOLATION='N'                                  
                          
IF(@DRIVER_DRIV_TYPE <> '11603' AND @DRIVER_LIC_STATE='')  -- if other then Licensed                                              
SET @DRIVER_LIC_STATE='N'                                  
                          
----moved by pravesh on 17 sep                           
--IF (@DRIVER_DRIV_TYPE = '3477' AND @FORM_F95 = 10963 ) -- Excluded   \                                                
-- BEGIN                          
--set @DRIVER_DRIV_TYPE='Y'                                    
--END                          
--                          
--ELSE IF (@DRIVER_DRIV_TYPE = '3477' AND @FORM_F95 = 10964 ) -- Excluded   \                              
--BEGIN                          
--set @DRIVER_DRIV_TYPE='N'                                   
--END                          
                                       
-------------------------------                                                
/*--- MANDATORY VIOLATIONS                             
 IF(@VIOLATIONS='')                                        
 BEGIN                                         
 SET @VIOLATIONS=''                           
 END                                       
 ELSE                                     
 BEGIN                                     
 SET @VIOLATIONS='N'                                    
 END                                    
 PRINT @VIOLATIONS                                    
---------------------------------  */                                    
/*Issues no 556                                      
For Driver Information If the License State is not same as that of application                                      
state then the application has been referred to underwriter*/                                       
                                      
--SELECT @DRIVER_LIC_STATE =DRIVER_LIC_STATE FROM APP_DRIVER_DETAILS                                      
--WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID                                       
                                     
DECLARE @DRIVER_LIC_STATE_APP_STATE VARCHAR(5)                                      
IF (@STATE_ID<>@INTDRIVER_LIC_STATE)                                      
 BEGIN                                      
 SET  @DRIVER_LIC_STATE_APP_STATE='Y'                                      
 END                                   
ELSE                                      
 BEGIN                                      
 SET @DRIVER_LIC_STATE_APP_STATE='N'                                      
 END                             
                                  
-------------------------------                                  
------------------------                                  
/*  DECLARE @T1 CHAR                                   
 --DECLARE @MVR_ORDERED INT                                  
 DECLARE @APP_VEHICLE_PRIN_OCC INT                                  
 PRINT @APP_VEHICLE_PRIN_OCC                                  
 PRINT @MVR_ORDERED                    
 SELECT @APP_VEHICLE_PRIN_OCC=APP_VEHICLE_PRIN_OCC_ID FROM APP_DRIVER_ASSIGNED_VEHICLE                                    
 WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND DRIVER_ID=@DRIVERID                             
 SELECT @MVR_ORDERED=MVR_ORDERED FROM APP_DRIVER_DETAILS                                  
 WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND DRIVER_ID=@DRIVERID                             
 IF( (@APP_VEHICLE_PRIN_OCC=11929 OR @APP_VEHICLE_PRIN_OCC= 11925 OR @APP_VEHICLE_PRIN_OCC=11398 OR @APP_VEHICLE_PRIN_OCC=11927) AND  @MVR_ORDERED=0)                                  
                                         
 BEGIN                                                 
 SET @T1='Y'                                                
 END                                    
 ELSE                                   
 BEGIN                               
 SET @T1='N'                                                
 END                                           
 PRINT @T1                             
 */                                             
-------------------------------------                             
DECLARE @MAJOR_VIOLATION VARCHAR(5)                            
SET @MAJOR_VIOLATION='N'                            
/*                            
 IF EXISTS (SELECT MVR_POINTS FROM APP_MVR_INFORMATION                                                  
 INNER JOIN  MNT_VIOLATIONS  ON APP_MVR_INFORMATION.VIOLATION_ID = MNT_VIOLATIONS.VIOLATION_ID                                                                          
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND DRIVER_ID=@DRIVERID                             
 AND ((ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(MVR_DATE),0))<=@MVR_PNTS_YEARS AND               
   (ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(MVR_DATE),0))>=0) AND VIOLATION_TYPE = 459)                            
                             
 BEGIN                             
  SET @MAJOR_VIOLATION ='Y'                            
 END                             
*/                           
      
--ADDED BY PRAVEEN KUMAR(22-01-09):To make verification rule for ARE YOU IN MILITARY COMBO IN DRIVER PAGE                    
         
IF(@INT_DIFFERENCE < 25)                    
BEGIN                    
 IF(@IN_MILITARY in(10963,10964))                    
  SET @IN_MILITARY_NEW='N'                    
 ELSE                    
  SET @IN_MILITARY_NEW=''                    
END                
                    
ELSE                   
 SET @IN_MILITARY_NEW='N'                  
                    
--To make verification rule for ARE YOU STATIONED IN U.S COMBO IN DRIVER PAGE    
IF(@INT_DIFFERENCE < 25 and @IN_MILITARY = 10963)                    
BEGIN                    
 IF(@STATIONED_IN_US_TERR = 10963 or @STATIONED_IN_US_TERR = 10964)            
  SET @STATIONED_IN_US_TERR_NEW = 'N'                    
 ELSE                    
   SET @STATIONED_IN_US_TERR_NEW = ''                    
END                     
                    
ELSE                    
 SET @STATIONED_IN_US_TERR_NEW = 'N'                    
                    
--To make verification rule for DO YOU HAVE THE CAR WITH YOU COMBO IN DRIVER PAGE                    
                    
IF(@INT_DIFFERENCE < 25 and @IN_MILITARY = 10963)                    
BEGIN                    
 IF(@HAVE_CAR = 10963 or @HAVE_CAR = 10964)                    
  SET @HAVE_CAR_NEW = 'N'                    
 ELSE                    
   SET @HAVE_CAR_NEW = ''                    
END                     
                    
ELSE                    
 SET @STATIONED_IN_US_TERR_NEW = 'N'                    
              
              
                    
--------------END BY PRAVEEN KUMAR----------                    
                    
  IF(@DRIVER_DRIV_TYPE = '3477' AND @IN_MILITARY_NEW='')--Done by Sibin on 13 Feb 09 for Itrack Issue 5424                  
   BEGIN                  
     SET @IN_MILITARY_NEW='N'                  
   END                     
                       
                            
--------------------------------                        
--Moved by Sibin for Itrack Issue 5424                
--moved by pravesh on 17 sep                           
IF (@DRIVER_DRIV_TYPE = '3477' AND @FORM_F95 = 10963 ) -- Excluded   \                  
 BEGIN                          
set @DRIVER_DRIV_TYPE='Y'                                    
END                          
                          
ELSE IF (@DRIVER_DRIV_TYPE = '3477' AND @FORM_F95 = 10964 ) -- Excluded   \                              
BEGIN                          
set @DRIVER_DRIV_TYPE='N'            
      
      
      
END            
    
                          
SELECT                                                                                                
 @DRIVER_DRINK_VIOLATION as DRIVER_DRINK_VIOLATION,                                                            
 @DRIVER_US_CITIZEN as US_CITIZEN,                                                                            
 @DISTANT_STUDENT as DISTANT_STUDENT, --Added by Charles on 18-Nov-09 for Itrack 6725                                                                                    
 @DRIVER_NAME as DRIVER_NAME,                                                                                                            
 @DRIVER_DRIV_LIC as DRIVER_DRIV_LIC,                                                                                                            
 @DRIVER_CODE as DRIVER_CODE,                                                                                                             
 @DRIVER_SEX as DRIVER_SEX ,                                           
 @DRIVER_FNAME as DRIVER_FNAME,                                                         
 @DRIVER_LNAME as DRIVER_LNAME,                                      
 @DRIVER_STATE as DRIVER_STATE,                          
 @DRIVER_ZIP as DRIVER_ZIP,                                  
 @DRIVER_LIC_STATE as DRIVER_LIC_STATE,                                  
 @DRIVER_DRIV_TYPE as DRIVER_DRIV_TYPE,                          
 @DRIVER_VOLUNTEER_POLICE_FIRE as DRIVER_VOLUNTEER_POLICE_FIRE,                                                                          
 @DATE_LICENSED as DATE_LICENSED, -- Driver detail                  
 @CONT_DRIVER_LICENSE as CONT_DRIVER_LICENSE, --                                                                
 @DRIVER_DOB as DRIVER_DOB ,                                 
 @DEACTIVATEVEHICLE as DEACTIVATEVEHICLE ,                                   
 -- Rule only                                                             
 @SD_POINTS as SD_POINTS  ,                              
 @ISDRVASSIGNEDVEH as ISDRVASSIGNEDVEH,                                                                
 @WAIVER_WORK_LOSS as WAIVER_WORK_LOSS,                                                
 @YOUTH_DRIVER as YOUTH_DRIVER,                                                
 @VIOLATION_POINT as VIOLATION_POINT,                                                
 @DRV_WITHPOINTS as DRV_WITHPOINTS,                                                
 @DRV_WITHOUTPOINTS as DRV_WITHOUTPOINTS ,                                                
 @DRIVER_PARENT_ELSEWHERE as DRIVER_PARENT_ELSEWHERE,                                                
 @DRIVER_GOOD_STUDENT as DRIVER_GOOD_STUDENT,                                                
 @COLLEGE_INSELSE AS COLLEGE_INSELSE,                                                
 @COLLEGE_CAR_STATE AS COLLEGE_CAR_STATE,                                                
 @COLLEGE_CAR_STATE_VEHCILE AS COLLEGE_CAR_STATE_VEHCILE,                                                
 @DRV_MIL_INSELSE as DRV_MIL_INSELSE,                                                
 @PARNT_USTERR_CAR_STA as PARNT_USTERR_CAR_STA,                                                
 @MI_OLDDRIVER as MI_OLDDRIVER,                                                
 @ENO as ENO ,                                        
 @DRIVER_MVR_ORDERED AS DRIVER_MVR_ORDERED,                                        
 @VIOLATIONS AS VIOLATIONS,                                      
 @DRIVER_LIC_STATE_APP_STATE AS DRIVER_LIC_STATE_APP_STATE ,                              
 @DRIVER_SUPPORTING_DOCUMENT AS DRIVER_SUPPORTING_DOCUMENT,                            
 @MAJOR_VIOLATION as MAJOR_VIOLATION ,                          
@NEGATIVE_VIOLATION_POINT AS NEGATIVE_VIOLATION_POINT ,                          
@IN_MILITARY_NEW AS IN_MILITARY ,                     
@STATIONED_IN_US_TERR_NEW AS  STATIONED_IN_US_TERR ,            
@HAVE_CAR_NEW  AS HAVE_CAR,              
 @DRIVERTURNEITTEEN AS  DRIVERTURNEITTEEN,  
@PARENTS_INSURANCE_NEW AS PARENTS_INSURANCE  --Added by Charles on 18-Nov-09 for Itrack 6725                                 
END 


GO

