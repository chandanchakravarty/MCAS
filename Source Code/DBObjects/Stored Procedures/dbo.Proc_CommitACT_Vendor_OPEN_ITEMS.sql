IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_CommitACT_Vendor_OPEN_ITEMS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_CommitACT_Vendor_OPEN_ITEMS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_CommitACT_Vendor_OPEN_ITEMS
Created by      : Ravindra
Date            : 12-29-2006
Purpose    	: 
Revison History :
Used In 	: Wolverine

------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
-- drop proc dbo.Proc_CommitACT_Vendor_OPEN_ITEMS
CREATE PROC dbo.Proc_CommitACT_Vendor_OPEN_ITEMS
(
	@ENTITY_ID  	 INT, -- AGENCY_ID
	@GROUP_ID  	 INT,
	@COMMITTED_BY    Int	--User ID 
)	
AS
BEGIN

IF NOT EXISTS (SELECT IDEN_ROW_NO FROM  ACT_VENDOR_RECON_GROUP_DETAILS WHERE GROUP_ID = @GROUP_ID)
BEGIN 
	-- No records exists against this group
	return -1 
		
END




--CURSOR
DECLARE curCUSTOMER_ITEMS CURSOR   
LOCAL FORWARD_ONLY STATIC  
FOR
SELECT ITEM_REFERENCE_ID, RECON_AMOUNT
FROM ACT_VENDOR_RECON_GROUP_DETAILS   
WHERE GROUP_ID = @GROUP_ID  
	
		  
	
--Variables for retreiving the values from cursor   
DECLARE @ITEM_REFERENCE_ID INT, @RECON_AMOUNT DECIMAL(18,2)

OPEN curCUSTOMER_ITEMS  

FETCH NEXT FROM curCUSTOMER_ITEMS INTO @ITEM_REFERENCE_ID,@RECON_AMOUNT

WHILE @@FETCH_STATUS = 0
BEGIN
	
	UPDATE ACT_VENDOR_OPEN_ITEMS  SET TOTAL_PAID = ISNULL(TOTAL_PAID,0) +  @RECON_AMOUNT
	WHERE IDEN_ROW_ID = @ITEM_REFERENCE_ID AND VENDOR_ID = @ENTITY_ID
			
	
	FETCH NEXT FROM curCUSTOMER_ITEMS INTO @ITEM_REFERENCE_ID,@RECON_AMOUNT
	IF @@FETCH_STATUS <> 0 
		BREAK
				
END

	

CLOSE curCUSTOMER_ITEMS
DEALLOCATE curCUSTOMER_ITEMS

UPDATE ACT_RECONCILIATION_GROUPS SET IS_COMMITTED = 'Y' ,
	DATE_COMMITTED = GETDATE(),
	COMMITTED_BY  = @COMMITTED_BY
WHERE GROUP_ID = @GROUP_ID


return 1


END






GO

