IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertVendorCheckDistribution]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertVendorCheckDistribution]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------  
Proc Name       : dbo.Proc_InsertVendorCheckDistribution  
Created by      : Ravindra
Date            : 12-04-2006
Purpose       	:
------------------------------------------------------------  
Date     Review By          Comments  
------   ------------       -------------------------*/  
--DROP PROC dbo.Proc_InsertVendorCheckDistribution
CREATE PROC dbo.Proc_InsertVendorCheckDistribution
(  
	@CHECK_ID		Int,
	@VENDOR_ID		Int,
	@OPEN_ITEM_ROW_ID	Int,
	@AMOUNT_TO_APPLY	Decimal(18,2),
	@REF_INVOICE_ID		Int,	
	@REF_INVOICE_NO		Varchar(100),
	@REF_INVOICE_REF_NO	Varchar(100),
	@IDEN_ROW_ID int out
)  
AS  
BEGIN  
DECLARE @AMOUNT DECIMAL(18,2)	
	INSERT INTO ACT_VENDOR_CHECK_DISTRIBUTION(	
				CHECK_ID,
				VENDOR_ID,
				OPEN_ITEM_ROW_ID,
				AMOUNT_TO_APPLY,
				REF_INVOICE_ID,
				REF_INVOICE_NO,
				REF_INVOICE_REF_NO,
				CREATED_BY,
				CREATED_DATETIME,
				MODIFIED_BY,
				LAST_UPDATED_DATETIME
				)
	SELECT @CHECK_ID,@VENDOR_ID,@OPEN_ITEM_ROW_ID,@AMOUNT_TO_APPLY,
		@REF_INVOICE_ID,@REF_INVOICE_NO,@REF_INVOICE_REF_NO,
		CREATED_BY,GETDATE(),NULL,NULL
	FROM TEMP_ACT_CHECK_INFORMATION 
	WHERE CHECK_ID = @CHECK_ID

	SET @IDEN_ROW_ID = @@IDENTITY

   IF EXISTS(SELECT CHECK_ID FROM TEMP_ACT_CHECK_INFORMATION WHERE CHECK_ID = @CHECK_ID)
	BEGIN
		SELECT @AMOUNT = ISNULL(SUM(ISNULL(AMOUNT_TO_APPLY,0)),0) FROM ACT_VENDOR_CHECK_DISTRIBUTION
	    WHERE CHECK_ID = @CHECK_ID	
	   
		UPDATE TEMP_ACT_CHECK_INFORMATION
		SET CHECK_AMOUNT = @AMOUNT
		WHERE CHECK_ID = @CHECK_ID
	END
   

END





GO

