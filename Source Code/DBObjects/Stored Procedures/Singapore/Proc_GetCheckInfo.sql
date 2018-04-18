------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------  
--begin tran     
--drop proc dbo.Proc_GetCheckInfo    
--go    
/*----------------------------------------------------------                    
Proc Name        : dbo.Proc_GetCheckInfo                    
Created by       : Sukhveer Singh                    
Date             : 15/09/2006                    
Purpose          : Procdure to generate check report Information                    
Revison History  :                    
Used In          : Wolverine                    
 Proc_GetCheckInfo 0,null,null,null,null,null,null,null,0,null,null,0            
------------------------------------------------------------                    
Date     Review By          Comments                    
------   ------------       -------------------------*/                    
                    
ALTER PROC [dbo].[Proc_GetCheckInfo]                    
(                    
@ACCOUNT_ID varchar(8000),  -- INT               
@CHECKTYPE varchar(8000),   -- NVARCHAR            
@FROMDATE DATETIME = '',    -- Valid Format mm/dd/yyyy               
@TODATE DATETIME = '',      -- Valid Format mm/dd/yyyy               
@FROMAMT VARCHAR(100) = '',              
@TOAMT VARCHAR(100) = '',              
@CHECK_NO VARCHAR(20) = '',              
@PAYEE_ID varchar(1000)='',   
--@CUSTOMER_ID  Added For Itrack Issue #6382                
--@CUSTOMER_ID VARCHAR(100),  
--@POLICY_ID varchar(8000),      
@PolicyNumber varchar(30),  
@CLAIM_NO VARCHAR(100) = '',               
@FIRST_SORT VARCHAR(100) = '',              
@DISP_VOID_CHECKS BIT = '1' ,  
   
--Added For Itrack Issue #5497.  
@CALLED_FROM VARCHAR(20) = null                
)                    
              
AS                    
BEGIN                    
DECLARE @UNION VARCHAR(8000)                    
DECLARE @QUERY VARCHAR(8000)  
DECLARE @CLAIM_ID VARCHAR(100)   
DECLARE @CUSTOMER_ID varchar(100)  
DECLARE @POLICY_ID varchar(100)  
  
SELECT @CUSTOMER_ID = CUSTOMER_ID, @POLICY_ID = POLICY_ID FROM POL_CUSTOMER_POLICY_LIST WHERE POLICY_NUMBER = @PolicyNumber  
  
 IF @PolicyNumber <> ''  
 BEGIN  
 IF (ISNULL(@CUSTOMER_ID,'') = '' AND ISNULL(@POLICY_ID,'') = '')  
 BEGIN  
  SET @CUSTOMER_ID = -1  
  SET @POLICY_ID = -1  
 END  
 END  
  
 IF @CLAIM_NO <> ''  
 BEGIN  
 SELECT @CLAIM_ID = CLAIM_ID FROM CLM_CLAIM_INFO WHERE CLAIM_NUMBER = @CLAIM_NO  
 IF (@CLAIM_ID = '' OR @CLAIM_ID IS NULL)  
 BEGIN  
  SET @CLAIM_ID = 0  
 END  
 END   
  
 --CHECK_DATE as POSTING_DATE Added FOR Itrack Issue #6340.  
 --DATE_CLEARED Added For Itrack Issue #6475.              
 SET @QUERY = 'SELECT CHI.ACCOUNT_ID, ISNULL(CHECK_NUMBER,'''') AS CHECK_NUMBER,chi.CHECK_ID ,ISNULL(GLA.ACC_DISP_NUMBER,'''') ACC_DISP_NUMBER,                    
 ISNULL(CONVERT(VARCHAR,CHECK_DATE,101),'''' ) AS CHECK_DATE,  
 CHECK_DATE as POSTING_DATE , UPPER(ISNULL(PAYEE_ENTITY_NAME,'''')) AS PAYEE_ENTITY_NAME,  
 CONVERT(CHAR,CHI.BANK_RECONCILED_DATE,101) as DATE_CLEARED,              
 ISNULL(MLV.LOOKUP_VALUE_DESC,'''') AS CHECK_TYPE,ISNULL(CHECK_AMOUNT,0) AS CHECK_AMOUNT,ISNULL(TRAN_TYPE,0) AS TRAN_TYPE,              
 CLEARED = CASE                
 WHEN UPPER(ISNULL(CHI.IS_BNK_RECONCILED,''N'')) = ''Y'' THEN ''YES''              
 ELSE ''NO''             
 END,              
 STATUS = CASE              
 WHEN CHI.GL_UPDATE = 2 THEN ''VOID'' ELSE            
 CASE            
 WHEN UPPER(CHI.IS_BNK_RECONCILED) = ''Y''  THEN ''RECONCILED''              
 ELSE ''UNRECONCILED''  END                     
 END              
 FROM ACT_CHECK_INFORMATION CHI                     
 LEFT JOIN ACT_GL_ACCOUNTS GLA ON CHI.ACCOUNT_ID = GLA.ACCOUNT_ID              
 LEFT JOIN MNT_LOOKUP_VALUES MLV ON CHI.CHECK_TYPE = MLV.LOOKUP_UNIQUE_ID  
 left JOIN CLM_ACTIVITY CLM ON  
 CLM.CHECK_ID = CHI.CHECK_ID  
 left JOIN CLM_PARTIES PART  
 ON PART.PARTY_ID = CHI.PAYEE_ENTITY_ID   AND PART.CLAIM_ID = CLM.CLAIM_ID'  
  
            
                          
DECLARE @WHERE VARCHAR(2000)  
--@WHERECLAUSE_VOID Added For Itrack Issue #5497.  
DECLARE @WHERECLAUSE_VOID VARCHAR(4000)                       
SET @WHERE = ''   
SET @WHERECLAUSE_VOID = @WHERE  
              
 IF (@ACCOUNT_ID <> '0')                    
 BEGIN                    
   SET @WHERE = ' CHI.ACCOUNT_ID IN (' + @ACCOUNT_ID + ')'                
 END                   
                
  
IF (@CHECKTYPE <> '0')                    
BEGIN            
 SELECT @CHECKTYPE = REPLACE(@CHECKTYPE,',',''',''')                    
 if ltrim(rtrim(@WHERE)) = ''                
SET @WHERE = ' CHI.CHECK_TYPE IN (''' + @CHECKTYPE + ''')'                    
 else                
  SET @WHERE = @WHERE + ' AND CHI.CHECK_TYPE IN (''' + @CHECKTYPE + ''')'                 
END             
--Ravindra(04-04-2008): If Check Type not specified , exclude check type                   
--Code Uncommented For Itrack Issue #7019.       
ELSE            
BEGIN             
 if ltrim(rtrim(@WHERE)) = ''                
 SET @WHERE = ' CHI.CHECK_TYPE NOT IN (9937)'          
 else                
 SET @WHERE = @WHERE + ' AND CHI.CHECK_TYPE NOT IN (9937)'             
END   
  
             
IF (@FROMDATE <> '')                    
BEGIN                    
 if ltrim(rtrim(@WHERE)) = ''                  
SET @WHERE = 'cast(convert(varchar,CHECK_DATE,101) as datetime) >= cast(''' + CONVERT(VARCHAR,@FROMDATE,101) + ''' AS DATETIME) '                 
 else                    
SET @WHERE = @WHERE + ' AND cast(convert(varchar,CHECK_DATE,101) as datetime) >= cast(''' + CONVERT(VARCHAR,@FROMDATE,101) + ''' AS DATETIME) '                
END                    
                
IF (@TODATE <> '')               
BEGIN                    
 if ltrim(rtrim(@WHERE)) = ''                   
SET @WHERE = 'cast(convert(varchar,CHECK_DATE,101) as datetime) <= cast(''' + CONVERT(VARCHAR,@TODATE,101) +  ''' AS DATETIME)  '          
 else                  
SET @WHERE = @WHERE + ' AND cast(convert(varchar,CHECK_DATE,101) as datetime) <=cast(''' + CONVERT(VARCHAR,@TODATE,101)  + '''AS DATETIME)'             
END               
              
IF (@FROMAMT <> '')              
BEGIN                    
 if ltrim(rtrim(@WHERE)) = ''                
  SET @WHERE = 'CHECK_AMOUNT >= '+ @FROMAMT                    
 else                
  SET @WHERE = @WHERE  + ' AND CHECK_AMOUNT >= '+ @FROMAMT              
END              
              
IF (@TOAMT <> '')              
BEGIN                    
 if ltrim(rtrim(@WHERE)) = ''                
  SET @WHERE = 'CHECK_AMOUNT <= '+ @TOAMT                    
 else                
  SET @WHERE = @WHERE  + ' AND CHECK_AMOUNT <= '+ @TOAMT              
END              
              
IF (@CHECK_NO <> '')              
BEGIN                    
 if ltrim(rtrim(@WHERE)) = ''                
  SET @WHERE = 'CHECK_NUMBER = '''+ @CHECK_NO + ''''                  
 else                
  SET @WHERE = @WHERE  + ' AND CHECK_NUMBER = ''' + @CHECK_NO  + ''''            
END              
              
IF (@PAYEE_ID <> '')              
BEGIN                    
 if ltrim(rtrim(@WHERE)) = ''                
  --Commented and Like operator Added For Itrack Issue #6382    
  -- SET @WHERE = 'PAYEE_ENTITY_NAME = ''' + @PAYEE_ID + ''''    
  SET @WHERE = 'PAYEE_ENTITY_NAME LIKE  ''%' + @PAYEE_ID + '%'''  
 else  
  --Commented and Like operator Added For Itrack Issue #6382                
  --SET @WHERE = @WHERE  + ' AND PAYEE_ENTITY_NAME = ''' +@PAYEE_ID + ''''               
  SET @WHERE = @WHERE  + ' AND PAYEE_ENTITY_NAME LIKE ''%' +@PAYEE_ID + '%'''               
END  
  
  
--Customer_id Added For Itrack Issue #6382.  
IF (@CUSTOMER_ID <> '0')              
BEGIN                    
 if ltrim(rtrim(@WHERE)) = ''                
  SET @WHERE = 'CHI.CUSTOMER_ID IN (' + @CUSTOMER_ID + ')'              
 else                
  SET @WHERE = @WHERE  + ' AND CHI.CUSTOMER_ID IN (' + @CUSTOMER_ID + ')'              
END   
  
IF (@POLICY_ID <> '0')              
BEGIN                    
 if ltrim(rtrim(@WHERE)) = ''                
  SET @WHERE = 'CHI.POLICY_ID IN (' + @POLICY_ID + ')'              
 else                
  SET @WHERE = @WHERE  + ' AND CHI.POLICY_ID IN (' + @POLICY_ID + ')'              
END   
  
--Claim_no Added For Itrack Issue #6382.  
IF (@CLAIM_ID <> '')              
BEGIN        
 if ltrim(rtrim(@WHERE)) = ''                
  SET @WHERE = 'CLM.CLAIM_ID = ''' + @CLAIM_ID + ''''                
 else                
  SET @WHERE = @WHERE  + ' AND CLM.CLAIM_ID = ''' + @CLAIM_ID + ''''               
END         
            
IF (ISNULL(@DISP_VOID_CHECKS,0) <> '1')  -- 0 : DON'T SHOW VOIDED CHECKS            
BEGIN       
 IF ltrim(rtrim(@WHERE)) = ''                
  SET @WHERE = ' ISNULL(CHI.GL_UPDATE,0) <> 2'            
 ELSE            
     SET @WHERE = @WHERE + ' AND ISNULL(CHI.GL_UPDATE,0) <> 2'            
END            
            
IF(@WHERE <>'')               
 SET @WHERE = @WHERE  +' AND ISNULL(CHI.IS_COMMITED,''N'') = ''Y'''            
ELSE            
 SET @WHERE = 'ISNULL(CHI.IS_COMMITED,''N'') = ''Y'''     
            
IF(@WHERE <>'')                  
 BEGIN                    
   SET @QUERY = @QUERY + ' WHERE ' + @WHERE   
   --PRINT (@QUERY + ' WHERE ' + @WHERE )               
 END              
--Added For Itrack Issue #5497.  
IF(@CALLED_FROM = 'CLM')  
BEGIN  
   
IF (@ACCOUNT_ID <> '0')                    
BEGIN                    
  SET @WHERECLAUSE_VOID = ' CHI.ACCOUNT_ID IN (' + @ACCOUNT_ID + ')'                
END   
  
IF (@FROMDATE <> '')                    
 BEGIN        
  if ltrim(rtrim(@WHERECLAUSE_VOID)) = ''                  
         SET @WHERECLAUSE_VOID = 'cast(convert(varchar,POSTING_DATE,101) as datetime) >= cast(''' + CONVERT(VARCHAR,@FROMDATE,101) + ''' AS DATETIME) '                 
           
else                    
  SET @WHERECLAUSE_VOID = @WHERECLAUSE_VOID + ' AND cast(convert(varchar,POSTING_DATE,101) as datetime) >= cast(''' + CONVERT(VARCHAR,@FROMDATE,101) + ''' AS DATETIME) '                
    END       
  
IF (@TODATE <> '')               
 BEGIN                    
  if ltrim(rtrim(@WHERECLAUSE_VOID)) = ''                   
  SET @WHERECLAUSE_VOID = 'cast(convert(varchar,POSTING_DATE,101) as datetime) <= cast(''' + CONVERT(VARCHAR,@TODATE,101) +  ''' AS DATETIME)  '          
 else                  
  SET @WHERECLAUSE_VOID = @WHERECLAUSE_VOID + ' AND cast(convert(varchar,POSTING_DATE,101) as datetime) <=cast(''' + CONVERT(VARCHAR,@TODATE,101)  + '''AS DATETIME)'             
     END   
  
 IF (@FROMAMT <> '')              
 BEGIN                    
  if ltrim(rtrim(@WHERECLAUSE_VOID)) = ''                
   SET @WHERECLAUSE_VOID = 'CHECK_AMOUNT >= '+ @FROMAMT                    
  else                
   SET @WHERECLAUSE_VOID = @WHERECLAUSE_VOID  + ' AND CHECK_AMOUNT >= '+ @FROMAMT              
 END              
               
 IF (@TOAMT <> '')              
 BEGIN                    
  if ltrim(rtrim(@WHERECLAUSE_VOID)) = ''                
   SET @WHERECLAUSE_VOID = 'CHECK_AMOUNT <= '+ @TOAMT                    
  else                
   SET @WHERECLAUSE_VOID = @WHERECLAUSE_VOID  + ' AND CHECK_AMOUNT <= '+ @TOAMT              
 END   
  
IF (@CHECK_NO <> '')              
BEGIN                    
 if ltrim(rtrim(@WHERECLAUSE_VOID)) = ''                
  SET @WHERECLAUSE_VOID = 'CHECK_NUMBER = '''+ @CHECK_NO + ''''                  
 else                
  SET @WHERECLAUSE_VOID = @WHERECLAUSE_VOID  + ' AND CHECK_NUMBER = ''' + @CHECK_NO  + ''''            
END   
  
IF (@PAYEE_ID <> '')              
BEGIN                    
 if ltrim(rtrim(@WHERECLAUSE_VOID)) = ''                
  --Commented and Like operator Added For Itrack Issue #6382    
  -- SET @WHERE = 'PAYEE_ENTITY_NAME = ''' + @PAYEE_ID + ''''    
  SET @WHERECLAUSE_VOID = 'PAYEE_ENTITY_NAME LIKE  ''%' + @PAYEE_ID + '%'''  
 else  
  --Commented and Like operator Added For Itrack Issue #6382                
  --SET @WHERE = @WHERE  + ' AND PAYEE_ENTITY_NAME = ''' +@PAYEE_ID + ''''               
  SET @WHERECLAUSE_VOID = @WHERECLAUSE_VOID  + ' AND PAYEE_ENTITY_NAME LIKE ''%' +@PAYEE_ID + '%'''               
END  
  
  
--Customer_id Added For Itrack Issue #6382.  
IF (@CUSTOMER_ID <> '0')              
BEGIN                    
 if ltrim(rtrim(@WHERECLAUSE_VOID)) = ''                
  SET @WHERECLAUSE_VOID = 'CHI.CUSTOMER_ID IN (' + @CUSTOMER_ID + ')'              
 else                
  SET @WHERECLAUSE_VOID = @WHERECLAUSE_VOID  + ' AND CHI.CUSTOMER_ID IN (' + @CUSTOMER_ID + ')'              
END   
  
IF (@POLICY_ID <> '0')              
BEGIN                    
 if ltrim(rtrim(@WHERECLAUSE_VOID)) = ''                
  SET @WHERECLAUSE_VOID = 'CHI.POLICY_ID IN (' + @POLICY_ID + ')'              
 else                
  SET @WHERECLAUSE_VOID = @WHERECLAUSE_VOID  + ' AND CHI.POLICY_ID IN (' + @POLICY_ID + ')'              
END   
  
--Claim_no Added For Itrack Issue #6382.  
IF (@CLAIM_NO <> '')              
BEGIN                    
 if ltrim(rtrim(@WHERECLAUSE_VOID)) = ''                
  SET @WHERECLAUSE_VOID = 'CLM.CLAIM_ID = ''' + @CLAIM_ID + ''''                
 else                
  SET @WHERECLAUSE_VOID = @WHERECLAUSE_VOID  + ' AND CLM.CLAIM_ID = ''' + @CLAIM_ID + ''''               
END    
  
 --AAPD.POSTING_DATE Added For Itrack Issue #6340.  
    --DATE_CLEARED Added For Itrack Issue #6475.  
 SET @UNION =   
 '  
 UNION  
  
 SELECT CHI.ACCOUNT_ID , ISNULL(CHECK_NUMBER,'''') AS CHECK_NUMBER,chi.CHECK_ID,ISNULL(GLA.ACC_DISP_NUMBER,'''')   
 ACC_DISP_NUMBER,                
 CONVERT(CHAR,AAPD.POSTING_DATE,101) AS CHECK_DATE  
 ,AAPD.POSTING_DATE ,UPPER(ISNULL(PAYEE_ENTITY_NAME,'''')) + '' - (Void)'' AS PAYEE_ENTITY_NAME,   
    CONVERT(CHAR,CHI.BANK_RECONCILED_DATE,101) as DATE_CLEARED,         
 ISNULL(MLV.LOOKUP_VALUE_DESC,'''') AS CHECK_TYPE,ISNULL(CHECK_AMOUNT,0) * -1    
 AS CHECK_AMOUNT,ISNULL(TRAN_TYPE,0) AS TRAN_TYPE,          
 CLEARED = CASE            
 WHEN UPPER(ISNULL(CHI.IS_BNK_RECONCILED,''N'')) = ''Y'' THEN ''YES''          
 ELSE ''NO''         
 END,          
 STATUS = CASE          
 WHEN CHI.GL_UPDATE = 2 THEN     ''VOID''  
 ELSE  
 CASE WHEN UPPER(CHI.IS_BNK_RECONCILED) = ''Y''  THEN ''RECONCILED''          
 ELSE ''UNRECONCILED''  END        
 END          
 FROM ACT_CHECK_INFORMATION CHI       
 INNER JOIN ACT_GL_ACCOUNTS GLA ON CHI.ACCOUNT_ID = GLA.ACCOUNT_ID                   
 LEFT JOIN MNT_LOOKUP_VALUES MLV ON CHI.CHECK_TYPE = MLV.LOOKUP_UNIQUE_ID   
    INNER JOIN ACT_ACCOUNTS_POSTING_DETAILS AAPD on AAPD.CUSTOMER_ID = CHI.CUSTOMER_ID      
    AND AAPD.POLICY_ID = CHI.POLICY_ID AND    
    AAPD.POLICY_VERSION_ID = CHI.POLICY_VER_TRACKING_ID   
    AND CHI.CHECK_ID = AAPD.SOURCE_ROW_ID AND AAPD.UPDATED_FROM = ''C'' AND ISNULL(GL_UPDATE,0) = ''2''   
    left JOIN CLM_ACTIVITY CLM ON  
 CLM.CHECK_ID = CHI.CHECK_ID  
 inner JOIN CLM_PARTIES PART  
 ON PART.PARTY_ID = CHI.PAYEE_ENTITY_ID  
 AND PART.CLAIM_ID = CLM.CLAIM_ID  
 AND AAPD.ITEM_TRAN_CODE_TYPE = ''CHK'' AND ITEM_TRAN_CODE = ''VOID'''  
   
--AND and Where clause Added For Itrack Issue #6382.  
 IF(@WHERECLAUSE_VOID <>'')              
 BEGIN          
  SET @UNION = @UNION + ' WHERE ' + @WHERECLAUSE_VOID  + ' AND ISNULL(CHI.IS_COMMITED,''N'') = ''Y'''       
 END    
ELSE  
    BEGIN  
        SET @WHERECLAUSE_VOID = 'ISNULL(CHI.IS_COMMITED,''N'') = ''Y'''  
    END        
  
IF(ISNULL(@FIRST_SORT,'') <>'')  
 BEGIN    
  IF (@FIRST_SORT='CHECK_DATE')  
  BEGIN  
    SELECT @UNION = @UNION + ' ORDER BY POSTING_DATE , CHI.CHECK_ID'  
  END  
    ELSE  
  BEGIN   
      SET @UNION = @UNION + ' ORDER BY ' + @FIRST_SORT  + ' , CHI.CHECK_ID'  
  END   
 END      
   
ELSE  
 BEGIN   
  SET @UNION = @UNION + ' ORDER BY  CHI.CHECK_ID'  
 END  
     SET @QUERY = @QUERY + @UNION   
       
END         
            
              
ELSE IF(@FIRST_SORT<>'')              
BEGIN      
  
 --Added For Itrack Issue Itrack Issue #5498.    
  IF (@FIRST_SORT='CHECK_DATE')  
  SELECT @QUERY=@QUERY + ' ORDER BY  CAST(' + @FIRST_SORT + ' AS DATETIME)'  
ELSE       
  SELECT @QUERY=@QUERY + ' ORDER BY ' + @FIRST_SORT  
END                  
    
--print @QUERY                
EXECUTE(@QUERY)                    
END    
                  
--go  
----exec Proc_GetCheckInfo '0','9937','','','','','','','A1000005','','','1','CLM'   
--exec Proc_GetCheckInfo '0','0','','','','','','s','','','','1','0'   
--rollback tran    
  
  
  
  
  
  
  
  
  