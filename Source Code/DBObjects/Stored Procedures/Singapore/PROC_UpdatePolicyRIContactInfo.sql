  
  
/*  
==============================================================  
Proc Name       : [DBO].[PROC_UpdatePolicyRIContactInfo]  
Created by      : LALIT CHAUHAN                                  
Date            : 05/20/2010                                              
Purpose         : INSERT FIRST TIME PREMIUM INSTALLMENT DETALS.                                              
Revison History :                                              
Used In        : Ebix Advantage                                              
================================================================                                          
Date     Review By          Comments                                              
================================================================  
*/  
CREATE PROC [DBO].[PROC_UpdatePolicyRIContactInfo]  
(  
@CUSTOMER_ID INT ,  
@POLICY_ID INT ,  
@POLICY_VERSION_ID INT ,  
@DISREGARD_RI_CONTRACT INT  
)  
AS  
BEGIN   
UPDATE POL_CUSTOMER_POLICY_LIST SET DISREGARD_RI_CONTRACT = @DISREGARD_RI_CONTRACT  
WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND   
POLICY_VERSION_ID = @POLICY_VERSION_ID  
  
END