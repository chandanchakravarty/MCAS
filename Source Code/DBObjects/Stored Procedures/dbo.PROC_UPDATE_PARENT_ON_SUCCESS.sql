IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_UPDATE_PARENT_ON_SUCCESS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_UPDATE_PARENT_ON_SUCCESS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

  
  
  
--DROP PROC PROC_UPDATE_PARENT_ON_SUCCESS  
CREATE PROC [dbo].[PROC_UPDATE_PARENT_ON_SUCCESS] --268  
@F_ID INT ,  
@OUT_PARAM VARCHAR(200)='' OUTPUT   
AS  
BEGIN  
  
  
   
 CREATE TABLE #TEMP  
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
 WHERE PER.[FILE_ID] = @F_ID    
   
   
 DECLARE @COUNT INT, @COUNTER INT =0,  
   @INTERFACE_CODE INT = NULL,  
   @PAYMENT_ROW_ID VARCHAR(25) = NULL,  
   @CLAIM_ID INT=NULL,  
   @ACTIVITY_ID INT = NULL,  
   @COMPANY_ID INT =NULL,   
    @COVERAGE_ID INT =NULL,  
    @EVENT_CODE VARCHAR(5)=NULL,  
    @FILE_ID INT =NULL    
     
   SELECT @COUNT = COUNT(*) FROM #TEMP  
     
   SET @COUNTER =1   
     
   WHILE (@COUNT > 0)    
   BEGIN  
     SELECT  @INTERFACE_CODE = CAST(INTERFACE_CODE AS INT ),  
      @PAYMENT_ROW_ID = PAYMENT_ID,  
      @EVENT_CODE = EVENT_CODE  FROM #TEMP WHERE ROW_ID = @COUNTER   
        
    IF(@INTERFACE_CODE = 4)  --UPDATE CUSTOMER REFUND  
     BEGIN 
		 IF(@EVENT_CODE = '01020')
		 BEGIN
			UPDATE ACT_POLICY_INSTALLMENT_DETAILS
			SET IS_PAID_TO_PAGNET ='Y',
				PAGNET_DATE = GETDATE()
			WHERE ROW_ID = CAST(@PAYMENT_ROW_ID AS INT)
		END
		ELSE
		BEGIN
			UPDATE ACT_CUSTOMER_OPEN_ITEMS
			SET IS_PAID_TO_PAGNET ='Y',
			PAGNET_DATE = GETDATE()
			WHERE IDEN_ROW_ID = CAST(@PAYMENT_ROW_ID AS INT)
		END  
	END
       
    IF(@INTERFACE_CODE = 1)  --UPDATE FOR BROKER COMMISSION  
     BEGIN  
      UPDATE ACT_AGENCY_STATEMENT_DETAILED  
      SET IS_PAID_TO_PAGNET ='Y',
         PAGNET_DATE = GETDATE()  
      WHERE ROW_ID = CAST(@PAYMENT_ROW_ID AS INT)  
        
      --SET @OUT_PARAM = '\n' + @OUT_PARAM + 'UPDATE FOR BROKER COMMISSION' + @PAYMENT_ROW_ID  
     END   
       
    IF(@INTERFACE_CODE = 3) --UPDATE FOR CLAIM IDEMNITY  
     BEGIN  
       --SELECT dbo.Piece(REPLACE('103SEP12SEP5','SEP','-'), '-',1)  
      SET @CLAIM_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',1)  
      SET @ACTIVITY_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',2)  
        
        
      UPDATE CLM_ACTIVITY_RESERVE  
      SET IS_PAID_TO_PAGNET ='Y',
         PAGNET_DATE = GETDATE()   
      WHERE CLAIM_ID = @CLAIM_ID  
       AND ACTIVITY_ID = @ACTIVITY_ID  
         
       --SET @OUT_PARAM = '\n' + @OUT_PARAM + 'UPDATE FOR CLAIM IDEMNITY' + @PAYMENT_ROW_ID  
     END   
       
    IF(@INTERFACE_CODE = 2) --UPDATE FOR CLAIM EXPENSE  
     BEGIN  
      SET @CLAIM_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',1)  
      SET @ACTIVITY_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',2)  
      SET @COVERAGE_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',3)  
        
      UPDATE CLM_ACTIVITY_RESERVE  
      SET IS_PAID_TO_PAGNET ='Y',
         PAGNET_DATE = GETDATE()   
      WHERE CLAIM_ID = @CLAIM_ID  
       AND ACTIVITY_ID = @ACTIVITY_ID  
       AND COVERAGE_ID = @COVERAGE_ID  
         
       --SET @OUT_PARAM = '\n' + @OUT_PARAM + 'UPDATE FOR CLAIM EXPENSE' + @PAYMENT_ROW_ID  
     END   
       
    IF(@INTERFACE_CODE = 5) --UPDATE FOR RI CLAIM  
     BEGIN  
      IF(@EVENT_CODE IN ('01025','01075'))  
       BEGIN   
            
       UPDATE ACT_COI_STATEMENT_DETAILED    
        SET IS_PAID_TO_PAGNET ='Y',
         PAGNET_DATE = GETDATE()    
        WHERE ROW_ID = CAST(@PAYMENT_ROW_ID AS INT)  
          
       --SET @OUT_PARAM = '\n' + @OUT_PARAM + 'UPDATE FOR RI CLAIM' + 'EVENT CODE = '+ @EVENT_CODE +'PAYMENT ID = '  + @PAYMENT_ROW_ID  
         
       END  
         
       IF(@EVENT_CODE IN ('01045','01050','01055 '))  
       BEGIN   
         
         
       UPDATE POL_REINSURANCE_BREAKDOWN_DETAILS    
        SET IS_PAID_TO_PAGNET ='Y',
         PAGNET_DATE = GETDATE()    
        WHERE IDEN_ROW_ID = CAST(@PAYMENT_ROW_ID AS INT)  
         
       --SET @OUT_PARAM = '\n' + @OUT_PARAM + 'UPDATE FOR RI CLAIM' + 'EVENT CODE = '+ @EVENT_CODE +'PAYMENT ID = '  + @PAYMENT_ROW_ID  
         
         
       END  
--     
              
        
     IF(@EVENT_CODE IN ('01080','01085','01090','01095'))         
      BEGIN   
       SET @CLAIM_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',1)  
       SET @ACTIVITY_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',2)  
       SET @COMPANY_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',3)  
         
       UPDATE CACRB  
        SET IS_PAID_TO_PAGNET ='Y',
         PAGNET_DATE = GETDATE()   
        FROM CLM_ACTIVITY_CO_RI_BREAKDOWN CACRB  WITH(NOLOCK)  
        INNER JOIN CLM_ACTIVITY_RESERVE  CAR WITH(NOLOCK)  
         ON CAR.CLAIM_ID= CACRB.CLAIM_ID   
         AND CAR.ACTIVITY_ID = CACRB.ACTIVITY_ID   
         AND CAR.RESERVE_ID = CACRB.RESERVE_ID  
        WHERE CACRB.COMP_ID = @COMPANY_ID  
         AND CAR.CLAIM_ID= @CLAIM_ID  
         AND CAR.ACTIVITY_ID =  @ACTIVITY_ID  
           
         --SET @OUT_PARAM = '\n' + @OUT_PARAM + 'UPDATE FOR RI CLAIM' + 'EVENT CODE = '+ @EVENT_CODE +'PAYMENT ID = '  + @PAYMENT_ROW_ID  
         
       END  
       IF(@EVENT_CODE IN ('01005','01011','01012','01014',  
       '01017','01019'))       
      BEGIN   
        
           
       SET @CLAIM_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',1)  
       SET @ACTIVITY_ID = dbo.Piece(REPLACE(@PAYMENT_ROW_ID,'SEP','-'), '-',2)  
        
        
       UPDATE CLM_ACTIVITY_RESERVE  
       SET IS_PAID_TO_PAGNET ='Y',
         PAGNET_DATE = GETDATE()   
       WHERE CLAIM_ID = @CLAIM_ID  
       AND ACTIVITY_ID = @ACTIVITY_ID          
           
         --SET @OUT_PARAM = '\n' + @OUT_PARAM + 'UPDATE FOR RI CLAIM' + 'EVENT CODE = '+ @EVENT_CODE +'PAYMENT ID = '  + @PAYMENT_ROW_ID  
         
       END  
      --PRINT 1  
     END   
       
  
           
      
      
     SET @COUNTER = @COUNTER +1  
    SET @COUNT=@COUNT-1   
   END  
     
   
   
   
  
  
END  
GO

