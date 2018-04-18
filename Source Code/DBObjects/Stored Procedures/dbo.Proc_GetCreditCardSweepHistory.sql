IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCreditCardSweepHistory]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCreditCardSweepHistory]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- begin tran      
--drop PROC dbo.Proc_GetCreditCardSweepHistory                         
--go        
/*----------------------------------------------------------                                      
Proc Name       :  dbo.Proc_GetCreditCardSweepHistory                      
Created by      :  Ravindra                      
Date            :  07-02-2007                      
Purpose         :                        
Revison History :                                      
Used In         :  Wolverine                                      
                                   
exec dbo.Proc_GetCreditCardSweepHistory   null,null,null,null,NULL,null                
------------------------------------------------------------                                      
Date     Review By          Comments                                      
------   ------------       -------------------------*/                                      
     
CREATE PROC [dbo].[Proc_GetCreditCardSweepHistory]                     
(                      
 @FROM_DATE_SPOOL Datetime = null,                      
 @TO_DATE_SPOOL   DateTime = null,                      
 @FROM_DATE_SWEEP Datetime = null,                      
 @TO_DATE_SWEEP   DateTime = null,                    
 @PROCESS_STATUS  varchar(20)  = null,                
 @TRANSACTION_AMOUNT decimal(18,2) = null ,            
 @USERS varchar(1000) = null            
               
)                      
AS                      
BEGIN                      
DECLARE @QUERY VArchar(8000)                 
                      
SET @QUERY =                       
'SELECT     SPOOL.IDEN_ROW_ID ,                   
CASE SPOOL.ENTITY_TYPE WHEN ''CUST'' Then ''Customer''                       
 WHEN ''AGN'' Then ''Agency''                      
 WHEn ''VEN'' Then ''Vendor''                      
END AS ENTITY_TYPE,                        
ISNULL(CUST.CUSTOMER_FIRST_NAME,'''') + '' ''  +                       
ISNULL(CUST.CUSTOMER_MIDDLE_NAME,'''') + '' '' +                       
ISNULL(CUST.CUSTOMER_LAST_NAME,'''') AS CUSTOMER_NAME ,                  
ISNULL(MUL.USER_FNAME,'''') + '' '' + ISNULL(MUL.USER_LNAME,'''') AS USER_NAME ,                      
ISNULL(SPOOL.POLICY_NUMBER,'''') AS  POLICY_NUMBER,                      
CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL( SPOOL.TRANSACTION_AMOUNT,0)),1) AS AMOUNT,                      
SPOOL.CREATED_DATETIME  AS SPOOLED_DATETIME,                      
SPOOL.PROCESSED_DATETIME AS PROCESSED_DATETIME,                      
CASE SPOOL.PROCESSED WHEN ''Y'' THEN                      
 CASE ISNULL(SPOOL.PAYPAL_RESULT,-1) WHEN 0 THEN ''Done''   
               
  ELSE ''Failed'' END                      
ELSE ''failed''  
  
  
END AS STATUS,                      
SPOOL.PAY_PAL_REF_ID AS CONFIRMATION_ID ,                      
SPOOL.ERROR_DESCRIPTION AS ADDITIONAL_INFO,                      
ISNULL(CONVERT(VARCHAR,DEP.DEPOSIT_NUMBER),'''') AS DEPOSIT_NUMBER,                
ISNULL(SPOOL.NOTE,'''') AS NOTE                      
FROM EOD_CREDIT_CARD_SPOOL SPOOL                      
LEFT JOIN CLT_CUSTOMER_LIST CUST                      
ON SPOOL.ENTITY_ID  = CUST.CUSTOMER_ID                       
LEFT JOIN ACT_CURRENT_DEPOSITS DEP                      
ON DEP.DEPOSIT_ID = SPOOL.REF_DEPOSIT_ID                   
LEFT JOIN MNT_USER_LIST MUL                   
ON SPOOL.CREATED_BY = MUL.USER_ID                  
WHERE ISNULL(SPOOL.PROCESSED,''N'') <> ''N''                      
  '                      
                      
IF( @FROM_DATE_SPOOL IS NOT NULL)                      
BEGIN                       
 SET @QUERY   = @QUERY  +  ' AND CAST(CONVERT(VARCHAR,SPOOL.CREATED_DATETIME,101) AS DATETIME) >= '''                       
  + CONVERT(VARCHAR,@FROM_DATE_SPOOL) + ''''                      
END                      
                      
IF( @TO_DATE_SPOOL IS NOT NULL)                      
BEGIN                       
 SET @QUERY   = @QUERY  +  ' AND CAST(CONVERT(VARCHAR,SPOOL.CREATED_DATETIME,101) AS DATETIME) <= '''                       
  + CONVERT(VARCHAR,@TO_DATE_SPOOL) + ''''                      
END                      
                      
IF( @FROM_DATE_SWEEP IS NOT NULL)                      
BEGIN                       
 SET @QUERY   = @QUERY  +  ' AND CAST(CONVERT(VARCHAR,SPOOL.PROCESSED_DATETIME,101) AS DATETIME) >= '''                       
  + CONVERT(VARCHAR,@FROM_DATE_SWEEP) + ''''                      
END                      
                      
IF( @TO_DATE_SWEEP IS NOT NULL)              
BEGIN                       
 SET @QUERY   = @QUERY  +  ' AND CAST(CONVERT(VARCHAR,SPOOL.PROCESSED_DATETIME,101) AS DATETIME) <= '''                       
  + CONVERT(VARCHAR,@TO_DATE_SWEEP) + ''''                      
END                    
                    
 IF( @PROCESS_STATUS IS NOT NULL)                      
 BEGIN                    
   if(@PROCESS_STATUS = '0')                    
     SET @QUERY   = @QUERY  +  ' AND SPOOL.PAYPAL_RESULT = '+ @PROCESS_STATUS                    
   else                    
  --0 CHANGE TO -1 FOR ITRACK ISSUE #5294 AND ALSO APLLY NULL CHECK ON TOP 
  SET @QUERY   = @QUERY  +  ' AND ISNULL(SPOOL.PAYPAL_RESULT,-1) <> 0 '  
--Comment For Itrack Issue #5294 NOTE PART  
--OR SPOOL.PAYPAL_RESULT IS NULL'                    
      
 END  
       
IF(@TRANSACTION_AMOUNT IS NOT NULL)                
BEGIN                
 SET @QUERY   = @QUERY  + ' AND SPOOL.TRANSACTION_AMOUNT = ' + CONVERT(VARCHAR,@TRANSACTION_AMOUNT)                
END                
IF(@USERS IS NOT NULL)            
BEGIN            
SET @QUERY  = @QUERY + ' AND SPOOL.CREATED_BY IN (' + @USERS + ')'            
--SET @QUERY = @QUERY + 'AND SPOOL.CREATED_BY IN = '+@USERS            
END                
SET @QUERY   = @QUERY + '  ORDER BY SPOOL.IDEN_ROW_ID'           
                               
--PRINT @QUERY    
EXEC (@QUERY)    
         
               
                 
END                      
--               
--   go                  
-- exec Proc_GetCreditCardSweepHistory  null,null,'1-1-2009','2-9-2009',0,null,'260'               
--  rollback tran                 
                
                
                
                
                
                
                
                  
                  
                  
                  
                  
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                
                  
                
                
                
                
                
                
                
                
                
                
                
          
GO

