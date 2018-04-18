/*---------------------------------------------------------------  
Proc Name          : dbo.[DELETE_POL_BILLING_INFO] 
Created by      : SNEHA          
Date            : 16/11/2011                      
--------------------------------------------------------  
--Date     Review By          Comments          
------   ------------       -------------------------*/         
-- drop proc dbo.[Proc_ACTIVE_DEACTIVE_POL_BILLING_INFO]  
------   ------------       -------------------------*/         
-- drop proc dbo.[DELETE_POL_BILLING_INFO]  8,1,1,41 
     
CREATE  PROCEDURE [dbo].[DELETE_POL_BILLING_INFO]          
(           
 
  @CUSTOMER_ID INT,
  @POLICY_ID INT,
  @POLICY_VERSION_ID INT,
  @LOB_ID INT
  
)            
AS           
BEGIN          
DELETE FROM POL_BILLING_INFO  WHERE  CUSTOMER_ID=@CUSTOMER_ID AND POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID AND LOB_ID=@LOB_ID
End