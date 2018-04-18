
IF NOT EXISTS (SELECT 1 FROM [MNT_TableDesc] WHERE [TableName] = 'CLM_Mandate') 
BEGIN 

INSERT INTO MNT_TableDesc VALUES('CLM_Mandate','Mandate','Claims')

END