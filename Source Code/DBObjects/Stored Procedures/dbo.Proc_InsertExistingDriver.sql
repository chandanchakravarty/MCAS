IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertExistingDriver]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertExistingDriver]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                                          
Proc Name       : dbo.Proc_InsertExistingDriver                                          
Used In  : Wolverine                                          
------------------------------------------------------------                                          
Date     Review By          Comments                                          
------   ------------       -------------------------*/                                          
-- DROP PROC Proc_InsertExistingDriver                                    
CREATE   PROCEDURE [dbo].[Proc_InsertExistingDriver]                                                                  
(                                                                  
 @TO_CUSTOMER_ID int,                                                                  
 @TO_APP_ID int,                                                                  
 @TO_APP_VERSION_ID int,                                                                  
 @FROM_CUSTOMER_ID int,                                                                  
 @FROM_APP_ID int,                                                                  
 @FROM_APP_VERSION_ID int,                                                                  
 @FROM_DRIVER_ID int,   --existing driver id                                                               
 @CREATED_BY_USER_ID  int,                                                              
 @CALLED_FROM VARCHAR(10)=null                                                                     
)                                                                  
AS                                                                 
declare @DRIVER_DOB datetime                                                                  
declare @NEW_DRIVER_DOB datetime                                                                  
declare @CurrentDate datetime                                                                  
declare @ClassLookupID int                                                                  
declare @ClassValue varchar(5)                                                                  
declare @Age int                                                                   
declare @Vehicle_ID int                                                                   
BEGIN                                                                  
Declare @To_Driver_Id int   --new driver id being generated                                                                   
SELECT  @To_Driver_Id = ISNULL(MAX(DRIVER_ID),0) + 1                                                                  
FROM     APP_DRIVER_DETAILS                                                                   
WHERE  CUSTOMER_ID=@TO_CUSTOMER_ID                                                                  
AND         APP_ID=@TO_APP_ID                                                                  
AND         APP_VERSION_ID=@TO_APP_VERSION_ID                                                                     
                                                                  
                          
DECLARE @DRIVERCODE VARCHAR(20)                                                                  
declare @FIRSTCHAR VARCHAR(10)                                                      
declare @LASTCHAR VARCHAR(10)                                     
declare @DRIVER_TYPE INT                       
                    
--Added by Sibin on 19 Jan 09 for Itrack issue 5304                       
DECLARE @TEMP_VIOLATION_TYPE INT                     
DECLARE @TEMP_TO_LOB_ID INT                    
DECLARE @TEMP_FROM_LOB_ID INT                                         
DECLARE @TEMP_VIOLATION_ID INT                     
DECLARE @TEMP_STATE_ID INT                     
                               
SELECT @TEMP_STATE_ID=STATE_ID FROM APP_LIST WHERE CUSTOMER_ID=@TO_CUSTOMER_ID AND APP_ID=@TO_APP_ID AND APP_VERSION_ID=@TO_APP_VERSION_ID                                               
                                                        
SELECT @TEMP_TO_LOB_ID=APP_LOB FROM APP_LIST WHERE CUSTOMER_ID=@TO_CUSTOMER_ID AND APP_ID=@TO_APP_ID AND APP_VERSION_ID=@TO_APP_VERSION_ID                      
                    
SELECT @TEMP_FROM_LOB_ID=APP_LOB FROM APP_LIST WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID AND APP_ID=@FROM_APP_ID AND APP_VERSION_ID=@FROM_APP_VERSION_ID                    
                              
                    
/*                                                      
The earlier method to determine unique driver_code has been removed.                                              
Duplicacy is allowed but prevented using random number generator driver code introducted now                                                      
SELECT @DRIVERCODE=DRIVER_CODE FROM APP_DRIVER_DETAILS WHERE DRIVER_ID=@FROM_DRIVER_ID                       
                                                                  
WHILE (EXISTS (SELECT DRIVER_CODE FROM APP_DRIVER_DETAILS WHERE DRIVER_CODE=@DRIVERCODE))                                                                  
BEGIN                                                    
 SET @LASTCHAR=RIGHT(@DRIVERCODE,1)                                                   
 SET @ASCII = ASCII(@LASTCHAR)                                                                  
 -- if the last character is int then increment & replace the last char.                                     
IF (@ASCII >= 48 AND @ASCII <= 57)--INT                                             BEGIN                                                                  
  SET @INCNUM=convert(int,@LASTCHAR) + 1                                                                  
  SET @LEN=LEN(@DRIVERCODE)                                    
  SET @CODE=SUBSTRING(@DRIVERCODE,0,@LEN)                                                                  
  SET @DRIVERCODE=@CODE+convert(varchar(2),@INCNUM)                                                                   
 END                                                                   
 ELSE --if the last char is char then concatenate 1 at the end.                                                                  
 BEGIN                                     
  SET @DRIVERCODE=@DRIVERCODE +'1'                                                      
 END                                                                  
END                                                                  
*/                                                                  
                                                                  
                                                                                                      
                
--Sumit Chhabra: 11/05/2007:Commented the following code as class will be set using WebService                                                        
/*IF (UPPER(@CALLED_FROM)='MOT')                                                                  
 BEGIN                                                                  
  SELECT @VEHICLE_ID=VEHICLE_ID,@DRIVER_DOB=DRIVER_DOB FROM APP_DRIVER_DETAILS WHERE                                                                  
     CUSTOMER_ID = @TO_CUSTOMER_ID AND APP_ID = @TO_APP_ID AND APP_VERSION_ID = @TO_APP_VERSION_ID                                                               
   AND IS_ACTIVE='Y' AND DRIVER_ID=@TO_DRIVER_ID                                                              
                                                            
  SELECT @NEW_DRIVER_DOB=MIN(DRIVER_DOB) FROM APP_DRIVER_DETAILS WHERE                                                                  
     CUSTOMER_ID = @TO_CUSTOMER_ID AND APP_ID = @TO_APP_ID AND APP_VERSION_ID = @TO_APP_VERSION_ID AND                                                                  
     VEHICLE_ID=@VEHICLE_ID  AND IS_ACTIVE = 'Y'                                                         
                                                                                        
    IF (@DRIVER_DOB>=@NEW_DRIVER_DOB)                                                                  
   BEGIN                                                                  
      SET @AGE = DATEDIFF(YY,@DRIVER_DOB,GETDATE())                                                                    
                                                                   
      IF @AGE<23                                               
     SET @CLASSVALUE='B'                                                                  
      ELSE IF (@AGE>=23 AND @AGE<30)                                        
        SET @CLASSVALUE='A'                                                                  
      ELSE IF(@AGE>=30)                                                                     
      SET @CLASSVALUE='C'                                          
                                                                    
     -- SELECT  @CLASSLOOKUPID=LOOKUP_UNIQUE_ID FROM MNT_LOOKUP_VALUES WHERE LOOKUP_ID=                                                                 
     --  (SELECT TOP 1 LOOKUP_ID FROM MNT_LOOKUP_TABLES WHERE LOOKUP_NAME='MCCLAS') AND LOOKUP_VALUE_CODE=@CLASSVALUE                                                                  
  select @CLASSLOOKUPID=v.lookup_unique_id from mnt_lookup_values v join mnt_lookup_tables t                                                          
   on  v.lookup_id=t.lookup_id                                                           
   where t.lookup_name='MCCLAS' and v.lookup_value_code=@CLASSVALUE  and v.is_active='Y'                                                            
                                                                  
       UPDATE APP_VEHICLES SET APP_VEHICLE_CLASS=@CLASSLOOKUPID WHERE                                                                   
         CUSTOMER_ID = @TO_CUSTOMER_ID AND APP_ID = @TO_APP_ID AND APP_VERSION_ID = @TO_APP_VERSION_ID AND                                                
         VEHICLE_ID=@VEHICLE_ID                                                                  
  END                                                              
END                             
*/                             
/* Added By Swarup to add MVR information with driver details */                           
                     
--Added by Sibin on 20 Jan 09 for Itrack Issue 5304                    
IF (@TEMP_FROM_LOB_ID = @TEMP_TO_LOB_ID)                    
BEGIN                 
  SET @DRIVERCODE=CONVERT(INT,RAND()*50)               
  SELECT @FIRSTCHAR=DRIVER_FNAME, @LASTCHAR=DRIVER_LNAME  FROM APP_DRIVER_DETAILS                                                       
   WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                                            
    AND         APP_ID=@FROM_APP_ID                                           
    AND         APP_VERSION_ID=@FROM_APP_VERSION_ID                                                  
    AND         DRIVER_ID= @FROM_DRIVER_ID                                                           
 SET @DRIVERCODE=(SUBSTRING(@FIRSTCHAR,1,3) + SUBSTRING(@LASTCHAR,1,4) + @DRIVERCODE)                                
                             
 --SELECT @DRIVER_TYPE=ISNULL(DRIVER_DRIV_TYPE,0) FROM APP_DRIVER_DETAILS                             
 --  WHERE CUSTOMER_ID=@TO_CUSTOMER_ID AND APP_ID=@TO_APP_ID AND APP_VERSION_ID=@TO_APP_VERSION_ID                            
                                                                   
                                                     
 IF (@TO_CUSTOMER_ID=@FROM_CUSTOMER_ID and @TO_APP_ID=@FROM_APP_ID and @TO_APP_VERSION_ID=@FROM_APP_VERSION_ID)                                                    
 BEGIN                                                    
 INSERT INTO APP_DRIVER_DETAILS          
 (                                                                  
 CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,DRIVER_SUFFIX,                 
 DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_HOME_PHONE,                
 DRIVER_BUSINESS_PHONE,DRIVER_EXT,DRIVER_MOBILE,DRIVER_DOB,DRIVER_SSN,DRIVER_MART_STAT,DRIVER_SEX,DRIVER_DRIV_LIC,                
 DRIVER_LIC_STATE,DRIVER_LIC_CLASS,DATE_EXP_START,DATE_LICENSED,DRIVER_REL,DRIVER_DRIV_TYPE,DRIVER_OCC_CODE,                
 DRIVER_OCC_CLASS,DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,DRIVER_INCOME,DRIVER_BROADEND_NOFAULT,                
 DRIVER_PHYS_MED_IMPAIRE,DRIVER_DRINK_VIOLATION,DRIVER_PREF_RISK,DRIVER_GOOD_STUDENT,                
 DRIVER_STUD_DIST_OVER_HUNDRED,DRIVER_LIC_SUSPENDED,DRIVER_VOLUNTEER_POLICE_FIRE,DRIVER_US_CITIZEN,IS_ACTIVE,                 
 CREATED_BY,CREATED_DATETIME,DRIVER_FAX,RELATIONSHIP,DRIVER_TITLE,Good_Driver_Student_Discount,                 
 Premier_Driver_Discount,Safe_Driver_Renewal_Discount,Vehicle_ID,Percent_Driven, Mature_Driver,                 
 Mature_Driver_Discount,Preferred_Risk_Discount,Preferred_Risk, TransferExp_Renewal_Discount,                 
 TransferExperience_RenewalCredit,SAFE_DRIVER,NO_DEPENDENTS,NO_CYCLE_ENDMT,APP_VEHICLE_PRIN_OCC_ID,WAIVER_WORK_LOSS_BENEFITS,                 
 FULL_TIME_STUDENT,SUPPORT_DOCUMENT,SIGNED_WAIVER_BENEFITS_FORM,HAVE_CAR,IN_MILITARY,STATIONED_IN_US_TERR,                
 PARENTS_INSURANCE,CYCL_WITH_YOU,COLL_STUD_AWAY_HOME,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,                          
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                          
 MVR_STATUS,MVR_REMARKS,FORM_F95,EXT_NON_OWN_COVG_INDIVI --Added for Itrack issue 5673                                     
                                       
 )                                                                  
 SELECT                                                                  
 @TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@To_Driver_Id,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,@DRIVERCODE,                                        
 DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_HOME_PHONE,                                     
 DRIVER_BUSINESS_PHONE,DRIVER_EXT,DRIVER_MOBILE,DRIVER_DOB,DRIVER_SSN,DRIVER_MART_STAT,DRIVER_SEX,DRIVER_DRIV_LIC,                                                            
 DRIVER_LIC_STATE,DRIVER_LIC_CLASS,DATE_EXP_START,DATE_LICENSED,DRIVER_REL,DRIVER_DRIV_TYPE,DRIVER_OCC_CODE,                                                                  
 DRIVER_OCC_CLASS,DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,DRIVER_INCOME,DRIVER_BROADEND_NOFAULT,                                                                  
 DRIVER_PHYS_MED_IMPAIRE,DRIVER_DRINK_VIOLATION,DRIVER_PREF_RISK,DRIVER_GOOD_STUDENT,                
 DRIVER_STUD_DIST_OVER_HUNDRED,DRIVER_LIC_SUSPENDED,DRIVER_VOLUNTEER_POLICE_FIRE,DRIVER_US_CITIZEN,'Y',                 
 @CREATED_BY_USER_ID,GETDATE(),DRIVER_FAX,RELATIONSHIP,DRIVER_TITLE,Good_Driver_Student_Discount,                               
 Premier_Driver_Discount,Safe_Driver_Renewal_Discount,Vehicle_ID,Percent_Driven, Mature_Driver,                
 Mature_Driver_Discount,Preferred_Risk_Discount,Preferred_Risk, TransferExp_Renewal_Discount,                
 TransferExperience_RenewalCredit,SAFE_DRIVER,NO_DEPENDENTS,NO_CYCLE_ENDMT,APP_VEHICLE_PRIN_OCC_ID,WAIVER_WORK_LOSS_BENEFITS,                
 FULL_TIME_STUDENT,SUPPORT_DOCUMENT,SIGNED_WAIVER_BENEFITS_FORM,HAVE_CAR,IN_MILITARY,STATIONED_IN_US_TERR,                 
 PARENTS_INSURANCE,CYCL_WITH_YOU,COLL_STUD_AWAY_HOME,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,        
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                          
 MVR_STATUS,MVR_REMARKS,FORM_F95,EXT_NON_OWN_COVG_INDIVI --Added for Itrack issue 5673                                                                                              
 FROM    APP_DRIVER_DETAILS                                            
 WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                                                  
 AND         APP_ID=@FROM_APP_ID                                                                  
 AND         APP_VERSION_ID=@FROM_APP_VERSION_ID                                                                  
 AND         DRIVER_ID= @FROM_DRIVER_ID                                                                
                                 
                                     
 INSERT INTO APP_DRIVER_ASSIGNED_VEHICLE                                                                  
 (                                                          
 CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,VEHICLE_ID,APP_VEHICLE_PRIN_OCC_ID                                 
 )                                                                  
 SELECT                                                             
 @TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@To_Driver_Id,VEHICLE_ID,11931--'Not Rated'                                         
 FROM    APP_VEHICLES                                                  
 WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID                                                                  
 AND         APP_ID=@TO_APP_ID                                                                  
 AND         APP_VERSION_ID=@TO_APP_VERSION_ID                                 
 ---                                    
             
   END                                               
   ELSE                                                
    BEGIN                                         
 INSERT INTO APP_DRIVER_DETAILS                                                                  
 (                                                                  
 CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,DRIVER_SUFFIX,  DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_HOME_PHONE,                    
 DRIVER_BUSINESS_PHONE,DRIVER_EXT,DRIVER_MOBILE,DRIVER_DOB,DRIVER_SSN,DRIVER_MART_STAT,DRIVER_SEX,DRIVER_DRIV_LIC, DRIVER_LIC_STATE,DRIVER_LIC_CLASS,DATE_EXP_START,DATE_LICENSED,DRIVER_REL,DRIVER_DRIV_TYPE,DRIVER_OCC_CODE,                 
 DRIVER_OCC_CLASS,DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,DRIVER_INCOME,DRIVER_BROADEND_NOFAULT,                  
 DRIVER_PHYS_MED_IMPAIRE,DRIVER_DRINK_VIOLATION,DRIVER_PREF_RISK,DRIVER_GOOD_STUDENT,                
 DRIVER_STUD_DIST_OVER_HUNDRED,DRIVER_LIC_SUSPENDED,DRIVER_VOLUNTEER_POLICE_FIRE,DRIVER_US_CITIZEN,IS_ACTIVE,  CREATED_BY,CREATED_DATETIME,DRIVER_FAX,RELATIONSHIP,DRIVER_TITLE,Good_Driver_Student_Discount,                  
 Premier_Driver_Discount,Safe_Driver_Renewal_Discount,Vehicle_ID,Percent_Driven,Mature_Driver,                 
 Mature_Driver_Discount,Preferred_Risk_Discount,Preferred_Risk, TransferExp_Renewal_Discount,                 
 TransferExperience_RenewalCredit,SAFE_DRIVER,NO_DEPENDENTS,NO_CYCLE_ENDMT,APP_VEHICLE_PRIN_OCC_ID,WAIVER_WORK_LOSS_BENEFITS,                 
 FULL_TIME_STUDENT,SUPPORT_DOCUMENT,                
 SIGNED_WAIVER_BENEFITS_FORM,                                        
 HAVE_CAR,IN_MILITARY,STATIONED_IN_US_TERR,PARENTS_INSURANCE,CYCL_WITH_YOU,COLL_STUD_AWAY_HOME,DATE_ORDERED,                  
 MVR_ORDERED,VIOLATIONS,                          
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                          
 MVR_STATUS,MVR_REMARKS,FORM_F95,EXT_NON_OWN_COVG_INDIVI --Added for Itrack issue 5673                                           
                                       
 )                                               
 SELECT                                                                  
 @TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@To_Driver_Id,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,@DRIVERCODE,                                                                 
 DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_HOME_PHONE,                                     
 DRIVER_BUSINESS_PHONE,DRIVER_EXT,DRIVER_MOBILE,DRIVER_DOB,DRIVER_SSN,DRIVER_MART_STAT,DRIVER_SEX,DRIVER_DRIV_LIC,                                                            
 DRIVER_LIC_STATE,DRIVER_LIC_CLASS,DATE_EXP_START,DATE_LICENSED,DRIVER_REL,DRIVER_DRIV_TYPE,DRIVER_OCC_CODE,                                                                  
 DRIVER_OCC_CLASS,DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,DRIVER_INCOME,DRIVER_BROADEND_NOFAULT,                                      
 DRIVER_PHYS_MED_IMPAIRE,DRIVER_DRINK_VIOLATION,DRIVER_PREF_RISK,DRIVER_GOOD_STUDENT,                
 DRIVER_STUD_DIST_OVER_HUNDRED,DRIVER_LIC_SUSPENDED,DRIVER_VOLUNTEER_POLICE_FIRE,DRIVER_US_CITIZEN,'Y',                
 @CREATED_BY_USER_ID,GETDATE(),DRIVER_FAX,RELATIONSHIP,DRIVER_TITLE,Good_Driver_Student_Discount,                  
 Premier_Driver_Discount,Safe_Driver_Renewal_Discount,Vehicle_ID,Percent_Driven, Mature_Driver,                 
 Mature_Driver_Discount,Preferred_Risk_Discount,Preferred_Risk, TransferExp_Renewal_Discount,                
 TransferExperience_RenewalCredit,SAFE_DRIVER,NO_DEPENDENTS,NO_CYCLE_ENDMT,APP_VEHICLE_PRIN_OCC_ID,WAIVER_WORK_LOSS_BENEFITS,                 
 FULL_TIME_STUDENT,SUPPORT_DOCUMENT,SIGNED_WAIVER_BENEFITS_FORM,HAVE_CAR,IN_MILITARY,STATIONED_IN_US_TERR,                 
 PARENTS_INSURANCE,CYCL_WITH_YOU,COLL_STUD_AWAY_HOME,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,                          
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                          
 MVR_STATUS,MVR_REMARKS,FORM_F95,EXT_NON_OWN_COVG_INDIVI --Added for Itrack issue 5673                                                                                               
 FROM    APP_DRIVER_DETAILS                                                                  
 WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                                        
 AND         APP_ID=@FROM_APP_ID                                                                  
 AND         APP_VERSION_ID=@FROM_APP_VERSION_ID                                                                  
 AND         DRIVER_ID= @FROM_DRIVER_ID                            
                                  
                             
 --If the driver copied belongs to application different from current, add default values at driver-vehicle assign table                            
                             
 INSERT INTO APP_DRIVER_ASSIGNED_VEHICLE                                                                  
 (                                                  
 CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,VEHICLE_ID,APP_VEHICLE_PRIN_OCC_ID                                 
 )                                                                  
 SELECT                                                             
 @TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@To_Driver_Id,VEHICLE_ID,11931--'Not Rated'               
 FROM    APP_VEHICLES                                                                  
 WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID                                                                  
 AND         APP_ID=@TO_APP_ID                                                                  
 AND         APP_VERSION_ID=@TO_APP_VERSION_ID                                             
 --AND         DRIVER_ID= @FROM_DRIVER_ID                                    
                             
                                     
                                    
 END                   
   
 INSERT INTO APP_MVR_INFORMATION                           
 (                                            
 APP_MVR_ID,CUSTOMER_ID,APP_ID, APP_VERSION_ID,VIOLATION_ID,DRIVER_ID,                                                  
 MVR_AMOUNT, MVR_DEATH,MVR_DATE,IS_ACTIVE,VERIFIED,VIOLATION_TYPE,                      
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                      
 OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                      
 --,CREATED_BY,CREATED_DATETIME                          
 )                        
 SELECT                                                
 APP_MVR_ID,@TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,VIOLATION_ID,@TO_DRIVER_ID,                           MVR_AMOUNT,MVR_DEATH,MVR_DATE,IS_ACTIVE,VERIFIED,VIOLATION_TYPE,                       
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                      
 OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                       
 --,CREATED_BY,GETDATE()                        
 FROM  APP_MVR_INFORMATION                   
                
 WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID                   
 AND APP_ID=@FROM_APP_ID                   
 AND APP_VERSION_ID=@FROM_APP_VERSION_ID                   
 AND DRIVER_ID=@FROM_DRIVER_ID                                                                   
                                     
                    
 END                                                
  ELSE                                                   
                     
  IF (@TEMP_FROM_LOB_ID='4'AND @TEMP_TO_LOB_ID IN (2,3))                    
  BEGIN                                
  SET @DRIVERCODE=CONVERT(INT,RAND()*50)               
   SELECT @FIRSTCHAR=DRIVER_FNAME, @LASTCHAR=DRIVER_LNAME  FROM APP_WATERCRAFT_DRIVER_DETAILS                                                       
   WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                                            
    AND         APP_ID=@FROM_APP_ID                                           
    AND         APP_VERSION_ID=@FROM_APP_VERSION_ID                                                  
    AND         DRIVER_ID= @FROM_DRIVER_ID                                                           
 SET @DRIVERCODE=(SUBSTRING(@FIRSTCHAR,1,3) + SUBSTRING(@LASTCHAR,1,4) + @DRIVERCODE)                                
                             
 --SELECT @DRIVER_TYPE=ISNULL(DRIVER_DRIV_TYPE,0) FROM APP_DRIVER_DETAILS                             
 --  WHERE CUSTOMER_ID=@TO_CUSTOMER_ID AND APP_ID=@TO_APP_ID AND APP_VERSION_ID=@TO_APP_VERSION_ID                            
                                                                   
                                                     
 IF (@TO_CUSTOMER_ID=@FROM_CUSTOMER_ID and @TO_APP_ID=@FROM_APP_ID and @TO_APP_VERSION_ID=@FROM_APP_VERSION_ID)                                                    
 BEGIN                                                   
 INSERT INTO APP_DRIVER_DETAILS                                                                  
 (                                                                  
 CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,DRIVER_SUFFIX,  DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_DOB,DRIVER_SSN,                
 DRIVER_SEX,DRIVER_MART_STAT,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_DRIV_TYPE,IS_ACTIVE,CREATED_BY,                  
 CREATED_DATETIME,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,                     
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                          
 MVR_STATUS,MVR_REMARKS                                         
                                       
 )                    
                
 SELECT                                                               
 @TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@TO_DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,@DRIVERCODE,                  
 DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_DOB,DRIVER_SSN,                 
 DRIVER_SEX,MARITAL_STATUS,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_DRIV_TYPE,'Y',@CREATED_BY_USER_ID,GETDATE(),                 
 DATE_ORDERED,MVR_ORDERED,VIOLATIONS,                  
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                        
 MVR_STATUS,MVR_REMARKS                  
                                 
 FROM    APP_WATERCRAFT_DRIVER_DETAILS                                                                   
 WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                                                  
 AND         APP_ID=@FROM_APP_ID                                                                  
 AND         APP_VERSION_ID=@FROM_APP_VERSION_ID                                                                  
 AND         DRIVER_ID= @FROM_DRIVER_ID                                       
                                     
                                     
                                     
                                     
                                     
 INSERT INTO APP_DRIVER_ASSIGNED_VEHICLE                                                                  
 (                                                          
 CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,VEHICLE_ID,APP_VEHICLE_PRIN_OCC_ID                                 
 )                                                                  
 SELECT                                                             
 @TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@To_Driver_Id,VEHICLE_ID,11931--'Not Rated'                                         
 FROM    APP_VEHICLES                                                                  
 WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID                                                                  
 AND         APP_ID=@TO_APP_ID                                                                  
 AND         APP_VERSION_ID=@TO_APP_VERSION_ID                                 
 ---                                    
                                     
 END                                               
 ELSE                                               
 BEGIN                                                  
 INSERT INTO APP_DRIVER_DETAILS                                                                  
 (                                                                  
 CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,DRIVER_SUFFIX,  DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_DOB,DRIVER_SSN,                
 DRIVER_SEX,DRIVER_MART_STAT,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_DRIV_TYPE,IS_ACTIVE,CREATED_BY,                  
 CREATED_DATETIME,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,                          
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                          
 MVR_STATUS,MVR_REMARKS                                         
                                       
 )                    
                
 SELECT                                                               
 @TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@TO_DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,@DRIVERCODE,                  
 DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_DOB,DRIVER_SSN,                 
 DRIVER_SEX,MARITAL_STATUS,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_DRIV_TYPE,'Y',@CREATED_BY_USER_ID,GETDATE(),                 
 DATE_ORDERED,MVR_ORDERED,VIOLATIONS,                  
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                        
 MVR_STATUS,MVR_REMARKS                  
                                 
 FROM    APP_WATERCRAFT_DRIVER_DETAILS      
 WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                                                  
 AND         APP_ID=@FROM_APP_ID                                                                  
 AND         APP_VERSION_ID=@FROM_APP_VERSION_ID                                                                  
 AND         DRIVER_ID= @FROM_DRIVER_ID                                                                  
                                               
                             
 --If the driver copied belongs to application different from current, add default values at driver-vehicle assign table                            
                             
 INSERT INTO APP_DRIVER_ASSIGNED_VEHICLE                                                                  
 (                                                  
 CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,VEHICLE_ID,APP_VEHICLE_PRIN_OCC_ID                                 
 )                                                                  
 SELECT                                                             
 @TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@To_Driver_Id,VEHICLE_ID,11931--'Not Rated'                                         
 FROM    APP_VEHICLES                                                                  
 WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID                                                                  
 AND         APP_ID=@TO_APP_ID                                                                  
 AND         APP_VERSION_ID=@TO_APP_VERSION_ID                                         
 --AND         DRIVER_ID= @FROM_DRIVER_ID                                    
                             
   END                 
              
   DECLARE @WATER_MVR_ID int,@WATER_OLD_VIOLATION_ID int,@VIOLATION_TYPE INT                  
   DECLARE @VIOLATION_CODE NVARCHAR(10)          
                    
   DECLARE CurWATER_MVR_INSERT CURSOR                    
   FOR SELECT APP_WATER_MVR_ID,VIOLATION_ID,VIOLATION_TYPE FROM APP_WATER_MVR_INFORMATION                      
   WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID AND APP_ID=@FROM_APP_ID AND APP_VERSION_ID=@FROM_APP_VERSION_ID                    
   AND DRIVER_ID=@FROM_DRIVER_ID                      
            
 OPEN CurWATER_MVR_INSERT                    
                    
   FETCH NEXT FROM CurWATER_MVR_INSERT INTO @WATER_MVR_ID,@WATER_OLD_VIOLATION_ID,@VIOLATION_TYPE               
                
  IF(@VIOLATION_TYPE IN (SELECT VIOLATION_ID FROM MNT_VIOLATIONS where VIOLATION_ID > =15000            
                         and VIOLATION_CODE!='SUSPN' ))          
  BEGIN-----VIOLATION_TYPE AVAILABLE,VIOLATION_ID NOT AVAILABLE -- Done BY Sibin on 12 Feb 09 for Itrack Issue 5304 to enable copying of MVR             
    WHILE (@@FETCH_STATUS=0 )                    
    BEGIN                    
                     
  SELECT @TEMP_VIOLATION_TYPE=VIOLATION_ID FROM MNT_VIOLATIONS                       
  WHERE LOB=@TEMP_TO_LOB_ID AND STATE=@TEMP_STATE_ID                     
  AND VIOLATION_CODE=(SELECT VIOLATION_CODE FROM MNT_VIOLATIONS WHERE VIOLATION_ID=@VIOLATION_TYPE)           
              
  INSERT INTO APP_MVR_INFORMATION                               
  (                                                
  APP_MVR_ID,CUSTOMER_ID,APP_ID, APP_VERSION_ID,VIOLATION_ID,DRIVER_ID,                                                      
  MVR_AMOUNT,MVR_DEATH,MVR_DATE,IS_ACTIVE,VERIFIED,VIOLATION_TYPE,                          
  --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                          
  OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                          
  --,CREATED_BY,CREATED_DATETIME                              
  )                            
  SELECT                                                    
  APP_WATER_MVR_ID,@TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,0,@TO_DRIVER_ID,          
  MVR_AMOUNT,MVR_DEATH,MVR_DATE,IS_ACTIVE,VERIFIED,@TEMP_VIOLATION_TYPE,                           
  --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                          
  OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                           
  --,CREATED_BY,GETDATE()                            
  FROM  APP_WATER_MVR_INFORMATION                       
                      
  WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID                       
  AND APP_ID=@FROM_APP_ID                       
  AND APP_VERSION_ID=@FROM_APP_VERSION_ID                       
  AND DRIVER_ID=@FROM_DRIVER_ID                                                                       
  AND APP_WATER_MVR_ID  = @WATER_MVR_ID                    
                          
  FETCH NEXT FROM CurWATER_MVR_INSERT INTO @WATER_MVR_ID,@WATER_OLD_VIOLATION_ID,@VIOLATION_TYPE                   
  END                    
   CLOSE CurWATER_MVR_INSERT                    
   DEALLOCATE CurWATER_MVR_INSERT                    
   END            
  --VIOLATION_TYPE,VIOLATION_ID BOTH AVAILABLE -- Done BY Sibin on 12 Feb 09 for Itrack Issue 5304 to enable copying of MVR          
   ELSE          
   BEGIN          
                   
                     
    WHILE (@@FETCH_STATUS=0 )                    
    BEGIN                          
          
  SELECT  @TEMP_VIOLATION_TYPE=VIOLATION_ID FROM MNT_VIOLATIONS                       
  WHERE VIOLATION_GROUP=                    
   (SELECT VIOLATION_PARENT FROM MNT_VIOLATIONS WHERE VIOLATION_ID=@WATER_OLD_VIOLATION_ID)                    
  AND LOB=@TEMP_TO_LOB_ID AND STATE=@TEMP_STATE_ID                     
                      
  SELECT @TEMP_VIOLATION_ID=VIOLATION_ID FROM MNT_VIOLATIONS                       
  WHERE VIOLATION_PARENT=(SELECT VIOLATION_GROUP FROM MNT_VIOLATIONS                     
  WHERE VIOLATION_ID= @TEMP_VIOLATION_TYPE)                    
  AND LOB=@TEMP_TO_LOB_ID AND STATE=@TEMP_STATE_ID                     
  AND VIOLATION_CODE=(SELECT VIOLATION_CODE FROM MNT_VIOLATIONS WHERE VIOLATION_ID=@WATER_OLD_VIOLATION_ID)                    
   --SELECT  @TEMP_VIOLATION_TYPE,@TEMP_VIOLATION_ID ,@VIOLATION_CODE          
                 
  INSERT INTO APP_MVR_INFORMATION                               
  (                                                
  APP_MVR_ID,CUSTOMER_ID,APP_ID, APP_VERSION_ID,VIOLATION_ID,DRIVER_ID,                                                      
  MVR_AMOUNT,MVR_DEATH,MVR_DATE,IS_ACTIVE,VERIFIED,VIOLATION_TYPE,                          
  --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                          
  OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                          
  --,CREATED_BY,CREATED_DATETIME                              
  )                            
  SELECT                                                    
  APP_WATER_MVR_ID,@TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@TEMP_VIOLATION_ID,@TO_DRIVER_ID,                     MVR_AMOUNT,MVR_DEATH,MVR_DATE,IS_ACTIVE,VERIFIED,@TEMP_VIOLATION_TYPE,                           
  --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                          
  OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                           
  --,CREATED_BY,GETDATE()                            
  FROM  APP_WATER_MVR_INFORMATION                       
                      
  WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID                       
  AND APP_ID=@FROM_APP_ID                       
  AND APP_VERSION_ID=@FROM_APP_VERSION_ID                       
  AND DRIVER_ID=@FROM_DRIVER_ID                                                                       
  AND APP_WATER_MVR_ID  = @WATER_MVR_ID                    
                          
  FETCH NEXT FROM CurWATER_MVR_INSERT INTO @WATER_MVR_ID,@WATER_OLD_VIOLATION_ID ,@VIOLATION_TYPE                   
  END                    
   CLOSE CurWATER_MVR_INSERT                    
   DEALLOCATE CurWATER_MVR_INSERT                    
  END                        
    END                    
   -------- If Lob other than Watercraft                    
  ELSE                   
 BEGIN              
    SET @DRIVERCODE=CONVERT(INT,RAND()*50)               
   SELECT @FIRSTCHAR=DRIVER_FNAME, @LASTCHAR=DRIVER_LNAME  FROM APP_DRIVER_DETAILS                                                       
   WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                                            
    AND         APP_ID=@FROM_APP_ID                                           
    AND         APP_VERSION_ID=@FROM_APP_VERSION_ID                                                  
    AND         DRIVER_ID= @FROM_DRIVER_ID                                            
 SET @DRIVERCODE=(SUBSTRING(@FIRSTCHAR,1,3) + SUBSTRING(@LASTCHAR,1,4) + @DRIVERCODE)              
                   
    IF (@TO_CUSTOMER_ID=@FROM_CUSTOMER_ID and @TO_APP_ID=@FROM_APP_ID and @TO_APP_VERSION_ID=@FROM_APP_VERSION_ID)                                                    
 BEGIN                                                                                    
 INSERT INTO APP_DRIVER_DETAILS                                                                  
 (                                                                  
 CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,DRIVER_SUFFIX,  DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_HOME_PHONE,                    
 DRIVER_BUSINESS_PHONE,DRIVER_EXT,DRIVER_MOBILE,DRIVER_DOB,DRIVER_SSN,DRIVER_MART_STAT,DRIVER_SEX,DRIVER_DRIV_LIC, DRIVER_LIC_STATE,DRIVER_LIC_CLASS,DATE_EXP_START,DATE_LICENSED,DRIVER_REL,DRIVER_DRIV_TYPE,DRIVER_OCC_CODE,                 
 DRIVER_OCC_CLASS,DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,DRIVER_INCOME,DRIVER_BROADEND_NOFAULT,                  
 DRIVER_PHYS_MED_IMPAIRE,DRIVER_DRINK_VIOLATION,DRIVER_PREF_RISK,DRIVER_GOOD_STUDENT,      
 DRIVER_STUD_DIST_OVER_HUNDRED,DRIVER_LIC_SUSPENDED,DRIVER_VOLUNTEER_POLICE_FIRE,DRIVER_US_CITIZEN,IS_ACTIVE,  CREATED_BY,CREATED_DATETIME,DRIVER_FAX,RELATIONSHIP,DRIVER_TITLE,Good_Driver_Student_Discount,                  
 Premier_Driver_Discount,Safe_Driver_Renewal_Discount,Vehicle_ID,Percent_Driven,Mature_Driver,                 
 Mature_Driver_Discount,Preferred_Risk_Discount,Preferred_Risk, TransferExp_Renewal_Discount,                 
 TransferExperience_RenewalCredit,SAFE_DRIVER,NO_DEPENDENTS,NO_CYCLE_ENDMT,APP_VEHICLE_PRIN_OCC_ID,WAIVER_WORK_LOSS_BENEFITS,                 
 FULL_TIME_STUDENT,SUPPORT_DOCUMENT,                
 SIGNED_WAIVER_BENEFITS_FORM,                                        
 HAVE_CAR,IN_MILITARY,STATIONED_IN_US_TERR,PARENTS_INSURANCE,CYCL_WITH_YOU,COLL_STUD_AWAY_HOME,DATE_ORDERED,                  
 MVR_ORDERED,VIOLATIONS,                          
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                          
 MVR_STATUS,MVR_REMARKS,FORM_F95,EXT_NON_OWN_COVG_INDIVI --Added for Itrack issue 5673                                           
                                      
 )                                                                  
 SELECT                                                                  
 @TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@To_Driver_Id,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,@DRIVERCODE,                                                                 
 DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_HOME_PHONE,                                     
 DRIVER_BUSINESS_PHONE,DRIVER_EXT,DRIVER_MOBILE,DRIVER_DOB,DRIVER_SSN,DRIVER_MART_STAT,DRIVER_SEX,DRIVER_DRIV_LIC,                                                            
 DRIVER_LIC_STATE,DRIVER_LIC_CLASS,DATE_EXP_START,DATE_LICENSED,DRIVER_REL,DRIVER_DRIV_TYPE,DRIVER_OCC_CODE,              
 DRIVER_OCC_CLASS,DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,DRIVER_INCOME,DRIVER_BROADEND_NOFAULT,                         
 DRIVER_PHYS_MED_IMPAIRE,DRIVER_DRINK_VIOLATION,DRIVER_PREF_RISK,DRIVER_GOOD_STUDENT,                
 DRIVER_STUD_DIST_OVER_HUNDRED,DRIVER_LIC_SUSPENDED,DRIVER_VOLUNTEER_POLICE_FIRE,DRIVER_US_CITIZEN,'Y',                
 @CREATED_BY_USER_ID,GETDATE(),DRIVER_FAX,RELATIONSHIP,DRIVER_TITLE,Good_Driver_Student_Discount,                  
 Premier_Driver_Discount,Safe_Driver_Renewal_Discount,Vehicle_ID,Percent_Driven, Mature_Driver,                 
 Mature_Driver_Discount,Preferred_Risk_Discount,Preferred_Risk, TransferExp_Renewal_Discount,                
 TransferExperience_RenewalCredit,SAFE_DRIVER,NO_DEPENDENTS,NO_CYCLE_ENDMT,APP_VEHICLE_PRIN_OCC_ID,WAIVER_WORK_LOSS_BENEFITS,                 
 FULL_TIME_STUDENT,SUPPORT_DOCUMENT,SIGNED_WAIVER_BENEFITS_FORM,HAVE_CAR,IN_MILITARY,STATIONED_IN_US_TERR,                 
 PARENTS_INSURANCE,CYCL_WITH_YOU,COLL_STUD_AWAY_HOME,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,                    
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                          
 MVR_STATUS,MVR_REMARKS,FORM_F95,EXT_NON_OWN_COVG_INDIVI --Added for Itrack issue 5673                                                                                               
 FROM    APP_DRIVER_DETAILS                                                                  
 WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                                                  
 AND         APP_ID=@FROM_APP_ID                                                                  
 AND         APP_VERSION_ID=@FROM_APP_VERSION_ID                                                                  
 AND         DRIVER_ID= @FROM_DRIVER_ID                                                                  
                                     
                                    
 INSERT INTO APP_DRIVER_ASSIGNED_VEHICLE                                                                  
 (                                                          
 CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,VEHICLE_ID,APP_VEHICLE_PRIN_OCC_ID                                 
 )                                                                  
 SELECT                                 
 @TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@To_Driver_Id,VEHICLE_ID,11931--'Not Rated'                                         
 FROM    APP_VEHICLES                                                                  
 WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID                                                                  
 AND         APP_ID=@TO_APP_ID                                                                  
 AND         APP_VERSION_ID=@TO_APP_VERSION_ID                                 
 ---                                    
                          
 END                                                
 ELSE                                               
    BEGIN                                   
 INSERT INTO APP_DRIVER_DETAILS                                                                  
 (                                                                  
 CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,DRIVER_SUFFIX,  DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_HOME_PHONE,                    
 DRIVER_BUSINESS_PHONE,DRIVER_EXT,DRIVER_MOBILE,DRIVER_DOB,DRIVER_SSN,DRIVER_MART_STAT,DRIVER_SEX,DRIVER_DRIV_LIC, DRIVER_LIC_STATE,DRIVER_LIC_CLASS,DATE_EXP_START,DATE_LICENSED,DRIVER_REL,DRIVER_DRIV_TYPE,DRIVER_OCC_CODE,                 
 DRIVER_OCC_CLASS,DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,DRIVER_INCOME,DRIVER_BROADEND_NOFAULT,                  
 DRIVER_PHYS_MED_IMPAIRE,DRIVER_DRINK_VIOLATION,DRIVER_PREF_RISK,DRIVER_GOOD_STUDENT,                
 DRIVER_STUD_DIST_OVER_HUNDRED,DRIVER_LIC_SUSPENDED,DRIVER_VOLUNTEER_POLICE_FIRE,DRIVER_US_CITIZEN,IS_ACTIVE,  CREATED_BY,CREATED_DATETIME,DRIVER_FAX,RELATIONSHIP,DRIVER_TITLE,Good_Driver_Student_Discount,                  
 Premier_Driver_Discount,Safe_Driver_Renewal_Discount,Vehicle_ID,Percent_Driven,Mature_Driver,                 
 Mature_Driver_Discount,Preferred_Risk_Discount,Preferred_Risk, TransferExp_Renewal_Discount,                 
 TransferExperience_RenewalCredit,SAFE_DRIVER,NO_DEPENDENTS,NO_CYCLE_ENDMT,APP_VEHICLE_PRIN_OCC_ID,WAIVER_WORK_LOSS_BENEFITS,                 
 FULL_TIME_STUDENT,SUPPORT_DOCUMENT,                
 SIGNED_WAIVER_BENEFITS_FORM,                                        
 HAVE_CAR,IN_MILITARY,STATIONED_IN_US_TERR,PARENTS_INSURANCE,CYCL_WITH_YOU,COLL_STUD_AWAY_HOME,DATE_ORDERED,                  
 MVR_ORDERED,VIOLATIONS,                          
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                          
 MVR_STATUS,MVR_REMARKS,FORM_F95,EXT_NON_OWN_COVG_INDIVI --Added for Itrack issue 5673                                          
                                       
 )                                                                  
 SELECT                  
 @TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@To_Driver_Id,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,@DRIVERCODE,                                                                 
 DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_HOME_PHONE,                                     
 DRIVER_BUSINESS_PHONE,DRIVER_EXT,DRIVER_MOBILE,DRIVER_DOB,DRIVER_SSN,DRIVER_MART_STAT,DRIVER_SEX,        
 DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_LIC_CLASS,DATE_EXP_START,DATE_LICENSED,DRIVER_REL,        
 DRIVER_DRIV_TYPE,DRIVER_OCC_CODE,DRIVER_OCC_CLASS,DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,        
 DRIVER_INCOME,DRIVER_BROADEND_NOFAULT,                                                                  
 DRIVER_PHYS_MED_IMPAIRE,DRIVER_DRINK_VIOLATION,DRIVER_PREF_RISK,DRIVER_GOOD_STUDENT,                
 DRIVER_STUD_DIST_OVER_HUNDRED,DRIVER_LIC_SUSPENDED,DRIVER_VOLUNTEER_POLICE_FIRE,DRIVER_US_CITIZEN,'Y',                
 @CREATED_BY_USER_ID,GETDATE(),DRIVER_FAX,RELATIONSHIP,DRIVER_TITLE,Good_Driver_Student_Discount,                  
 Premier_Driver_Discount,Safe_Driver_Renewal_Discount,Vehicle_ID,Percent_Driven, Mature_Driver,                 
 Mature_Driver_Discount,Preferred_Risk_Discount,Preferred_Risk, TransferExp_Renewal_Discount,                
 TransferExperience_RenewalCredit,SAFE_DRIVER,NO_DEPENDENTS,NO_CYCLE_ENDMT,APP_VEHICLE_PRIN_OCC_ID,WAIVER_WORK_LOSS_BENEFITS,                 
 FULL_TIME_STUDENT,SUPPORT_DOCUMENT,SIGNED_WAIVER_BENEFITS_FORM,HAVE_CAR,IN_MILITARY,STATIONED_IN_US_TERR,                 
 PARENTS_INSURANCE,CYCL_WITH_YOU,COLL_STUD_AWAY_HOME,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,                          
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                          
 MVR_STATUS,MVR_REMARKS,FORM_F95,EXT_NON_OWN_COVG_INDIVI --Added for Itrack issue 5673                                                                                              
 FROM    APP_DRIVER_DETAILS                                                                  
 WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                                                  
 AND         APP_ID=@FROM_APP_ID                                                                  
 AND         APP_VERSION_ID=@FROM_APP_VERSION_ID                                                                  
 AND         DRIVER_ID= @FROM_DRIVER_ID                            
                                  
                             
 --If the driver copied belongs to application different from current, add default values at driver-vehicle assign table                            
                             
 INSERT INTO APP_DRIVER_ASSIGNED_VEHICLE                                                                  
 (    
 CUSTOMER_ID,APP_ID,APP_VERSION_ID,DRIVER_ID,VEHICLE_ID,APP_VEHICLE_PRIN_OCC_ID                                 
 )                        
 SELECT                                                             
 @TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@To_Driver_Id,VEHICLE_ID,11931--'Not Rated'                                         
 FROM    APP_VEHICLES                                                                  
 WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID                                                                  
 AND         APP_ID=@TO_APP_ID                                                                  
 AND         APP_VERSION_ID=@TO_APP_VERSION_ID                                             
 --AND         DRIVER_ID= @FROM_DRIVER_ID                                    
                             
                                     
                                                                
 END                   
                                 
   DECLARE @MVR_ID int,@OLD_VIOLATION_ID INT                    
   DECLARE CurMVR_INSERT CURSOR                    
   FOR SELECT APP_MVR_ID,VIOLATION_ID,VIOLATION_TYPE FROM APP_MVR_INFORMATION                     
   WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID AND APP_ID=@FROM_APP_ID AND APP_VERSION_ID=@FROM_APP_VERSION_ID                    
   AND DRIVER_ID=@FROM_DRIVER_ID                       
                     
   OPEN CurMVR_INSERT                    
                   
   FETCH NEXT FROM CurMVR_INSERT INTO @MVR_ID,@OLD_VIOLATION_ID,@VIOLATION_TYPE                               
   WHILE (@@FETCH_STATUS=0 )                    
    BEGIN                    
       BEGIN              
   IF(@VIOLATION_TYPE IN (SELECT VIOLATION_ID FROM MNT_VIOLATIONS where VIOLATION_ID > =15000                  
                         and VIOLATION_CODE!='SUSPN' ))                
  BEGIN-----VIOLATION_TYPE AVAILABLE,VIOLATION_ID NOT AVAILABLE -- Done By Sibin on 12 Feb 09 for Itrack Issue 5304 to enable copying of MVR      
 SELECT @TEMP_VIOLATION_TYPE=VIOLATION_ID FROM MNT_VIOLATIONS                             
   WHERE LOB=@TEMP_TO_LOB_ID AND STATE=@TEMP_STATE_ID                           
   AND VIOLATION_CODE=(SELECT VIOLATION_CODE FROM MNT_VIOLATIONS WHERE VIOLATION_ID=@VIOLATION_TYPE)      
       
 INSERT INTO APP_MVR_INFORMATION                               
    (                                                
    APP_MVR_ID,CUSTOMER_ID,APP_ID, APP_VERSION_ID,VIOLATION_ID,DRIVER_ID,                                                      
    MVR_AMOUNT, MVR_DEATH,MVR_DATE,IS_ACTIVE,VERIFIED,VIOLATION_TYPE,                          
    --Added by Sibin on 16 Jan 09 for Itrack Issue 5304               
    OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                          
    --,CREATED_BY,CREATED_DATETIME                              
    )                            
    SELECT                                    
    APP_MVR_ID,@TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,0,@TO_DRIVER_ID, MVR_AMOUNT,MVR_DEATH,      
 MVR_DATE,IS_ACTIVE,VERIFIED,@TEMP_VIOLATION_TYPE,                           
    --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                          
    OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                           
    --,CREATED_BY,GETDATE()                            
    FROM  APP_MVR_INFORMATION                       
                     
    WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID                       
    AND APP_ID=@FROM_APP_ID                       
    AND APP_VERSION_ID=@FROM_APP_VERSION_ID                       
    AND DRIVER_ID=@FROM_DRIVER_ID                                                                       
    AND APP_MVR_ID  = @MVR_ID                    
                        
    FETCH NEXT FROM CurMVR_INSERT INTO @MVR_ID,@OLD_VIOLATION_ID,@VIOLATION_TYPE       
  END        
  ELSE      
   BEGIN         
    SELECT  @TEMP_VIOLATION_TYPE=VIOLATION_ID FROM MNT_VIOLATIONS                       
    WHERE VIOLATION_GROUP=                
     (SELECT VIOLATION_PARENT FROM MNT_VIOLATIONS WHERE VIOLATION_ID=@OLD_VIOLATION_ID)                    
    AND LOB=@TEMP_TO_LOB_ID AND STATE=@TEMP_STATE_ID                     
                     
    SELECT @TEMP_VIOLATION_ID=VIOLATION_ID FROM MNT_VIOLATIONS                       
    WHERE VIOLATION_PARENT=(SELECT VIOLATION_GROUP FROM MNT_VIOLATIONS                     
    WHERE VIOLATION_ID= @TEMP_VIOLATION_TYPE)                    
    AND LOB=@TEMP_TO_LOB_ID AND STATE=@TEMP_STATE_ID                     
    AND VIOLATION_CODE=(SELECT VIOLATION_CODE FROM MNT_VIOLATIONS WHERE VIOLATION_ID=@OLD_VIOLATION_ID)                               
    INSERT INTO APP_MVR_INFORMATION                               
    (                                                
    APP_MVR_ID,CUSTOMER_ID,APP_ID, APP_VERSION_ID,VIOLATION_ID,DRIVER_ID,                                                      
    MVR_AMOUNT, MVR_DEATH,MVR_DATE,IS_ACTIVE,VERIFIED,VIOLATION_TYPE,                          
    --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                          
    OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                          
    --,CREATED_BY,CREATED_DATETIME                             
    )                            
    SELECT                                    
    APP_MVR_ID,@TO_CUSTOMER_ID,@TO_APP_ID,@TO_APP_VERSION_ID,@TEMP_VIOLATION_ID,@TO_DRIVER_ID,                           MVR_AMOUNT,MVR_DEATH,MVR_DATE,IS_ACTIVE,VERIFIED,@TEMP_VIOLATION_TYPE,                           
    --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                          
    OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                           
    --,CREATED_BY,GETDATE()                            
    FROM  APP_MVR_INFORMATION                       
                     
    WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID                       
    AND APP_ID=@FROM_APP_ID                       
    AND APP_VERSION_ID=@FROM_APP_VERSION_ID                       
    AND DRIVER_ID=@FROM_DRIVER_ID                                                                       
    AND APP_MVR_ID  = @MVR_ID                    
                        
    FETCH NEXT FROM CurMVR_INSERT INTO @MVR_ID,@OLD_VIOLATION_ID,@VIOLATION_TYPE                    
    END        
  END                    
                
--Added till here                             
   END       
 CLOSE CurMVR_INSERT                    
  DEALLOCATE CurMVR_INSERT                   
  END       
END


GO

