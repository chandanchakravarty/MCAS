IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetRatingInformationForAuto_DriverComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetRatingInformationForAuto_DriverComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                              
Proc Name           : Dbo.Proc_GetRatingInformationForAuto_DriverComponent 936,205,1,1                                             
Created by            : Nidhi.                                              
Date                     : 04/10/2005                                              
Purpose             : To get the information for creating the input xml                                                
Revison History     :                                              
Used In                 :   Creating InputXML for vehicle                                              
------------------------------------------------------------                                              
Date     Review By          Comments                                              
------   ------------       -------------------------*/                                              
--    drop proc Proc_GetRatingInformationForAuto_DriverComponent                           
CREATE PROC [dbo].[Proc_GetRatingInformationForAuto_DriverComponent]                                             
(                                              
@CUSTOMERID    int,                                              
@APPID    int,                                              
@APPVERSIONID   int,                                              
@DRIVERID    int                                              
)                                              
AS                                              
                                              
BEGIN                                              
 set quoted_identifier off                                              
                                              
DECLARE    @DRIVERFNAME    nvarchar(100)                                              
DECLARE    @DRIVERMNAME    nvarchar(100)                                              
DECLARE    @DRIVERLNAME    nvarchar(100)                                              
 --DECLARE    @SUMOFACCIDENTPOINTS    nvarchar(100)                                              
 --DECLARE    @SUMOFVIOLATIONPOINTS    nvarchar(100)                                              
DECLARE    @BIRTHDATE    nvarchar(100)                                              
DECLARE    @GENDER    nvarchar(100)                                              
DECLARE    @DRIVERCLASS    nvarchar(100)                                              
DECLARE    @MARITALSTATUS    nvarchar(100)                                              
DECLARE    @DRIVERCLASSCOMPONENT1    nvarchar(100)                                              
DECLARE    @DRIVERCLASSCOMPONENT2    nvarchar(100)                                             
DECLARE    @DRIVERINCOME    nvarchar(100)                                              
DECLARE    @NODEPENDENT    nvarchar(100)                                              
DECLARE    @DRIVERLIC    nvarchar(100)                                              
DECLARE    @WAIVEWORKLOSS    nvarchar(100)                                              
DECLARE    @NOPREMDRIVERDISC    nvarchar(100)                                              
DECLARE    @COLLEGESTUDENT    nvarchar(100)                                              
DECLARE    @GOODSTUDENT    nvarchar(100)                                              
DECLARE    @DISTANTSTUDENT    nvarchar(100)                                              
DECLARE    @VEHICLEASSIGNEDASOPERATOR    nvarchar(100)                                              
DECLARE    @VEHICLEDRIVEDAS    nvarchar(100)                                              
 --DECLARE    @MVR    nvarchar(100)                                              
DECLARE    @AGEOFDRIVER    nvarchar(100)                                
--ADDED BY SHAFI FOR NO OF driver's license YEARS                            
DECLARE  @LICUNDER3YRS       nvarchar(100)               
DECLARE    @DRIVERCODE          nvarchar(20)                            
DECLARE @SAFEDRIVERDISCOUNT          nvarchar(20)
DECLARE    @VEHICLEDRIVEDASCODE NVARCHAR(20)                                      
                                              
/*                               
            <DRIVERFNAME>PEHLA </DRIVERFNAME>                     
            <DRIVERMNAME>                           
            </DRIVERMNAME>                                              
            <DRIVERLNAME>DRIVER</DRIVERLNAME>                                              
            <SUMOFACCIDENTPOINTS>5</SUMOFACCIDENTPOINTS>                 
            <SUMOFVIOLATIONPOINTS>2</SUMOFVIOLATIONPOINTS>                                              
            <BIRTHDATE>02/03/1978</BIRTHDATE>                                              
            <GENDER>Male</GENDER>                  
            <DRIVERCLASS>A</DRIVERCLASS>                                   
            <MARITALSTATUS>Married</MARITALSTATUS>                                              
            <DRIVERCLASS1>1</DRIVERCLASS1>                                              
            <DRIVERINCOME>HIGH</DRIVERINCOME>                                              
            <NODEPENDENT>2</NODEPENDENT>                                              
  <DRIVERLIC>1234</DRIVERLIC>                                              
            <WAIVEWORKLOSS>FALSE</WAIVEWORKLOSS>                                              
            <NOPREMDRIVERDISC>TRUE</NOPREMDRIVERDISC>                                   
            <COLLEGESTUDENT>FALSE</COLLEGESTUDENT>                                              
            <GOODSTUDENT>FALSE</GOODSTUDENT>                                              
            <DISTANTSTUDENT>TRUE</DISTANTSTUDENT>                                              
            <VEHICLEASSIGNEDASOPERATOR>1</VEHICLEASSIGNEDASOPERATOR>                                       
            <VEHICLEDRIVEDAS>OCCASIONAL</VEHICLEDRIVEDAS>                                              
            <MVR>0</MVR>                                              
            <AGEOFDRIVER>27</AGEOFDRIVER>                                              
*/                                              
                                              
/* GET the effective Date */                                              
 DECLARE @APPEFFECTIVEDATE datetime                                               
 SELECT @APPEFFECTIVEDATE = convert(char(10),APP_EFFECTIVE_DATE,101)    FROM APP_LIST  with (nolock)                                             
 WHERE CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID                                  
                                  
                             
SELECT                                              
     @DRIVERFNAME     =  ISNULL(DRIVER_FNAME,''),                                              
     @DRIVERMNAME     =  ISNULL(DRIVER_MNAME,''),                                              
     @DRIVERLNAME     =  ISNULL(DRIVER_LNAME,''),                                              
    -- @SUMOFACCIDENTPOINTS    ='0', /* TO CALCULATE */                                              
  --   @SUMOFVIOLATIONPOINTS   ='0' , /* TO CALCULATE */                                                  
     @BIRTHDATE    =  convert(char(10),DRIVER_DOB,101),                                            
     @GENDER       =  ISNULL(DRIVER_SEX,'')  ,                                         
     @DRIVERCLASS  = 'PA' , /* TO CALCULATE */                                         
     @DRIVERCLASSCOMPONENT1 ='P', /* TO CALCULATE */                                              
     @DRIVERCLASSCOMPONENT2 ='A', /* TO CALCULATE */                                              
     @DRIVERINCOME =ISNULL(DRIVER_INCOME,0),             
    -- @NODEPENDENT  =isnull(NO_DEPENDENTS,0),                                              
     @DRIVERLIC    =isnull(DRIVER_DRIV_LIC,''),                                              
     @WAIVEWORKLOSS= case ISNULL(WAIVER_WORK_LOSS_BENEFITS,'0')                                              
     when '0' then 'FALSE'                        
     when '1' then 'TRUE'                               
     end,                                                    
            @MARITALSTATUS    =   ISNULL(DRIVER_MART_STAT,'')   ,                                           
     @NOPREMDRIVERDISC =   case ISNULL(DRIVER_PREF_RISK,'0')                                   
     when '0' then 'TRUE'                                              
    when '1' then 'FALSE'                                              
     end,                                                
                                               
     @COLLEGESTUDENT   = case ISNULL(DRIVER_STUD_DIST_OVER_HUNDRED,'0')  -- on the screen data against college student is saved here                                            
     when '0' then 'FALSE'                                           
     when '1' then 'TRUE'                                              
  when '' then 'FALSE'                                              
     end,                                            
     @GOODSTUDENT      =  case ISNULL(DRIVER_GOOD_STUDENT,'0')                                   
     when '0' then 'FALSE'                                           
     when '1' then 'TRUE'                                              
     when '' then 'FALSE'                                              
     end,                    
     @DISTANTSTUDENT   = 'TRUE',  /*ISNULL(,'0') Not in table -- not required in rater*/                                           
    -- @VEHICLEASSIGNEDASOPERATOR    = ISNULL(A.VEHICLE_ID,'0'),                                               
    -- @VEHICLEDRIVEDAS  = cast(isnull(A.APP_VEHICLE_PRIN_OCC_ID,0)as varchar) ,                            
    --ADDED BY SHAFI FOR NO OF driver's license YEARS                            
     @LICUNDER3YRS       = case                                 
      when floor(DATEDIFF(DAY, convert(char(10),DATE_LICENSED,101), @APPEFFECTIVEDATE))/365 >= 3 then 'Y'                                
      else 'N'                                
      end  ,                          
      @DRIVERCODE =ISNULL(DRIVER_CODE,'')                          
                                  
                                           
                                              
 FROM APP_DRIVER_DETAILS D  with (nolock)           
 LEFT OUTER JOIN APP_DRIVER_ASSIGNED_VEHICLE A   with (nolock)          
 ON A.CUSTOMER_ID = D.CUSTOMER_ID AND A.APP_ID = D.APP_ID AND A.APP_VERSION_ID = D.APP_VERSION_ID AND A.DRIVER_ID = D.DRIVER_ID        
        
 WHERE D.CUSTOMER_ID=@CUSTOMERID AND D.APP_ID=@APPID AND D.APP_VERSION_ID=@APPVERSIONID AND D.DRIVER_ID=@DRIVERID                            
    
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
SET @VEHICLEDRIVEDASCODE = 'YPNPA^PRINCIPAL'   
END     
    
if (@VEHICLEASSIGNEDASOPERATOR is null)    
BEGIN    
 select @VEHICLEASSIGNEDASOPERATOR = VEHICLE_ID from #tempvehicle where Assigned_type = 11929    
SET @VEHICLEDRIVEDAS = 'Youthful Principal'  
SET @VEHICLEDRIVEDASCODE = 'YPPA^PRINCIPAL'  
END     
    
if (@VEHICLEASSIGNEDASOPERATOR is null)    
BEGIN    
 select @VEHICLEASSIGNEDASOPERATOR = VEHICLE_ID from #tempvehicle where Assigned_type = 11928    
SET @VEHICLEDRIVEDAS = 'Youthful Occasional' 
SET @VEHICLEDRIVEDASCODE = 'YONPA^OCCASIONAL'   
END                      
if (@VEHICLEASSIGNEDASOPERATOR is null)    
BEGIN    
 select @VEHICLEASSIGNEDASOPERATOR = VEHICLE_ID from #tempvehicle where Assigned_type = 11927    
SET @VEHICLEDRIVEDAS = 'Youthful Occasional' 
SET @VEHICLEDRIVEDASCODE= 'YOPA^OCCASIONAL'   
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
     
IF ISNULL(@DRIVERCODE,'')=''                          
 SET @DRIVERCODE = ' '                
                
    
/*                
application/aspx/GeneralInformation.aspx?CUSTOMER_ID=878&APP_ID=13&APP_VERSION_ID=1&CALLEDFROM=&LoadedAfterSave=&&transferdata=                
Proc_GetRatingInformationForAuto_DriverComponent 878 ,13,1,23                
*/                    
                                                            
                                  
                               
-- Get The Age Of The Driver                                          
 DECLARE @REALAGEOFDRIVER DECIMAL(18,8)                               
 --SET @REALAGEOFDRIVER = (DATEDIFF(day, @BIRTHDATE, @APPEFFECTIVEDATE));      
SET @AGEOFDRIVER = (dbo.GetAge(@BIRTHDATE, @APPEFFECTIVEDATE));      
--set @REALAGEOFDRIVER = @REALAGEOFDRIVER / 365.2425 
--SET @AGEOFDRIVER = CONVERT(VARCHAR(100),@REALAGEOFDRIVER)
--SET @AGEOFDRIVER = dbo.piece(@AGEOFDRIVER,'.',1) 
                            
DECLARE @W_WORKLOSS INT                                
   SET @W_WORKLOSS = CONVERT(INT,DATEDIFF(YEAR, @BIRTHDATE, @APPEFFECTIVEDATE));                                 
IF @W_WORKLOSS > 6 AND @WAIVEWORKLOSS='TRUE'                                
 SET @WAIVEWORKLOSS='TRUE'                                
ELSE                                
 SET @WAIVEWORKLOSS='FALSE'                                
--PRINT @WAIVEWORKLOSS                                
                                 
                                                 
       SELECT @MARITALSTATUS = isnull(MLV.LOOKUP_VALUE_DESC,'') FROM MNT_LOOKUP_VALUES MLV   with (nolock)                                                   
       INNER JOIN MNT_LOOKUP_TABLES MLT with (nolock)    ON   MLV.LOOKUP_ID = MLT.LOOKUP_ID                                                  
       WHERE MLT.LOOKUP_NAME = 'MARST' AND MLV.LOOKUP_VALUE_CODE= @MARITALSTATUS                                               
                                            
                                            
      SELECT @GENDER = isnull(MLV.LOOKUP_VALUE_DESC,'') FROM MNT_LOOKUP_VALUES MLV  with (nolock) 
      INNER JOIN MNT_LOOKUP_TABLES MLT with (nolock)    ON   MLV.LOOKUP_ID = MLT.LOOKUP_ID           
      WHERE MLT.LOOKUP_NAME = 'SEXCD' AND MLV.LOOKUP_VALUE_CODE= @GENDER                                               
                                            
                                            
      /*SELECT @VEHICLEDRIVEDAS =isnull(LOOKUP_VALUE_DESC,'') FROM MNT_LOOKUP_VALUES        
      WHERE LOOKUP_UNIQUE_ID =@VEHICLEDRIVEDAS               
          
      SELECT @VEHICLEDRIVEDAS =dbo.piece(LOOKUP_VALUE_CODE,'^',2) FROM MNT_LOOKUP_VALUES  with (nolock)                                                  
      WHERE LOOKUP_UNIQUE_ID =@VEHICLEDRIVEDAS */                                       
       
           
                                            
                                      
                                      
   IF @DRIVERINCOME ='11415'                                             
    SET @DRIVERINCOME='HIGH'                                            
   ELSE                                            
SET @DRIVERINCOME ='LOW'                                            
                                      
                                    
                    
                                                   
                                          
 /* IF @NODEPENDENT !='0'                                            
   begin                                                
   SELECT @NODEPENDENT =isnull(LOOKUP_VALUE_DESC,'') FROM MNT_LOOKUP_VALUES                                                
   WHERE LOOKUP_UNIQUE_ID =@NODEPENDENT                                             
   end */                 
                
 -- If any one of the assigned drivers has NODEPENDENT = NDEP then send <NODEPENDENT>NDEP</NODEPENDENT>                          
                           
 if exists(select NO_DEPENDENTS from APP_DRIVER_DETAILS  WITH (NOLOCK)                                                           
  where CUSTOMER_ID=@CUSTOMERID AND APP_ID=@APPID AND APP_VERSION_ID=@APPVERSIONID AND  NO_DEPENDENTS='11588')                          
 begin                           
    set @NODEPENDENT='NDEP'                                
 end                           
 else                          
 begin                           
  set   @NODEPENDENT='1MORE'                                
 end                   
                                           
/* SELECT @MVR= SUM(MV.MVR_POINTS)                                              
 FROM APP_MVR_INFORMATION AMI                                            
  INNER JOIN MNT_VIOLATIONS MV ON MV.VIOLATION_ID=AMI.APP_MVR_ID                                            
 WHERE  AMI.CUSTOMER_ID =@CUSTOMERID AND AMI.APP_ID=@APPID  AND AMI.APP_VERSION_ID=@APPVERSIONID                                            
 GROUP BY CUSTOMER_ID,APP_ID,APP_VERSION_ID                                            
*/                            
                              
                              
                              
                          
          /*          Get The @SUMOFACCIDENTPOINTS EQUAL TO Sum of all Accidents points   */                              
                        /* ACCIDENT  VIOLATION AFTER ACCIDENT=728  *****/              
                              
  /* 24 April 2006    SELECT @MVR= SUM(MV.MVR_POINTS)                                              
                 FROM MNT_VIOLATIONS MV                                            
                 INNER JOIN APP_MVR_INFORMATION AMI ON MV.VIOLATION_ID=AMI.VIOLATION_ID                                            
                 WHERE  AMI.CUSTOMER_ID =@CUSTOMERID AND AMI.APP_ID=@APPID  AND AMI.APP_VERSION_ID=@APPVERSIONID                                   
                 AND AMI.DRIVER_ID=@DRIVERID AND  VIOLATION_TYPE IN( 728,270)         
       
               IF  @MVR IS NULL                              
                  SET @SUMOFACCIDENTPOINTS=0                          
               ELSE                              
       SET @SUMOFACCIDENTPOINTS=@MVR  */                            
                              
                               
/*              Get The @SUMOFVIOLATIONPOINTS EQUAL TO Sum of all MVR points  OTHER THAN ACCIDENT POINTS  */              
                                         
        /* 24 april 2006    SELECT @MVR= SUM(MV.MVR_POINTS)                                              
                 FROM MNT_VIOLATIONS MV                                            
                 INNER JOIN APP_MVR_INFORMATION AMI ON MV.VIOLATION_ID=AMI.VIOLATION_ID                                            
                WHERE  AMI.CUSTOMER_ID =@CUSTOMERID AND AMI.APP_ID=@APPID  AND AMI.APP_VERSION_ID=@APPVERSIONID                                   
                 AND AMI.DRIVER_ID=@DRIVERID AND  VIOLATION_TYPE NOT IN( 728,270)                               
                                           
               IF  @MVR IS NULL                              
                  SET @SUMOFVIOLATIONPOINTS=0                              
               ELSE                              
                 SET @SUMOFVIOLATIONPOINTS=@MVR        */                      
                                 
  --check for safe driver discount          
DECLARE @SAFERENEWALDISCOUNT decimal                      
SELECT         
@SAFERENEWALDISCOUNT = SAFE_DRIVER_RENEWAL_DISCOUNT        
FROM APP_DRIVER_DETAILS with (nolock) WHERE  CUSTOMER_ID= @CUSTOMERID AND APP_ID= @APPID AND APP_VERSION_ID=@APPVERSIONID and DRIVER_ID=@DRIVERID         
        
if (@SAFERENEWALDISCOUNT > 0)        
set @SAFEDRIVERDISCOUNT = 'YES'        
else        
set @SAFEDRIVERDISCOUNT = 'NO'                                          
                                  
                                            
--------------- SENDING THE DATA -------                                            
SELECT                                               
 @DRIVERFNAME     AS DRIVERFNAME,                                              
 @DRIVERMNAME    AS  DRIVERMNAME,                                              
 @DRIVERLNAME    AS  DRIVERLNAME,                   
 @BIRTHDATE    AS BIRTHDATE,                                          
 @AGEOFDRIVER    AS AGEOFDRIVER ,                   
 @GENDER    AS GENDER,                    
 @MARITALSTATUS    AS MARITALSTATUS,                                           
 @DRIVERINCOME    AS DRIVERINCOME,                                              
 @NODEPENDENT    AS DEPENDENTS,                                              
 @DRIVERLIC    AS DRIVERLIC,                                              
 @WAIVEWORKLOSS    AS WAIVEWORKLOSS,                                              
 @NOPREMDRIVERDISC   AS  NOPREMDRIVERDISC,                             
 @COLLEGESTUDENT    AS COLLEGESTUDENT,                                              
 @GOODSTUDENT    AS GOODSTUDENT ,                  
 @DISTANTSTUDENT    AS DISTANTSTUDENT,                                                                          
 @VEHICLEASSIGNEDASOPERATOR    AS VEHICLEASSIGNEDASOPERATOR,                                              
 @VEHICLEDRIVEDAS    AS VEHICLEDRIVEDAS, 
 @VEHICLEDRIVEDASCODE AS VEHICLEDRIVEDASCODE,
 --@MVR    AS MVR,                                                                     
 --@SUMOFACCIDENTPOINTS    AS  SUMOFACCIDENTPOINTS,                                              
 --@SUMOFVIOLATIONPOINTS    AS SUMOFVIOLATIONPOINTS,                  
 @DRIVERCLASS    AS DRIVERCLASS,                                             
 @DRIVERCLASSCOMPONENT1    AS DRIVERCLASSCOMPONENT1,                    
 @DRIVERCLASSCOMPONENT2    AS DRIVERCLASSCOMPONENT2,                                       
 @LICUNDER3YRS         AS LICUNDER3YRS    ,                          
 @DRIVERCODE            AS DRIVERCODE,      
 @SAFEDRIVERDISCOUNT as SAFEDRIVER                                                                    
END      





GO

