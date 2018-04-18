CREATE PROCEDURE [dbo].[PROC_MIG_IL_VEHICLE_COMPLETE_IMPORT_REQUEST]
	@UPLOAD_FILE_ID [int]
WITH EXECUTE AS CALLER
AS
BEGIN  
declare @SuccessedRecords INT
declare @FailedRecords INT 
select @SuccessedRecords=COUNT(UPLOAD_FILE_ID) from MIG_IL_VEHICLE_DETAIL  where UPLOAD_FILE_ID= @UPLOAD_FILE_ID
select @FailedRecords=(TotalRecords-@SuccessedRecords) from  MNT_VEHICLELISTINGUPLOAD where UploadFileId= @UPLOAD_FILE_ID
                 
UPDATE MNT_VEHICLELISTINGUPLOAD SET [STATUS]='COMP',IS_PROCESSED='Y',PROCESSED_DATE=GETDATE(), UplodedSuccess=@SuccessedRecords,
UploadedFailed=@FailedRecords
WHERE UploadFileId=@UPLOAD_FILE_ID
END


