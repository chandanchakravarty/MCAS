IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTransactionXml]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTransactionXml]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO





--CREATED BY MOHIT ON 5/5/2005
/*
Modified By : Anurag Verma
Modified On : 26/02/2007
Purpose	    : To fetch is_validXml field 


*/



CREATE   PROCEDURE Proc_GetTransactionXml
(
	@TRANS_ID Int	
)
AS
SELECT TRANS_DETAILS,isnull(IS_VALIDXML,'')					IS_VALIDXML
FROM MNT_TRANSACTION_XML
WHERE TRANS_ID = @TRANS_ID



GO

