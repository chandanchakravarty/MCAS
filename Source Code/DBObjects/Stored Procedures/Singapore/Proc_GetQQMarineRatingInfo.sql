  
  
  
   
 /*----------------------------------------------------------      
Proc Name       : [Proc_GetQQMarineRatingInfo ]      
Created by      : Kuldeep Saxena      
Date            : 22-03-2012     
Purpose   : Demo      
Revison History :      
Used In        : Singapore      
------------------------------------------------------------      
Date     Review By          Comments  
DROP PROC    dbo.Proc_GetQQMarineRatingInfo
------   ------------       -------------------------*/     
  
  
  
  
CREATE PROC dbo.Proc_GetQQMarineRatingInfo    
(    
 @CUSTOMERID int,    
 @policyID int,    
 @policyVERSIONID int     
)    
    
As    
    
    
SELECT QL.QQ_ID,QL.QQ_NUMBER,IPM.SUM_INSURED,IPM.CALCULATED_PREMIUM,MRD.MARINE_RATE FROM QQ_INVOICE_PARTICULAR_MARINE IPM INNER JOIN QQ_MARINECARGO_RISK_DETAILS MRD
ON IPM.POLICY_ID=MRD.POLICY_ID AND IPM.CUSTOMER_ID=MRD.CUSTOMER_ID AND IPM.POLICY_VERSION_ID=MRD.POLICY_VERSION_ID inner join CLT_QUICKQUOTE_LIST QL
ON IPM.CUSTOMER_ID = QL.CUSTOMER_ID    
AND IPM.POLICY_ID = QL.APP_ID    
AND IPM.POLICY_VERSION_ID = QL.APP_VERSION_ID  
WHERE IPM.CUSTOMER_ID=@CUSTOMERID AND IPM.POLICY_ID=@policyID AND IPM.POLICY_VERSION_ID=@policyVERSIONID