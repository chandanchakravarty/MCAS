
IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_ACCOUNTS_POSTING_DETAILS' 
                        AND COLUMN_NAME = 'TRANSACTION_AMOUNT') 
                  BEGIN
                        ALTER TABLE ACT_ACCOUNTS_POSTING_DETAILS
                        ALTER COLUMN TRANSACTION_AMOUNT DECIMAL (25,4)
                        
                   END
                   
                   
 IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_ACCOUNTS_POSTING_DETAILS' 
                        AND COLUMN_NAME = 'AGENCY_COMM_PERC') 
                  BEGIN
                        ALTER TABLE ACT_ACCOUNTS_POSTING_DETAILS
                        ALTER COLUMN AGENCY_COMM_PERC DECIMAL (25,4)
                        
                   END
                   
                   
                      
 IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_ACCOUNTS_POSTING_DETAILS' 
                        AND COLUMN_NAME = 'AGENCY_COMM_AMT') 
                  BEGIN
                        ALTER TABLE ACT_ACCOUNTS_POSTING_DETAILS
                        ALTER COLUMN AGENCY_COMM_AMT DECIMAL (25,4)
                        
                   END
                   
                   

 IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_ACCOUNTS_POSTING_DETAILS' 
                        AND COLUMN_NAME = 'GROSS_AMOUNT') 
                  BEGIN
                        ALTER TABLE ACT_ACCOUNTS_POSTING_DETAILS
                        ALTER COLUMN GROSS_AMOUNT DECIMAL (25,4)
                        
                   END
                   
                   
  IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'BROUGHT_DOWN_AMOUNT') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN BROUGHT_DOWN_AMOUNT DECIMAL (25,4)
                        
                   END 
                   
                   
    IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'CURRENT_MTD_BALANCE') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN CURRENT_MTD_BALANCE DECIMAL (25,4)
                        
                   END  
                   
                   
     IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'CURRENT_YTD_BALANCE') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN CURRENT_YTD_BALANCE DECIMAL (25,4)
                        
                   END  
                   
                   
                   
   IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_JAN_MTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_JAN_MTD DECIMAL (25,4)
                        
                   END  
                   
                   
                   
    IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_JAN_YTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_JAN_YTD DECIMAL (25,4)
                        
                   END                 


IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_FEB_MTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_FEB_MTD DECIMAL (25,4)
                        
                   END 
                   
     
 IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_FEB_YTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_FEB_YTD DECIMAL (25,4)
                        
                   END
                   
                   
  IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_MAR_MTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_MAR_MTD DECIMAL (25,4)
                        
                   END
                   
                   
   IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_MAR_YTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_MAR_YTD DECIMAL (25,4)
                        
                   END
                   
                   
   IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_APR_MTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_APR_MTD DECIMAL (25,4)
                        
                   END
                   
                   
   IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_APR_YTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_APR_YTD DECIMAL (25,4)
                        
                   END
                   
                   
                   
     IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_MAY_MTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_MAY_MTD DECIMAL (25,4)
                        
                   END
                   
                   
         IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_MAY_YTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_MAY_YTD DECIMAL (25,4)
                        
                   END
                   
                   
        IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_JUN_MTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_JUN_MTD DECIMAL (25,4)
                        
                   END
                   
                   
       IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_JUN_YTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_JUN_YTD DECIMAL (25,4)
                        
                   END
                   
                   
          IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_JUL_MTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_JUL_MTD DECIMAL (25,4)
                        
                   END
                   
                   
       IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_JUL_YTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_JUL_YTD DECIMAL (25,4)
                        
                   END
                   
                   
                   
      IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_AUG_MTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_AUG_MTD DECIMAL (25,4)
                        
                   END
                   
                   
     IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_AUG_YTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_AUG_YTD DECIMAL (25,4)
                        
                   END
                   
                   
                   
      IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_SEP_MTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_SEP_MTD DECIMAL (25,4)
                        
                   END
                   
                   
       IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_SEP_YTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_SEP_YTD DECIMAL (25,4)
                        
                   END
                   
                   
       IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_OCT_MTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_OCT_MTD DECIMAL (25,4)
                        
                   END
                   
                   
      IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_OCT_YTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_OCT_YTD DECIMAL (25,4)
                        
                   END
                   
                   
                   
      IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_NOV_MTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_NOV_MTD DECIMAL (25,4)
                        
                   END
                   
                   
          IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_NOV_YTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_NOV_YTD DECIMAL (25,4)
                        
                   END
                   
                   
             IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_DEC_MTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_DEC_MTD DECIMAL (25,4)
                        
                   END
                   
                   
             IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'YEAR_DEC_YTD') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN YEAR_DEC_YTD DECIMAL (25,4)
                        
                   END
                   
                   
                   
       IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_GENERAL_LEDGER_TOTALS' 
                        AND COLUMN_NAME = 'CARRY_FWDED_AMOUNT') 
                  BEGIN
                        ALTER TABLE ACT_GENERAL_LEDGER_TOTALS
                        ALTER COLUMN CARRY_FWDED_AMOUNT DECIMAL (25,4)
                        
                   END
                   
                   
              