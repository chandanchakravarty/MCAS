IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'CLM_AUTHORITY_LIMIT' 
                        AND COLUMN_NAME ='REOPEN_CLAIM_LIMIT') 
BEGIN 
    ALTER TABLE CLM_AUTHORITY_LIMIT
     ADD REOPEN_CLAIM_LIMIT decimal(18,2)
END

EXEC   sp_addextendedproperty 'Caption', 'REOPEN CLAIM LIMIT', 'user', dbo, 'table', 'CLM_AUTHORITY_LIMIT', 'column',REOPEN_CLAIM_LIMIT                   
EXEC   sp_addextendedproperty 'Description', 'REOPEN CLAIM LIMIT', 'user', dbo, 'table', 'CLM_AUTHORITY_LIMIT', 'column',REOPEN_CLAIM_LIMIT
--EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'CLM_ADJUSTER', 'column',SUB_ADJUSTER_GST

GO

IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'CLM_AUTHORITY_LIMIT' 
                        AND COLUMN_NAME ='GRATIA_CLAIM_AMOUNT') 
BEGIN 
    ALTER TABLE CLM_AUTHORITY_LIMIT
     ADD GRATIA_CLAIM_AMOUNT decimal(18,2)
END


EXEC   sp_addextendedproperty 'Caption', 'GRATIA CLAIM AMOUNT', 'user', dbo, 'table', 'CLM_AUTHORITY_LIMIT', 'column',GRATIA_CLAIM_AMOUNT                   
EXEC   sp_addextendedproperty 'Description', 'GRATIA CLAIM AMOUNT', 'user', dbo, 'table', 'CLM_AUTHORITY_LIMIT', 'column',GRATIA_CLAIM_AMOUNT
--EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'CLM_ADJUSTER', 'column',SUB_ADJUSTER_GST
                   
