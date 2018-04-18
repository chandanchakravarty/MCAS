IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'MIG_IL_LAYOUT_COLUMNS' 
                        AND COLUMN_NAME ='SUSEP_LOB_CODE') 
                  BEGIN
                        ALTER TABLE MIG_IL_LAYOUT_COLUMNS
                        ADD SUSEP_LOB_CODE NVARCHAR(20)
                   END