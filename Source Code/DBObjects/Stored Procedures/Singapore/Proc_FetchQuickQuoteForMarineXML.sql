
 /*----------------------------------------------------------      
Proc Name       : [Proc_FetchQuickQuoteForMarineXML]      
Created by      : Kuldeep Saxena    
Date            : 22-03-2012    
Purpose   : Demo      
Revison History :      
Used In        : Singapore      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/     
  
-- drop proc Proc_FetchQuickQuoteForMarineXML -100,12,1      
CREATE PROC Proc_FetchQuickQuoteForMarineXML      
(      
 @CUSTOMER_ID int,      
 @POLICY_ID int,      
 @POLICY_VERSION_ID int      
)      

AS      
      
select POLICY_LOB,POLICY_CURRENCY,APP_TERMS,APP_EFFECTIVE_DATE,      
APP_EXPIRATION_DATE,BILL_TYPE,INSTALL_PLAN_ID,CUSTOMER_TYPE,COMPANY_NAME,BUSINESS_TYPE,INVOICE_AMOUNT,MARK_UP_RATE_PERC,VOYAGE_FROM,VOYAGE_TO,VOYAGE_FROM_DATE,MARINE_RATE      
     
from POL_CUSTOMER_POLICY_LIST  PL      
inner join CLT_QUICKQUOTE_LIST QL      
on PL.CUSTOMER_ID = QL.CUSTOMER_ID      
and PL.POLICY_ID = QL.APP_ID      
and PL.POLICY_VERSION_ID = QL.APP_VERSION_ID      
inner join QQ_INVOICE_PARTICULAR_MARINE CP      
on QL.CUSTOMER_ID = CP.CUSTOMER_ID      
and QL.QQ_ID = CP.QUOTE_ID      
inner join QQ_MARINECARGO_RISK_DETAILS QD      
on PL.CUSTOMER_ID = QD.CUSTOMER_ID      
and PL.POLICY_ID = QD.POLICY_ID      
and PL.POLICY_VERSION_ID = QD.POLICY_VERSION_ID       
and  CP.QUOTE_ID = QD.QUOTE_ID   
where PL.CUSTOMER_ID = @CUSTOMER_ID and PL.POLICY_ID = @POLICY_ID and PL.POLICY_VERSION_ID = @POLICY_VERSION_ID 