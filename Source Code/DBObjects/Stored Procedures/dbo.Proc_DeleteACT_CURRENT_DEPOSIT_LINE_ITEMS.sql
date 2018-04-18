IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeleteACT_CURRENT_DEPOSIT_LINE_ITEMS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeleteACT_CURRENT_DEPOSIT_LINE_ITEMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       :Proc_DeleteACT_CURRENT_DEPOSIT_LINE_ITEMS
Created by      :Vijay Joshi
Date            :6/24/2005
Purpose    	:Delete record in ACT_CURRENT_DEPOSIT_LINE_ITEMS
Revison History :
Used In 	:Wolverine
------------------------------------------------------------
Date     Review By          Comments
-----------------------------------------------------------*/
--drop proc Proc_DeleteACT_CURRENT_DEPOSIT_LINE_ITEMS

CREATE PROC Dbo.Proc_DeleteACT_CURRENT_DEPOSIT_LINE_ITEMS
(
	@CD_LINE_ITEM_ID	int
)
AS
BEGIN	

	
DECLARE @DEPOSIT_TYPE_CUSTOMER Varchar(5),
	 @DEPOSIT_TYPE_AGENCY Varchar(5),
	 @DEPOSIT_TYPE_CLAIM Varchar(5),
	 @DEPOSIT_TYPE_REINSURANCE Varchar(5),
	 @DEPOSIT_TYPE_MISC Varchar(5)

SET @DEPOSIT_TYPE_CUSTOMER = 'CUST'
SET @DEPOSIT_TYPE_AGENCY = 'AGN'
SET @DEPOSIT_TYPE_CLAIM = 'CLAM'
SET @DEPOSIT_TYPE_REINSURANCE = 'RINS'
SET @DEPOSIT_TYPE_MISC ='MISC'

Declare @DEPOSIT_ID int
DECLARE @DEPOSIT_TYPE varchar(6)

SELECT 
@DEPOSIT_ID = DEPOSIT_ID,
@DEPOSIT_TYPE = DEPOSIT_TYPE
FROM
ACT_CURRENT_DEPOSIT_LINE_ITEMS
WHERE
CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID


UPDATE ACT_CURRENT_DEPOSITS
SET	TOTAL_DEPOSIT_AMOUNT = (SELECT isnull(SUM(RECEIPT_AMOUNT),0)
FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS
WHERE DEPOSIT_ID = @DEPOSIT_ID AND CD_LINE_ITEM_ID <> @CD_LINE_ITEM_ID)
WHERE DEPOSIT_ID = @DEPOSIT_ID

-- -- TAPE_TOTAL
-- UPDATE ACT_CURRENT_DEPOSITS
-- SET
-- TAPE_TOTAL = (SELECT isnull(SUM(RECEIPT_AMOUNT),0)
-- FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS
-- WHERE DEPOSIT_ID = @DEPOSIT_ID AND CD_LINE_ITEM_ID <> @CD_LINE_ITEM_ID)
-- WHERE DEPOSIT_ID = @DEPOSIT_ID


--Ravindra  need to be handeled for individual screen
	-- Updating TAPE_TOTAL  
IF(@DEPOSIT_TYPE = @DEPOSIT_TYPE_CUSTOMER)
BEGIN 
	UPDATE ACT_CURRENT_DEPOSITS  
	SET TAPE_TOTAL_CUST = (
			SELECT SUM(RECEIPT_AMOUNT) FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS   
			WHERE DEPOSIT_ID = @DEPOSIT_ID AND DEPOSIT_TYPE = @DEPOSIT_TYPE
			AND CD_LINE_ITEM_ID <> @CD_LINE_ITEM_ID)  
	WHERE DEPOSIT_ID = @DEPOSIT_ID  
END  
ELSE IF(@DEPOSIT_TYPE = @DEPOSIT_TYPE_CLAIM OR @DEPOSIT_TYPE = @DEPOSIT_TYPE_REINSURANCE)
BEGIN 
	UPDATE ACT_CURRENT_DEPOSITS  
	SET TAPE_TOTAL_CLM = (
			SELECT SUM(RECEIPT_AMOUNT) FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS   
			WHERE DEPOSIT_ID = @DEPOSIT_ID AND DEPOSIT_TYPE = @DEPOSIT_TYPE
			AND CD_LINE_ITEM_ID <> @CD_LINE_ITEM_ID)  
	WHERE DEPOSIT_ID = @DEPOSIT_ID  
END  
ELSE IF(@DEPOSIT_TYPE = @DEPOSIT_TYPE_MISC )
BEGIN 
	UPDATE ACT_CURRENT_DEPOSITS  
	SET TAPE_TOTAL_MISC = (
			SELECT SUM(RECEIPT_AMOUNT) FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS   
			WHERE DEPOSIT_ID = @DEPOSIT_ID AND DEPOSIT_TYPE = @DEPOSIT_TYPE
			AND CD_LINE_ITEM_ID <> @CD_LINE_ITEM_ID)  
	WHERE DEPOSIT_ID = @DEPOSIT_ID  

	-- deleting distribution details for this line item
	DELETE FROM  ACT_DISTRIBUTION_DETAILS
	WHERE GROUP_TYPE='DEP' 
	AND GROUP_ID = @CD_LINE_ITEM_ID	
	
END  


IF @DEPOSIT_TYPE = @DEPOSIT_TYPE_AGENCY
BEGIN
	--Deleting the old reconciliation
	DECLARE @GROUP_ID INT
	SELECT @GROUP_ID = GROUP_ID FROM ACT_RECONCILIATION_GROUPS WHERE CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID
	
	--Updating the reconciled amount in open item table				
	UPDATE ACT_AGENCY_OPEN_ITEMS
	SET TOTAL_PAID = OI.TOTAL_PAID - MRGD.RECON_AMOUNT
	FROM ACT_AGENCY_OPEN_ITEMS OI, ACT_AGENCY_RECON_GROUP_DETAILS MRGD
	WHERE OI.IDEN_ROW_ID = MRGD.ITEM_REFERENCE_ID AND MRGD.ITEM_TYPE = 'AGN' 
	AND OI.UPDATED_FROM = 'D' AND OI.SOURCE_ROW_ID = @CD_LINE_ITEM_ID
	DELETE FROM ACT_AGENCY_RECON_GROUP_DETAILS WHERE GROUP_ID
	IN(
		SELECT GROUP_ID FROM ACT_RECONCILIATION_GROUPS WHERE CD_LINE_ITEM_ID 
		=@CD_LINE_ITEM_ID
	  )
	
	DELETE FROM ACT_RECONCILIATION_GROUPS WHERE GROUP_ID
	IN(
		SELECT GROUP_ID FROM ACT_RECONCILIATION_GROUPS WHERE CD_LINE_ITEM_ID 
		=@CD_LINE_ITEM_ID
	  )
END

DELETE FROM ACT_CUSTOMER_OPEN_ITEMS WHERE SOURCE_ROW_ID = @CD_LINE_ITEM_ID AND UPDATED_FROM = 'D'
DELETE FROM ACT_AGENCY_OPEN_ITEMS WHERE SOURCE_ROW_ID = @CD_LINE_ITEM_ID AND UPDATED_FROM = 'D'
DELETE FROM 
ACT_CURRENT_DEPOSIT_LINE_ITEMS
WHERE
CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID



return 1
END



















GO

