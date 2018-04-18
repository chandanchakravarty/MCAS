IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'ClaimAccidentDetails' and column_name = 'IsRecoveryBI') 
BEGIN 
ALTER TABLE ClaimAccidentDetails 
ADD IsRecoveryBI varchar(1) 
END

EXEC sp_RENAME 'ClaimAccidentDetails.IsRecovery' , 'IsRecoveryOD', 'COLUMN'