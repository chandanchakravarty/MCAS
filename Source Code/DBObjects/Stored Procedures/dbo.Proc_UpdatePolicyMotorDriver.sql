IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdatePolicyMotorDriver]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdatePolicyMotorDriver]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*                              
----------------------------------------------------------                                  
Proc Name       : Vijay Arora                                  
Created by      : Proc_UpdatePolicyMotorDriver                              
Date            : 14-11-2005                          
Purpose       : To update the Policy MotorCycle Driver Details                              
Revison History :                                  
Used In    : Wolverine                                  
                                  
------------------------------------------------------------                                  
Date        Review By          Comments                                  
------   ------------       -------------------------*/               
--Drop PROC dbo.Proc_UpdatePolicyMotorDriver                              
CREATE  PROC dbo.Proc_UpdatePolicyMotorDriver                                  
                                 
 @CUSTOMER_ID      INT,                                  
 @POLICY_ID      INT,                                  
 @POLICY_VERSION_ID  SMALLINT,                                  
 @DRIVER_ID       SMALLINT,                                  
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
 @DRIVER_SSN      NVARCHAR(88),                                  
 @DRIVER_MART_STAT      NCHAR(2),                                  
 @DRIVER_SEX      NCHAR(2),                                  
 @DRIVER_DRIV_LIC      NVARCHAR(60),                                  
 @DRIVER_LIC_STATE      NVARCHAR(10),                                  
 @DRIVER_LIC_CLASS      NVARCHAR(10),                                  
 @DATE_LICENSED      DATETIME,                                  
 @DRIVER_REL      NVARCHAR(10),                                  
 @DRIVER_DRIV_TYPE      NVARCHAR(10),                                  
 @DRIVER_OCC_CODE      NVARCHAR(12),                                  
 @DRIVER_OCC_CLASS      NVARCHAR(10),                                  
 @DRIVER_DRIVERLOYER_NAME NVARCHAR(150),                                  
 @DRIVER_DRIVERLOYER_ADD NVARCHAR(510),                                  
 @DRIVER_INCOME      DECIMAL(18,2),                                  
 @DRIVER_BROADEND_NOFAULT    SMALLINT,                                  
 @DRIVER_PHYS_MED_IMPAIRE    NCHAR(2)= NULL,                                  
 @DRIVER_DRINK_VIOLATION     NCHAR(1),                                  
 @DRIVER_PREF_RISK      NCHAR(2),                                  
 @DRIVER_GOOD_STUDENT    NCHAR(2),                                  
 @DRIVER_STUD_DIST_OVER_HUNDRED    NCHAR(2),                                  
 @DRIVER_LIC_SUSPENDED  NCHAR(1)= NULL,                                  
 @DRIVER_VOLUNTEER_POLICE_FIRE     NCHAR(2),                                  
 @DRIVER_US_CITIZEN      NCHAR(2),                                  
 @SAFE_DRIVER_RENEWAL_DISCOUNT DECIMAL(9,2)= NULL,                                  
 @DRIVER_FAX VARCHAR(40)= NULL,                                   
 @RELATIONSHIP INT = NULL,                                  
 @MATURE_DRIVER NCHAR(1),                          
 @MATURE_DRIVER_DISCOUNT DECIMAL(5),                          
 @PREFERRED_RISK_DISCOUNT DECIMAL(5),                          
 @PREFERRED_RISK NCHAR(1),  

--Default value NULL added by Charles on 2-Jul-09 for Itack 6012                          
 @TRANSFEREXP_RENEWAL_DISCOUNT DECIMAL(5)=NULL,
--Default value NULL added by Charles on 2-Jul-09 for Itack 6012                  
 @TRANSFEREXPERIENCE_RENEWALCREDIT NCHAR(1)=NULL,                          

 @VEHICLE_ID SMALLINT,                          
 @PERCENT_DRIVEN DECIMAL(5),                          
 @APP_VEHICLE_PRIN_OCC_ID INT,                          
 @MODIFIED_BY      INT,                                  
 @LAST_UPDATED_DATETIME    DATETIME,                      
 @CALLED_FROM VARCHAR(10),                
 @NO_CYCLE_ENDMT NCHAR(1)=null,            
 @CYCL_WITH_YOU smallint = null,            
 @COLL_STUD_AWAY_HOME smallint = null,                
 @VIOLATIONS int = null,              
 @MVR_ORDERED int = null,              
 @DATE_ORDERED DATETIME=null,        
 @MVR_CLASS varchar(50),            
 @MVR_LIC_CLASS varchar(50),            
 @MVR_LIC_RESTR varchar(50),            
 @MVR_DRIV_LIC_APPL varchar(50),      
 @MVR_REMARKS varchar(250),      
 @MVR_STATUS varchar(10),                                             
 @LOSSREPORT_ORDER int=null,    
 @LOSSREPORT_DATETIME DateTime =null                                                
           
              
                          
                          
AS                                  
BEGIN                               
                    
if(upper(@called_from)='MOT')                      
  EXEC Proc_SetMotorVehicleClassRuleOnChangeForPolicy @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID, @DRIVER_ID                    
                    
--delete MVR Informaton  if driver name was changed                              
declare @OLD_DRIVER_FNAME VARCHAR(30)          
declare @OLD_DRIVER_MNAME VARCHAR(30)          
declare @OLD_DRIVER_LNAME VARCHAR(30)          
SELECT  @OLD_DRIVER_FNAME=DRIVER_FNAME,@OLD_DRIVER_MNAME=isnull(DRIVER_MNAME,''),@OLD_DRIVER_LNAME=DRIVER_LNAME           
   FROM POL_DRIVER_DETAILS WHERE  CUSTOMER_ID  = @CUSTOMER_ID AND                                                              
   POLICY_ID   = @POLICY_ID AND  POLICY_VERSION_ID  = @POLICY_VERSION_ID AND  DRIVER_ID   = @DRIVER_ID                       
if (@OLD_DRIVER_FNAME<>@DRIVER_FNAME or @OLD_DRIVER_MNAME<> isnull(@DRIVER_MNAME,'') or @OLD_DRIVER_LNAME<>@DRIVER_LNAME )          
   delete from POL_mvr_information WHERE  CUSTOMER_ID  = @CUSTOMER_ID AND                                                             
    POLICY_ID   = @POLICY_ID AND  POLICY_VERSION_ID  = @POLICY_VERSION_ID AND  DRIVER_ID   = @DRIVER_ID           
--end here                                
                          
UPDATE POL_DRIVER_DETAILS                                  
SET                           
 DRIVER_FNAME = @DRIVER_FNAME,                                  
 DRIVER_MNAME = @DRIVER_MNAME,                                  
 DRIVER_LNAME = @DRIVER_LNAME,                                  
 DRIVER_CODE = @DRIVER_CODE,                                  
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
 DRIVER_SSN = @DRIVER_SSN,                                   DRIVER_MART_STAT = @DRIVER_MART_STAT,                               
 DRIVER_SEX = @DRIVER_SEX,                                  
 DRIVER_DRIV_LIC = @DRIVER_DRIV_LIC,                                  
 DRIVER_LIC_STATE = @DRIVER_LIC_STATE,            
 DRIVER_LIC_CLASS = @DRIVER_LIC_CLASS,                                  
 DATE_LICENSED = @DATE_LICENSED,                                  
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
 SAFE_DRIVER_RENEWAL_DISCOUNT = @SAFE_DRIVER_RENEWAL_DISCOUNT,                                  
 DRIVER_FAX = @DRIVER_FAX,                                   
 RELATIONSHIP = @RELATIONSHIP,                                  
 MATURE_DRIVER = @MATURE_DRIVER,                          
 MATURE_DRIVER_DISCOUNT = @MATURE_DRIVER_DISCOUNT,                          
 PREFERRED_RISK_DISCOUNT = @PREFERRED_RISK_DISCOUNT,                          
 PREFERRED_RISK = @PREFERRED_RISK,                          
 TRANSFEREXP_RENEWAL_DISCOUNT = @TRANSFEREXP_RENEWAL_DISCOUNT,                          
 TRANSFEREXPERIENCE_RENEWALCREDIT = @TRANSFEREXPERIENCE_RENEWALCREDIT,                          
 VEHICLE_ID = @VEHICLE_ID,                          
 PERCENT_DRIVEN = @PERCENT_DRIVEN,                          
 APP_VEHICLE_PRIN_OCC_ID = @APP_VEHICLE_PRIN_OCC_ID,                     
 MODIFIED_BY = @MODIFIED_BY,                                  
 LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME,                
 NO_CYCLE_ENDMT = @NO_CYCLE_ENDMT,            
 CYCL_WITH_YOU = @CYCL_WITH_YOU,            
 COLL_STUD_AWAY_HOME= @COLL_STUD_AWAY_HOME,                
 VIOLATIONS=@VIOLATIONS,               
 MVR_ORDERED=@MVR_ORDERED,               
 DATE_ORDERED=@DATE_ORDERED,        
 MVR_CLASS = @MVR_CLASS,            
 MVR_LIC_CLASS = @MVR_LIC_CLASS,            
 MVR_LIC_RESTR = @MVR_LIC_RESTR,            
 MVR_DRIV_LIC_APPL = @MVR_DRIV_LIC_APPL,      
 MVR_REMARKS = @MVR_REMARKS,      
 MVR_STATUS = @MVR_STATUS,                                             
 LOSSREPORT_ORDER=@LOSSREPORT_ORDER,    
 LOSSREPORT_DATETIME=@LOSSREPORT_DATETIME                                                                          
                  
WHERE                           
 CUSTOMER_ID =  @CUSTOMER_ID AND                                  
 POLICY_ID =  @POLICY_ID AND                                  
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND                   
 DRIVER_ID = @DRIVER_ID                                  
                      
                      
if(upper(@called_from)='MOT')                      
  EXEC Proc_SetMotorVehicleClassRuleForPolicy @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID, @DRIVER_ID, @VEHICLE_ID, @DRIVER_DOB                                           
END                                  
                                  
                              
                  
                              
                              
                              
               
                          
                        
                      
                    
                  
                
              
            
          
          
          
        
      
    
    
  
GO

