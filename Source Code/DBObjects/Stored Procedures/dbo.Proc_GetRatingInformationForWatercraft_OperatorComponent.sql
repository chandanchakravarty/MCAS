IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRatingInformationForWatercraft_OperatorComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRatingInformationForWatercraft_OperatorComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[Proc_GetRatingInformationForWatercraft_OperatorComponent]                              
(                              
@CUSTOMERID    int,                              
@APPID    int,                              
@APPVERSIONID   int,                              
@DRIVERID   int                              
)                              
AS                              
BEGIN                              
 set quoted_identifier off                              
                              
DECLARE @OPERATORFNAME    		varchar(100)                              
DECLARE @OPERATORMNAME    		varchar(100)                              
DECLARE @OPERATORLNAME    		varchar(100)                              
DECLARE @BIRTHDATE    			varchar(100)                              
DECLARE @GENDER       			varchar(100)                              
DECLARE @MARITALSTATUS    		varchar(100)                              
DECLARE @YEARSLICENSED    		varchar(100)                              
DECLARE @POWERSQUADRONCOURSE   		varchar(100)                              
DECLARE @COASTGUARDAUXILARYCOURSE  	varchar(100)                              
DECLARE @HAS_5_YEARSOPERATOREXPERIENCE  varchar(100)                              
DECLARE @BOATASSIGNEDASOPERATOR   	varchar(100)                              
DECLARE @BOATDRIVEDAS    		varchar(100)                              
DECLARE @MVR     			varchar(100)                              
DECLARE @SUMOFVIOLATIONPOINTS   	varchar(100)                              
DECLARE @SUMOFACCIDENTPOINTS   		varchar(100)                              
DECLARE @AGEOFDRIVER    		varchar(100)                              
DECLARE @DRIVERCLASS    		varchar(100)                              
DECLARE @DRIVERCLASSCOMPONENT1   	varchar(100)                              
DECLARE @DRIVERCLASSCOMPONENT2    	varchar(100)                              
DECLARE @VIOLATIONS    			varchar(100)                              
DECLARE @DRIVER_COST_GAURAD_AUX   	int                        
DECLARE @DRIVER_CODE   			varchar(100)          
DECLARE @APPEFFECTIVEDATE 		datetime                   
--Added 12 May 2006          
DECLARE @WAT_SAFETY_COURSE  		varchar(12)          
DECLARE @CERT_COAST_GUARD  		varchar(12)          
DECLARE @DRIVER_DOB  			datetime          
DECLARE @EFFECTIVE_DATE 		varchar(20)          
                
-- START                              
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 





SELECT                              
  @OPERATORFNAME   = ISNULL(DRIVER_FNAME,''),                              
  @OPERATORMNAME   = ISNULL(DRIVER_MNAME,''),                              
  @OPERATORLNAME   = ISNULL(DRIVER_LNAME,'') ,                              
  @BIRTHDATE   = convert(varchar(10),DRIVER_DOB,101),                              
  @DRIVER_COST_GAURAD_AUX  = ISNULL(DRIVER_COST_GAURAD_AUX ,0),          
  --Added new 12 May 2006 10964 (N)          
  @WAT_SAFETY_COURSE = ISNULL(WAT_SAFETY_COURSE ,'10964'),          
  @CERT_COAST_GUARD  = ISNULL(CERT_COAST_GUARD ,'10964'),          
  @DRIVER_DOB = DRIVER_DOB          
FROM APP_WATERCRAFT_DRIVER_DETAILS WITH (NOLOCK)                             
WHERE CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                         
AND DRIVER_ID=@DRIVERID                        

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------




 
SELECT @APPEFFECTIVEDATE = convert(char(10),APP_EFFECTIVE_DATE,101)    
FROM APP_LIST  WITH (NOLOCK)         
WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID        
                            
IF (@DRIVER_COST_GAURAD_AUX IS NULL)                              
	BEGIN                               
	 SET @DRIVER_COST_GAURAD_AUX=0                              
	END                               
--Added 12 May --Water Safety          
IF(@WAT_SAFETY_COURSE IS NULL)          
	BEGIN                               
	 SET @WAT_SAFETY_COURSE=''                              
	END            
--Added 12 May --Coast Guard          
IF(@CERT_COAST_GUARD  IS NULL)          
	BEGIN                               
	 SET @CERT_COAST_GUARD=''                              
	END            
IF(@BIRTHDATE IS NULL)                              
	BEGIN                               
	 SET @BIRTHDATE='0'                              
	END                              
IF(@OPERATORLNAME IS NULL)                              
	BEGIN                               
	 SET @OPERATORLNAME=''                              
	END                               
IF(@OPERATORMNAME IS NULL)                              
	BEGIN                               
	 SET @OPERATORMNAME=''                              
	END                             
IF(@OPERATORFNAME IS NULL)                              
	BEGIN                               
	 SET @OPERATORFNAME=''                              
	END                               
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

 


SELECT                               
 @GENDER = isnull(rtrim(ltrim(DRIVER_SEX)),'M'),            
 @DRIVER_CODE  =ISNULL(DRIVER_CODE,'')            
FROM APP_WATERCRAFT_DRIVER_DETAILS WITH (NOLOCK)                               
WHERE CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID 
AND DRIVER_ID = @DRIVERID                               

IF(@GENDER='M')                    
	BEGIN                     
	 SET @GENDER='MALE'                    
	END                     
ELSE IF(@GENDER='F')                    
	BEGIN                     
	 SET @GENDER='FEMALE'                    
	END 
ELSE IF(@GENDER IS NULL)                    
	BEGIN                     
	 SET @GENDER=''                    
	END                               
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 




SELECT 
 @BOATASSIGNEDASOPERATOR = ISNULL(BOAT_ID,0)                             
FROM APP_OPERATOR_ASSIGNED_BOAT WITH (NOLOCK)                             
WHERE CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND DRIVER_ID = @DRIVERID   AND APP_VEHICLE_PRIN_OCC_ID = 11936  
IF( @BOATASSIGNEDASOPERATOR IS NULL)
BEGIN 
SELECT 
 @BOATASSIGNEDASOPERATOR = ISNULL(BOAT_ID,0)                             
FROM APP_OPERATOR_ASSIGNED_BOAT WITH (NOLOCK)                             
WHERE CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID  AND DRIVER_ID = @DRIVERID
END                                
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------





-- MARITAL-STATUS                                
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 




SET @MARITALSTATUS = ''                              
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 




-- YEARS LICENSED                                
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


 


SELECT                           
 @YEARSLICENSED=YEARS_LICENSED                          
FROM APP_WATERCRAFT_DRIVER_DETAILS WITH (NOLOCK)                          
WHERE CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID 
AND DRIVER_ID = @DRIVERID                             
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 




--POWER SQUADRON COURSE, COAST GUARD AUXILARY COURSE AND  HAS_5_YEARSOPERATOREXPERIENCE START                              
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 




/*IF(@DRIVER_COST_GAURAD_AUX IS NULL OR @DRIVER_COST_GAURAD_AUX = 0 OR  @DRIVER_COST_GAURAD_AUX='')                              
 BEGIN                               
  SET @POWERSQUADRONCOURSE  = 'N'                              
  SET @COASTGUARDAUXILARYCOURSE  = 'N'                              
  SET @HAS_5_YEARSOPERATOREXPERIENCE = 'N'                              
 END                              
 ELSE                              
 BEGIN                              
  SET @POWERSQUADRONCOURSE  = 'Y'                              
  SET @COASTGUARDAUXILARYCOURSE  = 'Y'                              
  SET @HAS_5_YEARSOPERATOREXPERIENCE = 'Y'                              
 END */          
-------------------------------------------Question UQ-------12 May 2006--------          
if(@WAT_SAFETY_COURSE IS NULL or @WAT_SAFETY_COURSE = '10964')          
	BEGIN          
	 SET @POWERSQUADRONCOURSE  = 'N'              
	END          
ELSE          
	BEGIN          
	 SET @POWERSQUADRONCOURSE  = 'Y'              
	END          
--------Do you have a Certificate for Coast guard or Power Squadron Course ?                         
if(@CERT_COAST_GUARD IS NULL or @CERT_COAST_GUARD =0 or @CERT_COAST_GUARD='' or @CERT_COAST_GUARD = '10964')          
	BEGIN          
	 SET @COASTGUARDAUXILARYCOURSE  = 'N'              
	END          
ELSE          
	BEGIN          
	 SET @COASTGUARDAUXILARYCOURSE  = 'Y'              
	END          
-----------Boating Experience Since (YYYY) Field ..           
SELECT 
 @EFFECTIVE_DATE = convert(varchar(20),APP_EFFECTIVE_DATE,109) FROM APP_LIST           
WHERE CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID           
print YEAR(@EFFECTIVE_DATE)        
IF((YEAR(@EFFECTIVE_DATE) - @DRIVER_COST_GAURAD_AUX )>5 AND @DRIVER_COST_GAURAD_AUX <> 0 AND @DRIVER_COST_GAURAD_AUX IS NOT NULL )          
	BEGIN         
	 SET @HAS_5_YEARSOPERATOREXPERIENCE  = 'Y'              
	END          
ELSE          
	BEGIN          
	 SET @HAS_5_YEARSOPERATOREXPERIENCE  = 'N'          
	END     
--END Questions
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------




 
IF @BIRTHDATE IS NOT NULL                               
	 BEGIN                          
	  SET @AGEOFDRIVER=FLOOR(DATEDIFF(MONTH,@BIRTHDATE,@APPEFFECTIVEDATE)/12.00)                          
	 END                          
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  




SELECT
 @BOATDRIVEDAS=MNT.LOOKUP_VALUE_DESC 
FROM APP_OPERATOR_ASSIGNED_BOAT  APP  WITH (NOLOCK)              
INNER JOIN MNT_LOOKUP_VALUES MNT  WITH (NOLOCK)
ON APP.APP_VEHICLE_PRIN_OCC_ID= MNT.LOOKUP_UNIQUE_ID                
WHERE  CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID 
AND DRIVER_ID = @DRIVERID AND APP_VEHICLE_PRIN_OCC_ID = 11936  
IF(@BOATDRIVEDAS IS NULL)
BEGIN
SELECT
 @BOATDRIVEDAS=MNT.LOOKUP_VALUE_DESC 
FROM APP_OPERATOR_ASSIGNED_BOAT  APP  WITH (NOLOCK)              
INNER JOIN MNT_LOOKUP_VALUES MNT  WITH (NOLOCK)
ON APP.APP_VEHICLE_PRIN_OCC_ID= MNT.LOOKUP_UNIQUE_ID                
WHERE  CUSTOMER_ID =@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID 
AND DRIVER_ID = @DRIVERID
END                               
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 




/* Get The @SUMOFACCIDENTPOINTS EQUAL TO Sum of all Accidents points in previous 3 yrs */                  
SELECT 
 @MVR= SUM(MV.MVR_POINTS)                                              
FROM MNT_VIOLATIONS MV  WITH (NOLOCK)                                   
INNER JOIN APP_WATER_MVR_INFORMATION AMI WITH (NOLOCK)
ON MV.VIOLATION_ID=AMI.VIOLATION_ID                                            
WHERE  AMI.CUSTOMER_ID =@CUSTOMERID AND AMI.APP_ID=@APPID  AND AMI.APP_VERSION_ID=@APPVERSIONID  
AND AMI.DRIVER_ID = @DRIVERID AND  VIOLATION_TYPE IN(2100,2558)                        
And  AMI.MVR_DATE >=(DATEADD(YEAR,-3,@APPEFFECTIVEDATE))
IF  @MVR IS NULL                              
	BEGIN
	  SET @SUMOFACCIDENTPOINTS=0                              
	END
ELSE                              
	BEGIN
	  SET @SUMOFACCIDENTPOINTS=@MVR           
	END                              
                               
/* Get The @SUMOFVIOLATIONPOINTS EQUAL TO Sum of all MVR points in previous 2 yrs OTHER THAN ACCIDENT POINTS  */                                
   /*SELECT @MVR= SUM(MV.MVR_POINTS)                                              
                FROM MNT_VIOLATIONS MV WITH (NOLOCK)                            
                INNER JOIN APP_MVR_INFORMATION AMI  WITH (NOLOCK) ON MV.VIOLATION_ID=AMI.VIOLATION_ID                                            
                WHERE  AMI.CUSTOMER_ID =@CUSTOMERID AND AMI.APP_ID=@APPID  AND AMI.APP_VERSION_ID=@APPVERSIONID AND AMI.DRIVER_ID = @DRIVERID                                    
   AND VIOLATION_TYPE NOT IN(2100,2558)                             
                And  AMI.MVR_DATE >=(DATEADD(YEAR,-2,@APPEFFECTIVEDATE))--  AND AMI.MVR_DATE <= @APPEFFECTIVEDATE )                              
                                 
   IF  @MVR IS NULL                              
       SET @SUMOFVIOLATIONPOINTS=0                              
   ELSE                              
       SET @SUMOFVIOLATIONPOINTS=@MVR                               
                                       
IF(@MVR IS NULL)                                          
 BEGIN                                          
   SET @MVR = ''                                 
 END */                                         
-- Get The @SUMOFVIOLATIONPOINTS EQUAL TO Sum of all MVR points in previous 2 yrs OTHER THAN ACCIDENT POINTS   
SELECT
@MVR= SUM(MV.MVR_POINTS)                                              
FROM MNT_VIOLATIONS MV WITH (NOLOCK)                            
INNER JOIN APP_WATER_MVR_INFORMATION AMI  WITH (NOLOCK) ON MV.VIOLATION_ID=AMI.VIOLATION_ID                                            
WHERE  AMI.CUSTOMER_ID =@CUSTOMERID AND AMI.APP_ID=@APPID  AND AMI.APP_VERSION_ID=@APPVERSIONID 
AND AMI.DRIVER_ID = @DRIVERID AND VIOLATION_TYPE NOT IN(2100,2558)                             
And  AMI.MVR_DATE >=(DATEADD(YEAR,-2,@APPEFFECTIVEDATE))
                                 
IF  @MVR IS NULL                              
	BEGIN
	 SET @SUMOFVIOLATIONPOINTS=0             
	END
ELSE                              
	BEGIN
	 SET @SUMOFVIOLATIONPOINTS=@MVR                               
	END                 
IF(@MVR IS NULL)                                          
	BEGIN                                          
	 SET @MVR = ''                                 
	END  
--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------






/*                          
SET @BOATDRIVEDAS   = ''                              
SET @MVR    = ''                              
SET @SUMOFVIOLATIONPOINTS  = ''                           
SET @SUMOFACCIDENTPOINTS  = ''                              
SET @AGEOFDRIVER   = ''                              
SET @DRIVERCLASS   = ''                              
SET @DRIVERCLASSCOMPONENT1  = ''                              
SET @DRIVERCLASSCOMPONENT2   = ''                              
SET @VIOLATIONS    = ''                              
SET @BOATASSIGNEDASOPERATOR  =  ''                              
*/                          
-------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 




SELECT                              
 @OPERATORFNAME     		AS OPERATORFNAME,                              
 @OPERATORMNAME     		AS OPERATORMNAME,                              
 @OPERATORLNAME     		AS OPERATORLNAME,                            
 @BIRTHDATE     		AS BIRTHDATE,                              
 @GENDER      			AS GENDER,                              
 @MARITALSTATUS     		AS MARITALSTATUS,                              
 @YEARSLICENSED     		AS YEARSLICENSED,                              
 @POWERSQUADRONCOURSE    	AS POWERSQUADRONCOURSE,                              
 @COASTGUARDAUXILARYCOURSE   	AS COASTGUARDAUXILARYCOURSE,                              
 @HAS_5_YEARSOPERATOREXPERIENCE AS HAS_5_YEARSOPERATOREXPERIENCE,                              
 @BOATASSIGNEDASOPERATOR    	AS BOATASSIGNEDASOPERATOR,                              
 @BOATDRIVEDAS     		AS BOATDRIVEDAS,                              
 --@MVR      			AS MVR,                              
 --@SUMOFVIOLATIONPOINTS    	AS SUMOFVIOLATIONPOINTS,                              
 --@SUMOFACCIDENTPOINTS    	AS SUMOFACCIDENTPOINTS,                              
 @AGEOFDRIVER     		AS AGEOFDRIVER,                              
 @DRIVERCLASS     		AS DRIVERCLASS,                              
 @DRIVERCLASSCOMPONENT1    	AS DRIVERCLASSCOMPONENT1,                              
 @DRIVERCLASSCOMPONENT2     	AS DRIVERCLASSCOMPONENT2 ,                              
 --@VIOLATIONS     		AS VIOLATIONS,            
 @DRIVER_CODE    		AS DRIVER_CODE                              
END                              




                                   


GO

