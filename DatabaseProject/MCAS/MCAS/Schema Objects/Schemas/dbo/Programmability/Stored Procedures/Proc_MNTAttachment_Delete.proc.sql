CREATE PROCEDURE [dbo].[Proc_MNTAttachment_Delete]
	@AttachId [int]
WITH EXECUTE AS CALLER
AS
BEGIN
  SET FMTONLY OFF; 
  Delete from MNT_AttachmentList  where AttachId = @AttachId 
  END


