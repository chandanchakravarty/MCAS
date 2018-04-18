IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'STG_TAC_IP' and column_name = 'InitialLiability')
BEGIN
    ALTER TABLE STG_TAC_IP ADD InitialLiability nvarchar(20)
END

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'STG_TAC_IP' and column_name = 'CollisionType')
BEGIN
    ALTER TABLE STG_TAC_IP ADD CollisionType nvarchar(10)
END

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'STG_UploadedFileData' and column_name = 'InitialLiability')
BEGIN
    ALTER TABLE STG_UploadedFileData  ADD InitialLiability nvarchar(20)
END

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'STG_UploadedFileData' and column_name = 'CollisionType')
BEGIN
    ALTER TABLE STG_UploadedFileData ADD CollisionType nvarchar(10)
END


IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'STG_CLAIM_FILE_DATA' and column_name = 'InitialLiability')
BEGIN
    ALTER TABLE STG_CLAIM_FILE_DATA ADD InitialLiability nvarchar(20)
END

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'STG_CLAIM_FILE_DATA' and column_name = 'CollisionType')
BEGIN
    ALTER TABLE STG_CLAIM_FILE_DATA ADD CollisionType nvarchar(10)
END

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'InitialLiability')
BEGIN
    ALTER TABLE ClaimAccidentDetails ADD InitialLiability nvarchar(20)
END

IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'CollisionType')
BEGIN
    ALTER TABLE ClaimAccidentDetails ADD CollisionType nvarchar(500)
END






