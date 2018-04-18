IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCustomer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------      
Proc Name    : dbo.Proc_UpdateCustomer      
Created by   : Ashwani      
Date         : 23 Feb.,2005     
Purpose     : Update the record into Customers Table    
Revison History :  
Used In  :   Wolverine         
 ------------------------------------------------------------                  
Date     Review By          Comments                
       
------   ------------       -------------------------*/   
  
CREATE PROC dbo.Proc_UpdateCustomer  
(  
@CustomerID     NCHAR(5),  
@CompanyName NVARCHAR(40),  
@ContactName NVARCHAR(30),  
@ContactTitle NVARCHAR(30),  
@Address NVARCHAR(60),  
@City  NVARCHAR(15),  
@Region  NVARCHAR(15),  
@PostalCode NVARCHAR(10),  
@Country NVARCHAR(15),  
@Phone  NVARCHAR(24),  
@Fax  NVARCHAR(24),  
@Gender  CHAR(1),  
@PaymentMode CHAR(1),  
@Isactive NUMERIC(18)  
)  
AS   
BEGIN   
 UPDATE Customers  
 SET   
  CompanyName=@CompanyName,  
  ContactName=@ContactName,  
  ContactTitle=@ContactTitle,  
  Address=@Address,  
  City=@City,  
  Region=@Region,  
  PostalCode=@PostalCode,  
  Country=@Country,  
  Phone=@Phone,  
  Fax=@Fax,  
  Gender=@Gender,  
  PaymentMode=@PaymentMode,  
  Isactive=@Isactive  
 WHERE	 CustomerID=@CustomerID  
  
END  



GO

