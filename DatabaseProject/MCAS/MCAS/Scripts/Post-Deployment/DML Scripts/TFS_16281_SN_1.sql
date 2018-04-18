 IF NOT EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'Status')
     BEGIN
         ALTER TABLE ClaimAccidentDetails ADD  Status NVARCHAR(50)
     END