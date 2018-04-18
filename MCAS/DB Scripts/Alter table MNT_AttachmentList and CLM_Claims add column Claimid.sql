IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_AttachmentList' and column_name = 'ClaimID') 

BEGIN 

ALTER TABLE MNT_AttachmentList 

add ClaimID int 

END 

IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'ClaimID') 

BEGIN 

ALTER TABLE CLM_Claims 

add ClaimID int 

END 
