
IF EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'ClaimAccidentDetails' AND [COLUMN_NAME] = 'ODAssignmentTranId')
BEGIN
  ALTER TABLE ClaimAccidentDetails
    ALTER COLUMN ODAssignmentTranId nvarchar(200)
END

IF EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'ClaimAccidentDetails' AND [COLUMN_NAME] = 'TPAssignmentTranId')
BEGIN
  ALTER TABLE ClaimAccidentDetails
    ALTER COLUMN TPAssignmentTranId nvarchar(200)
END

IF EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'ClaimAccidentDetails' AND [COLUMN_NAME] = 'ClaimNo')
BEGIN
  ALTER TABLE ClaimAccidentDetails
    ALTER COLUMN ClaimNo nvarchar(100) null
END

IF EXISTS (SELECT * 
  FROM sys.foreign_keys 
   WHERE object_id = OBJECT_ID(N'dbo.FK__CLM_Claim__Adjus__0E591826')
   AND parent_object_id = OBJECT_ID(N'dbo.CLM_Claims')
)
BEGIN
ALTER TABLE [dbo].[CLM_Claims]
DROP CONSTRAINT FK__CLM_Claim__Adjus__0E591826
END