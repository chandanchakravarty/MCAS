IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicyDriverDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicyDriverDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*                      
----------------------------------------------------------                          
Proc Name       : Vijay Arora                      
Created by      : Proc_GetPolicyDriverDetails                          
Date            : 07-11-2005                      
Purpose      : Select the Policy Driver Details.                      
Revison History :                          
Used In   : Wolverine                          
------------------------------------------------------------                          
Date     Review By          Comments                          
------   ------------       -------------------------*/                          
--drop proc dbo.Proc_GetPolicyDriverDetails                
CREATE PROC dbo.Proc_GetPolicyDriverDetails                          
(                          
 @CUSTOMER_ID int,                          
 @POLICY_ID  int,                          
 @POLICY_VERSION_ID int,                          
 @DRIVER_ID    smallint                          
)                          
AS                          
BEGIN                          
SELECT  PDD.CUSTOMER_ID, DRIVER_ID, DRIVER_FNAME, DRIVER_MNAME, DRIVER_LNAME, DRIVER_CODE, DRIVER_SUFFIX, DRIVER_ADD1,                          
 DRIVER_ADD2, DRIVER_CITY, DRIVER_STATE, DRIVER_ZIP, DRIVER_COUNTRY, DRIVER_HOME_PHONE, DRIVER_BUSINESS_PHONE,                          
 DRIVER_EXT, DRIVER_MOBILE, Convert(Varchar,DRIVER_DOB,101) As DRIVER_DOB, DRIVER_SSN, DRIVER_MART_STAT, DRIVER_SEX, DRIVER_DRIV_LIC,DRIVER_LIC_STATE,                          
 DRIVER_LIC_CLASS,Convert(Varchar, DATE_EXP_START,101) As DATE_EXP_START , Convert(Varchar, DATE_LICENSED, 101) As DATE_LICENSED,                          
 DRIVER_REL, DRIVER_DRIV_TYPE, DRIVER_OCC_CODE, DRIVER_OCC_CLASS,                          
 DRIVER_DRIVERLOYER_NAME, DRIVER_DRIVERLOYER_ADD, DRIVER_INCOME, DRIVER_BROADEND_NOFAULT, DRIVER_PHYS_MED_IMPAIRE,                          
 DRIVER_DRINK_VIOLATION, DRIVER_PREF_RISK, DRIVER_GOOD_STUDENT, DRIVER_STUD_DIST_OVER_HUNDRED, DRIVER_LIC_SUSPENDED,                          
 DRIVER_VOLUNTEER_POLICE_FIRE, DRIVER_US_CITIZEN, PDD.IS_ACTIVE, PDD.CREATED_BY, Convert(Varchar, PDD.CREATED_DATETIME, 101) as CREATED_DATETIME, PDD.MODIFIED_BY,                          
 Convert(Varchar, PDD.LAST_UPDATED_DATETIME, 101) as LAST_UPDATED_DATETIME,DRIVER_FAX,RELATIONSHIP,GOOD_DRIVER_STUDENT_DISCOUNT,                          
 PREMIER_DRIVER_DISCOUNT,                
--SAFE_DRIVER_RENEWAL_DISCOUNT,                
CAST(ISNULL(SAFE_DRIVER_RENEWAL_DISCOUNT,0) as int) as SAFE_DRIVER_RENEWAL_DISCOUNT,                
VEHICLE_ID,PERCENT_DRIVEN, MATURE_DRIVER,MATURE_DRIVER_DISCOUNT,                           
 PREFERRED_RISK_DISCOUNT,PREFERRED_RISK, TRANSFEREXP_RENEWAL_DISCOUNT,TRANSFEREXPERIENCE_RENEWALCREDIT,SAFE_DRIVER,                        
 ISNULL(APP_VEHICLE_PRIN_OCC_ID,0) as APP_VEHICLE_PRIN_OCC_ID,NO_DEPENDENTS,  isnull(WAIVER_WORK_LOSS_BENEFITS,'') as WAIVER_WORK_LOSS_BENEFITS,                
FORM_F95,EXT_NON_OWN_COVG_INDIVI,FULL_TIME_STUDENT,SUPPORT_DOCUMENT,SIGNED_WAIVER_BENEFITS_FORM,            
HAVE_CAR,IN_MILITARY,STATIONED_IN_US_TERR,PARENTS_INSURANCE, VIOLATIONS,MVR_ORDERED,CONVERT(VARCHAR(10),DATE_ORDERED,101) AS DATE_ORDERED,    
  MVR_CLASS,MVR_LIC_CLASS,MVR_LIC_RESTR,MVR_DRIV_LIC_APPL,MVR_REMARKS,MVR_STATUS, LOSSREPORT_ORDER,
 CONVERT(VARCHAR,LOSSREPORT_DATETIME,101) LOSSREPORT_DATETIME, APP_EFFECTIVE_DATE                                            
         
FROM  POL_DRIVER_DETAILS PDD  inner join POL_CUSTOMER_POLICY_LIST PCPL
on    PDD.CUSTOMER_ID = PCPL.CUSTOMER_ID AND
  PDD.POLICY_ID =  PCPL.POLICY_ID  AND
PDD.POLICY_VERSION_ID  =  PCPL.POLICY_VERSION_ID             
WHERE  PDD.CUSTOMER_ID = @CUSTOMER_ID AND                          
 PDD.POLICY_ID = @POLICY_ID AND                          
 PDD.POLICY_VERSION_ID = @POLICY_VERSION_ID AND                          
 PDD.DRIVER_ID = @DRIVER_ID                          
                          
END                        

GO

