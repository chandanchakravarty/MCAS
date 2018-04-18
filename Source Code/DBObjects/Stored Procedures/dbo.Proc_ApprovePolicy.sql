IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ApprovePolicy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ApprovePolicy]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*--------------------------------------------------------------------
CREATED BY			:	Vijay Joshi
CREATED DATE TIME	:	Sep 30, 2005
PURPOSE				: 	Use for the approval or unapproval of policy
REVIEW HISTORY
REVIEW BY		DATE		PURPOSE

---------------------------------------------------------------------*/
CREATE PROCEDURE dbo.Proc_ApprovePolicy
(
	@POLICY_ID				INT,			--POLICY Identification number
	@POLICY_VERSION_ID		INT,			--Policy veriosn indentification number
	@CUSTOMER_ID			INT,			--Customer identification number
	@POLICY_ACCOUNT_STATUS	CHAR(2),		--Policy status
	@PARAM1					VARCHAR(20),	--Extra Prarameter to be used in future
	@PARAM2					VARCHAR(20)		--Extra Prarameter to be used in future
)
AS
BEGIN
	DECLARE @PS_NORMAL CHAR(2)
	SET @PS_NORMAL = 'NP'		--Policy status normal

	--Changing the status of policy 
	UPDATE POL_CUSTOMER_POLICY_LIST
	SET POLICY_ACCOUNT_STATUS = @POLICY_ACCOUNT_STATUS
	WHERE POLICY_ID = @POLICY_ID AND
	POLICY_VERSION_ID = @POLICY_VERSION_ID AND
	CUSTOMER_ID = @CUSTOMER_ID

	--Now If any deposit exists against this deposit then changing its status also
	IF EXISTS (SELECT IDEN_ROW_ID 
				FROM ACT_CUSTOMER_OPEN_ITEMS
				WHERE UPDATED_FROM = 'D'
					AND POLICY_ID = @POLICY_ID 
					AND POLICY_VERSION_ID = @POLICY_VERSION_ID
					AND CUSTOMER_ID = @CUSTOMER_ID)
	BEGIN
		
		IF @POLICY_ACCOUNT_STATUS = @PS_NORMAL
		BEGIN
			
			--Policy status is normal hence converting all suspense payment to normal payment
			--IF due is equal to paid
			UPDATE ACT_CUSTOMER_OPEN_ITEMS
			SET ITEM_STATUS = 'NP'
			WHERE UPDATED_FROM = 'D' 
				AND POLICY_ID = @POLICY_ID 
				AND POLICY_VERSION_ID = @POLICY_VERSION_ID
				AND CUSTOMER_ID = @CUSTOMER_ID
				AND TOTAL_DUE >= TOTAL_PAID
				AND ITEM_STATUS = 'SP'
			
			--IF due is not equal to paid, onvertion it to over payment
			UPDATE ACT_CUSTOMER_OPEN_ITEMS
			SET ITEM_STATUS = 'OP'
			WHERE UPDATED_FROM = 'D' 
				AND POLICY_ID = @POLICY_ID 
				AND POLICY_VERSION_ID = @POLICY_VERSION_ID
				AND CUSTOMER_ID = @CUSTOMER_ID
				AND TOTAL_DUE < TOTAL_PAID
				AND ITEM_STATUS = 'SP'

		END
	END

END


GO

