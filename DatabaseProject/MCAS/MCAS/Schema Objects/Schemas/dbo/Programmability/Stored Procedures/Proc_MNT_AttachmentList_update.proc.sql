CREATE PROCEDURE [dbo].[Proc_MNT_AttachmentList_update]
	@p_AttachLoc [nchar](2),
	@p_AttachEntId [int],
	@p_AttachFileName [nvarchar](255),
	@p_AttachDateTime [datetime],
	@p_AttachFileDesc [nvarchar](255),
	@p_AttachFileType [nvarchar](5),
	@p_AttachType [int],
	@p_FilePath [nvarchar](200),
	@p_CreatedBy [varchar](50),
	@p_CreatedDate [datetime],
	@p_ModifiedBy [varchar](50),
	@p_ModifiedDate [datetime],
	@p_ClaimID [int],
	@w_AttachId [int],
	@w_ClaimID [int]
WITH EXECUTE AS CALLER
AS
BEGIN

	UPDATE [dbo].MNT_AttachmentList SET AttachLoc=@p_AttachLoc,AttachEntId=@p_AttachEntId,AttachFileName=@p_AttachFileName,AttachDateTime=@p_AttachDateTime,AttachFileDesc=@p_AttachFileDesc,AttachFileType=@p_AttachFileType,AttachType=@p_AttachType,FilePath=@p_FilePath,CreatedBy=@p_CreatedBy,CreatedDate=@p_CreatedDate,ModifiedBy=@p_ModifiedBy,ModifiedDate=@p_ModifiedDate,ClaimID=@p_ClaimID 
	WHERE AttachId=@w_AttachId 


END


