IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetGLAccDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetGLAccDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------                        
       
Proc Name        : dbo.Proc_GetGLAccDetails                        
Created by       : Sukhveer Singh                        
Date             : 29/11/2006                        
Purpose          : Get Account Detail Information  (Based on transaction type)                        
Revison History  :                        
Used In          : Wolverine                        
        
Modified By   : Swastika - 24th Jul'07 (iTrack #-2185)    
       Changed SOURCE_TRAN_DATE to POSTING_DATE    
------------------------------------------------------------                        
Date     Review By          Comments                        
------   ------------       -------------------------*/                        
--DROP PROC dbo.Proc_GetGLAccDetails      
--exec Proc_GetGLAccDetails  NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'H1001713'    
    
--Proc_GetGLAccDetails  NULL,NULL,NULL,NULL,NULL,29    
--Proc_GetGLAccDetails  NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,17910,17910    
    
--Proc_GetGLAccDetails  'SOURCE_TRAN_DATE1,SOURCE_NUM',NULL,NULL,NULL,NULL,3    
                   
--Proc_GetGLAccDetails null,null,null,'01/12/2008','01/22/2008',null,'C'  
CREATE PROC [dbo].[Proc_GetGLAccDetails]                      
(            
    
@ORDERBY varchar(100) = NULL,                   
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
@MYEAR INT =NULL,  
@POLICY_NUMBER varchar(8000)=null    
)                        
        
AS                        
BEGIN                        
     
DECLARE @QUERY VARCHAR(8000)                   
     
SELECT @MMONTH = SUBSTRING(@YEARMONTH,1,CHARINDEX( ',', @YEARMONTH)-1)                  
SELECT @MYEAR = SUBSTRING(@YEARMONTH,CHARINDEX( ',', @YEARMONTH)+1,LEN(@YEARMONTH))                  
SELECT @TRANSTYPE = REPLACE(@TRANSTYPE,',',''',''')      
SELECT @FROMSOURCE = REPLACE(@FROMSOURCE,',',''',''')                  
  
SET @QUERY = 'SELECT POL.POLICY_NUMBER,ISNULL(CONVERT(VARCHAR,ACC_DISP_NUMBER)+'' - ''+ ACC_DESCRIPTION,'''') AS ACCOUNT,              
ISNULL(CONVERT(DATETIME,CONVERT(VARCHAR,POSTING_DATE,109),101),'''') AS SOURCE_TRAN_DATE,CAST(CONVERT(VARCHAR,POSTING_DATE,101)AS DATETIME) AS SOURCE_TRAN_DATE1,ISNULL(TRAN_ENTITY,'''') AS NAME,              
ISNULL(TRAN_DESC,'''') AS DESCRIPTION,  
CONVERT(VARCHAR(30),CONVERT(MONEY,ISNULL(TRANSACTION_AMOUNT,0)),1) AS TRANSACTION_AMOUNT,     
CASE UPDATED_FROM WHEN ''J'' THEN ''JOURNAL''                        
WHEN ''D'' THEN ''DEPOSIT''                      
WHEN ''C'' THEN   
 CASE ITEM_TRAN_CODE WHEN ''VOID'' THEN ''VOID CHECK'' ELSE  
''CHECK'' END      
WHEN ''P'' THEN ''PREMIUM''      
WHEN ''I'' THEN ''INVOICE''    
WHEN ''L'' THEN ''CLAIM''    
WHEN ''F'' THEN ''FEES''    
WHEN ''W'' THEN ''PREMIUM WRITE OFF''                    
END                        
AS UPDATED_FROM,ISNULL(SOURCE_NUM,'''') AS SOURCE_NUM      
FROM ACT_GL_ACCOUNTS GLA                         
INNER JOIN ACT_ACCOUNTS_POSTING_DETAILS APD ON APD.ACCOUNT_ID = GLA.ACCOUNT_ID  
LEFT JOIN POL_CUSTOMER_POLICY_LIST POL ON APD.POLICY_ID = POL.POLICY_ID AND   
APD.POLICY_VERSION_ID = POL.POLICY_VERSION_ID AND APD.CUSTOMER_ID=POL.CUSTOMER_ID'          
        
        
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
        
    
/*IF NOT @FROMDATE IS NULL                 
BEGIN                        
 if ltrim(rtrim(@WHERE)) = ''                    
  SET @WHERE = 'POSTING_DATE >=''' + CONVERT(VARCHAR,@FROMDATE,101) + ''''                        
 else                    
  SET @WHERE = @WHERE + ' AND POSTING_DATE >= ''' + CONVERT(VARCHAR,@FROMDATE) + ''''                        
END                        
        
IF NOT @TODATE IS NULL                
BEGIN                        
 if ltrim(rtrim(@WHERE)) = ''                    
   SET @WHERE = 'POSTING_DATE <=''' + CONVERT(VARCHAR,@TODATE,101) + ''''                        
 else                    
  SET @WHERE = @WHERE + ' AND POSTING_DATE <= ''' + CONVERT(VARCHAR,@TODATE,101) + ''''                        
END */    
    
IF NOT @FROMDATE IS NULL                 
BEGIN                        
 if ltrim(rtrim(@WHERE)) = ''                    
  SET @WHERE = ' convert(datetime,convert(varchar,POSTING_DATE,101),101) >=''' + CONVERT(VARCHAR,@FROMDATE,101) + ''''                        
 else                    
  SET @WHERE = @WHERE + ' AND convert(datetime,convert(varchar,POSTING_DATE,101),101) >= ''' + CONVERT(VARCHAR,@FROMDATE) + ''''                        
END                        
        
IF NOT @TODATE IS NULL                
BEGIN                        
 if ltrim(rtrim(@WHERE)) = ''                    
   SET @WHERE = ' convert(datetime,convert(varchar,POSTING_DATE,101),101) <=''' + CONVERT(VARCHAR,@TODATE,101) + ''''                        
 else                    
  SET @WHERE = @WHERE + ' AND  convert(datetime,convert(varchar,POSTING_DATE,101),101) <= ''' + CONVERT(VARCHAR,@TODATE,101) + ''''                        
END     
      
     
IF NOT @ACCOUNT_ID IS NULL                        
BEGIN                        
 if ltrim(rtrim(@WHERE)) = ''                    
  SET @WHERE =  ' APD.ACCOUNT_ID IN ('+ @ACCOUNT_ID +')'                  
 else                    
  SET @WHERE =  @WHERE + ' AND APD.ACCOUNT_ID IN ('+ @ACCOUNT_ID +')'                        
END                        
        
/*IF NOT @UPDATED_FROM IS NULL                        
BEGIN                        
 if ltrim(rtrim(@WHERE)) = ''                    
   SET @WHERE = 'UPDATED_FROM =''' + @UPDATED_FROM + ''''                        
 else                    
  SET @WHERE = @WHERE  + ' AND UPDATED_FROM = ''' + @UPDATED_FROM + ''''                        
END*/                 
--Void Check  
DECLARE @VOID_TRANS_CODE VARCHAR(20)  
SET @VOID_TRANS_CODE ='VOID'  
  
IF NOT @UPDATED_FROM IS NULL                        
BEGIN      
  
              
IF(@UPDATED_FROM='V')  
BEGIN  
 IF LTRIM(RTRIM(@WHERE)) = ''                    
   SET @WHERE = 'ITEM_TRAN_CODE =''' + @VOID_TRANS_CODE + ''''                      
 ELSE                    
  SET @WHERE = @WHERE  + ' AND ITEM_TRAN_CODE = ''' + @VOID_TRANS_CODE + ''''                  
END  
ELSE  
BEGIN  
 IF LTRIM(RTRIM(@WHERE)) = ''                    
   SET @WHERE = 'UPDATED_FROM =''' + @UPDATED_FROM + ''''                        
 ELSE                    
  SET @WHERE = @WHERE  + ' AND UPDATED_FROM = ''' + @UPDATED_FROM + ''''           
END  
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
   SET @WHERE ='MONTH(POSTING_DATE) = ' + CONVERT(VARCHAR, @MMONTH) + ' AND YEAR (POSTING_DATE) = ' + CONVERT(VARCHAR, @MYEAR)                  
  else                    
   SET @WHERE = @WHERE + ' AND MONTH(POSTING_DATE) = ' + CONVERT(VARCHAR, @MMONTH) + ' AND YEAR (POSTING_DATE) = ' + CONVERT(VARCHAR, @MYEAR)                  
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
    
IF NOT @FROMAMT IS NULL  AND  @TOAMT IS NULL                       
BEGIN                        
 if ltrim(rtrim(@WHERE)) = ''                    
  SET @WHERE = 'APD.TRANSACTION_AMOUNT >= '+ @FROMAMT     
 else                    
  SET @WHERE = @WHERE  + ' AND APD.TRANSACTION_AMOUNT >= '+ @FROMAMT     
END     
    
IF @FROMAMT IS NULL  AND NOT @TOAMT IS NULL                       
BEGIN                        
 if ltrim(rtrim(@WHERE)) = ''                    
  SET @WHERE = 'APD.TRANSACTION_AMOUNT <= ' + @TOAMT                        
 else                    
  SET @WHERE = @WHERE  + ' AND APD.TRANSACTION_AMOUNT <= ' + @TOAMT                  
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
    
/*IF @ACCOUNT_ID IS NULL AND (NOT @FROMACT IS NULL  AND NOT @TOACT IS NULL)    
BEGIN                
 if ltrim(rtrim(@WHERE)) = ''            
  SET @WHERE =  ' APD.ACCOUNT_ID >= '+ @FROMACT + ' AND APD.ACCOUNT_ID <= ' + @TOACT          
 else   
 SET @WHERE = @WHERE  + ' AND APD.ACCOUNT_ID >= '+ @FROMACT + ' AND APD.ACCOUNT_ID <= ' + @TOACT                
END */        
    
IF @ACCOUNT_ID IS NULL AND (NOT @FROMACT IS NULL AND @TOACT IS NULL)    
BEGIN        
if ltrim(rtrim(@WHERE)) = ''            
  SET @WHERE =  'APD.ACCOUNT_ID IN(Select APD1.ACCOUNT_ID From ACT_GL_ACCOUNTS GLA1                         
                INNER JOIN ACT_ACCOUNTS_POSTING_DETAILS APD1 ON APD1.ACCOUNT_ID = GLA1.ACCOUNT_ID    
  where ISNULL(CONVERT(decimal,ACC_DISP_NUMBER),0) >= ' + isnull(@FROMACT,0) + ')'    
 else            
  SET @WHERE = @WHERE  + 'AND APD.ACCOUNT_ID IN(Select APD1.ACCOUNT_ID From ACT_GL_ACCOUNTS GLA1                         
                INNER JOIN ACT_ACCOUNTS_POSTING_DETAILS APD1 ON APD1.ACCOUNT_ID = GLA1.ACCOUNT_ID    
  where ISNULL(CONVERT(decimal,ACC_DISP_NUMBER),0) >= ' + isnull(@FROMACT,0) + ')'    
END     
    
IF @ACCOUNT_ID IS NULL AND (@FROMACT IS NULL AND NOT @TOACT IS NULL)    
BEGIN                
if ltrim(rtrim(@WHERE)) = ''            
  SET @WHERE =  'APD.ACCOUNT_ID IN(Select APD1.ACCOUNT_ID From ACT_GL_ACCOUNTS GLA1                         
                INNER JOIN ACT_ACCOUNTS_POSTING_DETAILS APD1 ON APD1.ACCOUNT_ID = GLA1.ACCOUNT_ID    
  where ISNULL(CONVERT(decimal,ACC_DISP_NUMBER),0) <=' +  isnull(@TOACT,0) + ')'    
 else            
  SET @WHERE = @WHERE  + 'AND APD.ACCOUNT_ID IN(Select APD1.ACCOUNT_ID From ACT_GL_ACCOUNTS GLA1                         
                INNER JOIN ACT_ACCOUNTS_POSTING_DETAILS APD1 ON APD1.ACCOUNT_ID = GLA1.ACCOUNT_ID    
  where ISNULL(CONVERT(decimal,ACC_DISP_NUMBER),0) <=' +  isnull(@TOACT,0) + ')'    
END     
      
IF @ACCOUNT_ID IS NULL AND (NOT @FROMACT IS NULL AND NOT @TOACT IS NULL)    
BEGIN                
if ltrim(rtrim(@WHERE)) = ''            
  SET @WHERE =  'APD.ACCOUNT_ID IN(Select APD1.ACCOUNT_ID From ACT_GL_ACCOUNTS GLA1                         
                INNER JOIN ACT_ACCOUNTS_POSTING_DETAILS APD1 ON APD1.ACCOUNT_ID = GLA1.ACCOUNT_ID    
   where ISNULL(CONVERT(decimal,ACC_DISP_NUMBER),0) >= ' + isnull(@FROMACT,0) + '  and     
  ISNULL(CONVERT(decimal,ACC_DISP_NUMBER),0) <=' +  isnull(@TOACT,0) + ')'    
 else            
  SET @WHERE = @WHERE  + 'AND APD.ACCOUNT_ID IN(Select APD1.ACCOUNT_ID From ACT_GL_ACCOUNTS GLA1                         
                INNER JOIN ACT_ACCOUNTS_POSTING_DETAILS APD1 ON APD1.ACCOUNT_ID = GLA1.ACCOUNT_ID    
  where ISNULL(CONVERT(decimal,ACC_DISP_NUMBER),0) >= ' + isnull(@FROMACT,0) + '  and     
  ISNULL(CONVERT(decimal,ACC_DISP_NUMBER),0) <=' +  isnull(@TOACT,0) + ')'    
END         
        
IF NOT @POLICY_NUMBER IS NULL                  
BEGIN                        
 if ltrim(rtrim(@WHERE)) = ''             
  SET @WHERE = 'POL.POLICY_NUMBER IN (''' + @POLICY_NUMBER + ''')'           
 else            
  SET @WHERE = @WHERE  + ' AND POL.POLICY_NUMBER IN (''' + @POLICY_NUMBER + ''')'                  
END                  
        
      
IF(@WHERE <>'')                      
BEGIN                        
  SET @QUERY = @QUERY + ' WHERE ' + @WHERE                        
END   
--     
-- IF ltrim(rtrim(@WHERE)) = ''          
--    SET @QUERY = @QUERY + ' WHERE SOURCE_NUM  <  ''A'''                       
-- ELSE                       
--    SET @QUERY = @QUERY + ' WHERE ' + @WHERE + ' AND SOURCE_NUM < ''A'''   
    
IF NOT @ORDERBY IS NULL    
BEGIN    
  SET @QUERY = @QUERY + ' ORDER BY ' + @ORDERBY               
END    
    
       
--Print @QUERY                  
       
EXECUTE(@QUERY)                        
        
END             
        
  





GO

