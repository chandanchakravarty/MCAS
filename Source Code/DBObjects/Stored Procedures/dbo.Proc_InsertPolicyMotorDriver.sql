IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertPolicyMotorDriver]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertPolicyMotorDriver]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*                          
----------------------------------------------------------                              
Proc Name       : Vijay Arora                              
Created by      : Proc_InsertPolicyMotorDriver                          
Date            : 14-11-2005                      
Purpose       : To add the Policy MotorCycle Driver Details                          
Revison History :                              
Used In    : Wolverine                  
                
Modified By :  Shafi                      
Date        :  13-02-2006                   
Purpose     :  Remove The Check For Duplicacy                              
                              
------------------------------------------------------------                              
Date        Review By          Comments                              
------   ------------       -------------------------*/                              
--drop proc Proc_InsertPolicyMotorDriver                
create  PROC dbo.Proc_InsertPolicyMotorDriver                              
                             
 @CUSTOMER_ID      INT,                              
 @POLICY_ID      INT,                              
 @POLICY_VERSION_ID  SMALLINT,                              
 @DRIVER_ID       SMALLINT  OUTPUT,                              
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
 @DRIVER_LIC_CLASS      NVARCHAR(10) = null,                              
 @DATE_LICENSED      DATETIME,                              
 @DRIVER_REL      NVARCHAR(10),                              
 @DRIVER_DRIV_TYPE      NVARCHAR(10),                              
 @DRIVER_OCC_CODE      NVARCHAR(12) = NULL,                              
 @DRIVER_OCC_CLASS      NVARCHAR(10),                              
 @DRIVER_DRIVERLOYER_NAME NVARCHAR(150),                              
 @DRIVER_DRIVERLOYER_ADD NVARCHAR(510),                              
 @DRIVER_INCOME      DECIMAL(18,2),                              
 @DRIVER_BROADEND_NOFAULT    SMALLINT = null,                              
 @DRIVER_PHYS_MED_IMPAIRE    NCHAR(2)=NULL,                              
 @DRIVER_DRINK_VIOLATION     NCHAR(1) = null,                              
 @DRIVER_PREF_RISK      NCHAR(2),                              
 @DRIVER_GOOD_STUDENT    NCHAR(2),                              
 @DRIVER_STUD_DIST_OVER_HUNDRED    NCHAR(2),                              
 @DRIVER_LIC_SUSPENDED  NCHAR(1)=NULL,                              
 @DRIVER_VOLUNTEER_POLICE_FIRE     NCHAR(2),                              
 @DRIVER_US_CITIZEN      NCHAR(2),                              
 @SAFE_DRIVER_RENEWAL_DISCOUNT DECIMAL(9,2)=NULL,                              
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
 @CREATED_BY      INT,              
 @CREATED_DATETIME    DATETIME,                  
 @CALLED_FROM varchar(10)=null,              
 @NO_CYCLE_ENDMT NCHAR(1)=null,            
 @CYCL_WITH_YOU smallint = null,            
 @COLL_STUD_AWAY_HOME smallint = null,          
 @MVR_CLASS varchar(50),              
 @MVR_LIC_CLASS varchar(50),              
 @MVR_LIC_RESTR varchar(50),              
 @MVR_DRIV_LIC_APPL varchar(50),              
 @VIOLATIONS int =null,        
 @MVR_ORDERED int,          
 @MVR_REMARKS varchar(250),      
 @MVR_STATUS varchar(10),                                             
 @LOSSREPORT_ORDER int=null,    
 @LOSSREPORT_DATETIME DateTime = null                                                
                      
                      
AS                              
BEGIN                              
                             
                          
 /*Checking the duplicay of code*/                   
/*                           
IF Exists(SELECT DRIVER_CODE FROM POL_DRIVER_DETAILS WHERE DRIVER_CODE = @DRIVER_CODE)                              
BEGIN                              
  SELECT @DRIVER_ID = -1                              
  return 0                              
END                            
 */                         
                          
SELECT @DRIVER_ID = IsNull(Max(DRIVER_ID),0) + 1 FROM POL_DRIVER_DETAILS                               
WHERE                           
CUSTOMER_ID = CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = POLICY_VERSION_ID                              
                              
INSERT INTO POL_DRIVER_DETAILS                              
 (                              
 CUSTOMER_ID,                              
 POLICY_ID,                              
 POLICY_VERSION_ID,                             
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
 SAFE_DRIVER_RENEWAL_DISCOUNT,      
 DRIVER_FAX,                               
 RELATIONSHIP,                              
 MATURE_DRIVER,                      
 MATURE_DRIVER_DISCOUNT,                      
 PREFERRED_RISK_DISCOUNT,                      
 PREFERRED_RISK,                      
 TRANSFEREXP_RENEWAL_DISCOUNT,                      
 TRANSFEREXPERIENCE_RENEWALCREDIT,                      
 VEHICLE_ID,                      
 PERCENT_DRIVEN,                      
 APP_VEHICLE_PRIN_OCC_ID,                      
 IS_ACTIVE,                               
 CREATED_BY,                              
 CREATED_DATETIME,              
 NO_CYCLE_ENDMT,            
 CYCL_WITH_YOU,            
 COLL_STUD_AWAY_HOME,          
 MVR_CLASS,              
 MVR_LIC_CLASS,              
 MVR_LIC_RESTR,              
 MVR_DRIV_LIC_APPL,        
 VIOLATIONS,        
 MVR_ORDERED,      
 MVR_REMARKS,      
 MVR_STATUS,         
 LOSSREPORT_ORDER,    
 LOSSREPORT_DATETIME                                                
             
 )                              
 VALUES                              
 (                              
 @CUSTOMER_ID,                  
 @POLICY_ID,                              
 @POLICY_VERSION_ID,                              
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
 @SAFE_DRIVER_RENEWAL_DISCOUNT,                              
 @DRIVER_FAX,                               
 @RELATIONSHIP,                              
 @MATURE_DRIVER,                      
 @MATURE_DRIVER_DISCOUNT,                      
 @PREFERRED_RISK_DISCOUNT,                      
 @PREFERRED_RISK,                      
 @TRANSFEREXP_RENEWAL_DISCOUNT,                      
 @TRANSFEREXPERIENCE_RENEWALCREDIT,                      
 @VEHICLE_ID,                      
 @PERCENT_DRIVEN,                      
 @APP_VEHICLE_PRIN_OCC_ID,                      
 'Y',    
 @CREATED_BY,                              
 GETDATE(),              
 @NO_CYCLE_ENDMT,            
 @CYCL_WITH_YOU,            
 @COLL_STUD_AWAY_HOME,          
 @MVR_CLASS,              
 @MVR_LIC_CLASS,              
 @MVR_LIC_RESTR,              
 @MVR_DRIV_LIC_APPL,        
 @VIOLATIONS,        
 @MVR_ORDERED,      
 @MVR_REMARKS,      
 @MVR_STATUS,          
 @LOSSREPORT_ORDER,    
 @LOSSREPORT_DATETIME                                                
              
 )         
                              
if(upper(@called_from)='MOT')                  
  EXEC Proc_SetMotorVehicleClassRuleForPolicy @CUSTOMER_ID, @POLICY_ID, @POLICY_VERSION_ID, @DRIVER_ID, @VEHICLE_ID, @DRIVER_DOB                                       
END                              
                              
                          
                            
                          
                          
                          
                        
                      
                    
                  
                
              
            
          
        
      
    
    
  
GO

