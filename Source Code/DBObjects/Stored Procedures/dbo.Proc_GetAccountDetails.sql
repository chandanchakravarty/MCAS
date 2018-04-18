IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAccountDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAccountDetails]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------      
Proc Name        : dbo.Proc_GetAccountDetails      
Created by       : Gaurav Tyagi      
Date             : 07/04/2005      
Purpose        : Get Account Information       
Revison History :      
Used In          : Wolverine      
------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/      
-- drop proc dbo.Proc_GetAccountDetails
create PROC dbo.Proc_GetAccountDetails      
(      
@FROMSOURCE int,      
@TOSOURCE int,      
@FROMDATE DATETIME,      
@TODATE DATETIME,      
@ACCOUNT_ID DECIMAL(18,2),      
@UPDATED_FROM NCHAR(1),      
@STATE INT,      
@LOB INT  
      
)      
AS      
BEGIN      
      
DECLARE @QUERY VARCHAR(8000)      
      
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
  


IF NOT @FROMSOURCE IS NULL      
BEGIN      
  SET @WHERE = ' SOURCE_NUM >=''' + CONVERT(VARCHAR,@FROMSOURCE) + ''''     
END     


  
IF NOT @TOSOURCE IS NULL      
BEGIN      
 if ltrim(rtrim(@WHERE)) = ''  
   SET @WHERE = ' SOURCE_NUM <=' + CONVERT(VARCHAR,@TOSOURCE)      
 else  
  SET @WHERE = @WHERE + ' AND SOURCE_NUM <=''' + CONVERT(VARCHAR,@TOSOURCE)  + ''''
END      
  
IF NOT @FROMDATE IS NULL      
BEGIN      
 if ltrim(rtrim(@WHERE)) = ''  
  SET @WHERE = 'SOURCE_TRAN_DATE >=''' + CONVERT(VARCHAR,@FROMDATE,101) + ''''      
 else  
  SET @WHERE = @WHERE + ' AND SOURCE_TRAN_DATE >=''' + CONVERT(VARCHAR,@FROMDATE) + ''''      
END      
  
IF NOT @TODATE IS NULL      
BEGIN      
 if ltrim(rtrim(@WHERE)) = ''  
   SET @WHERE = 'SOURCE_TRAN_DATE <=''' + CONVERT(VARCHAR,@TODATE,101) + ''''      
 else  
  SET @WHERE = @WHERE + ' AND SOURCE_TRAN_DATE <=''' + CONVERT(VARCHAR,@TODATE,101) + ''''      
    
END      
      
IF NOT @ACCOUNT_ID IS NULL      
BEGIN      
 if ltrim(rtrim(@WHERE)) = ''  
  SET @WHERE =  ' GLA.ACC_NUMBER =' + CONVERT(VARCHAR,@ACCOUNT_ID)      
 else  
  SET @WHERE =  @WHERE + ' AND GLA.ACC_NUMBER =' + CONVERT(VARCHAR,@ACCOUNT_ID)      
END      
      
IF NOT @UPDATED_FROM IS NULL   
BEGIN      
 if ltrim(rtrim(@WHERE)) = ''  
   SET @WHERE = 'UPDATED_FROM =''' + @UPDATED_FROM + ''''      
 else  
  SET @WHERE = @WHERE  + ' AND UPDATED_FROM =''' + @UPDATED_FROM + ''''      
END     
  
  
      
     
IF NOT @LOB IS NULL      
BEGIN      
 if ltrim(rtrim(@WHERE)) = ''  
  SET @WHERE = 'APD.LOB_ID ='+ CONVERT(VARCHAR,@LOB)      
 else  
  SET @WHERE = @WHERE  + ' AND APD.LOB_ID ='+ CONVERT(VARCHAR,@LOB)      
    
END      
       
IF(@WHERE <>'')    
BEGIN      
  SET @QUERY = @QUERY + ' WHERE ' + @WHERE      
END    
    
 set @QUERY = @QUERY  + ' ORDER BY IDENTITY_ROW_ID'  
  
--print @QUERY  
  
EXECUTE(@QUERY)      
END      
    
  
  


GO

