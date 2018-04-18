IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_REPROCESS_PAGNET_EXPORT_RECORD]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_REPROCESS_PAGNET_EXPORT_RECORD]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

    
/*----------------------------------------------------------                                      
Proc Name    : dbo.PROC_REPROCESS_PAGNET_EXPORT_RECORD                                    
Created by   : Shubhanshu Pandey                   
Date         : 02-05-2010                          
Purpose      :                
                                      
 ----------------------------------------------------------                                                  
Date     Review By          Comments                                                
 drop proc dbo.PROC_REPROCESS_PAGNET_EXPORT_RECORD 8                       
----   ------------       -------------------------*/                                     
CREATE PROC [dbo].[PROC_REPROCESS_PAGNET_EXPORT_RECORD]        
(         
 @FILE_ID INT=NULL,
 @FLAG_REPROCESS INT = NULL      --0 FOR ERR AND 1 FOR APR    
)        
AS        
BEGIN                      
                   
     DECLARE @COUNT INT, @COUNTER INT =0,   ---DECLARE VARIABLES                      
     @INTERFACE_CODE INT = NULL,                        
     @PAYMENT_ROW_ID VARCHAR(25) = NULL,                        
     @CLAIM_ID INT=NULL,                        
     @ACTIVITY_ID INT = NULL,                        
     @COMPANY_ID INT =NULL,                         
     @COVERAGE_ID INT =NULL,                        
     @EVENT_CODE VARCHAR(5)=NULL,                        
     @FILE_ID1 INT =NULL                 
                   
                      
                      
 IF (@FLAG_REPROCESS = 0)              
  BEGIN                    
  CREATE TABLE #TEMP     ---CREATE A TEMPORARY TABLE                      
  (                        
  ROW_ID INT IDENTITY(1,1),                        
  [FILE_ID] INT,                        
  PAYMENT_ID VARCHAR(25),                        
  INTERFACE_CODE INT,                        
  EVENT_CODE VARCHAR(5)                        
  )                        
                          
  INSERT INTO #TEMP                        
  (                        
  [FILE_ID],                        
  PAYMENT_ID ,                        
  INTERFACE_CODE,                        
  EVENT_CODE                          
  )                        
  SELECT                         
   PER.[FILE_ID],                        
   PER.PAYMENT_ID,                        
   PER.INTERFACE_CODE,                        
   PER.EVENT_CODE                         
   FROM  PAGNET_EXPORT_RECORD  PER WITH (NOLOCK)                       
   WHERE                       
   PER.[FILE_ID] = @FILE_ID AND                       
  RETURN_STATUS = 'Re-sent to Pagnet' AND              
 -- RETURN_STATUS = 'Re-enviado para o Pagnet' AND              
                       
   PROCESSED = 'N'                       
                         
                        
                           
                           
   ---UPDATION FOR PARENT TABLES                      
                            
     SELECT @COUNT = COUNT(*) FROM #TEMP                        
                             
     SET @COUNTER =1                         
                        
     WHILE (@COUNT > 0)                         
     BEGIN                        
        SELECT  @INTERFACE_CODE = CAST(INTERFACE_CODE AS INT ),                        
         @PAYMENT_ROW_ID = PAYMENT_ID,                        
         @EVENT_CODE = EVENT_CODE  FROM #TEMP WHERE ROW_ID = @COUNTER                        
                                   
       IF(@INTERFACE_CODE = 4)  --UPDATE CUSTOMER REFUND              
       BEGIN                        
        --BEGIN                        
        -- UPDATE ACT_CUSTOMER_OPEN_ITEMS                        
        -- SET IS_COMMISSION_PROCESS ='Y'                        
        -- WHERE IDEN_ROW_ID = CAST(@PAYMENT_ROW_ID AS INT)                          
        --END              
        IF(@EVENT_CODE = '01020' OR @EVENT_CODE  = '01060')              
        BEGIN     
         UPDATE ACT_POLICY_INSTALLMENT_DETAILS              
         SET IS_COMMISSION_PROCESS ='Y'              
         WHERE ROW_ID = CAST(@PAYMENT_ROW_ID AS INT)              
        END    
        --Added by naveen , itrack 1750 ,dec 07,2011, boleto repaid case.  
        IF(@EVENT_CODE  = '01060')              
        BEGIN     
           UPDATE ACT_CUSTOMER_OPEN_ITEMS              
           SET IS_COMMISSION_PROCESS ='Y'              
           WHERE IDEN_ROW_ID = CAST(@PAYMENT_ROW_ID AS INT)             
        END              
  --ELSE              
  --      BEGIN              
  --       UPDATE ACT_CUSTOMER_OPEN_ITEMS              
  --       SET IS_COMMISSION_PROCESS ='Y'              
  --       WHERE IDEN_ROW_ID = CAST(@PAYMENT_ROW_ID AS INT)              
  --      END                       
                                 
           END                      
        IF(@INTERFACE_CODE = 1)  --UPDATE FOR BROKER COMMISSION                        
        BEGIN                        
         UPDATE ACT_AGENCY_STATEMENT_DETAILED                        
         SET IS_COMMISSION_PROCESS ='Y'                        
         WHERE ROW_ID = CAST(@PAYMENT_ROW_ID AS INT)                        
   END                        
                                 
                                 
        IF(@INTERFACE_CODE = 3) --UPDATE FOR CLAIM IDEMNITY                        
        BEGIN                        
                            
         SET @CLAIM_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',1)                    
         SET @ACTIVITY_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',2)                        
                                 
                                    
         UPDATE CLM_ACTIVITY_RESERVE                        
         SET IS_COMMISSION_PROCESS ='Y'                        
         WHERE CLAIM_ID = @CLAIM_ID                        
          AND ACTIVITY_ID = @ACTIVITY_ID                        
        END                          
                                 
                                 
        IF(@INTERFACE_CODE = 2) --UPDATE FOR CLAIM EXPENSE                        
        BEGIN                        
         SET @CLAIM_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',1)                        
         SET @ACTIVITY_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',2)                        
         SET @COVERAGE_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',3)                        
                                    
         UPDATE CLM_ACTIVITY_RESERVE                        
         SET IS_COMMISSION_PROCESS ='Y'                        
         WHERE CLAIM_ID = @CLAIM_ID                        
          AND ACTIVITY_ID = @ACTIVITY_ID                        
       AND COVERAGE_ID = @COVERAGE_ID                        
        END                         
                                 
                                 
                                 
         IF(@INTERFACE_CODE = 5) --UPDATE FOR RI CLAIM                        
        BEGIN                        
          IF(@EVENT_CODE IN ('01025','01075'))                        
         BEGIN                         
           --commented by naveen , itrack 1750                              
         UPDATE ACT_COI_STATEMENT_DETAILED                          
         SET IS_COMMISSION_PROCESS = 'Y'                          
         WHERE ROW_ID = CAST(@PAYMENT_ROW_ID AS INT)             
                   
  UPDATE ACT_POLICY_INSTALLMENT_DETAILS                           
  SET IS_COMMISSION_PROCESS = 'Y'                          
  WHERE ROW_ID = CAST(@PAYMENT_ROW_ID AS INT)                         
                                       
                                      
           END                        
           
           IF(@EVENT_CODE IN ('01045','01050','01055 '))                        
           BEGIN                         
                                      
                                      
            UPDATE POL_REINSURANCE_BREAKDOWN_DETAILS                          
         SET IS_COMMISSION_PROCESS = 'Y'                         
         WHERE IDEN_ROW_ID = CAST(@PAYMENT_ROW_ID AS INT)                        
                                     
                                     
                                     
         END               
                                   
           --IF(@EVENT_CODE IN ('01005','01011','01012','01014',                        
           --'01017','01019','01080','01085','01090','01095')                        
           --)               
           IF(@EVENT_CODE IN ('01080','01085','01090','01095'))                       
           BEGIN                         
            SET @CLAIM_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',1)          
            SET @ACTIVITY_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',2)                        
            SET @COMPANY_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',3)                        
                                       
            UPDATE CACRB                        
          SET CACRB.IS_COMMISSION_PROCESS = 'Y'                     
          FROM CLM_ACTIVITY_CO_RI_BREAKDOWN CACRB  WITH(NOLOCK)                        
          INNER JOIN CLM_ACTIVITY_RESERVE  CAR WITH(NOLOCK)                        
           ON CAR.CLAIM_ID= CACRB.CLAIM_ID                         
           AND CAR.ACTIVITY_ID = CACRB.ACTIVITY_ID                         
           AND CAR.RESERVE_ID = CACRB.RESERVE_ID                     
          WHERE CACRB.COMP_ID = @COMPANY_ID                        
           AND CAR.CLAIM_ID= @CLAIM_ID                        
           AND CAR.ACTIVITY_ID =  @ACTIVITY_ID                        
                                                
           END               
                         
           IF(@EVENT_CODE IN ('01005','01011','01012','01014',              
         '01017','01019'))                   
         BEGIN              
          SET @CLAIM_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',1)              
          SET @ACTIVITY_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',2)              
                       
                      
          UPDATE CLM_ACTIVITY_RESERVE              
          SET IS_COMMISSION_PROCESS ='Y'              
          WHERE CLAIM_ID = @CLAIM_ID              
          AND ACTIVITY_ID = @ACTIVITY_ID              
         END                        
        END                         
                                 
                                 
                                 
         SET @COUNTER = @COUNTER +1                        
        SET @COUNT=@COUNT-1                         
     END                        
                           
                                       
   ---UPDATION FOR TABLE PAGNET_EXPORT_RECORD                      
                         
   UPDATE PAGNET_EXPORT_RECORD                       
   SET PROCESSED = 'Y'                 
     WHERE [FILE_ID] = @FILE_ID              
     --commented by naveen for itrack    1405,            
     and RETURN_STATUS = 'Re-sent to Pagnet'              
    --and RETURN_STATUS = ' Re-enviado para o Pagnet'                 
               
    AND PROCESSED = 'N'                 
                               
  END                
                
 ELSE              
  BEGIN                    
  CREATE TABLE #TEMP2     ---CREATE A TEMPORARY TABLE                      
  (                        
  ROW_ID INT IDENTITY(1,1),                        
  [FILE_ID] INT,                        
  PAYMENT_ID VARCHAR(25),                        
  INTERFACE_CODE INT,                        
  EVENT_CODE VARCHAR(5)                        
  )                        
               
  INSERT INTO #TEMP2                        
  (                        
  [FILE_ID],                        
  PAYMENT_ID ,                        
  INTERFACE_CODE,                        
  EVENT_CODE                          
  )                        
  SELECT                         
   PER.[FILE_ID],                        
   PER.PAYMENT_ID,                        
   PER.INTERFACE_CODE,                        
   PER.EVENT_CODE                         
   FROM  PAGNET_EXPORT_RECORD  PER WITH (NOLOCK)                       
   WHERE                       
   PER.[FILE_ID] = @FILE_ID AND                       
   --RETURN_STATUS = 'Re-sent to Pagnet'  AND                      
   PROCESSED = 'Y'                       
                         
   --DECLARE @COUNT INT, @COUNTER INT =0,   ---DECLARE VARIABLES                      
   --  @INTERFACE_CODE INT = NULL,                        
   --  @PAYMENT_ROW_ID VARCHAR(25) = NULL,                        
   --  @CLAIM_ID INT=NULL,                        
   --  @ACTIVITY_ID INT = NULL,                        
   --  @COMPANY_ID INT =NULL,                         
   --  @COVERAGE_ID INT =NULL,                        
   --  @EVENT_CODE VARCHAR(5)=NULL,                        
   --  @FILE_ID1 INT =NULL                        
                           
                           
   ---UPDATION FOR PARENT TABLES                     
                            
     SELECT @COUNT = COUNT(*) FROM #TEMP2                        
                             
     SET @COUNTER =1                         
                        
     WHILE (@COUNT > 0)                         
     BEGIN                        
    SELECT  @INTERFACE_CODE = CAST(INTERFACE_CODE AS INT ),                        
     @PAYMENT_ROW_ID = PAYMENT_ID,                        
     @EVENT_CODE = EVENT_CODE  FROM #TEMP2 WHERE ROW_ID = @COUNTER                        
                               
   IF(@INTERFACE_CODE = 4)  --UPDATE CUSTOMER REFUND               
   BEGIN                       
    --BEGIN                        
    -- UPDATE ACT_CUSTOMER_OPEN_ITEMS                        
    -- SET IS_COMMISSION_PROCESS ='N'                        
    -- WHERE IDEN_ROW_ID = CAST(@PAYMENT_ROW_ID AS INT)                          
    --END              
    IF(@EVENT_CODE = '01020')              
    BEGIN              
     UPDATE ACT_POLICY_INSTALLMENT_DETAILS              
     SET IS_COMMISSION_PROCESS ='N'              
     WHERE ROW_ID = CAST(@PAYMENT_ROW_ID AS INT)              
    END              
    ELSE              
    BEGIN              
     UPDATE ACT_CUSTOMER_OPEN_ITEMS              
     SET IS_COMMISSION_PROCESS ='N'              
     WHERE IDEN_ROW_ID = CAST(@PAYMENT_ROW_ID AS INT)              
    END                        
                             
       END                      
    IF(@INTERFACE_CODE = 1)  --UPDATE FOR BROKER COMMISSION                        
    BEGIN                        
     UPDATE ACT_AGENCY_STATEMENT_DETAILED                        
     SET IS_COMMISSION_PROCESS ='N'                        
     WHERE ROW_ID = CAST(@PAYMENT_ROW_ID AS INT)                        
    END                        
                             
                             
    IF(@INTERFACE_CODE = 3) --UPDATE FOR CLAIM IDEMNITY                        
    BEGIN                        
                        
     SET @CLAIM_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',1)                        
     SET @ACTIVITY_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',2)                     
                             
                                
     UPDATE CLM_ACTIVITY_RESERVE                        
     SET IS_COMMISSION_PROCESS ='N'                        
     WHERE CLAIM_ID = @CLAIM_ID                        
      AND ACTIVITY_ID = @ACTIVITY_ID                        
    END                          
                             
                             
    IF(@INTERFACE_CODE = 2) --UPDATE FOR CLAIM EXPENSE                        
    BEGIN                        
     SET @CLAIM_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',1)                        
     SET @ACTIVITY_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',2)                        
     SET @COVERAGE_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',3)                        
                                
     UPDATE CLM_ACTIVITY_RESERVE                        
     SET IS_COMMISSION_PROCESS ='N'                        
    WHERE CLAIM_ID = @CLAIM_ID                        
      AND ACTIVITY_ID = @ACTIVITY_ID                        
   AND COVERAGE_ID = @COVERAGE_ID                        
    END                         
                             
                             
                             
     IF(@INTERFACE_CODE = 5) --UPDATE FOR RI CLAIM                        
    BEGIN                        
     IF(@EVENT_CODE IN ('01025','01075'))                        
   BEGIN                     
          --commented by naveen , itrack 1750                             
      UPDATE ACT_COI_STATEMENT_DETAILED                          
    SET IS_COMMISSION_PROCESS = 'N'                          
    WHERE ROW_ID = CAST(@PAYMENT_ROW_ID AS INT)            
              
              
     UPDATE ACT_POLICY_INSTALLMENT_DETAILS                          
    SET IS_COMMISSION_PROCESS = 'N'                          
    WHERE ROW_ID = CAST(@PAYMENT_ROW_ID AS INT)          
              
    --  UPDATE ACT_COI_OPEN_ITEMS                          
    --SET IS_COMMISSION_PROCESS = 'N'                          
    --WHERE IDEN_ROW_ID = CAST(@PAYMENT_ROW_ID AS INT)                        
      
                                 
      END                        
                                 
      IF(@EVENT_CODE IN ('01045','01050','01055 '))                        
      BEGIN                         
                                 
                                 
      UPDATE POL_REINSURANCE_BREAKDOWN_DETAILS                          
    SET IS_COMMISSION_PROCESS = 'N'                         
    WHERE IDEN_ROW_ID = CAST(@PAYMENT_ROW_ID AS INT)                        
                                 
                                 
                                 
      END                        
                               
                               
                               
                               
                               
                               
      --IF(@EVENT_CODE IN ('01005','01011','01012','01014',                        
      --'01017','01019','01080','01085','01090','01095')                        
      --)              
      IF(@EVENT_CODE IN ('01080','01085','01090','01095'))                          
      BEGIN                         
       SET @CLAIM_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',1)                        
       SET @ACTIVITY_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',2)                        
       SET @COMPANY_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',3)                        
                                  
       UPDATE CACRB                        
     SET CACRB.IS_COMMISSION_PROCESS = 'N'                        
     FROM CLM_ACTIVITY_CO_RI_BREAKDOWN CACRB  WITH(NOLOCK)                        
     INNER JOIN CLM_ACTIVITY_RESERVE  CAR WITH(NOLOCK)                        
      ON CAR.CLAIM_ID= CACRB.CLAIM_ID                         
      AND CAR.ACTIVITY_ID = CACRB.ACTIVITY_ID                         
      AND CAR.RESERVE_ID = CACRB.RESERVE_ID                        
     WHERE CACRB.COMP_ID = @COMPANY_ID                        
      AND CAR.CLAIM_ID= @CLAIM_ID                        
      AND CAR.ACTIVITY_ID =  @ACTIVITY_ID                        
                                           
      END              
                    
      IF(@EVENT_CODE IN ('01005','01011','01012','01014',              
         '01017','01019'))                   
   BEGIN              
      SET @CLAIM_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',1)              
      SET @ACTIVITY_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',2)              
                   
                   
      UPDATE CLM_ACTIVITY_RESERVE              
      SET IS_COMMISSION_PROCESS ='N'              
      WHERE CLAIM_ID = @CLAIM_ID              
      AND ACTIVITY_ID = @ACTIVITY_ID              
     END                           
    END                         
                             
                             
                             
     SET @COUNTER = @COUNTER +1                        
   SET @COUNT=@COUNT-1                         
     END                        
                           
                           
                        
   ---UPDATION FOR TABLE PAGNET_EXPORT_RECORD                      
                         
   UPDATE PAGNET_EXPORT_RECORD                       
   SET PROCESSED = 'N'                 
     WHERE [FILE_ID] = @FILE_ID                 
    --and RETURN_STATUS = 'Re-sent to Pagnet'                 
    AND PROCESSED = 'Y'                 
    
END                
                
                
 END 
 

	  
GO

