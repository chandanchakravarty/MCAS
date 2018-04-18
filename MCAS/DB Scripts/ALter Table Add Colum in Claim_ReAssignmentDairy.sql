IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'Claim_ReAssignmentDairy' and column_name = 'ClaimId') 

BEGIN 

ALTER TABLE Claim_ReAssignmentDairy 

add ClaimId int 

END 


IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'Claim_ReAssignmentDairy' and column_name = 'AccidentClaimId') 

BEGIN 

ALTER TABLE Claim_ReAssignmentDairy 

add AccidentClaimId int null

END 


IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'TODODIARYLIST' and column_name = 'MovementType') 

BEGIN 

ALTER TABLE TODODIARYLIST 

add MovementType nvarchar(50) null

END 

IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'TODODIARYLIST' and column_name = 'ParentId') 

BEGIN 

ALTER TABLE TODODIARYLIST 

add ParentId int null

END 


IF not EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'TODODIARYLIST' and column_name = 'IsActive') 

BEGIN 

ALTER TABLE TODODIARYLIST 

add IsActive char(1) null

END 