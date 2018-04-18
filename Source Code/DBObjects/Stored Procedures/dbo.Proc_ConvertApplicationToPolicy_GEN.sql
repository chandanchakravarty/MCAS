IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ConvertApplicationToPolicy_GEN]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ConvertApplicationToPolicy_GEN]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/* =======================================================================                                                           
 Proc Name       : dbo.Proc_ConvertApplicationToPolicy_GEN            
 Created by      : Ravindra            
 Date            : 03-31-2006            
 Purpose         : Copy the General Liability Application Data to Policy Tables.                                                                              
                                                          
Modify By:    Pravesh Chandel      
Modify Date : 23 Oct 2006      
Purpose     : call a comman Proc for Comman task while converting App To Pol

==========================================================================                                                                        
Date     Review By          Comments                    

========================================================================== */                                                                            
--drop proc dbo.Proc_ConvertApplicationToPolicy_GEN            
            
CREATE PROC dbo.Proc_ConvertApplicationToPolicy_GEN             
@CUSTOMER_ID int,                                                                              
@APP_ID int,                                                                              
@APP_VERSION_ID smallint,                                                                              
@CREATED_BY int,                                                                              
@PARAM1 int = NULL,                                                      
@PARAM2 int = NULL,                                                  
@PARAM3 int = NULL,                   
@CALLED_FROM NVARCHAR(30),                                                   
@RESULT int output                                                                     
as                                                                              
begin                            

---ADDED BY PRAVESH    
BEGIN TRAN                                                       
    
DECLARE @TEMP_ERROR_CODE INT                                                                                      
EXEC     @RESULT=Proc_ConvertApplicationToPolicy_ALL  @CUSTOMER_ID,@APP_ID,@APP_VERSION_ID,@CREATED_BY,@PARAM1,@PARAM2,@PARAM3,@CALLED_FROM,@RESULT                                                                        
    
IF (@RESULT = -1)  GOTO PROBLEM    
                                                                                    
DECLARE @TEMP_POLICY_ID INT                                                                                       
DECLARE @TEMP_POLICY_VERSION_ID INT    
SET @TEMP_POLICY_ID=@RESULT    
SELECT   @TEMP_POLICY_VERSION_ID = POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID=@CUSTOMER_ID AND APP_ID=@APP_ID AND APP_VERSION_ID=@APP_VERSION_ID     
--END HERE    
/*                                        

 IF EXISTS (SELECT POLICY_ID FROM POL_CUSTOMER_POLICY_LIST                           
           WHERE CUSTOMER_ID = @CUSTOMER_ID  AND APP_ID = @APP_ID)                          
                          
BEGIN                          
 SET @RESULT = -1                    
 RETURN @RESULT                         
END                          
ELSE                          
BEGIN                                                                          
begin tran                                                                              
declare @TEMP_ERROR_CODE int                                                                             
declare @TEMP_POLICY_ID int                                                                               
declare @TEMP_POLICY_VERSION_ID int                                                                          
                                                                                  
select @TEMP_POLICY_ID = MAX(ISNULL(POLICY_ID,0))+1  from POL_CUSTOMER_POLICY_LIST                                       
where CUSTOMER_ID = @CUSTOMER_ID                                                                                     
if @TEMP_POLICY_ID IS NULL OR @TEMP_POLICY_ID = ''                                                                              
 begin                                                                              
  set @TEMP_POLICY_ID = 1                                                         
 end                                                                             
set @TEMP_POLICY_VERSION_ID = 1                                                                        
            
            
--1. Insert data in POL_CUSTOMER_POLICY_LIST                                               
                                                                     
if @CALLED_FROM = 'ANYWAY'                      
insert into POL_CUSTOMER_POLICY_LIST                                      
(                                                                              
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,APP_ID,APP_VERSION_ID,PARENT_APP_VERSION_ID,APP_STATUS,APP_NUMBER,APP_VERSION,       APP_TERMS,                                                      
 APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,POLICY_LOB,POLICY_SUBLOB,CSR, UNDERWRITER,IS_UNDER_REVIEW,       AGENCY_ID,       IS_ACTIVE,       CREATED_BY,                                                                              
 CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,COUNTRY_ID,STATE_ID,DIV_ID,DEPT_ID,PC_ID,BILL_TYPE,BILL_TYPE_ID,COMPLETE_APP,       PROPRTY_INSP_CREDIT,       INSTALL_PLAN_ID,                                                                            
  
   
 CHARGE_OFF_PRMIUM,RECEIVED_PRMIUM,PROXY_SIGN_OBTAINED,POLICY_TYPE,SHOW_QUOTE,APP_VERifICATION_XML,YEAR_AT_CURR_RESI,       YEARS_AT_PREV_ADD,       POLICY_STATUS,       POLICY_NUMBER,                                                                       
  
   
  
    
     
      
       
 POLICY_DISP_VERSION,IS_HOME_EMP                                                                              
 )                                                                                    
select                                                                 
 @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,@APP_ID,@APP_VERSION_ID,PARENT_APP_VERSION_ID,APP_STATUS,                                                                        
 APP_NUMBER,APP_VERSION,APP_TERMS,APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,APP_LOB,APP_SUBLOB,                                                                              
 CSR,UNDERWRITER,IS_UNDER_REVIEW,APP_AGENCY_ID,IS_ACTIVE,@CREATED_BY,GETDATE(),MODifIED_BY,LasT_UPDATED_DATETIME,                                                                         
 COUNTRY_ID,STATE_ID,DIV_ID,DEPT_ID,PC_ID,BILL_TYPE,BILL_TYPE_ID,COMPLETE_APP,PROPRTY_INSP_CREDIT,INSTALL_PLAN_ID,                         
 CHARGE_OFF_PRMIUM,RECEIVED_PRMIUM,PROXY_SIGN_OBTAINED, POLICY_TYPE,SHOW_QUOTE,                                                             
 APP_VERifICATION_XML,YEAR_AT_CURR_RESI,YEARS_AT_PREV_ADD,'SUSPENDED',SUBSTRING(APP_NUMBER,1,(LEN(APP_NUMBER)-3)),                          
 CONVERT(VARCHAR,@TEMP_POLICY_VERSION_ID) + '.0' ,IS_HOME_EMP                                                                              
 from APP_LIST                       
 where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND IS_ACTIVE = 'Y'                                               
                  
                                              
ELSE                                             
                                              
insert into POL_CUSTOMER_POLICY_LIST                                                                               
(                                                                              
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,APP_ID,APP_VERSION_ID,PARENT_APP_VERSION_ID,APP_STATUS,APP_NUMBER,APP_VERSION,       APP_TERMS,                                                                              
 APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,POLICY_LOB,POLICY_SUBLOB,CSR, UNDERWRITER,IS_UNDER_REVIEW,       AGENCY_ID,       IS_ACTIVE,       CREATED_BY,                                                                              
 CREATED_DATETIME,MODifIED_BY,LasT_UPDATED_DATETIME,COUNTRY_ID,STATE_ID,DIV_ID,DEPT_ID,PC_ID,BILL_TYPE,COMPLETE_APP,       PROPRTY_INSP_CREDIT,       INSTALL_PLAN_ID,                                                                    
 CHARGE_OFF_PRMIUM,RECEIVED_PRMIUM,PROXY_SIGN_OBTAINED,POLICY_TYPE,SHOW_QUOTE,APP_VERifICATION_XML,YEAR_AT_CURR_RESI,       YEARS_AT_PREV_ADD,       POLICY_STATUS,       POLICY_NUMBER,                                                                       
  
  
  
    
    
      
       
POLICY_DISP_VERSION,IS_HOME_EMP                                                                              
 )                                                    
select                                                                 
@CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,@APP_ID,@APP_VERSION_ID,PARENT_APP_VERSION_ID,APP_STATUS,                            
APP_NUMBER,APP_VERSION,APP_TERMS,APP_INCEPTION_DATE,APP_EFFECTIVE_DATE,APP_EXPIRATION_DATE,APP_LOB,APP_SUBLOB,             
                                           
CSR,UNDERWRITER,IS_UNDER_REVIEW,APP_AGENCY_ID,IS_ACTIVE,@CREATED_BY,GETDATE(),MODifIED_BY,LasT_UPDATED_DATETIME,                                                 
COUNTRY_ID,STATE_ID,DIV_ID,DEPT_ID,PC_ID,BILL_TYPE,COMPLETE_APP,PROPRTY_INSP_CREDIT,INSTALL_PLAN_ID,                                                                    
CHARGE_OFF_PRMIUM,RECEIVED_PRMIUM,PROXY_SIGN_OBTAINED, POLICY_TYPE,SHOW_QUOTE,                                                                              
APP_VERifICATION_XML,YEAR_AT_CURR_RESI,YEARS_AT_PREV_ADD,'SUSPENDED',SUBSTRING(APP_NUMBER,1,(LEN(APP_NUMBER)-3)),                                                                              
CONVERT(VARCHAR,@TEMP_POLICY_VERSION_ID) + '.0',IS_HOME_EMP                                                                               
 from APP_LIST                                                
 where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID AND IS_ACTIVE = 'Y'                                                                                  
                                                                              
                  
                  
 select @TEMP_ERROR_CODE = @@ERROR                                                                              
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                     
                               
            
-- 2.To insert data in  POL_APPLICANT_LIST                                       
                  
 IF EXISTS (                  
  SELECT POLICY_ID FROM POL_CUSTOMER_POLICY_LIST                   
  WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @TEMP_POLICY_ID                                
  AND POLICY_VERSION_ID = @TEMP_POLICY_VERSION_ID                  
                  
  )                                
BEGIN                                                      
            
insert into POL_APPLICANT_LIST                                               
(                                                                          
 POLICY_ID,POLICY_VERSION_ID,CUSTOMER_ID, APPLICANT_ID,CREATED_BY,CREATED_DATETIME,                                                                          
 IS_PRIMARY_APPLICANT                                                                          
)                                                          
SELECT                                                                               
 @TEMP_POLICY_ID,                                                                              
 @TEMP_POLICY_VERSION_ID,                                       
 @CUSTOMER_ID,                                                                              
 A.APPLICANT_ID,                                                                              
 @CREATED_BY,                                                                              
 GETDATE(),                                                                 
 A.IS_PRIMARY_APPLICANT                                                                              
 FROM APP_APPLICANT_LIST A                                                          
 ,CLT_APPLICANT_LIST  B,                                                    
 APP_LIST C                                                               
 WHERE A.CUSTOMER_ID=@CUSTOMER_ID AND A.APP_ID=@APP_ID AND A.APP_VERSION_ID=@APP_VERSION_ID                                                                              
            
  AND A.APPLICANT_ID = B.APPLICANT_ID and B.IS_ACTIVE='Y'                                                       
  AND C.CUSTOMER_ID = @CUSTOMER_ID AND C.APP_ID = @APP_ID AND C.APP_VERSION_ID = @APP_VERSION_ID                               
  AND C.IS_ACTIVE = 'Y'                                                                          
*/                                                                             
 select @TEMP_ERROR_CODE = @@ERROR                                                                              
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM               
                                                          
         
---- Copy LOB Specific Data                                      
--1.      
            
insert into POL_LOCATIONS                                                          
(                                                                     
 CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID, LOCATION_ID, LOC_NUM,                                                                    
 IS_PRIMARY, LOC_ADD1, LOC_ADD2, LOC_CITY, LOC_COUNTY,                                                                    
 LOC_STATE,LOC_ZIP,LOC_COUNTRY,PHONE_NUMBER,FAX_NUMBER,                                                                    
 DEDUCTIBLE,NAMED_PERILL,DESCRIPTION,IS_ACTIVE,CREATED_BY,                                             
 CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME,LOC_TERRITORY                                                                        
)                                                                         
                                                                          
select                                                                           
 @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,LOCATION_ID,LOC_NUM,                                                                              
 IS_PRIMARY,LOC_ADD1,LOC_ADD2,LOC_CITY,LOC_COUNTY,                                                                    
 LOC_STATE,LOC_ZIP, LOC_COUNTRY, PHONE_NUMBER,FAX_NUMBER,                                                                    
 DEDUCTIBLE,NAMED_PERILL,DESCRIPTION,IS_ACTIVE,@CREATED_BY,                                                                    
 GETDATE(),null,null,LOC_TERRITORY                            
 from APP_LOCATIONS                                                                              
 where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID  and IS_Active='Y'                                                                  
                                                                          
 select @TEMP_ERROR_CODE = @@ERROR                                                                          
     if (@TEMP_ERROR_CODE <> 0) goto PROBLEM              
       
--- 2.      
insert into POL_GENERAL_LIABILITY_DETAILS       
(      
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,POLICY_GEN_ID,LOCATION_ID,CLASS_CODE,BUSINESS_DESCRIPTION,      
 COVERAGE_TYPE,COVERAGE_FORM,EXPOSURE_BASE,EXPOSURE,RATE,IS_ACTIVE,CREATED_BY,CREATED_DATETIME,      
 MODIFIED_BY,LAST_UPDATED_DATETIME      
)      
select             
 @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,APP_GEN_ID,LOCATION_ID,CLASS_CODE,BUSINESS_DESCRIPTION,      
 COVERAGE_TYPE,COVERAGE_FORM,EXPOSURE_BASE,EXPOSURE,RATE,IS_ACTIVE,@CREATED_BY,GETDATE(),      
 null,null      
 from APP_GENERAL_LIABILITY_DETAILS          
 where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                            
 and IS_Active='Y'    AND  
  LOCATION_ID IN (Select LOCATION_ID from POL_LOCATIONS where IS_Active='Y')                                                                
 select @TEMP_ERROR_CODE = @@ERROR                                                                              
     if (@TEMP_ERROR_CODE <> 0) goto PROBLEM         
      
      
--- 3.      
insert into POL_GENERAL_COVERAGE_LIMITS      
(      
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,COVERAGE_L_AMOUNT,COVERAGE_L_ID,COVERAGE_L_AGGREGATE,COVERAGE_O_AMOUNT,      
 COVERAGE_O_ID,COVERAGE_O_AGGREGATE,COVERAGE_M_EACH_PERSON_AMOUNT,COVERAGE_M_EACH_PERSON_ID,      
 COVERAGE_M_EACH_OCC_AMOUNT,COVERAGE_M_EACH_OCC_ID,TOTAL_GENERAL_AGGREGATE,IS_ACTIVE,      
 CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME      
)        
select             
 @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,COVERAGE_L_AMOUNT,COVERAGE_L_ID,COVERAGE_L_AGGREGATE,COVERAGE_O_AMOUNT,      
 COVERAGE_O_ID,COVERAGE_O_AGGREGATE,COVERAGE_M_EACH_PERSON_AMOUNT,COVERAGE_M_EACH_PERSON_ID,      
 COVERAGE_M_EACH_OCC_AMOUNT,COVERAGE_M_EACH_OCC_ID,TOTAL_GENERAL_AGGREGATE,IS_ACTIVE,      
 @CREATED_BY,GETDATE(),null,null      
 from APP_GENERAL_COVERAGE_LIMITS       
 where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                            
 and IS_Active='Y'                       
 select @TEMP_ERROR_CODE = @@ERROR                                                                              
     if (@TEMP_ERROR_CODE <> 0) goto PROBLEM       
      
---- 4. APP_GENERAL_HOLDER_INTEREST                        
      
insert into POL_GENERAL_HOLDER_INTEREST      
(      
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,ADD_INT_ID,HOLDER_ID,DWELLING_ID,MEMO,NATURE_OF_INTEREST,RANK,      
 LOAN_REF_NUMBER,HOLDER_NAME,HOLDER_ADD1,HOLDER_ADD2,HOLDER_CITY,HOLDER_COUNTRY,HOLDER_STATE,HOLDER_ZIP,      
 IS_ACTIVE,CREATED_BY,CREATED_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME      
)      
select             
 @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,ADD_INT_ID,HOLDER_ID,DWELLING_ID,MEMO,NATURE_OF_INTEREST,RANK,      
 LOAN_REF_NUMBER,HOLDER_NAME,HOLDER_ADD1,HOLDER_ADD2,HOLDER_CITY,HOLDER_COUNTRY,HOLDER_STATE,HOLDER_ZIP,      
 IS_ACTIVE,@CREATED_BY,GETDATE(),null,null      
 from APP_GENERAL_HOLDER_INTEREST      
 where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                            
 and IS_Active='Y'                           
 select @TEMP_ERROR_CODE = @@ERROR                                                                              
     if (@TEMP_ERROR_CODE <> 0) goto PROBLEM       
      
--- 5.      
insert into POL_GENERAL_UNDERWRITING_INFO      
(      
 CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,INSURANCE_DECLINED_FIVE_YEARS,MEDICAL_PROFESSIONAL_EMPLOYEED,      
 EXPOSURE_RATIOACTIVE_NUCLEAR,HAVE_PAST_PRESENT_OPERATIONS,ANY_OPERATIONS_SOLD,MACHINERY_LOANED,ANY_WATERCRAFT_LEASED,      
 ANY_PARKING_OWNED,FEE_CHARGED_PARKING,RECREATION_PROVIDED,SWIMMING_POOL_PREMISES,SPORTING_EVENT_SPONSORED,      
 STRUCTURAL_ALTERATION_CONTEMPATED,DEMOLITION_EXPOSURE_CONTEMPLATED,CUSTOMER_ACTIVE_JOINT_VENTURES,LEASE_EMPLOYEE,      
 LABOR_INTERCHANGE_OTH_BUSINESS,DAY_CARE_FACILITIES,ADDITIONAL_COMMENTS,DESC_INSURANCE_DECLINED,DESC_MEDICAL_PROFESSIONAL,      
 DESC_EXPOSURE_RATIOACTIVE,DESC_HAVE_PAST_PRESENT,DESC_ANY_OPERATIONS,DESC_MACHINERY_LOANED,DESC_ANY_WATERCRAFT,      
 DESC_ANY_PARKING,DESC_FEE_CHARGED,DESC_RECREATION_PROVIDED,DESC_SWIMMING_POOL,DESC_SPORTING_EVENT,DESC_STRUCTURAL_ALTERATION,      
 DESC_DEMOLITION_EXPOSURE,DESC_CUSTOMER_ACTIVE,DESC_LEASE_EMPLOYEE,DESC_LABOR_INTERCHANGE,DESC_DAY_CARE,IS_ACTIVE,      
 CREATED_BY,CREATE_DATETIME,MODIFIED_BY,LAST_UPDATED_DATETIME       
)      
select             
 @CUSTOMER_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID,INSURANCE_DECLINED_FIVE_YEARS,MEDICAL_PROFESSIONAL_EMPLOYEED,      
 EXPOSURE_RATIOACTIVE_NUCLEAR,HAVE_PAST_PRESENT_OPERATIONS,ANY_OPERATIONS_SOLD,MACHINERY_LOANED,ANY_WATERCRAFT_LEASED,      
 ANY_PARKING_OWNED,FEE_CHARGED_PARKING,RECREATION_PROVIDED,SWIMMING_POOL_PREMISES,SPORTING_EVENT_SPONSORED,      
 STRUCTURAL_ALTERATION_CONTEMPATED,DEMOLITION_EXPOSURE_CONTEMPLATED,CUSTOMER_ACTIVE_JOINT_VENTURES,LEASE_EMPLOYEE,      
 LABOR_INTERCHANGE_OTH_BUSINESS,DAY_CARE_FACILITIES,ADDITIONAL_COMMENTS,DESC_INSURANCE_DECLINED,DESC_MEDICAL_PROFESSIONAL,      
 DESC_EXPOSURE_RATIOACTIVE,DESC_HAVE_PAST_PRESENT,DESC_ANY_OPERATIONS,DESC_MACHINERY_LOANED,DESC_ANY_WATERCRAFT,      
 DESC_ANY_PARKING,DESC_FEE_CHARGED,DESC_RECREATION_PROVIDED,DESC_SWIMMING_POOL,DESC_SPORTING_EVENT,DESC_STRUCTURAL_ALTERATION,      
 DESC_DEMOLITION_EXPOSURE,DESC_CUSTOMER_ACTIVE,DESC_LEASE_EMPLOYEE,DESC_LABOR_INTERCHANGE,DESC_DAY_CARE,IS_ACTIVE,      
 @CREATED_BY,GETDATE(),null,null      
 from APP_GENERAL_UNDERWRITING_INFO        
 where CUSTOMER_ID = @CUSTOMER_ID AND APP_ID = @APP_ID AND APP_VERSION_ID = @APP_VERSION_ID                                                            
-- and IS_Active='Y'                           
 select @TEMP_ERROR_CODE = @@ERROR                                                
     if (@TEMP_ERROR_CODE <> 0) goto PROBLEM       
    
      
      
/*Commented by pravesh      
            
-- Insert Diary entry            
            
declare @UNDERWRITER int             
            
select @UNDERWRITER=isnull(UNDERWRITER,0)                                                                        
from POL_CUSTOMER_POLICY_LIST                                                                        
where CUSTOMER_ID=@CUSTOMER_ID AND  POLICY_ID=@TEMP_POLICY_ID AND   POLICY_VERSION_ID=@TEMP_POLICY_VERSION_ID                                                                                                                                    
INSERT into TODOLIST                                                                          
 (                                                                          
  RECBYSYSTEM,RECDATE,FOLLOWUPDATE,LISTTYPEID,POLICYCLIENTID,POLICYID,            
  POLICYVERSION,POLICYCARRIERID,POLICYBROKERID,SUBJECTLINE,LISTOPEN,            
  SYSTEMFOLLOWUPID,PRIORITY,TOUSERID,FROMUSERID,STARTTIME,ENDTIME,NOTE,            
  PROPOSALVERSION,QUOTEID,CLAIMID,CLAIMMOVEMENTID,TOENTITYID,FROMENTITYID,            
  CUSTOMER_ID,APP_ID,APP_VERSION_ID,POLICY_ID,POLICY_VERSION_ID                                                                          
 )                                                                           
values                                                
 (                                                                          
  null,getdate(),getdate(),7,@CUSTOMER_ID,@TEMP_POLICY_ID,                                                                       
  @TEMP_POLICY_VERSION_ID,null, null,'New Application Submitted','Y',                                                                        
  null,'M',@CREATED_BY,@CREATED_BY,null,null,null,                                                              
  null,null,null,null,null,null,                                                                          
  @CUSTOMER_ID, @APP_ID,@APP_VERSION_ID,@TEMP_POLICY_ID,@TEMP_POLICY_VERSION_ID                                                                          
 )                         
                               
                                                                    
 select @TEMP_ERROR_CODE = @@ERROR                                                                                  
    if (@TEMP_ERROR_CODE <> 0) goto PROBLEM                                                                         
 */                          
            
            
                                                                             
 commit tran                                                                              
 set @RESULT = @TEMP_POLICY_ID                                
                              
--Update status                       
  return @RESULT                                   
                                
--END                                                                             
                                                                              
PROBLEM:                      
                  
if (@TEMP_ERROR_CODE <> 0)                                                     
 begin                            
   rollback tran                    
    set @RESULT = -1                                                                              
    return @RESULT      
  end     
else                                                                              
  begin                     
    set @RESULT = @TEMP_POLICY_ID                                                                              
    return @RESULT                           
 end                                       
--end                                 
END                                      
                             
    
  
  
  
  
  






GO

