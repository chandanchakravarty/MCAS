
                   
IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'CLM_PROCESS_LOG' 
                        AND COLUMN_NAME ='BRANCH_CODE') 
                  BEGIN
                        ALTER TABLE CLM_PROCESS_LOG
                        ADD BRANCH_CODE VARCHAR(8) null
                   END
               
EXEC   sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'CLM_PROCESS_LOG', 'column', BRANCH_CODE
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'CLM_PROCESS_LOG', 'column', BRANCH_CODE
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'CLM_PROCESS_LOG', 'column', BRANCH_CODE

 
               