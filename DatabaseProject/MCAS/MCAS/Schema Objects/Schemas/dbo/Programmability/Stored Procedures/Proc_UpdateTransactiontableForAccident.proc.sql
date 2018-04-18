CREATE PROCEDURE [dbo].[Proc_UpdateTransactiontableForAccident]
	@AccidentId int,
	@TranAuditId int
WITH EXECUTE AS CALLER
AS
BEGIN  
SET FMTONLY OFF;  
DECLARE @temp XML


SELECT 
     @temp = CAST(NewData AS XML) 
FROM 
     dbo.MNT_TransactionAuditLog
WHERE
     TranAuditId = @TranAuditId     -- or whatever criteria you have

-- make your modification on that local XML var
SET 
   @temp.modify('replace value of (/ClaimAccidentDetail/AccidentClaimId/text())[1] with sql:variable("@Accidentid")') 

-- write it back into the table as TEXT column      
UPDATE 
   dbo.MNT_TransactionAuditLog
SET 
   NewData = CAST(CAST(@temp AS NVARCHAR(MAX)) AS TEXT),
   AccidentId = @AccidentId
   
WHERE
   TranAuditId = @TranAuditId        -- or whatever criteria you havedesc  

END

GO


