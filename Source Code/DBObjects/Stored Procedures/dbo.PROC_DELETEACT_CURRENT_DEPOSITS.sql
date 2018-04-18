IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_DELETEACT_CURRENT_DEPOSITS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_DELETEACT_CURRENT_DEPOSITS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------
Proc Name       : Proc_DeleteACT_CURRENT_DEPOSITS
Created by      : Vijay Joshi
Date            : 23/June/2005
Purpose    	: Delete records from Deposit table
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
--drop proc PROC_DELETEACT_CURRENT_DEPOSITS
CREATE PROC dbo.PROC_DELETEACT_CURRENT_DEPOSITS
(
  @DEPOSIT_ID     int
)
AS
BEGIN
--Checking whether record commited or not
--If commited then we will not delete record as commited record can not deleted
If (SELECT Upper(ISNULL(IS_COMMITED,'')) FROM ACT_CURRENT_DEPOSITS WHERE DEPOSIT_ID = @DEPOSIT_ID) = 'Y'
BEGIN
--Record commited , hence we will not delete it
	return -2
END

--Ravindra(05-15-2008)
--If commited to spool then we will not delete record as commited record can not deleted
If (SELECT Upper(ISNULL(IS_COMMITED_TO_SPOOL,'')) FROM ACT_CURRENT_DEPOSITS WHERE DEPOSIT_ID = @DEPOSIT_ID) = 'Y'
BEGIN
--Record commited , hence we will not delete it
	return -2
END


--If agency receipt exists, then deleting the automatic reconciliation also
IF EXISTS (
	SELECT CD_LINE_ITEM_ID FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS 
	WHERE DEPOSIT_ID = @DEPOSIT_ID AND DEPOSIT_TYPE = 'AGN')

BEGIN
	--Deleting the old reconciliation
	
	DELETE ACT_AGENCY_RECON_GROUP_DETAILS 
	FROM ACT_AGENCY_RECON_GROUP_DETAILS DET
	LEFT JOIN ACT_RECONCILIATION_GROUPS GRP ON GRP.GROUP_ID = DET.GROUP_ID
	LEFT JOIN ACT_CURRENT_DEPOSIT_LINE_ITEMS CDLI ON CDLI.CD_LINE_ITEM_ID = GRP.CD_LINE_ITEM_ID
	WHERE CDLI.DEPOSIT_ID = @DEPOSIT_ID 
	
	
	DELETE ACT_RECONCILIATION_GROUPS
	FROM ACT_RECONCILIATION_GROUPS GRP
	LEFT JOIN ACT_CURRENT_DEPOSIT_LINE_ITEMS CDLI ON CDLI.CD_LINE_ITEM_ID = GRP.CD_LINE_ITEM_ID
	WHERE CDLI.DEPOSIT_ID = @DEPOSIT_ID 
	
	--Deleting the customer open items
	DELETE ACT_AGENCY_OPEN_ITEMS
	FROM ACT_AGENCY_OPEN_ITEMS ACOI
	LEFT JOIN ACT_CURRENT_DEPOSIT_LINE_ITEMS CDLI ON CDLI.CD_LINE_ITEM_ID = ACOI.SOURCE_ROW_ID AND ACOI.UPDATED_FROM = 'D'
	LEFT JOIN ACT_CURRENT_DEPOSITS CD ON CD.DEPOSIT_ID = CDLI.DEPOSIT_ID
	WHERE CD.DEPOSIT_ID = @DEPOSIT_ID

	-- Delete From Agency Statement
	DELETE ACT_AGENCY_STATEMENT 
	FROM ACT_AGENCY_STATEMENT  ACOI
	LEFT JOIN ACT_CURRENT_DEPOSIT_LINE_ITEMS CDLI ON CDLI.CD_LINE_ITEM_ID = ACOI.SOURCE_ROW_ID AND ACOI.UPDATED_FROM = 'D'
	LEFT JOIN ACT_CURRENT_DEPOSITS CD ON CD.DEPOSIT_ID = CDLI.DEPOSIT_ID
	WHERE CD.DEPOSIT_ID = @DEPOSIT_ID

END	

--Ravindra(05-15-2008): Customer Open items are created only when deposit is commited and a commited
--deposit can not be deleted
--Deleting the customer open items
--DELETE ACT_CUSTOMER_OPEN_ITEMS
--FROM ACT_CUSTOMER_OPEN_ITEMS ACOI
--LEFT JOIN ACT_CURRENT_DEPOSIT_LINE_ITEMS CDLI ON CDLI.CD_LINE_ITEM_ID = ACOI.SOURCE_ROW_ID AND ACOI.UPDATED_FROM = 'D'
--LEFT JOIN ACT_CURRENT_DEPOSITS CD ON CD.DEPOSIT_ID = CDLI.DEPOSIT_ID
--WHERE CD.DEPOSIT_ID = @DEPOSIT_ID



--Deleting entries from sitributin details 
DELETE ACT_DISTRIBUTION_DETAILS
FROM ACT_DISTRIBUTION_DETAILS DD
LEFT JOIN ACT_CURRENT_DEPOSIT_LINE_ITEMS CDLI ON DD.GROUP_ID= CDLI.CD_LINE_ITEM_ID AND DD.GROUP_TYPE = 'DEP'
WHERE CDLI.DEPOSIT_ID = @DEPOSIT_ID


/*Deleting the child records*/
DELETE FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS
WHERE DEPOSIT_ID = @DEPOSIT_ID

DELETE FROM ACT_CURRENT_DEPOSITS
WHERE DEPOSIT_ID = @DEPOSIT_ID

/*Deleting form RTL*/
DELETE FROM RTL_PROCESS_LOG
WHERE DEPOSIT_ID = @DEPOSIT_ID

return 1
	
END

















GO

