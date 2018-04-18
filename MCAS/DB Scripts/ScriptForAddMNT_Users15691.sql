

EXEC sp_rename 'MNT_Users.FAL_OD', 'PaymentLimit', 'COLUMN'

EXEC sp_rename 'MNT_Users.FAL_PDBI', 'CreditNoteLimit', 'COLUMN'


IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Users' and column_name = 'FAL_OD') 
BEGIN 
ALTER TABLE MNT_Users
ADD FAL_OD int NULL
END

IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_Users' and column_name = 'FAL_PDBI') 
BEGIN 
ALTER TABLE MNT_Users
ADD FAL_PDBI int NULL
END