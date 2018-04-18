IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCustomerCounty]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCustomerCounty]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name          : Dbo.Proc_GetCustomerDetails  
Created by           : Gaurav  
Date                    : 01/07/2005  
Purpose               : To get County Information  from MNT_TERRITORY_CODES table  
Revison History :  
Used In                :   Wolverine  
  
------------------------------------------------------------  
  
Date     Review By          Comments  
------   ------------       -------------------------*/  
Create PROC Dbo.Proc_GetCustomerCounty  
(  
 @CustomerID  int  
)  
AS  
BEGIN  
  
SELECT MNT_TERRITORY_CODES.LOBID, MNT_TERRITORY_CODES.COUNTY, MNT_TERRITORY_CODES.TERR   
FROM CLT_CUSTOMER_LIST   
LEFT JOIN MNT_TERRITORY_CODES ON Substring(MNT_TERRITORY_CODES.ZIP,1,5) = Substring(CLT_CUSTOMER_LIST.CUSTOMER_ZIP,1,5)
WHERE CLT_CUSTOMER_LIST.CUSTOMER_ID = @CustomerID   
  
END  
  
  


GO

