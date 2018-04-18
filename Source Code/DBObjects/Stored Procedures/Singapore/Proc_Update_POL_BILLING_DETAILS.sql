  
 /*----------------------------------------------------------                    
 Proc Name       : dbo.[Proc_Update_POL_BILLING_DETAILS]        
 Created by      : Kuldeep Saxena   
 Date            : May 26, 2010      
 Purpose         : Update Policy Billing Details  
 Revison History :                    
 Used In     : Ebix Advantage                    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------    
Drop Proc Proc_UpdateInstall_Plan_Data    
    
*/    
Create Proc [dbo].[Proc_Update_POL_BILLING_DETAILS]     
(    
 @CUSTOMER_ID   INT,                     -- ID OF CUSTOMER WHOSE  POLICY PREMIUM BILL INSTALLMENT WILL BE POSTED             
 @POLICY_ID  INT,                        -- POLICY IDENTIFICATION NUMBER            
 @POLICY_VERSION_ID SMALLINT,            -- POLICY VERSION IDENTIFICATION NUMBER    
 @GROSS_PREMIUM DECIMAL(25,2)=NULL,
 @OTHER_CHARGES DECIMAL(25,2)=NULL,  
 @TOTAL_BEFORE_GST DECIMAL(25,2)=NULL,
 @GST DECIMAL(25,2)=NULL,        
 @TOTAL_AFTER_GST DECIMAL(25,2)=NULL,            
 @GROSS_COMMISSION DECIMAL(25,2)=NULL,
 @GST_ON_COMMISSION DECIMAL(25,2)=NULL,
 @TOTAL_COMM_AFTER_GST DECIMAL(25,2)=NULL,
 @NET_AMOUNT DECIMAL(25,2)=NULL,
 @MODIFIED_BY INT,    
 @LAST_UPDATED_DATETIME DATETIME  
 --@TRAN_TYPE  
)AS    
BEGIN    

 UPDATE POL_BILLING_DETAILS SET          
 
 GROSS_PREMIUM=@GROSS_PREMIUM,
 OTHER_CHARGES=@OTHER_CHARGES,
 TOTAL_BEFORE_GST=@TOTAL_BEFORE_GST,
 GST= @GST,
 TOTAL_AFTER_GST=@TOTAL_AFTER_GST,
 GROSS_COMMISSION= @GROSS_COMMISSION,
 GST_ON_COMMISSION=@GST_ON_COMMISSION,
 TOTAL_COMM_AFTER_GST=@TOTAL_COMM_AFTER_GST,
 NET_AMOUNT=@NET_AMOUNT,
 MODIFIED_BY=@MODIFIED_BY,    
 LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME
   
        
 WHERE CUSTOMER_ID = @CUSTOMER_ID AND  POLICY_ID = @POLICY_ID AND  POLICY_VERSION_ID = @POLICY_VERSION_ID        
    IF(@@ERROR<>0)    
    RETURN -1    
     
     
END  