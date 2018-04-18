CREATE PROCEDURE [dbo].[Proc_MIG_IL_VEHICLE_DETAIL_Update]
	@w_FileName [nvarchar](200)
WITH EXECUTE AS CALLER
AS
BEGIN
SET FMTONLY OFF;
	UPDATE [dbo].MIG_IL_VEHICLE_DETAIL SET UPLOAD_FILE_ID=(select UploadFileId from MNT_VehicleListingUpload where UploadFileName = @w_FileName
) 
	WHERE FileName=@w_FileName
END


