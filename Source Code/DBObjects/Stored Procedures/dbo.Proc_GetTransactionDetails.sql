IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetTransactionDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetTransactionDetails]
GO

SET ANSI_NULLS OFF
GO

SET QUOTED_IDENTIFIER OFF
GO



/*----------------------------------------------------------      
Proc Name     : dbo.Proc_GetTransactionDetails
Created by      : Anurag Verma      
Date                  : 28/03/2005      
Purpose         : To retrieve transaction details
Revison History :      
Used In         : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
CREATE PROC Dbo.Proc_GetTransactionDetails
(      
  @Tran_ID      nvarchar(4)
)      
AS      
SET NOCOUNT ON
BEGIN    
select TL.CHANGES_XML XMLDATASTRING from TRANSACTIONLOG TL where TL.ID = @tran_id
END
SET NOCOUNT OFF


GO

