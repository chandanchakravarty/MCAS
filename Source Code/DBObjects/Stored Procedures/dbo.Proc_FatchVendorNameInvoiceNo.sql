IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FatchVendorNameInvoiceNo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FatchVendorNameInvoiceNo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------  
Proc Name          	: Dbo.Proc_FatchVendorNameInvoiceNo  
Created by           	: kranti singh  
Date                    : 07/06/2007  
Purpose               	:   
Revison History :  
Used In                	:   Wolverine    

Reviewed By	:	Anurag verma
Reviewed On	:	12-07-2007
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
---- DROP PROC Dbo.Proc_FatchVendorNameInvoiceNo    

create procedure dbo.Proc_FatchVendorNameInvoiceNo
(
 @INVOICE_ID int
)
AS


SELECT 
 MVL.INVOICE_NUM ,
 --AVI.COMPANY_NAME + ' '+ AVI.VENDOR_LNAME AS VENDOR_NAME
 AVI.COMPANY_NAME + ' - '  + isnull(AVI.VENDOR_ACC_NUMBER,'') AS  VENDOR_NAME 
 FROM ACT_VENDOR_INVOICES  MVL 
LEFT OUTER JOIN  MNT_VENDOR_LIST AVI ON MVL.VENDOR_ID = AVI.VENDOR_ID
WHERE MVL.INVOICE_ID=@INVOICE_ID









GO

