 
 IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'POL_POLICY_PROCESS' 
                        AND COLUMN_NAME = 'RETURN_PREMIUM') 
                  BEGIN
                        ALTER TABLE POL_POLICY_PROCESS
                        ALTER COLUMN RETURN_PREMIUM DECIMAL (25,2)
                        
                   END
                   
                   
 IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'POL_POLICY_PROCESS' 
                        AND COLUMN_NAME = 'PAST_DUE_PREMIUM') 
                  BEGIN
                        ALTER TABLE POL_POLICY_PROCESS
                        ALTER COLUMN PAST_DUE_PREMIUM DECIMAL (25,2)
                        
                   END
                   
  IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'POL_POLICY_PROCESS' 
                        AND COLUMN_NAME = 'WRITTEN_OFF_PREMIUM') 
                  BEGIN
                        ALTER TABLE POL_POLICY_PROCESS
                        ALTER COLUMN WRITTEN_OFF_PREMIUM DECIMAL (25,2)
                        
                   END
                   
   IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_CUSTOMER_OPEN_ITEMS' 
                        AND COLUMN_NAME = 'NET_PREMIUM') 
                  BEGIN
                        ALTER TABLE ACT_CUSTOMER_OPEN_ITEMS
                        ALTER COLUMN NET_PREMIUM DECIMAL (25,2)
                        
                   END
                   
   IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_AGENCY_OPEN_ITEMS' 
                        AND COLUMN_NAME = 'NET_PREMIUM') 
                  BEGIN
                        ALTER TABLE ACT_AGENCY_OPEN_ITEMS
                        ALTER COLUMN NET_PREMIUM DECIMAL (25,2)
                        
                   END
                   
     IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_COI_OPEN_ITEMS' 
                        AND COLUMN_NAME = 'NET_PREMIUM') 
                  BEGIN
                        ALTER TABLE ACT_COI_OPEN_ITEMS
                        ALTER COLUMN NET_PREMIUM DECIMAL (25,2)
                        
                   END
                   
                   
  IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_COI_OPEN_ITEMS' 
                        AND COLUMN_NAME = 'NET_PREMIUM') 
                  BEGIN
                        ALTER TABLE ACT_COI_OPEN_ITEMS
                        ALTER COLUMN NET_PREMIUM DECIMAL (25,2)
                        
                   END
                   
                   
 IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_AGENCY_STATEMENT' 
                        AND COLUMN_NAME = 'PREMIUM_AMOUNT') 
                  BEGIN
                        ALTER TABLE ACT_AGENCY_STATEMENT
                        ALTER COLUMN PREMIUM_AMOUNT DECIMAL (25,2)
                        
                   END
                   
                   
   IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_AGENCY_STATEMENT_DETAILED' 
                        AND COLUMN_NAME = 'PREMIUM_AMOUNT') 
                  BEGIN
                        ALTER TABLE ACT_AGENCY_STATEMENT_DETAILED
                        ALTER COLUMN PREMIUM_AMOUNT DECIMAL (25,2)
                        
                   END
                   
                   
   IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_COI_STATEMENT' 
                        AND COLUMN_NAME = 'PREMIUM_AMOUNT') 
                  BEGIN
                        ALTER TABLE ACT_COI_STATEMENT
                        ALTER COLUMN PREMIUM_AMOUNT DECIMAL (25,2)
                        
                   END
                   
                   
     IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_COI_STATEMENT' 
                        AND COLUMN_NAME = 'GROSS_PREMIUM') 
                  BEGIN
                        ALTER TABLE ACT_COI_STATEMENT
                        ALTER COLUMN GROSS_PREMIUM DECIMAL (25,2)
                        
                   END
                   
                   
     IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_COI_STATEMENT_DETAILED' 
                        AND COLUMN_NAME = 'PREMIUM_AMOUNT') 
                  BEGIN
                        ALTER TABLE ACT_COI_STATEMENT_DETAILED
                        ALTER COLUMN PREMIUM_AMOUNT DECIMAL (25,2)
                        
                   END
                   
                  
     IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_COI_STATEMENT_DETAILED' 
                        AND COLUMN_NAME = 'GROSS_PREMIUM') 
                  BEGIN
                        ALTER TABLE ACT_COI_STATEMENT_DETAILED
                        ALTER COLUMN GROSS_PREMIUM DECIMAL (25,2)
                        
                   END
                   
     IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_POLICY_CO_BILLING_DETAILS' 
                        AND COLUMN_NAME = 'TRAN_PREMIUM_AMOUNT') 
                  BEGIN
                        ALTER TABLE ACT_POLICY_CO_BILLING_DETAILS
                        ALTER COLUMN TRAN_PREMIUM_AMOUNT DECIMAL (25,2)
                        
                   END
                   
                   
     IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_CUSTOMER_BALANCE_INFORMATION' 
                        AND COLUMN_NAME = 'TOTAL_PREMIUM_DUE') 
                  BEGIN
                        ALTER TABLE ACT_CUSTOMER_BALANCE_INFORMATION
                        ALTER COLUMN TOTAL_PREMIUM_DUE DECIMAL (25,2)
                        
                   END
                   
       
     IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_PREMIUM_PROCESS_SUB_DETAILS' 
                        AND COLUMN_NAME = 'NET_PREMIUM') 
                  BEGIN
                        ALTER TABLE ACT_PREMIUM_PROCESS_SUB_DETAILS
                        ALTER COLUMN NET_PREMIUM DECIMAL (25,2)
                        
                   END
                   
                   
   IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_PREMIUM_PROCESS_SUB_DETAILS' 
                        AND COLUMN_NAME = 'GROSS_PREMIUM') 
                  BEGIN
                        ALTER TABLE ACT_PREMIUM_PROCESS_SUB_DETAILS
                        ALTER COLUMN GROSS_PREMIUM DECIMAL (25,2)
                        
                   END
                   
                   
   IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_PREMIUM_PROCESS_SUB_DETAILS' 
                        AND COLUMN_NAME = 'INFORCE_PREMIUM') 
                  BEGIN
                        ALTER TABLE ACT_PREMIUM_PROCESS_SUB_DETAILS
                        ALTER COLUMN INFORCE_PREMIUM DECIMAL (25,2)
                        
                   END
                   
                   
    IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_PREMIUM_PROCESS_SUB_DETAILS' 
                        AND COLUMN_NAME = 'AJUSTED_PREMIUM') 
                  BEGIN
                        ALTER TABLE ACT_PREMIUM_PROCESS_SUB_DETAILS
                        ALTER COLUMN AJUSTED_PREMIUM DECIMAL (25,2)
                        
                   END
                   
                   
    IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_EARNED_PREMIUM' 
                        AND COLUMN_NAME = 'INFORCE_PREMIUM') 
                  BEGIN
                        ALTER TABLE ACT_EARNED_PREMIUM
                        ALTER COLUMN INFORCE_PREMIUM DECIMAL (25,6)
                        
                   END
                   
                   
    IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_EARNED_PREMIUM' 
                        AND COLUMN_NAME = 'WRITTEN_PREMIUM') 
                  BEGIN
                        ALTER TABLE ACT_EARNED_PREMIUM
                        ALTER COLUMN WRITTEN_PREMIUM DECIMAL (25,6)
                        
                   END
                   
                   
    IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_EARNED_PREMIUM' 
                        AND COLUMN_NAME = 'EARNED_PREMIUM') 
                  BEGIN
                        ALTER TABLE ACT_EARNED_PREMIUM
                        ALTER COLUMN EARNED_PREMIUM DECIMAL (25,2)
                        
                   END
                   
                   
    IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_PREMIUM_PROCESS_DETAILS' 
                        AND COLUMN_NAME = 'PREMIUM_AMOUNT') 
                  BEGIN
                        ALTER TABLE ACT_PREMIUM_PROCESS_DETAILS
                        ALTER COLUMN PREMIUM_AMOUNT DECIMAL (25,2)
                        
                   END
                   
    
                  
                   
 IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'POL_REINSURANCE_BREAKDOWN_DETAILS' 
                        AND COLUMN_NAME = 'TRAN_PREMIUM') 
                  BEGIN
                        ALTER TABLE POL_REINSURANCE_BREAKDOWN_DETAILS
                        ALTER COLUMN TRAN_PREMIUM NUMERIC (25,2)
                        
                   END
                   
                   
   IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'POL_REINSURANCE_BREAKDOWN_DETAILS' 
                        AND COLUMN_NAME = 'EARNED') 
                  BEGIN
                        ALTER TABLE POL_REINSURANCE_BREAKDOWN_DETAILS
                        ALTER COLUMN EARNED NUMERIC (25,2)
                        
                   END
                   
                   
     IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'POL_REINSURANCE_BREAKDOWN_DETAILS' 
                        AND COLUMN_NAME = 'WRITTEN') 
                  BEGIN
                        ALTER TABLE POL_REINSURANCE_BREAKDOWN_DETAILS
                        ALTER COLUMN WRITTEN Numeric(25,2)
                        
                   END
                   
                   
        IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'POL_REINSURANCE_BREAKDOWN_DETAILS' 
                        AND COLUMN_NAME = 'REIN_PREMIUM') 
                  BEGIN
                        ALTER TABLE POL_REINSURANCE_BREAKDOWN_DETAILS
                        ALTER COLUMN REIN_PREMIUM Numeric(25,2)
                        
                   END
                   
      IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'POL_REINSURANCE_BREAKDOWN_DETAILS' 
                        AND COLUMN_NAME = 'COMM_AMOUNT') 
                  BEGIN
                        ALTER TABLE POL_REINSURANCE_BREAKDOWN_DETAILS
                        ALTER COLUMN COMM_AMOUNT numeric(18,2)
                        
                   END
                   
                   
                   
   IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_INSTALL_PLAN_DETAIL' 
                        AND COLUMN_NAME = 'LATE_FEES') 
                  BEGIN
                        ALTER TABLE ACT_INSTALL_PLAN_DETAIL
                        ALTER COLUMN LATE_FEES DECIMAL (25,4)
                        
                   END
                   
                   
          IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_INSTALL_PLAN_DETAIL' 
                        AND COLUMN_NAME = 'SERVICE_CHARGE') 
                  BEGIN
                        ALTER TABLE ACT_INSTALL_PLAN_DETAIL
                        ALTER COLUMN SERVICE_CHARGE DECIMAL (25,4)
                        
                   END
                   
                   
           IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_INSTALL_PLAN_DETAIL' 
                        AND COLUMN_NAME = 'CONVENIENCE_FEES') 
                  BEGIN
                        ALTER TABLE ACT_INSTALL_PLAN_DETAIL
                        ALTER COLUMN CONVENIENCE_FEES DECIMAL (25,4)
                        
                   END
                   
                   
           IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'ACT_INSTALL_PLAN_DETAIL' 
                        AND COLUMN_NAME = 'INTREST_RATES') 
                  BEGIN
                        ALTER TABLE ACT_INSTALL_PLAN_DETAIL
                        ALTER COLUMN INTREST_RATES DECIMAL (25,4)
                        
                   END
                   
                   
                  