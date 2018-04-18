IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DOC_UpdateTransactionDesc]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DOC_UpdateTransactionDesc]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





/*----------------------------------------------------------                
Proc Name       : dbo.Proc_DOC_UpdateTransactionDesc
Created by      : Deepak Gupta         
Date            : 09-14-2006                         
Purpose         : To Update Transaction Description
Revison History :                
Used In         : Wolverine                
               
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
    
CREATE PROC DBO.Proc_DOC_UpdateTransactionDesc
(                
	@TRANS_ID INT,
	@TRANSDESC text
)                
AS
BEGIN                
	
	UPDATE MNT_TRANSACTION_LOG SET TRANS_DESC = @TRANSDESC WHERE TRANS_ID = @TRANS_ID;

END





GO

