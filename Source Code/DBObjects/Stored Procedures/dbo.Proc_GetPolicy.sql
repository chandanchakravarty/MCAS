IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------      
Proc Name       : dbo.Proc_GetPolicy      
Created by      : Ashwani      
Date            : 5/4/2005      
Purpose      	: Return Policy Number  for Customer Notes
Revison History :      
Used In   	: Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC Dbo.Proc_GetPolicy      
(      
 	 @Customer_Id  int  
)      
AS      
BEGIN      
 	SELECT APP_Number    
   	FROM APP_LIST    
 	WHERE  CUSTOMER_ID = @Customer_Id  
END  



GO

