IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'TODODIARYLIST' and column_name = 'ReassignedDiary') 
BEGIN 
ALTER TABLE TODODIARYLIST
ADD ReassignedDiary nvarchar(4) NOT NULL DEFAULT('No')
END 

IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'TODODIARYLIST' and column_name = 'ReassignedDiaryDate') 
BEGIN 
ALTER TABLE TODODIARYLIST
ADD ReassignedDiaryDate datetime NULL 
END 

