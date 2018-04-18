IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyExistingDriver]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyExistingDriver]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran  
--drop proc Proc_InsertPolicyExistingDriver  
--go  
  
/*----------------------------------------------------------                                                    
Proc Name       : dbo.Proc_InsertPolicyExistingDriver                                                    
Used In  : Wolverine                                                    
------------------------------------------------------------                                                    
Date     Review By          Comments                                                    
------   ------------       -------------------------*/                        
--drop proc Proc_InsertPolicyExistingDriver                                         
CREATE  PROCEDURE [dbo].[Proc_InsertPolicyExistingDriver]                  
(                                                            
 @TO_CUSTOMER_ID int,                                                            
 @TO_POLICY_ID int,                                                            
 @TO_POLICY_VERSION_ID int,                                                            
 @FROM_CUSTOMER_ID int,                                                            
 @FROM_POLICY_ID int,                                                            
 @FROM_POLICY_VERSION_ID int,                                                            
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
--declare @DRIVER_TYPE int                                                           
BEGIN                                                            
 Declare @To_Driver_Id int   --new driver id being generated                                                             
 SELECT  @To_Driver_Id = ISNULL(MAX(DRIVER_ID),0) + 1                                                            
 FROM     POL_DRIVER_DETAILS                                                             
 WHERE  CUSTOMER_ID=@TO_CUSTOMER_ID                                                            
 AND         POLICY_ID=@TO_POLICY_ID                                                            
 AND         POLICY_VERSION_ID=@TO_POLICY_VERSION_ID                                                               
                                                             
                                                             
                  
                                                   
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
          
 SELECT @TEMP_STATE_ID=STATE_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@TO_CUSTOMER_ID                         
    AND POLICY_ID=@TO_POLICY_ID AND POLICY_VERSION_ID=@TO_POLICY_VERSION_ID                                                                         
                                   
 SELECT @TEMP_TO_LOB_ID=POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@TO_CUSTOMER_ID                         
 AND POLICY_ID=@TO_POLICY_ID AND POLICY_VERSION_ID=@TO_POLICY_VERSION_ID                             
                                 
 SELECT @TEMP_FROM_LOB_ID=POLICY_LOB FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID AND   POLICY_ID=@FROM_POLICY_ID AND POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                         
                                
 ----Added by Sibin on 27 Jan 09 for Itrack Issue 5304                                
 IF (@TEMP_FROM_LOB_ID = @TEMP_TO_LOB_ID)                                
 BEGIN                             
  SET @DRIVERCODE=CONVERT(INT,RAND()*50)                                                      
  SELECT @FIRSTCHAR=DRIVER_FNAME, @LASTCHAR=DRIVER_LNAME  FROM POL_DRIVER_DETAILS                                WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                                      
  AND   POLICY_ID=@FROM_POLICY_ID                                                      
  AND   POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                                                      
  AND   DRIVER_ID= @FROM_DRIVER_ID                                                     
    SET @DRIVERCODE=(SUBSTRING(@FIRSTCHAR,1,3) + SUBSTRING(@LASTCHAR,1,4) + @DRIVERCODE)                          
                                                                  
                            
 IF (@TO_CUSTOMER_ID=@FROM_CUSTOMER_ID and @TO_POLICY_ID=@FROM_POLICY_ID                         
 and @TO_POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID)                                              
 BEGIN                                             
   INSERT INTO POL_DRIVER_DETAILS                                                            
   (                                                            
  CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,                     
  DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_HOME_PHONE,                     
  DRIVER_BUSINESS_PHONE,DRIVER_EXT,DRIVER_MOBILE,DRIVER_DOB,DRIVER_SSN,DRIVER_MART_STAT,DRIVER_SEX,                        
  DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_LIC_CLASS,DATE_EXP_START,DATE_LICENSED,DRIVER_REL,DRIVER_DRIV_TYPE,                    
  DRIVER_OCC_CODE,DRIVER_OCC_CLASS,DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,                     
  DRIVER_INCOME,DRIVER_BROADEND_NOFAULT,DRIVER_PHYS_MED_IMPAIRE,DRIVER_DRINK_VIOLATION,                     
  DRIVER_PREF_RISK,DRIVER_GOOD_STUDENT,DRIVER_STUD_DIST_OVER_HUNDRED,DRIVER_LIC_SUSPENDED,                     
  DRIVER_VOLUNTEER_POLICE_FIRE,DRIVER_US_CITIZEN,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,                    
  DRIVER_FAX,RELATIONSHIP,DRIVER_TITLE,Good_Driver_Student_Discount,Premier_Driver_Discount,                     
  Safe_Driver_Renewal_Discount,Vehicle_ID,Percent_Driven, Mature_Driver,Mature_Driver_Discount,                    
  Preferred_Risk_Discount,Preferred_Risk,TransferExp_Renewal_Discount,TransferExperience_RenewalCredit,                    
  SAFE_DRIVER,NO_DEPENDENTS,APP_VEHICLE_PRIN_OCC_ID ,WAIVER_WORK_LOSS_BENEFITS,NO_CYCLE_ENDMT,FULL_TIME_STUDENT,                    
  SUPPORT_DOCUMENT,SIGNED_WAIVER_BENEFITS_FORM,HAVE_CAR,IN_MILITARY,STATIONED_IN_US_TERR,PARENTS_INSURANCE,                    
  CYCL_WITH_YOU,COLL_STUD_AWAY_HOME,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,                                      
  --Added by Sibin on 27 Jan 09 for Itrack Issue 5304                                      
 MVR_STATUS,MVR_REMARKS,FORM_F95,EXT_NON_OWN_COVG_INDIVI --Added for Itrack issue 5673                                                                                   
   )                                                                           
   SELECT                                                            
  @TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@To_Driver_Id,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,      @DRIVERCODE,DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,                      
  DRIVER_HOME_PHONE,DRIVER_BUSINESS_PHONE,DRIVER_EXT,DRIVER_MOBILE,DRIVER_DOB,DRIVER_SSN,DRIVER_MART_STAT,       DRIVER_SEX,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_LIC_CLASS,DATE_EXP_START,DATE_LICENSED,DRIVER_REL,                         
  DRIVER_DRIV_TYPE,DRIVER_OCC_CODE,DRIVER_OCC_CLASS,DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,                        
  DRIVER_INCOME,DRIVER_BROADEND_NOFAULT,DRIVER_PHYS_MED_IMPAIRE,DRIVER_DRINK_VIOLATION,DRIVER_PREF_RISK,                        
  DRIVER_GOOD_STUDENT,DRIVER_STUD_DIST_OVER_HUNDRED,DRIVER_LIC_SUSPENDED,DRIVER_VOLUNTEER_POLICE_FIRE,                        
  DRIVER_US_CITIZEN,'Y', @CREATED_BY_USER_ID,GETDATE(),DRIVER_FAX,RELATIONSHIP,DRIVER_TITLE,                          
  Good_Driver_Student_Discount,Premier_Driver_Discount,Safe_Driver_Renewal_Discount,Vehicle_ID,Percent_Driven,                   
  Mature_Driver,Mature_Driver_Discount,Preferred_Risk_Discount,Preferred_Risk, TransferExp_Renewal_Discount,                     
  TransferExperience_RenewalCredit,SAFE_DRIVER,NO_DEPENDENTS,APP_VEHICLE_PRIN_OCC_ID ,                                      
  WAIVER_WORK_LOSS_BENEFITS,NO_CYCLE_ENDMT,FULL_TIME_STUDENT,SUPPORT_DOCUMENT,SIGNED_WAIVER_BENEFITS_FORM,                                  
  HAVE_CAR,IN_MILITARY,STATIONED_IN_US_TERR,PARENTS_INSURANCE,CYCL_WITH_YOU,COLL_STUD_AWAY_HOME,DATE_ORDERED,                        
  MVR_ORDERED,VIOLATIONS,                                      
  --Added by Sibin on 27 Jan 09 for Itrack Issue 5304                                      
  MVR_STATUS,MVR_REMARKS,FORM_F95,EXT_NON_OWN_COVG_INDIVI --Added for Itrack issue 5673                                    
  FROM    POL_DRIVER_DETAILS                                                            
  WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                                            
  AND         POLICY_ID=@FROM_POLICY_ID                                                            
  AND         POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                                                            
  AND         DRIVER_ID= @FROM_DRIVER_ID                                   
                                
  ---                              
  INSERT INTO POL_DRIVER_ASSIGNED_VEHICLE                                                      
  (                                                            
  CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,VEHICLE_ID,APP_VEHICLE_PRIN_OCC_ID                                                     
  )                                                            
  SELECT                                                            
  CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,@To_Driver_Id,VEHICLE_ID,11931--'Not Rated'                          
  FROM    POL_VEHICLES                                                          
  WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID                                                            
  AND         POLICY_ID=@TO_POLICY_ID                                                            
  AND         POLICY_VERSION_ID=@TO_POLICY_VERSION_ID                                                  
   END  -- end  of @TO_CUSTOMER_ID=@FROM_CUSTOMER_ID and @TO_POLICY_ID=@FROM_POLICY_ID                
ELSE                                    
 BEGIN                                   
  INSERT INTO POL_DRIVER_DETAILS                                                            
  (                                                            
  CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,                     
  DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_HOME_PHONE,                     
  DRIVER_BUSINESS_PHONE,DRIVER_EXT,DRIVER_MOBILE,DRIVER_DOB,DRIVER_SSN,DRIVER_MART_STAT,DRIVER_SEX,                        
  DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_LIC_CLASS,DATE_EXP_START,DATE_LICENSED,DRIVER_REL,DRIVER_DRIV_TYPE,                    
  DRIVER_OCC_CODE,DRIVER_OCC_CLASS,DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,                     
  DRIVER_INCOME,DRIVER_BROADEND_NOFAULT,DRIVER_PHYS_MED_IMPAIRE,DRIVER_DRINK_VIOLATION,                     
  DRIVER_PREF_RISK,DRIVER_GOOD_STUDENT,DRIVER_STUD_DIST_OVER_HUNDRED,DRIVER_LIC_SUSPENDED,                     
  DRIVER_VOLUNTEER_POLICE_FIRE,DRIVER_US_CITIZEN,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,                    
  DRIVER_FAX,RELATIONSHIP,DRIVER_TITLE,Good_Driver_Student_Discount,Premier_Driver_Discount,                     
  Safe_Driver_Renewal_Discount,Vehicle_ID,Percent_Driven, Mature_Driver,Mature_Driver_Discount,                  
  Preferred_Risk_Discount,Preferred_Risk,TransferExp_Renewal_Discount,TransferExperience_RenewalCredit,                    
  SAFE_DRIVER,NO_DEPENDENTS,APP_VEHICLE_PRIN_OCC_ID ,WAIVER_WORK_LOSS_BENEFITS,NO_CYCLE_ENDMT,FULL_TIME_STUDENT,                    
  SUPPORT_DOCUMENT,SIGNED_WAIVER_BENEFITS_FORM,HAVE_CAR,IN_MILITARY,STATIONED_IN_US_TERR,PARENTS_INSURANCE,                    
  CYCL_WITH_YOU,COLL_STUD_AWAY_HOME,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,                                      
  --Added by Sibin on 27 Jan 09 for Itrack Issue 5304                                      
  MVR_STATUS,MVR_REMARKS,FORM_F95,EXT_NON_OWN_COVG_INDIVI --Added for Itrack issue 5673                                                                                      
  )                                                                                         
  SELECT                                                            
  @TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@To_Driver_Id,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,        @DRIVERCODE,DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,                      
  DRIVER_HOME_PHONE,DRIVER_BUSINESS_PHONE,DRIVER_EXT,DRIVER_MOBILE,DRIVER_DOB,DRIVER_SSN,DRIVER_MART_STAT,                       
  DRIVER_SEX,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_LIC_CLASS,DATE_EXP_START,DATE_LICENSED,DRIVER_REL,                         
  DRIVER_DRIV_TYPE,DRIVER_OCC_CODE,DRIVER_OCC_CLASS,DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,                        
  DRIVER_INCOME,DRIVER_BROADEND_NOFAULT,DRIVER_PHYS_MED_IMPAIRE,DRIVER_DRINK_VIOLATION,DRIVER_PREF_RISK,                        
  DRIVER_GOOD_STUDENT,DRIVER_STUD_DIST_OVER_HUNDRED,DRIVER_LIC_SUSPENDED,DRIVER_VOLUNTEER_POLICE_FIRE,                        
  DRIVER_US_CITIZEN,'Y', @CREATED_BY_USER_ID,GETDATE(),DRIVER_FAX,RELATIONSHIP,DRIVER_TITLE,                          
  Good_Driver_Student_Discount,Premier_Driver_Discount,Safe_Driver_Renewal_Discount,Vehicle_ID,Percent_Driven,                   
   Mature_Driver,Mature_Driver_Discount,Preferred_Risk_Discount,Preferred_Risk, TransferExp_Renewal_Discount,                    
  TransferExperience_RenewalCredit,SAFE_DRIVER,NO_DEPENDENTS,APP_VEHICLE_PRIN_OCC_ID ,                                      
  WAIVER_WORK_LOSS_BENEFITS,NO_CYCLE_ENDMT,FULL_TIME_STUDENT,SUPPORT_DOCUMENT,SIGNED_WAIVER_BENEFITS_FORM,                                  
  HAVE_CAR,IN_MILITARY,STATIONED_IN_US_TERR,PARENTS_INSURANCE,CYCL_WITH_YOU,COLL_STUD_AWAY_HOME,DATE_ORDERED,            
  MVR_ORDERED,VIOLATIONS,                                      
  --Added by Sibin on 27 Jan 09 for Itrack Issue 5304                                      
  MVR_STATUS,MVR_REMARKS,FORM_F95,EXT_NON_OWN_COVG_INDIVI --Added for Itrack issue 5673                                    
  FROM    POL_DRIVER_DETAILS                                                            
  WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                                            
  AND         POLICY_ID=@FROM_POLICY_ID                                                            
  AND         POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                                                            
  AND         DRIVER_ID= @FROM_DRIVER_ID                               
                         
                           
  INSERT INTO POL_DRIVER_ASSIGNED_VEHICLE                                                            
  (                                                            
  CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,VEHICLE_ID,APP_VEHICLE_PRIN_OCC_ID                                                     
  )                                                            
  SELECT                      
  CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,@To_Driver_Id,VEHICLE_ID,11931--'Not Rated'                          
  FROM    POL_VEHICLES                                                            
  WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID                                                            
  AND         POLICY_ID=@TO_POLICY_ID                                             
  AND         POLICY_VERSION_ID=@TO_POLICY_VERSION_ID                                                            
  --AND         DRIVER_ID= @FROM_DRIVER_ID                            
 END       -- end of else                  
-- copying MVR of Same LOB                   
 INSERT INTO POL_MVR_INFORMATION                           
 (                           
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,POL_MVR_ID,MVR_AMOUNT,MVR_DEATH,MVR_DATE,                          
 VIOLATION_ID,IS_ACTIVE,VIOLATION_SOURCE,VERIFIED,VIOLATION_TYPE,                                  
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                                  
 OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                                  
 --,CREATED_BY,CREATED_DATETIME                                    
 )                            
 SELECT                                                    
 @TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@TO_DRIVER_ID,POL_MVR_ID,MVR_AMOUNT,MVR_DEATH,MVR_DATE,                          
 VIOLATION_ID,IS_ACTIVE,VIOLATION_SOURCE,VERIFIED,VIOLATION_TYPE,                    
 --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                                  
 OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                                  
 --,CREATED_BY,CREATED_DATETIME                                    
                         
 FROM  POL_MVR_INFORMATION                            
 WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                      
 AND         POLICY_ID=@FROM_POLICY_ID                                      
 AND         POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                                      
 AND         DRIVER_ID= @FROM_DRIVER_ID                                                             
END        -- end of same LOB                          
ELSE   -- if lob are differ                                                             
 BEGIN                                
  IF (@TEMP_FROM_LOB_ID='4'AND @TEMP_TO_LOB_ID IN (2,3))                                
  BEGIN                                          
  SET @DRIVERCODE=CONVERT(INT,RAND()*50)                          
  SELECT @FIRSTCHAR=DRIVER_FNAME, @LASTCHAR=DRIVER_LNAME  FROM POL_WATERCRAFT_DRIVER_DETAILS                           WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                      
  AND     POLICY_ID=@FROM_POLICY_ID                                      
  AND     POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                                      
  AND     DRIVER_ID= @FROM_DRIVER_ID                                                 
  SET @DRIVERCODE=(SUBSTRING(@FIRSTCHAR,1,3) + SUBSTRING(@LASTCHAR,1,4) + @DRIVERCODE)                     
                  
  IF (@TO_CUSTOMER_ID=@FROM_CUSTOMER_ID)               
  BEGIN                                             
   INSERT INTO POL_DRIVER_DETAILS                                                            
   (                                                               
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,                         
 DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_DOB,                         
 DRIVER_SSN,DRIVER_SEX,DRIVER_MART_STAT,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_DRIV_TYPE,IS_ACTIVE,                        
   CREATED_BY,CREATED_DATETIME,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,                                      
   --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                                      
   MVR_STATUS,MVR_REMARKS                                                                              
   )                                
   SELECT                                       
@TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@TO_DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,                        
   @DRIVERCODE,DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,                        
   DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,MARITAL_STATUS,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_DRIV_TYPE,'Y',                        
   @CREATED_BY_USER_ID,GETDATE(),DATE_ORDERED,MVR_ORDERED,VIOLATIONS,                              
   --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                                    
   MVR_STATUS,MVR_REMARKS                              
                                              
   FROM    POL_WATERCRAFT_DRIVER_DETAILS                                                                               
   WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                      
   AND         POLICY_ID=@FROM_POLICY_ID                                      
   AND         POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                                      
   AND         DRIVER_ID= @FROM_DRIVER_ID                                                     
                                                 
                                                
    INSERT INTO POL_DRIVER_ASSIGNED_VEHICLE                                                            
    (                                                            
    CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,VEHICLE_ID,APP_VEHICLE_PRIN_OCC_ID                                                     
    )                                                            
    SELECT                                                            
    CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,@To_Driver_Id,VEHICLE_ID,11931--'Not Rated'                          
    FROM    POL_VEHICLES                                                            
    WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID                             
    AND         POLICY_ID=@TO_POLICY_ID                                                            
    AND         POLICY_VERSION_ID=@TO_POLICY_VERSION_ID                                                            
    --AND         DRIVER_ID= @FROM_DRIVER_ID                            
 END    -- end of same customer                  
    ELSE   -- if customer r differ                    
    BEGIN                                                              
     INSERT INTO POL_DRIVER_DETAILS                                                                            
     (                                                                              
     CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,                       
     DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_DOB,                        
     DRIVER_SSN,DRIVER_SEX,DRIVER_MART_STAT,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_DRIV_TYPE,IS_ACTIVE,                         
     CREATED_BY,CREATED_DATETIME,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,                                      
     --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                                      
     MVR_STATUS,MVR_REMARKS                                                                              
     )                                
     SELECT                                                                           
     @TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@TO_DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,                   
     @DRIVERCODE,DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,                       
     DRIVER_DOB,DRIVER_SSN,DRIVER_SEX,MARITAL_STATUS,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_DRIV_TYPE,'Y',                      
     @CREATED_BY_USER_ID,GETDATE(),DATE_ORDERED,MVR_ORDERED,VIOLATIONS,                              
     --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                                    
     MVR_STATUS,MVR_REMARKS                              
     FROM    POL_WATERCRAFT_DRIVER_DETAILS                                                                               
  WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                      
   AND    POLICY_ID=@FROM_POLICY_ID                                      
   AND    POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                                      
   AND    DRIVER_ID= @FROM_DRIVER_ID                                                                                
    --                                                           
    --                                         
    -- --If the driver copied belongs to application different from current, add default values                   
    -- at driver-ehicle        assign table                                        
    --                                         
    INSERT INTO POL_DRIVER_ASSIGNED_VEHICLE                                                            
    (                                                            
    CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,VEHICLE_ID,APP_VEHICLE_PRIN_OCC_ID                              )                                                  
    SELECT                                                            
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,@To_Driver_Id,VEHICLE_ID,11931--'Not Rated'                          
    FROM    POL_VEHICLES                                                            
    WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID                                                            
    AND         POLICY_ID=@TO_POLICY_ID                                                            
    AND         POLICY_VERSION_ID=@TO_POLICY_VERSION_ID                                                            
    --AND         DRIVER_ID= @FROM_DRIVER_ID                            
  END  -- end of else                  
-- copying MVR info                  
   DECLARE @WATER_MVR_ID int,@WATER_OLD_VIOLATION_ID int,@VIOLATION_TYPE INT                        
   DECLARE @VIOLATION_CODE NVARCHAR(10)                               
                   
   DECLARE CurWATER_MVR_INSERT CURSOR                          
   FOR SELECT APP_WATER_MVR_ID,VIOLATION_ID,VIOLATION_TYPE FROM POL_WATERCRAFT_MVR_INFORMATION                            
   WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID AND POLICY_ID=@FROM_POLICY_ID                  
 AND POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID AND DRIVER_ID=@FROM_DRIVER_ID                             
                  
 OPEN CurWATER_MVR_INSERT                          
                          
   FETCH NEXT FROM CurWATER_MVR_INSERT INTO @WATER_MVR_ID,@WATER_OLD_VIOLATION_ID,@VIOLATION_TYPE                     
    --SELECT @WATER_MVR_ID,@WATER_OLD_VIOLATION_ID,@VIOLATION_TYPE                  
   WHILE (@@FETCH_STATUS=0 )                          
    BEGIN   
   IF(@VIOLATION_TYPE IN (SELECT VIOLATION_ID FROM MNT_VIOLATIONS where VIOLATION_ID > =15000                  
 and VIOLATION_CODE!='SUSPN' ))                
   BEGIN-----VIOLATION_TYPE AVAILABLE,VIOLATION_ID NOT AVAILABLE -- Done By Sibin on 12 Feb 09 for Itrack Issue 5304 to enable copying of MVR                   
                                
   SELECT @TEMP_VIOLATION_TYPE=VIOLATION_ID FROM MNT_VIOLATIONS                             
   WHERE LOB=@TEMP_TO_LOB_ID AND STATE=@TEMP_STATE_ID                           
   AND VIOLATION_CODE=(SELECT VIOLATION_CODE FROM MNT_VIOLATIONS WHERE VIOLATION_ID=@VIOLATION_TYPE)                 
                      
           
    INSERT INTO POL_MVR_INFORMATION                                      
    (                                                            
    POL_MVR_ID,CUSTOMER_ID,POLICY_ID, POLICY_VERSION_ID,VIOLATION_ID,DRIVER_ID,MVR_AMOUNT,MVR_DEATH,MVR_DATE,             
 IS_ACTIVE,VERIFIED,VIOLATION_TYPE,                                      
    --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                                      
    OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                                     
    --,CREATED_BY,CREATED_DATETIME                                          
    )                                        
    SELECT                                                                
    APP_WATER_MVR_ID,@TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,0,@TO_DRIVER_ID,                     
  MVR_AMOUNT,MVR_DEATH,MVR_DATE,IS_ACTIVE,VERIFIED,@TEMP_VIOLATION_TYPE,                                       
    --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                                      
    OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                                       
    --,CREATED_BY,GETDATE()                                        
    FROM    POL_WATERCRAFT_MVR_INFORMATION                                   
    WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                                            
    AND     POLICY_ID=@FROM_POLICY_ID                                
    AND     POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                                                            
    AND     DRIVER_ID= @FROM_DRIVER_ID                                                                                  
    AND  APP_WATER_MVR_ID  = @WATER_MVR_ID                          
                                
                            
   END                  
  --VIOLATION_TYPE,VIOLATION_ID BOTH AVAILABLE -- Done BY Sibin on 12 Feb 09 for Itrack Issue 5304 to enable copying of MVR                
   ELSE                
   BEGIN                
--    WHILE (@@FETCH_STATUS=0 )                          
--    BEGIN                                
    --SELECT @WATER_MVR_ID,@WATER_OLD_VIOLATION_ID,@VIOLATION_TYPE            
   SELECT  @TEMP_VIOLATION_TYPE=VIOLATION_ID FROM MNT_VIOLATIONS                             
   WHERE VIOLATION_GROUP=                          
    (SELECT VIOLATION_PARENT FROM MNT_VIOLATIONS WHERE VIOLATION_ID=@WATER_OLD_VIOLATION_ID)                          
   AND LOB=@TEMP_TO_LOB_ID AND STATE=@TEMP_STATE_ID                           
                  
                     
   SELECT @TEMP_VIOLATION_ID=VIOLATION_ID FROM MNT_VIOLATIONS                             
   WHERE VIOLATION_PARENT=(SELECT VIOLATION_GROUP FROM MNT_VIOLATIONS                           
   WHERE VIOLATION_ID= @TEMP_VIOLATION_TYPE)                          
   AND LOB=@TEMP_TO_LOB_ID AND STATE=@TEMP_STATE_ID                           
   AND VIOLATION_CODE=(SELECT VIOLATION_CODE FROM MNT_VIOLATIONS WHERE VIOLATION_ID=@WATER_OLD_VIOLATION_ID)                 
                  
    INSERT INTO POL_MVR_INFORMATION                                           
    (                                                            
    POL_MVR_ID,CUSTOMER_ID,POLICY_ID, POLICY_VERSION_ID,VIOLATION_ID,DRIVER_ID,                                                
    MVR_AMOUNT,MVR_DEATH,MVR_DATE,IS_ACTIVE,VERIFIED,VIOLATION_TYPE,                                      
   --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                                      
    OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                                     
    --,CREATED_BY,CREATED_DATETIME                                          
    )                                        
    SELECT                                                                
    APP_WATER_MVR_ID,@TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@TEMP_VIOLATION_ID,@TO_DRIVER_ID,   MVR_AMOUNT,MVR_DEATH,MVR_DATE,IS_ACTIVE,VERIFIED,@TEMP_VIOLATION_TYPE,                                       
    --Added by Sibin on 16 Jan 09 for Itrack Issue 5304                                      
    OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                                       
    --,CREATED_BY,GETDATE()                                        
    FROM  POL_WATERCRAFT_MVR_INFORMATION                                   
    WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                                            
    AND     POLICY_ID=@FROM_POLICY_ID                                
    AND     POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                                                            
    AND     DRIVER_ID= @FROM_DRIVER_ID                                                                                  
   AND     APP_WATER_MVR_ID  = @WATER_MVR_ID                       
  END                              
  FETCH NEXT FROM CurWATER_MVR_INSERT INTO @WATER_MVR_ID,@WATER_OLD_VIOLATION_ID,@VIOLATION_TYPE                         
 END                          
   CLOSE CurWATER_MVR_INSERT                          
   DEALLOCATE CurWATER_MVR_INSERT                          
END                              
                                
-- END  -- end of from lob WAT                   
   -------- If Lob other than Watercraft                                
 ELSE                               
 BEGIN                         
 SET @DRIVERCODE=CONVERT(INT,RAND()*50)                                                      
 SELECT @FIRSTCHAR=DRIVER_FNAME, @LASTCHAR=DRIVER_LNAME  FROM POL_DRIVER_DETAILS                               
 WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                                      
 AND         POLICY_ID=@FROM_POLICY_ID                                                      
 AND         POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                                                      
 AND         DRIVER_ID= @FROM_DRIVER_ID                                                     
 SET @DRIVERCODE=(SUBSTRING(@FIRSTCHAR,1,3) + SUBSTRING(@LASTCHAR,1,4) + @DRIVERCODE)                          
--- if same customer                   
  IF(@TO_CUSTOMER_ID=@FROM_CUSTOMER_ID and @TO_POLICY_ID=@FROM_POLICY_ID                         
  and @TO_POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID)                                              
  BEGIN           
  INSERT INTO POL_DRIVER_DETAILS                                                            
  (                                                            
  CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,                     
  DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_HOME_PHONE,                    
  DRIVER_BUSINESS_PHONE,DRIVER_EXT,DRIVER_MOBILE,DRIVER_DOB,DRIVER_SSN,DRIVER_MART_STAT,DRIVER_SEX,                        
  DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_LIC_CLASS,DATE_EXP_START,DATE_LICENSED,DRIVER_REL,DRIVER_DRIV_TYPE,                    
  DRIVER_OCC_CODE,DRIVER_OCC_CLASS,DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,                     
  DRIVER_INCOME,DRIVER_BROADEND_NOFAULT,DRIVER_PHYS_MED_IMPAIRE,DRIVER_DRINK_VIOLATION,                     
  DRIVER_PREF_RISK,DRIVER_GOOD_STUDENT,DRIVER_STUD_DIST_OVER_HUNDRED,DRIVER_LIC_SUSPENDED,                     
  DRIVER_VOLUNTEER_POLICE_FIRE,DRIVER_US_CITIZEN,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,           
  DRIVER_FAX,RELATIONSHIP,DRIVER_TITLE,Good_Driver_Student_Discount,Premier_Driver_Discount,                     
  Safe_Driver_Renewal_Discount,Vehicle_ID,Percent_Driven, Mature_Driver,Mature_Driver_Discount,                    
  Preferred_Risk_Discount,Preferred_Risk,TransferExp_Renewal_Discount,TransferExperience_RenewalCredit,                    
  SAFE_DRIVER,NO_DEPENDENTS,APP_VEHICLE_PRIN_OCC_ID ,WAIVER_WORK_LOSS_BENEFITS,NO_CYCLE_ENDMT,FULL_TIME_STUDENT,                    
  SUPPORT_DOCUMENT,SIGNED_WAIVER_BENEFITS_FORM,HAVE_CAR,IN_MILITARY,STATIONED_IN_US_TERR,PARENTS_INSURANCE,                    
  CYCL_WITH_YOU,COLL_STUD_AWAY_HOME,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,                     
  --Added by Sibin on 27 Jan 09 for Itrack Issue 5304                                      
  MVR_STATUS,MVR_REMARKS,FORM_F95,EXT_NON_OWN_COVG_INDIVI --Added for Itrack issue 5673                                                                                                                       
  )                                        
  SELECT                                                            
  @TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@To_Driver_Id,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,        @DRIVERCODE,DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,                      
  DRIVER_HOME_PHONE,DRIVER_BUSINESS_PHONE,DRIVER_EXT,DRIVER_MOBILE,DRIVER_DOB,DRIVER_SSN,DRIVER_MART_STAT,                       
  DRIVER_SEX,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_LIC_CLASS,DATE_EXP_START,DATE_LICENSED,DRIVER_REL,                       DRIVER_DRIV_TYPE,DRIVER_OCC_CODE,DRIVER_OCC_CLASS,DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,                        
  DRIVER_INCOME,DRIVER_BROADEND_NOFAULT,DRIVER_PHYS_MED_IMPAIRE,DRIVER_DRINK_VIOLATION,DRIVER_PREF_RISK,                        
  DRIVER_GOOD_STUDENT,DRIVER_STUD_DIST_OVER_HUNDRED,DRIVER_LIC_SUSPENDED,DRIVER_VOLUNTEER_POLICE_FIRE,                        
  DRIVER_US_CITIZEN,'Y', @CREATED_BY_USER_ID,GETDATE(),DRIVER_FAX,RELATIONSHIP,DRIVER_TITLE,                          
  Good_Driver_Student_Discount,Premier_Driver_Discount,Safe_Driver_Renewal_Discount,Vehicle_ID,Percent_Driven,                  
    Mature_Driver,Mature_Driver_Discount,Preferred_Risk_Discount,Preferred_Risk, TransferExp_Renewal_Discount,                   
  TransferExperience_RenewalCredit,SAFE_DRIVER,NO_DEPENDENTS,APP_VEHICLE_PRIN_OCC_ID ,                                     
  WAIVER_WORK_LOSS_BENEFITS,NO_CYCLE_ENDMT,FULL_TIME_STUDENT,SUPPORT_DOCUMENT,SIGNED_WAIVER_BENEFITS_FORM,                                  
  HAVE_CAR,IN_MILITARY,STATIONED_IN_US_TERR,PARENTS_INSURANCE,CYCL_WITH_YOU,COLL_STUD_AWAY_HOME,DATE_ORDERED,       MVR_ORDERED,VIOLATIONS,                                      
  --Added by Sibin on 27 Jan 09 for Itrack Issue 5304                                      
  MVR_STATUS,MVR_REMARKS,FORM_F95,EXT_NON_OWN_COVG_INDIVI --Added for Itrack issue 5673                                    
  FROM    POL_DRIVER_DETAILS                                                            
  WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                                                            
  AND         POLICY_ID=@FROM_POLICY_ID                                                            
  AND         POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                                                      
  AND DRIVER_ID= @FROM_DRIVER_ID                                   
    ---                              
    INSERT INTO POL_DRIVER_ASSIGNED_VEHICLE                                                            
    (                                                            
    CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,VEHICLE_ID,APP_VEHICLE_PRIN_OCC_ID                                 )                                                            
    SELECT                                                            
    CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,@To_Driver_Id,VEHICLE_ID,11931--'Not Rated'                          
    FROM    POL_VEHICLES                                                       
    WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID                                                            
    AND         POLICY_ID=@TO_POLICY_ID                                                            
    AND         POLICY_VERSION_ID=@TO_POLICY_VERSION_ID                                   
  END                                              
 ELSE -- if customr is differ                                   
 BEGIN                                    
  INSERT INTO POL_DRIVER_DETAILS                                                            
  (               
  CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,DRIVER_CODE,                     
  DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,DRIVER_HOME_PHONE,                    
  DRIVER_BUSINESS_PHONE,DRIVER_EXT,DRIVER_MOBILE,DRIVER_DOB,DRIVER_SSN,DRIVER_MART_STAT,DRIVER_SEX,                        
  DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_LIC_CLASS,DATE_EXP_START,DATE_LICENSED,DRIVER_REL,DRIVER_DRIV_TYPE,                    
  DRIVER_OCC_CODE,DRIVER_OCC_CLASS,DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,                     
  DRIVER_INCOME,DRIVER_BROADEND_NOFAULT,DRIVER_PHYS_MED_IMPAIRE,DRIVER_DRINK_VIOLATION,                     
  DRIVER_PREF_RISK,DRIVER_GOOD_STUDENT,DRIVER_STUD_DIST_OVER_HUNDRED,DRIVER_LIC_SUSPENDED,                     
  DRIVER_VOLUNTEER_POLICE_FIRE,DRIVER_US_CITIZEN,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,                    
  DRIVER_FAX,RELATIONSHIP,DRIVER_TITLE,Good_Driver_Student_Discount,Premier_Driver_Discount,                     
  Safe_Driver_Renewal_Discount,Vehicle_ID,Percent_Driven, Mature_Driver,Mature_Driver_Discount,                    
  Preferred_Risk_Discount,Preferred_Risk,TransferExp_Renewal_Discount,TransferExperience_RenewalCredit,                    
  SAFE_DRIVER,NO_DEPENDENTS,APP_VEHICLE_PRIN_OCC_ID ,WAIVER_WORK_LOSS_BENEFITS,NO_CYCLE_ENDMT,FULL_TIME_STUDENT,                    
  SUPPORT_DOCUMENT,SIGNED_WAIVER_BENEFITS_FORM,HAVE_CAR,IN_MILITARY,STATIONED_IN_US_TERR,PARENTS_INSURANCE,                    
  CYCL_WITH_YOU,COLL_STUD_AWAY_HOME,DATE_ORDERED,MVR_ORDERED,VIOLATIONS,                                      
  --Added by Sibin on 27 Jan 09 for Itrack Issue 5304                                      
  MVR_STATUS,MVR_REMARKS,FORM_F95,EXT_NON_OWN_COVG_INDIVI --Added for Itrack issue 5673                                 
  )                                                                                         
  SELECT                                                            
  @TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@To_Driver_Id,DRIVER_FNAME,DRIVER_MNAME,DRIVER_LNAME,        @DRIVERCODE,DRIVER_SUFFIX,DRIVER_ADD1,DRIVER_ADD2,DRIVER_CITY,DRIVER_STATE,DRIVER_ZIP,DRIVER_COUNTRY,                      
  DRIVER_HOME_PHONE,DRIVER_BUSINESS_PHONE,DRIVER_EXT,DRIVER_MOBILE,DRIVER_DOB,DRIVER_SSN,DRIVER_MART_STAT,                       
 DRIVER_SEX,DRIVER_DRIV_LIC,DRIVER_LIC_STATE,DRIVER_LIC_CLASS,DATE_EXP_START,DATE_LICENSED,DRIVER_REL,                         
  DRIVER_DRIV_TYPE,DRIVER_OCC_CODE,DRIVER_OCC_CLASS,DRIVER_DRIVERLOYER_NAME,DRIVER_DRIVERLOYER_ADD,                        
  DRIVER_INCOME,DRIVER_BROADEND_NOFAULT,DRIVER_PHYS_MED_IMPAIRE,DRIVER_DRINK_VIOLATION,DRIVER_PREF_RISK,                        
  DRIVER_GOOD_STUDENT,DRIVER_STUD_DIST_OVER_HUNDRED,DRIVER_LIC_SUSPENDED,DRIVER_VOLUNTEER_POLICE_FIRE,                        
  DRIVER_US_CITIZEN,'Y', @CREATED_BY_USER_ID,GETDATE(),DRIVER_FAX,RELATIONSHIP,DRIVER_TITLE,                          
  Good_Driver_Student_Discount,Premier_Driver_Discount,Safe_Driver_Renewal_Discount,Vehicle_ID,Percent_Driven,                  
    Mature_Driver,Mature_Driver_Discount,Preferred_Risk_Discount,Preferred_Risk, TransferExp_Renewal_Discount,                   
  TransferExperience_RenewalCredit,SAFE_DRIVER,NO_DEPENDENTS,APP_VEHICLE_PRIN_OCC_ID ,                                      
  WAIVER_WORK_LOSS_BENEFITS,NO_CYCLE_ENDMT,FULL_TIME_STUDENT,SUPPORT_DOCUMENT,SIGNED_WAIVER_BENEFITS_FORM,                                  
  HAVE_CAR,IN_MILITARY,STATIONED_IN_US_TERR,PARENTS_INSURANCE,CYCL_WITH_YOU,COLL_STUD_AWAY_HOME,DATE_ORDERED,       MVR_ORDERED,VIOLATIONS,                                      
  --Added by Sibin on 27 Jan 09 for Itrack Issue 5304                                      
  MVR_STATUS,MVR_REMARKS,FORM_F95,EXT_NON_OWN_COVG_INDIVI --Added for Itrack issue 5673                                    
  FROM    POL_DRIVER_DETAILS                                                            
  WHERE   CUSTOMER_ID=@FROM_CUSTOMER_ID                               
  AND         POLICY_ID=@FROM_POLICY_ID                                                   
  AND         POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                                                            
  AND         DRIVER_ID= @FROM_DRIVER_ID                                   
                              
  ---                              
  INSERT INTO POL_DRIVER_ASSIGNED_VEHICLE                                                            
  (                                                            
  CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,VEHICLE_ID,APP_VEHICLE_PRIN_OCC_ID                                                     
  )                                                            
  SELECT                                                            
  CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,@To_Driver_Id,VEHICLE_ID,11931--'Not Rated'                          
  FROM    POL_VEHICLES                                                          
  WHERE   CUSTOMER_ID=@TO_CUSTOMER_ID                                                            
  AND     POLICY_ID=@TO_POLICY_ID                                                            
  AND     POLICY_VERSION_ID=@TO_POLICY_VERSION_ID                        
  END                         
 -- copying MVR Info                                             
  DECLARE @MVR_ID int,@OLD_VIOLATION_ID int                               
  DECLARE CurMVR_INSERT CURSOR                                
  FOR SELECT POL_MVR_ID,VIOLATION_ID,VIOLATION_TYPE FROM POL_MVR_INFORMATION                                 
  WHERE CUSTOMER_ID=@FROM_CUSTOMER_ID AND POLICY_ID=@FROM_POLICY_ID                   
  AND POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                                
  AND DRIVER_ID=@FROM_DRIVER_ID                                   
  OPEN CurMVR_INSERT                                
                  
  FETCH NEXT FROM CurMVR_INSERT INTO @MVR_ID,@OLD_VIOLATION_ID,@VIOLATION_TYPE                                
                               
  WHILE (@@FETCH_STATUS=0 )                                
  BEGIN              
   IF(@VIOLATION_TYPE IN (SELECT VIOLATION_ID FROM MNT_VIOLATIONS where VIOLATION_ID > =15000                  
                         and VIOLATION_CODE!='SUSPN' ))                
  BEGIN-----VIOLATION_TYPE AVAILABLE,VIOLATION_ID NOT AVAILABLE -- Done By Sibin on 12 Feb 09 for Itrack Issue 5304 to enable copying of MVR          
         
   SELECT @TEMP_VIOLATION_TYPE=VIOLATION_ID FROM MNT_VIOLATIONS                             
   WHERE LOB=@TEMP_TO_LOB_ID AND STATE=@TEMP_STATE_ID                           
   AND VIOLATION_CODE=(SELECT VIOLATION_CODE FROM MNT_VIOLATIONS WHERE VIOLATION_ID=@VIOLATION_TYPE)        
      
   INSERT INTO POL_MVR_INFORMATION                               
   (                           
   CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,POL_MVR_ID,MVR_AMOUNT,MVR_DEATH,MVR_DATE,                          
   VIOLATION_ID,IS_ACTIVE,VIOLATION_SOURCE,VERIFIED,VIOLATION_TYPE,                                  
   --Added by Sibin on 27 Jan 09 for Itrack Issue 5304                                  
   OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                                  
   --,CREATED_BY,CREATED_DATETIME                                    
   )                            
   SELECT     
   @TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@TO_DRIVER_ID,POL_MVR_ID,                  
   MVR_AMOUNT,MVR_DEATH,MVR_DATE,                          
   0,IS_ACTIVE,VIOLATION_SOURCE,VERIFIED,@TEMP_VIOLATION_TYPE,                                  
   --Added by Sibin on 27 Jan 09 for Itrack Issue 5304                                  
   OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                                  
   --,CREATED_BY,CREATED_DATETIME                                    
   FROM  POL_MVR_INFORMATION                            
   WHERE  CUSTOMER_ID=@FROM_CUSTOMER_ID                                      
   AND    POLICY_ID=@FROM_POLICY_ID                                      
   AND    POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                                      
   AND    DRIVER_ID= @FROM_DRIVER_ID                    
    AND    POL_MVR_ID= @MVR_ID                   
                                                             
   --FETCH NEXT FROM CurMVR_INSERT INTO @MVR_ID,@OLD_VIOLATION_ID,@VIOLATION_TYPE        
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
   INSERT INTO POL_MVR_INFORMATION                               
   (                           
   CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,DRIVER_ID,POL_MVR_ID,MVR_AMOUNT,MVR_DEATH,MVR_DATE,                          
  VIOLATION_ID,IS_ACTIVE,VIOLATION_SOURCE,VERIFIED,VIOLATION_TYPE,                                  
   --Added by Sibin on 27 Jan 09 for Itrack Issue 5304                                  
   OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                                  
   --,CREATED_BY,CREATED_DATETIME                                    
   )                            
   SELECT                                                    
   @TO_CUSTOMER_ID,@TO_POLICY_ID,@TO_POLICY_VERSION_ID,@TO_DRIVER_ID,POL_MVR_ID,                  
   MVR_AMOUNT,MVR_DEATH,MVR_DATE,               
   @TEMP_VIOLATION_ID,IS_ACTIVE,VIOLATION_SOURCE,VERIFIED,@TEMP_VIOLATION_TYPE,                                  
   --Added by Sibin on 27 Jan 09 for Itrack Issue 5304                                  
   OCCURENCE_DATE,POINTS_ASSIGNED,ADJUST_VIOLATION_POINTS,DETAILS                                  
   --,CREATED_BY,CREATED_DATETIME                                    
   FROM  POL_MVR_INFORMATION                            
   WHERE  CUSTOMER_ID=@FROM_CUSTOMER_ID                                      
   AND    POLICY_ID=@FROM_POLICY_ID                                      
   AND    POLICY_VERSION_ID=@FROM_POLICY_VERSION_ID                                      
   AND    DRIVER_ID= @FROM_DRIVER_ID                    
    AND    POL_MVR_ID= @MVR_ID                   
                                                             
    --FETCH NEXT FROM CurMVR_INSERT INTO @MVR_ID,@OLD_VIOLATION_ID,@VIOLATION_TYPE            
                                  
    END  -- end of while      
   FETCH NEXT FROM CurMVR_INSERT INTO @MVR_ID,@OLD_VIOLATION_ID,@VIOLATION_TYPE                        
  END    -- end of while       
--  END                             
     CLOSE CurMVR_INSERT                     
  DEALLOCATE CurMVR_INSERT     
  END                   
 --Added till here                                         
 END                                  
END  
  
--go  
--exec Proc_InsertPolicyExistingDriver 1682,1,1,1682,2,1,1,296,'mot'  
--rollback tran


GO

