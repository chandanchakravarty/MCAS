IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertUmbrellaDriverDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertUmbrellaDriverDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
-- drop proc Proc_InsertUmbrellaDriverDetails                        
CREATE PROC dbo.Proc_InsertUmbrellaDriverDetails                    
(                    
 @CUSTOMER_ID      int,                    
 @APP_ID   int,                    
 @APP_VERSION_ID  int,                    
 @DRIVER_ID       smallint  OUTPUT,                    
 @DRIVER_FNAME      nvarchar(550)=null,                    
 @DRIVER_MNAME      nvarchar(550)=null,                    
 @DRIVER_LNAME      nvarchar(550)=null,                    
 @DRIVER_CODE      nvarchar(520)=null,                    
 @DRIVER_SUFFIX      nvarchar(520)=null,                    
 @DRIVER_ADD1      nvarchar(540)=null,                    
 @DRIVER_ADD2      nvarchar(540)=null,                    
 @DRIVER_CITY      nvarchar(100)=null,                    
 @DRIVER_STATE      nvarchar(110)=null,                    
 @DRIVER_ZIP      nvarchar(320)=null,                    
 @DRIVER_COUNTRY      nchar(410)=null,                    
 @DRIVER_HOME_PHONE      nvarchar(240)=null,                    
 @DRIVER_BUSINESS_PHONE  nvarchar(240)=null,                    
 @DRIVER_EXT      nvarchar(210)=null,                    
 @DRIVER_MOBILE      nvarchar(140)=null,                    
 @DRIVER_DOB      datetime=null,                    
 @DRIVER_SSN      nvarchar(520)=null,                    
 @DRIVER_MART_STAT      nchar(20)=null,                    
 @DRIVER_SEX      nchar(20)=null,                    
 @DRIVER_DRIV_LIC      nvarchar(160)=null,                    
 @DRIVER_LIC_STATE      nvarchar(110)=null,                    
 @DRIVER_LIC_CLASS      nvarchar(110)=null,                    
 @DATE_LICENSED       datetime=null,                    
 @DRIVER_REL      nvarchar(110)=null,                    
 @DRIVER_DRIV_TYPE      nvarchar(110)=null,                    
 @DRIVER_OCC_CODE      nvarchar(120)=null,                    
 @DRIVER_OCC_CLASS      nvarchar(100)=null,                    
 @DRIVER_DRIVERLOYER_NAME nvarchar(550)=null,                    
 @DRIVER_DRIVERLOYER_ADD nvarchar(510)=null,                    
 @DRIVER_INCOME      decimal(18,2)=null,                    
 @DRIVER_BROADEND_NOFAULT    smallint=null,                    
 @DRIVER_PHYS_MED_IMPAIRE    nchar(20)=null,                    
 @DRIVER_DRINK_VIOLATION     nchar(20)=null,                    
 @DRIVER_PREF_RISK      nchar(20)=null,                    
 @DRIVER_GOOD_STUDENT    nchar(20)=null,                    
 @DRIVER_STUD_DIST_OVER_HUNDRED    nchar(20)=null,                    
 @DRIVER_LIC_SUSPENDED  nchar(20)=null,                    
 @DRIVER_VOLUNTEER_POLICE_FIRE     nchar(20)=null,                    
 @DRIVER_US_CITIZEN      nchar(20)=null,                    
 @CREATED_BY      int=null,                    
 @CREATED_DATETIME      datetime=null,                    
 @MODIFIED_BY      int=null,                    
 @LAST_UPDATED_DATETIME  datetime=null,                    
 @INSERTUPDATE  varchar(10)=null,                    
 @DRIVER_FAX varchar(400)= null,                    
 @RELATIONSHIP int = null,                  
 @SAFE_DRIVER_RENEWAL_DISCOUNT nchar(2)=null,                
 @APP_VEHICLE_PRIN_OCC_ID   INT=null,              
 @VEHICLE_ID  INT=null,      
 @OP_VEHICLE_ID smallint=NULL,              
 @OP_APP_VEHICLE_PRIN_OCC_ID  INT=null,            
 @OP_DRIVER_COST_GAURAD_AUX     int =null,  
 @FORM_F95 INT=NULL,
 @MOT_VEHICLE_ID smallint = null,
 @MOT_APP_VEHICLE_PRIN_OCC_ID smallint = null                     
    
)                    
AS                    
BEGIN     



               
                    
if @INSERTUPDATE = 'I'                    
 BEGIN                    
                      
                     
  /*Generating the new driver id*/                    
  SELECT @DRIVER_ID = IsNull(Max(DRIVER_ID),0) + 1 FROM APP_UMBRELLA_DRIVER_DETAILS                     
                     
  INSERT INTO APP_UMBRELLA_DRIVER_DETAILS                     
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
   DATE_LICENSED,                    
   DRIVER_REL,                    
   DRIVER_DRIV_TYPE,                    
   DRIVER_OCC_CODE,                    
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
   DRIVER_FAX,                    
   RELATIONSHIP,                  
   SAFE_DRIVER_RENEWAL_DISCOUNT,                
   APP_VEHICLE_PRIN_OCC_ID,              
   VEHICLE_ID,       
   --Added By Ravindra(02-27-2006)      
   OP_VEHICLE_ID,      
   OP_APP_VEHICLE_PRIN_OCC_ID,      
   OP_DRIVER_COST_GAURAD_AUX,  
   FORM_F95,
   MOT_VEHICLE_ID,
   MOT_APP_VEHICLE_PRIN_OCC_ID       
   --------------               
               
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
   @DATE_LICENSED,                    
   @DRIVER_REL,                    
   @DRIVER_DRIV_TYPE,                    
   @DRIVER_OCC_CODE,                    
   @DRIVER_OCC_CLASS,                    
   @DRIVER_DRIVERLOYER_NAME,                    
   @DRIVER_DRIVERLOYER_ADD,                    
   @DRIVER_INCOME,                    
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
   @SAFE_DRIVER_RENEWAL_DISCOUNT,                
   @APP_VEHICLE_PRIN_OCC_ID,              
   @VEHICLE_ID ,       
   @OP_VEHICLE_ID,      
   @OP_APP_VEHICLE_PRIN_OCC_ID,      
   @OP_DRIVER_COST_GAURAD_AUX,  
   @FORM_F95,
   @MOT_VEHICLE_ID,
   @MOT_APP_VEHICLE_PRIN_OCC_ID              
         
  )                    
 END                    
ELSE                    
 BEGIN      
  /*Update the values */     
--delete MVR Informaton  if driver name was changed                    
declare @OLD_DRIVER_FNAME VARCHAR(30)
declare @OLD_DRIVER_MNAME VARCHAR(30)
declare @OLD_DRIVER_LNAME VARCHAR(30)
SELECT  @OLD_DRIVER_FNAME=DRIVER_FNAME,@OLD_DRIVER_MNAME=isnull(DRIVER_MNAME,''),@OLD_DRIVER_LNAME=DRIVER_LNAME 
   FROM APP_UMBRELLA_DRIVER_DETAILS WHERE  CUSTOMER_ID  = @CUSTOMER_ID AND                                                    
    APP_ID   = @APP_ID AND  APP_VERSION_ID  = @APP_VERSION_ID AND  DRIVER_ID   = @DRIVER_ID              
if (@OLD_DRIVER_FNAME<>@DRIVER_FNAME or @OLD_DRIVER_MNAME<> isnull(@DRIVER_MNAME,'') or @OLD_DRIVER_LNAME<>@DRIVER_LNAME )
   delete from app_mvr_information WHERE  CUSTOMER_ID  = @CUSTOMER_ID AND                                                   
    APP_ID   = @APP_ID AND  APP_VERSION_ID  = @APP_VERSION_ID AND  DRIVER_ID   = @DRIVER_ID 
--end here              
              
  UPDATE APP_UMBRELLA_DRIVER_DETAILS                     
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
   DATE_LICENSED   = @DATE_LICENSED,                     
   DRIVER_REL   = @DRIVER_REL,                     
   DRIVER_DRIV_TYPE  = @DRIVER_DRIV_TYPE,                     
   DRIVER_OCC_CODE  = @DRIVER_OCC_CODE,                     
   DRIVER_OCC_CLASS  = @DRIVER_OCC_CLASS,                     
   DRIVER_DRIVERLOYER_NAME = @DRIVER_DRIVERLOYER_NAME,                     
   DRIVER_DRIVERLOYER_ADD  = @DRIVER_DRIVERLOYER_ADD,                     
   DRIVER_INCOME    = @DRIVER_INCOME,                     
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
   SAFE_DRIVER_RENEWAL_DISCOUNT=@SAFE_DRIVER_RENEWAL_DISCOUNT,                
   APP_VEHICLE_PRIN_OCC_ID = @APP_VEHICLE_PRIN_OCC_ID,              
   VEHICLE_ID = @VEHICLE_ID    ,      
   --Added By Ravindra(02-27-2006)      
   OP_VEHICLE_ID = @OP_VEHICLE_ID,      
   OP_APP_VEHICLE_PRIN_OCC_ID =@OP_APP_VEHICLE_PRIN_OCC_ID,      
   OP_DRIVER_COST_GAURAD_AUX = @OP_DRIVER_COST_GAURAD_AUX,  
   FORM_F95 = @FORM_F95,
   MOT_VEHICLE_ID=@MOT_VEHICLE_ID,
   MOT_APP_VEHICLE_PRIN_OCC_ID=@MOT_APP_VEHICLE_PRIN_OCC_ID             
   --------------            
               
  WHERE                    
   CUSTOMER_ID  = @CUSTOMER_ID AND                    
   APP_ID   = @APP_ID AND                    
   APP_VERSION_ID  = @APP_VERSION_ID AND           
   DRIVER_ID   = @DRIVER_ID                    
                      
  /*Updating values end*/                     
 END        
    
--Added by Swastika on 24th Apr'06 for Gen Iss #2557    
EXEC PROC_SetUmbrellaAppVehicleClassRule @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@DRIVER_ID,NULL                     
                    
END              
      
    
    
    
    
   
    
    
  





GO

