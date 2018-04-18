IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_Insert_CHECK_ACT_RECON_GROUP_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_Insert_CHECK_ACT_RECON_GROUP_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc_insert
Created by      : Vijay
Date            : 6/29/2005
Purpose    	: Insert value in ALL RECONCILIATION GROUP DETAILS FOR CHECK OPEN ITEMS.
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_Insert_CHECK_ACT_RECON_GROUP_DETAILS
(
	@IDEN_ROW_NO    	int	OUTPUT,
	@GROUP_ID     		int,
	@ITEM_TYPE     		nvarchar(10),
	@ITEM_REFERENCE_ID     	int,
	@SUB_LEDGER_TYPE	nvarchar(10),
	@RECON_AMOUNT  		decimal(9),
	@NOTE     		nvarchar(510),
	@DIV_ID     		smallint,
	@DEPT_ID     		smallint,
	@PC_ID     		smallint,
	@CHECK_TYPE_ID          int
	
)
AS
BEGIN
	--Customer
	--"2474" /*Return Premium Checks*/
	--"9936" :/*Return Suspense Checks*/
	--"9935" /*Return Over Payments/*/--
	if @CHECK_TYPE_ID=  2474 or @CHECK_TYPE_ID=  9936 or @CHECK_TYPE_ID=  9935 
	begin
		INSERT INTO ACT_CUSTOMER_RECON_GROUP_DETAILS
			(
				GROUP_ID,
				ITEM_TYPE,
				ITEM_REFERENCE_ID,
				SUB_LEDGER_TYPE,
				RECON_AMOUNT,
				NOTE,
				DIV_ID,
				DEPT_ID,
				PC_ID
			)
			VALUES
			(
				@GROUP_ID,
				@ITEM_TYPE,
				@ITEM_REFERENCE_ID,
				@SUB_LEDGER_TYPE,
				@RECON_AMOUNT,
				@NOTE,
				@DIV_ID,
				@DEPT_ID,
				@PC_ID
			)

	SELECT @IDEN_ROW_NO = Max(IDEN_ROW_NO) FROM ACT_CUSTOMER_RECON_GROUP_DETAILS
	end
		
	--Agency
	--Agency Commission Checks
	if @CHECK_TYPE_ID=  2472
	begin
		INSERT INTO ACT_AGENCY_RECON_GROUP_DETAILS
			(
				GROUP_ID,
				ITEM_TYPE,
				ITEM_REFERENCE_ID,
				SUB_LEDGER_TYPE,
				RECON_AMOUNT,
				NOTE,
				DIV_ID,
				DEPT_ID,
				PC_ID
			)
			VALUES
			(
				@GROUP_ID,
				@ITEM_TYPE,
				@ITEM_REFERENCE_ID,
				@SUB_LEDGER_TYPE,
				@RECON_AMOUNT,
				@NOTE,
				@DIV_ID,
				@DEPT_ID,
				@PC_ID
			)

	SELECT @IDEN_ROW_NO = Max(IDEN_ROW_NO) FROM ACT_AGENCY_RECON_GROUP_DETAILS
	end
		
	--Vendor
	if @CHECK_TYPE_ID=  9938
	begin
		INSERT INTO ACT_VENDOR_RECON_GROUP_DETAILS
			(
				GROUP_ID,
				ITEM_TYPE,
				ITEM_REFERENCE_ID,
				SUB_LEDGER_TYPE,
				RECON_AMOUNT,
				NOTE,
				DIV_ID,
				DEPT_ID,
				PC_ID
			)
			VALUES
			(
				@GROUP_ID,
				@ITEM_TYPE,
				@ITEM_REFERENCE_ID,
				@SUB_LEDGER_TYPE,
				@RECON_AMOUNT,
				@NOTE,
				@DIV_ID,
				@DEPT_ID,
				@PC_ID
			)

	SELECT @IDEN_ROW_NO = Max(IDEN_ROW_NO) FROM ACT_VENDOR_RECON_GROUP_DETAILS
	end
		
	--Tax
	if @CHECK_TYPE_ID=  9939
	begin
		INSERT INTO ACT_TAX_RECON_GROUP_DETAILS
			(
				GROUP_ID,
				ITEM_TYPE,
				ITEM_REFERENCE_ID,
				SUB_LEDGER_TYPE,
				RECON_AMOUNT,
				NOTE,
				DIV_ID,
				DEPT_ID,
				PC_ID
			)
			VALUES
			(
				@GROUP_ID,
				@ITEM_TYPE,
				@ITEM_REFERENCE_ID,
				@SUB_LEDGER_TYPE,
				@RECON_AMOUNT,
				@NOTE,
				@DIV_ID,
				@DEPT_ID,
				@PC_ID
			)

	SELECT @IDEN_ROW_NO = Max(IDEN_ROW_NO) FROM ACT_TAX_RECON_GROUP_DETAILS
	end
		
	--Claim DEFFRED TILL DISCUSSION
	--if @CHECK_TYPE_ID=  9937
	/*begin
			INSERT INTO ACT_CLAIM_RECON_GROUP_DETAILS
				(
					GROUP_ID,
					ITEM_TYPE,
					ITEM_REFERENCE_ID,
					SUB_LEDGER_TYPE,
					RECON_AMOUNT,
					NOTE,
					DIV_ID,
					DEPT_ID,
					PC_ID
				)
				VALUES
				(
					@GROUP_ID,
					@ITEM_TYPE,
					@ITEM_REFERENCE_ID,
					@SUB_LEDGER_TYPE,
					@RECON_AMOUNT,
					@NOTE,
					@DIV_ID,
					@DEPT_ID,
					@PC_ID
				)
	
		SELECT @IDEN_ROW_NO = Max(IDEN_ROW_NO) FROM ACT_CLAIM_RECON_GROUP_DETAILS
		end*/
		

END






GO

