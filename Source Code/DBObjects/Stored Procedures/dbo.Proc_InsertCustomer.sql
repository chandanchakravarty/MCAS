IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertCustomer]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertCustomer]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------    
Proc Name   	: dbo.Proc_InsertCustomer    
Created by  	: Ashwani    
Date        	: 23 Feb.,2005   
Purpose    	: Insert the record into Customers Table  
Revison History :
Used In 	:   Wolverine       
 ------------------------------------------------------------                
Date     Review By          Comments              
     
------   ------------       -------------------------*/   

CREATE PROC dbo.Proc_InsertCustomer
(
@CustomerID     nchar(5),
@CompanyName	NVARCHAR(40),
@ContactName	NVARCHAR(30),
@ContactTitle	NVARCHAR(30),
@Address	NVARCHAR(60),
@City		NVARCHAR(15),
@Region		NVARCHAR(15),
@PostalCode	NVARCHAR(10),
@Country	NVARCHAR(15),
@Phone		NVARCHAR(24),
@Fax		NVARCHAR(24),
@Gender		CHAR(1),
@PaymentMode	CHAR(1),
@Isactive	NUMERIC(18),
@Cust_Id	int 		OUTPUT
)
AS
BEGIN
INSERT INTO Customers
(
CustomerID,	
CompanyName,	
ContactName,	
ContactTitle,	
Address,	
City,		
Region,
PostalCode,	
Country,	
Phone,		
Fax,	
Gender,		
PaymentMode,
Isactive
)
VALUES
(
@CustomerID,
@CompanyName,	
@ContactName,
@ContactTitle,	
@Address,
@City,		
@Region,
@PostalCode,	
@Country,
@Phone,		
@Fax,
@Gender,	
@PaymentMode,
@Isactive
)
SELECT @Cust_Id= Max(User_Id) FROM Customers 
END


GO

