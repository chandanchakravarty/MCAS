    
 /*----------------------------------------------------------              
Proc Name       : dbo.Proc_UpdateACT_POLICY_REIN_INSTALLMENT_DETAILS             
Created by      : Aditya Goel              
Date            : 04/11/2011              
Purpose         : To update record in ACT_POLICY_REIN_INSTALLMENT_DETAILS              
Revison History :              
Used In         :  Ebix Advantage web          
------------------------------------------------------------              
Date     Review By          Comments              
DROP PROC Dbo.Proc_UpdateACT_POLICY_REIN_INSTALLMENT_DETAILS   46,28070,902,1        
------   ------------       -------------------------*/              
              
CREATE  PROC [dbo].[Proc_UpdateACT_POLICY_REIN_INSTALLMENT_DETAILS]              
(              
@REIN_INSTALLMENT_NO   varchar(100), 
@CUSTOMER_ID INT,
@POLICY_ID INT,
@POLICY_VERSION_ID INT,
@IDEN_ROW_ID INT         
)             
AS              
              
BEGIN              
              
  UPDATE ACT_POLICY_REIN_INSTALLMENT_DETAILS SET          
                
   REIN_INSTALLMENT_NO   = @REIN_INSTALLMENT_NO   
   WHERE CUSTOMER_ID = @CUSTOMER_ID AND POLICY_ID = @POLICY_ID
   AND POLICY_VERSION_ID = @POLICY_VERSION_ID  
   AND IDEN_ROW_ID = @IDEN_ROW_ID  
   
END       
            
          