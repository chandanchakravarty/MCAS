IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
  WHERE TABLE_NAME = 'POL_REINSURANCE_INFO' 
        AND COLUMN_NAME ='COMM_AMOUNT') 
  BEGIN
        ALTER TABLE POL_REINSURANCE_INFO
        ADD COMM_AMOUNT DECIMAL(18,2)
   END
   
   
  IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
  WHERE TABLE_NAME = 'POL_REINSURANCE_INFO' 
        AND COLUMN_NAME ='LAYER_AMOUNT') 
  BEGIN
        ALTER TABLE POL_REINSURANCE_INFO
        ADD LAYER_AMOUNT DECIMAL(18,2)
   END
   
   
  IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
  WHERE TABLE_NAME = 'POL_REINSURANCE_INFO' 
        AND COLUMN_NAME ='REIN_PREMIUM') 
  BEGIN
        ALTER TABLE POL_REINSURANCE_INFO
        ADD REIN_PREMIUM DECIMAL(18,2)
   END
   
