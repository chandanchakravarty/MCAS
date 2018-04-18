IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyUmbrellaDriver]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyUmbrellaDriver]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*              
----------------------------------------------------------                  
Proc Name       : Vijay Arora                  
Created by      : Proc_UpdatePolicyUmbrellaDriver              
Date            : 08-11-2005              
Purpose      : To update the Policy Driver Details              
Revison History :                  
Used In   : Wolverine     
  
            
                  
------------------------------------------------------------                  
Date        Review By          Comments                  
------   ------------       -------------------------*/         
--   drop proc Proc_UpdatePolicyUmbrellaDriver   
CREATE  PROC Dbo.Proc_UpdatePolicyUmbrellaDriver                  
                 
 @CUSTOMER_ID      INT,                  
 @POLICY_ID      INT,                  
 @POLICY_VERSION_ID  SMALLINT,                  
 @DRIVER_ID       SMALLINT  ,                  
 @DRIVER_FNAME      NVARCHAR(150),                  
 @DRIVER_MNAME      NVARCHAR(50),                  
 @DRIVER_LNAME      NVARCHAR(150),                  
 @DRIVER_CODE      NVARCHAR(20),                  
 @DRIVER_SUFFIX      NVARCHAR(20),                  
 @DRIVER_ADD1      NVARCHAR(140),                  
 @DRIVER_ADD2      NVARCHAR(140),                  
 @DRIVER_CITY      NVARCHAR(80),                  
 @DRIVER_STATE      NVARCHAR(10),                  
 @DRIVER_ZIP      NVARCHAR(20),                  
 @DRIVER_COUNTRY      NCHAR(10),                  
 @DRIVER_HOME_PHONE      NVARCHAR(40),                  
 @DRIVER_BUSINESS_PHONE  NVARCHAR(40),                  
 @DRIVER_EXT      NVARCHAR(10),                  
 @DRIVER_MOBILE      NVARCHAR(40),                  
 @DRIVER_DOB      DATETIME,                  
 @DRIVER_SSN      NVARCHAR(22),                  
 @DRIVER_MART_STAT      NCHAR(2),                  
 @DRIVER_SEX      NCHAR(2),                  
 @DRIVER_DRIV_LIC      NVARCHAR(60),                  
 @DRIVER_LIC_STATE      NVARCHAR(10),                  
 @DRIVER_LIC_CLASS      NVARCHAR(10),                  
 @DATE_EXP_START      DATETIME,                  
 @DRIVER_REL      NVARCHAR(10),                  
 @DRIVER_DRIV_TYPE      NVARCHAR(10),                  
 @DRIVER_OCC_CODE      NVARCHAR(12),                  
 @DRIVER_OCC_CLASS      NVARCHAR(10),                  
 @DRIVER_DRIVERLOYER_NAME NVARCHAR(150),                  
 @DRIVER_DRIVERLOYER_ADD NVARCHAR(510),                  
 @DRIVER_INCOME      DECIMAL(18,2),                  
 @DRIVER_BROADEND_NOFAULT    SMALLINT,                  
 @DRIVER_PHYS_MED_IMPAIRE    NCHAR(2)=NULL,                  
 @DRIVER_DRINK_VIOLATION     NCHAR(1),                  
 @DRIVER_PREF_RISK      NCHAR(2),                  
 @DRIVER_GOOD_STUDENT    NCHAR(2),                  
 @DRIVER_STUD_DIST_OVER_HUNDRED    NCHAR(2),                  
 @DRIVER_LIC_SUSPENDED  NCHAR(1)=NULL,                  
 @DRIVER_VOLUNTEER_POLICE_FIRE     NCHAR(2),                  
 @DRIVER_US_CITIZEN      NCHAR(2),                  
 @RELATIONSHIP INT = NULL,                  
-- @SAFE_DRIVER BIT,                  
-- @GOOD_DRIVER_STUDENT_DISCOUNT DECIMAL(9,2)=NULL,                  
-- @PREMIER_DRIVER_DISCOUNT DECIMAL(9,2)=NULL,                  
 @SAFE_DRIVER_RENEWAL_DISCOUNT DECIMAL(9,2)=NULL,                  
 @VEHICLE_ID     SMALLINT=NULL,                   
-- @PERCENT_DRIVEN DECIMAL(9,2)=NULL,                   
 @APP_VEHICLE_PRIN_OCC_ID  INT,              
 @DRIVER_FAX VARCHAR(40)= NULL,                   
 @MODIFIED_BY      INT,                  
 @LAST_UPDATED_DATETIME    DATETIME,          
 @CALLED_FROM VARCHAR(10)=NULL        
-- @NO_DEPENDENTS  smallint,    
-- @WAIVER_WORK_LOSS_BENEFITS nchar(1)=null        
              
AS                  
BEGIN                  
                
 /*Checking the duplicay of code*/                  
/*  
IF Exists(SELECT DRIVER_CODE FROM POL_DRIVER_DETAILS WHERE DRIVER_CODE = @DRIVER_CODE              
          AND DRIVER_ID != @DRIVER_ID)                  
BEGIN                  
  SELECT @DRIVER_ID = -1                  
  return 0                  
END                   
 */             
--delete MVR Informaton  if driver name was changed                    
declare @OLD_DRIVER_FNAME VARCHAR(30)
declare @OLD_DRIVER_MNAME VARCHAR(30)
declare @OLD_DRIVER_LNAME VARCHAR(30)
SELECT  @OLD_DRIVER_FNAME=DRIVER_FNAME,@OLD_DRIVER_MNAME=isnull(DRIVER_MNAME,''),@OLD_DRIVER_LNAME=DRIVER_LNAME 
   FROM POL_UMBRELLA_DRIVER_DETAILS WHERE  CUSTOMER_ID  = @CUSTOMER_ID AND                                                    
   POLICY_ID   = @POLICY_ID AND  POLICY_VERSION_ID  = @POLICY_VERSION_ID AND  DRIVER_ID   = @DRIVER_ID             
if (@OLD_DRIVER_FNAME<>@DRIVER_FNAME or @OLD_DRIVER_MNAME<> isnull(@DRIVER_MNAME,'') or @OLD_DRIVER_LNAME<>@DRIVER_LNAME )
   delete from POL_mvr_information WHERE  CUSTOMER_ID  = @CUSTOMER_ID AND                                                   
    POLICY_ID   = @POLICY_ID AND  POLICY_VERSION_ID  = @POLICY_VERSION_ID AND  DRIVER_ID   = @DRIVER_ID 
--end here   
UPDATE POL_UMBRELLA_DRIVER_DETAILS                  
SET               
               
 DRIVER_FNAME = @DRIVER_FNAME,                  
 DRIVER_MNAME = @DRIVER_MNAME,          
 DRIVER_LNAME = DRIVER_LNAME,                  
 DRIVER_CODE =  @DRIVER_CODE,               
 DRIVER_SUFFIX = @DRIVER_SUFFIX,                  
 DRIVER_ADD1 = @DRIVER_ADD1,                  
 DRIVER_ADD2 = @DRIVER_ADD2,                  
 DRIVER_CITY = @DRIVER_CITY,                  
 DRIVER_STATE = @DRIVER_STATE,                  
 DRIVER_ZIP = @DRIVER_ZIP,                  
 DRIVER_COUNTRY = @DRIVER_COUNTRY,                  
 DRIVER_HOME_PHONE = @DRIVER_HOME_PHONE,                  
 DRIVER_BUSINESS_PHONE = @DRIVER_BUSINESS_PHONE,                  
 DRIVER_EXT = @DRIVER_EXT,                  
 DRIVER_MOBILE = @DRIVER_MOBILE,                  
 DRIVER_DOB = @DRIVER_DOB,                  
 DRIVER_SSN = @DRIVER_SSN,                  
 DRIVER_MART_STAT = @DRIVER_MART_STAT,                  
 DRIVER_SEX = @DRIVER_SEX,                  
 DRIVER_DRIV_LIC = @DRIVER_DRIV_LIC,                  
 DRIVER_LIC_STATE = @DRIVER_LIC_STATE,                  
 DRIVER_LIC_CLASS = @DRIVER_LIC_CLASS,                  
 DATE_EXP_START = @DATE_EXP_START,                  
 DRIVER_REL = @DRIVER_REL,                  
 DRIVER_DRIV_TYPE = @DRIVER_DRIV_TYPE,                  
 DRIVER_OCC_CODE = @DRIVER_OCC_CODE,                  
 DRIVER_OCC_CLASS = @DRIVER_OCC_CLASS,                  
 DRIVER_DRIVERLOYER_NAME = @DRIVER_DRIVERLOYER_NAME,                  
 DRIVER_DRIVERLOYER_ADD = @DRIVER_DRIVERLOYER_ADD,                  
 DRIVER_INCOME = @DRIVER_INCOME,                  
 DRIVER_BROADEND_NOFAULT = @DRIVER_BROADEND_NOFAULT,                  
 DRIVER_PHYS_MED_IMPAIRE = @DRIVER_PHYS_MED_IMPAIRE,                  
 DRIVER_DRINK_VIOLATION = @DRIVER_DRINK_VIOLATION,                  
 DRIVER_PREF_RISK = @DRIVER_PREF_RISK,                  
 DRIVER_GOOD_STUDENT = @DRIVER_GOOD_STUDENT,                  
 DRIVER_STUD_DIST_OVER_HUNDRED = @DRIVER_STUD_DIST_OVER_HUNDRED,                  
 DRIVER_LIC_SUSPENDED = @DRIVER_LIC_SUSPENDED,                  
 DRIVER_VOLUNTEER_POLICE_FIRE = @DRIVER_VOLUNTEER_POLICE_FIRE,                  
 DRIVER_US_CITIZEN = @DRIVER_US_CITIZEN,                  
 RELATIONSHIP = @RELATIONSHIP,                  
-- SAFE_DRIVER = @SAFE_DRIVER,                  
-- GOOD_DRIVER_STUDENT_DISCOUNT = @GOOD_DRIVER_STUDENT_DISCOUNT,                  
-- PREMIER_DRIVER_DISCOUNT = @PREMIER_DRIVER_DISCOUNT,                  
 SAFE_DRIVER_RENEWAL_DISCOUNT = @SAFE_DRIVER_RENEWAL_DISCOUNT,                  
 VEHICLE_ID = @VEHICLE_ID,                   
-- PERCENT_DRIVEN = @PERCENT_DRIVEN,                   
 APP_VEHICLE_PRIN_OCC_ID = @APP_VEHICLE_PRIN_OCC_ID,              
 DRIVER_FAX = @DRIVER_FAX,              
 MODIFIED_BY = @MODIFIED_BY,              
 LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME         
-- NO_DEPENDENTS=@NO_DEPENDENTS,    
-- WAIVER_WORK_LOSS_BENEFITS = @WAIVER_WORK_LOSS_BENEFITS    
               
WHERE CUSTOMER_ID = @CUSTOMER_ID AND                  
 POLICY_ID = @POLICY_ID AND                  
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND                  
 DRIVER_ID = @DRIVER_ID                  
          

          
               
                 
END                  
                  
              
                
              
              
              
            
          
        
      
    
  





GO

