      
 /*----------------------------------------------------------            
Proc Name       : dbo.PROC_GET_RI_CONTRACT_INFO            
Created by      : Aditya Goel     
Date            : 12/12/2011          
Purpose         : To fetch record FROM POL_CUSTOMER_POLICY_LIST            
Revison History :            
Used In         : Ebix Advantage web      
        
------------------------------------------------------------            
Date     Review By          Comments            
--Drop Proc dbo.PROC_GET_RI_CONTRACT_INFO  28325,147,1       
------   ------------       -------------------------*/               
CREATE PROCEDURE [dbo].[PROC_GET_RI_CONTRACT_INFO]                                    
(    
@CUSTOMER_ID INT,    
@POLICY_ID INT,    
@POLICY_VERSION_ID INT    
)                                   
AS                                    
BEGIN                                    
SELECT DISREGARD_RI_CONTRACT from POL_CUSTOMER_POLICY_LIST WITH(NOLOCK)  
WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
END           
        
        