IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ProcessPremiumOnDecline]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ProcessPremiumOnDecline]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
 Proc Name       : Dbo.Proc_ProcessPremiumOnDecline
 Created by      : Ravindra 
 Date            : 2-10-2007
 Purpose         : Mark the payment recieved on policy for return (RSP) in case of decline
			or resciend of Suspended policy
 Revison History :                
 Used In     	 : Wolverine        

------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
-- drop proc Dbo.Proc_ProcessPremiumOnDecline
CREATE PROC Dbo.Proc_ProcessPremiumOnDecline
(                
	@CUSTOMER_ID INT,      
	@POLICY_ID INT,      
	@POLICY_VERSION_ID Int
)                
AS               
BEGIN 
	DECLARE @DEPOSIT_OPEN_ITEMS TABLE 
		(
			[IDENT_COL] [int] IDENTITY(1,1) NOT NULL ,
			[OPEN_ITEM_ID] Int,
			[ITEM_STATUS]  VarChar(3)
		)

	INSERT INTO @DEPOSIT_OPEN_ITEMS (OPEN_ITEM_ID,ITEM_STATUS)
	SELECT IDEN_ROW_ID , ITEM_STATUS
	FROM ACT_CUSTOMER_OPEN_ITEMS 
	WHERE CUSTOMER_ID = @CUSTOMER_ID 
	AND POLICY_ID = @POLICY_ID 
	AND POLICY_VERSION_ID = @POLICY_VERSION_ID 
	AND UPDATED_FROM = 'D'
	AND ITEM_TRAN_CODE = 'DEP'

	DECLARE @ITERATOR Int
	SET @ITERATOR  = 1
	
	WHILE 1=1 
	BEGIN 
		IF NOT EXISTS(SELECT IDENT_COL FROM @DEPOSIT_OPEN_ITEMS WHERE IDENT_COL = @ITERATOR)
		BEGIN 
			BREAK
		END

		UPDATE ACT_CUSTOMER_OPEN_ITEMS 
		SET ITEM_ORIGINAL_STATUS = TEMP_OP.ITEM_STATUS,
			ITEM_STATUS = 'RSP'
		FROM @DEPOSIT_OPEN_ITEMS TEMP_OP
		WHERE ACT_CUSTOMER_OPEN_ITEMS.IDEN_ROW_ID = TEMP_OP.OPEN_ITEM_ID
	
		SET @ITERATOR = @ITERATOR + 1
	END
	
END


GO

