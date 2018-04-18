IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteACT_VENDOR_INVOICES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteACT_VENDOR_INVOICES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*----------------------------------------------------------
Proc Name       : dbo.ACT_VENDOR_INVOICES
Created by      : Ajit Singh Chahal
Date            : 6/28/2005
Purpose    		: To delete records of vendor invoices entity and its distribution.
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- drop proc dbo.Proc_DeleteACT_VENDOR_INVOICES
CREATE PROC dbo.Proc_DeleteACT_VENDOR_INVOICES
(
	@INVOICE_ID     int
)
AS
BEGIN

	delete from ACT_DISTRIBUTION_DETAILS where group_type='VEN' and group_id=@INVOICE_ID

	delete  from ACT_VENDOR_INVOICES where INVOICE_ID = @INVOICE_ID
END




GO

