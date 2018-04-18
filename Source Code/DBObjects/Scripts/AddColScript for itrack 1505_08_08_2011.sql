IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_BANK_INFORMATION' 
                        AND COLUMN_NAME ='BANK_TYPE') 
                  BEGIN
                        ALTER TABLE ACT_BANK_INFORMATION
                        ADD BANK_TYPE INT
                   END
                   
       