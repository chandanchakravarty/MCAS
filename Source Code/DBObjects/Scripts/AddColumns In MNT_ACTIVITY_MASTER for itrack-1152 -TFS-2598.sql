 
IF NOT EXISTS 
(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
      WHERE TABLE_NAME = 'MNT_ACTIVITY_MASTER' AND COLUMN_NAME = 'RUBRICA')
BEGIN
      ALTER TABLE MNT_ACTIVITY_MASTER  
      ADD  RUBRICA  NVARCHAR(5)
 END
 GO
 IF NOT EXISTS 
(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
      WHERE TABLE_NAME = 'MNT_ACTIVITY_MASTER' AND COLUMN_NAME = 'SUBMIT_ANYWAY')
BEGIN
      ALTER TABLE MNT_ACTIVITY_MASTER  
      ADD  SUBMIT_ANYWAY  BIT
 END
 GO
 IF NOT EXISTS 
(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
      WHERE TABLE_NAME = 'MNT_ACTIVITY_MASTER' AND COLUMN_NAME = 'EFFECTIVE_DATE')
BEGIN
      ALTER TABLE MNT_ACTIVITY_MASTER  
      ADD  EFFECTIVE_DATE  DATETIME
 END
 GO
  
 