IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertACT_VENDOR_INVOICES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertACT_VENDOR_INVOICES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name       : dbo.ACT_VENDOR_INVOICES    
Created by      : Ajit Singh Chahal    
Date            : 6/28/2005    
Purpose     :To insert records of vendor invoices entity.    
Revison History :    
Used In  : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
-- drop proc dbo.Proc_InsertACT_VENDOR_INVOICES    
CREATE PROC dbo.Proc_InsertACT_VENDOR_INVOICES    
(    
 @INVOICE_ID     int out,    
 @VENDOR_ID     int,    
 @INVOICE_NUM     nvarchar(40),    
 @REF_PO_NUM     nvarchar(50),    
 @TRANSACTION_DATE     datetime,    
 @DUE_DATE     datetime,    
 @INVOICE_AMOUNT     decimal(18,2),    
 @NOTE     nvarchar(140),    
 @IS_ACTIVE     nchar(2),    
 @CREATED_BY     int,    
 @CREATED_DATETIME     datetime,    
 @MODIFIED_BY     int,    
 @LAST_UPDATED_DATETIME     datetime,  
 @FISCAL_ID smallint    
)    
AS    
BEGIN    
 select @INVOICE_ID= isnull(max(INVOICE_ID)+1,1) from ACT_VENDOR_INVOICES    
 INSERT INTO ACT_VENDOR_INVOICES    
 (    
  INVOICE_ID,    
  VENDOR_ID,    
  INVOICE_NUM,    
  REF_PO_NUM,    
  TRANSACTION_DATE,    
  DUE_DATE,    
  INVOICE_AMOUNT,    
  NOTE,    
  IS_ACTIVE,    
  CREATED_BY,    
  CREATED_DATETIME,    
  MODIFIED_BY,    
  LAST_UPDATED_DATETIME,  
  FISCAL_ID    
 )    
 VALUES    
 (    
  @INVOICE_ID,    
  @VENDOR_ID,    
  @INVOICE_NUM,    
  @REF_PO_NUM,    
  @TRANSACTION_DATE,    
  @DUE_DATE,    
  @INVOICE_AMOUNT,    
  @NOTE,    
  @IS_ACTIVE,    
  @CREATED_BY,    
  @CREATED_DATETIME,    
  @MODIFIED_BY,    
  @LAST_UPDATED_DATETIME,  
  @FISCAL_ID    
 )    
END    
    
    
  
  
  



GO

