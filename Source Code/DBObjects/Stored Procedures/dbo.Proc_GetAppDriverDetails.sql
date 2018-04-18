IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAppDriverDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAppDriverDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[Proc_GetAppDriverDetails]                                
(                                
 @CUSTOMER_ID int,                                
 @APP_ID  int,                                
 @APP_VERSION_ID int,                                
 @DRIVER_ID    smallint                                
)                                
AS                                
BEGIN                                
SELECT  adds.CUSTOMER_ID, adds.DRIVER_ID, adds.DRIVER_FNAME, DRIVER_MNAME, DRIVER_LNAME, DRIVER_CODE, DRIVER_SUFFIX, DRIVER_ADD1,                                
 DRIVER_ADD2, DRIVER_CITY, DRIVER_STATE, DRIVER_ZIP, DRIVER_COUNTRY, DRIVER_HOME_PHONE, DRIVER_BUSINESS_PHONE,                                
 DRIVER_EXT, DRIVER_MOBILE, Convert(Varchar,DRIVER_DOB,101) As DRIVER_DOB, DRIVER_SSN, DRIVER_MART_STAT, DRIVER_SEX, DRIVER_DRIV_LIC,DRIVER_LIC_STATE,                                
 DRIVER_LIC_CLASS,Convert(Varchar, DATE_EXP_START,101) As DATE_EXP_START, Convert(Varchar, DATE_LICENSED, 101) As DATE_LICENSED,                                
 DRIVER_REL, DRIVER_DRIV_TYPE, DRIVER_OCC_CODE, DRIVER_OCC_CLASS,                                
 DRIVER_DRIVERLOYER_NAME, DRIVER_DRIVERLOYER_ADD, DRIVER_INCOME, DRIVER_BROADEND_NOFAULT,ISNULL(DRIVER_PHYS_MED_IMPAIRE,0),                                
 DRIVER_DRINK_VIOLATION, DRIVER_PREF_RISK, DRIVER_GOOD_STUDENT, DRIVER_STUD_DIST_OVER_HUNDRED,                          
--ISNULL(DRIVER_LIC_SUSPENDED,0),                                
DRIVER_LIC_SUSPENDED,                          
 DRIVER_VOLUNTEER_POLICE_FIRE, DRIVER_US_CITIZEN, adds.IS_ACTIVE, adds.CREATED_BY, Convert(Varchar, adds.CREATED_DATETIME, 101) as CREATED_DATETIME, adds.MODIFIED_BY,                                
 Convert(Varchar, adds.LAST_UPDATED_DATETIME, 101) as LAST_UPDATED_DATETIME,DRIVER_FAX,RELATIONSHIP,GOOD_DRIVER_STUDENT_DISCOUNT,                                
 PREMIER_DRIVER_DISCOUNT,              
CAST(ISNULL(SAFE_DRIVER_RENEWAL_DISCOUNT,0) as int) as SAFE_DRIVER_RENEWAL_DISCOUNT,              
--SAFE_DRIVER_RENEWAL_DISCOUNT,              
VEHICLE_ID,VIOLATIONS,MVR_ORDERED,CONVERT(VARCHAR(10),DATE_ORDERED,101) AS DATE_ORDERED,PERCENT_DRIVEN, MATURE_DRIVER,MATURE_DRIVER_DISCOUNT,                                 
 PREFERRED_RISK_DISCOUNT,PREFERRED_RISK, TRANSFEREXP_RENEWAL_DISCOUNT,TRANSFEREXPERIENCE_RENEWALCREDIT,SAFE_DRIVER,                              
 ISNULL(APP_VEHICLE_PRIN_OCC_ID,0) as APP_VEHICLE_PRIN_OCC_ID, NO_DEPENDENTS,isnull(WAIVER_WORK_LOSS_BENEFITS,'') as WAIVER_WORK_LOSS_BENEFITS,                
 ISNULL(NO_CYCLE_ENDMT,'') AS NO_CYCLE_ENDMT, FORM_F95, EXT_NON_OWN_COVG_INDIVI,HAVE_CAR,STATIONED_IN_US_TERR,IN_MILITARY,            
 FULL_TIME_STUDENT,SUPPORT_DOCUMENT,SIGNED_WAIVER_BENEFITS_FORM,PARENTS_INSURANCE,CYCL_WITH_YOU,COLL_STUD_AWAY_HOME,        
(select MNT_COUNTRY_STATE_LIST.STATE_CODE from MNT_COUNTRY_STATE_LIST where MNT_COUNTRY_STATE_LIST.STATE_ID=adds.DRIVER_LIC_STATE) as STATE_CODE,    
 MVR_CLASS,MVR_LIC_CLASS,MVR_LIC_RESTR,MVR_DRIV_LIC_APPL, MVR_REMARKS, MVR_STATUS, LOSSREPORT_ORDER,
CONVERT(VARCHAR,LOSSREPORT_DATETIME,101) LOSSREPORT_DATETIME,APP_EFFECTIVE_DATE                                            
       
FROM  APP_DRIVER_DETAILS adds inner join app_list al
on   adds.CUSTOMER_ID  =  al.CUSTOMER_ID and 
adds.APP_ID = al.APP_ID and
adds.APP_VERSION_ID = al.APP_VERSION_ID                        
WHERE  adds.CUSTOMER_ID = @CUSTOMER_ID AND                                
 adds.APP_ID = @APP_ID AND                                
 adds.APP_VERSION_ID = @APP_VERSION_ID AND                                
 adds.DRIVER_ID = @DRIVER_ID                       
                                
END                              


GO

