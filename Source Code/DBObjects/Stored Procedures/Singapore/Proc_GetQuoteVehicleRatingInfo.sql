  
  
  
   
 /*----------------------------------------------------------      
Proc Name       : [Proc_GetQuoteVehicleRatingInfo]      
Created by      : Kuldeep Saxena     
Date            : 15/01/2012      
Purpose   : Demo      
Revison History :      
Used In        : Singapore  to fetch vehicle rating information when quote is generated without quick quote    
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------

*/     
  
 
CREATE PROC dbo.Proc_GetQuoteVehicleRatingInfo    
(    
 @CUSTOMERID int,    
 @policyID int,    
 @policyVERSIONID int     
)    
    
As    
    
    
SELECT PL.APP_ID  --QL.QQ_ID
,PL.APP_NUMBER --QL.QQ_NUMBER
,PL.RECEIVED_PRMIUM,
1 AS DEMERIT_DISCOUNT,--CL.DEMERIT_DISCOUNT,    
0 AS NAMED_DRIVER_AMT,    
0 AS UNNAMED_DRIVER_AMT,    
BASE_PREMIUM,    
PVP.DEMERIT_DISC_AMT,    
GST_AMOUNT,    
FINAL_PREMIUM    
FROM POL_VEHICLE_PREMIUM_DETAILS  PVP 
INNER JOIN POL_CUSTOMER_POLICY_LIST PL    
ON	PL.CUSTOMER_ID = PVP.CUSTOMER_ID    
AND PL.POLICY_ID = PVP.POLICY_ID
AND PL.POLICY_VERSION_ID= PVP.POLICY_VERSION_ID 
--AND PL.POLICY_VERSION_ID = QL.APP_VERSION_ID    
--INNER JOIN QQ_CUSTOMER_PARTICULAR CL    
--ON CL.CUSTOMER_ID = QL.CUSTOMER_ID    
--AND CL.QUOTE_ID = QL.QQ_ID    
--INNER JOIN QQ_MOTOR_QUOTE_DETAILS QD    
--ON QD.CUSTOMER_ID = PL.CUSTOMER_ID    
--AND QD.POLICY_ID = PL.POLICY_ID    
--AND QD.POLICY_VERSION_ID = PL.POLICY_VERSION_ID    
--INNER JOIN MNT_COUNTRY_LIST CN    
--ON CN.COUNTRY_ID = CL.NATIONALITY    
--INNER JOIN MNT_LOOKUP_VALUES LV    
--ON LV.LOOKUP_UNIQUE_ID = CL.CUSTOMER_TYPE    
WHERE PL.CUSTOMER_ID = @CUSTOMERID
AND PL.POLICY_ID = @policyID
AND PL.POLICY_VERSION_ID = @policyVERSIONID