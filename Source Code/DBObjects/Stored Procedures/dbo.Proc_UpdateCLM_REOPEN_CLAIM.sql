IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateCLM_REOPEN_CLAIM]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateCLM_REOPEN_CLAIM]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------
Proc Name       : dbo.Proc_UpdateCLM_REOPEN_CLAIM
Created by      : Vijay Arora
Date            : 6/19/2006
Purpose    	: To update the record in table named CLM_REOPEN_CLAIM
Revison History :
Used In 	: Wolverine
------------------------------------------------------------
Date     Review By          Comments
------   ------------       -------------------------*/
CREATE PROC Dbo.Proc_UpdateCLM_REOPEN_CLAIM
(
@CLAIM_ID     int,
@REOPEN_ID     int,
@REASON     varchar(500),
@MODIFIED_BY     int
)
AS
BEGIN
UPDATE  CLM_REOPEN_CLAIM
SET
REASON  =  @REASON,
MODIFIED_BY  =  @MODIFIED_BY,
LAST_UPDATED_DATETIME  =  GETDATE()
WHERE CLAIM_ID = @CLAIM_ID AND REOPEN_ID  =  @REOPEN_ID
END



GO

