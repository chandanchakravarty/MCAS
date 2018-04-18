CREATE TRIGGER [dbo].[AfterUpdateMNT_HosipitalDetails] ON [dbo].[MNT_HosipitalDetails] WITH EXECUTE AS CALLER AFTER  UPDATE AS 
BEGIN
   UPDATE dbo.MNT_HosipitalDetails
   SET ModifiedDate = GETDATE()
   FROM INSERTED i
   WHERE i.ID = MNT_HosipitalDetails.ID
END


