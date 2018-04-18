IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateACT_CURRENT_DEPOSIT_LINE_ITEMS_Edited]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateACT_CURRENT_DEPOSIT_LINE_ITEMS_Edited]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : dbo.Proc
Created by      : Ebix
Date            : 6/24/2005
Purpose    	: To update recodes of ACT_CURRENT_DEPOSIT_LINE_ITEMS
Revison History :
Used In 	      : Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_UpdateACT_CURRENT_DEPOSIT_LINE_ITEMS_Edited
(
@CD_LINE_ITEM_ID     int,
@LINE_ITEM_INTERNAL_NUMBER     int,
@DIV_ID     smallint,
@PC_ID     smallint,
@DEPT_ID smallint,
@DEPOSIT_TYPE     nvarchar(10),
@BANK_NAME     nvarchar(510),
@CHECK_NUM     nvarchar(50),
@RECEIPT_AMOUNT     decimal(9),
@PAYOR_TYPE     nvarchar(10),
@RECEIPT_FROM_ID     int,
@RECEIPT_FROM_NAME varchar(255),
@LINE_ITEM_DESCRIPTION     nvarchar(510),
@POLICY_ID     smallint,
@REF_CUSTOMER_ID     int,
@MODIFIED_BY     int,
@LAST_UPDATED_DATETIME     datetime
)AS
BEGIN
update  ACT_CURRENT_DEPOSIT_LINE_ITEMS
set
LINE_ITEM_INTERNAL_NUMBER     = @LINE_ITEM_INTERNAL_NUMBER,
DIV_ID     =@DIV_ID ,
DEPT_ID     = @DEPT_ID,
PC_ID     = @PC_ID,
DEPOSIT_TYPE   =  @DEPOSIT_TYPE,
BANK_NAME     =@BANK_NAME,
CHECK_NUM     = @CHECK_NUM,
RECEIPT_AMOUNT     = @RECEIPT_AMOUNT,
PAYOR_TYPE     = @PAYOR_TYPE,
RECEIPT_FROM_ID     = @RECEIPT_FROM_ID,
RECEIPT_FROM_NAME = @RECEIPT_FROM_NAME,
LINE_ITEM_DESCRIPTION    = @LINE_ITEM_DESCRIPTION,
POLICY_ID     = @POLICY_ID,
REF_CUSTOMER_ID     = @REF_CUSTOMER_ID,
MODIFIED_BY     = @MODIFIED_BY,
LAST_UPDATED_DATETIME    = @LAST_UPDATED_DATETIME
where CD_LINE_ITEM_ID = @CD_LINE_ITEM_ID
end









GO

