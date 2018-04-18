IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCustomerFax]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCustomerFax]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 /*----------------------------------------------------------      
Proc Name  : dbo.Proc_GetCustomerEmail     
Created by       : Ashish     
Date             : 08/19/2005     
Purpose       : Fetch the E-Mail address      
Revison History :      
Used In   : Wolverine      
------------------------------------------------------
Date     Review By          Comments      
------   ------------       -------------------------*/      
--drop proc dbo.Proc_GetCustomerFax 
CREATE PROC dbo.Proc_GetCustomerFax  
(      
 @Customer_ID  int  
)      
AS      
BEGIN      
SELECT ISNULL(CUSTOMER_FAX,'') AS CUSTOMER_FAX , ISNULL(CUSTOMER_FIRST_NAME,'') + ' ' +  isnull(CUSTOMER_LAST_NAME,'') AS CUSTOMER_NAME
FROM CLT_CUSTOMER_LIST  
WHERE    
CUSTOMER_ID = @Customer_ID  
END  
  
  
  
  
  


GO

