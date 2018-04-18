IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteBillingInfo]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteBillingInfo]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
CREATE PROC [dbo].Proc_DeleteBillingInfo
(
	@POLICY_NUMBER VARCHAR(20),
	@TRANS_TYPE VARCHAR(10) --EFT / CREDIT
)

AS


DECLARE @CUSTOMER_ID INT
DECLARE @POLICY_ID INT



SELECT @CUSTOMER_ID = CUSTOMER_ID ,@POLICY_ID = POLICY_ID
FROM POL_CUSTOMER_POLICY_LIST WHERE POLICY_NUMBER = @POLICY_NUMBER


IF(@TRANS_TYPE = 'EFT')
BEGIN
	
	DELETE FROM ACT_POL_EFT_CUST_INFO WHERE 
	CUSTOMER_ID = @CUSTOMER_ID
	AND POLICY_ID=@POLICY_ID
	
END
ELSE IF(@TRANS_TYPE = 'CREDIT')
BEGIN
	DELETE FROM ACT_POL_CREDIT_CARD_DETAILS  WHERE
     CUSTOMER_ID = @CUSTOMER_ID
	AND POLICY_ID=@POLICY_ID
	
END 














GO

