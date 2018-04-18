  
  
  
   
 /*----------------------------------------------------------      
Proc Name       : [Proc_Update_QuoteDetails_Premium_NONQQ]      
Created by      :	KULDEEP SAXENA     
Date            : 7/17/2011      
Purpose   : Demo      
Revison History :  TO UPDATE POL_VEHICLED WHEN QUOTE GENERATED WITHOUT QQ    
Used In         : Singapore      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/     
  
  
  
  
CREATE PROC dbo.Proc_Update_QuoteDetails_Premium_NONQQ    
(    
 @CUSTOMER_ID int,    
 @POLICY_ID int,    
 @POLICY_VERSION_ID int,    
 @VEHICLE_ID int,    
 @BASE_PREMIUM varchar(15),---decimal(18,2),    
 @DEMERIT_DISC_AMT varchar(15),    
 @GST_AMOUNT varchar(15),    
 @FINAL_PREMIUM varchar(15)
     
)    
   
As    
    
Begin    
    
if exists(select * from POL_VEHICLE_PREMIUM_DETAILS
where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID = @POLICY_ID     
and POLICY_VERSION_ID = @POLICY_VERSION_ID AND VEHICLE_ID=@VEHICLE_ID)   
Begin    
UPDATE POL_VEHICLE_PREMIUM_DETAILS    
set BASE_PREMIUM =  @BASE_PREMIUM,    
DEMERIT_DISC_AMT = @DEMERIT_DISC_AMT,    
GST_AMOUNT = @GST_AMOUNT,    
FINAL_PREMIUM = @FINAL_PREMIUM    
where CUSTOMER_ID = @CUSTOMER_ID     
and POLICY_ID = @POLICY_ID     
and POLICY_VERSION_ID = @POLICY_VERSION_ID     
--and QUOTE_ID = @QUOTE_ID  
END  
 ELSE
 BEGIN
 INSERT INTO POL_VEHICLE_PREMIUM_DETAILS VALUES(@CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID,@VEHICLE_ID,@BASE_PREMIUM,@DEMERIT_DISC_AMT,@GST_AMOUNT,    
 @FINAL_PREMIUM)  
    
END 
    
    
End

