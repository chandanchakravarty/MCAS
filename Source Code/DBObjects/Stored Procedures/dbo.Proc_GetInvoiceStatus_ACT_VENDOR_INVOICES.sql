IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetInvoiceStatus_ACT_VENDOR_INVOICES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetInvoiceStatus_ACT_VENDOR_INVOICES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.ACT_VENDOR_INVOICES
Created by      : Ajit Singh Chahal
Date            : 6/28/2005
Rule: Invoice can be approved only if Distributed fully, can be committed only if approved.
Purpose    	: Returns status of invoice 
N: Not Distributed
D:Distributed 
A:approved it is implicit that invoice is distributed.
C:committed , it is implicit that invoice is distributed and approved.
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- Proc_GetInvoiceStatus_ACT_VENDOR_INVOICES 20
-- drop PROC dbo.Proc_GetInvoiceStatus_ACT_VENDOR_INVOICES
CREATE PROC dbo.Proc_GetInvoiceStatus_ACT_VENDOR_INVOICES
(
@INVOICE_ID     int
)
AS
BEGIN
DECLARE @RETVALUE VARCHAR(5) ,
@ISAPPROVED CHAR(1),
@ISCOMMITED CHAR(1),
@REMAININGAMOUNT DECIMAL(18,2),
@INV_AMT DECIMAL(18,2)

SELECT @REMAININGAMOUNT=(VINV.INVOICE_AMOUNT - ISNULL((SELECT SUM(DISTRIBUTION_AMOUNT) 
FROM ACT_DISTRIBUTION_DETAILS WHERE GROUP_TYPE = 'VEN' AND GROUP_ID = VINV.INVOICE_ID),0)),
@INV_AMT = INVOICE_AMOUNT 
FROM ACT_VENDOR_INVOICES VINV WHERE INVOICE_ID = @INVOICE_ID


-- If Invoice amount = 0,or Remaining amount is <> 0,  then cannot COMMIT. Hence, status : N
 IF @INV_AMT <> 0
	 BEGIN
		IF @REMAININGAMOUNT=0
			BEGIN
				-- Commented <#1 Start>: IsApproved button is currently not being used.
				/*SELECT @ISAPPROVED=IS_APPROVED FROM  ACT_VENDOR_INVOICES WHERE INVOICE_ID = @INVOICE_ID
				IF @ISAPPROVED='Y'
				BEGIN*/
					SELECT @ISCOMMITED=IS_COMMITTED FROM  ACT_VENDOR_INVOICES WHERE INVOICE_ID = @INVOICE_ID
					IF @ISCOMMITED='Y'
			            BEGIN
						 SET @RETVALUE = 'C' -- Committed
					    END
					ELSE
			       		SET @RETVALUE = 'A' -- Approved : Display Commit
				/*END
				ELSE
					SET @RETVALUE = 'D' <#1 End> */
			END
		ELSE
			SET @RETVALUE = 'N' -- Not Distributed
	 END	
 ELSE
	 SET @RETVALUE = 'N'

	 SELECT @RETVALUE AS STATUS 

END


GO

