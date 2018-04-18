IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateVendorCheckDistribution]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateVendorCheckDistribution]
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
--DROP PROC dbo.Proc_UpdateVendorCheckDistribution
CREATE PROC dbo.Proc_UpdateVendorCheckDistribution
(  
	@CHECK_ID INT =NULL,
	@IDEN_ROW_ID	Int,
	@AMOUNT_TO_APPLY	Decimal(18,2)
)  
AS  
BEGIN  
DECLARE @AMOUNT DECIMAL(18,2)
	UPDATE ACT_VENDOR_CHECK_DISTRIBUTION	
	SET 
	AMOUNT_TO_APPLY	 = @AMOUNT_TO_APPLY,	
	LAST_UPDATED_DATETIME = GETDATE()
	WHERE IDEN_ROW_ID = @IDEN_ROW_ID

	
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

