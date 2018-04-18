IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetOpenItemIDForCCDeposit]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetOpenItemIDForCCDeposit]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------    
Proc Name       : dbo.Proc_GetOpenItemIDForCCDeposit
Created by      : Ravindra Gupta
Date            : 6/28/2005    
Purpose			:To commit records of check entity.    
Revison History :    
Used In  : Wolverine    
 
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
--DROP PROC dbo.Proc_GetOpenItemIDForCCDeposit
CREATE PROC [dbo].[Proc_GetOpenItemIDForCCDeposit]    
(   
	@PAYPAL_REF_ID		Varchar(50) , 
	@OPEN_ITEM_ID	Int	out
)
AS
BEGIN 

	SELECT @OPEN_ITEM_ID = OI.IDEN_ROW_ID
	FROM EOD_CREDIT_CARD_SPOOL SPOOL 
	INNER JOIN ACT_CURRENT_DEPOSIT_LINE_ITEMS CDLI 
	ON SPOOL.REF_DEPOSIT_ID =  CDLI.DEPOSIT_ID
	AND SPOOL.REF_DEP_DETAIL_ID  = CDLI.CD_LINE_ITEM_ID
	INNER JOIN ACT_CUSTOMER_OPEN_ITEMS OI
	ON SPOOL.REF_DEP_DETAIL_ID = OI.SOURCE_ROW_ID 
	AND OI.UPDATED_FROM = 'D'
	WHERE SPOOL.PAY_PAL_REF_ID = @PAYPAL_REF_ID


END    





GO

