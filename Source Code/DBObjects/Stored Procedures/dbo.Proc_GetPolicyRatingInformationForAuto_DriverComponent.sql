IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyRatingInformationForAuto_DriverComponent]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyRatingInformationForAuto_DriverComponent]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                    
Proc Name               : Dbo.Proc_GetPolicyRatingInformationForAuto_DriverComponent                                    
Created by              : shafi.                                    
Date                    : 02 March 2006                                    
Purpose                 : To get the information for creating the input xml                                      
Revison History    :                                    
Used In                 :   Creating InputXML for vehicle                                    
    
Reviewed By Anurag Verma on 18-06-2007    
------------------------------------------------------------                                    
Date     Review By          Comments                                    
------   ------------       -------------------------*/                                    
  --  DROP PROC  Proc_GetPolicyRatingInformationForAuto_DriverComponent                          
CREATE PROC [dbo].[Proc_GetPolicyRatingInformationForAuto_DriverComponent]                                    
(                                    
@CUSTOMERID    int,                                    
@POLICYID    int,                                    
@POLICYVERSIONID   int,                                    
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
DECLARE    @SAFEDRIVERDISCOUNT          nvarchar(20)                  
--DECLARE    @MVR    nvarchar(100)                                    
                  
DECLARE    @AGEOFDRIVER    nvarchar(100)                           
DECLARE    @LICUNDER3YRS       nvarchar(100)                             
DECLARE    @DRIVERCODE          nvarchar(20)                                   
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
DECLARE @POLEFFECTIVEDATE datetime                              
 SELECT @POLEFFECTIVEDATE = convert(char(10),APP_EFFECTIVE_DATE,101)    FROM POL_CUSTOMER_POLICY_LIST  WITH (NOLOCK)                                   
 WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID                                       
                          
                                    
 SELECT                                  
     @DRIVERFNAME     =  ISNULL(DRIVER_FNAME,''),                                    
     @DRIVERMNAME     =  ISNULL(DRIVER_MNAME,''),                                    
     @DRIVERLNAME     =  ISNULL(DRIVER_LNAME,''),                                    
     @BIRTHDATE    =  convert(char(10),DRIVER_DOB,101),                                  
     @GENDER       =  ISNULL(DRIVER_SEX,'')  ,                                    
     @DRIVERCLASS  = 'PA' , /* TO CALCULATE */                                    
     @DRIVERCLASSCOMPONENT1 ='P', /* TO CALCULATE */                                    
     @DRIVERCLASSCOMPONENT2 ='A', /* TO CALCULATE */                                    
     @DRIVERINCOME =ISNULL(DRIVER_INCOME,0),                                    
--     @NODEPENDENT  =isnull(NO_DEPENDENTS,0),                                    
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
    -- @VEHICLEASSIGNEDASOPERATOR    = ISNULL(VEHICLE_ID,'0'),                                     
    -- @VEHICLEDRIVEDAS  = cast(isnull(APP_VEHICLE_PRIN_OCC_ID,0)as varchar) ,                          
      @LICUNDER3YRS       = case                                     
      when floor(DATEDIFF(DAY, convert(char(10),DATE_LICENSED,101), @POLEFFECTIVEDATE))/365 >= 3 then 'Y'                                    
      else 'N'                                    
      end  ,                            
      @DRIVERCODE =ISNULL(DRIVER_CODE,'')                           
                                    
 FROM POL_DRIVER_DETAILS WITH (NOLOCK)                                   
 WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID AND DRIVER_ID=@DRIVERID  and IS_ACTIVE='Y'                
            
 -----------------------------------------------        
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
select vehicle_id,app_vehicle_prin_occ_id from POL_DRIVER_ASSIGNED_VEHICLE WITH (NOLOCK)        
where  CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID AND DRIVER_ID=@DRIVERID        
        
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
        
------------------------------------------------------------------------------------------            
 IF ISNULL(@DRIVERCODE,'')=''                        
         SET @DRIVERCODE = ' '                              
      IF ISNULL(@LICUNDER3YRS,'')=''                          
          SET @LICUNDER3YRS='N'                          
                         
 /* Age of Driver */                                    
                                   
     --SET @AGEOFDRIVER = floor(DATEDIFF(day, @BIRTHDATE, @POLEFFECTIVEDATE)/365.2425);                      
              
       SET @AGEOFDRIVER = dbo.GetAge(@BIRTHDATE, @POLEFFECTIVEDATE);                      
                              
                                
DECLARE @W_WORKLOSS INT                                    
   SET @W_WORKLOSS = CONVERT(INT,DATEDIFF(YEAR, @BIRTHDATE, @POLEFFECTIVEDATE));                                     
IF @W_WORKLOSS > 6 AND @WAIVEWORKLOSS='TRUE'                                    
 SET @WAIVEWORKLOSS='TRUE'                                    
ELSE               
 SET @WAIVEWORKLOSS='FALSE'                                    
                                                 
                                       
       SELECT @MARITALSTATUS = isnull(MLV.LOOKUP_VALUE_DESC,'') FROM MNT_LOOKUP_VALUES MLV WITH (NOLOCK)                                       
       INNER JOIN MNT_LOOKUP_TABLES MLT WITH (NOLOCK) ON   MLV.LOOKUP_ID = MLT.LOOKUP_ID                                        
  WHERE MLT.LOOKUP_NAME = 'MARST' AND MLV.LOOKUP_VALUE_CODE= @MARITALSTATUS                     
                                  
                                  
      SELECT @GENDER = isnull(MLV.LOOKUP_VALUE_DESC,'') FROM MNT_LOOKUP_VALUES MLV WITH (NOLOCK)                                       
      INNER JOIN MNT_LOOKUP_TABLES MLT WITH (NOLOCK) ON   MLV.LOOKUP_ID = MLT.LOOKUP_ID                                        
      WHERE MLT.LOOKUP_NAME = 'SEXCD' AND MLV.LOOKUP_VALUE_CODE= @GENDER                                     
                                  
                                  
      /*SELECT @VEHICLEDRIVEDAS =isnull(LOOKUP_VALUE_DESC,'') FROM MNT_LOOKUP_VALUES  WITH (NOLOCK)                                    
      WHERE LOOKUP_UNIQUE_ID =@VEHICLEDRIVEDAS                                 
              
      SELECT @VEHICLEDRIVEDAS =dbo.piece(LOOKUP_VALUE_CODE,'^',2) FROM MNT_LOOKUP_VALUES WITH (NOLOCK)                                                    
      WHERE LOOKUP_UNIQUE_ID =@VEHICLEDRIVEDAS  */                  
                                  
 IF @DRIVERINCOME !=''                                  
   begin                                      
   IF @DRIVERINCOME ='11415'                                   
    SET @DRIVERINCOME='HIGH'                                  
   ELSE                                  
    SET @DRIVERINCOME ='LOW'                          
   end                                  
                                  
                                  
                                  
  /*IF @NODEPENDENT !='0'                                  
   begin                                      
   SELECT @NODEPENDENT =isnull(LOOKUP_VALUE_DESC,'') FROM MNT_LOOKUP_VALUES WITH (NOLOCK)                                     
   WHERE LOOKUP_UNIQUE_ID =@NODEPENDENT             
   end  */               
                   
                     
 -- If any one of the assigned drivers has NODEPENDENT = NDEP then send <NODEPENDENT>NDEP</NODEPENDENT>                              
                               
 if exists(select NO_DEPENDENTS from POL_DRIVER_DETAILS  WITH (NOLOCK)                                                               
   WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID AND  NO_DEPENDENTS='11588' and IS_ACTIVE='Y')                       
begin                               
    set @NODEPENDENT='NDEP'                                    
 end                               
 else                              
 begin                               
  set   @NODEPENDENT='1MORE'                                    
 end                      
               
   
DECLARE @SAFERENEWALDISCOUNT decimal                            
SELECT               
@SAFERENEWALDISCOUNT = SAFE_DRIVER_RENEWAL_DISCOUNT              
FROM POL_DRIVER_DETAILS WITH (NOLOCK) WHERE CUSTOMER_ID=@CUSTOMERID AND POLICY_ID=@POLICYID AND POLICY_VERSION_ID=@POLICYVERSIONID and DRIVER_ID=@DRIVERID  and IS_ACTIVE='Y'              
              
if (@SAFERENEWALDISCOUNT > 0)              
set @SAFEDRIVERDISCOUNT = 'YES'              
else              
set @SAFEDRIVERDISCOUNT = 'NO'                  
                     
 /*          Get The @SUMOFACCIDENTPOINTS EQUAL TO Sum of all Accidents points   */                                  
                        /* ACCIDENT  VIOLATION AFTER ACCIDENT=728  *****/                   
                           
           /* 25 April 2006      SELECT @MVR= SUM(MV.MVR_POINTS)                                                  
                 FROM MNT_VIOLATIONS MV                                                
                 INNER JOIN POL_MVR_INFORMATION AMI ON MV.VIOLATION_ID=AMI.VIOLATION_ID                                                
                 WHERE  AMI.CUSTOMER_ID =@CUSTOMERID AND AMI.POLICY_ID=@POLICYID  AND AMI.POLICY_VERSION_ID=@POLICYVERSIONID                                       
                 AND AMI.DRIVER_ID=@DRIVERID AND  VIOLATION_TYPE IN( 728,270)                                      
                                               
               IF  @MVR IS NULL                                  
                  SET @SUMOFACCIDENTPOINTS=0                                  
               ELSE                                
                 SET @SUMOFACCIDENTPOINTS=@MVR   */                               
                                  
                                   
/*              Get The @SUMOFVIOLATIONPOINTS EQUAL TO Sum of all MVR points  OTHER THAN ACCIDENT POINTS  */                                    
                                             
     /* 25 April 2006      SELECT @MVR= SUM(MV.MVR_POINTS)                                                  
                 FROM MNT_VIOLATIONS MV                                                
                 INNER JOIN POL_MVR_INFORMATION AMI ON MV.VIOLATION_ID=AMI.VIOLATION_ID                                                
                WHERE  AMI.CUSTOMER_ID =@CUSTOMERID AND AMI.POLICY_ID=@POLICYID  AND AMI.POLICY_VERSION_ID=@POLICYVERSIONID                                       
                 AND AMI.DRIVER_ID=@DRIVERID AND  VIOLATION_TYPE NOT IN( 728,270)                                   
                                               
               IF  @MVR IS NULL                                  
                  SET @SUMOFVIOLATIONPOINTS=0                                  
               ELSE                        
                 SET @SUMOFVIOLATIONPOINTS=@MVR                                   
                                  
 SELECT @MVR= SUM(MV.MVR_POINTS)                                    
 FROM POL_MVR_INFORMATION AMI WITH (NOLOCK)                        
  INNER JOIN MNT_VIOLATIONS MV WITH (NOLOCK) ON MV.VIOLATION_ID=AMI.POL_MVR_ID                                  
 WHERE  AMI.CUSTOMER_ID =@CUSTOMERID AND AMI.POLICY_ID=@POLICYID  AND AMI.POLICY_VERSION_ID=@POLICYVERSIONID                                  
 GROUP BY CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID  */                                
                                  
--------------- SENDING THE DATA -------                                  
SELECT                                     
    @DRIVERFNAME     AS DRIVERFNAME,                                    
    @DRIVERMNAME    AS  DRIVERMNAME,                                    
    @DRIVERLNAME    AS  DRIVERLNAME,                        
                              
--  @SUMOFACCIDENTPOINTS    AS  SUMOFACCIDENTPOINTS,                                    
--  @SUMOFVIOLATIONPOINTS    AS SUMOFVIOLATIONPOINTS,                                    
                  
    @BIRTHDATE    AS BIRTHDATE,                                    
    @GENDER    AS GENDER,                                    
    @DRIVERCLASS    AS DRIVERCLASS,                                    
    @MARITALSTATUS    AS MARITALSTATUS,                                    
    @DRIVERCLASSCOMPONENT1    AS DRIVERCLASSCOMPONENT1,                                    
    @DRIVERCLASSCOMPONENT2    AS DRIVERCLASSCOMPONENT2,                                  
    @DRIVERINCOME    AS DRIVERINCOME,                                    
    @NODEPENDENT    AS DEPENDENT,                                    
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
                  
    @AGEOFDRIVER    AS AGEOFDRIVER ,                      
    @LICUNDER3YRS  AS LICUNDER3YRS ,                          
    @DRIVERCODE    AS DRIVERCODE,                                    
    @SAFEDRIVERDISCOUNT as SAFEDRIVER                                 
END         
    
  








GO

