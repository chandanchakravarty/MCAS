  
  
  
   
 /*----------------------------------------------------------      
Proc Name       : [Proc_Proc_GetInvoiceParticularInfo]      
Created by      :Kuldeep Saxena    
Date            : 19-3-2012    
Purpose   : Demo      
Revison History :      
Used In        : Singapore      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------
--exec Proc_GetInvoiceParticularInfo -100, 1075
*/     
  
  
  
  
CREATE PROC dbo.Proc_GetInvoiceParticularInfo (@CUSTOMER_ID int,@ID int)  
  
AS  
  
BEGIN  
  
 SELECT * FROM QQ_INVOICE_PARTICULAR_MARINE 
 WHERE CUSTOMER_ID = @CUSTOMER_ID and QUOTE_ID = @ID  
END