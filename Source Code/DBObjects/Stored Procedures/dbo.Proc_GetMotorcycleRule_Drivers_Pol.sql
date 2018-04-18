IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMotorcycleRule_Drivers_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMotorcycleRule_Drivers_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/* ----------------------------------------------------------                                                                                
Proc Name                : Dbo.Proc_GetMotorcycleRule_Drivers_Pol                                                                
Created by               : Ashwani                                                                                
Date                     : 02 Mar. 2006                                                                    
Purpose                  : To get the Driver's  Policy Rule Information for Motorcycle                                                          
Revison History          :                                                                                
Used In                  : Wolverine                                                                      
Created by               : Sumit Chhabra                        
Date                     : 13 April 2006                                                                    
Purpose                  : Added rule for mvr violations accumulating 5 points during the last 3 years                        
------------------------------------------------------------                                                                                
Date     Review By          Comments                                                                                
------   ------------       -------------------------*/     

CREATE proc [dbo].[Proc_GetMotorcycleRule_Drivers_Pol]                                                                                  
(                                                                                  
@CUSTOMER_ID    int,                                                                                  
@POLICY_ID    int,                                                                                  
@POLICY_VERSION_ID   int,                                                                        
@DRIVER_ID int                                                                              
)                                                                                  
as                                                                   
begin                                                         
-- Driver Detail                                                                                  
--POL_DRIVER_DETAILS                                                                                  
 declare @DRIVER_FNAME nvarchar(75)                                                                                 
 declare @DRIVER_LNAME nvarchar(75)                                                                                 
 declare @DRIVER_CODE nvarchar(20)                                                                                  
 declare @DRIVER_SEX nchar(12)                                                         
 declare @DRIVER_ADD1 nvarchar(70)                                                        
 declare @DRIVER_CITY nvarchar(70)                                                        
 declare @DRIVER_STATE nvarchar(5)                                                                                
 declare @DRIVER_ZIP varchar(11)                                                         
 declare @DRIVER_DOB datetime                                                                                   
 declare @DRIVER_LIC_STATE nvarchar(5)                                                            
 declare @RELATIONSHIP int                                                        
 declare @VEHICLE_ID nvarchar(15)                                                                               
 declare @DRIVER_US_CITIZEN char                                                    
 declare @DRIVER_NAME nvarchar(225)                     
 declare @DRIVER_DRIV_LIC nvarchar(30)     
 declare @INTAPP_VEHICLE_PRIN_OCC_ID int                      
 declare @APP_VEHICLE_PRIN_OCC_ID char                        
 declare @DRIVER_DRINK_VIOLATION char                        
 declare @COLL_STUD_AWAY_HOME char                            
 declare @CYCL_WITH_YOU char                            
 declare @VIOLATIONS VARCHAR(5)                          
 DECLARE @intVIOLATIONS INT               
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
            
            
           
-------------END PRAVEEN KUMAR------------------            
                  
SET @DRIVER_DRINK_VIOLATION=''                     
IF EXISTS (SELECT CUSTOMER_ID FROM POL_DRIVER_DETAILS                                                       
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND DRIVER_ID=@DRIVER_ID)                                              
BEGIN                                                                                     
SELECT                                                         
 @DRIVER_FNAME=ISNULL(DRIVER_FNAME,''),@DRIVER_LNAME=ISNULL(DRIVER_LNAME,''),                                                        
 @DRIVER_CODE=ISNULL(DRIVER_CODE,''),@DRIVER_SEX=ISNULL(DRIVER_SEX,'') ,                                                        
 @DRIVER_ADD1=ISNULL(DRIVER_ADD1,''),@DRIVER_CITY=ISNULL(DRIVER_CITY,''),                                                        
 @DRIVER_STATE=ISNULL(DRIVER_STATE,''),@DRIVER_ZIP=ISNULL(DRIVER_ZIP,''),                                                        
 @DRIVER_DOB=ISNULL(DRIVER_DOB,''),@DRIVER_LIC_STATE=ISNULL(DRIVER_LIC_STATE,''),                                                        
 @RELATIONSHIP=ISNULL(RELATIONSHIP,0),@VEHICLE_ID=ISNULL(CONVERT(VARCHAR(15),VEHICLE_ID),''),@DRIVER_US_CITIZEN=ISNULL(DRIVER_US_CITIZEN,0),                                                  
 @DRIVER_NAME=(ISNULL(DRIVER_FNAME,'') + '  ' + ISNULL(DRIVER_MNAME,'') + '  ' + ISNULL(DRIVER_LNAME,'')),                                                  
 @DRIVER_DRIV_LIC=ISNULL(DRIVER_DRIV_LIC,''),@INTAPP_VEHICLE_PRIN_OCC_ID=ISNULL(APP_VEHICLE_PRIN_OCC_ID,-1),                                                   
 @DRIVER_DRINK_VIOLATION=ISNULL(DRIVER_DRINK_VIOLATION,''),@COLL_STUD_AWAY_HOME=ISNULL(COLL_STUD_AWAY_HOME,''),                            
 @CYCL_WITH_YOU=ISNULL(CYCL_WITH_YOU,'') ,@INTVIOLATIONS=VIOLATIONS ,@VIOLATIONS=ISNULL(VIOLATIONS,''),
 @DRIVER_DRIV_TYPE=isnull(DRIVER_DRIV_TYPE,'')                         
FROM POL_DRIVER_DETAILS                                                                                  
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND DRIVER_ID=@DRIVER_ID                                                                                  
END                                                                                   
ELSE                                                                           
BEGIN                                                  
 set @DRIVER_FNAME =''                    
 set @DRIVER_LNAME =''            
 set @DRIVER_CODE  =''                                                                 
 set @DRIVER_SEX  =''                                                  
 set @DRIVER_ADD1 =''                                                                 
 set @DRIVER_CITY  =''                               
 set @DRIVER_STATE =''                        
 set @DRIVER_ZIP =''                                                        
 set @DRIVER_DOB=''                                                                 
 set @DRIVER_LIC_STATE =''                                                                 
 set @RELATIONSHIP=0                                              
 set @VEHICLE_ID=''                                                        
 set @DRIVER_US_CITIZEN=''                                                  
 set @DRIVER_NAME=''                                     
 set @DRIVER_DRIV_LIC=''                                             
 set @APP_VEHICLE_PRIN_OCC_ID=''  
 set @DRIVER_DRIV_TYPE=''                                                 
                                                      
END                                                                                   
/* Driver/Household Member  Are you a college student?*  If yes and If yes in the  Field Do you have /cycle with you?*                            
Refer to underwriters */                            
declare @COLLG_STD_CAR char                            
set @COLLG_STD_CAR='N'                            
                            
if(@COLL_STUD_AWAY_HOME='1' and @CYCL_WITH_YOU='1')                            
 set @COLLG_STD_CAR='Y'                                                    
--                                                   
--if(@DRIVER_DRINK_VIOLATION='1')                                                                    
--begin                                              
--  set @DRIVER_DRINK_VIOLATION='Y'                            
--end                              
--else                            
-- set @DRIVER_DRINK_VIOLATION='N'                
            
---ADDED BY PRAVEEN KUMAR ITRACK 5118---            
IF(@DRIVER_DRINK_VIOLATION='1')                                                              
BEGIN                                                               
SET @DRIVER_DRINK_VIOLATION='Y'                                                              
END                        
ELSE IF(@DRIVER_DRINK_VIOLATION='0')            
BEGIN               
SET @DRIVER_DRINK_VIOLATION='N'            
END            
ELSE                                                 
SET @DRIVER_DRINK_VIOLATION=''            
            
----------------------------END PRAVEEN KUMAR---------------            
                                           
--                                            
IF(@INTAPP_VEHICLE_PRIN_OCC_ID=11399 OR @INTAPP_VEHICLE_PRIN_OCC_ID=11398)                                            
BEGIN                                             
SET @APP_VEHICLE_PRIN_OCC_ID='N'                                       
END                                             
ELSE                                            
BEGIN                                             
SET @APP_VEHICLE_PRIN_OCC_ID=''                                            
END                                                   
--                                             
IF(@DRIVER_US_CITIZEN='0')                                                                  
BEGIN                                                                   
SET @DRIVER_US_CITIZEN='Y'                                                    
END                                                                  
ELSE IF (@DRIVER_US_CITIZEN='1')                                              
BEGIN               
SET @DRIVER_US_CITIZEN='N'                                               
END                                                                 
----------------                                         
--  Licence Date                                                    
 declare @DATEAPP_EFFECTIVE_DATE datetime                                                                                
 declare @APP_EFFECTIVE_DATE char                           declare @DATEDATE_LICENSED datetime        
 declare @DATE_LICENSED char                                                                                
-- declare @DATEDATECONT_DRIVER_LICENSE int                                                                                
DECLARE @CONT_DRIVER_LICENSE  CHAR -- 2.B.6 2.B.7.B                          
DECLARE @STATE_ID VARCHAR(20)                                                                               
SELECT @DATEAPP_EFFECTIVE_DATE=APP_EFFECTIVE_DATE ,@STATE_ID=STATE_ID                                                                                
FROM POL_CUSTOMER_POLICY_LIST    WITH(NOLOCK)                                                                             
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                                 
                                                                        
                                                  
SELECT @DATEDATE_LICENSED=DATE_LICENSED                                                                                 
FROM POL_DRIVER_DETAILS                                                                                
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND DRIVER_ID=@DRIVER_ID                                                                                
                                                                        
                                                                            
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
                                                     
/* if(@DATEDATECONT_DRIVER_LICENSE <12)                                                              
 begin                                   
  set @CONT_DRIVER_LICENSE='Y'                                      
 end                                                                                
 else                                          
 begin                                                                                 
  set @CONT_DRIVER_LICENSE='N'                                                                            
 end                                        */                             
                   
IF(@DATEAPP_EFFECTIVE_DATE IS NULL)                            
BEGIN                  
SET @APP_EFFECTIVE_DATE=''           
END                            
ELSE                                                                                 
BEGIN                                    
 SET @APP_EFFECTIVE_DATE='N'                                                                                
END                                                               
                                                                   
IF(@DATEDATE_LICENSED IS NULL)             
BEGIN                                                      
SET @DATE_LICENSED=''                                                                                
END                                                                                
ELSE                                                                                 
BEGIN                                                      
SET @DATE_LICENSED='N'                                                                                 
END                                                       
                                                  
--               
            
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
	IF NOT EXISTS(SELECT PDD.DRIVER_ID FROM POL_VEHICLES PV INNER JOIN POL_DRIVER_DETAILS PDD
	ON  PV.CUSTOMER_ID = PDD.CUSTOMER_ID AND PV.POLICY_ID = PDD.POLICY_ID AND                                                    
	PV.POLICY_VERSION_ID = PDD.POLICY_VERSION_ID AND PV.IS_ACTIVE='Y'
	INNER JOIN POL_DRIVER_ASSIGNED_VEHICLE PDAV 
	ON PDD.CUSTOMER_ID = PDAV.CUSTOMER_ID AND PDD.POLICY_ID = PDAV.POLICY_ID AND                                                    
	PDD.POLICY_VERSION_ID = PDAV.POLICY_VERSION_ID 
	WHERE PDD.CUSTOMER_ID=@CUSTOMER_ID AND PDD.POLICY_ID=@POLICY_ID AND 
	PDD.POLICY_VERSION_ID=@POLICY_VERSION_ID AND PDD.DRIVER_ID=@DRIVER_ID)                                                                        
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
                                    
 declare @INTSD_PONITS int                                   
                     
 select @INTSD_PONITS=isnull(SUM(WOLVERINE_VIOLATIONS),0) from VIW_DRIVER_VIOLATIONS where VIOLATION_ID in                                          
 (                                          
   select VIOLATION_ID                                                
   from   POL_MVR_INFORMATION                                                   
   where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID  and DRIVER_ID=@DRIVER_ID                                    
   and IS_ACTIVE='Y'                                        
 )                                    
--2.                        
 declare @DATE_DATE_LICENSED datetime                                    
                                    
 select @DATE_DATE_LICENSED=DATE_LICENSED                        
 from POL_DRIVER_DETAILS                                       
 where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID  and DRIVER_ID=@DRIVER_ID                                    
-- 3.      
 declare @INTDATE int                                    
 select @INTDATE=datediff(month,@DATE_DATE_LICENSED,getdate())                            
--4.                                     
 declare @PREFERRED_DISC  char                                    
                                    
 if(@INTDATE<36 or @INTSD_PONITS>2)                                    
 begin                                    
 set @PREFERRED_DISC='Y'                                    
 end                
 else                                    
 begin                                     
 set @PREFERRED_DISC='N'                                   
 end                                     
-- 5.                                    
if(@PREFERRED_DISC='')                                    
begin                                     
 set @PREFERRED_DISC='N'                                    
end                                    
----------------------------------------------------------------------------------------                          
------------------------------------------------------------------------------------------                                                                        
--MVR Violation, Accumulation of over 5 points during the past 3 years                            
--Rule for ineligible  driving record.                     
--declare @IS_CONVICTED_ACCIDENT char                                                                    
--DECLARE @INT_CONVICTED_ACCIDENT INT                 
DECLARE @INT_CONVICTED_ACCIDENT char                 
                                                      
IF EXISTS(SELECT POLICY_ID  FROM POL_AUTO_GEN_INFO                                                                      
WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID)                          
BEGIN                                                                     
 SELECT @INT_CONVICTED_ACCIDENT=ISNULL(IS_CONVICTED_ACCIDENT,'')                                                          
 FROM POL_AUTO_GEN_INFO                                             
 WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID= @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID                                                                   
END                                                                    
ELSE       
BEGIN                                                                     
SET @INT_CONVICTED_ACCIDENT =''                                           
END                                                      
                                            
----SD Points Rule..                                  
                                                  
DECLARE @INTSD_POINTS INT                                    
DECLARE @SD_POINTS CHAR                                              
SET @SD_POINTS='N'                
/* Commented by Manoj Rathore on 18 oct 2007                                        
 SELECT @INTSD_POINTS=SUM(WOLVERINE_VIOLATIONS)                                                    
 FROM POL_MVR_INFORMATION                                                    
 INNER JOIN  VIW_DRIVER_VIOLATIONS  ON POL_MVR_INFORMATION.VIOLATION_ID = VIW_DRIVER_VIOLATIONS.VIOLATION_ID                                                    
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND DRIVER_ID=@DRIVER_ID                          
 AND (YEAR(GETDATE())- YEAR(MVR_DATE))<3 AND POL_MVR_INFORMATION.IS_ACTIVE='Y' -- AND  @INT_CONVICTED_ACCIDENT=1                 
 IF(@INTSD_POINTS > 5)                                                                        
 BEGIN                                                   
 SET @SD_POINTS='Y'                                                                        
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
	IF(@DRIVER_LIC_STATE='0' OR @DRIVER_LIC_STATE IS NULL)                
	BEGIN                
	SET @DRIVER_LIC_STATE=''                
	END 
              
	IF(@STATE_ID<>@DRIVER_LIC_STATE)                            
	BEGIN                             
	SET @APP_DATEVS_LIC_DATE='Y'                  
	END
END                             
-----------------------------------------------------------------------                 
/* Commented by Manoj Rathore on 18 oct 2007                 
                 
 IF EXISTS( SELECT MVR_POINTS FROM POL_MVR_INFORMATION                                                      
 INNER JOIN  MNT_VIOLATIONS  ON POL_MVR_INFORMATION.VIOLATION_ID = MNT_VIOLATIONS.VIOLATION_ID                                                      
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID   AND DRIVER_ID=@DRIVER_ID                             
 AND (MVR_POINTS=8 OR MVR_POINTS<0) AND ((ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(MVR_DATE),0))<=5 AND                                      
 (ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(MVR_DATE),0))>=0)                                      
 AND POL_MVR_INFORMATION.IS_ACTIVE='Y')                            
 BEGIN                             
 SET @VIOLATION_POINT='Y'                            
 END                   
*/        
----------------------MISC POINTS Itrack # 5081 Manoj Rathore -----------------      
                              
DECLARE @SUMOFMISCPOINTS int                            
DECLARE @LICUNDER3YRS VARCHAR(5)        
SET @SUMOFMISCPOINTS = 0         
SELECT  @LICUNDER3YRS  = CASE                  
 WHEN DATEDIFF(DAY, CONVERT(CHAR(10),adds.DATE_LICENSED,101), @DATEAPP_EFFECTIVE_DATE)/365 >= 3 THEN 'N' ELSE 'Y'  END        
 FROM                             
Pol_DRIVER_DETAILS adds WITH (NOLOCK)         
           
WHERE                                           
adds.CUSTOMER_ID=@CUSTOMER_ID AND adds.POLICY_ID=@POLICY_ID AND adds.POLICY_VERSION_ID=@POLICY_VERSION_ID         
AND adds.DRIVER_ID=@DRIVER_ID          
        
         
IF (@LICUNDER3YRS = 'Y')        
BEGIN  
 SET @SUMOFMISCPOINTS     = 3                            
END        
    
                        
/* Driver/Household Member MVR tab If there is any violation with a point count of (8)Refer to underwriters */                            
  DECLARE @VIOLATION_POINT CHAR       
  SET  @VIOLATION_POINT='N'                
-- Check Major Violation With in 5 years 
IF(@DRIVER_DRIV_TYPE='11941') --Added by Manoj Rathore Itrack # 5847
BEGIN               
	 DECLARE @MAJOR_VIOLATION INT                 
	 SELECT @MAJOR_VIOLATION=ISNULL(SUM(ISNULL(POINTS_ASSIGNED,0))+ SUM(ISNULL(ADJUST_VIOLATION_POINTS,0)),0)                 
	 FROM POL_MVR_INFORMATION                 
	 INNER JOIN  VIW_DRIVER_VIOLATIONS  ON POL_MVR_INFORMATION.VIOLATION_TYPE = VIW_DRIVER_VIOLATIONS.VIOLATION_ID                                                    
	 WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND DRIVER_ID=@DRIVER_ID                                                                                      
	 AND VIOLATION_CODE IN('10000','40000','SUSPN')                 
	 AND DATEDIFF(DAY,MVR_DATE,@DATEAPP_EFFECTIVE_DATE)<= 5*365.5                 
	 AND DATEDIFF(DAY,MVR_DATE,@DATEAPP_EFFECTIVE_DATE)>= 0                
	 AND POL_MVR_INFORMATION.IS_ACTIVE='Y'                    
                
-- Check Minor Violation With in 3 years                
	DECLARE @MINOR_VIOLATION INT                 
	SELECT @MINOR_VIOLATION=ISNULL(SUM(ISNULL(POINTS_ASSIGNED,0))+ SUM(ISNULL(ADJUST_VIOLATION_POINTS,0)),0)                 
	FROM POL_MVR_INFORMATION                 
	INNER JOIN  VIW_DRIVER_VIOLATIONS  ON POL_MVR_INFORMATION.VIOLATION_TYPE = VIW_DRIVER_VIOLATIONS.VIOLATION_ID                                          
	WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND DRIVER_ID=@DRIVER_ID                   
	AND DATEDIFF(DAY,MVR_DATE,@DATEAPP_EFFECTIVE_DATE)<= 3*365.25                 
	AND DATEDIFF(DAY,MVR_DATE,@DATEAPP_EFFECTIVE_DATE)>= 0                
	AND VIOLATION_CODE NOT IN('10000','40000','SUSPN')                
	AND POL_MVR_INFORMATION.IS_ACTIVE='Y'                     
                   
	 -- Sum of Major and Minor violation greater than 5 refer to underwriter                
	 IF((@MAJOR_VIOLATION + @MINOR_VIOLATION + @SUMOFMISCPOINTS) > 5 or (@MAJOR_VIOLATION + @MINOR_VIOLATION + @SUMOFMISCPOINTS)< 0)                     
	 BEGIN                       
	 SET @VIOLATION_POINT='Y'                      
	 END             
            
-- ACCIDENT POINTS ACCUMULATION            
	DECLARE @ACCIDENT_POINTS INT       
	--added by Manoj Rathore  Itrack # 5081        
	DECLARE @MISCPOINTS INT                
	CREATE TABLE #DRIVER_VIOLATION_ACCIDENT                  
	(                  
	 [SUM_MVR_POINTS]  INT,                                                              
	 [ACCIDENT_POINTS]  INT,                                                              
	 [COUNT_ACCIDENTS]  INT,                  
	 [MVR_POINTS]  INT ,        
	 [SUMOFMISCPOINTS] INT                  
	                                 
	)                  
	                 
	INSERT INTO #DRIVER_VIOLATION_ACCIDENT exec GetMotorMVRViolationPointsPol @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID ,@DRIVER_ID,@ACCIDENTREFERYEAR,@VIOLATIONNUMYEAR,@VIOLATIONREFERYEAR,@ACCIDENTCHARGES                             
	SELECT @ACCIDENT_POINTS = ACCIDENT_POINTS,@MISCPOINTS=SUMOFMISCPOINTS FROM #DRIVER_VIOLATION_ACCIDENT                
	IF(@ACCIDENT_POINTS IS NULL)                  
	SET @ACCIDENT_POINTS =0       
	--added by Manoj Rathore  Itrack # 5081        
	IF(@MISCPOINTS IS NULL)                
	SET @MISCPOINTS =0                
	DROP TABLE  #DRIVER_VIOLATION_ACCIDENT                 
	IF( (@MAJOR_VIOLATION + @MINOR_VIOLATION + @ACCIDENT_POINTS + @MISCPOINTS) > 5 or (@MAJOR_VIOLATION + @MINOR_VIOLATION + @ACCIDENT_POINTS + @MISCPOINTS) <0)                         
	BEGIN                                  
	SET @VIOLATION_POINT='Y'      
	END  
END              
------------------------------------------------------------------------------------------                                      
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
            
------------------Added by Charles on 5-May-09 for Itrack 5802------------------------------------------

DECLARE @POL_MVR_ID INT                                     
DECLARE @MVR_ORDERED INT                                     
DECLARE @DRIVER_MVR_ORDERED CHAR                
DECLARE @MVR_STATUS CHAR(1)          
SELECT @POL_MVR_ID=ISNULL(POL_MVR_ID,'0') FROM POL_MVR_INFORMATION                                     
 WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and DRIVER_ID=@DRIVER_ID and IS_ACTIVE='Y'                                    
SELECT @MVR_ORDERED=ISNULL(MVR_ORDERED,'0'),@MVR_STATUS=MVR_STATUS FROM POL_DRIVER_DETAILS                                     
WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and DRIVER_ID=@DRIVER_ID and IS_ACTIVE='Y'             
            
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
	ELSE IF((@POL_MVR_ID IS NULL AND @MVR_ORDERED=10963) or (@POL_MVR_ID IS NOT NULL AND @MVR_ORDERED=10964))                                    
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
                                    
 /*Itrack No---> 2933 Comment By Manoj Rathore                
                                   
 IF EXISTS (SELECT MVR_POINTS FROM POL_MVR_INFORMATION                                      
 INNER JOIN  MNT_VIOLATIONS  ON POL_MVR_INFORMATION.VIOLATION_ID = MNT_VIOLATIONS.VIOLATION_ID                                                              
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND DRIVER_ID=@DRIVER_ID                    
 AND POL_MVR_INFORMATION.IS_ACTIVE='Y' AND MVR_POINTS>0)                                    
                                     
 BEGIN                                     
    -- MVR PTS EXISTS & ANY DRIVER WITH NO POINTS- REFER                                    
   IF EXISTS (SELECT P.APP_VEHICLE_PRIN_OCC_ID FROM POL_DRIVER_DETAILS D                                    
   INNER JOIN POL_DRIVER_ASSIGNED_VEHICLE P                                     
   ON D.CUSTOMER_ID=P.CUSTOMER_ID AND D.POLICY_ID=P.POLICY_ID AND D.POLICY_VERSION_ID=P.POLICY_VERSION_ID AND D.DRIVER_ID=P.DRIVER_ID                                    
   WHERE D.CUSTOMER_ID=@CUSTOMER_ID AND D.POLICY_ID=@POLICY_ID AND D.POLICY_VERSION_ID=@POLICY_VERSION_ID AND D.DRIVER_ID=@DRIVER_ID                                    
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
   IF EXISTS (SELECT P.APP_VEHICLE_PRIN_OCC_ID FROM POL_DRIVER_DETAILS D                                    
   INNER JOIN POL_DRIVER_ASSIGNED_VEHICLE P                                     
   ON D.CUSTOMER_ID=P.CUSTOMER_ID AND D.POLICY_ID=P.POLICY_ID AND D.POLICY_VERSION_ID=P.POLICY_VERSION_ID AND D.DRIVER_ID=P.DRIVER_ID                                    
   WHERE  D.CUSTOMER_ID=@CUSTOMER_ID AND D.POLICY_ID=@POLICY_ID AND D.POLICY_VERSION_ID=@POLICY_VERSION_ID AND D.DRIVER_ID=@DRIVER_ID                        
   AND  P.APP_VEHICLE_PRIN_OCC_ID IN (11929,11925,11398,11927))-- AND  D.MVR_ORDERED=0)                     
                          
      BEGIN                                     
      SET @DRV_WITHPOINTS='Y'                                    
      END                        
   ELSE                       
      BEGIN                                     
      SET @DRV_WITHPOINTS='N'                                    
      END                                    
                                    
END  */                                      
                    
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
/* Commented by Pravesh on 27 Nov 08 - rule Moved to Vehicle Level              
IF EXISTS(SELECT CUSTOMER_ID                     
--FROM POL_DRIVER_DETAILS                 
FROM POL_DRIVER_ASSIGNED_VEHICLE                    
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID                  
AND APP_VEHICLE_PRIN_OCC_ID IN (11931,11925,11926,11927,11928)AND DRIVER_ID=@DRIVER_ID)                    
 BEGIN                     
  SET @YOUTHFUL_PRIN_DRIVER='Y'                    
 END                 
*/              
            
            
 SET @RELATIONSHIP = -1 --Itrack 5666 as relationship is not a mandatory field           
                
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
  @VEHICLE_ID as  VEHICLE_ID,            
  @DRIVER_US_CITIZEN as DRIVER_US_CITIZEN ,                                                    
 -- Continous Driver Licence                                                    
  @CONT_DRIVER_LICENSE as CONT_DRIVER_LICENSE,                   
  @APP_EFFECTIVE_DATE as APP_EFFECTIVE_DATE,                            
  @DATE_LICENSED as DATE_LICENSED,                                                  
  @DRIVER_NAME as DRIVER_NAME,                                                  
  @DRIVER_DRIV_LIC as DRIVER_DRIV_LIC,                                            
 -- @APP_VEHICLE_PRIN_OCC_ID as APP_VEHICLE_PRIN_OCC_ID,                                
 -- @PREFERRED_DISC as PREFERRED_DISC,                          
  @SD_POINTS AS SD_POINTS ,                        
  @DRIVER_DRINK_VIOLATION as  DRIVER_DRINK_VIOLATION,                         
  @APP_DATEVS_LIC_DATE as APP_DATEVS_LIC_DATE,                        
  @COLLG_STD_CAR as COLLG_STD_CAR,                                                                       
  @VIOLATION_POINT as VIOLATION_POINT ,                          
  @VIOLATIONS AS VIOLATIONS , 
  --Added by Charles on 5-May-2009 for Itrack 5802
  @DRIVER_MVR_ORDERED AS DRIVER_MVR_ORDERED, 
                   
  @DRV_WITHPOINTS AS DRV_WITHPOINTS,                    
  @DRV_WITHOUTPOINTS AS DRV_WITHOUTPOINTS ,                
  @YOUTHFUL_PRIN_DRIVER AS YOUTHFUL_PRIN_DRIVER ,
  @DRIVER_DRIV_TYPE AS DRIVER_DRIV_TYPE

                                                 
END                
            
            
                
                
                
                  















GO

