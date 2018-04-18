IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCustomerEmail]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCustomerEmail]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------    
Proc Name	 : dbo.Proc_GetCustomerEmail   
Created by      	: Shrikant Bhatt    
Date            	: 06/02/2005    
Purpose      	: Fetch the E-Mail address    
Revison History :    
Used In   : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC Dbo.Proc_GetCustomerEmail
(    
	@Customer_ID  int
)    
AS    
BEGIN    
SELECT CUSTOMER_Email
FROM CLT_CUSTOMER_LIST
WHERE  
CUSTOMER_ID = @Customer_ID
END


GO

