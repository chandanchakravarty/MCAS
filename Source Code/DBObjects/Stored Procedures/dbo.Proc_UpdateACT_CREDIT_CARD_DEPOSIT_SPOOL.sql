IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateACT_CREDIT_CARD_DEPOSIT_SPOOL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateACT_CREDIT_CARD_DEPOSIT_SPOOL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------  
Proc Name        : dbo.Proc_UpdateACT_CREDIT_CARD_DEPOSIT_SPOOL
Created by       : Praveen kasana
Date             : 07-08-2009
Purpose      	 : Update status of record in  CC Spool Table
Revison History :  
Used In   :Wolverine  
------------------------------------------------------------  
Date     Review By          Comments  
------------------------------------------------------------*/  
-- drop proc dbo.Proc_UpdateACT_CREDIT_CARD_DEPOSIT_SPOOL
CREATE PROC dbo.Proc_UpdateACT_CREDIT_CARD_DEPOSIT_SPOOL
(
@SPOOL_ID Int,
@REF_DEPOSIT_ID Int = null,
@REF_DEP_DETAIL_ID int = null

)
AS
BEGIN 


UPDATE ACT_CREDIT_CARD_DEPOSIT_SPOOL SET PROCESSED_DATETIME = GETDATE(),PROCESSED = 'Y' 
WHERE IDEN_ROW_ID = @SPOOL_ID

DECLARE @REF_SPOOL_ID INT
SELECT @REF_SPOOL_ID = REF_SPOOL_ID FROM ACT_CREDIT_CARD_DEPOSIT_SPOOL WITH(NOLOCK)
WHERE IDEN_ROW_ID = @SPOOL_ID


UPDATE EOD_CREDIT_CARD_SPOOL SET REF_DEPOSIT_ID = @REF_DEPOSIT_ID,
REF_DEP_DETAIL_ID = @REF_DEP_DETAIL_ID 
WHERE IDEN_ROW_ID = @REF_SPOOL_ID




END







GO

