IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAppDriverDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAppDriverDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
/*----------------------------------------------------------                                                                      
Proc Name       : Vijay                                                                      
Created by      : Proc_InsertAppDriverDetails                                                                      
Date            : 4/28/2005                                                                      
Purpose     : Build                                                                      
Revison History :                                                                      
Used In  : Wolverine                                                                      
                                                                      
Modified By : Anurag Verma                                                                      
Modified On : 15/09/2005                                                                      
Purpose  : merger of driver details, driver discount and assign vehicle screen                                                                       
                                                                    
Modified By : Vijay Arora                                                                    
Modified On : 17-10-2005                                                                    
Purpose  : To add the principal / occasional vehicle option in assigned vehicle.                                                                    
                                                                
Modified By : Sumit Chhabra                                                                
Modified On : 11-11-2005                                                                    
Purpose  : Duplicacy of Driver Code has been done away with                                                                
Modified By : Sumit Chhabra                                                                
Modified On : 1-12-2005                                                                    
Purpose   : Added call to procedure to set rule on vehicle class                                                    
Modified By : Shafi                                            
Modified On : 12-01-2006                                            
Purpose   : Added Fild for No Of Dependents                                            
                                      
Modified By : Sumit Chhabra                                      
Modified On : 10-02-2006                                            
Purpose   : Added Fild for @WAIVER_WORK_LOSS_BENEFITS                                      
------------------------------------------------------------                                                                      
                                            
Date        Review By          Comments                                                                      
13/05/2005   Nidhi   Added two more parameters for FAX and RELATIONSHIP (both have default  NULL) -This was done to accomodate MOTORCYCLE DRIVER too within this SP                                                                      
                                                                
------   ------------       -------------------------*/                                                                      
--DROP PROC dbo.Proc_InsertAppDriverDetails                                                                 
CREATE PROC [dbo].[Proc_InsertAppDriverDetails]                                                                      
(                                                                      
 @CUSTOMER_ID      int,                                                                      
 @APP_ID   int,                                                                      
 @APP_VERSION_ID  int,           
 @DRIVER_ID       smallint  OUTPUT,                                    
 @DRIVER_FNAME      nvarchar(150),                                                        
 @DRIVER_MNAME  nvarchar(50),                                                                   
 @DRIVER_LNAME      nvarchar(150),                                                                      
 @DRIVER_CODE      nvarchar(20),                                                                      
 @DRIVER_SUFFIX      nvarchar(20),                                                                      
 @DRIVER_ADD1      nvarchar(140),                                                                      
 @DRIVER_ADD2      nvarchar(140),                                                                      
 @DRIVER_CITY      nvarchar(80),                                                                      
 @DRIVER_STATE      nvarchar(10),                                                            
 @DRIVER_ZIP      nvarchar(20),                                         
 @DRIVER_COUNTRY      nchar(10),                                                                      
 @DRIVER_HOME_PHONE      nvarchar(40),                                                                      
 @DRIVER_BUSINESS_PHONE  nvarchar(40),                                                                      
 @DRIVER_EXT      nvarchar(10),                                                  
 @DRIVER_MOBILE      nvarchar(40),                                                                      
 @DRIVER_DOB      datetime,                                                                      
 @DRIVER_SSN      nvarchar(88),                                          
 @DRIVER_MART_STAT      nchar(2),                                                                      
 @DRIVER_SEX      nchar(2),                                                              
 @DRIVER_DRIV_LIC      nvarchar(60),                                                                      
 @DRIVER_LIC_STATE    nvarchar(10),                                                                      
 @DRIVER_LIC_CLASS      nvarchar(10) = NULL,                                                                      
 @DRIVER_REL      nvarchar(10),                                                                      
 @DRIVER_DRIV_TYPE      nvarchar(10) = NULL,                                                                      
 @DRIVER_OCC_CODE      nvarchar(12) = NULL,                                                                      
 @DRIVER_OCC_CLASS      nvarchar(10),                                                                   
 @DRIVER_DRIVERLOYER_NAME nvarchar(150),                                                                      
 @DRIVER_DRIVERLOYER_ADD nvarchar(510),                                                                      
 @DRIVER_INCOME      decimal(18,2),                                                                      
 @DRIVER_BROADEND_NOFAULT    smallint = NULL,                                                                      
 @DRIVER_PHYS_MED_IMPAIRE    nchar(2) =NULL,                                                                      
 @DRIVER_DRINK_VIOLATION     nchar(1) = NULL,                      
 @DRIVER_PREF_RISK      nchar(2),                                                    
 @DRIVER_GOOD_STUDENT    nchar(2),                                                                      
 @DRIVER_STUD_DIST_OVER_HUNDRED    nchar(2),                                                                      
@DRIVER_LIC_SUSPENDED  nchar(1)=NULL,                                                                      
 @DRIVER_VOLUNTEER_POLICE_FIRE     nchar(2),                                                                      
 @DRIVER_US_CITIZEN      nchar(2),                                                                      
 @CREATED_BY      int,                                                                      
 @CREATED_DATETIME      datetime,                                                                      
 @MODIFIED_BY      int,             
 @LAST_UPDATED_DATETIME  datetime,                                                                      
 @INSERTUPDATE  varchar(1),                                             
 @DRIVER_FAX varchar(40)= null,                                                                      
 @RELATIONSHIP int = null,                                       
 @SAFE_DRIVER BIT,                                                                      
 -- Added By anurag verma on 14/09/2005 related to merger of driver details, driver discount and assign vehicle screen                                                                  
@Good_Driver_Student_Discount decimal(9,2)=null,                                                                      
 @Premier_Driver_Discount decimal(9,2)=null,                                                                      
 @Safe_Driver_Renewal_Discount decimal(9,2)=null,                                                                      
 @VEHICLE_ID     smallint=null,                                                                       
 @VIOLATIONS         int,                            
 @MVR_ORDERED         int,                            
 @DATE_ORDERED     datetime =null,                            
 @PERCENT_DRIVEN decimal(9,2)=null,                                              
 @Mature_Driver nchar(1)=null,                                                                      
 @Mature_Driver_Discount decimal(9,2)=null,                                                                      
 @Preferred_Risk_Discount decimal(9,2)=null,                      
 @Preferred_Risk nchar(1)=null,                                                                      
 @TransferExp_Renewal_Discount decimal(9,2)=null,                                       
 @TransferExperience_RenewalCredit nchar(1)=null,                                                                    
 @APP_VEHICLE_PRIN_OCC_ID  INT,                             
 @CALLED_FROM VARCHAR(10)=null,                                                
 @DATE_LICENSED datetime,                                            
 --Added By shafi 12-01-2006                                            
 @NO_DEPENDENTS  smallint=null ,                                      
 --Added by Sumit on 10-02-2006                                      
 @WAIVER_WORK_LOSS_BENEFITS nchar(1)=null,                                    
--Added by Sumit on 15-02-2006                                    
@NO_CYCLE_ENDMT nchar(1)=null,                              
@FORM_F95 int = null,                             
@EXT_NON_OWN_COVG_INDIVI int = null,                            
@HAVE_CAR INT = NULL,                            
@STATIONED_IN_US_TERR INT = NULL,                            
@IN_MILITARY INT = NULL,                          
@FULL_TIME_STUDENT int = null,                          
@SUPPORT_DOCUMENT int = null,                          
@SIGNED_WAIVER_BENEFITS_FORM int = null,                        
@PARENTS_INSURANCE int = null,                      
@CYCL_WITH_YOU SMALLINT = NULL,                      
@COLL_STUD_AWAY_HOME SMALLINT = NULL,              
@MVR_CLASS varchar(50),              
@MVR_LIC_CLASS varchar(50),              
@MVR_LIC_RESTR varchar(50),              
@MVR_DRIV_LIC_APPL varchar(50)=null,              
@MVR_REMARKS varchar(250)= null,            
@MVR_STATUS varchar(10)=null,        
@LOSSREPORT_ORDER int=null,        
@LOSSREPORT_DATETIME DateTime=null                                                                  
)                                                                      
AS                                                              
                                                 
/*declare @NEW_DRIVER_DOB datetime                                                              
declare @CurrentDate datetime     
declare @ClassLookupID int                                                              
declare @ClassValue varchar(5)                                                              
declare @Age int                                         
*/                                                              
BEGIN                                                                      
                                                                      
if @INSERTUPDATE = 'I'                                                                      
BEGIN                                   
                                                                       
 /*Checking the duplicay of code*/                   
/*Nov 11,2005:Sumit Chhabra:Duplicacy of driver code has been done away with                                                                
 IF Exists(SELECT DRIVER_CODE FROM APP_DRIVER_DETAILS WHERE DRIVER_CODE = @DRIVER_CODE)                                                                      
 BEGIN               
  SELECT @DRIVER_ID = -1                                                                      
  return 0                                                                      
 END                                                                      
 */                                                                     
 /*Generating the new driver id*/                                                                      
 SELECT @DRIVER_ID = IsNull(Max(DRIVER_ID),0) + 1                                                                       
 FROM APP_DRIVER_DETAILS                                                                      
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND                                                                      
  APP_ID = @APP_ID AND                                                                      
  APP_VERSION_ID = @APP_VERSION_ID                                                           
                                                                      
 INSERT INTO APP_DRIVER_DETAILS                                                                      
 (                                                                      
  CUSTOMER_ID,                                                                      
 APP_ID,                                                                      
  APP_VERSION_ID,                                                                      
  DRIVER_ID,                                                                      
  DRIVER_FNAME,                                                
  DRIVER_MNAME,                                                                      
  DRIVER_LNAME,                                                                      
  DRIVER_CODE,                                                                      
  DRIVER_SUFFIX,                                                                      
  DRIVER_ADD1,                                                                      
  DRIVER_ADD2,                                                                      
 DRIVER_CITY,                                                                      
  DRIVER_STATE,                                                                      
  DRIVER_ZIP,                                                                      
  DRIVER_COUNTRY,                                                                   
  DRIVER_HOME_PHONE,                                                                      
  DRIVER_BUSINESS_PHONE,                                                                      
  DRIVER_EXT,                                                                      
  DRIVER_MOBILE,                                                        
  DRIVER_DOB,                                              
  DRIVER_SSN,                                                         
  DRIVER_MART_STAT,     
  DRIVER_SEX,                                                                      
  DRIVER_DRIV_LIC,                                                                      
  DRIVER_LIC_STATE,                                                                      
  DRIVER_LIC_CLASS,                            
                                                            
  DRIVER_REL,                                                                      
  DRIVER_DRIV_TYPE,                                                                  
  [DRIVER_OCC_CODE],                                                                      
  DRIVER_OCC_CLASS,                                                                     
  DRIVER_DRIVERLOYER_NAME,                                                                      
  DRIVER_DRIVERLOYER_ADD,                                 
  DRIVER_INCOME,                                                                      
  DRIVER_BROADEND_NOFAULT,             
  DRIVER_PHYS_MED_IMPAIRE,                                      
  DRIVER_DRINK_VIOLATION,                                                                      
  DRIVER_PREF_RISK,                                                                      
  DRIVER_GOOD_STUDENT,                
  DRIVER_STUD_DIST_OVER_HUNDRED,                                                            
  DRIVER_LIC_SUSPENDED,                                         
  DRIVER_VOLUNTEER_POLICE_FIRE,                                                                      
  DRIVER_US_CITIZEN,                                                                      
  IS_ACTIVE,                                                                      
  CREATED_BY,                                                                      
  CREATED_DATETIME,                                        
  DRIVER_FAX,                                                            RELATIONSHIP,                                                                      
  SAFE_DRIVER,                                                                      
  Good_Driver_Student_Discount ,                                                                      
  Premier_Driver_Discount,                                                                      
  Safe_Driver_Renewal_Discount,                                                                      
  VEHICLE_ID,                                                                       
  VIOLATIONS,                    
  MVR_ORDERED,                    
  DATE_ORDERED,                            
  PERCENT_DRIVEN,                                                                       
  Mature_Driver,                                                                      
  Mature_Driver_Discount,                                                                      
  Preferred_Risk_Discount,                                                                      
  Preferred_Risk,                                                                      
  TransferExp_Renewal_Discount,                                                                      
  TransferExperience_RenewalCredit,                                                                    
  APP_VEHICLE_PRIN_OCC_ID,                        
  DATE_LICENSED,                                            
 --Added By shafi 12-01-2005                                            
  NO_DEPENDENTS,                                      
  WAIVER_WORK_LOSS_BENEFITS,                                    
  NO_CYCLE_ENDMT,                              
 FORM_F95,                              
 EXT_NON_OWN_COVG_INDIVI,                            
 HAVE_CAR,                            
 STATIONED_IN_US_TERR,                            
 IN_MILITARY,                          
FULL_TIME_STUDENT,                          
SUPPORT_DOCUMENT,                          
SIGNED_WAIVER_BENEFITS_FORM,                        
PARENTS_INSURANCE,                      
CYCL_WITH_YOU,                      
COLL_STUD_AWAY_HOME,              
MVR_CLASS,              
MVR_LIC_CLASS,              
MVR_LIC_RESTR,              
MVR_DRIV_LIC_APPL,            
MVR_REMARKS,            
MVR_STATUS,        
LOSSREPORT_ORDER,        
LOSSREPORT_DATETIME                                                    
 )                                             
 VALUES                      
 (                                                                      
  @CUSTOMER_ID,                                             
  @APP_ID,                                                                      
  @APP_VERSION_ID,                                                                      
  @DRIVER_ID,                                                                      
  @DRIVER_FNAME,                                                        
  @DRIVER_MNAME,                                                                
  @DRIVER_LNAME,                                                                      
  @DRIVER_CODE,                                                                      
  @DRIVER_SUFFIX,                                                                      
  @DRIVER_ADD1,                                     
  @DRIVER_ADD2,                    
  @DRIVER_CITY,                                                                      
  @DRIVER_STATE,                                                              
  @DRIVER_ZIP,                                                                      
  @DRIVER_COUNTRY,                                                                      
  @DRIVER_HOME_PHONE,                                                                      
  @DRIVER_BUSINESS_PHONE,                                                                      
  @DRIVER_EXT,                                                                      
  @DRIVER_MOBILE,                                                                      
  @DRIVER_DOB,                                              
  @DRIVER_SSN,                                                                      
  @DRIVER_MART_STAT,                                                                      
  @DRIVER_SEX,                                                                      
  @DRIVER_DRIV_LIC,                                                                      
  @DRIVER_LIC_STATE,                                                                      
  @DRIVER_LIC_CLASS,                                                                      
                                                                      
  @DRIVER_REL,                     
  @DRIVER_DRIV_TYPE,                                                                      
  @DRIVER_OCC_CODE,                                                        
  @DRIVER_OCC_CLASS,                                                                      
  @DRIVER_DRIVERLOYER_NAME,                                                                      
  @DRIVER_DRIVERLOYER_ADD,                                                  
  Cast(@DRIVER_INCOME As decimal(10,2)),                                                                      
  @DRIVER_BROADEND_NOFAULT,                                                                      
  @DRIVER_PHYS_MED_IMPAIRE,                                                                      
  @DRIVER_DRINK_VIOLATION,                                          
  @DRIVER_PREF_RISK,                                                                      
  @DRIVER_GOOD_STUDENT,                    
  @DRIVER_STUD_DIST_OVER_HUNDRED,                                                                      
  @DRIVER_LIC_SUSPENDED,                                                                      
  @DRIVER_VOLUNTEER_POLICE_FIRE,                                                                      
  @DRIVER_US_CITIZEN,                                                                      
  'Y',                                                              
  @CREATED_BY,                                                             
  @CREATED_DATETIME,                                                                      
  @DRIVER_FAX,                                                                      
  @RELATIONSHIP,                                                            
  @SAFE_DRIVER,          
  @Good_Driver_Student_Discount ,                                                                      
  @Premier_Driver_Discount,                                                                      
  @Safe_Driver_Renewal_Discount,                                                                      
  @VEHICLE_ID,                                                                       
  @VIOLATIONS,                    
  @MVR_ORDERED,                    
  @DATE_ORDERED,                            
  @PERCENT_DRIVEN,                                                                       
  @Mature_Driver,                                                                      
  @Mature_Driver_Discount,                                                                      
  @Preferred_Risk_Discount,                                                                      
  @Preferred_Risk,                                                                      
  @TransferExp_Renewal_Discount,                                                                      
  @TransferExperience_RenewalCredit,                                                                    
  @APP_VEHICLE_PRIN_OCC_ID,                                       
  @DATE_LICENSED,                                            
  @NO_DEPENDENTS,                                      
  @WAIVER_WORK_LOSS_BENEFITS,                                    
  @NO_CYCLE_ENDMT,                              
  @FORM_F95,                              
  @EXT_NON_OWN_COVG_INDIVI,                            
  @HAVE_CAR,                            
  @STATIONED_IN_US_TERR,                            
  @IN_MILITARY,                
  @FULL_TIME_STUDENT,                          
  @SUPPORT_DOCUMENT,                          
  @SIGNED_WAIVER_BENEFITS_FORM,                        
  @PARENTS_INSURANCE,                      
  @CYCL_WITH_YOU,                      
  @COLL_STUD_AWAY_HOME,              
  @MVR_CLASS,              
  @MVR_LIC_CLASS,              
  @MVR_LIC_RESTR,              
  @MVR_DRIV_LIC_APPL,            
  @MVR_REMARKS,            
  @MVR_STATUS,                              
  @LOSSREPORT_ORDER,        
  @LOSSREPORT_DATETIME                                                    
                           
 )                                                                      
                                                                      
                                                    
                                                                      
END                                                                      
ELSE                                                                      
BEGIN                                                                      
 /*Checking the duplicay of code*/                                                         
/*Nov 11,2005:Sumit Chhabra:Duplicacy of driver code has been done away with                                                                
 IF Exists(SELECT DRIVER_CODE FROM APP_DRIVER_DETAILS WHERE DRIVER_CODE = @DRIVER_CODE AND DRIVER_ID <> @DRIVER_ID)                                                                    
 BEGIN                                                                      
  return 0                                                                      
 END                                                                      
*/                                                            
 /*Update the values */                                              
 --Update class of existing vehicle assigned to current driver before updating data                                        
 IF (UPPER(@CALLED_FROM)='MOT')                                                    
   EXEC Proc_SetMotorVehicleClassRuleOnChange @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@DRIVER_ID                                        
--delete MVR Informaton  if driver name was changed                                      
declare @OLD_DRIVER_FNAME VARCHAR(30)                  
declare @OLD_DRIVER_MNAME VARCHAR(30)                  
declare @OLD_DRIVER_LNAME VARCHAR(30)         
SELECT  @OLD_DRIVER_FNAME=DRIVER_FNAME,@OLD_DRIVER_MNAME=isnull(DRIVER_MNAME,''),@OLD_DRIVER_LNAME=DRIVER_LNAME                   
   FROM APP_DRIVER_DETAILS WHERE  CUSTOMER_ID  = @CUSTOMER_ID AND                                                                      
    APP_ID   = @APP_ID AND  APP_VERSION_ID  = @APP_VERSION_ID AND  DRIVER_ID   = @DRIVER_ID                                
if (@OLD_DRIVER_FNAME<>@DRIVER_FNAME or @OLD_DRIVER_MNAME<> isnull(@DRIVER_MNAME,'') or @OLD_DRIVER_LNAME<>@DRIVER_LNAME )                  
   delete from app_mvr_information WHERE  CUSTOMER_ID  = @CUSTOMER_ID AND                                                                     
    APP_ID   = @APP_ID AND  APP_VERSION_ID  = @APP_VERSION_ID AND  DRIVER_ID   = @DRIVER_ID                   
--end here                  
                  
                  
 UPDATE APP_DRIVER_DETAILS                                                                      
 SET                                                                      
  DRIVER_FNAME   = @DRIVER_FNAME,                                                                      
  DRIVER_MNAME   = @DRIVER_MNAME,                                                                      
  DRIVER_LNAME   = @DRIVER_LNAME,         
  DRIVER_CODE    = @DRIVER_CODE,                                                                      
  DRIVER_SUFFIX   = @DRIVER_SUFFIX,                            
  DRIVER_ADD1   = @DRIVER_ADD1,                                                                    
  DRIVER_ADD2   = @DRIVER_ADD2,                                                          
  DRIVER_CITY   = @DRIVER_CITY,                                                                      
  DRIVER_STATE   = @DRIVER_STATE,                                                                      
  DRIVER_ZIP   = @DRIVER_ZIP,                                                                       
  DRIVER_COUNTRY   = @DRIVER_COUNTRY,            
  DRIVER_HOME_PHONE  = @DRIVER_HOME_PHONE,                                                                       
  DRIVER_BUSINESS_PHONE  = @DRIVER_BUSINESS_PHONE,                                                          
  DRIVER_EXT   = @DRIVER_EXT,                                                                       
  DRIVER_MOBILE   = @DRIVER_MOBILE,                                                                       
  DRIVER_DOB   = @DRIVER_DOB,                              
  DRIVER_SSN   = @DRIVER_SSN,                                                                       
  DRIVER_MART_STAT  = @DRIVER_MART_STAT,                                                            
  DRIVER_SEX   = @DRIVER_SEX,                                                       
  DRIVER_DRIV_LIC  = @DRIVER_DRIV_LIC,                                                                       
  DRIVER_LIC_STATE  = @DRIVER_LIC_STATE,                                        
  DRIVER_LIC_CLASS  = @DRIVER_LIC_CLASS,                                                                       
                                                                      
  DRIVER_REL   = @DRIVER_REL,                                                                 
  DRIVER_DRIV_TYPE  = @DRIVER_DRIV_TYPE,                                                                       
  [DRIVER_OCC_CODE]  = @DRIVER_OCC_CODE,                      
  DRIVER_OCC_CLASS  = @DRIVER_OCC_CLASS,          
  DRIVER_DRIVERLOYER_NAME = @DRIVER_DRIVERLOYER_NAME,                                              
  DRIVER_DRIVERLOYER_ADD  = @DRIVER_DRIVERLOYER_ADD,                                                                       
  DRIVER_INCOME    = Cast(@DRIVER_INCOME As decimal(10,2)),                                                                       
  DRIVER_BROADEND_NOFAULT = @DRIVER_BROADEND_NOFAULT,                                                                       
  DRIVER_PHYS_MED_IMPAIRE = @DRIVER_PHYS_MED_IMPAIRE,         
  DRIVER_DRINK_VIOLATION  = @DRIVER_DRINK_VIOLATION,                                                                
  DRIVER_PREF_RISK  = @DRIVER_PREF_RISK,                                                                       
  DRIVER_GOOD_STUDENT  =  @DRIVER_GOOD_STUDENT,                                                                       
  DRIVER_STUD_DIST_OVER_HUNDRED = @DRIVER_STUD_DIST_OVER_HUNDRED,                              
  DRIVER_LIC_SUSPENDED  = @DRIVER_LIC_SUSPENDED,                                                                       
  DRIVER_VOLUNTEER_POLICE_FIRE = @DRIVER_VOLUNTEER_POLICE_FIRE,                                                              
  DRIVER_US_CITIZEN  = @DRIVER_US_CITIZEN,                                                                       
  MODIFIED_BY   = @MODIFIED_BY,                                                                       
  LAST_UPDATED_DATETIME  = @LAST_UPDATED_DATETIME,                                                                      
  DRIVER_FAX=@DRIVER_FAX,                                                                      
  RELATIONSHIP=@RELATIONSHIP,                                                                      
  SAFE_DRIVER=@SAFE_DRIVER,                           
  Good_Driver_Student_Discount=@Good_Driver_Student_Discount ,                      
  Premier_Driver_Discount=@Premier_Driver_Discount,                                                                      
  Safe_Driver_Renewal_Discount=@Safe_Driver_Renewal_Discount,                                      
  VEHICLE_ID=@VEHICLE_ID,                                                                       
  VIOLATIONS=@VIOLATIONS,                    
  MVR_ORDERED=@MVR_ORDERED,                    
  DATE_ORDERED=@DATE_ORDERED,                            
 PERCENT_DRIVEN=@PERCENT_DRIVEN,                                                                       
  Mature_Driver=@Mature_Driver,                                                                      
  Mature_Driver_Discount=@Mature_Driver_Discount,                                        
  Preferred_Risk_Discount=@Preferred_Risk_Discount,                                                                      
  Preferred_Risk=@Preferred_Risk,                                                                      
  TransferExp_Renewal_Discount=@TransferExp_Renewal_Discount,                                                    
  TransferExperience_RenewalCredit=@TransferExperience_RenewalCredit,                                                                 
  APP_VEHICLE_PRIN_OCC_ID = @APP_VEHICLE_PRIN_OCC_ID,                                                
  DATE_LICENSED      = @DATE_LICENSED,                                            
  NO_DEPENDENTS   = @NO_DEPENDENTS,                         
  WAIVER_WORK_LOSS_BENEFITS = @WAIVER_WORK_LOSS_BENEFITS,                                    
  NO_CYCLE_ENDMT = @NO_CYCLE_ENDMT,                              
  FORM_F95 = @FORM_F95,                              
  EXT_NON_OWN_COVG_INDIVI = @EXT_NON_OWN_COVG_INDIVI,                            
  HAVE_CAR = @HAVE_CAR,                            
  STATIONED_IN_US_TERR = @STATIONED_IN_US_TERR,                            
  IN_MILITARY = @IN_MILITARY,                          
 FULL_TIME_STUDENT = @FULL_TIME_STUDENT,                          
 SUPPORT_DOCUMENT = @SUPPORT_DOCUMENT,                          
 SIGNED_WAIVER_BENEFITS_FORM = @SIGNED_WAIVER_BENEFITS_FORM,                        
 PARENTS_INSURANCE = @PARENTS_INSURANCE,                      
 CYCL_WITH_YOU = @CYCL_WITH_YOU,                      
 COLL_STUD_AWAY_HOME = @COLL_STUD_AWAY_HOME,              
 MVR_CLASS = @MVR_CLASS,              
 MVR_LIC_CLASS = @MVR_LIC_CLASS,              
 MVR_LIC_RESTR = @MVR_LIC_RESTR,              
 MVR_DRIV_LIC_APPL = @MVR_DRIV_LIC_APPL,            
 MVR_REMARKS = @MVR_REMARKS,            
 MVR_STATUS = @MVR_STATUS,                                                   
 LOSSREPORT_ORDER= @LOSSREPORT_ORDER,        
 LOSSREPORT_DATETIME= @LOSSREPORT_DATETIME                                            
                                
 WHERE                                                                      
  CUSTOMER_ID  = @CUSTOMER_ID AND                                                                      
  APP_ID   = @APP_ID AND                                                                      
  APP_VERSION_ID  = @APP_VERSION_ID AND                                                                      
  DRIVER_ID   = @DRIVER_ID                                                                      
  

 /*Updating values end*/                                                                   
END                                                                      
                                                              
IF (UPPER(@CALLED_FROM)='MOT')                 
 EXEC PROC_SETMOTORVEHICLECLASSRULE @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, @DRIVER_ID, @VEHICLE_ID, @DRIVER_DOB                                                  
--BEGIN                                                       
 /* SELECT @NEW_DRIVER_DOB=MIN(DRIVER_DOB) FROM APP_DRIVER_DETAILS WHERE                                                              
   CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND                                                        
   VEHICLE_ID=@VEHICLE_ID  and IS_ACTIVE = 'Y'                     
                                                            
                                                                
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
    --(SELECT TOP 1 LOOKUP_ID FROM MNT_LOOKUP_TABLES WHERE LOOKUP_NAME='MCCLAS') AND LOOKUP_VALUE_CODE=@CLASSVALUE                                                              
 select @CLASSLOOKUPID=v.lookup_unique_id from mnt_lookup_values v join mnt_lookup_tables t                                            
  on  v.lookup_id=t.lookup_id                                                           
  where t.lookup_name='MCCLAS' and v.lookup_value_code=@CLASSVALUE and v.is_active='Y'                                                        
             
    UPDATE APP_VEHICLES SET APP_VEHICLE_CLASS=@CLASSLOOKUPID WHERE                                          
     CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND                                                              
     VEHICLE_ID=@VEHICLE_ID                                                        
*/                                                  
 --EXEC PROC_SETMOTORVEHICLECLASSRULE @CUSTOMER_ID, @APP_ID, @APP_VERSION_ID, @DRIVER_ID, @VEHICLE_ID, @DRIVER_DOB                                                  
--END                                                              
--ELSE IF (UPPER(@CALLED_FROM)='PPA')                                                    
--  EXEC PROC_SETVEHICLECLASSRULE @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@DRIVER_ID,NULL          
/* Moved to PROC_UPDATEVEHICLECLASS 22-Sep-08
DECLARE @PARENTS_INSURANCES NVarchar(20)        
SET @PARENTS_INSURANCES='FALSE'         
--Modified by Mohit Agarwal ITrack 4737 16-Sep-08    
--IF (@PARENTS_INSURANCE = 11935)           
-- BEGIN        
--  UPDATE APP_AUTO_GEN_INFO        
--  SET ANY_OTH_AUTO_INSU = '1'        
--  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID        
-- END          
--ELSE        
BEGIN        
IF EXISTS(SELECT CUSTOMER_ID FROM APP_DRIVER_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID AND PARENTS_INSURANCE = 11934)        
  BEGIN        
   SET @PARENTS_INSURANCES='TRUE'        
  END        
IF EXISTS(SELECT CUSTOMER_ID FROM APP_DRIVER_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID and APP_ID=@APP_ID and APP_VERSION_ID=@APP_VERSION_ID AND PARENTS_INSURANCE = 11935)        
  BEGIN     
   DECLARE @DATE_BEF25YRS DATETIME    
   DECLARE @DATE_APPEFF DATETIME    
   SET @DATE_APPEFF = (SELECT APP_EFFECTIVE_DATE FROM APP_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID)    
   SET @DATE_BEF25YRS = DATEADD(YY, -25, @DATE_APPEFF)    
   IF EXISTS(SELECT * FROM APP_DRIVER_DETAILS WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID        
    AND DRIVER_ID IN (SELECT CLASS_DRIVERID FROM APP_VEHICLES WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID)    
    AND DRIVER_DOB < @DATE_BEF25YRS)    
  SET @PARENTS_INSURANCES='FALSE'        
   ELSE     
  SET @PARENTS_INSURANCES='TRUE'        
  END        
   --IF(@PARENTS_INSURANCES='FALSE')        
   -- BEGIN        
   --  UPDATE APP_AUTO_GEN_INFO        
   --  SET ANY_OTH_AUTO_INSU = '0'        
   --  WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID        
   -- END        
   IF(@PARENTS_INSURANCES='TRUE')          
    BEGIN        
     UPDATE APP_AUTO_GEN_INFO        
     SET ANY_OTH_AUTO_INSU = '1'        
     WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID        
    END        
END       */         

                                     
END                      


GO

