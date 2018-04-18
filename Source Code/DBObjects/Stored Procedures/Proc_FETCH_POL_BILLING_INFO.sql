    
 /*----------------------------------------------------------                      
Proc Name       : dbo.[Proc_Insert_POL_BILLING_INFO]              
Created by      : SNEHA          
Date            : 16/11/2011                      
Purpose         :INSERT RECORDS IN POL_BILLING_INFO TABLE.                      
Revison History :                      
Used In        : Ebix Advantage                      
------------------------------------------------------------                      
Date     Review By          Comments                      
------   ------------       -------------------------      
DROP PROC dbo.[Proc_FETCH_POL_BILLING_INFO]  8,1,1,41      
      
*/  

CREATE PROC dbo.[Proc_FETCH_POL_BILLING_INFO]    
(
@CUSTOMER_ID INT,
@POLICY_ID INT,
@POLICY_VERSION_ID INT,
@LOB_ID INT
)
AS
BEGIN  
declare @BILLING_ID int
	IF NOT EXISTS(SELECT BILLING_ID	FROM POL_BILLING_INFO WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND LOB_ID=@LOB_ID)
	BEGIN
	SELECT  -1 as Billing_ID
	END
 ELSE
 BEGIN

	SELECT @BILLING_ID= ISNULL(BILLING_ID,0) 	FROM POL_BILLING_INFO	WHERE CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND LOB_ID=@LOB_ID 
	SELECT @BILLING_ID as Billing_ID
	
 END
END

