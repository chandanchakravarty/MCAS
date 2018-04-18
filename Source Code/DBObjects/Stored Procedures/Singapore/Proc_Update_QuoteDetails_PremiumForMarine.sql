    
    
    
     
 /*----------------------------------------------------------        
Proc Name       : [Proc_Update_QuoteDetails_PremiumForMarine]        
Created by      : Kuldeep       
Date            : 22-03-2012       
Purpose   : Demo        
Revison History :        
Used In         : Singapore        
------------------------------------------------------------        
Date     Review By          Comments     
drop proc   Proc_Update_QuoteDetails_PremiumForMarine
------   ------------       -------------------------*/       
    
    
    
    
CREATE PROC dbo.Proc_Update_QuoteDetails_PremiumForMarine      
(      
 @CUSTOMER_ID int, 
 @POLICY_ID INT,
 @POLICY_VERSION_ID INT ,     
 @QUOTE_ID int,      
 @SUM_INSURED DECIMAL(18,2),---decimal(18,2),      
 @CALCULATED_PREMIUM DECIMAL(18,2)
)      
     
As      
      
Begin      
      
if exists(select * from QQ_INVOICE_PARTICULAR_MARINE      
where CUSTOMER_ID = @CUSTOMER_ID and POLICY_ID=@POLICY_ID AND POLICY_VERSION_ID=@POLICY_VERSION_ID) --and QUOTE_ID = @QUOTE_ID)      
Begin      
      
UPDATE QQ_INVOICE_PARTICULAR_MARINE      
set SUM_INSURED =  @SUM_INSURED,      
  
CALCULATED_PREMIUM = @CALCULATED_PREMIUM     
where CUSTOMER_ID = @CUSTOMER_ID       
and POLICY_ID = @POLICY_ID       
and POLICY_VERSION_ID = @POLICY_VERSION_ID       
--and QUOTE_ID = @QUOTE_ID      
      
      
end      
      
      
End