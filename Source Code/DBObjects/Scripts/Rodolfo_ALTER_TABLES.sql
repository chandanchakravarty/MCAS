

---1
IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'Rodolfo_spPolicy' 
                        AND COLUMN_NAME = 'rod_cd_susepLOB') 
                  BEGIN
                        ALTER TABLE Rodolfo_spPolicy
                        ALTER COLUMN rod_cd_susepLOB varchar(20)

                        
                   END
                   
 --------------------------------------------------------------------------------------------------------------                  
 
 
 IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'Rodolfo_spPolicy' 
                        AND COLUMN_NAME = 'rod_id_policy') 
                  BEGIN
                        ALTER TABLE Rodolfo_spPolicy
                        ALTER COLUMN rod_id_policy varchar(30)


                        
                   END
                   
  --------------------------------------------------------------------------------------------------------------                  
 
 
 IF  NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'Rodolfo_spPolicy' 
                        AND COLUMN_NAME = 'rod_cd_endorsement') 
                  BEGIN
                        ALTER TABLE Rodolfo_spPolicy
                        ADD  rod_cd_endorsement varchar(6)

                        
                   END
                   
      
 ----------------------------------------------------------------------------------------------------------    
 --2
 IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'Rodolfo_spInstallments' 
                        AND COLUMN_NAME = 'rod_cd_susepLOB') 
                  BEGIN
                        ALTER TABLE Rodolfo_spInstallments
                        ALTER COLUMN rod_cd_susepLOB varchar(20)

                        
                   END
                   
 --------------------------------------------------------------------------------------------------------------                  
 
 
 IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'Rodolfo_spInstallments' 
                        AND COLUMN_NAME = 'rod_id_policy') 
                  BEGIN
                        ALTER TABLE Rodolfo_spInstallments
                        ALTER COLUMN rod_id_policy varchar(30)


                        
                   END
                   
  --------------------------------------------------------------------------------------------------------------                  
 
 
 IF  NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'Rodolfo_spInstallments' 
                        AND COLUMN_NAME = 'rod_cd_endorsement') 
                  BEGIN
                        ALTER TABLE Rodolfo_spInstallments
                        ADD  rod_cd_endorsement varchar(6)

                        
                   END
                   
      
 ----------------------------------------------------------------------------------------------------------   
--3 
  IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'Rodolfo_vwBroker' 
                        AND COLUMN_NAME = 'rod_cd_susepLOB') 
                  BEGIN
                        ALTER TABLE Rodolfo_vwBroker
                        ALTER COLUMN rod_cd_susepLOB varchar(20)

                        
                   END
                   
 --------------------------------------------------------------------------------------------------------------                  
 
 
 IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'Rodolfo_vwBroker' 
                        AND COLUMN_NAME = 'rod_id_policy') 
                  BEGIN
                        ALTER TABLE Rodolfo_vwBroker
                        ALTER COLUMN rod_id_policy varchar(30)


                        
                   END
                   
  --------------------------------------------------------------------------------------------------------------                  
 
 
 IF   EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'Rodolfo_vwBroker' 
                        AND COLUMN_NAME = 'rod_cd_endorsement') 
                  BEGIN
                        ALTER TABLE Rodolfo_vwBroker
                       ALTER COLUMN rod_cd_endorsement varchar(6)

                        
                   END
 ---------------------------------------------------------------------------------------------------------------
 
  IF   EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'Rodolfo_vwBroker' 
                        AND COLUMN_NAME = 'rod_de_brokerDescription') 
                  BEGIN
                        ALTER TABLE Rodolfo_vwBroker
                       ALTER COLUMN rod_de_brokerDescription varchar(100)

                        
                   END
                   
                   
    ----------------------------------------------------------------------------------------------------------    
 --4
 IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'Rodolfo_spBrokerInstallments' 
                        AND COLUMN_NAME = 'rod_cd_susepLOB') 
                  BEGIN
                        ALTER TABLE Rodolfo_spBrokerInstallments
                        ALTER COLUMN rod_cd_susepLOB varchar(20)

                        
                   END
                   
 --------------------------------------------------------------------------------------------------------------                  
 
 
 IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'Rodolfo_spBrokerInstallments' 
                        AND COLUMN_NAME = 'rod_id_policy') 
                  BEGIN
                        ALTER TABLE Rodolfo_spBrokerInstallments
                        ALTER COLUMN rod_id_policy varchar(30)


                        
                   END
                   
  --------------------------------------------------------------------------------------------------------------                  
 
 
 IF  NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'Rodolfo_spBrokerInstallments' 
                        AND COLUMN_NAME = 'rod_cd_endorsement') 
                  BEGIN
                        ALTER TABLE Rodolfo_spBrokerInstallments
                        ADD  rod_cd_endorsement varchar(6)

                        
                   END
 
  -------------------------------------------------------------------------------------------------------------   
  
  --5
 IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'Rodolfo_spClaim' 
                        AND COLUMN_NAME = 'rod_cd_susepLOB') 
                  BEGIN
                        ALTER TABLE Rodolfo_spClaim
                        ALTER COLUMN rod_cd_susepLOB varchar(20)

                        
                   END
                   
 --------------------------------------------------------------------------------------------------------------                  
 
 
 IF EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'Rodolfo_spClaim' 
                        AND COLUMN_NAME = 'rod_id_policy') 
                  BEGIN
                        ALTER TABLE Rodolfo_spClaim
                        ALTER COLUMN rod_id_policy varchar(30)


                        
                   END
                   
  --------------------------------------------------------------------------------------------------------------                  
 
 
 IF   EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'Rodolfo_spClaim' 
                        AND COLUMN_NAME = 'rod_cd_endorsement') 
                  BEGIN
                        ALTER TABLE Rodolfo_spClaim
                       ALTER COLUMN  rod_cd_endorsement varchar(6)

                        
                   END
 
  -------------------------------------------------------------------------------------------------------------          
 IF   EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'Rodolfo_spClaim' 
                        AND COLUMN_NAME = 'rod_id_claim') 
                  BEGIN
                        ALTER TABLE Rodolfo_spClaim
                       ALTER COLUMN  rod_id_claim varchar(10)

                        
                   END        
                   
   --------------------------------------------------------------------------------------------------------------                  
 --6
 
 IF   EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'Rodolfo_spClaimDetails' 
                        AND COLUMN_NAME = 'rod_cd_endorsement') 
                  BEGIN
                        ALTER TABLE Rodolfo_spClaimDetails
                       ALTER COLUMN  rod_cd_endorsement varchar(6)

                        
                   END
 
  -------------------------------------------------------------------------------------------------------------          
 IF   EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'Rodolfo_spClaimDetails' 
                        AND COLUMN_NAME = 'rod_id_claim') 
                  BEGIN
                        ALTER TABLE Rodolfo_spClaimDetails
                       ALTER COLUMN  rod_id_claim varchar(10)

                        
                   END                
         