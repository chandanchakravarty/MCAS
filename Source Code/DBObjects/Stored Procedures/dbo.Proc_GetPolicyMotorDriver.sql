IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyMotorDriver]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyMotorDriver]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                
Proc Name           : Dbo.Proc_GetPolicyMotorDriver              
Created by            : Anurag Verma                
Date                    : 21/09/2005                
Purpose                : To get the policy motor driver details               
Revison History  :                
Used In                 : Wolverine               
Modified By             : shafi            
Date                    :20-01-2006            
Purpose                 : Column for DRIVER_PHYS_MED_IMPAIRE  and DRIVER_LIC_SUSPENDED                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
--  drop proc Proc_GetPolicyMotorDriver            
create PROC dbo.Proc_GetPolicyMotorDriver                    
(                    
 @CUSTOMER_ID int,                    
 @POLICY_ID  int,                    
 @POLICY_VERSION_ID int,                    
 @DRIVER_ID    smallint                    
)                    
AS                    
BEGIN                    
SELECT  CUSTOMER_ID, DRIVER_ID, DRIVER_FNAME, DRIVER_MNAME, DRIVER_LNAME, DRIVER_CODE, DRIVER_SUFFIX, DRIVER_ADD1,                    
 DRIVER_ADD2, DRIVER_CITY, DRIVER_STATE, DRIVER_ZIP, DRIVER_COUNTRY, DRIVER_HOME_PHONE, DRIVER_BUSINESS_PHONE,                    
 DRIVER_EXT, DRIVER_MOBILE, Convert(Varchar,DRIVER_DOB,101) As DRIVER_DOB, DRIVER_SSN, DRIVER_MART_STAT, DRIVER_SEX, DRIVER_DRIV_LIC,DRIVER_LIC_STATE,                    
 DRIVER_LIC_CLASS,Convert(Varchar, DATE_EXP_START,101) As DATE_EXP_START , Convert(Varchar, DATE_LICENSED, 101) As DATE_LICENSED,                    
 DRIVER_REL, DRIVER_DRIV_TYPE, DRIVER_OCC_CODE, DRIVER_OCC_CLASS,                    
 DRIVER_DRIVERLOYER_NAME, DRIVER_DRIVERLOYER_ADD, DRIVER_INCOME, DRIVER_BROADEND_NOFAULT,ISNULL(DRIVER_PHYS_MED_IMPAIRE,0) DRIVER_PHYS_MED_IMPAIRE,                    
 DRIVER_DRINK_VIOLATION, DRIVER_PREF_RISK, DRIVER_GOOD_STUDENT, DRIVER_STUD_DIST_OVER_HUNDRED,ISNULL(DRIVER_LIC_SUSPENDED,0) DRIVER_LIC_SUSPENDED,                    
 DRIVER_VOLUNTEER_POLICE_FIRE, DRIVER_US_CITIZEN, IS_ACTIVE, CREATED_BY, Convert(Varchar, CREATED_DATETIME, 101) as CREATED_DATETIME, MODIFIED_BY,                    
 Convert(Varchar, LAST_UPDATED_DATETIME, 101) as LAST_UPDATED_DATETIME,DRIVER_FAX,RELATIONSHIP,GOOD_DRIVER_STUDENT_DISCOUNT,                    
 PREMIER_DRIVER_DISCOUNT,SAFE_DRIVER_RENEWAL_DISCOUNT,VEHICLE_ID,PERCENT_DRIVEN, MATURE_DRIVER,MATURE_DRIVER_DISCOUNT,                     
 PREFERRED_RISK_DISCOUNT,PREFERRED_RISK, TRANSFEREXP_RENEWAL_DISCOUNT,TRANSFEREXPERIENCE_RENEWALCREDIT,SAFE_DRIVER,                  
 ISNULL(APP_VEHICLE_PRIN_OCC_ID,0) as APP_VEHICLE_PRIN_OCC_ID,          
 ISNULL(NO_CYCLE_ENDMT,'') as NO_CYCLE_ENDMT,CYCL_WITH_YOU,COLL_STUD_AWAY_HOME,Convert(Varchar, DATE_ORDERED, 101) As DATE_ORDERED,MVR_ORDERED,VIOLATIONS,    
  MVR_CLASS,MVR_LIC_CLASS,MVR_LIC_RESTR,MVR_DRIV_LIC_APPL,MVR_REMARKS,MVR_STATUS, LOSSREPORT_ORDER,
 CONVERT(VARCHAR,LOSSREPORT_DATETIME,101) LOSSREPORT_DATETIME                                                              
                    
FROM  POL_DRIVER_DETAILS                    
WHERE  CUSTOMER_ID = @CUSTOMER_ID AND                    
 POLICY_ID = @POLICY_ID AND                    
 POLICY_VERSION_ID = @POLICY_VERSION_ID AND                    
 DRIVER_ID = @DRIVER_ID                    
                    
END                  
                  
                  
                  
                
              
            
          
        
      
    
  



GO

