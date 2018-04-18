IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMotorcycleRule_Drivers]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMotorcycleRule_Drivers]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------                                                                            
Proc Name                : Dbo.Proc_GetMotorcycleRule_Drivers                                                            
Created by               : Ashwani                                                                            
Date                     : 19 Oct.,2005                                                                          
Purpose                  : To get the Driver's Rule Information for Motorcycle                                                      
Revison History          :                                                                            
Used In                  : Wolverine                                                                  
Modified by               : Sumit Chhabra                      
Date                     : 13 April,2006                                                                          
Purpose                  : Added the check for mvr violations not exceeding 5 points during the past 3 years                      
------------------------------------------------------------                                                                            
Date     Review By          Comments                                                                            
------   ------------       -------------------------*/                
--drop proc dbo.Proc_GetMotorcycleRule_Drivers 547,226,1,1,'desc'               
CREATE proc [dbo].[Proc_GetMotorcycleRule_Drivers]   --                                                                         
(                                                                              
@CUSTOMERID    int,                                                                              
@APPID    int,                                                                              
@APPVERSIONID   int,                                                                    
@DRIVERID int,                                                     
@DESC varchar(10)                                                                           
)                                                                              
AS                                                               
BEGIN                                                     
-- Driver Detail                                                                              
--APP_DRIVER_DETAILS                                                                              
 DECLARE @DRIVER_FNAME NVARCHAR(75)                                                                             
 DECLARE @DRIVER_LNAME NVARCHAR(75)                                                                             
 DECLARE @DRIVER_CODE NVARCHAR(20)                                                                              
 DECLARE @DRIVER_SEX NCHAR(12)                                                     
 DECLARE @DRIVER_ADD1 NVARCHAR(70)                                                    
 DECLARE @DRIVER_CITY NVARCHAR(70)                                                    
 DECLARE @DRIVER_STATE NVARCHAR(5)                                                                            
 DECLARE @DRIVER_ZIP VARCHAR(11)                                                     
 DECLARE @DRIVER_DOB DATETIME                                                                               
 DECLARE @DRIVER_LIC_STATE NVARCHAR(5)                                                        
 DECLARE @RELATIONSHIP INT                                                    
 DECLARE @VEHICLE_ID VARCHAR(15)                                                                           
 DECLARE @DRIVER_US_CITIZEN CHAR                                                
 DECLARE @DRIVER_NAME NVARCHAR(225)                                               
 DECLARE @DRIVER_DRIV_LIC NVARCHAR(30)                                          
 DECLARE @INTAPP_VEHICLE_PRIN_OCC_ID INT           
 DECLARE @APP_VEHICLE_PRIN_OCC_ID CHAR                      
 DECLARE @DRIVER_DRINK_VIOLATION CHAR                       
 DECLARE @COLL_STUD_AWAY_HOME CHAR                      
 DECLARE @CYCL_WITH_YOU CHAR                      
 DECLARE @VIOLATIONS VARCHAR(5)                    
 DECLARE @INTVIOLATIONS INT             
---ADDED BY PRAVEEN KUMAR FOR ITRACK 5118--------            
 DECLARE @DRIVER_DRIV_TYPE nvarchar(9)            
            
            
 DECLARE @VIOLATIONREFERYEAR INT              
 DECLARE @ACCIDENTREFERYEAR INT              
 DECLARE @VIOLATIONNUMYEAR INT              
 DECLARE @ACCIDENTCHARGES INT              
 SET   @VIOLATIONREFERYEAR=3              
 SET   @ACCIDENTREFERYEAR =3              
 SET   @VIOLATIONNUMYEAR=2                 
 SET   @ACCIDENTCHARGES=1000             
            
 SELECT @DRIVER_DRIV_TYPE=DRIVER_DRIV_TYPE from App_driver_details where            
 CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID and DRIVER_ID=@DRIVERID            
            
-------------END PRAVEEN KUMAR------------------                    
 --Made Y to blank for itrack 5118 --Praveen kumar                      
SET @DRIVER_DRINK_VIOLATION=''                                
IF EXISTS (SELECT CUSTOMER_ID FROM APP_DRIVER_DETAILS                                                                             
WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND DRIVER_ID=@DRIVERID)                                        
BEGIN                                                                                 
 SELECT                                                     
 @DRIVER_FNAME=isnull(DRIVER_FNAME,''),@DRIVER_LNAME=isnull(DRIVER_LNAME,''),                                                    
 @DRIVER_CODE=isnull(DRIVER_CODE,''),@DRIVER_SEX=isnull(DRIVER_SEX,'') ,                                                    
 @DRIVER_ADD1=isnull(DRIVER_ADD1,''),@DRIVER_CITY=isnull(DRIVER_CITY,''),                                                    
 @DRIVER_STATE=isnull(DRIVER_STATE,''),@DRIVER_ZIP=isnull(DRIVER_ZIP,''),                                                    
 @DRIVER_DOB=isnull(DRIVER_DOB,''),@DRIVER_LIC_STATE=isnull(DRIVER_LIC_STATE,''),                                                    
 @RELATIONSHIP=isnull(RELATIONSHIP,0),@VEHICLE_ID=isnull(convert(varchar(15),VEHICLE_ID),''),@DRIVER_US_CITIZEN=isnull(DRIVER_US_CITIZEN,0),                                              
 @DRIVER_NAME=(isnull(DRIVER_FNAME,'') + '  ' + isnull(DRIVER_MNAME,'') + '  ' + isnull(DRIVER_LNAME,'')),                                              
 @DRIVER_DRIV_LIC=isnull(DRIVER_DRIV_LIC,''),@INTAPP_VEHICLE_PRIN_OCC_ID=isnull(APP_VEHICLE_PRIN_OCC_ID,-1),                      
 @DRIVER_DRINK_VIOLATION=isnull(DRIVER_DRINK_VIOLATION,''),@COLL_STUD_AWAY_HOME=isnull(COLL_STUD_AWAY_HOME,''),                      
 @CYCL_WITH_YOU=isnull(CYCL_WITH_YOU,'') ,@intVIOLATIONS=VIOLATIONS ,@VIOLATIONS=ISNULL(VIOLATIONS,''),  
 @DRIVER_DRIV_TYPE=isnull(DRIVER_DRIV_TYPE,'')                     
FROM APP_DRIVER_DETAILS                                                                              
WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID   AND DRIVER_ID=@DRIVERID                                                                              
END                                                                               
ELSE                                                                       
BEGIN                                              
 SET @DRIVER_FNAME =''                                                                           
 SET @DRIVER_LNAME =''                                                        
 SET @DRIVER_CODE  =''                                                             
 SET @DRIVER_SEX  =''                                                             
 SET @DRIVER_ADD1 =''                                                             
 SET @DRIVER_CITY  =''                                
 SET @DRIVER_STATE =''                                                               
 SET @DRIVER_ZIP =''                                                    
 SET @DRIVER_DOB=''                                                             
 SET @DRIVER_LIC_STATE =''                                           
 SET @RELATIONSHIP=0                                                             
 SET @VEHICLE_ID=''                                                    
 SET @DRIVER_US_CITIZEN=''                                              
 SET @DRIVER_NAME=''                                              
 SET @DRIVER_DRIV_LIC=''                                         
 SET @APP_VEHICLE_PRIN_OCC_ID=''                     
 SET @VIOLATIONS=''                                                                          
                      
END                    
                    
------------                      
/* Driver/Household Member  Are you a college student?*  If yes and If yes in the  Field Do you have /cycle with you?*                      
Refer to underwriters */                      
DECLARE @COLLG_STD_CAR CHAR                      
SET @COLLG_STD_CAR='N'     
IF(@COLL_STUD_AWAY_HOME='1' AND @CYCL_WITH_YOU='1')                      
SET @COLLG_STD_CAR='Y'                           
--------------------                      
IF(@DRIVER_DRINK_VIOLATION='1')                                                              
BEGIN                                                               
SET @DRIVER_DRINK_VIOLATION='Y'                                                              
END                        
ELSE IF(@DRIVER_DRINK_VIOLATION='0')            
BEGIN               
SET @DRIVER_DRINK_VIOLATION='N'            
END            
ELSE                                      --Added else for itrack 5118 by Praveen Kumar.            
SET @DRIVER_DRINK_VIOLATION=''                 
--------------------                                        
IF(@INTAPP_VEHICLE_PRIN_OCC_ID=11399 OR @INTAPP_VEHICLE_PRIN_OCC_ID=11398)                                        
BEGIN                                         
SET @APP_VEHICLE_PRIN_OCC_ID='N'                         
END                                   
ELSE                                        
BEGIN                                         
SET @APP_VEHICLE_PRIN_OCC_ID=''                                        
END                                               
-------------------                                                    
IF(@DRIVER_US_CITIZEN='0')                                                              
BEGIN                                                               
SET @DRIVER_US_CITIZEN='Y'                                                              
END                               
ELSE IF (@DRIVER_US_CITIZEN='1')                                                              
BEGIN                                                               
SET @DRIVER_US_CITIZEN='N'                                                              
END                                                             
-------------------                                                   
--  Licence Date                                                
 declare @DATEAPP_EFFECTIVE_DATE datetime                                                                            
 declare @APP_EFFECTIVE_DATE char                                                                             
 declare @DATEDATE_LICENSED datetime                                                                            
 declare @DATE_LICENSED char                                                                            
-- declare @DATEDATECONT_DRIVER_LICENSE int                                
 declare @CONT_DRIVER_LICENSE  char -- 2.B.6 2.B.7.b                       
 declare @STATE_ID varchar(20)                                                                           
                          
 SELECT @DATEAPP_EFFECTIVE_DATE=APP_EFFECTIVE_DATE,@STATE_ID=STATE_ID                                                                             
 FROM APP_LIST  WITH(NOLOCK)                                     
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND ISNULL(IS_ACTIVE,'N')='Y'                      
                                                                            
                     
 SELECT @DATEDATE_LICENSED=DATE_LICENSED                                                                             
 FROM APP_DRIVER_DETAILS WITH(NOLOCK)                                                                           
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND DRIVER_ID=@DRIVERID                                                                            
                                                                            
                                                                            
IF(@DATEAPP_EFFECTIVE_DATE IS NOT NULL AND @DATEDATE_LICENSED IS NOT NULL)                                           
BEGIN                                                                             
-- SET @DATEDATECONT_DRIVER_LICENSE = DATEDIFF(MM,@DATEDATE_LICENSED,@DATEAPP_EFFECTIVE_DATE)                                                          
 IF (SELECT DATEADD(MONTH, 12, CONVERT(DATETIME,@DATEDATE_LICENSED) )) >((SELECT CONVERT(DATETIME,@DATEAPP_EFFECTIVE_DATE)))                                      
  BEGIN                                       
  SET @CONT_DRIVER_LICENSE='Y'                                
  END                                       
 ELSE                                      
  BEGIN                        
  SET @CONT_DRIVER_LICENSE='N'                                      
  END                                        
END                                                                            
-------------------                                                                     
/* if(@DATEDATECONT_DRIVER_LICENSE <12)                                                                       
 begin                                                                             
  set @CONT_DRIVER_LICENSE='Y'                                                                            
 end                                                                            
 else                                      
 begin                                      
  set @CONT_DRIVER_LICENSE='N'                                                                        
 end                                        */                                      
-------------------                                                                            
IF(@DATEAPP_EFFECTIVE_DATE IS NULL)                         
 BEGIN                                                                     
 SET @APP_EFFECTIVE_DATE=''                                                                       
 END                                                                            
ELSE                                                                             
 BEGIN                                              
 SET @APP_EFFECTIVE_DATE='N'                                                                            
 END                                                                             
---------------                                                                             
IF(@DATEDATE_LICENSED IS NULL)                                                                
 BEGIN                                                                 
 SET @DATE_LICENSED=''                                                                            
 END                                                                            
ELSE                                                           
 BEGIN        
 SET @DATE_LICENSED='N'        
 END                                                   
      
-----------------                
                                  
--IF(@DRIVER_DRIV_LIC='')                                      
-- BEGIN               
--                                             
-- SET @DRIVER_DRIV_LIC='N'                                              
-- END                
--ADDED BY PRAVEEN KUMAR ITRACK 5118------            
IF (@DRIVER_DRIV_TYPE= '11942')            
BEGIN                                              
  IF(@DRIVER_DRIV_LIC='')                                                  
  BEGIN                                                   
   SET @DRIVER_DRIV_LIC='N'                                                
  END  
 --Added by Manoj Rathore Itrack # 5881    
  IF(@DRIVER_LIC_STATE='')                                                  
  BEGIN                                                   
   SET @DRIVER_LIC_STATE='N'                                                
  END  
  IF(@DATE_LICENSED='')                                                  
  BEGIN                                                   
   SET @DATE_LICENSED='N'                                                
  END   
  IF(@DRIVER_DRINK_VIOLATION='')                                                  
  BEGIN                                                   
   SET @DRIVER_DRINK_VIOLATION='N'                                                
  END  
            
END             
--------------------END PRAVEEN KUMAR---------               
/*IF(@VEHICLE_ID=0)                                    
BEGIN                                     
SET @VEHICLE_ID=''                                    
END */   
---Added by Manoj Rathore on 23 Jun 2009 Itrack # 6002   
SET @VEHICLE_ID='1'  
IF(@DRIVER_DRIV_TYPE='11941')  
BEGIN  
 IF NOT EXISTS(SELECT PDD.DRIVER_ID FROM APP_VEHICLES PV INNER JOIN APP_DRIVER_DETAILS PDD  
 ON  PV.CUSTOMER_ID = PDD.CUSTOMER_ID AND PV.APP_ID = PDD.APP_ID AND                                                      
 PV.APP_VERSION_ID = PDD.APP_VERSION_ID AND PV.IS_ACTIVE='Y'  
 INNER JOIN APP_DRIVER_ASSIGNED_VEHICLE PDAV   
 ON PDD.CUSTOMER_ID = PDAV.CUSTOMER_ID AND PDD.APP_ID = PDAV.APP_ID AND                                                      
 PDD.APP_VERSION_ID = PDAV.APP_VERSION_ID   
 WHERE PDD.CUSTOMER_ID=@CUSTOMERID AND PDD.APP_ID=@APPID AND   
 PDD.APP_VERSION_ID=@APPVERSIONID AND PDD.DRIVER_ID=@DRIVERID)                                                                          
 BEGIN                                           
 SET @VEHICLE_ID=''                         
 END   
END                                                                               
-------------------------------------------------------------                                  
IF(@DRIVER_SEX='M')                                  
 BEGIN                                   
 SET @DRIVER_SEX='MALE'                                  
 END                                   
ELSE IF(@DRIVER_SEX='F')                                  
 BEGIN             
 SET @DRIVER_SEX='FEMALE'                                  
 END                                  
---------------------------------------------------------------------------------------------------------------------------                                 
--On Motorcycle, if over 2 points and  Driver License date < 3 years ,then Preferred Risk Discount should not be there, refer in such case.                                 
                                
--1. MVR_Points                                 
                 
 DECLARE @INTSD_PONITS INT                                  
                                
 SELECT @INTSD_PONITS=ISNULL(SUM(WOLVERINE_VIOLATIONS),0) FROM VIW_DRIVER_VIOLATIONS WHERE VIOLATION_ID IN                            
 (                                      
SELECT VIOLATION_ID                                            
   FROM   APP_MVR_INFORMATION                                               
   WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND DRIVER_ID=@DRIVERID      
   AND IS_ACTIVE='Y'                                    
 )                                
--2.                                 
 DECLARE @DATE_DATE_LICENSED DATETIME                                
 DECLARE @PREFERRED_RISK CHAR                            
                                
 SELECT @DATE_DATE_LICENSED=DATE_LICENSED ,@PREFERRED_RISK=PREFERRED_RISK                                                      
 FROM APP_DRIVER_DETAILS   WITH(NOLOCK)                                                                                     
 WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID  and DRIVER_ID=@DRIVERID                                
-- 3.                        
 DECLARE @INTDATE INT                                
 SELECT @INTDATE=DATEDIFF(MONTH,@DATE_DATE_LICENSED,GETDATE())                                
--4.                                 
DECLARE @PREFERRED_DISC  CHAR                                
                        
IF(@PREFERRED_RISK ='1')                            
BEGIN                             
 IF(@INTDATE<36 OR @INTSD_PONITS>2)                                
  BEGIN                                
  SET @PREFERRED_DISC='Y'                                
  END                   
 ELSE                          
  BEGIN                                
  SET @PREFERRED_DISC='N'                                
  END                              
END                             
ELSE                            
 BEGIN                             
 SET @PREFERRED_DISC='N'                                
 END                                
-- 5.                                
IF(@PREFERRED_DISC='')                      
BEGIN                                 
SET @PREFERRED_DISC='N'                     
END                                
------------------------------------------------------------------------------------------                                                                    
--MVR Violation, Accumulation of over 5 points during the past 3 years                        
--Rule for ineligible  driving record.                                                   
--declare @IS_CONVICTED_ACCIDENT char                                                                
--DECLARE @INT_CONVICTED_ACCIDENT INT                
DECLARE @INT_CONVICTED_ACCIDENT char                
                                                
IF EXISTS(SELECT APP_ID  FROM APP_AUTO_GEN_INFO                                                                  
WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID)                                                             
BEGIN                                                                 
 SELECT @INT_CONVICTED_ACCIDENT=ISNULL(IS_CONVICTED_ACCIDENT,'')                                                      
 FROM APP_AUTO_GEN_INFO                                         
 WHERE  CUSTOMER_ID=@CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID = @APPVERSIONID                                                               
END                                                                
ELSE                                                                
BEGIN                                                                 
SET @INT_CONVICTED_ACCIDENT =''                                                        
END                              
                                        
/* Drivers/Household Members If the total of  points for Violations and Claims is greater then 5                       
   BASED on the effective date of the policy minus the date of the violation or claim is 3 or less                 
   (LAST 3 YEARS) then refer to UNDERWRITERS  */                      
DECLARE @INTMVR_PONITS INT                       
DECLARE @INTACCIDENT_POINTS INT                      
DECLARE @SUMOFPOINTS INT                      
                            
DECLARE @SD_POINTS CHAR                        
SET @SD_POINTS ='N'           
-- Commented by Manoj Rathore on 18 Oct 2007                 
 /*SELECT @INTACCIDENT_POINTS=COUNT(CUSTOMER_ID) FROM FETCH_ACCIDENT                               
  WHERE CUSTOMER_ID = @CUSTOMERID AND (POLICY_ID IS NULL) AND (POLICY_VERSION_ID IS NULL) AND LOB=2                
  AND ((ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(OCCURENCE_DATE,0)),0))<=3                 
  AND (ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(ISNULL(OCCURENCE_DATE,0)),0))>=0)                 
  AND DRIVER_ID=@DRIVERID                              
                            
 IF (@INTACCIDENT_POINTS>0)                          
  SET @INTACCIDENT_POINTS = (ISNULL(@INTACCIDENT_POINTS,0) * 4 ) - 1                 
                                                            
 SELECT  @INTMVR_PONITS = SUM(ISNULL(MVR_POINTS,0)) FROM  APP_MVR_INFORMATION A  JOIN  MNT_VIOLATIONS M  ON   A.VIOLATION_ID = M.VIOLATION_ID                                        
 WHERE  A.CUSTOMER_ID = @CUSTOMERID AND  A.APP_ID = @APPID AND  A.APP_VERSION_ID = @APPVERSIONID AND A.DRIVER_ID = @DRIVERID AND    ISNULL(M.MVR_POINTS,0)>0                
  AND ((ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(MVR_DATE),0))<=3                 
  AND (ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(MVR_DATE),0))>=0)                                      
  AND A.IS_ACTIVE='Y'                
                 
 SET @SUMOFPOINTS = @INTMVR_PONITS + @INTACCIDENT_POINTS                      
                 
 IF(@SUMOFPOINTS > 5)                                                            
  BEGIN                                                             
  SET @SD_POINTS='Y'                                                            
  END                                                             
 ELSE                                                            
  BEGIN                                                             
  SET @SD_POINTS='N'                                                          
  END                 
*/                     
                      
-------------------------------------------------------------------                      
/* Application/Policy Details State Field                       
  If the state listed above is not equal to the state on the Drivers/Household Members tab                       
- Licensed  State Field for all drivers/household members on the policy Then refer to underwriters*/                  
DECLARE @APP_DATEVS_LIC_DATE CHAR                      
SET @APP_DATEVS_LIC_DATE='N'   
  
IF(@DRIVER_DRIV_TYPE='11941') --Added by Manoj Rathore Itrack # 5847  
BEGIN  
 IF (@DRIVER_LIC_STATE = '0' OR @DRIVER_LIC_STATE IS NULL)                    
  BEGIN                 
  SET @DRIVER_LIC_STATE=''                
  END                
               
                
 IF(@STATE_ID<>@DRIVER_LIC_STATE)                      
 BEGIN                       
 SET @APP_DATEVS_LIC_DATE='Y'                      
 END    
END                   
                
-----------------------------------------------------------------------                   
/* Commented by Manoj Rathore on 18 Oct 2007                 
  IF EXISTS( SELECT MVR_POINTS FROM APP_MVR_INFORMATION                                                
  INNER JOIN  MNT_VIOLATIONS  ON APP_MVR_INFORMATION.VIOLATION_ID = MNT_VIOLATIONS.VIOLATION_ID                                               
  WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DRIVER_ID=@DRIVERID                       
  AND (MVR_POINTS=8 OR MVR_POINTS<0) AND ((ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(MVR_DATE),0))<=5 AND                                
  (ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(MVR_DATE),0))>=0)                    
  AND APP_MVR_INFORMATION.IS_ACTIVE='Y')                
 */                
            
----------------------MISC POINTS Itrack # 5081 Manoj Rathore -----------------      
                              
DECLARE @SUMOFMISCPOINTS int                   
DECLARE @LICUNDER3YRS VARCHAR(5)        
SET @SUMOFMISCPOINTS = 0         
SELECT     
 @LICUNDER3YRS  = CASE                  
 WHEN DATEDIFF(DAY, CONVERT(CHAR(10),adds.DATE_LICENSED,101), @DATEAPP_EFFECTIVE_DATE)/365 >= 3 THEN 'N'                                
 ELSE 'Y'                    
 END        
 FROM                             
APP_DRIVER_DETAILS adds WITH (NOLOCK)         
           
WHERE                                           
adds.CUSTOMER_ID=@CUSTOMERID AND adds.APP_ID=@APPID AND adds.APP_VERSION_ID=@APPVERSIONID         
AND adds.DRIVER_ID=@DRIVERID          
        
         
IF (@LICUNDER3YRS = 'Y')        
BEGIN        
 SET @SUMOFMISCPOINTS     = 3                            
END        
        
----------------------------------------------------------------------------------        
/* Driver/Household Member MVR tab If there is any violation with a point count of (8)Refer to underwriters */                      
                       
DECLARE @VIOLATION_POINT char                      
SET  @VIOLATION_POINT='N'  
    
IF(@DRIVER_DRIV_TYPE='11941') --Added by Manoj Rathore Itrack # 5847  
BEGIN              
 -- Check Major Violation With in 5 years    
              
 DECLARE @intMAJOR_VIOLATION INT                 
 SELECT @intMAJOR_VIOLATION=ISNULL(SUM(ISNULL(POINTS_ASSIGNED,0))+ SUM(ISNULL(ADJUST_VIOLATION_POINTS,0)),0)                 
 FROM APP_MVR_INFORMATION                 
 INNER JOIN  VIW_DRIVER_VIOLATIONS  ON APP_MVR_INFORMATION.VIOLATION_TYPE = VIW_DRIVER_VIOLATIONS.VIOLATION_ID                  
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DRIVER_ID=@DRIVERID                
 AND VIOLATION_CODE IN('10000','40000','SUSPN')               
 AND DATEDIFF(DAY,MVR_DATE,@DATEAPP_EFFECTIVE_DATE)<= 5*365.5               
 AND DATEDIFF(DAY,MVR_DATE,@DATEAPP_EFFECTIVE_DATE)>= 0 AND APP_MVR_INFORMATION.IS_ACTIVE='Y'              
 -----------------      
      
 -- Check Minor Violation With in 3 years             
 DECLARE @MINOR_VIOLATION INT                 
 SELECT @MINOR_VIOLATION=ISNULL(SUM(ISNULL(POINTS_ASSIGNED,0))+ SUM(ISNULL(ADJUST_VIOLATION_POINTS,0)),0)                FROM APP_MVR_INFORMATION               
 INNER JOIN  VIW_DRIVER_VIOLATIONS  ON APP_MVR_INFORMATION.VIOLATION_TYPE = VIW_DRIVER_VIOLATIONS.VIOLATION_ID                  
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID   AND DRIVER_ID=@DRIVERID                
 AND VIOLATION_CODE NOT IN('10000','40000','SUSPN')               
 AND DATEDIFF(DAY,MVR_DATE,@DATEAPP_EFFECTIVE_DATE)<= 3*365.25               
 AND DATEDIFF(DAY,MVR_DATE,@DATEAPP_EFFECTIVE_DATE)>= 0 AND APP_MVR_INFORMATION.IS_ACTIVE='Y'              
 -- Sum of Major and Minor violation greater than 5 and less than 0 refer to underwriter                
 IF((@intMAJOR_VIOLATION + @MINOR_VIOLATION + @SUMOFMISCPOINTS) > 5 or (@intMAJOR_VIOLATION + @MINOR_VIOLATION + @SUMOFMISCPOINTS)< 0)               
 BEGIN                       
 SET @VIOLATION_POINT='Y'                      
 END                 
                
 --code added by Pravesh on 29 dec 2008 Itrack 5101            
-- ACCIDENT POINTS ACCUMULATION              
 DECLARE @ACCIDENT_POINTS INT        
 DECLARE @MISCPOINTS INT --added by Manoj Rathore  Itrack # 5081          
 CREATE TABLE #DRIVER_VIOLATION_ACCIDENT                
 (                
  [SUM_MVR_POINTS]  INT,                                                            
  [ACCIDENT_POINTS]  INT,                                                            
  [COUNT_ACCIDENTS]  INT,                
  [MVR_POINTS]  INT ,        
  [SUMOFMISCPOINTS] INT              
                                
 )                 
 INSERT INTO #DRIVER_VIOLATION_ACCIDENT exec GetMotorMVRViolationPoints @CUSTOMERID,@APPID,@APPVERSIONID ,@DRIVERID,@ACCIDENTREFERYEAR,@VIOLATIONNUMYEAR,@VIOLATIONREFERYEAR,@ACCIDENTCHARGES                           
 SELECT @ACCIDENT_POINTS = ACCIDENT_POINTS,@MISCPOINTS=SUMOFMISCPOINTS FROM #DRIVER_VIOLATION_ACCIDENT    
 IF(@ACCIDENT_POINTS IS NULL)                
 SET @ACCIDENT_POINTS =0      
 --added by Manoj Rathore  Itrack # 5081        
 IF(@MISCPOINTS IS NULL)                
 SET @MISCPOINTS =0               
 DROP TABLE  #DRIVER_VIOLATION_ACCIDENT               
       
 IF((@intMAJOR_VIOLATION + @MINOR_VIOLATION + @ACCIDENT_POINTS + @MISCPOINTS) > 5 or (@intMAJOR_VIOLATION + @MINOR_VIOLATION + @ACCIDENT_POINTS + @MISCPOINTS)< 0)                     
 BEGIN                       
 SET @VIOLATION_POINT='Y'                      
 END  
END                
            
---------------------`------------------------------------------------------------                   
-- If driver is assigned "with points" and No MVR information is provided OR driver is assigned with "no points" and                                 
-- MVR info is provided Refere to underwriter                        
                                
 DECLARE @DRV_WITHPOINTS CHAR                                
 DECLARE @DRV_WITHOUTPOINTS CHAR                                
 SET @DRV_WITHPOINTS='N'                          
 SET @DRV_WITHOUTPOINTS ='N'                  
            
            
----------------------------Added by praveen kumar  Itrack 5118---------            
IF @DRIVER_ADD1 =''                                                             
 SET @DRIVER_ADD1='N'            
            
IF @DRIVER_CITY  =''            
 SET @DRIVER_CITY='N'            
            
-----------------------------------END Praveen Kumar----------------       
  
  
------------------Added by Charles on 5-May-2009 for Itrack 5802------------------------------------------  
  
DECLARE @APP_MVR_ID INT                                       
DECLARE @MVR_ORDERED INT                                       
DECLARE @DRIVER_MVR_ORDERED CHAR                  
DECLARE @MVR_STATUS CHAR(1)            
SELECT @APP_MVR_ID=ISNULL(APP_MVR_ID,'0') FROM APP_MVR_INFORMATION                                       
 WHERE  CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID and DRIVER_ID=@DRIVERID and IS_ACTIVE='Y'                                      
SELECT @MVR_ORDERED=ISNULL(MVR_ORDERED,'0'),@MVR_STATUS=MVR_STATUS FROM APP_DRIVER_DETAILS                                       
WHERE CUSTOMER_ID=@CUSTOMERID and APP_ID=@APPID and APP_VERSION_ID=@APPVERSIONID and DRIVER_ID=@DRIVERID and IS_ACTIVE='Y'                          
  
IF(@DRIVER_DRIV_TYPE='11941')--Added by Manoj Rathore on 23 Jun. 2009 Itrack # 5847  
BEGIN              
 IF(@MVR_ORDERED=10964 OR @MVR_ORDERED IS NULL OR @MVR_ORDERED=0)                  
  BEGIN                  
  SET @DRIVER_MVR_ORDERED='Y'                    
  END                     
 ELSE IF (@MVR_ORDERED=10963 AND @MVR_STATUS IN ('C','V'))                  
  BEGIN                  
  SET @DRIVER_MVR_ORDERED='N'                    
  END          
 ELSE IF((@APP_MVR_ID IS NULL AND @MVR_ORDERED=10963) or (@APP_MVR_ID IS NOT NULL AND @MVR_ORDERED=10964))                                      
  BEGIN                                      
  SET @DRIVER_MVR_ORDERED='Y'                                      
  END                                
 ELSE                                         
 BEGIN                                      
 SET @DRIVER_MVR_ORDERED='N'                                     
 END   
END    
ELSE                                         
 BEGIN                                      
 SET @DRIVER_MVR_ORDERED='N'                                     
 END                             
---------------------Added till here----------------------------------------       
                                
/*Itrack NO.2933 --->Commented by Manoj Rathore 29 Nov 2007                 
                               
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
  AND  P.APP_VEHICLE_PRIN_OCC_ID IN (11929,11925,11398,11927))-- AND  D.MVR_ORDERED=0)                   
                       
    BEGIN                                 
    SET @DRV_WITHPOINTS='Y'                                
    END                    
  ELSE                   
    BEGIN                            
    SET @DRV_WITHPOINTS='N'                                
    END                                
                                
END */                
                                    
--End Comment                
-- There should be atleast one "Pricipal" or "Youthful Principal" driver for each vehicle added in application. Else Refer.                    
                    
DECLARE @YOUTHFUL_PRIN_DRIVER CHAR                    
SET @YOUTHFUL_PRIN_DRIVER='N'                    
--==============                
--Not Rated - 11931                
--11926 - Occasional - No Points Assigned                
--11925 - Occasional - Points Assigned                  
--11928 - Youthful Occasional - No Points Assigned                
--11927 - Youthful Occasional - Points Assigned                   
--==============                    
/* Commented By Pravesh on 27 Nov 08 - Rule Moved to Vehicle Level              
IF EXISTS(SELECT CUSTOMER_ID                   
--FROM APP_DRIVER_DETAILS                 
FROM APP_DRIVER_ASSIGNED_VEHICLE                    
WHERE CUSTOMER_ID=@CUSTOMERID AND  APP_ID=@APPID  AND  APP_VERSION_ID=@APPVERSIONID                    
AND APP_VEHICLE_PRIN_OCC_ID IN (11931,11925,11926,11927,11928)AND DRIVER_ID=@DRIVERID)                    
 BEGIN                     
  SET @YOUTHFUL_PRIN_DRIVER='Y'                    
 END                   
 */              
SET @RELATIONSHIP = -1   --Itrack 5666 as relationship is not a mandatory field                      
SELECT                                              
 @DRIVER_FNAME as  DRIVER_FNAME,                                                    
 @DRIVER_LNAME as DRIVER_LNAME,                                                    
 @DRIVER_CODE as DRIVER_CODE,                                                    
 @DRIVER_SEX as DRIVER_SEX,                    
 @DRIVER_ADD1 as DRIVER_ADD1,                          
 @DRIVER_CITY as DRIVER_CITY,                                                    
 @DRIVER_STATE as DRIVER_STATE,                                                     
 @DRIVER_ZIP as DRIVER_ZIP,                                                    
 @DRIVER_DOB as DRIVER_DOB,                                              
 @DRIVER_LIC_STATE as DRIVER_LIC_STATE,                                              
 @RELATIONSHIP as RELATIONSHIP,                                               
                  
 @DRIVER_US_CITIZEN as DRIVER_US_CITIZEN ,                                                
 -- Continous Driver Licence                                                
 @CONT_DRIVER_LICENSE as CONT_DRIVER_LICENSE,                                                
 @APP_EFFECTIVE_DATE as APP_EFFECTIVE_DATE,                                                
 @DATE_LICENSED as DATE_LICENSED,                                              
 @DRIVER_NAME as DRIVER_NAME,                                              
 @DRIVER_DRIV_LIC as DRIVER_DRIV_LIC,                                        
 --  @APP_VEHICLE_PRIN_OCC_ID as APP_VEHICLE_PRIN_OCC_ID,                                
 -- @PREFERRED_DISC as PREFERRED_DISC,                        
 @SD_POINTS as SD_POINTS ,                      
 @DRIVER_DRINK_VIOLATION as  DRIVER_DRINK_VIOLATION,                      
 @APP_DATEVS_LIC_DATE as APP_DATEVS_LIC_DATE,                      
 @COLLG_STD_CAR as COLLG_STD_CAR,                      
 @VIOLATION_POINT as VIOLATION_POINT ,  
--Added by Charles on 5-May-2009 for Itrack 5802  
 @DRIVER_MVR_ORDERED AS DRIVER_MVR_ORDERED,   
        
 @VIOLATIONS AS VIOLATIONS ,                  
 @VEHICLE_ID as  VEHICLE_ID,                
 @DRV_WITHPOINTS AS DRV_WITHPOINTS,                
 @DRV_WITHOUTPOINTS AS DRV_WITHOUTPOINTS,                
 @YOUTHFUL_PRIN_DRIVER as YOUTHFUL_PRIN_DRIVER,  
 @DRIVER_DRIV_TYPE as DRIVER_DRIV_TYPE                      
                                            
END   
  
  
  
  
  
  
  
  
  
GO

