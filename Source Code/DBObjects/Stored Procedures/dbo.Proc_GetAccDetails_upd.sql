IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAccDetails_upd]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAccDetails_upd]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name        : dbo.Proc_GetAccDetails_upd       
Created by       : Sukhveer Singh        
Date             : 29/09/2006        
Purpose          : Get Account Detail Information         
Revison History  :        
Used In          : Wolverine        
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
--DROP PROC dbo.Proc_GetAccDetails_upd 
        
CREATE PROC dbo.Proc_GetAccDetails_upd        
(        
@FROMSOURCE INT = 0,        
@TOSOURCE INT = 0,        
@FROMDATE DATETIME = '',        
@TODATE DATETIME = '',        
@ACCOUNT_ID VARCHAR(8000),  --INT        
@UPDATED_FROM VARCHAR(10)= '',        
@STATE VARCHAR(8000) = '-99', --INT
@LOB VARCHAR(8000) = '', --INT  
-- Input value for YEARMONTH is as '06,2006' (month 06 and year 2006)  
@YEARMONTH VARCHAR(200) = '',  
@VENDORID VARCHAR(8000), --INT  
@CUSTOMERID VARCHAR(8000), -- INT  
@FROMAMT VARCHAR(10) = '',  
@TOAMT VARCHAR(10) = '',  
@TRANSTYPE VARCHAR(8000), --NVARCHAR  
@POLICYID VARCHAR(8000),  --INT  

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
        
SET @QUERY = 'SELECT ISNULL(CONVERT(VARCHAR,ACC_DISP_NUMBER)+'' - ''+ ACC_DESCRIPTION,'''') AS ACCOUNT,ISNULL(SOURCE_NUM,'''') SOURCE_NUM,        
ISNULL(CONVERT(VARCHAR,SOURCE_TRAN_DATE,101),'''') SOURCE_TRAN_DATE,ISNULL(TRANSACTION_AMOUNT,0) TRANSACTION_AMOUNT,        
ISNULL(AGENCY_COMM_AMT,0) AGENCY_COMM_AMT,ISNULL(AL.AGENCY_DISPLAY_NAME,'''') AGENCY_DISPLAY_NAME,        
CLT.CUSTOMER_FIRST_NAME+'' ''+CLT.CUSTOMER_MIDDLE_NAME+'' ''+CLT.CUSTOMER_LAST_NAME AS CUSTOMER_NAME,      
ISNULL(POLICY_NUMBER,'''') + '' '' + ISNULL(POLICY_DISP_VERSION,'''') AS POLICY_NUMBER,    
ISNULL(BILL_CODE,'''') BILL_CODE,        
ISNULL(GROSS_AMOUNT,0) GROSS_AMOUNT,ISNULL(ITEM_TRAN_CODE,'''') ITEM_TRAN_CODE,ISNULL(LOB.LOB_DESC,'''') LOB_DESC,        
ISNULL(STATE_NAME,'''') STATE_NAME,ISNULL(VENDOR_FNAME+'' ''+VENDOR_LNAME,'''') AS VENDOR_NAME,ISNULL(TAX_NAME,'''') TAX_NAME,        
CASE UPDATED_FROM WHEN ''J'' THEN ''JOURNAL''        
WHEN ''D'' THEN ''DEPOSIT''        
WHEN ''C'' THEN ''CHECK''    
WHEN ''P'' THEN ''PREMIUM''     
WHEN ''I'' THEN ''INVOICE''     
END        
AS UPDATED_FROM         
FROM ACT_ACCOUNTS_POSTING_DETAILS APD         
LEFT JOIN ACT_GL_ACCOUNTS GLA ON APD.ACCOUNT_ID = GLA.ACCOUNT_ID         
LEFT JOIN MNT_LOB_MASTER LOB ON APD.LOB_ID = LOB.LOB_ID         
LEFT JOIN CLT_CUSTOMER_LIST CLT ON APD.CUSTOMER_ID = CLT.CUSTOMER_ID        
LEFT JOIN MNT_AGENCY_LIST AL ON APD.AGENCY_ID = AL.AGENCY_ID        
LEFT JOIN MNT_COUNTRY_STATE_LIST CSL ON APD.STATE_ID = CSL.STATE_ID        
LEFT JOIN MNT_VENDOR_LIST VL ON APD.VENDOR_ID = VL.VENDOR_ID        
LEFT JOIN MNT_TAX_ENTITY_LIST  TEL ON APD.TAX_ID = TEL.TAX_ID     
LEFT JOIN POL_CUSTOMER_POLICY_LIST POL ON APD.CUSTOMER_ID = POL.CUSTOMER_ID   AND APD.POLICY_ID = POL.POLICY_ID     
 AND APD.POLICY_VERSION_ID = POL.POLICY_VERSION_ID'      
  
  
  
DECLARE @WHERE VARCHAR(2000)        
SET @WHERE = ''          
    
    
IF (@FROMSOURCE <> 0)        
BEGIN        
  SET @WHERE = ' SOURCE_NUM >=' + CONVERT(VARCHAR,@FROMSOURCE)        
END       
    
    
IF (@TOSOURCE <> 0)        
BEGIN        
 if ltrim(rtrim(@WHERE)) = ''    
   SET @WHERE = ' SOURCE_NUM <=' + CONVERT(VARCHAR,@TOSOURCE)        
 else    
  SET @WHERE = @WHERE + ' AND SOURCE_NUM <= ' + CONVERT(VARCHAR,@TOSOURCE)     
END        
    
IF (@FROMDATE <> '')        
BEGIN        
 if ltrim(rtrim(@WHERE)) = ''    
  SET @WHERE = 'SOURCE_TRAN_DATE >=''' + CONVERT(VARCHAR,@FROMDATE,101) + ''''        
 else    
  SET @WHERE = @WHERE + ' AND SOURCE_TRAN_DATE >= ''' + CONVERT(VARCHAR,@FROMDATE) + ''''        
END        
    
IF (@TODATE <> '')
BEGIN        
 if ltrim(rtrim(@WHERE)) = ''    
   SET @WHERE = 'SOURCE_TRAN_DATE <=''' + CONVERT(VARCHAR,@TODATE,101) + ''''        
 else    
  SET @WHERE = @WHERE + ' AND SOURCE_TRAN_DATE <= ''' + CONVERT(VARCHAR,@TODATE,101) + ''''        
      
END        
  
IF (@ACCOUNT_ID <> '0')        
BEGIN        
 if ltrim(rtrim(@WHERE)) = ''    
  SET @WHERE =  ' APD.ACCOUNT_ID IN (' + @ACCOUNT_ID  + ')'  
 else    
  SET @WHERE =  @WHERE + ' AND APD.ACCOUNT_ID IN (' + @ACCOUNT_ID + ')'        
END        
        
IF (@UPDATED_FROM <> '')        
BEGIN        
 if ltrim(rtrim(@WHERE)) = ''    
   SET @WHERE = 'UPDATED_FROM =''' + @UPDATED_FROM + ''''        
 else    
  SET @WHERE = @WHERE  + ' AND UPDATED_FROM = ''' + @UPDATED_FROM + ''''        
END       
    
IF (@STATE <> '-99')        
BEGIN        
 if ltrim(rtrim(@WHERE)) = ''    
   SET @WHERE = 'APD.STATE_ID IN (' + @STATE +')'        
 else    
  SET @WHERE = @WHERE  + ' AND APD.STATE_ID IN (' + @STATE +')'        
END   
      
IF (ISNULL(@LOB,'0') <> '0' AND @LOB<>'' )        
BEGIN        
 if ltrim(rtrim(@WHERE)) = ''    
  SET @WHERE = 'APD.LOB_ID IN ('+ @LOB +')'  
 else    
  SET @WHERE = @WHERE  + ' AND APD.LOB_ID IN ('+ @LOB +')'        
      
END   
  
IF (@FROMDATE = '' AND @TODATE = '')  
BEGIN  
 IF (@YEARMONTH <> '')        
 BEGIN        
  if ltrim(rtrim(@WHERE)) = ''    
   SET @WHERE ='MONTH(SOURCE_TRAN_DATE) = ' + CONVERT(VARCHAR, @MMONTH) + ' AND YEAR (SOURCE_TRAN_DATE) = ' + CONVERT(VARCHAR, @MYEAR)  
  else    
   SET @WHERE = @WHERE + ' AND MONTH(SOURCE_TRAN_DATE) = ' + CONVERT(VARCHAR, @MMONTH) + ' AND YEAR (SOURCE_TRAN_DATE) = ' + CONVERT(VARCHAR, @MYEAR)  
 END  
END  
  
IF (@VENDORID <> '0')        
BEGIN        
 if ltrim(rtrim(@WHERE)) = ''    
  SET @WHERE = 'APD.VENDOR_ID IN (' + @VENDORID + ')'       
 else    
  SET @WHERE = @WHERE  + ' AND APD.VENDOR_ID IN (' + @VENDORID + ')'  
END  
  
IF (@CUSTOMERID <> '0')        
BEGIN        
 if ltrim(rtrim(@WHERE)) = ''    
  SET @WHERE = 'APD.CUSTOMER_ID IN (' + @CUSTOMERID + ')'  
 else    
  SET @WHERE = @WHERE  + ' AND APD.CUSTOMER_ID IN (' + @CUSTOMERID + ')'  
END    
  
IF (@FROMAMT <> ''  AND @TOAMT <> '')       
BEGIN        
 if ltrim(rtrim(@WHERE)) = ''    
  SET @WHERE = 'APD.GROSS_AMOUNT >= '+ @FROMAMT + ' AND APD.GROSS_AMOUNT <= ' + @TOAMT        
 else    
  SET @WHERE = @WHERE  + ' AND APD.GROSS_AMOUNT >= '+ @FROMAMT + ' AND APD.GROSS_AMOUNT <= ' + @TOAMT  
END  
  
IF (@TRANSTYPE <> '0')  
BEGIN        
 if ltrim(rtrim(@WHERE)) = ''    
  SET @WHERE = 'APD.ITEM_TRAN_CODE_TYPE IN (''' + @TRANSTYPE + ''')'        
 else    
  SET @WHERE = @WHERE  + ' AND APD.ITEM_TRAN_CODE_TYPE IN (''' + @TRANSTYPE + ''')'  
END  
  
IF (@POLICYID <> '0')  
BEGIN        
 if ltrim(rtrim(@WHERE)) = ''    
  SET @WHERE = 'APD.POLICY_ID IN (' + @POLICYID + ')'  
 else    
  SET @WHERE = @WHERE  + ' AND APD.POLICY_ID IN (' + @POLICYID + ')'  
END   
  
         
IF(@WHERE <>'')      
BEGIN        
  SET @QUERY = @QUERY + ' WHERE ' + @WHERE        
END      
      
print @QUERY    
    
--EXECUTE(@QUERY)        
END        
      
    
    
--dbo.Proc_GetAccDetails_upd    


  

  
--SELECT * FROM ACT_ACCOUNTS_POSTING_DETAILS WHERE POLICY_ID IN ('30')




GO

