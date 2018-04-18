 IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchQuickQuoteDataForRqXml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchQuickQuoteDataForRqXml]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO


 
 /*----------------------------------------------------------    
Proc Name       : [Proc_FetchQuickQuoteDataForRqXml]    
Created by      : Agniswar Das    
Date            : 7/17/2011    
Purpose			: Demo    
Revison History :    
Used In        : Singapore    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/   






-- drop proc Proc_FetchQuickQuoteDataForRqXml -100,12,1    
CREATE PROC Proc_FetchQuickQuoteDataForRqXml    
(    
 @CUSTOMER_ID int,    
 @POLICY_ID int,    
 @POLICY_VERSION_ID int    
     
)    
    
AS    
    
select POLICY_LOB,POLICY_CURRENCY,APP_TERMS,APP_EFFECTIVE_DATE,    
APP_EXPIRATION_DATE,BILL_TYPE,INSTALL_PLAN_ID,CUSTOMER_TYPE,CUSTOMER_FIRST_NAME,    
CUSTOMER_MIDDLE_NAME,CUSTOMER_LAST_NAME,CUSTOMER_CODE,GENDER,DATE_OF_BIRTH,NATIONALITY,IS_HOME_EMPLOYEE,CUSTOMER_OCCU,    
DRIVER_EXP_YEAR,CP.ANY_CLAIM,EXIST_NCD_LESS_10,EXISTING_NCD,DEMERIT_DISCOUNT,    
YEAR_OF_REG,MAKE,MODEL,MODEL_TYPE,ENG_CAPACITY,NO_OF_DRIVERS,QD.ANY_CLAIM as 'CLAIM_RISK',NO_OF_CLAIM,TOTAL_CLAIM_AMT,    
COVERAGE_TYPE,NO_CLAIM_DISCOUNT    
from POL_CUSTOMER_POLICY_LIST  PL    
inner join CLT_QUICKQUOTE_LIST QL    
on PL.CUSTOMER_ID = QL.CUSTOMER_ID    
and PL.POLICY_ID = QL.APP_ID    
and PL.POLICY_VERSION_ID = QL.APP_VERSION_ID    
inner join QQ_CUSTOMER_PARTICULAR CP    
on QL.CUSTOMER_ID = CP.CUSTOMER_ID    
and QL.QQ_ID = CP.QUOTE_ID    
inner join QQ_MOTOR_QUOTE_DETAILS QD    
on PL.CUSTOMER_ID = QD.CUSTOMER_ID    
and PL.POLICY_ID = QD.POLICY_ID    
and PL.POLICY_VERSION_ID = QD.POLICY_VERSION_ID     
and  CP.QUOTE_ID = QD.QUOTE_ID    
where PL.CUSTOMER_ID = @CUSTOMER_ID and PL.POLICY_ID = @POLICY_ID and PL.POLICY_VERSION_ID = @POLICY_VERSION_ID    