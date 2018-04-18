IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetXMLACT_VENDOR_INVOICES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetXMLACT_VENDOR_INVOICES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.ACT_VENDOR_INVOICES
Created by      : Ajit Singh Chahal
Date            : 6/28/2005
Purpose    	:To get xml record of vendor invoices entity.
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- DROP PROC dbo.Proc_GetXMLACT_VENDOR_INVOICES
CREATE PROC dbo.Proc_GetXMLACT_VENDOR_INVOICES
(
@INVOICE_ID     int out
)
as begin
select 
INVOICE_ID ,
VENDOR_ID    ,
INVOICE_NUM    ,
REF_PO_NUM    ,
convert(varchar,TRANSACTION_DATE,101) as TRANSACTION_DATE   ,
convert(varchar,DUE_DATE,101)   as DUE_DATE  ,
INVOICE_AMOUNT    , 
NOTE    ,
CREATED_DATETIME,FISCAL_ID    
from ACT_VENDOR_INVOICES 
where INVOICE_ID   = @INVOICE_ID  
end





GO

