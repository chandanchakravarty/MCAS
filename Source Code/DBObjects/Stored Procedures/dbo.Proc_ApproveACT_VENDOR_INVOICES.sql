IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ApproveACT_VENDOR_INVOICES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ApproveACT_VENDOR_INVOICES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : dbo.ACT_VENDOR_INVOICES
Created by      : Ajit Singh Chahal
Date            : 6/28/2005
Purpose    	:To approve records of vendor invoices entity.
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_ApproveACT_VENDOR_INVOICES
(
@INVOICE_ID     int,
@IS_APPROVED     char(1),
@APPROVED_BY     int ,
@APPROVED_DATE_TIME datetime,
@MODIFIED_BY     int,
@LAST_UPDATED_DATETIME     datetime
)
AS
BEGIN
update ACT_VENDOR_INVOICES
set
IS_APPROVED =    @IS_APPROVED,
APPROVED_DATE_TIME =    @APPROVED_DATE_TIME,
APPROVED_BY =   @APPROVED_BY ,
MODIFIED_BY =  @MODIFIED_BY  ,
LAST_UPDATED_DATETIME =@LAST_UPDATED_DATETIME
where INVOICE_ID =  @INVOICE_ID 
END






GO

