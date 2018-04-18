    
 /*----------------------------------------------------------                      
Proc Name       : dbo.[Proc_GET_POL_BILLING_INFO]              
Created by      : SNEHA          
Date            : 16/11/2011                      
Purpose         :INSERT RECORDS IN POL_BILLING_INFO TABLE.                      
Revison History :                      
Used In        : Ebix Advantage                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------      
DROP PROC dbo.[Proc_GET_POL_BILLING_INFO]        
      
*/  
CREATE PROC dbo.[Proc_GET_POL_BILLING_INFO]    
(
@BILLING_ID INT,
@CUSTOMER_ID INT,
@POLICY_ID INT,
@POLICY_VERSION_ID INT,
@LOB_ID INT
)
AS
BEGIN
	SELECT BILLING_ID,CUSTOMER_ID,POLICY_ID,POLICY_VERSION_ID,LOB_ID,BILLING_TYPE,BILLING_PLAN,DOWN_PAYMENT_MODE,PROXY_SIGN_OBTAIN,UNDERWRITER,ROLLOVER,RECIVED_PREMIUM,COMP_APP_BONUS_APPLIES,CURRENT_RESIDENCE,IS_ACTIVE
	FROM POL_BILLING_INFO
	WHERE BILLING_ID=@BILLING_ID AND CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND LOB_ID=@LOB_ID
END