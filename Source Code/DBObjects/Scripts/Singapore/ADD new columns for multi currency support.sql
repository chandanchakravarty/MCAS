BEGIN TRAN
IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'POL_CUSTOMER_POLICY_LIST' 
                        AND COLUMN_NAME ='BILLING_CURRENCY') 
                  BEGIN
                        ALTER TABLE POL_CUSTOMER_POLICY_LIST
                        ADD BILLING_CURRENCY INT
                   END

--for risk tables

IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'POL_PERILS' 
                        AND COLUMN_NAME ='RISK_CURRENCY') 
                  BEGIN
                        ALTER TABLE POL_PERILS
                        ADD RISK_CURRENCY INT
                   END
                   
                   
IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'POL_PRODUCT_LOCATION_INFO' 
                        AND COLUMN_NAME ='RISK_CURRENCY') 
                  BEGIN
                        ALTER TABLE POL_PRODUCT_LOCATION_INFO
                        ADD RISK_CURRENCY INT
                   END
                   
                   
IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'POL_MARITIME' 
                        AND COLUMN_NAME ='RISK_CURRENCY') 
                  BEGIN
                        ALTER TABLE POL_MARITIME
                        ADD RISK_CURRENCY INT
                   END
                   
                   
IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'POL_PERSONAL_ACCIDENT_INFO' 
                        AND COLUMN_NAME ='RISK_CURRENCY') 
                  BEGIN
                        ALTER TABLE POL_PERSONAL_ACCIDENT_INFO
                        ADD RISK_CURRENCY INT
                   END
                   
                   
IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'POL_CIVIL_TRANSPORT_VEHICLES' 
                        AND COLUMN_NAME ='RISK_CURRENCY') 
                  BEGIN
                        ALTER TABLE POL_CIVIL_TRANSPORT_VEHICLES
                        ADD RISK_CURRENCY INT
                   END
                   
                   
IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'POL_COMMODITY_INFO' 
                        AND COLUMN_NAME ='RISK_CURRENCY') 
                  BEGIN
                        ALTER TABLE POL_COMMODITY_INFO
                        ADD RISK_CURRENCY INT
                   END
                   
                   
IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'POL_PASSENGERS_PERSONAL_ACCIDENT_INFO' 
                        AND COLUMN_NAME ='RISK_CURRENCY') 
                  BEGIN
                        ALTER TABLE POL_PASSENGERS_PERSONAL_ACCIDENT_INFO
                        ADD RISK_CURRENCY INT
                   END  
                   
                   
IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'POL_PENHOR_RURAL_INFO' 
                        AND COLUMN_NAME ='RISK_CURRENCY') 
                  BEGIN
                        ALTER TABLE POL_PENHOR_RURAL_INFO
                        ADD RISK_CURRENCY INT
                   END                    


-----for coverage table

IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'POL_PRODUCT_COVERAGES' 
                        AND COLUMN_NAME ='CURRENCY_FACTOR') 
                  BEGIN
                        ALTER TABLE POL_PRODUCT_COVERAGES
                        ADD CURRENCY_FACTOR DECIMAL(8,2)
                   END  


IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'POL_PRODUCT_COVERAGES' 
                        AND COLUMN_NAME ='BASE_CURRENCY_AMOUNT') 
                  BEGIN
                        ALTER TABLE POL_PRODUCT_COVERAGES
                        ADD BASE_CURRENCY_AMOUNT DECIMAL(25,2)
                   END  
                   go
sp_help POL_PRODUCT_COVERAGES

ROLLBACK TRAN