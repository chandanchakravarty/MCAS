CREATE PROCEDURE [dbo].[Proc_FileNameForMNT_AttachentList]
	@AttachFileName nvarchar(max),
	@AttachId int
WITH EXECUTE AS CALLER
AS
BEGIN  
SET FMTONLY OFF;  

update MNT_AttachmentList set AttachFileName = @AttachFileName where AttachId =@AttachId
   
END

GO


