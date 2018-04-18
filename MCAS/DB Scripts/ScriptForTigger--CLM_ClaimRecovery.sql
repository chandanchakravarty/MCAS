

CREATE TRIGGER [dbo].[AfterUpdateCLM_ClaimRecovery]
ON [dbo].[CLM_ClaimRecovery]
AFTER UPDATE 
AS BEGIN
   UPDATE dbo.CLM_ClaimRecovery
   SET ModifiedDate = GETDATE()
   FROM INSERTED i
   WHERE i.Id = CLM_ClaimRecovery.Id
END

GO


