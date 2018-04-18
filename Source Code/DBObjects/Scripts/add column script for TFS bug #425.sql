IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'POL_CUSTOMER_POLICY_LIST' 
                        AND COLUMN_NAME ='SUSEP_LOB_CODE') 
                  BEGIN
                        ALTER TABLE POL_CUSTOMER_POLICY_LIST
                        ADD SUSEP_LOB_CODE NVARCHAR(20)
                   END
                   
                   
                   
           