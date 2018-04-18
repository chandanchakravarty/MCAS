IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateStatusOverPayment]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateStatusOverPayment]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
Proc Name        : dbo.Proc_UpdateStatusOverPayment  
Created by       : Praveen Kasana 
Date             :   
Purpose        :   
Revison History :    
Used In   :Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------------------------------------------------------------*/    
-- drop proc dbo.Proc_UpdateStatusOverPayment  
CREATE PROC dbo.Proc_UpdateStatusOverPayment
(   
 @PAY_PAL_REF_ID VARCHAR(50)
)  
AS  
BEGIN   
DECLARE @IDEN_ROW_ID INT
DECLARE @STATUS VARCHAR(20)

	SELECT
	@IDEN_ROW_ID = OI.IDEN_ROW_ID ,
	@STATUS = OI.ITEM_STATUS		

	FROM ACT_CUSTOMER_OPEN_ITEMS OI WITH(NOLOCK)
	INNER JOIN EOD_CREDIT_CARD_SPOOL  SPOOL  WITH(NOLOCK)
	ON SPOOL.REF_DEP_DETAIL_ID = OI.SOURCE_ROW_ID
	AND OI.UPDATED_FROM = 'D'
	WHERE SPOOL.PAY_PAL_REF_ID = @PAY_PAL_REF_ID

IF(@STATUS = 'OP')
BEGIN
	UPDATE ACT_CUSTOMER_OPEN_ITEMS SET ITEM_STATUS = 'DP' 
	WHERE IDEN_ROW_ID = @IDEN_ROW_ID
END


 
END




GO

