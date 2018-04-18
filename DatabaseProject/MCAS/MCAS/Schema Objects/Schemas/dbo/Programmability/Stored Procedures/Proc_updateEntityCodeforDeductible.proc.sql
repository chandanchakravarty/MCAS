CREATE PROCEDURE Proc_updateEntityCodeforDeductible  
@deductibleId int,  
@TranAuditId int,  
@Remarks Nvarchar(250)      
WITH EXECUTE AS CALLER    
AS    
SET FMTONLY OFF;   
DECLARE @temp XML      
BEGIN    
SET NOCOUNT ON;    
SELECT       
     @temp = CAST(NewData AS XML)       
FROM       
     dbo.MNT_TransactionAuditLog      
WHERE      
     TranAuditId = @TranAuditId     -- or whatever criteria you have      
      
-- make your modification on that local XML var      
SET       
   @temp.modify('replace value of (/MNT_Deductible/DeductibleId/text())[1] with sql:variable("@deductibleId")')       
      
-- write it back into the table as TEXT column            
UPDATE       
   dbo.MNT_TransactionAuditLog      
SET       
   NewData = CAST(CAST(@temp AS NVARCHAR(MAX)) AS TEXT),      
   EntityCode = @deductibleId,  
   CustomInfo = @Remarks      
         
WHERE      
   TranAuditId = @TranAuditId        -- or whatever criteria you havedesc        
END      