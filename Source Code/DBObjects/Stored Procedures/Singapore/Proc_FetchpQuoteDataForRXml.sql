    
    
    
     
 /*----------------------------------------------------------        
Proc Name       : [Proc_FetchQuickQuoteDataForRqXml]        
Created by      : Kuldeep Saxena       
Date            : 10/Jan/2012        
Purpose   : To generate preminum for singapore if application is going to be submitted without quick quote        
Revison History :        
Used In        : Singapore        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/       
  
    
-- drop proc Proc_FetchpQuoteDataForRXml  28775,380,1   
CREATE PROC Proc_FetchpQuoteDataForRXml        
(        
 @CUSTOMER_ID int,        
 @POLICY_ID int,        
 @POLICY_VERSION_ID int        
         
)        
        
AS        
        
select   
POLICY_LOB,POLICY_CURRENCY,APP_TERMS,APP_EFFECTIVE_DATE,        
APP_EXPIRATION_DATE,BILL_TYPE,INSTALL_PLAN_ID,CUSTOMER_TYPE,CUSTOMER_FIRST_NAME,        
CUSTOMER_MIDDLE_NAME,CUSTOMER_LAST_NAME,CUSTOMER_CODE,GENDER,DATE_OF_BIRTH,NATIONALITY,IS_HOME_EMPLOYEE,CL.APPLICANT_OCCU as 'CUSTOMER_OCCU',        
4 AS DRIVER_EXP_YEAR,  
0 AS ANY_CLAIM,  
0 AS EXIST_NCD_LESS_10,  
'50%' AS EXISTING_NCD,  
30 AS DEMERIT_DISCOUNT,        
PV.VEHICLE_YEAR as 'YEAR_OF_REG',MAKE,MODEL,pv.VEHICLE_TYPE as 'MODEL_TYPE',PV.VEHICLE_CC as 'ENG_CAPACITY',PV.TOTAL_DRIVERS as 'NO_OF_DRIVERS',  
1 as 'CLAIM_RISK',--QD.ANY_CLAIM   
0 AS NO_OF_CLAIM,  
0 AS TOTAL_CLAIM_AMT,        
pv.VEHICLE_COVERAGE AS COVERAGE_TYPE,  
1 AS NO_CLAIM_DISCOUNT        
from   
POL_CUSTOMER_POLICY_LIST  PL        
--inner join   
--CLT_QUICKQUOTE_LIST QL        
--on PL.CUSTOMER_ID = QL.CUSTOMER_ID        
--and PL.POLICY_ID = QL.APP_ID        
--and PL.POLICY_VERSION_ID = QL.APP_VERSION_ID        
left join   
CLT_CUSTOMER_LIST CL        
on PL.CUSTOMER_ID = CL.CUSTOMER_ID        
  
left join   
POL_VEHICLES PV        
on PL.CUSTOMER_ID = PV.CUSTOMER_ID        
and PL.POLICY_ID = PV.POLICY_ID        
and PL.POLICY_VERSION_ID = PV.POLICY_VERSION_ID         
--and  CP.QUOTE_ID = QD.QUOTE_ID        
where PL.CUSTOMER_ID = @CUSTOMER_ID and PL.POLICY_ID = @POLICY_ID and PL.POLICY_VERSION_ID =@POLICY_VERSION_ID 
  
