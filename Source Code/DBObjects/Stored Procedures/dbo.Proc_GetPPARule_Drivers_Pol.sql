IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPPARule_Drivers_Pol]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPPARule_Drivers_Pol]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*--------------------------------------------------------------------------------------------------                                                                                                                                                        
Proc Name                : Dbo.Proc_GetPPARule_Drivers_Pol                                                                                                                                        
Created by               : Ashwani                                                                                                                                                        
Date                     : 01 Mar. 2006                                                                  
Purpose                  : To get the Driver's Information for Private PPA policy                                                                                                                                                       
Revison History          :                                                                                                                                                        
Used In                  : Wolverine                                                                                                                                                        
                                          
Reviewed By : Anurag Verma                                          
Reviewed On : 12-07-2007                                          
Proc_GetPPARule_Drivers_Pol 864,34,1,1                                        
----------------------------------------------------------------------------------------------------                                                                                                                                                        
Date     Review By          Comments                                                                                                                                                        
------   ------------       ------------------------------------------------------------------------                                                    
drop proc dbo.Proc_GetPPARule_Drivers_Pol 1692,25,1,1                                                      
*/                                            
CREATE proc [dbo].[Proc_GetPPARule_Drivers_Pol]                                                                                                                                                        
(                                                                                                                                                        
@CUSTOMER_ID    int,                                                                                                                                                        
@POLICY_ID    int,                                                                                                                                                        
@POLICY_VERSION_ID   int,                                                                                                                                              
@DRIVER_ID int                                                                                              
)                                                                                                                                                        
as                                                                                                                                         
begin                                                                                                                                         
--POL_DRIVER_DETAILS                                                                                                                                           
declare @DRIVER_DRINK_VIOLATION char                                        
declare @DRIVER_US_CITIZEN char                                           
declare @DRIVER_STUD_DIST_OVER_HUNDRED char                            
declare @DRIVER_LIC_SUSPENDED char                             
declare @SAFE_DRIVER char                     
-- DOB                                  
declare @DATE_DRIVER_DOB datetime                                                                  
declare @INT_DIFFERENCE int                                                              
declare @DRIVER_DOB char                                                                                                        
declare @DRIVER_NAME nvarchar(225)                                                                    
declare @DRIVER_DRIV_LIC nvarchar(30) -- Lic. number                                                                                                          
declare @DRIVER_CODE nvarchar(20)                                                                                         
declare @DRIVER_SEX nchar(12)                                                                                
declare @DRIVER_FNAME nvarchar(75)                                                                                                       
declare @DRIVER_LNAME nvarchar(75)                                                                          
declare @DRIVER_STATE nvarchar(5)                                                                                                         
declare @DRIVER_ZIP varchar(11)                                                                                                                            
declare @DRIVER_LIC_STATE nvarchar(5)                                                                                                                            
declare @DRIVER_DRIV_TYPE nvarchar(5)  -- Driver Type                                                                                                                          
declare @DRIVER_VOLUNTEER_POLICE_FIRE nchar(1)                                                                                                                            
declare @INTAPP_VEHICLE_PRIN_OCC_ID int                                                                                                                           
declare @APP_VEHICLE_PRIN_OCC_ID char                                                 
---                                                
declare @WAIVER_WORK_LOSS_BENEFITS char                                                
declare @IN_MILITARY int                                                
declare @HAVE_CAR int                                                  
declare @STATIONED_IN_US_TERR int                                                 
declare @DRIVER_GOOD_STUDENT char                                                
declare @PARENTS_INSURANCE int                                                 
declare @intDRIVER_LIC_STATE int                                                 
DECLARE @intVIOLATIONS int                                                                 
declare @VIOLATIONS VARCHAR(5)                                               
---                                              
declare @FULL_TIME_STUDENT char                                               
declare @SUPPORT_DOCUMENT char                                                  
---                                            
declare @RENEWAL NVARCHAR(20)                                                
--MVR_POINTS YRS                                                              
declare @MVR_PNTS_YEARS int                                                               
declare @ACC_MVR_POINTS_YRS int                                                               
set @MVR_PNTS_YEARS = 5                                                                 
set @ACC_MVR_POINTS_YRS = 3                                    
DECLARE @FORM_F95 INT                                      
 DECLARE @VIOLATIONREFERYEAR INT       
 DECLARE @ACCIDENTREFERYEAR INT                                        
 DECLARE @VIOLATIONNUMYEAR INT                                        
 DECLARE @ACCIDENTCHARGES INT                                  
 DECLARE @DRIVERTURNEITTEEN nvarchar(5)         
--ADDED BY PRAVEEN KUMAR(22-01-09):                                
DECLARE @DRIVERTURNTWENTYFIVE nvarchar(5)                                  
DECLARE   @IN_MILITARY_NEW CHAR                                  
DECLARE   @STATIONED_IN_US_TERR_NEW CHAR                                     
DECLARE   @HAVE_CAR_NEW  CHAR                                         
 SET @VIOLATIONREFERYEAR=3                                        
 SET   @ACCIDENTREFERYEAR =3                                        
 SET   @VIOLATIONNUMYEAR=2                                           
 SET   @ACCIDENTCHARGES=1000               
        
--Added by Charles on 19-Nov-09 for Itrack 6725          
DECLARE @DISTANT_STUDENT CHAR(2)         
DECLARE @PARENTS_INSURANCE_NEW CHAR(2)        
        
SET @DISTANT_STUDENT='-1'          
SET @PARENTS_INSURANCE_NEW='-1'                                                                                                 
--Added till here         
      
 --Added by Charles on 20-Nov-09 for Itrack 6592      
-- DECLARE @ISRENEWEDPOLICY CHAR      
-- DECLARE @PRIOR_LOSS CHAR      
-- SET @ISRENEWEDPOLICY = 'N'      
-- SET @PRIOR_LOSS = 'N'      
--           
-- IF EXISTS(SELECT CUSTOMER_ID FROM POL_POLICY_PROCESS WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID         
--    AND POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID = @POLICY_VERSION_ID AND PROCESS_ID = 5)      
-- BEGIN      
-- SET @ISRENEWEDPOLICY = 'Y'       
-- END        
--      
-- IF EXISTS(SELECT CUSTOMER_ID FROM APP_PRIOR_LOSS_INFO WITH(NOLOCK) WHERE CUSTOMER_ID = @CUSTOMER_ID)      
-- BEGIN      
-- SET @PRIOR_LOSS = 'Y'      
-- END      
 --Added till here                                                                  
                                                                                                                                    
if exists(select CUSTOMER_ID from  POL_DRIVER_DETAILS where                                                                                   
CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and DRIVER_ID=@DRIVER_ID)                            
begin                                        
--@DRIVER_LIC_SUSPENDED=isnull(DRIVER_LIC_SUSPENDED,''),                                                                          
                                                                      
select  @DRIVER_US_CITIZEN=isnull(DRIVER_US_CITIZEN,''), @DRIVER_DRINK_VIOLATION=isnull(DRIVER_DRINK_VIOLATION,''),                                                                                                                   
@DRIVER_STUD_DIST_OVER_HUNDRED=isnull(DRIVER_STUD_DIST_OVER_HUNDRED,''),@SAFE_DRIVER=isnull(SAFE_DRIVER,''),                                                              
@DRIVER_DOB =DRIVER_DOB ,@DATE_DRIVER_DOB=DRIVER_DOB,                                                                
@DRIVER_NAME=(isnull(DRIVER_FNAME,'') + '  ' + isnull(DRIVER_MNAME,'') + '  ' + isnull(DRIVER_LNAME,'')),                                                                       
@DRIVER_FNAME=isnull(DRIVER_FNAME,''),@DRIVER_LNAME=isnull(DRIVER_LNAME,''),@DRIVER_DRIV_LIC=isnull(DRIVER_DRIV_LIC,''),                                                                                                     
@DRIVER_CODE=isnull(DRIVER_CODE,''),@DRIVER_SEX=isnull(DRIVER_SEX,'') ,@DRIVER_STATE=isnull(DRIVER_STATE,''),                                                                                                                            
@DRIVER_ZIP=isnull(DRIVER_ZIP,''),@DRIVER_SEX=isnull(DRIVER_SEX,''),@DRIVER_LIC_STATE=isnull(DRIVER_LIC_STATE,''),     
@DRIVER_DRIV_TYPE=isnull(DRIVER_DRIV_TYPE,''),@DRIVER_VOLUNTEER_POLICE_FIRE=isnull(DRIVER_VOLUNTEER_POLICE_FIRE,''),                                                                     
@INTAPP_VEHICLE_PRIN_OCC_ID=isnull(APP_VEHICLE_PRIN_OCC_ID,-1),@WAIVER_WORK_LOSS_BENEFITS=isnull(WAIVER_WORK_LOSS_BENEFITS,''),                                                
@IN_MILITARY=ISNULL(IN_MILITARY,0),@HAVE_CAR=HAVE_CAR,@STATIONED_IN_US_TERR =STATIONED_IN_US_TERR,                                                
@DRIVER_GOOD_STUDENT=isnull(DRIVER_GOOD_STUDENT,''),@PARENTS_INSURANCE=ISNULL(PARENTS_INSURANCE,0),                                                
@intDRIVER_LIC_STATE= DRIVER_LIC_STATE ,@intVIOLATIONS=VIOLATIONS ,  @VIOLATIONS=ISNULL(VIOLATIONS,'') ,@FULL_TIME_STUDENT =ISNULL(FULL_TIME_STUDENT,''),                                              
 @SUPPORT_DOCUMENT =ISNULL(SUPPORT_DOCUMENT,'') ,@FORM_F95 = ISNULL(FORM_F95,0)                                                                                             
                                                
from  POL_DRIVER_DETAILS                                                                                   
where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and DRIVER_ID=@DRIVER_ID                                                                                                 
end                                                                                                                                                                                         
                                                                                 
-- If State is Indiana Driver/Household Member  Date of Birth Take effective date of policy minus the date of birth                                 
-- is under 21 Then look at Field  -Parents Insurance If  - Insured Elsewhere  Refer to underwriters                                                                 
                                                                
 declare @DATEAPP_EFFECTIVE_DATE datetime                                                      
 declare  @STATE_ID int                                                                  
                                                                
 select @STATE_ID=STATE_ID,@DATEAPP_EFFECTIVE_DATE=APP_EFFECTIVE_DATE                                                                
 from  POL_CUSTOMER_POLICY_LIST   with (nolock) -- by pravesh                                                                                                         
 where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and IS_ACTIVE='Y'                                                                                     
                                                            
-- declare @INSUREDELSEWHERE char  commented by Pravesh on 22 oct 08 as this Question is on Driver Page hence condider @PARENTS_INSURANCE                                        
-- select  @INSUREDELSEWHERE=isnull(INSUREDELSEWHERE,'N')                                                                
-- from POL_AUTO_GEN_INFO with(nolock)                                                                     
-- where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID  and IS_ACTIVE='Y'                                                                
                                                                          
 -- D_O_B                                                   
 SET @INT_DIFFERENCE = DATEDIFF(DAY,@DATE_DRIVER_DOB,@DATEAPP_EFFECTIVE_DATE)                                         
  SET @INT_DIFFERENCE = @INT_DIFFERENCE/365.2425                                                                                  
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
                                                                                                                 
--                                  
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
   IF(@DRIVER_STUD_DIST_OVER_HUNDRED='1')                                                                                
  BEGIN                                                                        
   SET @DRIVER_STUD_DIST_OVER_HUNDRED='Y'                                                                                                                                            
  END                                                         
  ELSE IF(@DRIVER_STUD_DIST_OVER_HUNDRED='0')                                    
  BEGIN                                                                                                                 
   SET @DRIVER_STUD_DIST_OVER_HUNDRED='N'                                                                
  END ELSE IF(@DRIVER_STUD_DIST_OVER_HUNDRED='')                                                                                         
    BEGIN                                                                                               
    SET @DRIVER_STUD_DIST_OVER_HUNDRED='N'                             
   END                                                                                    
 END                                                                                                           
                                                                                     
                                                                 
--                                   
if(@DRIVER_DRINK_VIOLATION='1')                                                                                                                                 
begin                                                                                                                              
set @DRIVER_DRINK_VIOLATION='Y'                                                                                                             
end                               
else if(@DRIVER_DRINK_VIOLATION='0')                                                                                                                                            
begin                                                                                                       
set @DRIVER_DRINK_VIOLATION='N'                                                                                                  
end                                                                                                  
--                                                     
if(@DRIVER_US_CITIZEN='0')                                                 
begin                                                           
set @DRIVER_US_CITIZEN='Y'                                                      
end                                                       
else if(@DRIVER_US_CITIZEN='1')                                                                  
begin                                                             
set @DRIVER_US_CITIZEN='N'                                            
end                                                                                                                                   
--                                                               
                                        
--                                                                  
/*if(@DRIVER_LIC_SUSPENDED='1')                                                                                                                                            
begin                                                                                                       
set @DRIVER_LIC_SUSPENDED='Y'                                                                                          
end                                                                             
else if(@DRIVER_LIC_SUSPENDED='0')                                                                                                                                            
begin                                                                         
set @DRIVER_LIC_SUSPENDED='N'          
end */                                       
--                                                                                                                           
if(@SAFE_DRIVER='1')                                                      
begin                                       
set @SAFE_DRIVER='Y'                                                                                                                  
end                                                            
else if(@SAFE_DRIVER='0')                             
begin                                               
set @SAFE_DRIVER='N'                                                                                                                                            
end                                                        
                                                                                                                    
------------------------------------------------------------------------------------                                                               
 declare @VEHICLE_ID varchar(20)                                                                                 
 -- if Driver Type is Licensed & value="11603"                                                                      
 if(@DRIVER_DRIV_TYPE=11603)                                                                      
begin                                                    
  if exists (select     POL_DRIVER_DETAILS.VEHICLE_ID                                                                      
 from  POL_VEHICLES inner join                                                                      
                       POL_DRIVER_DETAILS on POL_VEHICLES.CUSTOMER_ID = POL_DRIVER_DETAILS.CUSTOMER_ID and                                                                       
                       POL_VEHICLES.POLICY_ID = POL_DRIVER_DETAILS.POLICY_ID AND                                                                    
                       POL_VEHICLES.POLICY_VERSION_ID = POL_DRIVER_DETAILS.POLICY_VERSION_ID and                                                                       
                       POL_VEHICLES.VEHICLE_ID = POL_DRIVER_DETAILS.VEHICLE_ID                                                                      
 where     (POL_VEHICLES.IS_ACTIVE = 'Y')                                                                       
  AND (POL_DRIVER_DETAILS.CUSTOMER_ID = @CUSTOMER_ID) and (POL_DRIVER_DETAILS.POLICY_ID = @POLICY_ID) and                                                                       
      (POL_DRIVER_DETAILS.POLICY_VERSION_ID = @POLICY_VERSION_ID))                                                          
  begin                                                
  select     @VEHICLE_ID=ISNULL(convert(varchar(20),POL_DRIVER_DETAILS.VEHICLE_ID),'')                                                                
  from         POL_VEHICLES inner join                                                                      
            POL_DRIVER_DETAILS on POL_VEHICLES.CUSTOMER_ID = POL_DRIVER_DETAILS.CUSTOMER_ID and                                                           
             POL_VEHICLES.POLICY_ID = POL_DRIVER_DETAILS.POLICY_ID AND                                                       
          POL_VEHICLES.POLICY_VERSION_ID = POL_DRIVER_DETAILS.POLICY_VERSION_ID and                                          
  POL_VEHICLES.VEHICLE_ID = POL_DRIVER_DETAILS.VEHICLE_ID                                                       
  where (POL_VEHICLES.IS_ACTIVE = 'Y')                                                                       
        AND (POL_DRIVER_DETAILS.CUSTOMER_ID = @CUSTOMER_ID) and (POL_DRIVER_DETAILS.POLICY_ID = @POLICY_ID) and                                                                       
               (POL_DRIVER_DETAILS.POLICY_VERSION_ID = @POLICY_VERSION_ID)                                                          
  end                                                                       
  else                             
  begin                                                                       
  set @VEHICLE_ID=''                                                                      
  end                                             
end                                                                       
else                                                                      
begin                                                                       
 set @VEHICLE_ID='1'                                            
                                                               
end                                                                                                                                   
                                                                   
-- declare @DATEAPP_EFFECTIVE_DATE datetime                                                                                                                          
 declare @APP_EFFECTIVE_DATE char                                                                                           
 declare @DATEDATE_LICENSED datetime                                                                                            
 declare @DATE_LICENSED char                                                                                                                          
-- declare @DATEDATECONT_DRIVER_LICENSE int                                                                                                                    
 declare @CONT_DRIVER_LICENSE  char -- 2.B.6 2.B.7.b                                                                 
                                                              
--  select @DATEAPP_EFFECTIVE_DATE=APP_EFFECTIVE_DATE                                                                                  
--  from POL_CUSTOMER_POLICY_LIST                                                                                                                           
--  where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                               
                                                                                                              
                                                                                                                          
 SELECT @DATEDATE_LICENSED=DATE_LICENSED                                                                                         
 FROM POL_DRIVER_DETAILS                                                           
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND DRIVER_ID=@DRIVER_ID                                     
                                                                                                                          
                                                                                         
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
--=======================================================================                                                                                        
IF(@DATEAPP_EFFECTIVE_DATE IS NULL)                                                                                                               
BEGIN                                                                                                             
SET @APP_EFFECTIVE_DATE=''                                                         
END                                                                                                                          
ELSE                                          
BEGIN                                                                                                                           
SET @APP_EFFECTIVE_DATE='N'                                                                
END                                                       
--=========================================================================                                                                                                                           
IF(@DATEDATE_LICENSED IS NULL)                                                                                                            
BEGIN                                                                                                               
SET @DATE_LICENSED=''                                                                                      
END                                                                                                                  
ELSE                                                                                                                           
BEGIN                                                                                                                           
SET @DATE_LICENSED='N'                                                                                                                           
END                                                                                        
                                                                                                
--=========================================================================                                                                         
                                                                                                        
DECLARE  @INTCOUNTISACTIVE INT                                                                   
DECLARE  @DEACTIVATEVEHICLE CHAR                                                                                                        
                                           
SELECT @INTCOUNTISACTIVE=COUNT(DRIVER_ID) FROM POL_DRIVER_DETAILS D                                                                                                    
WHERE DRIVER_ID = @DRIVER_ID AND D.CUSTOMER_ID=@CUSTOMER_ID AND  D.POLICY_ID=@POLICY_ID  AND  D.POLICY_VERSION_ID=@POLICY_VERSION_ID                                                          
AND D.VEHICLE_ID IN                                                                                               
( SELECT V.VEHICLE_ID FROM POL_VEHICLES V                                                                                                
WHERE V.CUSTOMER_ID=@CUSTOMER_ID AND  V.POLICY_ID=@POLICY_ID  AND  V.POLICY_VERSION_ID=@POLICY_VERSION_ID AND V.IS_ACTIVE='N')                                                                                                   
--                                                  
IF (@INTCOUNTISACTIVE>0)                                                          
BEGIN                                                                                                     
SET @DEACTIVATEVEHICLE='Y'                                       
END                                                                        
ELSE                                                                              
BEGIN                                                      
SET @DEACTIVATEVEHICLE='N'                                                         
END                                                 
                                                                 
 --  Accumulation of more than 5 eligibility points during the preceding 3 years.                                                          
/* Any violations and accidents (both prior loss and claims)  within the last 3 years (Entered through MVR tab) ,                                                              
  Based on the effective date of the policy minus the conviction date                                                            
  If the number (sum of accident and violations)   is  greater then 5 Refer to Underwriter */                                                    
                                                   
 ---  MVR points                                          
                                                                                
 DECLARE @SD_POINTS CHAR                                          
 SET @SD_POINTS='N'                                                          
/*     Commented by MANOJ Rathore 18 oct 2007(New implementation has been done)                                          
                                          
  -- DECLARE @INTSD_PONITS INT                                                                   
 SELECT @INTSD_PONITS=SUM(ISNULL(MVR_POINTS,0))                                                              
 FROM POL_MVR_INFORMATION                                                                                        
 INNER JOIN  MNT_VIOLATIONS  ON POL_MVR_INFORMATION.VIOLATION_ID = MNT_VIOLATIONS.VIOLATION_ID                                                                    
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND DRIVER_ID=@DRIVER_ID                                                                                                                          
 AND (YEAR(GETDATE())- YEAR(MVR_DATE))<3 AND POL_MVR_INFORMATION.IS_ACTIVE='Y'                                                                                                          
 PRINT @INTSD_PONITS                                                                       
 IF(@INTSD_PONITS > 5)                                                                        
 BEGIN                                            
 SET @SD_POINTS='Y'                                                                                   
 END                
 ELSE                                                                                                    
 BEGIN                                                           
 SET @SD_POINTS='N'                                                                           
 END                                                                                                  
 PRINT @SD_POINTS                                           
*/                                                        
--============================================================================                                                                                                 
 declare @INTVEHICLE_ID int                                                      
 declare @ISDRVASSIGNEDVEH char                                                                                                 
                                                                                                
 select @INTVEHICLE_ID=count(VEHICLE_ID )                                                                                                
 from  POL_DRIVER_DETAILS                                                     
 where CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and DRIVER_ID=@DRIVER_ID                       
 and VEHICLE_ID is null                                                                  
                                                                                                
 if(@INTVEHICLE_ID>0 )                                                                                                
 begin                                                                                                 
  set @ISDRVASSIGNEDVEH='Y'                                                                  
 end                                                                                                 
 else                                             
 begin                                                                                                 
  set @ISDRVASSIGNEDVEH='N'                                                                    
 end                                                                               
--                                                                              
if(@DRIVER_LIC_STATE=0)                                                                          
begin                                                 
 set @DRIVER_LIC_STATE=''                                                                            
end                                                  
--                       
if(@DRIVER_DRINK_VIOLATION ='Y' and @DRIVER_DRIV_TYPE= '3477')                                        
 set @DRIVER_DRINK_VIOLATION='N'                             
                                        
                                                          
--                                                
if(@DRIVER_DRIV_TYPE=0)                                     
begin                                                        
 set @DRIVER_DRIV_TYPE=''                                                                           
end                                          
                                        
/* commented by Pravesh on 17 sep 08                                        
--------------Added by Raghav for Itrack Issue #4627-----                                        
                                        
IF (@DRIVER_DRIV_TYPE = '3477' AND @FORM_F95 = 10963 ) -- Excluded   \                                                              
 BEGIN                                        
set @DRIVER_DRIV_TYPE='Y'                                                                  
END                                        
                                        
ELSE IF (@DRIVER_DRIV_TYPE = '3477' AND @FORM_F95 = 10964 ) -- Excluded  \                                   
BEGIN                                        
set @DRIVER_DRIV_TYPE='N'                                                   
END                                        
--------------------                   
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
if(@VEHICLE_ID='')                                                                    
begin                                                                     
                                                                                                                     
 if(@INTAPP_VEHICLE_PRIN_OCC_ID=11399 or @INTAPP_VEHICLE_PRIN_OCC_ID=11398)                                                              
 begin                                                                       
  set @APP_VEHICLE_PRIN_OCC_ID='N'                                       
 end                                                                                   
 else                                                                                  
 begin                                                                                   
  set @APP_VEHICLE_PRIN_OCC_ID=''                                                                                  
 end                                                                      
end                                                                     
else                                                                    
begin                                                                     
 set @APP_VEHICLE_PRIN_OCC_ID='N'                                                                    
                                                                    
end                                                           
------------------------------------------------------------------------------------------                                                     
-- MVR information of 2 points & Put "NO" to the field "MVR Ordered" under Driver information, refer to underwriter                                                      
                            
DECLARE @POL_MVR_ID INT                                                     
DECLARE @MVR_ORDERED INT                                                     
DECLARE @DRIVER_MVR_ORDERED CHAR                                                    
DECLARE @MVR_STATUS CHAR(1) -- ITRACK 5529                            
SELECT @POL_MVR_ID=ISNULL(POL_MVR_ID,'0') FROM POL_MVR_INFORMATION                                                     
WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and DRIVER_ID=@DRIVER_ID and IS_ACTIVE='Y'                                                    
SELECT @MVR_ORDERED=ISNULL(MVR_ORDERED,'0'),@MVR_STATUS=MVR_STATUS FROM POL_DRIVER_DETAILS                                                     
WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID and DRIVER_ID=@DRIVER_ID and IS_ACTIVE='Y'                               
-- ITRACK 5529                            
IF(@MVR_ORDERED=10964 OR @MVR_ORDERED IS NULL OR @MVR_ORDERED=0)                            
BEGIN                            
 SET @DRIVER_MVR_ORDERED='Y'              
END        
ELSE IF (@MVR_ORDERED=10963 AND @MVR_STATUS IN ('C','V'))                            
BEGIN                            
 SET @DRIVER_MVR_ORDERED='N'                              
END --END OF ITRACK 5529 CHANGES                            
ELSE IF((@POL_MVR_ID IS NULL AND @MVR_ORDERED=10963) or (@POL_MVR_ID IS NOT NULL AND @MVR_ORDERED=10964))                                                    
 BEGIN                                                    
 SET @DRIVER_MVR_ORDERED='Y'                 
 END                                                    
 ELSE                                                   
 BEGIN                                                   SET     @DRIVER_MVR_ORDERED='N'                                                    
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
--------------------------------------------------------------------------------------------------------------------                                           
-- 3477- Excluded or if other then Licensed                                      
                                    
--ADDED BY PRAVEEN KUMAR(11-12-08)                                                                
if(@DRIVER_DRIV_TYPE<>'11603' and @DRIVER_DRIV_LIC='') --ONLY THIS CONDITION END PRAVEEN KUMAR                                                                 
 set @DRIVER_DRIV_LIC='N'                                                     
                                                            
if(@DRIVER_DRIV_TYPE<>'11603' and @DATE_LICENSED='')                                                                  
 set @DATE_LICENSED='N'                               
                                                       
if(@DRIVER_DRIV_TYPE<>'11603' and @DRIVER_DRINK_VIOLATION='')                                                
 set @DRIVER_DRINK_VIOLATION='N'                                  
                                        
if(@DRIVER_DRIV_TYPE<>'11603' and @DRIVER_LIC_STATE='')                                                
 set @DRIVER_LIC_STATE='N'                                              
                                        
----moved by Pravesh on 17 sep 08                                        
----------------Added by Raghav for Itrack Issue #4627-----                                        
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
                                        
-------------------------------                                                                  
-- Drivers/Household Members Tab If Yes to Waiver of Work Loss Coverage Then look at Field                                           
-- Signed Waiver of Benefits Form If no refer to underwriters                                                                 
                                    
DECLARE @WAIVER_WORK_LOSS CHAR                                      
SET @WAIVER_WORK_LOSS='N'                                             
                                                        
IF EXISTS(SELECT CUSTOMER_ID FROM POL_DRIVER_DETAILS                                                                
WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND DRIVER_ID=@DRIVER_ID                                                             
AND SIGNED_WAIVER_BENEFITS_FORM='0' AND WAIVER_WORK_LOSS_BENEFITS='1')-- AND (@ISRENEWEDPOLICY = 'N' OR @PRIOR_LOSS = 'Y')                                                               
BEGIN  -- @ISRENEWEDPOLICY,@PRIOR_LOSS condition added by Charles on 20-Nov-09 for Itrack 6592                                                                
SET @WAIVER_WORK_LOSS='Y'           
END                                                                                
                                                                
 -- Michigan Youthful driver                                                                
-- We will apply class 5C to youthful drivers (under the age of 25) who are on their parent's policy and also have                                                 
-- joined the Military Services.  He or she must be stationed in the "Policy Territory" (United States, its territories                                                                
-- and possessions, Puerto Rico and Canada) and not have a car with them on base.  If stationed outside of the "Policy                                                                 
-- Territory," they may be removed from the policy."                                                                
                                                                
-- MI ,age < 25 AND Country = USA or Canada AND Are you in the Military?* = yes AND Are you a college student?* = yes                         
-- and Do you have the car with you?* = No True then ok else Refer                                                                
                                                                 
DECLARE @YOUTH_DRIVER CHAR                                                             
SET @YOUTH_DRIVER = 'N'                                                 
                              
IF(@STATE_ID=22 AND @INT_DIFFERENCE<25 AND  @IN_MILITARY=10964 AND @DRIVER_STUD_DIST_OVER_HUNDRED='0'                                                        
AND @HAVE_CAR=10964 AND @STATIONED_IN_US_TERR<>10964)                                                                
BEGIN                                                                 
SET @YOUTH_DRIVER='Y'                                                                
END                                                                    
/* MVR Tab ,Violation                                                         
   For each driver in the lister - look at the list of violations and should there be any                                                                
   one violation where the number of points is 8                                                                 
   Then take the effective date of the policy minus the conviction date and if less                                                                 
   than 5 years , Refer to underwriters                                           
*/                                                                 
                                                                
/*  Commented by Manoj Rathore on 18 oct 2007 (New implementation has been done)                                          
 DECLARE @VIOLATION_POINT CHAR                                                                
 SET  @VIOLATION_POINT='N'                                           
 IF EXISTS( SELECT MVR_POINTS FROM POL_MVR_INFORMATION                                  
 INNER JOIN  MNT_VIOLATIONS  ON POL_MVR_INFORMATION.VIOLATION_ID = MNT_VIOLATIONS.VIOLATION_ID                                                                                          
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND DRIVER_ID=@DRIVER_ID                                                  
 AND MVR_POINTS=8 AND ((ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(MVR_DATE),0))<=@MVR_PNTS_YEARS AND                                                                          
(ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(MVR_DATE),0))>=0)                                                             
 AND POL_MVR_INFORMATION.IS_ACTIVE='Y')                                                                
 BEGIN                                                                 
 SET @VIOLATION_POINT='Y'                                                       
 END                                            
*/              
 -- If TOTAL violation Major(with in 5 year) and Minor(with in 3 years) greater than 5 refer to underwriter                                          
---------------------------------------------------                                          
-- RENEWAL PROCCESS(START)                                          
---------------------------------------------------                                          
/*                                              
Operators/Household Member                                    
MVR Info can be supplied by the agency or  can be done electronically  -                                             
Major Violation are all those that fall in the Violation Type Field                                              
 - Accident and Violations after Accident                                             
 - Serious Offences                                             
ALL other Violation Types are considered Minor                                             
ON NEW BUSINESS LOOK AT ALL THE POINTS TOTAL PER OPERATOR - for all Major violations in the last 5 years  on the effective date of the policy and the year of the violation and the points total for all Minor violations in the last 3 years based on the eff

  
    
      
        
             
                        
ective date of the policy and the year of the violation - if the number of points is 6 or higher - submit or if any of the points are showing as N/A                                            
ON RENEWAL LOOK AT ALL THE POINTS TOTAL PER OPERATOR FOR ALL Major violations in the last 5 years  on the effective date of the policy and the year of the violation and the points total for all Minor violations in the last 3 years based on the effective 
 
  
    
      
        
          
               
date of the policy and the year of the violation - if the number of points is 9 or higher - submit or if any of the points are showing as N/A                                            
Mid term updates on violations will be controlled by the underwriters - manually                                             
Operators/Members - MVR tab                                             
*/                                       
 DECLARE @AUTO_MAJOR_VIOLATION VARCHAR                                            
 DECLARE @RENEW_AUTO_MAJOR_VIOLATION VARCHAR                                            
 DECLARE @IS_RENEW INT                                            
 DECLARE @MVR_MAJOR_POINTS INT                                            
 DECLARE @MVR_MAJOR_POINT INT                                            
 DECLARE @POINT_ASSIGNED INT                                            
 SET  @AUTO_MAJOR_VIOLATION='N'                                            
 SET  @RENEW_AUTO_MAJOR_VIOLATION='N'                                            
 SET  @IS_RENEW=0                         
 --- if policy is renewed then renwal point violation rule will follow                      
                      
 DECLARE @HAVE_RENEWED NVARCHAR(20)                      
 SET @HAVE_RENEWED = 'N'             
 IF EXISTS(SELECT PROCESS_ID FROM POL_POLICY_PROCESS   WITH(NOLOCK)                                              
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND PROCESS_STATUS!='ROLLBACK' AND PROCESS_ID IN (5,18))                      
 BEGIN                      
 SET @HAVE_RENEWED='Y'                      
 END                                         
 -- Condition for Renewal and CUP                                              
 SELECT @IS_RENEW=COUNT(*) FROM POL_POLICY_PROCESS WITH(NOLOCK)                                            
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID                                              
 AND  NEW_POLICY_VERSION_ID=@POLICY_VERSION_ID  AND PROCESS_ID IN (5,18)                  
              
--Itrack # 5464 and 5950 and Added by Manoj Rathore on 9th Jun. 2009                                           
              
 /*IF (@IS_RENEW=0)--@HAVE_RENEWED = 'N')                                            
           
 BEGIN SET @MVR_MAJOR_POINT=6  END                                          
 ELSE                                            
 --SET @MVR_MAJOR_POINTS=9                                            
 BEGIN SET @MVR_MAJOR_POINTS=6 END*/              
          
  SET @MVR_MAJOR_POINT=6            
  SET @MVR_MAJOR_POINTS=6            
--End Itrack # 5464 and 5950 and Added by Manoj Rathore on 9th Jun. 2009                                            
              
 ----SD Points Rule.. (Minor Violation)                                                  
 DECLARE @INTSD_PONITS INT                                                        
 DECLARE @AUTO_SD_POINTS CHAR ,                                            
 @RENEW_AUTO_SD_POINTS CHAR                                                                                     
 SET @AUTO_SD_POINTS='N'                           
 SET @RENEW_AUTO_SD_POINTS='N'                                              
                                                                         
 SELECT @INTSD_PONITS=ISNULL((SUM(ISNULL(POINTS_ASSIGNED,0))+ SUM(ISNULL(ADJUST_VIOLATION_POINTS,0))),0) FROM POL_MVR_INFORMATION                                                            
 INNER JOIN  VIW_DRIVER_VIOLATIONS  ON POL_MVR_INFORMATION.VIOLATION_TYPE = VIW_DRIVER_VIOLATIONS.VIOLATION_ID                                           
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND DRIVER_ID=@DRIVER_ID                                           
 --AND (YEAR(GETDATE())- YEAR(MVR_DATE))<= 3                                         
 AND DATEDIFF(DAY,MVR_DATE,@DATEAPP_EFFECTIVE_DATE)<= 3*365.25                                         
 AND DATEDIFF(DAY,MVR_DATE,@DATEAPP_EFFECTIVE_DATE)>= 0                                        
 --AND (VIOLATION_TYPE<>2099 AND VIOLATION_TYPE<>1830)                                         
 AND VIOLATION_CODE NOT IN('10000','40000','SUSPN')                                        
 AND POL_MVR_INFORMATION.IS_ACTIVE='Y'                                             
 -- AND  @INT_CONVICTED_ACCIDENT=1                                                 
                                              
 --== Major Violation                 
                                           
 SELECT @POINT_ASSIGNED=ISNULL((SUM(ISNULL(POINTS_ASSIGNED,0))+ SUM(ISNULL(ADJUST_VIOLATION_POINTS,0))),0) FROM POL_MVR_INFORMATION                                                
 INNER JOIN  VIW_DRIVER_VIOLATIONS  ON POL_MVR_INFORMATION.VIOLATION_TYPE = VIW_DRIVER_VIOLATIONS.VIOLATION_ID                                                                            
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID   AND DRIVER_ID=@DRIVER_ID                                                                                                              
 --AND (VIOLATION_TYPE=2099 or VIOLATION_TYPE=1830)                                         
 AND VIOLATION_CODE IN('10000','40000','SUSPN')                                         
 --AND (YEAR(GETDATE())- YEAR(MVR_DATE))<= 5                                         
 AND DATEDIFF(DAY,MVR_DATE,@DATEAPP_EFFECTIVE_DATE)<= 5*365.5                                         
 AND DATEDIFF(DAY,MVR_DATE,@DATEAPP_EFFECTIVE_DATE)>= 0                                        
 AND POL_MVR_INFORMATION.IS_ACTIVE='Y'                                            
 /*IF(@POINT_ASSIGNED >=@MVR_MAJOR_POINT)  -- At New Business(Major Violation) Points 6 or Higher than 6                                     
 BEGIN                                            
 SET @WATER_MAJOR_VIOLATION='Y'                                            
 END                                                     
 ELSE IF(@POINT_ASSIGNED >=@MVR_MAJOR_POINTS)  -- At Renewal Points 9 or higher than 9                                            
 BEGIN                              
 SET @RENEW_WATER_MAJOR_VIOLATION='Y'                                    
 END                                            
 */                                               
                      
                                                                          
 IF( @HAVE_RENEWED ='N' and (@INTSD_PONITS + @POINT_ASSIGNED >= @MVR_MAJOR_POINT or (@INTSD_PONITS + @POINT_ASSIGNED <0)))      -- At New Business(Minor Violation) Points 6 or Higher than 6  or negative  points                                            

  
     
      
       
             
 BEGIN                                                          
 SET @AUTO_SD_POINTS='Y'                                         
 END                                                                                       
 ELSE IF( @HAVE_RENEWED ='Y' and               
(@INTSD_PONITS + @POINT_ASSIGNED >= @MVR_MAJOR_POINT or (@INTSD_PONITS + @POINT_ASSIGNED < 0))) -- At Renewal Points 9 or higher than 9  or negative   or negative  points                                                 
 BEGIN                                                                             
 SET @RENEW_AUTO_SD_POINTS='Y'                                                   
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
                                         
INSERT INTO #DRIVER_VIOLATION_ACCIDENT exec GetMVRViolationPointsPol @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID ,@DRIVER_ID,@ACCIDENTREFERYEAR,@VIOLATIONNUMYEAR,@VIOLATIONREFERYEAR,@ACCIDENTCHARGES                                        
SELECT @ACCIDENT_POINTS = ACCIDENT_POINTS FROM #DRIVER_VIOLATION_ACCIDENT                             
IF(@ACCIDENT_POINTS IS NULL)                                          
SET @ACCIDENT_POINTS =0                                         
DROP TABLE  #DRIVER_VIOLATION_ACCIDENT                                         
IF( @HAVE_RENEWED ='N' and (@INTSD_PONITS + @POINT_ASSIGNED + @ACCIDENT_POINTS >= @MVR_MAJOR_POINT or (@INTSD_PONITS + @POINT_ASSIGNED + @ACCIDENT_POINTS <0)))      -- At New Business(Minor Violation) Points 6 or Higher than 6  or negative  points       


   
    
      
        
             
                  
BEGIN                                                          
SET @AUTO_SD_POINTS='Y'                                         
END                                                                                       
ELSE IF( @HAVE_RENEWED ='Y' and (@INTSD_PONITS + @POINT_ASSIGNED + @ACCIDENT_POINTS  >= @MVR_MAJOR_POINT or (@INTSD_PONITS + @POINT_ASSIGNED + @ACCIDENT_POINTS  < 0))) -- At Renewal Points 9 or higher than 9  or negative   or negative  points             

  
    
      
        
            
                 
BEGIN                                                             
SET @RENEW_AUTO_SD_POINTS='Y'                                     
END                                           
                 
----------------------------------------------------                                          
-- RENEWAL PROCCESS(END)                                          
----------------------------------------------------               
                                                                                        
  DECLARE @VIOLATION_POINT char                                                
  SET  @VIOLATION_POINT='N'                                           
/*-- Check Major Violation With in 5 years                                          
 DECLARE @intMAJOR_VIOLATION INT                                           
 SELECT @intMAJOR_VIOLATION=ISNULL(SUM(ISNULL(POINTS_ASSIGNED,0))+ SUM(ISNULL(ADJUST_VIOLATION_POINTS,0)),0)                                           
 FROM POL_MVR_INFORMATION                                           
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND DRIVER_ID=@DRIVER_ID                                          
 AND (VIOLATION_TYPE =270 OR VIOLATION_TYPE=1 )                      
 AND ((ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(OCCURENCE_DATE),0)))<=5                                           
                                          
-- Check Minor Violation With in 3 years                                           
 DECLARE @MINOR_VIOLATION INT                                           
 SELECT @MINOR_VIOLATION=ISNULL(SUM(ISNULL(POINTS_ASSIGNED,0))+ SUM(ISNULL(ADJUST_VIOLATION_POINTS,0)),0)                                           
 FROM POL_MVR_INFORMATION                                           
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID  AND DRIVER_ID=@DRIVER_ID                                          
 AND (VIOLATION_TYPE !=270 AND VIOLATION_TYPE!=1 )                                          
 AND ((ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(OCCURENCE_DATE),0)))<=3                                           
                                           
 -- Sum of Major and Minor violation greater than 5 and less than 0 refer to underwriter                                          
 IF((@intMAJOR_VIOLATION + @MINOR_VIOLATION) > 5 or (@intMAJOR_VIOLATION + @MINOR_VIOLATION)< 0)                                               
 BEGIN                                                 
 SET @VIOLATION_POINT='Y'                                                
 END  */                                                                                                          
-- If driver is assigned "with points" and No MVR information is provided OR driver is assigned with "no points" and                                                
-- MVR info is provided Refere to underwriter                                                                
                                                                
 DECLARE @DRV_WITHPOINTS CHAR                                                                
 DECLARE @DRV_WITHOUTPOINTS CHAR                                                                
 SET @DRV_WITHPOINTS='N'                                    
 SET @DRV_WITHOUTPOINTS ='N'                               
                               
 /*  ===>>> Itrack No. 2933 Commented By Manoj RATHORE                                           
                                        
 IF EXISTS (SELECT MVR_POINTS FROM POL_MVR_INFORMATION                                               
 INNER JOIN  MNT_VIOLATIONS  ON POL_MVR_INFORMATION.VIOLATION_ID = MNT_VIOLATIONS.VIOLATION_ID                                                 
 WHERE CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID  and DRIVER_ID=@DRIVER_ID                                                                              
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
  AND  P.APP_VEHICLE_PRIN_OCC_ID IN (11929,11925,11398,11927) AND  D.MVR_ORDERED=0)                                                   
                                                         
  BEGIN                                                        
  SET @DRV_WITHPOINTS='Y'                                                                
  END                                                    
  ELSE                                                   
  BEGIN                                                                 
  SET @DRV_WITHPOINTS='N'                                                                
  END                                                                
                                                                
 END                                            
 */                                                 
                                                
-- Check the list If we have any drivers under 25 If Yes The look at Field Parents Insurance                      -- If Insured Elsewhere - then refer to underwriters                                                                
                                                                
declare @DRIVER_PARENT_ELSEWHERE char                                                                
set @DRIVER_PARENT_ELSEWHERE='N'                                                              
                                                                
if(@PARENTS_INSURANCE=11934 and @INT_DIFFERENCE<21 and @STATE_ID=14  )                                                
begin                                        
 set @DRIVER_PARENT_ELSEWHERE='Y'                                              
   if (@DRIVER_DOB='Y')                                        
 set @DRIVER_DOB='N'                                        
end                                        
                          
-------------------------------------------------------------------------------------------------------------------                                                               
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
from POL_VEHICLES                                            
where CUSTOMER_ID=@CUSTOMER_ID and  POLICY_ID=@POLICY_ID  and  POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                 
 and VEHICLE_ID in (select VEHICLE_ID from POL_DRIVER_DETAILS                                                                
    where CUSTOMER_ID=@CUSTOMER_ID  and  POLICY_ID=@POLICY_ID  and  POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                 
     and DRIVER_ID=@DRIVER_ID                                                    
)                                                 
                                                                
declare @COLLEGE_INSELSE char                                                           
set @COLLEGE_INSELSE='N'                          
                                               
if(@PARENTS_INSURANCE=11934 and (@DRIVER_STUD_DIST_OVER_HUNDRED='1' or @DRIVER_STUD_DIST_OVER_HUNDRED='Y'))                              
begin                                                                 
 set @COLLEGE_INSELSE='Y'                           
end                                                                 
                        
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
                                                
                                                                
 /* Issue no 336                                               
Driver/Household Member If Driver is under 25 years of age the question Are you in the Military?* will                                        
 appear on this screen If Yes  The look at Field Parents Insurance If Insured Elsewhere - then refer to underwriters */                      
                                         
declare @DRV_MIL_INSELSE char                                          
set @DRV_MIL_INSELSE='N'                                                  
                                                             
if(@INT_DIFFERENCE<25 and @IN_MILITARY=10963 and @PARENTS_INSURANCE=11934)                                                                
begin                                                                 
 set @DRV_MIL_INSELSE='Y'                                                   
end                                                     
                                                
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
                                                                
declare @PARNT_USTERR_CAR_STA char                                                                
set @PARNT_USTERR_CAR_STA='N'                                                                
                                                                
if(@PARENTS_INSURANCE=11935 and @STATIONED_IN_US_TERR=10963 and @HAVE_CAR=10963 and (@STATE_ID<>@intDRIVER_LIC_STATE))                                                                 
begin                                                                 
 set @PARNT_USTERR_CAR_STA='Y'                                                                
end                       
                                                
/*If State is Michigan Driver/Household member If age is over 60                                                                 
Rule (Refer) should be implemented as follows:                                                                
If Waiver of Loss is Yes and A-94 not available OR A-94 available and Waiver of Loss is not Yes for any of driver */                                                   
                                                    
declare @COV_A94_EXISTS char                                                                
declare @MI_OLDDRIVER char                                         
DECLARE @ELIGIBLE_VEHICLE CHAR                                                                
SET @ELIGIBLE_VEHICLE='N'                                                              
                                                 
set @MI_OLDDRIVER='N'                                                                 
set @COV_A94_EXISTS ='N'                                                                
-- ADDED BY PRAVESH ON 25 JULY 11618->Suspended-Comp Only ;11337->Utility Trailer ; 11341->Trailer A94 IS NOT APPLICABLE TO THESE TYPE                                           
IF EXISTS(SELECT V.CUSTOMER_ID FROM POL_VEHICLES V WHERE V.CUSTOMER_ID=@CUSTOMER_ID AND V.POLICY_ID=@POLICY_ID                                          
   AND V.POLICY_VERSION_ID=@POLICY_VERSION_ID                                          
   AND ISNULL(APP_VEHICLE_PERTYPE_ID,0) NOT IN (11337,11618)                                          
   AND ISNULL(APP_VEHICLE_COMTYPE_ID,0) NOT IN (11341)                
   AND V.IS_SUSPENDED <> 10963 -- ADDED CONDITION OF 'IS_SUSPENDED' for Itrack Issue 5609 on 12 May 2009                                           
  )                                          
BEGIN                                          
 SET @ELIGIBLE_VEHICLE='Y'                                            
END                                               
--END HERE                                                                
if exists(select CUSTOMER_ID from POL_VEHICLE_COVERAGES COV                                                                
  where COV.CUSTOMER_ID=@CUSTOMER_ID AND  COV.POLICY_ID=@POLICY_ID  AND  COV.POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                
  AND COV.COVERAGE_CODE_ID IN (1006)                                                         
  AND  COV.VEHICLE_ID IN(select VEHICLE_ID from POL_DRIVER_ASSIGNED_VEHICLE D                                                                
  where D.CUSTOMER_ID=@CUSTOMER_ID AND  D.POLICY_ID=@POLICY_ID  AND  D.POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                   
  AND D.DRIVER_ID= @DRIVER_ID                                                    
))                                                                
begin                                                                 
 set @COV_A94_EXISTS='Y'                               
end                                                                 
else                       
begin                                                
 set @COV_A94_EXISTS='N'                               
end                                                                
                                                                
---                                                                 
--ADDED BY PRAVEEN KUMAR(02-03-2009):ITRACK 5503                            
IF  EXISTS(                      
 SELECT APP.VEHICLE_ID FROM                       
 POL_VEHICLES APP INNER JOIN POL_DRIVER_ASSIGNED_VEHICLE ASS ON                            
 APP.CUSTOMER_ID = ASS.CUSTOMER_ID AND APP.POLICY_ID = ASS.POLICY_ID AND APP.POLICY_VERSION_ID = ASS.POLICY_VERSION_ID                            
 WHERE APP.IS_ACTIVE = 'Y' AND APP.CUSTOMER_ID = @CUSTOMER_ID AND APP.POLICY_ID = @POLICY_ID AND ASS.DRIVER_ID=@DRIVER_ID                            
 AND APP_USE_VEHICLE_ID ='11332'                      
 AND APP.POLICY_VERSION_ID = @POLICY_VERSION_ID and ASS.APP_VEHICLE_PRIN_OCC_ID IN('11398','11399')                      
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
/*Issues no 556                                                      
For Driver Information If the License State is not same as that of application                                                      
state then the application has been referred to underwriter*/                                            
                                                      
--SELECT @DRIVER_LIC_STATE =DRIVER_LIC_STATE FROM POL_DRIVER_DETAILS                                               
--WHERE  CUSTOMER_ID=@CUSTOMER_ID and POLICY_ID=@POLICY_ID and POLICY_VERSION_ID=@POLICY_VERSION_ID                                                       
                                                     
DECLARE @DRIVER_LIC_STATE_APP_STATE VARCHAR(5)                                                      
IF (@STATE_ID<>@INTDRIVER_LIC_STATE and ltrim(RTRIM(@INTDRIVER_LIC_STATE))!='' AND ltrim(RTRIM(@INTDRIVER_LIC_STATE))!=0)                                                      
BEGIN                                                      
 SET  @DRIVER_LIC_STATE_APP_STATE='Y'                                                      
 END                                                      
ELSE                                                      
 BEGIN                                                      
 SET @DRIVER_LIC_STATE_APP_STATE='N'                                                      
 END                 
                                                
/*                                                                
If Extended Non- Owned Coverage for Named Insured is checked off, then when doing the verify make sure that                                                                 
Drivers/Household members tab that the number of drivers in the limit field "Equal to" the number of drivers                                                                 
that have a yes in the Field Extended Non Owned Coverages Required.                                                                
If there is a yes in the Field Extended Non Owned Coverages Required on the                                                                 
Drivers/Household members tab                                                    
Then make sure  Extended Non- Owned Coverage for Named Insured is checked off */                                                       
                                                        
declare @ENO char                     
 SET @ENO='N'                                 
/* This Rule Moved To Vehicle Level on 27 Jan 09 Itrack 4771 By Pravesh                       
declare @ADD_INFORMATION varchar(20)                                                                
                                                                
SELECT @ADD_INFORMATION=ISNULL(ADD_INFORMATION,0)                                                                
 FROM POL_VEHICLE_COVERAGES COV                                                                
 WHERE  COV.CUSTOMER_ID=@CUSTOMER_ID AND  COV.POLICY_ID=@POLICY_ID  AND  COV.POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                   
 AND  COV.COVERAGE_CODE_ID IN (52,254)                                           
-- AND  COV.VEHICLE_ID IN(SELECT VEHICLE_ID FROM POL_DRIVER_DETAILS D                                                                
-- WHERE D.CUSTOMER_ID=@CUSTOMER_ID AND  D.POLICY_ID=@POLICY_ID  AND  D.POLICY_VERSION_ID=@POLICY_VERSION_ID AND D.DRIVER_ID= @DRIVER_ID)                                         
 AND  COV.VEHICLE_ID IN(                                        
  SELECT P.VEHICLE_ID FROM POL_DRIVER_DETAILS D                                             
  INNER JOIN POL_DRIVER_ASSIGNED_VEHICLE P                                                              
  ON D.CUSTOMER_ID=P.CUSTOMER_ID AND D.POLICY_ID=P.POLICY_ID AND D.POLICY_VERSION_ID=P.POLICY_VERSION_ID AND D.DRIVER_ID=P.DRIVER_ID              
  WHERE D.CUSTOMER_ID=@CUSTOMER_ID AND  D.POLICY_ID=@POLICY_ID  AND D.POLICY_VERSION_ID=@POLICY_VERSION_ID AND D.DRIVER_ID= @DRIVER_ID                                        
  )                                                               
                                                        
-- DRIVERS/HOUSEHOLD MEMBERS TAB THAT THE NUMBER OF DRIVERS IN THE LIMIT FIELD "EQUAL TO" THE NUMBER OF DRIVERS                                             
-- THAT HAVE A YES IN THE FIELD EXTENDED NON OWNED COVERAGES REQUIRED                                           
DECLARE @INTCOUNT INT                                                         
SELECT @INTCOUNT= COUNT(ISNULL(DRIVER_ID,0)) FROM POL_DRIVER_DETAILS                                                                 
WHERE CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND  EXT_NON_OWN_COVG_INDIVI='10963'                    
--                                                                 
IF(@ADD_INFORMATION<>@INTCOUNT)                                                                
SET @ENO='Y'                                                                
ELSE                                                                
SET @ENO='N'                                                                
                                
--End here                                 
*/                                
--cOMMENTED BY PRAVESH ON 19 SEP 08 ITRACK 4771                          
/*                                          
---NEW IMPLEMENTATION 04 JUNE 2007                                          
--STEP 1                                          
IF EXISTS(SELECT CUSTOMER_ID                                                          
  FROM POL_VEHICLE_COVERAGES COV                                            
  WHERE  COV.CUSTOMER_ID=@CUSTOMER_ID AND  COV.POLICY_ID=@POLICY_ID  and  COV.POLICY_VERSION_ID=@POLICY_VERSION_ID                                           
  AND  COV.COVERAGE_CODE_ID IN (52,254)                                                      
  AND  COV.VEHICLE_ID IN(SELECT P.VEHICLE_ID FROM POL_DRIVER_DETAILS D                                             
INNER JOIN POL_DRIVER_ASSIGNED_VEHICLE P                                                              
ON D.CUSTOMER_ID=P.CUSTOMER_ID AND D.POLICY_ID=P.POLICY_ID AND D.POLICY_VERSION_ID=P.POLICY_VERSION_ID AND D.DRIVER_ID=P.DRIVER_ID                                            
WHERE D.CUSTOMER_ID=@CUSTOMER_ID AND  D.POLICY_ID=@POLICY_ID  AND  D.POLICY_VERSION_ID=@POLICY_VERSION_ID                                
AND D.DRIVER_ID= @DRIVER_ID))                                          
BEGIN                                          
 IF EXISTS(SELECT CUSTOMER_ID FROM POL_DRIVER_DETAILS WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                                          
   POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND  EXT_NON_OWN_COVG_INDIVI='10964' AND  DRIVER_ID = @DRIVER_ID)                                          
 BEGIN                                 
  SET @ENO='Y'                                                  
 END                                          
 ELSE                                          
 BEGIN                                          
  SET @ENO='N'                                            
 END                                    
END                                          
--STEP 2 11332 Personal; 11618->Suspended-Comp Only ;11337->Utility Trailer ; 11341->Trailer ADDED BY pRAVESH ON 25 jULY 2008                                          
IF (EXISTS(SELECT EXT_NON_OWN_COVG_INDIVI FROM POL_DRIVER_DETAILS WHERE  CUSTOMER_ID=@CUSTOMER_ID AND                                          
   POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID  AND  EXT_NON_OWN_COVG_INDIVI='10963' AND  DRIVER_ID = @DRIVER_ID)                                          
)                                          
AND                                          
(                                          
 EXISTS(SELECT VEHICLE_ID FROM POL_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID  AND  POLICY_VERSION_ID=@POLICY_VERSION_ID                                           
   AND ISNULL(APP_VEHICLE_PERTYPE_ID,0) NOT IN (11337,11618)                                          
   AND ISNULL(APP_VEHICLE_COMTYPE_ID,0) NOT IN (11341)                                           
    )                                
                                          
)                                          
BEGIN                                          
IF NOT EXISTS(SELECT CUSTOMER_ID                                             
 FROM POL_VEHICLE_COVERAGES COV                                                              
 WHERE  COV.CUSTOMER_ID=@CUSTOMER_ID AND  COV.POLICY_ID=@POLICY_ID AND  COV.POLICY_VERSION_ID=@POLICY_VERSION_ID                     
 AND COV.COVERAGE_CODE_ID IN (52,254)                                                           
 AND  COV.VEHICLE_ID IN(SELECT P.VEHICLE_ID FROM POL_DRIVER_DETAILS D                                             
INNER JOIN POL_DRIVER_ASSIGNED_VEHICLE P                            
ON D.CUSTOMER_ID=P.CUSTOMER_ID AND D.POLICY_ID=P.POLICY_ID AND D.POLICY_VERSION_ID=P.POLICY_VERSION_ID AND D.DRIVER_ID=P.DRIVER_ID                                            
WHERE D.CUSTOMER_ID=@CUSTOMER_ID AND  D.POLICY_ID=@POLICY_ID  AND  D.POLICY_VERSION_ID=@POLICY_VERSION_ID                                                                 
AND D.DRIVER_ID= @DRIVER_ID))                                          
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
                                          
DECLARE @MAJOR_VIOLATION VARCHAR(5)                                          
SET @MAJOR_VIOLATION='N'                                    
                                
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
 SET @HAVE_CAR_NEW = 'N'                                 
IF(@INT_DIFFERENCE < 25 and @IN_MILITARY = 10963)                                
BEGIN                                
 IF(@HAVE_CAR = 10963 or @HAVE_CAR = 10964)                                
  SET @HAVE_CAR_NEW = 'N'                                
 ELSE                                
   SET @HAVE_CAR_NEW = ''                                
END                                 
                                
ELSE                                
 SET @STATIONED_IN_US_TERR_NEW = 'N'                        
                      
-----------iTRACK 5313------------------                      
DECLARE @DRIVERAGE INT                      
DECLARE @DRIVERAGEPRE INT                    
declare @APP_EFFECTIVE_DATE_PRE DATETIME                      
SET @DRIVERTURNTWENTYFIVE = 'N'                      
set @DRIVERTURNEITTEEN = 'N'                      
IF (@HAVE_RENEWED='Y')                      
BEGIN                      
 select                       
 @DRIVERAGE=dbo.piece(DATEDIFF(DAY,PDD.DRIVER_DOB,@DATEAPP_EFFECTIVE_DATE)/365.2425,'.',1)                       
 from POL_DRIVER_DETAILS PDD                                                               
  where PDD.CUSTOMER_ID=@CUSTOMER_ID  and  PDD.POLICY_ID=@POLICY_ID  and  PDD.POLICY_VERSION_ID=@POLICY_VERSION_ID                                                  
   and PDD.DRIVER_ID=@DRIVER_ID AND PDD.IS_ACTIVE = 'Y'                                                 
 select                       
 @APP_EFFECTIVE_DATE_PRE = APP_EFFECTIVE_DATE  FROM                       
 POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID  and  POLICY_ID=@POLICY_ID  and  POLICY_VERSION_ID=(@POLICY_VERSION_ID-1)                       
 select                       
 @DRIVERAGEPRE=dbo.piece(DATEDIFF(DAY,PDD.DRIVER_DOB,@APP_EFFECTIVE_DATE_PRE)/365.2425,'.',1)                       
 from POL_DRIVER_DETAILS PDD                                                               
  where PDD.CUSTOMER_ID=@CUSTOMER_ID  and  PDD.POLICY_ID=@POLICY_ID  and  PDD.POLICY_VERSION_ID=(@POLICY_VERSION_ID-1)                                                                 
   and PDD.DRIVER_ID=@DRIVER_ID AND PDD.IS_ACTIVE = 'Y'                         
IF(@DRIVERAGE=18 AND @DRIVERAGEPRE=17)                      
 BEGIN                      
  SET @DRIVERTURNEITTEEN = 'Y'                      
 END                                              
--END                      
                      
IF(@DRIVERAGE=25 AND @DRIVERAGEPRE=24)                      
 BEGIN                      
  SET @DRIVERTURNTWENTYFIVE = 'Y'                      
 END                                              
END                      
                               
--------------END BY PRAVEEN KUMAR----------                                
                                   
  IF(@DRIVER_DRIV_TYPE = '3477' AND @IN_MILITARY_NEW='')--Done by Sibin on 13 Feb 09 for Itrack Issue 5424                              
   BEGIN                              
     SET @IN_MILITARY_NEW='N'                              
   END                              
-----------------------------------------------------------------                               
--Moved by Sibin for Itrack Issue 5424                          
--moved by Pravesh on 17 sep 08                                        
--------------Added by Raghav for Itrack Issue #4627-----                                   
IF (@DRIVER_DRIV_TYPE = '3477' AND @FORM_F95 = 10963 )--AND (@ISRENEWEDPOLICY = 'N' OR @PRIOR_LOSS = 'Y') ) -- Excluded   \                                                              
 BEGIN  -- @ISRENEWEDPOLICY,@PRIOR_LOSS condition added by Charles on 20-Nov-09 for Itrack 6592                       
set @DRIVER_DRIV_TYPE='Y'                                 
END                                        
                                        
ELSE IF (@DRIVER_DRIV_TYPE = '3477' AND @FORM_F95 = 10964 ) -- Excluded   \                                                               
BEGIN                                        
set @DRIVER_DRIV_TYPE='N'                 
END                                        
-------------------------------                                                   
                                        
-------------------------------                                
 /*                                          
 IF EXISTS (SELECT MVR_POINTS FROM POL_MVR_INFORMATION                                                                
 INNER JOIN  MNT_VIOLATIONS  ON POL_MVR_INFORMATION.VIOLATION_ID = MNT_VIOLATIONS.VIOLATION_ID                                                                                        
 WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND DRIVER_ID=@DRIVER_ID            
 AND ((ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(MVR_DATE),0))<=@MVR_PNTS_YEARS AND                                                
 (ISNULL(YEAR(@DATEAPP_EFFECTIVE_DATE),0) - ISNULL(YEAR(MVR_DATE),0))>=0) AND VIOLATION_TYPE = 459)                                          
 BEGIN                                           
 SET @MAJOR_VIOLATION ='Y'                            
 END                                          
 */                                       
------------------------------------------                                                 
------------------------                
                                       
SELECT                                                       
 @DRIVER_DRINK_VIOLATION as DRIVER_DRINK_VIOLATION,                                                                   
 @DRIVER_US_CITIZEN as US_CITIZEN,                                                                                              
 @DISTANT_STUDENT as DISTANT_STUDENT, --Added by Charles on 19-Nov-09 for Itrack 6725                                                                                                     
 --@DRIVER_LIC_SUSPENDED as LIC_SUSPENDED,                                                                                                                
 -- @SAFE_DRIVER  as SAFE_DRIVER,                                                                     
 @DRIVER_NAME as DRIVER_NAME,                                             
 @DRIVER_DRIV_LIC as DRIVER_DRIV_LIC,                                                           
 @DRIVER_CODE as DRIVER_CODE,                                                   
 @DRIVER_SEX as DRIVER_SEX ,                                                                                                            
 @DRIVER_FNAME as DRIVER_FNAME,                                                                                     
 @DRIVER_LNAME as DRIVER_LNAME,                                        @DRIVER_STATE as DRIVER_STATE,                                                                                              
 @DRIVER_ZIP as DRIVER_ZIP,                                              
 @DRIVER_LIC_STATE as DRIVER_LIC_STATE,                                        
 @DRIVER_DRIV_TYPE as DRIVER_DRIV_TYPE,                                                                                                                            
 @DRIVER_VOLUNTEER_POLICE_FIRE as DRIVER_VOLUNTEER_POLICE_FIRE,                                                                                                                            
 -- @VEHICLE_ID as  VEHICLE_ID, --Drive                                                                                                                            
 -- @APP_EFFECTIVE_DATE as APP_EFFECTIVE_DATE,-- application                                         
 @DATE_LICENSED as DATE_LICENSED, -- Driver detail                                                  
 @CONT_DRIVER_LICENSE as CONT_DRIVER_LICENSE, --              
 @DRIVER_DOB as DRIVER_DOB ,                           
 @DEACTIVATEVEHICLE as DEACTIVATEVEHICLE ,                             
 -- Rule only                             
 @SD_POINTS as SD_POINTS  ,                                                                                                
 @ISDRVASSIGNEDVEH as ISDRVASSIGNEDVEH,                                                
 ---                                                
 @WAIVER_WORK_LOSS as WAIVER_WORK_LOSS,                                                
 @YOUTH_DRIVER as YOUTH_DRIVER,                                                
 @VIOLATION_POINT as VIOLATION_POINT,                                                 
 @DRV_WITHPOINTS as DRV_WITHPOINTS,                                                
 @DRV_WITHOUTPOINTS as DRV_WITHOUTPOINTS ,                                                
 @DRIVER_PARENT_ELSEWHERE as DRIVER_PARENT_ELSEWHERE,                                                
 @DRIVER_GOOD_STUDENT as DRIVER_GOOD_STUDENT,                                                
 @COLLEGE_INSELSE AS COLLEGE_INSELSE,                                                 
 @COLLEGE_CAR_STATE AS COLLEGE_CAR_STATE,                                                
 @COLLEGE_CAR_STATE_VEHCILE AS COLLEGE_CAR_STATE_VEHCILE ,                                           
 @DRV_MIL_INSELSE as DRV_MIL_INSELSE,                                                
 @PARNT_USTERR_CAR_STA as PARNT_USTERR_CAR_STA,                                                 
 @MI_OLDDRIVER as MI_OLDDRIVER,                                                
 @ENO as ENO ,                                                
 @VIOLATIONS AS VIOLATIONS,                                             
 ---                                                                                  
 -- @APP_VEHICLE_PRIN_OCC_ID as APP_VEHICLE_PRIN_OCC_ID                                                                                  
 @DRIVER_MVR_ORDERED AS DRIVER_MVR_ORDERED ,                                                
 @DRIVER_LIC_STATE_APP_STATE AS DRIVER_LIC_STATE_APP_STATE  ,                                              
 @DRIVER_SUPPORTING_DOCUMENT as DRIVER_SUPPORTING_DOCUMENT  ,                                          
 @MAJOR_VIOLATION as MAJOR_VIOLATION  ,                                          
 @AUTO_SD_POINTS AS AUTO_SD_POINTS ,                                          
 @RENEW_AUTO_SD_POINTS AS RENEW_AUTO_SD_POINTS   ,                                
 @IN_MILITARY_NEW AS IN_MILITARY ,                                 
 @STATIONED_IN_US_TERR_NEW AS  STATIONED_IN_US_TERR ,                                
 @HAVE_CAR_NEW  AS HAVE_CAR,                      
 @DRIVERTURNEITTEEN AS  DRIVERTURNEITTEEN,                      
 @DRIVERTURNTWENTYFIVE AS DRIVERTURNTWENTYFIVE,        
 @PARENTS_INSURANCE_NEW AS PARENTS_INSURANCE  --Added by Charles on 18-Nov-09 for Itrack 6725                                               
END 



GO

