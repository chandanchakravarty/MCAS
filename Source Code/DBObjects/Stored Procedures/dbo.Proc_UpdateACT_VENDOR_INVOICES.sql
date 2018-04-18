IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateACT_VENDOR_INVOICES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateACT_VENDOR_INVOICES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.ACT_VENDOR_INVOICES  
Created by      : Ajit Singh Chahal  
Date            : 6/28/2005  
Purpose     :To update records of vendor invoices entity.  
Revison History :  
Used In  : Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
-- drop proc dbo.Proc_UpdateACT_VENDOR_INVOICES  
CREATE PROC dbo.Proc_UpdateACT_VENDOR_INVOICES  
(  
 @INVOICE_ID     int,  
 @VENDOR_ID     int,  
 @INVOICE_NUM     nvarchar(40),  
 @REF_PO_NUM     nvarchar(50),  
 @TRANSACTION_DATE     datetime,  
 @DUE_DATE     datetime,  
 @INVOICE_AMOUNT     decimal(18,2),  
 @NOTE     nvarchar(140),  
 @MODIFIED_BY     int,  
 @LAST_UPDATED_DATETIME     datetime,  
 @FISCAL_ID smallint  
  
)  
AS  
BEGIN  
 update ACT_VENDOR_INVOICES  
 set  
 VENDOR_ID =   @VENDOR_ID ,  
 INVOICE_NUM =   @INVOICE_NUM ,  
 REF_PO_NUM =   @REF_PO_NUM ,  
 TRANSACTION_DATE =   @TRANSACTION_DATE ,  
 DUE_DATE =   @DUE_DATE ,  
 INVOICE_AMOUNT =    @INVOICE_AMOUNT,  
 IS_APPROVED = 'N',  
 IS_COMMITTED = 'N',  
 NOTE =   @NOTE ,  
 MODIFIED_BY =  @MODIFIED_BY  ,  
 LAST_UPDATED_DATETIME =@LAST_UPDATED_DATETIME,  
 FISCAL_ID = @FISCAL_ID  
 where INVOICE_ID = @INVOICE_ID  
END  
  
  
  
  
  
  
  
  



GO

