IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetGLAccDetailsGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetGLAccDetailsGroup]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



/*----------------------------------------------------------                    
   
Proc Name        : dbo.Proc_GetGLAccDetailsGroup                    
Created by       : Sukhveer Singh                    
Date             : 29/11/2006                    
Purpose          : Get Account Detail Information  (Based on transaction type)                    
Revison History  :                    
Used In          : Wolverine                    
    
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/                    
    
--DROP PROC dbo.Proc_GetGLAccDetailsGroup              
--dbo.Proc_GetGLAccDetails  NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'9,2006',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL        
--Proc_GetGLAccDetails  NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL 
--Proc_GetGLAccDetails  NULL,NULL,'1/17/2007','1/17/2007',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL        
--Proc_GetGLAccDetails  NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'22220.00', '22220.20'            
--Proc_GetGLAccDetails  NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'42500.00', '42500.22'            
--Proc_GetGLAccDetails  NULL,NULL,NULL,NULL,'256,262',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL        
--Proc_GetGLAccDetails  NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'11.11','25.25',NULL,NULL,NULL,NULL 
--Proc_GetGLAccDetails  1,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'11.11','25.25',NULL,NULL,NULL,NULL  
--Proc_GetGLAccDetails  NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'43,44',NULL,NULL,NULL,NULL,NULL,NULL,NULL       
  
CREATE PROC dbo.Proc_GetGLAccDetailsGroup                    
(                    
@FROMSOURCE VARCHAR(8000) = NULL,                    
@TOSOURCE INT = NULL,                    
@FROMDATE DATETIME = NULL,                    
@TODATE DATETIME = NULL,                    
@ACCOUNT_ID VARCHAR(8000) = NULL,                    
@UPDATED_FROM NCHAR(1)= NULL,                    
@STATE VARCHAR(8000) = NULL,                    
@LOB VARCHAR(8000) = NULL,              
    
-- Input value for YEARMONTH is as '06,2006' (month 06 and year 2006)      
    
-- YEARMONTH Will work, if FROMDATE and @TODATE is NULL      
    
@YEARMONTH VARCHAR(200) = NULL,              
@VENDORID VARCHAR(8000) = NULL,              
@CUSTOMERID VARCHAR(8000) = NULL,              
@FROMAMT VARCHAR(10) = NULL,              
@TOAMT VARCHAR(10) = NULL,              
@TRANSTYPE VARCHAR(8000)= NULL,              
@POLICYID VARCHAR(8000) = NULL,
@FROMACT VARCHAR(200) = NULL,
@TOACT VARCHAR(200) = NULL,               
    
          
    
/* Variables used to store month and year*/              
    
@MMONTH int = NULL,              
@MYEAR INT =NULL              
    
   
)                    
    
AS                    
    
BEGIN                    
    
        
    
DECLARE @QUERY VARCHAR(8000)               
    
              
    
SELECT @MMONTH = SUBSTRING(@YEARMONTH,1,CHARINDEX( ',', @YEARMONTH)-1)              
SELECT @MYEAR = SUBSTRING(@YEARMONTH,CHARINDEX( ',', @YEARMONTH)+1,LEN(@YEARMONTH))              
SELECT @TRANSTYPE = REPLACE(@TRANSTYPE,',',''',''')  
SELECT @FROMSOURCE = REPLACE(@FROMSOURCE,',',''',''')              

--substring(acc_disp_number,1,charindex(''.'',acc_disp_number)-1) as Disp_number,ACC_Parent_id,GLA.account_id,ACC_DESCRIPTION,
              
    
SET @QUERY = 'SELECT substring(acc_disp_number,1,charindex(''.'',acc_disp_number)-1) as Disp_number,ACC_Parent_id,GLA.account_id,ACC_DESCRIPTION,ISNULL(CONVERT(VARCHAR,ACC_DISP_NUMBER)+'' - ''+ ACC_DESCRIPTION,'''') AS ACCOUNT,          
ISNULL(CONVERT(VARCHAR,SOURCE_TRAN_DATE,101),'''') AS SOURCE_TRAN_DATE,ISNULL(TRAN_ENTITY,'''') AS NAME,          
ISNULL(TRAN_DESC,'''') AS DESCRIPTION,ISNULL(TRANSACTION_AMOUNT,0) TRANSACTION_AMOUNT,          
CASE UPDATED_FROM WHEN ''J'' THEN ''JOURNAL''                    
WHEN ''D'' THEN ''DEPOSIT''                    
WHEN ''C'' THEN ''CHECK''                
WHEN ''P'' THEN ''PREMIUM''                 
WHEN ''I'' THEN ''INVOICE''                 
END                    
AS UPDATED_FROM,ISNULL(SOURCE_NUM,'''') AS SOURCE_NUM    
FROM ACT_GL_ACCOUNTS GLA                     
LEFT JOIN ACT_ACCOUNTS_POSTING_DETAILS APD ON APD.ACCOUNT_ID = GLA.ACCOUNT_ID'       
    
    
DECLARE @WHERE VARCHAR(2000)                    
SET @WHERE = ''                      
    
IF NOT @FROMSOURCE IS NULL            
BEGIN            
  SET @WHERE = ' SOURCE_NUM IN (''' + @FROMSOURCE + ''')'            
END   
                
    
IF NOT @TOSOURCE IS NULL                    
BEGIN                    
 if ltrim(rtrim(@WHERE)) = ''                
   SET @WHERE = ' SOURCE_NUM <=' + CONVERT(VARCHAR,@TOSOURCE)                    
 else                
  SET @WHERE = @WHERE + ' AND SOURCE_NUM <= ' + CONVERT(VARCHAR,@TOSOURCE)                 
END                    
    
--AND cast((Convert(varchar,ACOI.SOURCE_EFF_DATE,101)) as datetime)  BETWEEN  @FROMDATE AND @ToDate
IF NOT @FROMDATE IS NULL AND NOT @TODATE IS NULL             
BEGIN                    
 if ltrim(rtrim(@WHERE)) = ''                
  SET @WHERE =   ' cast((Convert(varchar,SOURCE_TRAN_DATE,101)) as datetime) BETWEEN ''' 
	+ CONVERT(VARCHAR,(cast((Convert(varchar,@FROMDATE,101)) as datetime))) 
	+ ''' AND ''' + CONVERT(VARCHAR,(cast((Convert(varchar,@TODATE,101)) as datetime)))  + ''''
 else                
	  SET @WHERE = @WHERE  +   ' AND cast((Convert(varchar,SOURCE_TRAN_DATE,101)) as datetime) BETWEEN ' + @FROMDATE + 'AND ' + @ToDate                    
	
END
                    
    
   
IF NOT @ACCOUNT_ID IS NULL                    
BEGIN                    
 if ltrim(rtrim(@WHERE)) = ''                
  SET @WHERE =  ' GLA.ACCOUNT_ID IN ('+ @ACCOUNT_ID +')'              
 else                
  SET @WHERE =  @WHERE + ' AND GLA.ACCOUNT_ID IN ('+ @ACCOUNT_ID +')'                    
END                    
    
IF NOT @UPDATED_FROM IS NULL                    
BEGIN                    
 if ltrim(rtrim(@WHERE)) = ''                
   SET @WHERE = 'UPDATED_FROM =''' + @UPDATED_FROM + ''''                    
 else                
  SET @WHERE = @WHERE  + ' AND UPDATED_FROM = ''' + @UPDATED_FROM + ''''                    
END                   
    
   
IF NOT @STATE IS NULL                    
BEGIN                    
 if ltrim(rtrim(@WHERE)) = ''                
   SET @WHERE = 'APD.STATE_ID IN (' + @STATE +')'                    
 else                
  SET @WHERE = @WHERE  + ' AND APD.STATE_ID IN (' + @STATE +')'                    
END               
    
IF NOT @LOB IS NULL                    
BEGIN                    
 if ltrim(rtrim(@WHERE)) = ''                
  SET @WHERE = 'APD.LOB_ID IN ('+ @LOB +')'              
 else                
  SET @WHERE = @WHERE  + ' AND APD.LOB_ID IN ('+ @LOB +')'                    
END               
    
    
IF @FROMDATE IS NULL AND @TODATE IS NULL              
BEGIN              
 IF NOT @YEARMONTH IS NULL                    
 BEGIN                    
  if ltrim(rtrim(@WHERE)) = ''                
   SET @WHERE ='MONTH(SOURCE_TRAN_DATE) = ' + CONVERT(VARCHAR, @MMONTH) + ' AND YEAR (SOURCE_TRAN_DATE) = ' + CONVERT(VARCHAR, @MYEAR)              
  else                
   SET @WHERE = @WHERE + ' AND MONTH(SOURCE_TRAN_DATE) = ' + CONVERT(VARCHAR, @MMONTH) + ' AND YEAR (SOURCE_TRAN_DATE) = ' + CONVERT(VARCHAR, @MYEAR)              
 END              
END              
              
    
IF NOT @VENDORID IS NULL                    
BEGIN                    
 if ltrim(rtrim(@WHERE)) = ''                
  SET @WHERE = 'APD.VENDOR_ID IN ('+ @VENDORID + ')'                   
 else                
  SET @WHERE = @WHERE  + ' AND APD.VENDOR_ID IN ('+ @VENDORID + ')'              
END              
    
IF NOT @CUSTOMERID IS NULL                    
BEGIN                    
 if ltrim(rtrim(@WHERE)) = ''                
  SET @WHERE = 'APD.CUSTOMER_ID IN (' + @CUSTOMERID + ')'              
 else                
  SET @WHERE = @WHERE  + ' AND APD.CUSTOMER_ID IN (' + @CUSTOMERID + ')'              
END                
    

IF NOT @FROMAMT IS NULL  AND NOT @TOAMT IS NULL                   
BEGIN                    
 if ltrim(rtrim(@WHERE)) = ''                
  SET @WHERE = 'APD.TRANSACTION_AMOUNT >= '+ @FROMAMT + ' AND APD.TRANSACTION_AMOUNT <= ' + @TOAMT                    
 else                
  SET @WHERE = @WHERE  + ' AND APD.TRANSACTION_AMOUNT >= '+ @FROMAMT + ' AND APD.TRANSACTION_AMOUNT <= ' + @TOAMT              
END             
    
              
    
IF NOT @TRANSTYPE IS NULL              
BEGIN                    
 if ltrim(rtrim(@WHERE)) = ''                
  SET @WHERE = 'APD.ITEM_TRAN_CODE_TYPE IN (''' + @TRANSTYPE + ''')'                    
 else                
  SET @WHERE = @WHERE  + ' AND APD.ITEM_TRAN_CODE_TYPE IN (''' + @TRANSTYPE + ''')'              
END              
    
          
    
IF NOT @POLICYID IS NULL              
BEGIN                    
 if ltrim(rtrim(@WHERE)) = ''                
  SET @WHERE = 'APD.POLICY_ID IN (' + @POLICYID + ')'              
 else                
  SET @WHERE = @WHERE  + ' AND APD.POLICY_ID IN (' + @POLICYID + ')'              
END 

IF @ACCOUNT_ID IS NULL AND (NOT @FROMACT IS NULL  AND NOT @TOACT IS NULL)
BEGIN            
 if ltrim(rtrim(@WHERE)) = ''        
  SET @WHERE =  ' CAST(GLA.ACC_DISP_NUMBER as Decimal(18,2)) >= '+ @FROMACT + ' AND CAST(GLA.ACC_DISP_NUMBER as Decimal(18,2)) <= ' + @TOACT      
 else        
  SET @WHERE = @WHERE + ' AND CAST(GLA.ACC_DISP_NUMBER as Decimal(18,2))  >= '+ @FROMACT 
	+ ' AND CAST(GLA.ACC_DISP_NUMBER as Decimal(18,2)) <= ' + @TOACT            
END              
    
  
IF(@WHERE <>'')                  
BEGIN                    
  SET @QUERY = @QUERY + ' WHERE ' + @WHERE                    
END                  
   
print @QUERY                
   
--EXECUTE(@QUERY)                    
    
END         
    
                  
    
       
  


    
    
      
    
  




GO

