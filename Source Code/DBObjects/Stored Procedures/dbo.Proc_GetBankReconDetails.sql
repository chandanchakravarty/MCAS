IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetBankReconDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetBankReconDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--begin tran              
--drop proc dbo.[Proc_GetBankReconDetails]              
--go              
/*----------------------------------------------------------                  
Proc Name       : dbo.Proc_GetBankReconDetails                  
Created by      : Ravindra                  
Date            : 2/21/2007                  
Purpose   : Return Reonciled / Outstanding Itmes related to the specified group                  
Revison History :                  
Used In  : Wolverine                  
                  
exec Proc_GetBankReconDetails 17,3,'detail'                  
------------------------------------------------------------                  
Date     Review By          Comments                  
------   ------------       -------------------------*/                  
-- drop proc dbo.Proc_GetBankReconDetails                  
CREATE   PROC [dbo].[Proc_GetBankReconDetails]                  
(                  
 @AC_RECONCILIATION_ID int,                  
 @ACCOUNT_ID int,                  
 @CALLED_FROM Varchar(20)                  
)                  
AS                   
BEGIN                  
                  
DECLARE @EFT Int,                  
  @CHECK Int,                  
  @ALREADY_PROCESSED Int                  
                  
SET  @CHECK = 11787--11975                  
SET  @EFT = 11976                  
SET  @ALREADY_PROCESSED = 11977                  
                  
CREATE TABLE #TEMP_DETAIL                  
(                  
 [IDENT_COL] Int IDENTITY(1,1) NOT NULL ,                        
 [ROW_LEVEL] Char(1),                  
 [UPDATED_FROM] Char(1),                  
 --[SOURCE_TRAN_DATE] Varchar(15),                  
 [SOURCE_TRAN_DATE] DATETIME,                  
 [IDENTITY_ROW_ID] Int,                  
 -- [SOURCE_NUMBER] INt,                  
 [SOURCE_NUMBER] varchar(50),                
 [TRANSACTION_AMOUNT] Decimal(18,2),                  
 [TRANSACTION_TYPE] Varchar(50),                  
 [NOTEMEMO] Varchar(500)                  
)                 
          
CREATE TABLE #FINAL_DETAIL                  
(                  
 [IDENT_COL] Int IDENTITY(1,1) NOT NULL ,                        
 [ROW_LEVEL] Char(1),                  
 [UPDATED_FROM] Char(1),                  
 [SOURCE_TRAN_DATE] DATETIME,                  
 [IDENTITY_ROW_ID] Int,                  
 [SOURCE_NUMBER] varchar(50),                
 [TRANSACTION_AMOUNT] Decimal(18,2),                  
 [TRANSACTION_TYPE] Varchar(50),                  
 [NOTEMEMO] Varchar(500)                  
)                 
           
CREATE TABLE #RECORDS_TO_DISPLAY          
(                  
 [IDENT_COL]   Int IDENTITY(1,1) NOT NULL ,                        
 [ROW_LEVEL]   Char(1),                  
 [UPDATED_FROM]   Char(1),                  
 [SOURCE_TRAN_DATE]  DATETIME,                  
 [IDENTITY_ROW_ID]  Int,                  
 [SOURCE_NUMBER]  varchar(50),                
 [TRANSACTION_AMOUNT] Decimal(18,2),                  
 [TRANSACTION_TYPE]  Varchar(50),                  
 [NOTEMEMO]    Varchar(500)                  
)                 
                  
DECLARE @TOTAL_DEPOSITS  Decimal(18,2),                  
  @TOTAL_CHECKS  Decimal(18,2),                  
  @TOTAL_JE  Decimal(18,2),                  
  @TOTAL_OTHER  Decimal(18,2),             
  @STATEMENT_DATE DATETIME               
                    
SET @TOTAL_DEPOSITS  = 0                   
SET @TOTAL_CHECKS  = 0                   
SET @TOTAL_JE  = 0                  
                
                
     
IF(@CALLED_FROM  = 'OUTSTANDING')                  
BEGIN                   
 -- Sum of checks            
    SELECT @STATEMENT_DATE = ISNULL(ABR.STATEMENT_DATE,'')           
    FROM ACT_BANK_RECONCILIATION ABR WHERE AC_RECONCILIATION_ID = @AC_RECONCILIATION_ID               
          
    SELECT @TOTAL_CHECKS = ISNULL(SUM(ISNULL(PD.TRANSACTION_AMOUNT,0)),0)                   
 FROM ACT_ACCOUNTS_POSTING_DETAILS PD                  
 WHERE PD.ACCOUNT_ID = @ACCOUNT_ID                  
 AND PD.UPDATED_FROM = 'C'                  
 AND ISNULL(PD.IS_BANK_RECONCILED,'N')='N'                     
 AND PD.IDENTITY_ROW_ID NOT IN                 
 (                  
 SELECT IDENTITY_ROW_ID FROM ACT_BANK_RECONCILIATION_ITEMS_DETAILS                   
 WHERE AC_RECONCILIATION_ID= @AC_RECONCILIATION_ID             
 )  
 AND DATEDIFF(DD,@STATEMENT_DATE,ISNULL(PD.SOURCE_TRAN_DATE,'') ) <= 0                   
         
 IF(@TOTAL_CHECKS <> 0)                  
 BEGIN                   
  INSERT INTO #TEMP_DETAIL                  
  (                  
  ROW_LEVEL,UPDATED_FROM,SOURCE_TRAN_DATE,IDENTITY_ROW_ID,                  
  SOURCE_NUMBER,      
  TRANSACTION_AMOUNT,TRANSACTION_TYPE,                  
  NOTEMEMO                  
  )                   
  VALUES( 'H' ,'C',' ' ,' ' ,                  
  ' ' ,@TOTAL_CHECKS ,'Checks' ,                  
  ' ' )            
  -- Checks                   
  INSERT INTO #TEMP_DETAIL                  
  (                  
  ROW_LEVEL,UPDATED_FROM,SOURCE_TRAN_DATE,IDENTITY_ROW_ID,                  
  SOURCE_NUMBER,TRANSACTION_AMOUNT,TRANSACTION_TYPE,                  
  NOTEMEMO                  
  )                   
  SELECT                   
  'R' ,PD.UPDATED_FROM ,CONVERT(VARCHAR,PD.SOURCE_TRAN_DATE,101) ,PD.IDENTITY_ROW_ID,                  
  PD.SOURCE_NUM , PD.TRANSACTION_AMOUNT,'Check',                  
  PD.ADNL_INFO                   
  FROM ACT_ACCOUNTS_POSTING_DETAILS PD                  
  WHERE PD.ACCOUNT_ID = @ACCOUNT_ID                  
  AND PD.UPDATED_FROM = 'C'                  
  AND ISNULL(PD.IS_BANK_RECONCILED,'N')='N'   
  AND PD.IDENTITY_ROW_ID NOT IN                 
  (   
   SELECT IDENTITY_ROW_ID FROM ACT_BANK_RECONCILIATION_ITEMS_DETAILS                   
   WHERE AC_RECONCILIATION_ID= @AC_RECONCILIATION_ID    
  )                  
  AND  DATEDIFF(DD,@STATEMENT_DATE,ISNULL(PD.SOURCE_TRAN_DATE,'')) <= 0               
 END               
 -- Sum of Deposits                    
  SELECT @TOTAL_DEPOSITS = ISNULL(SUM(ISNULL(PD.TRANSACTION_AMOUNT,0)),0)                  
  FROM ACT_ACCOUNTS_POSTING_DETAILS PD                  
  WHERE PD.ACCOUNT_ID = @ACCOUNT_ID                  
  AND PD.UPDATED_FROM = 'D'                  
  AND ISNULL(PD.IS_BANK_RECONCILED,'N')='N'                                      
  AND PD.IDENTITY_ROW_ID NOT IN                 
   (                  
  SELECT IDENTITY_ROW_ID FROM ACT_BANK_RECONCILIATION_ITEMS_DETAILS                   
  WHERE AC_RECONCILIATION_ID= @AC_RECONCILIATION_ID             
   )   
   AND  DATEDIFF(DD,@STATEMENT_DATE,ISNULL(PD.SOURCE_TRAN_DATE,'')) <= 0                
 IF(@TOTAL_DEPOSITS <> 0)                  
 BEGIN                
  INSERT INTO #TEMP_DETAIL                  
  (                  
  ROW_LEVEL,UPDATED_FROM,SOURCE_TRAN_DATE,IDENTITY_ROW_ID,                  
  SOURCE_NUMBER,TRANSACTION_AMOUNT,TRANSACTION_TYPE,                  
  NOTEMEMO                  
  )                   
  VALUES(                  
  'H' ,'D' ,' ' ,' ' ,                  
  ' ',@TOTAL_DEPOSITS ,'Deposits',                   
  ' ' )   
  -- Deposits                   
  INSERT INTO #TEMP_DETAIL                  
  (                  
  ROW_LEVEL,UPDATED_FROM,SOURCE_TRAN_DATE,IDENTITY_ROW_ID,                  
  SOURCE_NUMBER,TRANSACTION_AMOUNT,TRANSACTION_TYPE,                  
  NOTEMEMO                  
  )                   
  SELECT                   
  'R' ,PD.UPDATED_FROM ,CONVERT(VARCHAR,PD.SOURCE_TRAN_DATE,101) ,PD.IDENTITY_ROW_ID,                  
  PD.SOURCE_NUM ,PD.TRANSACTION_AMOUNT,'Deposit' ,                  
  PD.ADNL_INFO                   
  FROM ACT_ACCOUNTS_POSTING_DETAILS PD                  
  WHERE PD.ACCOUNT_ID = @ACCOUNT_ID                  
  AND PD.UPDATED_FROM = 'D'                  
  AND ISNULL(PD.IS_BANK_RECONCILED,'N')='N'    
  AND PD.IDENTITY_ROW_ID NOT IN                 
  (                  
  SELECT IDENTITY_ROW_ID FROM ACT_BANK_RECONCILIATION_ITEMS_DETAILS                   
  WHERE AC_RECONCILIATION_ID= @AC_RECONCILIATION_ID    
  )                  
  AND DATEDIFF(DD,@STATEMENT_DATE,ISNULL(PD.SOURCE_TRAN_DATE,'') ) <= 0                  
 END                 
                    
                   
 -- Sum of JEs                       
    SELECT @TOTAL_JE = ISNULL(SUM(ISNULL(PD.TRANSACTION_AMOUNT,0)),0)                  
 FROM ACT_ACCOUNTS_POSTING_DETAILS PD                  
 WHERE PD.ACCOUNT_ID = @ACCOUNT_ID                  
 AND PD.UPDATED_FROM = 'J'                  
 AND ISNULL(PD.IS_BANK_RECONCILED,'N')='N'                                    
 AND PD.IDENTITY_ROW_ID NOT IN                 
 (                  
 SELECT IDENTITY_ROW_ID FROM ACT_BANK_RECONCILIATION_ITEMS_DETAILS                   
 WHERE AC_RECONCILIATION_ID= @AC_RECONCILIATION_ID   
 )         
 AND DATEDIFF(DD,@STATEMENT_DATE,ISNULL(PD.SOURCE_TRAN_DATE,'') ) <= 0                           
          
 IF(@TOTAL_JE <> 0)                  
 BEGIN                   
  INSERT INTO #TEMP_DETAIL                  
  (                  
  ROW_LEVEL,UPDATED_FROM,SOURCE_TRAN_DATE,IDENTITY_ROW_ID,                  
  SOURCE_NUMBER,TRANSACTION_AMOUNT,TRANSACTION_TYPE,                  
  NOTEMEMO                  
  )                   
  VALUES(                  
  'H' ,'J' ,' ' ,' ' ,                  
  ' ',@TOTAL_JE ,'Journal Entries',                   
  ' ' )                  
          
          
  -- Journal Entries                  
  INSERT INTO #TEMP_DETAIL                  
  (                  
  ROW_LEVEL,UPDATED_FROM,SOURCE_TRAN_DATE,IDENTITY_ROW_ID,                  
  SOURCE_NUMBER,TRANSACTION_AMOUNT,TRANSACTION_TYPE,                  
  NOTEMEMO                  
  )                   
  SELECT                   
  'R' ,PD.UPDATED_FROM ,CONVERT(VARCHAR,PD.SOURCE_TRAN_DATE,101) ,PD.IDENTITY_ROW_ID,                  
  PD.SOURCE_NUM ,PD.TRANSACTION_AMOUNT,'Journal Entries' ,                  
  PD.ADNL_INFO                   
  FROM ACT_ACCOUNTS_POSTING_DETAILS PD                  
  WHERE PD.ACCOUNT_ID = @ACCOUNT_ID                  
  AND PD.UPDATED_FROM = 'J'                  
  AND ISNULL(PD.IS_BANK_RECONCILED,'N')='N'                                  
  AND PD.IDENTITY_ROW_ID NOT IN                 
  (                  
  SELECT IDENTITY_ROW_ID FROM ACT_BANK_RECONCILIATION_ITEMS_DETAILS                   
  WHERE AC_RECONCILIATION_ID= @AC_RECONCILIATION_ID                          
  )                  
  AND DATEDIFF(DD,@STATEMENT_DATE,ISNULL(PD.SOURCE_TRAN_DATE,'')) <= 0                  
 END                
                
 --Other Entries Total                  
 SELECT @TOTAL_OTHER = ISNULL(SUM(ISNULL(PD.TRANSACTION_AMOUNT,0)),0)                  
 FROM ACT_ACCOUNTS_POSTING_DETAILS PD                  
 WHERE PD.ACCOUNT_ID = @ACCOUNT_ID                  
 AND PD.UPDATED_FROM NOT IN ('J','D','C')                  
 AND ISNULL(PD.IS_BANK_RECONCILED,'N')='N'                   
 AND PD.IDENTITY_ROW_ID NOT IN                 
 (                  
 SELECT IDENTITY_ROW_ID FROM ACT_BANK_RECONCILIATION_ITEMS_DETAILS                   
 WHERE AC_RECONCILIATION_ID= @AC_RECONCILIATION_ID                  
 )                  
  AND DATEDIFF(DD,@STATEMENT_DATE,ISNULL(PD.SOURCE_TRAN_DATE,'') ) <= 0 
        
 IF(@TOTAL_OTHER <> 0)                  
 BEGIN                   
  INSERT INTO #TEMP_DETAIL                  
  (                  
  ROW_LEVEL,UPDATED_FROM,SOURCE_TRAN_DATE,IDENTITY_ROW_ID,                  
  SOURCE_NUMBER,TRANSACTION_AMOUNT,TRANSACTION_TYPE,                  
  NOTEMEMO                  
  )                   
  VALUES(                  
  'H' ,'O' ,' ' ,' ' ,                  
  ' ',@TOTAL_OTHER ,'Other Entries',                   
  ' ' )                  
          
          
  -- Other Entries                  
  INSERT INTO #TEMP_DETAIL                  
  (                  
  ROW_LEVEL,UPDATED_FROM,SOURCE_TRAN_DATE,IDENTITY_ROW_ID,                  
  SOURCE_NUMBER,TRANSACTION_AMOUNT,TRANSACTION_TYPE,                  
  NOTEMEMO                  
  )                   
  SELECT                   
  'R' ,PD.UPDATED_FROM ,CONVERT(VARCHAR,PD.SOURCE_TRAN_DATE,101) ,PD.IDENTITY_ROW_ID,             
  PD.SOURCE_NUM ,PD.TRANSACTION_AMOUNT,'Other Entry' ,                  
  PD.ADNL_INFO                   
  FROM ACT_ACCOUNTS_POSTING_DETAILS PD                  
  WHERE PD.ACCOUNT_ID = @ACCOUNT_ID                  
  AND PD.UPDATED_FROM  NOT IN ('J','D','C')                 
  AND ISNULL(PD.IS_BANK_RECONCILED,'N')='N'                   
  AND PD.IDENTITY_ROW_ID NOT IN                 
  (                  
  SELECT IDENTITY_ROW_ID FROM ACT_BANK_RECONCILIATION_ITEMS_DETAILS                   
  WHERE AC_RECONCILIATION_ID= @AC_RECONCILIATION_ID                  
  ) 
  AND DATEDIFF(DD,@STATEMENT_DATE,ISNULL(PD.SOURCE_TRAN_DATE,'') ) <= 0                  
 END                
          
    --Created New Temp table For Itrack #Issue 5205          
 /*INSERT INTO #FINAL_DETAIL                         
 SELECT  ROW_LEVEL,UPDATED_FROM ,          
     SOURCE_TRAN_DATE,               
     IDENTITY_ROW_ID,                  
 SOURCE_NUMBER AS SOURCE_NUM,            
 TRANSACTION_AMOUNT,            
 TRANSACTION_TYPE  AS TRANSACTIONTYPE,                  
 NOTEMEMO                   
 FROM  #TEMP_DETAIL            
 WHERE ISNUMERIC(ISNULL(SOURCE_NUMBER,'0')) =  1             
           
          
          
 INSERT INTO #RECORDS_TO_DISPLAY          
 SELECT                   
 ROW_LEVEL,UPDATED_FROM ,          
    SOURCE_TRAN_DATE ,               
     IDENTITY_ROW_ID,                  
 SOURCE_NUMBER AS SOURCE_NUM,            
 TRANSACTION_AMOUNT ,          
 TRANSACTION_TYPE  AS TRANSACTIONTYPE,                  
 NOTEMEMO                   
 FROM #FINAL_DETAIL          
 ORDER BY UPDATED_FROM,SOURCE_TRAN_DATE, CAST(ISNULL(SOURCE_NUMBER,'0') AS INT)            
          
 INSERT INTO #RECORDS_TO_DISPLAY          
 SELECT  ROW_LEVEL,UPDATED_FROM ,          
    SOURCE_TRAN_DATE,          
      --CONVERT(varchar,SOURCE_TRAN_DATE,101) as  SOURCE_TRAN_DATE,          
    IDENTITY_ROW_ID,                  
 SOURCE_NUMBER AS SOURCE_NUM,            
 TRANSACTION_AMOUNT,            
 TRANSACTION_TYPE  AS TRANSACTIONTYPE,                  
 NOTEMEMO                   
 FROM  #TEMP_DETAIL            
 WHERE ISNUMERIC(ISNULL(SOURCE_NUMBER,'0')) <>  1            
 ORDER BY UPDATED_FROM,SOURCE_TRAN_DATE, SOURCE_NUMBER          
          
 SELECT                   
 ROW_LEVEL,UPDATED_FROM ,             
    CONVERT(varchar,SOURCE_TRAN_DATE,101) as  SOURCE_TRAN_DATE ,           
    --SOURCE_TRAN_DATE ,          
    IDENTITY_ROW_ID,                  
 SOURCE_NUMBER AS SOURCE_NUM,            
 CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(TRANSACTION_AMOUNT,0)),1) AS TRANSACTION_AMOUNT,             
 TRANSACTION_TYPE  AS TRANSACTIONTYPE,               
 NOTEMEMO                   
 FROM #RECORDS_TO_DISPLAY          
 ORDER BY IDENT_COL  */        
        
     
  INSERT INTO #FINAL_DETAIL                  
 SELECT  ROW_LEVEL,UPDATED_FROM ,              
   SOURCE_TRAN_DATE ,          
   IDENTITY_ROW_ID,                  
  SOURCE_NUMBER AS SOURCE_NUM,            
   TRANSACTION_AMOUNT,            
  TRANSACTION_TYPE  AS TRANSACTIONTYPE,                  
  NOTEMEMO                   
  FROM  #TEMP_DETAIL            
        
 WHERE --ISNUMERIC(ISNULL(SOURCE_NUMBER,'0')) =  1            
(ROW_LEVEL='H'  or ROW_LEVEL='R') and UPDATED_FROM ='C'  and  isnumeric(SOURCE_NUMBER)=0           
 ORDER BY ROW_LEVEL ,SOURCE_TRAN_DATE,SOURCE_NUMBER     
        
  UPDATE #FINAL_DETAIL SET SOURCE_TRAN_DATE = NULL WHERE ROW_LEVEL='H'        
        
  INSERT INTO #FINAL_DETAIL                  
 SELECT  ROW_LEVEL,UPDATED_FROM ,              
   SOURCE_TRAN_DATE ,          
   IDENTITY_ROW_ID,                  
  SOURCE_NUMBER AS SOURCE_NUM,            
   TRANSACTION_AMOUNT,            
  TRANSACTION_TYPE  AS TRANSACTIONTYPE,                  
  NOTEMEMO                   
  FROM  #TEMP_DETAIL            
        
 WHERE --ISNUMERIC(ISNULL(SOURCE_NUMBER,'0')) =  1            
(ROW_LEVEL='H'  or ROW_LEVEL='R') and UPDATED_FROM ='C'  and  isnumeric(SOURCE_NUMBER)<>0           
 ORDER BY ROW_LEVEL ,SOURCE_TRAN_DATE  ,CAST(SOURCE_NUMBER AS INT)         
         
  UPDATE #FINAL_DETAIL SET SOURCE_TRAN_DATE = NULL WHERE ROW_LEVEL='H'        
          
        
INSERT INTO #FINAL_DETAIL                         
SELECT  ROW_LEVEL,UPDATED_FROM ,              
 SOURCE_TRAN_DATE ,          
 IDENTITY_ROW_ID,                  
 SOURCE_NUMBER AS SOURCE_NUM,            
 TRANSACTION_AMOUNT,            
 TRANSACTION_TYPE  AS TRANSACTIONTYPE,                  
 NOTEMEMO                   
 FROM  #TEMP_DETAIL            
 WHERE --ISNUMERIC(ISNULL(SOURCE_NUMBER,'0')) =  1            
 (ROW_LEVEL='H'   or ROW_LEVEL='R') and UPDATED_FROM ='D'  and  isnumeric(SOURCE_NUMBER)= 0              
 ORDER BY ROW_LEVEL,SOURCE_TRAN_DATE  , SOURCE_NUMBER         
        
UPDATE #FINAL_DETAIL SET SOURCE_TRAN_DATE = NULL WHERE ROW_LEVEL='H'      
    
        
INSERT INTO #FINAL_DETAIL                         
SELECT  ROW_LEVEL,UPDATED_FROM ,              
 SOURCE_TRAN_DATE ,          
 IDENTITY_ROW_ID,                  
 SOURCE_NUMBER AS SOURCE_NUM,            
 TRANSACTION_AMOUNT,            
 TRANSACTION_TYPE  AS TRANSACTIONTYPE,                  
 NOTEMEMO                   
 FROM  #TEMP_DETAIL            
 WHERE --ISNUMERIC(ISNULL(SOURCE_NUMBER,'0')) =  1            
 (ROW_LEVEL='H'  or ROW_LEVEL='R') and UPDATED_FROM ='D' and  isnumeric(SOURCE_NUMBER)<>0                
 ORDER BY ROW_LEVEL,SOURCE_TRAN_DATE,CAST(SOURCE_NUMBER AS INT) --, ISNULL(SOURCE_NUMBER,'0')       
    
UPDATE #FINAL_DETAIL SET SOURCE_TRAN_DATE = NULL WHERE ROW_LEVEL='H'        
        
        
INSERT INTO #FINAL_DETAIL                         
SELECT  ROW_LEVEL,UPDATED_FROM ,              
SOURCE_TRAN_DATE ,          
IDENTITY_ROW_ID,                  
SOURCE_NUMBER AS SOURCE_NUM,            
TRANSACTION_AMOUNT,            
TRANSACTION_TYPE  AS TRANSACTIONTYPE,                  
NOTEMEMO                   
FROM  #TEMP_DETAIL            
WHERE --ISNUMERIC(ISNULL(SOURCE_NUMBER,'0')) =  1            
(ROW_LEVEL='H' or ROW_LEVEL='R') and UPDATED_FROM ='J' and  isnumeric(SOURCE_NUMBER)= 0                  
ORDER BY ROW_LEVEL,SOURCE_NUMBER  ,SOURCE_TRAN_DATE--, ISNULL(SOURCE_NUMBER,'0')       
    
UPDATE #FINAL_DETAIL SET SOURCE_TRAN_DATE = NULL WHERE ROW_LEVEL='H'       
        
        
INSERT INTO #FINAL_DETAIL                         
SELECT  ROW_LEVEL,UPDATED_FROM ,              
SOURCE_TRAN_DATE ,          
IDENTITY_ROW_ID,                  
SOURCE_NUMBER AS SOURCE_NUM,            
TRANSACTION_AMOUNT,            
TRANSACTION_TYPE  AS TRANSACTIONTYPE,                  
NOTEMEMO                   
FROM  #TEMP_DETAIL            
WHERE --ISNUMERIC(ISNULL(SOURCE_NUMBER,'0')) =  1            
(ROW_LEVEL='H' or ROW_LEVEL='R') and UPDATED_FROM ='J'  and  isnumeric(SOURCE_NUMBER)<> 0               
ORDER BY ROW_LEVEL,SOURCE_TRAN_DATE,CAST(SOURCE_NUMBER AS INT) --, ISNULL(SOURCE_NUMBER,'0')        
        
 UPDATE #FINAL_DETAIL SET SOURCE_TRAN_DATE = NULL WHERE ROW_LEVEL='H'         
        
INSERT INTO #FINAL_DETAIL                         
SELECT  ROW_LEVEL,UPDATED_FROM ,              
 SOURCE_TRAN_DATE ,          
 IDENTITY_ROW_ID,                  
 SOURCE_NUMBER AS SOURCE_NUM,            
 TRANSACTION_AMOUNT,            
 TRANSACTION_TYPE  AS TRANSACTIONTYPE,                  
 NOTEMEMO                   
FROM  #TEMP_DETAIL            
 WHERE --ISNUMERIC(ISNULL(SOURCE_NUMBER,'0')) =  1            
(ROW_LEVEL='H' or ROW_LEVEL='R') and UPDATED_FROM  IN ('I','O')  and  isnumeric(SOURCE_NUMBER)= 0            
ORDER BY ROW_LEVEL, SOURCE_TRAN_DATE , SOURCE_NUMBER  --, ISNULL(SOURCE_NUMBER,'0')        
    
UPDATE #FINAL_DETAIL SET SOURCE_TRAN_DATE = NULL WHERE ROW_LEVEL='H'       
        
INSERT INTO #FINAL_DETAIL                         
SELECT  ROW_LEVEL,UPDATED_FROM ,              
 SOURCE_TRAN_DATE ,          
 IDENTITY_ROW_ID,                  
 SOURCE_NUMBER AS SOURCE_NUM,            
 TRANSACTION_AMOUNT,            
 TRANSACTION_TYPE  AS TRANSACTIONTYPE,     
 NOTEMEMO                   
FROM  #TEMP_DETAIL            
 WHERE --ISNUMERIC(ISNULL(SOURCE_NUMBER,'0')) =  1            
(ROW_LEVEL='H' or ROW_LEVEL='R') and UPDATED_FROM IN ('I','O') and  isnumeric(SOURCE_NUMBER)<> 0               
ORDER BY ROW_LEVEL,SOURCE_TRAN_DATE,CAST(SOURCE_NUMBER AS INT)  --, ISNULL(SOURCE_NUMBER,'0')         
        
  UPDATE #FINAL_DETAIL SET SOURCE_TRAN_DATE = NULL WHERE ROW_LEVEL='H'        
        
  select          
 [IDENT_COL]  ,                        
 [ROW_LEVEL] ,          
 CONVERT(VARCHAR,SOURCE_TRAN_DATE,101) AS SOURCE_TRAN_DATE ,                
 [UPDATED_FROM] ,                             
 [IDENTITY_ROW_ID] ,                  
 [SOURCE_NUMBER] as SOURCE_NUM ,                
 --[TRANSACTION_AMOUNT] as TRANSACTION_AMOUNT ,        
 CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(TRANSACTION_AMOUNT,0)),1) AS TRANSACTION_AMOUNT,        
 [TRANSACTION_TYPE] as TRANSACTIONTYPE ,                  
 [NOTEMEMO] as NoteMemo           
  from #FINAL_DETAIL ORDER BY IDENT_COL           
      
        
--        
              
 --fetching details to be displayed on top of grid                  
 SELECT                   
 (                  
 --SELECT CONVERT(VARCHAR,ACC_DISP_NUMBER)+': '+ACC_DESCRIPTION  FROM ACT_GL_ACCOUNTS                   
 SELECT CONVERT(VARCHAR,ACC_NUMBER) FROM ACT_GL_ACCOUNTS                   
 WHERE ACCOUNT_ID=@ACCOUNT_ID                  
 ) AS ACC_DESCRIPTION,                  
 CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(STARTING_BALANCE,0)),1) AS STARTING_BALANCE,                  
 CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(ENDING_BALANCE,0)),1) AS ENDING_BALANCE,                  
 (                 
 SELECT               
 CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(ISNULL(SUM(TRANSACTION_AMOUNT),0),0)),1)              
 --ISNULL(SUM(TRANSACTION_AMOUNT),0)              
 FROM ACT_ACCOUNTS_POSTING_DETAILS                   
 WHERE ACCOUNT_ID = @ACCOUNT_ID                  
 ) AS GL_BALANCE,                  
 --CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(@TOTAL_CHECKS + @TOTAL_DEPOSITS + @TOTAL_JE + @TOTAL_OTHER,0)),1) AS TOTAL_OUTSTANDNIG,                  
 CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(@TOTAL_CHECKS + @TOTAL_DEPOSITS + @TOTAL_JE + @TOTAL_OTHER,0)),1) AS TOTAL_OUTSTANDNIG,             
          
 BANK_CHARGES_CREDITS AS BANK_CHARGES,                  
 (                 
 CONVERT(VARCHAR(30),              
 CONVERT(MONEY,              
          
 (SELECT               
 ISNULL(SUM(TRANSACTION_AMOUNT),0)               
 --CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(ISNULL(SUM(TRANSACTION_AMOUNT),0),0)),1)              
 FROM ACT_ACCOUNTS_POSTING_DETAILS                   
 WHERE ACCOUNT_ID = @ACCOUNT_ID)                  
 - BANK_CHARGES_CREDITS - (@TOTAL_CHECKS + @TOTAL_DEPOSITS + @TOTAL_JE + @TOTAL_OTHER)                  
 )             
 ,1))AS CALC_BALANCE , 
--Statement_date added For itrack Issue #5921.
CONVERT(varchar,STATEMENT_DATE,101) as STATEMENT_DATE           
 FROM ACT_BANK_RECONCILIATION                  
 WHERE AC_RECONCILIATION_ID = @AC_RECONCILIATION_ID                  
          
 select @TOTAL_CHECKS , @TOTAL_DEPOSITS , @TOTAL_JE , @TOTAL_OTHER               
          
END                  
ELSE -- Recon Details                   
BEGIN     
 -- Sum of checks                  
 SELECT @TOTAL_CHECKS = ISNULL(SUM(ISNULL(PD.TRANSACTION_AMOUNT,0)),0)                  
 FROM ACT_ACCOUNTS_POSTING_DETAILS PD                  
 WHERE PD.ACCOUNT_ID = @ACCOUNT_ID                  
 AND PD.UPDATED_FROM = 'C'                  
 AND ISNULL(PD.IS_BANK_RECONCILED,'N')='N'                   
 AND PD.IDENTITY_ROW_ID  IN                 
  (                  
  SELECT IDENTITY_ROW_ID FROM ACT_BANK_RECONCILIATION_ITEMS_DETAILS                   
  WHERE AC_RECONCILIATION_ID= @AC_RECONCILIATION_ID                  
  )                  
                
 IF(@TOTAL_CHECKS <> 0)                  
 BEGIN                   
  INSERT INTO #TEMP_DETAIL                  
  (                  
  ROW_LEVEL,UPDATED_FROM,SOURCE_TRAN_DATE,IDENTITY_ROW_ID,                  
  SOURCE_NUMBER,TRANSACTION_AMOUNT,TRANSACTION_TYPE,                  
NOTEMEMO                  
  )                   
  VALUES( 'H' ,'C',' ' ,' ' ,                  
  ' ' ,@TOTAL_CHECKS ,'Checks' ,                  
  ' ' )                  
                   
                   
  -- Checks                   
  INSERT INTO #TEMP_DETAIL                  
  (                  
  ROW_LEVEL,UPDATED_FROM,SOURCE_TRAN_DATE,IDENTITY_ROW_ID,                  
  SOURCE_NUMBER,TRANSACTION_AMOUNT,TRANSACTION_TYPE,                  
  NOTEMEMO                  
  )                   
  SELECT                   
  'R' ,PD.UPDATED_FROM ,CONVERT(VARCHAR,PD.SOURCE_TRAN_DATE,101) ,PD.IDENTITY_ROW_ID,                  
  PD.SOURCE_NUM , PD.TRANSACTION_AMOUNT,'Check',                  
  PD.ADNL_INFO                   
  FROM ACT_ACCOUNTS_POSTING_DETAILS PD                  
  WHERE PD.ACCOUNT_ID = @ACCOUNT_ID                  
  AND PD.UPDATED_FROM = 'C'                  
  AND ISNULL(PD.IS_BANK_RECONCILED,'N')='N'                   
AND PD.IDENTITY_ROW_ID  IN                 
   (                  
   SELECT IDENTITY_ROW_ID FROM ACT_BANK_RECONCILIATION_ITEMS_DETAILS                   
   WHERE AC_RECONCILIATION_ID= @AC_RECONCILIATION_ID                  
   )                  
 END                
                
                
 -- Sum of Deposits                  
 SELECT @TOTAL_DEPOSITS = ISNULL(SUM(ISNULL(PD.TRANSACTION_AMOUNT,0)),0)                  
 FROM ACT_ACCOUNTS_POSTING_DETAILS PD                  
 WHERE PD.ACCOUNT_ID = @ACCOUNT_ID                  
 AND PD.UPDATED_FROM = 'D'                  
 AND ISNULL(PD.IS_BANK_RECONCILED,'N')='N'                   
 AND PD.IDENTITY_ROW_ID  IN                 
  (                  
  SELECT IDENTITY_ROW_ID FROM ACT_BANK_RECONCILIATION_ITEMS_DETAILS             WHERE AC_RECONCILIATION_ID= @AC_RECONCILIATION_ID                 
  )                  
                
 IF(@TOTAL_DEPOSITS <> 0)                  
 BEGIN                   
  INSERT INTO #TEMP_DETAIL                  
  (                  
  ROW_LEVEL,UPDATED_FROM,SOURCE_TRAN_DATE,IDENTITY_ROW_ID,                  
  SOURCE_NUMBER,TRANSACTION_AMOUNT,TRANSACTION_TYPE,                  
  NOTEMEMO                  
  )                   
  VALUES(                  
  'H' ,'D' ,' ' ,' ' ,                  
  ' ',@TOTAL_DEPOSITS ,'Deposits',                   
  ' ' )                  
                   
                  
  -- Deposits                   
  INSERT INTO #TEMP_DETAIL                  
  (                  
  ROW_LEVEL,UPDATED_FROM,SOURCE_TRAN_DATE,IDENTITY_ROW_ID,                  
  SOURCE_NUMBER,TRANSACTION_AMOUNT,TRANSACTION_TYPE,                  
  NOTEMEMO                  
  )                   
  SELECT                   
  'R' ,PD.UPDATED_FROM ,CONVERT(VARCHAR,PD.SOURCE_TRAN_DATE,101) ,PD.IDENTITY_ROW_ID,                  
  PD.SOURCE_NUM ,PD.TRANSACTION_AMOUNT,'Deposit' ,                  
  PD.ADNL_INFO                   
  FROM ACT_ACCOUNTS_POSTING_DETAILS PD                  
  WHERE PD.ACCOUNT_ID = @ACCOUNT_ID                  
  AND PD.UPDATED_FROM = 'D'                  
  AND ISNULL(PD.IS_BANK_RECONCILED,'N')='N'                   
  AND PD.IDENTITY_ROW_ID  IN                 
   (                  
   SELECT IDENTITY_ROW_ID FROM ACT_BANK_RECONCILIATION_ITEMS_DETAILS                   
   WHERE AC_RECONCILIATION_ID= @AC_RECONCILIATION_ID                  
   )                  
 END                
                   
                   
 -- Sum of JEs                  
 SELECT @TOTAL_JE = ISNULL(SUM(ISNULL(PD.TRANSACTION_AMOUNT,0)),0)                  
 FROM ACT_ACCOUNTS_POSTING_DETAILS PD                  
 WHERE PD.ACCOUNT_ID = @ACCOUNT_ID                  
 AND PD.UPDATED_FROM = 'J'                  
 AND ISNULL(PD.IS_BANK_RECONCILED,'N')='N'                   
 AND PD.IDENTITY_ROW_ID  IN                 
  (                  
  SELECT IDENTITY_ROW_ID FROM ACT_BANK_RECONCILIATION_ITEMS_DETAILS                   
  WHERE AC_RECONCILIATION_ID= @AC_RECONCILIATION_ID                  
  )                  
                   
 IF(@TOTAL_JE <> 0)        
 BEGIN                   
  INSERT INTO #TEMP_DETAIL                  
  (                  
  ROW_LEVEL,UPDATED_FROM,SOURCE_TRAN_DATE,IDENTITY_ROW_ID,                  
  SOURCE_NUMBER,TRANSACTION_AMOUNT,TRANSACTION_TYPE,                  
  NOTEMEMO                  
  )                   
  VALUES(                  
  'H' ,'J' ,' ' ,' ' ,                  
  ' ',@TOTAL_JE ,'Journal Entries',                   
  ' ' )                  
                   
                  
 -- Journal Entries                  
  INSERT INTO #TEMP_DETAIL          
  (                  
  ROW_LEVEL,UPDATED_FROM,SOURCE_TRAN_DATE,IDENTITY_ROW_ID,                  
  SOURCE_NUMBER,TRANSACTION_AMOUNT,TRANSACTION_TYPE,                  
  NOTEMEMO                  
  )                   
  SELECT                   
  'R' ,PD.UPDATED_FROM ,CONVERT(VARCHAR,PD.SOURCE_TRAN_DATE,101) ,PD.IDENTITY_ROW_ID,                  
  PD.SOURCE_NUM ,PD.TRANSACTION_AMOUNT,'Journal Entries' ,                  
  PD.ADNL_INFO                   
  FROM ACT_ACCOUNTS_POSTING_DETAILS PD                  
  WHERE PD.ACCOUNT_ID = @ACCOUNT_ID                  
  AND PD.UPDATED_FROM = 'J'                  
  AND ISNULL(PD.IS_BANK_RECONCILED,'N')='N'                   
  AND PD.IDENTITY_ROW_ID IN                 
   (                  
   SELECT IDENTITY_ROW_ID FROM ACT_BANK_RECONCILIATION_ITEMS_DETAILS                   
   WHERE AC_RECONCILIATION_ID= @AC_RECONCILIATION_ID                  
   )                  
 END                
                  
 --Other Entries Total                  
 SELECT @TOTAL_OTHER = ISNULL(SUM(ISNULL(PD.TRANSACTION_AMOUNT,0)),0)                  
 FROM ACT_ACCOUNTS_POSTING_DETAILS PD                  
 WHERE PD.ACCOUNT_ID = @ACCOUNT_ID                  
 AND PD.UPDATED_FROM NOT IN ('J','D','C')                  
 AND ISNULL(PD.IS_BANK_RECONCILED,'N')='N'                   
 AND PD.IDENTITY_ROW_ID IN                 
  (                  
  SELECT IDENTITY_ROW_ID FROM ACT_BANK_RECONCILIATION_ITEMS_DETAILS                   
  WHERE AC_RECONCILIATION_ID= @AC_RECONCILIATION_ID                  
  )                  
            
 IF(@TOTAL_OTHER <> 0)                  
 BEGIN                   
  INSERT INTO #TEMP_DETAIL                  
  (                  
  ROW_LEVEL,UPDATED_FROM,SOURCE_TRAN_DATE,IDENTITY_ROW_ID,                  
  SOURCE_NUMBER,TRANSACTION_AMOUNT,TRANSACTION_TYPE,                  
  NOTEMEMO                  
  )                   
  VALUES(                  
  'H' ,'O' ,' ' ,' ' ,                  
  ' ',@TOTAL_OTHER ,'Other Entries',                   
  ' ' )                  
                   
                  
  -- Other Entries                  
  INSERT INTO #TEMP_DETAIL                  
  (                  
  ROW_LEVEL,UPDATED_FROM,SOURCE_TRAN_DATE,IDENTITY_ROW_ID,                  
  SOURCE_NUMBER,TRANSACTION_AMOUNT,TRANSACTION_TYPE,                  
  NOTEMEMO                  
  )                   
  SELECT                   
  'R' ,PD.UPDATED_FROM ,CONVERT(VARCHAR,PD.SOURCE_TRAN_DATE,101) ,PD.IDENTITY_ROW_ID,                  
  PD.SOURCE_NUM ,PD.TRANSACTION_AMOUNT,'Other Entry' ,                  
  PD.ADNL_INFO                   
  FROM ACT_ACCOUNTS_POSTING_DETAILS PD                  
  WHERE PD.ACCOUNT_ID = @ACCOUNT_ID                  
  AND PD.UPDATED_FROM  NOT IN ('J','D','C')                  
  AND ISNULL(PD.IS_BANK_RECONCILED,'N')='N'                   
  AND PD.IDENTITY_ROW_ID IN       
   (                  
   SELECT IDENTITY_ROW_ID FROM ACT_BANK_RECONCILIATION_ITEMS_DETAILS                   
   WHERE AC_RECONCILIATION_ID= @AC_RECONCILIATION_ID                  
   )                  
 END                
                
 -- Record Set 1     
   INSERT INTO #FINAL_DETAIL                  
 SELECT  ROW_LEVEL,UPDATED_FROM ,              
   SOURCE_TRAN_DATE ,          
   IDENTITY_ROW_ID,                  
  SOURCE_NUMBER AS SOURCE_NUM,            
   TRANSACTION_AMOUNT,            
  TRANSACTION_TYPE  AS TRANSACTIONTYPE,                  
  NOTEMEMO                   
 FROM  #TEMP_DETAIL            
        
 WHERE --ISNUMERIC(ISNULL(SOURCE_NUMBER,'0')) =  1            
(ROW_LEVEL='H' or ROW_LEVEL='R') and UPDATED_FROM ='C'  and  isnumeric(SOURCE_NUMBER)=0           
 ORDER BY ROW_LEVEL ,SOURCE_TRAN_DATE,SOURCE_NUMBER         
        
 UPDATE #FINAL_DETAIL SET SOURCE_TRAN_DATE = NULL WHERE ROW_LEVEL='H'         
    
  INSERT INTO #FINAL_DETAIL                  
 SELECT  ROW_LEVEL,UPDATED_FROM ,              
   SOURCE_TRAN_DATE ,          
   IDENTITY_ROW_ID,                  
  SOURCE_NUMBER AS SOURCE_NUM,            
   TRANSACTION_AMOUNT,            
  TRANSACTION_TYPE  AS TRANSACTIONTYPE,                  
  NOTEMEMO                   
  FROM  #TEMP_DETAIL            
        
 WHERE --ISNUMERIC(ISNULL(SOURCE_NUMBER,'0')) =  1            
(ROW_LEVEL='H' or ROW_LEVEL='R') and UPDATED_FROM ='C'  and  isnumeric(SOURCE_NUMBER)<>0           
 ORDER BY ROW_LEVEL ,SOURCE_TRAN_DATE  ,CAST(SOURCE_NUMBER AS INT)         
         
 UPDATE #FINAL_DETAIL SET SOURCE_TRAN_DATE = NULL WHERE ROW_LEVEL='H'        
          
        
INSERT INTO #FINAL_DETAIL                         
SELECT  ROW_LEVEL,UPDATED_FROM ,              
 SOURCE_TRAN_DATE ,          
 IDENTITY_ROW_ID,                  
 SOURCE_NUMBER AS SOURCE_NUM,            
 TRANSACTION_AMOUNT,            
 TRANSACTION_TYPE  AS TRANSACTIONTYPE,             
 NOTEMEMO                   
 FROM  #TEMP_DETAIL            
 WHERE --ISNUMERIC(ISNULL(SOURCE_NUMBER,'0')) =  1            
 (ROW_LEVEL='H' or ROW_LEVEL='R') and UPDATED_FROM ='D'  and  isnumeric(SOURCE_NUMBER)= 0              
 ORDER BY ROW_LEVEL,SOURCE_TRAN_DATE  , SOURCE_NUMBER        
     
UPDATE #FINAL_DETAIL SET SOURCE_TRAN_DATE = NULL WHERE ROW_LEVEL='H'         
        
INSERT INTO #FINAL_DETAIL                         
SELECT  ROW_LEVEL,UPDATED_FROM ,              
 SOURCE_TRAN_DATE ,          
 IDENTITY_ROW_ID,                  
 SOURCE_NUMBER AS SOURCE_NUM,            
 TRANSACTION_AMOUNT,            
 TRANSACTION_TYPE  AS TRANSACTIONTYPE,                  
 NOTEMEMO                   
 FROM  #TEMP_DETAIL            
 WHERE --ISNUMERIC(ISNULL(SOURCE_NUMBER,'0')) =  1            
 (ROW_LEVEL='H' or ROW_LEVEL='R') and UPDATED_FROM ='D' and  isnumeric(SOURCE_NUMBER)<>0                
 ORDER BY ROW_LEVEL,SOURCE_TRAN_DATE,CAST(SOURCE_NUMBER AS INT) --, ISNULL(SOURCE_NUMBER,'0')         
      
UPDATE #FINAL_DETAIL SET SOURCE_TRAN_DATE = NULL WHERE ROW_LEVEL='H'       
        
INSERT INTO #FINAL_DETAIL              
SELECT  ROW_LEVEL,UPDATED_FROM ,              
SOURCE_TRAN_DATE ,          
IDENTITY_ROW_ID,                  
SOURCE_NUMBER AS SOURCE_NUM,            
TRANSACTION_AMOUNT,            
TRANSACTION_TYPE  AS TRANSACTIONTYPE,                  
NOTEMEMO                   
FROM  #TEMP_DETAIL            
WHERE --ISNUMERIC(ISNULL(SOURCE_NUMBER,'0')) =  1            
(ROW_LEVEL='H' or ROW_LEVEL='R') and UPDATED_FROM ='J' and  isnumeric(SOURCE_NUMBER)= 0                  
ORDER BY ROW_LEVEL,SOURCE_NUMBER  ,SOURCE_TRAN_DATE--, ISNULL(SOURCE_NUMBER,'0')        
        
UPDATE #FINAL_DETAIL SET SOURCE_TRAN_DATE = NULL WHERE ROW_LEVEL='H'      
       
INSERT INTO #FINAL_DETAIL                         
SELECT  ROW_LEVEL,UPDATED_FROM ,              
SOURCE_TRAN_DATE ,          
IDENTITY_ROW_ID,                  
SOURCE_NUMBER AS SOURCE_NUM,            
TRANSACTION_AMOUNT,            
TRANSACTION_TYPE  AS TRANSACTIONTYPE,                  
NOTEMEMO                   
FROM  #TEMP_DETAIL            
WHERE --ISNUMERIC(ISNULL(SOURCE_NUMBER,'0')) =  1            
(ROW_LEVEL='H' or ROW_LEVEL='R') and UPDATED_FROM ='J'  and  isnumeric(SOURCE_NUMBER)<> 0               
ORDER BY ROW_LEVEL,SOURCE_TRAN_DATE,CAST(SOURCE_NUMBER AS INT) --, ISNULL(SOURCE_NUMBER,'0')        
        
 UPDATE #FINAL_DETAIL SET SOURCE_TRAN_DATE = NULL WHERE ROW_LEVEL='H'        
        
INSERT INTO #FINAL_DETAIL                         
SELECT  ROW_LEVEL,UPDATED_FROM ,              
 SOURCE_TRAN_DATE ,          
 IDENTITY_ROW_ID,                  
 SOURCE_NUMBER AS SOURCE_NUM,            
 TRANSACTION_AMOUNT,            
 TRANSACTION_TYPE  AS TRANSACTIONTYPE,                  
 NOTEMEMO                   
FROM  #TEMP_DETAIL            
 WHERE --ISNUMERIC(ISNULL(SOURCE_NUMBER,'0')) =  1            
(ROW_LEVEL='H' or ROW_LEVEL='R') and UPDATED_FROM  IN ('I','O')  and  isnumeric(SOURCE_NUMBER)= 0            
ORDER BY ROW_LEVEL, SOURCE_TRAN_DATE , SOURCE_NUMBER  --, ISNULL(SOURCE_NUMBER,'0')         
        
INSERT INTO #FINAL_DETAIL                         
SELECT  ROW_LEVEL,UPDATED_FROM ,              
 SOURCE_TRAN_DATE ,          
 IDENTITY_ROW_ID,                  
 SOURCE_NUMBER AS SOURCE_NUM,            
 TRANSACTION_AMOUNT,            
 TRANSACTION_TYPE  AS TRANSACTIONTYPE,                  
 NOTEMEMO                   
FROM  #TEMP_DETAIL            
 WHERE --ISNUMERIC(ISNULL(SOURCE_NUMBER,'0')) =  1            
(ROW_LEVEL='H' or ROW_LEVEL='R') and UPDATED_FROM IN ('I','O') and  isnumeric(SOURCE_NUMBER)<> 0               
ORDER BY ROW_LEVEL,SOURCE_TRAN_DATE,CAST(SOURCE_NUMBER AS INT)  --, ISNULL(SOURCE_NUMBER,'0')      
    
UPDATE #FINAL_DETAIL SET SOURCE_TRAN_DATE = NULL WHERE ROW_LEVEL='H'      
    
select          
 [IDENT_COL]  ,                        
 [ROW_LEVEL] ,          
 CONVERT(VARCHAR,SOURCE_TRAN_DATE,101) AS SOURCE_TRAN_DATE ,                
 [UPDATED_FROM] ,                             
 [IDENTITY_ROW_ID] ,                  
 [SOURCE_NUMBER] as SOURCE_NUM ,                
 --[TRANSACTION_AMOUNT] as TRANSACTION_AMOUNT ,                  
 CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(TRANSACTION_AMOUNT,0)),1) AS TRANSACTION_AMOUNT,        
 [TRANSACTION_TYPE] as TRANSACTIONTYPE ,                  
 [NOTEMEMO] as NoteMemo           
  from #FINAL_DETAIL ORDER BY IDENT_COL         
        
    
    /*INSERT INTO #FINAL_DETAIL                         
 SELECT  ROW_LEVEL,UPDATED_FROM ,              
    SOURCE_TRAN_DATE ,          
    IDENTITY_ROW_ID,                  
 SOURCE_NUMBER AS SOURCE_NUM,            
 TRANSACTION_AMOUNT,            
 TRANSACTION_TYPE  AS TRANSACTIONTYPE,                  
 NOTEMEMO                   
 FROM  #TEMP_DETAIL            
 WHERE ISNUMERIC(ISNULL(SOURCE_NUMBER,'0')) =  1            
          
          
 INSERT INTO #RECORDS_TO_DISPLAY          
 SELECT                   
 ROW_LEVEL,UPDATED_FROM ,               
    SOURCE_TRAN_DATE ,          
    IDENTITY_ROW_ID,                  
 SOURCE_NUMBER AS SOURCE_NUM,            
 TRANSACTION_AMOUNT ,          
 TRANSACTION_TYPE  AS TRANSACTIONTYPE,                  
 NOTEMEMO                   
 FROM #FINAL_DETAIL          
 ORDER BY UPDATED_FROM,SOURCE_TRAN_DATE, CAST(ISNULL(SOURCE_NUMBER,'0') AS INT)            
          
 INSERT INTO #RECORDS_TO_DISPLAY          
 SELECT  ROW_LEVEL,UPDATED_FROM ,                
    SOURCE_TRAN_DATE ,          
    IDENTITY_ROW_ID,                  
 SOURCE_NUMBER AS SOURCE_NUM,            
 TRANSACTION_AMOUNT,            
 TRANSACTION_TYPE  AS TRANSACTIONTYPE,                  
 NOTEMEMO                   
 FROM  #TEMP_DETAIL            
 WHERE ISNUMERIC(ISNULL(SOURCE_NUMBER,'0')) <>  1            
 ORDER BY UPDATED_FROM,SOURCE_TRAN_DATE, SOURCE_NUMBER          
          
 SELECT                   
 ROW_LEVEL,UPDATED_FROM ,          
    CONVERT(varchar,SOURCE_TRAN_DATE,101) as  SOURCE_TRAN_DATE ,               
    IDENTITY_ROW_ID,                  
 SOURCE_NUMBER AS SOURCE_NUM,            
 CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(TRANSACTION_AMOUNT,0)),1) AS TRANSACTION_AMOUNT,             
 TRANSACTION_TYPE  AS TRANSACTIONTYPE,                  
 NOTEMEMO                   
 FROM #RECORDS_TO_DISPLAY          
 ORDER BY IDENT_COL*/           
          
--Comment For Itrack Issue #5205                  
 --ORDER BY IDENT_COL                  
                  
--  --fetching details to be displayed on top of grid                  
--  SELECT                   
--  (                  
--  SELECT CONVERT(VARCHAR,ACC_DISP_NUMBER)+': '+ACC_DESCRIPTION  FROM ACT_GL_ACCOUNTS                   
--  WHERE ACCOUNT_ID=@ACCOUNT_ID                  
--  ) AS ACC_DESCRIPTION,         
--  STARTING_BALANCE,                  
--  ENDING_BALANCE,                  
--  BANK_CHARGES_CREDITS AS BANK_CHARGES,                  
--  (                  
--  ISNULL(STARTING_BALANCE,0) + (                  
--    SELECT ISNULL(SUM(TRANSACTION_AMOUNT),0) FROM ACT_ACCOUNTS_POSTING_DETAILS                   
--    WHERE IDENTITY_ROW_ID IN (                  
--      SELECT IDENTITY_ROW_ID FROM ACT_BANK_RECONCILIATION_ITEMS_DETAILS                   
--      WHERE AC_RECONCILIATION_ID=@AC_RECONCILIATION_ID)                  
--    )                  
--  -ISNULL(ENDING_BALANCE,0)                  
--  -ISNULL(BANK_CHARGES_CREDITS,0)                  
--  )                   
--  AS "DIFFERENCE"                  
--  FROM ACT_BANK_RECONCILIATION                  
--  WHERE AC_RECONCILIATION_ID = @AC_RECONCILIATION_ID                  
                
--Dispaly Detail Top                
 SELECT                   
 (                  
 --SELECT CONVERT(VARCHAR,ACC_DISP_NUMBER)+': '+ACC_DESCRIPTION  FROM ACT_GL_ACCOUNTS                   
 SELECT CONVERT(VARCHAR,ACC_NUMBER) FROM ACT_GL_ACCOUNTS                   
 WHERE ACCOUNT_ID=@ACCOUNT_ID                  
 ) AS ACC_DESCRIPTION,                  
 CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(STARTING_BALANCE,0)),1) AS STARTING_BALANCE,                  
 CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(ENDING_BALANCE,0)),1) AS ENDING_BALANCE,                 
 (                  
 SELECT               
CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(ISNULL(SUM(TRANSACTION_AMOUNT),0),0)),1)              
--ISNULL(SUM(TRANSACTION_AMOUNT),0)               
FROM ACT_ACCOUNTS_POSTING_DETAILS                   
 WHERE ACCOUNT_ID = @ACCOUNT_ID         
 )  GL_BALANCE,                  
 CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(@TOTAL_CHECKS + @TOTAL_DEPOSITS + @TOTAL_JE + @TOTAL_OTHER,0)),1) AS TOTAL_OUTSTANDNIG,                  
 BANK_CHARGES_CREDITS AS BANK_CHARGES,                  
 (                
CONVERT(VARCHAR(30),              
CONVERT(MONEY,              
    
 (SELECT               
ISNULL(SUM(TRANSACTION_AMOUNT),0)               
FROM ACT_ACCOUNTS_POSTING_DETAILS                    
WHERE ACCOUNT_ID = @ACCOUNT_ID)                  
 - BANK_CHARGES_CREDITS - (@TOTAL_CHECKS + @TOTAL_DEPOSITS + @TOTAL_JE + @TOTAL_OTHER)                  
 )              
,1))              
              
              
 AS CALC_BALANCE ,
--Statement_date added For itrack Issue #5921.
CONVERT(varchar,STATEMENT_DATE,101) as STATEMENT_DATE           


 

                 
              
              
 FROM ACT_BANK_RECONCILIATION                  
 WHERE AC_RECONCILIATION_ID = @AC_RECONCILIATION_ID                   
                  
END                  
              
    
--UPDATE #TEMP_DETAIL SET SOURCE_TRAN_DATE = NULL WHERE ROW_LEVEL='H'        
--select * from #FINAL_DETAIL    
--UPDATE #FINAL_DETAIL SET SOURCE_TRAN_DATE = NULL WHERE ROW_LEVEL='H'       
--UPDATE #TEMP_DETAIL  SET SOURCE_TRAN_DATE = NULL WHERE ROW_LEVEL='H'    
--select * from #FINAL_DETAIL WHERE ROW_LEVEL='H'      
--UPDATE #TEMP_DETAIL SET SOURCE_TRAN_DATE = NULL WHERE ROW_LEVEL='H'      
--select * from #TEMP_DETAIL WHERE ROW_LEVEL='H'      
    
DROP TABLE #TEMP_DETAIL                 
DROP TABLE #FINAL_DETAIL               
DROP TABLE #RECORDS_TO_DISPLAY           
          
END               
--go              
--execute DBO.[Proc_GetBankReconDetails] 134,3,'OUTSTANDING'              
--------              
--rollback tran                 
                  
  
  
           
                
                


GO

