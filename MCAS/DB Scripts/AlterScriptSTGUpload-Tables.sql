
IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'STG_UploadedFileData' and column_name = 'HistoryIds')
     BEGIN
         alter Table STG_UploadedFileData alter column HistoryIds varchar(50)
     END

IF Not EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_FileUpload' and column_name = 'LastModifiedDateTime')
     BEGIN
         alter Table MNT_FileUpload add LastModifiedDateTime datetime
     END

IF Not EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_FileUpload' and column_name = 'ModifiedBy')
     BEGIN
         alter Table MNT_FileUpload add ModifiedBy nvarchar(30)
     END
     
          