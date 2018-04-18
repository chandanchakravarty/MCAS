IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTransactionCodeDetailsXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTransactionCodeDetailsXML]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------
Proc Name       : Proc_GetTransactionCodeDetailsXML
Created by      : Ajit Singh Chahal
Date            : 6/7/2005
Purpose    	: To get record for xml.
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Proc_GetTransactionCodeDetailsXML
@DETAIL_ID int
as
begin
SELECT     ACT_TRAN_CODE_GROUP_DETAILS.DETAIL_ID, ACT_TRAN_CODE_GROUP_DETAILS.TRAN_ID, 
           ACT_TRAN_CODE_GROUP_DETAILS.DEF_SEQ, ACT_TRAN_CODE_GROUP_DETAILS.IS_ACTIVE, 
           ACT_TRANSACTION_CODES.TRAN_CODE, ACT_TRANSACTION_CODES.DISPLAY_DESCRIPTION
FROM       ACT_TRAN_CODE_GROUP_DETAILS INNER JOIN
           ACT_TRANSACTION_CODES ON ACT_TRAN_CODE_GROUP_DETAILS.TRAN_ID = ACT_TRANSACTION_CODES.TRAN_ID
where (DETAIL_ID  = @DETAIL_ID)
end

--exec Proc_GetTransactionCodeDetailsXML 3







GO

