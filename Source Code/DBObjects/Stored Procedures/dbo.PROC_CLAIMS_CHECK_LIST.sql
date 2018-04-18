IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_CLAIMS_CHECK_LIST]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_CLAIMS_CHECK_LIST]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran     
--DROP PROC  PROC_CLAIMS_CHECK_LIST    
--go    
    
CREATE PROC [dbo].[PROC_CLAIMS_CHECK_LIST]        
  @REPORTTYPE VARCHAR(20),        
  @StartDate datetime=null,        
  @EndDate datetime=null,        
  @Check_From_Amount  VARCHAR(10) = NULL,                      
  @Check_To_Amount VARCHAR(10) = NULL ,      
  @ORDERBY varchar(100) = NULL                   
         
AS        
        
BEGIN        
         
 DECLARE @WHERECLAUSE VARCHAR(4000)    
 DECLARE @WHERECLAUSE_VOID VARCHAR(4000)     
 DECLARE @SELECTCLAUSE VARCHAR(8000)        
 declare @finalQuery varchar(8000)       
  DECLARE @QUERY VARCHAR(8000)       
--SELECT @REPORTTYPE = 'OUTSTANDING'        
        
if @REPORTTYPE =''        
begin      
set @REPORTTYPE= null      
end      
      
if @StartDate =''        
begin      
set @StartDate= null      
end      
      
if  @EndDate =''        
begin      
set  @EndDate = null      
end      
      
if  @Check_From_Amount  =''        
begin      
set  @Check_From_Amount  = null      
end      
      
if  @Check_To_Amount =''        
begin      
set  @Check_To_Amount = null      
end      
      
if  @ORDERBY =''        
begin      
set  @ORDERBY = null      
end      
      
      
      
IF @REPORTTYPE = 'ISSUED'        
 SELECT @WHERECLAUSE  =  ' WHERE  CHECK_TYPE=9937 and GL_UPDATE <> 2  '     --CLAIM CHECK   
        
IF @REPORTTYPE = 'CLEARED'        
 SELECT @WHERECLAUSE  =  ' WHERE IS_BNK_RECONCILED = ''Y'' AND CHECK_TYPE=9937  '        
        
IF @REPORTTYPE = 'OUTSTANDING'        
-- Commented by Asfa (25/Oct/2007) - Against iTrack issue #1892        
-- SELECT @WHERECLAUSE  =  ' WHERE IS_BNK_RECONCILED_VOID = ''Y'' AND CHECK_TYPE=9937  '        
 --Gl_Update Added For Itrack #Issue 5497.   
 SELECT @WHERECLAUSE  =  ' WHERE (IS_BNK_RECONCILED = ''N'' OR IS_BNK_RECONCILED IS NULL) AND CHECK_TYPE=9937 and GL_UPDATE <> 2   ' 

      
IF @REPORTTYPE = 'VOID'        
 SELECT @WHERECLAUSE  =  ' WHERE  CHECK_TYPE=9937 '        
        
IF @Check_From_Amount  <> ''                        
 SELECT @WHERECLAUSE  =  @WHERECLAUSE  + ' AND CHECK_AMOUNT  >= '+ @Check_From_Amount         
        
IF @Check_To_Amount <> ''                           
 SELECT @WHERECLAUSE  =  @WHERECLAUSE  + ' AND CHECK_AMOUNT  <= '+ @Check_To_Amount        
        
/*        
IF NOT @Check_From_Amount IS NULL                        
 SELECT @WHERECLAUSE  =  @WHERECLAUSE  + ' AND CHECK_AMOUNT  >= '+ @Check_From_Amount         
        
IF NOT @Check_To_Amount IS NULL                           
 SELECT @WHERECLAUSE  =  @WHERECLAUSE  + ' AND CHECK_AMOUNT  <= '+ @Check_To_Amount        
*/        
        
        
/*IF @StartDate is not  null and @EndDate is not  null        
 SELECT @WHERECLAUSE  =  @WHERECLAUSE   + '  AND   CHECK_DATE between  ''' + @StartDate + '''  and  ''' + @EndDate + ''''        
        
IF @StartDate is not  null and @EndDate is null        
 SELECT @WHERECLAUSE  =  @WHERECLAUSE   + '  AND   CHECK_DATE between  ' + @StartDate + '  and  ' + @StartDate        
        
IF @StartDate is null and @EndDate is not  null        
 SELECT @WHERECLAUSE  =  @WHERECLAUSE   + '  AND   CHECK_DATE between  ' + @EndDate  + '  and  ' + @EndDate         
*/        
--CHANGE FOR ITRACK ISSUE #5333 DATE 22-01-2009      
IF @REPORTTYPE = 'CLEARED'     
begiN    
    
        
	 IF NOT @StartDate IS NULL                     
	 BEGIN                            
	  if ltrim(rtrim(@WHERECLAUSE)) = ''                        
	   SET @WHERECLAUSE = 'CAST(CONVERT(VARCHAR,BANK_RECONCILED_DATE,101) as DATETIME) >='''         
	   + CONVERT(VARCHAR, CAST(CONVERT(VARCHAR,@StartDate,101) AS DATETIME) ) + ''''                            
	  else                        
	   SET @WHERECLAUSE = @WHERECLAUSE         
	   + ' AND CAST(CONVERT(VARCHAR,BANK_RECONCILED_DATE,101) as DATETIME) >= '''         
	   + CONVERT(VARCHAR, CAST(CONVERT(VARCHAR,@StartDate,101) AS DATETIME) )+ ''''                            
	 END                            
	     
	 IF NOT @EndDate IS NULL                  
	 BEGIN          
	  if ltrim(rtrim(@WHERECLAUSE)) = ''                        
	   SET @WHERECLAUSE = 'CAST(CONVERT(VARCHAR,BANK_RECONCILED_DATE,101) as DATETIME) <='''         
	   + CONVERT(VARCHAR, CAST(CONVERT(VARCHAR,@EndDate,101) AS DATETIME) )+ ''''                            
	  else                        
	   SET @WHERECLAUSE = @WHERECLAUSE + ' AND CAST(CONVERT(VARCHAR,BANK_RECONCILED_DATE,101) as DATETIME) <= '''         
	   + CONVERT(VARCHAR, CAST(CONVERT(VARCHAR,@EndDate,101) AS DATETIME) )+ ''''                            
	 END           
    
END

ELSE IF  @REPORTTYPE = 'VOID' 

		BEGIN
		IF NOT @StartDate IS NULL                     
		BEGIN                            
			if ltrim(rtrim(@WHERECLAUSE)) = ''                        
				SET @WHERECLAUSE = 'CAST(CONVERT(VARCHAR,POSTING_DATE,101) as DATETIME) >='''         
				+ CONVERT(VARCHAR, CAST(CONVERT(VARCHAR,@StartDate,101) AS DATETIME) ) + ''''                            
			else                        
				SET @WHERECLAUSE = @WHERECLAUSE         
				+ ' AND CAST(CONVERT(VARCHAR,POSTING_DATE,101) as DATETIME) >= '''         
				+ CONVERT(VARCHAR, CAST(CONVERT(VARCHAR,@StartDate,101) AS DATETIME) )+ ''''                            
		END                            
		     
		IF NOT @EndDate IS NULL                    
		BEGIN                            
			if ltrim(rtrim(@WHERECLAUSE)) = ''                        
				SET @WHERECLAUSE = 'CAST(CONVERT(VARCHAR,POSTING_DATE,101) as DATETIME) <='''         
				+ CONVERT(VARCHAR, CAST(CONVERT(VARCHAR,@EndDate,101) AS DATETIME) )+ ''''                            
			else                        
				SET @WHERECLAUSE = @WHERECLAUSE + ' AND CAST(CONVERT(VARCHAR,POSTING_DATE,101) as DATETIME) <= '''         
				+ CONVERT(VARCHAR, CAST(CONVERT(VARCHAR,@EndDate,101) AS DATETIME) )+ ''''                            
		END     
		 END
  
ELSE    

BEGIN    
    
	--SET @WHERECLAUSE_VOID = @WHERECLAUSE
	IF NOT @StartDate IS NULL                     
	BEGIN                            
		if ltrim(rtrim(@WHERECLAUSE)) = ''                        
		SET @WHERECLAUSE = 'CAST(CONVERT(VARCHAR,CHECK_DATE,101) as DATETIME) >='''         
		+ CONVERT(VARCHAR, CAST(CONVERT(VARCHAR,@StartDate,101) AS DATETIME) ) + ''''                            
	else                        
		SET @WHERECLAUSE = @WHERECLAUSE         
		+ ' AND CAST(CONVERT(VARCHAR,CHECK_DATE,101) as DATETIME) >= '''         
		+ CONVERT(VARCHAR, CAST(CONVERT(VARCHAR,@StartDate,101) AS DATETIME) )+ ''''                            
	END                            

	IF NOT @EndDate IS NULL                    
	BEGIN                            
		if ltrim(rtrim(@WHERECLAUSE)) = ''                        
		SET @WHERECLAUSE = 'CAST(CONVERT(VARCHAR,CHECK_DATE,101) as DATETIME) <='''         
		+ CONVERT(VARCHAR, CAST(CONVERT(VARCHAR,@EndDate,101) AS DATETIME) )+ ''''                            
	else                        
		SET @WHERECLAUSE = @WHERECLAUSE + ' AND CAST(CONVERT(VARCHAR,CHECK_DATE,101) as DATETIME) <= '''         
		+ CONVERT(VARCHAR, CAST(CONVERT(VARCHAR,@EndDate,101) AS DATETIME) )+ ''''                            
	END        
     
END 
  --Commented For Itrack issue #6893
/*
--Added For Itrack Issue #5497.
-----------------------------------handel issue CHECK 

IF @REPORTTYPE = 'ISSUED'     
begiN    

CREATE TABLE #TEMP_ISSUED
(
CHECK_ID int,
CHECK_DATE datetime ,
CHECK_NUMBER varchar(20),
CHECK_AMOUNT decimal(18,2),
PAYEE_ENTITY_NAME  varchar(2000),
CLAIM_NUMBER varchar(80),
MANUAL_CHECK varchar(10),
PAYMENT_TYPE VARCHAR(200)

)

--Added For Itrack Issue #5497.
declare @SELECTCLAUSE_ISSUED varchar(8000)
/*--Commented and add a new For Itrack Issue #6499
SELECT @SELECTCLAUSE_ISSUED  = 'SELECT DISTINCT  
		 CHECK_ID,        
		 -- CHECK_TYPE,  -- UNIQUE ID FOR CLAIMS CHECK        
		 -- PAYMENT_MODE, -- CHECK / MANUAL CHECK / EFT        
		 CONVERT(CHAR,CHECK_DATE,101) CHECK_DATE,         
		 ISNULL(CHECK_NUMBER,''NOT GENERATED'') AS CHECK_NUMBER,         
		 CHECK_AMOUNT,        
		 CASE         
		 WHEN ISNULL(CLAIM_TO_ORDER_DESC,'''') !=''''        
		 THEN ISNULL(CLAIM_TO_ORDER_DESC,'''')        
		 ELSE       
		 PAYEE_ENTITY_NAME + '' '' + PAYEE_ADD1 + '' '' + PAYEE_ADD2 +  '' '' + PAYEE_CITY + '' '' + '' '' +PAYEE_STATE + '' '' + PAYEE_ZIP end as PAYEE_ENTITY_NAME,        
		 --TRAN_DESC AS CLAIM_NUMBER,        
		 ISNULL(ACCOUNTS.TRAN_DESC,'''') AS CLAIM_NUMBER,        
		 CASE WHEN ISNULL(MANUAL_CHECK,''N'') = ''Y'' THEN ''YES'' ELSE ''NO'' END AS MANUAL_CHECK,          
		 LOOKUP_VALUE_DESC AS PAYMENT_TYPE        
		FROM ACT_CHECK_INFORMATION       
		INNER JOIN MNT_LOOKUP_VALUES ON LOOKUP_UNIQUE_ID=PAYMENT_MODE        
		LEFT JOIN ACT_ACCOUNTS_POSTING_DETAILS ACCOUNTS      
		ON ACCOUNTS.CUSTOMER_ID = ACT_CHECK_INFORMATION.CUSTOMER_ID AND ACCOUNTS.POLICY_ID = ACT_CHECK_INFORMATION.POLICY_ID       
		AND ACCOUNTS.POLICY_VERSION_ID = ACT_CHECK_INFORMATION.POLICY_VER_TRACKING_ID      
		AND ACT_CHECK_INFORMATION.CHECK_AMOUNT = ACCOUNTS.TRANSACTION_AMOUNT      
		and ACCOUNTS.UPDATED_FROM = ''C'' '       */

SELECT @SELECTCLAUSE_ISSUED =' SELECT DISTINCT        
		 ACT_CHECK_INFORMATION.CHECK_ID,        
		 -- CHECK_TYPE,  -- UNIQUE ID FOR CLAIMS CHECK        
		 -- PAYMENT_MODE, -- CHECK / MANUAL CHECK / EFT        
		 CONVERT(CHAR,CHECK_DATE,101) CHECK_DATE,         
		 ISNULL(CHECK_NUMBER,''NOT GENERATED'') AS CHECK_NUMBER,         
		 CHECK_AMOUNT,        
		 CASE         
		 WHEN ISNULL(CLAIM_TO_ORDER_DESC,'''') !=''''        
		 THEN ISNULL(CLAIM_TO_ORDER_DESC,'''')        
		 ELSE       
		 PAYEE_ENTITY_NAME + '' '' + PAYEE_ADD1 + '' '' + PAYEE_ADD2 +  '' '' + PAYEE_CITY + '' '' + '' '' + MCSL.STATE_NAME + '' '' + PAYEE_ZIP end as PAYEE_ENTITY_NAME,        
		 --TRAN_DESC AS CLAIM_NUMBER,        
		 --ISNULL(ACCOUNTS.TRAN_DESC,'') AS CLAIM_NUMBER,
		 CLAIM_NUMBER,        
		 CASE WHEN ISNULL(MANUAL_CHECK,''N'') = ''Y'' THEN ''YES'' ELSE ''NO'' END AS MANUAL_CHECK,          
		 LOOKUP_VALUE_DESC AS PAYMENT_TYPE        
		 FROM ACT_CHECK_INFORMATION       
		 INNER JOIN MNT_LOOKUP_VALUES ON LOOKUP_UNIQUE_ID=PAYMENT_MODE 
		 INNER JOIN CLM_ACTIVITY ACT  
		 ON ACT.CHECK_ID = ACT_CHECK_INFORMATION.CHECK_ID
		 INNER JOIN  CLM_CLAIM_INFO CLM
		 ON CLM.CUSTOMER_ID = ACT_CHECK_INFORMATION.CUSTOMER_ID AND 
		 CLM.POLICY_ID = ACT_CHECK_INFORMATION.POLICY_ID       
		 AND CLM.POLICY_VERSION_ID = ACT_CHECK_INFORMATION.POLICY_VER_TRACKING_ID 
		 AND CLM.CLAIM_ID = ACT.CLAIM_ID	     
		 LEFT JOIN ACT_ACCOUNTS_POSTING_DETAILS ACCOUNTS      
		 ON ACCOUNTS.CUSTOMER_ID = ACT_CHECK_INFORMATION.CUSTOMER_ID AND ACCOUNTS.POLICY_ID = ACT_CHECK_INFORMATION.POLICY_ID       
		 AND ACCOUNTS.POLICY_VERSION_ID = ACT_CHECK_INFORMATION.POLICY_VER_TRACKING_ID      
		 AND ACT_CHECK_INFORMATION.CHECK_AMOUNT = ACCOUNTS.TRANSACTION_AMOUNT 
         INNER JOIN MNT_COUNTRY_STATE_LIST MCSL  ON MCSL.STATE_ID = ACT_CHECK_INFORMATION.PAYEE_STATE    
		 and ACCOUNTS.UPDATED_FROM = ''C'''

		 DECLARE @QUERY_ISSUED VARCHAR(8000)       
		 IF NOT @ORDERBY IS NULL        
		 BEGIN        
		  SET @QUERY_ISSUED = @SELECTCLAUSE_ISSUED + '       ' + @WHERECLAUSE + ' ORDER BY ' + @ORDERBY   
           print (@QUERY_ISSUED) 
		    INSERT INTO #TEMP_ISSUED
		     EXEC (@QUERY_ISSUED)                 
		 END        
		 ELSE      
		 BEGIN      
           --print(@SELECTCLAUSE_ISSUED + '       ' + @WHERECLAUSE) 
		   INSERT INTO #TEMP_ISSUED               
		  EXEC (@SELECTCLAUSE_ISSUED + '       ' + @WHERECLAUSE ) 
		
		 END   

----GET VOID NEGATIVE RECORDS
			--Added For Itrack Issue #5497.
declare @SELECTCLAUSE_ISSUED_VOIDED varchar(8000)

IF NOT @StartDate IS NULL      
BEGIN                            
	if ltrim(rtrim(@WHERECLAUSE_VOID)) = ''                        
		SET @WHERECLAUSE_VOID = 'CAST(CONVERT(VARCHAR,POSTING_DATE,101) as DATETIME) >='''         
		+ CONVERT(VARCHAR, CAST(CONVERT(VARCHAR,@StartDate,101) AS DATETIME) ) + ''''                            
	else                        
		SET @WHERECLAUSE_VOID = @WHERECLAUSE_VOID         
		+ ' AND CAST(CONVERT(VARCHAR,POSTING_DATE,101) as DATETIME) >= '''         
		+ CONVERT(VARCHAR, CAST(CONVERT(VARCHAR,@StartDate,101) AS DATETIME) )+ ''''                            
END                            
     
IF NOT @EndDate IS NULL                    
BEGIN                            
	if ltrim(rtrim(@WHERECLAUSE_VOID)) = ''                        
		SET @WHERECLAUSE_VOID = 'CAST(CONVERT(VARCHAR,POSTING_DATE,101) as DATETIME) <='''         
		+ CONVERT(VARCHAR, CAST(CONVERT(VARCHAR,@EndDate,101) AS DATETIME) )+ ''''                            
	else                        
		SET @WHERECLAUSE_VOID = @WHERECLAUSE_VOID + ' AND CAST(CONVERT(VARCHAR,POSTING_DATE,101) as DATETIME) <= '''         
		+ CONVERT(VARCHAR, CAST(CONVERT(VARCHAR,@EndDate,101) AS DATETIME) )+ ''''                            
END        

/*SELECT @SELECTCLAUSE_ISSUED_VOIDED  = 'SELECT DISTINCT        
		 CHECK_ID,        
		 -- CHECK_TYPE,  -- UNIQUE ID FOR CLAIMS CHECK        
		 -- PAYMENT_MODE, -- CHECK / MANUAL CHECK / EFT        
		 isnull(CONVERT(CHAR,POSTING_DATE,101),'''') as  CHECK_DATE,         
		 ISNULL(CHECK_NUMBER,''NOT GENERATED'') AS CHECK_NUMBER,         
		 -1 * CHECK_AMOUNT,        
		 CASE         
		 WHEN ISNULL(CLAIM_TO_ORDER_DESC,'''') !=''''        
		 THEN ISNULL(CLAIM_TO_ORDER_DESC,'''')        
		 ELSE       
		 PAYEE_ENTITY_NAME + '' '' + PAYEE_ADD1 + '' '' + PAYEE_ADD2 +  '' '' + PAYEE_CITY + '' '' + '' '' +PAYEE_STATE + '' '' + PAYEE_ZIP + '' '' +  ''-'' +'' (Void) '' end as PAYEE_ENTITY_NAME,        
		 --TRAN_DESC AS CLAIM_NUMBER,        
		 ISNULL(ACCOUNTS.TRAN_DESC,'''') AS CLAIM_NUMBER,        
		 CASE WHEN ISNULL(MANUAL_CHECK,''N'') = ''Y'' THEN ''YES'' ELSE ''NO'' END AS MANUAL_CHECK,          
		 LOOKUP_VALUE_DESC AS PAYMENT_TYPE        
		FROM ACT_CHECK_INFORMATION       
		INNER JOIN MNT_LOOKUP_VALUES ON LOOKUP_UNIQUE_ID=PAYMENT_MODE        
		INNER JOIN ACT_ACCOUNTS_POSTING_DETAILS ACCOUNTS      
		ON ACCOUNTS.CUSTOMER_ID = ACT_CHECK_INFORMATION.CUSTOMER_ID 
        AND ACCOUNTS.POLICY_ID = ACT_CHECK_INFORMATION.POLICY_ID       
		AND ACCOUNTS.POLICY_VERSION_ID = ACT_CHECK_INFORMATION.POLICY_VER_TRACKING_ID      
		AND ACT_CHECK_INFORMATION.CHECK_ID = ACCOUNTS.SOURCE_ROW_ID 
		and ACCOUNTS.UPDATED_FROM = ''C'' and ISNULL(GL_UPDATE,0) = ''2'' and ACCOUNTS.ITEM_TRAN_CODE = ''VOID'' ' */        	        
--Commented and add a new For Itrack Issue #6499

SELECT @SELECTCLAUSE_ISSUED_VOIDED  = ' SELECT DISTINCT        
		 ACT_CHECK_INFORMATION.CHECK_ID,        
		 -- CHECK_TYPE,  -- UNIQUE ID FOR CLAIMS CHECK        
		 -- PAYMENT_MODE, -- CHECK / MANUAL CHECK / EFT        
		 isnull(CONVERT(CHAR,POSTING_DATE,101),'''') as  CHECK_DATE,         
		 ISNULL(CHECK_NUMBER,''NOT GENERATED'') AS CHECK_NUMBER,         
		 -1 * CHECK_AMOUNT as CHECK_AMOUNT ,        
		 CASE         
		 WHEN ISNULL(CLAIM_TO_ORDER_DESC,'''') !=''''        
		 THEN ISNULL(CLAIM_TO_ORDER_DESC,'''')        
		 ELSE       
		 PAYEE_ENTITY_NAME + '' '' + PAYEE_ADD1 + '' '' + PAYEE_ADD2 +  '' '' + PAYEE_CITY + '' '' + '' '' + MCSL.STATE_NAME + '' '' + PAYEE_ZIP + '' '' +  ''-'' +'' (Void) '' end as PAYEE_ENTITY_NAME,        
		 --TRAN_DESC AS CLAIM_NUMBER,        
		 --ISNULL(ACCOUNTS.TRAN_DESC,'''') AS CLAIM_NUMBER,
		 CLAIM_NUMBER,        
		 CASE WHEN ISNULL(MANUAL_CHECK,''N'') = ''Y'' THEN ''YES'' ELSE ''NO'' END AS MANUAL_CHECK,          
		 LOOKUP_VALUE_DESC AS PAYMENT_TYPE        
		 FROM ACT_CHECK_INFORMATION       
		 INNER JOIN MNT_LOOKUP_VALUES ON LOOKUP_UNIQUE_ID=PAYMENT_MODE 
		 INNER JOIN CLM_ACTIVITY ACT  
		 ON ACT.CHECK_ID = ACT_CHECK_INFORMATION.CHECK_ID
		 INNER JOIN  CLM_CLAIM_INFO CLM
		 ON CLM.CUSTOMER_ID = ACT_CHECK_INFORMATION.CUSTOMER_ID AND 
		 CLM.POLICY_ID = ACT_CHECK_INFORMATION.POLICY_ID       
		 AND CLM.POLICY_VERSION_ID = ACT_CHECK_INFORMATION.POLICY_VER_TRACKING_ID 
		 AND CLM.CLAIM_ID = ACT.CLAIM_ID	     
		 LEFT JOIN ACT_ACCOUNTS_POSTING_DETAILS ACCOUNTS      
		 ON ACCOUNTS.CUSTOMER_ID = ACT_CHECK_INFORMATION.CUSTOMER_ID AND ACCOUNTS.POLICY_ID = ACT_CHECK_INFORMATION.POLICY_ID       
		 AND ACCOUNTS.POLICY_VERSION_ID = ACT_CHECK_INFORMATION.POLICY_VER_TRACKING_ID      
		 AND ACT_CHECK_INFORMATION.CHECK_AMOUNT = ACCOUNTS.TRANSACTION_AMOUNT 
		 AND ACT_CHECK_INFORMATION.CHECK_ID = ACCOUNTS.SOURCE_ROW_ID and ACCOUNTS.UPDATED_FROM = ''C'' 
         INNER JOIN MNT_COUNTRY_STATE_LIST MCSL  ON MCSL.STATE_ID = ACT_CHECK_INFORMATION.PAYEE_STATE
		 and ISNULL(GL_UPDATE,0) = ''2'' and ACCOUNTS.ITEM_TRAN_CODE = ''VOID'' '
		 
		 
		 DECLARE @QUERY_ISSUED_VOIDED VARCHAR(8000)       
		 IF NOT @ORDERBY IS NULL        
		 BEGIN        
			SET @QUERY_ISSUED_VOIDED = @SELECTCLAUSE_ISSUED_VOIDED + '       ' + @WHERECLAUSE_VOID + ' ORDER BY ' + @ORDERBY        
             print (@QUERY_ISSUED_VOIDED)
			INSERT INTO #TEMP_ISSUED
		     EXEC (@QUERY_ISSUED_VOIDED)                 
		 END        
		 ELSE      
		 BEGIN      
           
			INSERT INTO #TEMP_ISSUED 
			EXEC (@SELECTCLAUSE_ISSUED_VOIDED + '       ' + @WHERECLAUSE_VOID ) 

		 END  

--------------------------------END NEGATIVE VOIDED ENTRIES
  --Added For Itrack Issue #5497.
--Case Added For Itrack Issue #6836.
IF(@ORDERBY IS NOT NULL OR @ORDERBY != '')
BEGIN
SET @FINALQUERY = 'SELECT 
 CHECK_ID,ISNULL(CONVERT(CHAR,CHECK_DATE,101),'''') AS CHECK_DATE  ,CHECK_NUMBER ,CHECK_AMOUNT , PAYEE_ENTITY_NAME
 ,CLAIM_NUMBER,MANUAL_CHECK , PAYMENT_TYPE   
FROM #TEMP_ISSUED ORDER BY ' + @ORDERBY + ',CHECK_ID'
 END
ELSE
BEGIN
SET @FINALQUERY = 'SELECT 
 CHECK_ID,ISNULL(CONVERT(CHAR,CHECK_DATE,101),'''') AS CHECK_DATE  ,CHECK_NUMBER ,CHECK_AMOUNT , PAYEE_ENTITY_NAME
 ,CLAIM_NUMBER,MANUAL_CHECK , PAYMENT_TYPE   
FROM #TEMP_ISSUED ORDER BY CHECK_ID'
END
EXEC  (@FINALQUERY) 

--, CLAIM_NUMBER, CHECK_AMOUNT DESC     
END*/

----------------------end --handle issue CHECK 
--ELSE
--BEGIN    
      /* 
         
		 SELECT @SELECTCLAUSE  = 'SELECT DISTINCT        
		 CHECK_ID,        
		 -- CHECK_TYPE,  -- UNIQUE ID FOR CLAIMS CHECK        
		 -- PAYMENT_MODE, -- CHECK / MANUAL CHECK / EFT        
		 CONVERT(CHAR,CHECK_DATE,101) CHECK_DATE,         
		 ISNULL(CHECK_NUMBER,''NOT GENERATED'') AS CHECK_NUMBER,         
		 CHECK_AMOUNT,        
		 CASE         
		 WHEN ISNULL(CLAIM_TO_ORDER_DESC,'''') !=''''        
		 THEN ISNULL(CLAIM_TO_ORDER_DESC,'''')        
		 ELSE       
		 PAYEE_ENTITY_NAME + '' '' + PAYEE_ADD1 + '' '' + PAYEE_ADD2 +  '' '' + PAYEE_CITY + '' '' + '' '' +PAYEE_STATE + '' '' + PAYEE_ZIP end as PAYEE_ENTITY_NAME,        
		 --TRAN_DESC AS CLAIM_NUMBER,        
		 ISNULL(ACCOUNTS.TRAN_DESC,'''') AS CLAIM_NUMBER,        
		 CASE WHEN ISNULL(MANUAL_CHECK,''N'') = ''Y'' THEN ''YES'' ELSE ''NO'' END AS MANUAL_CHECK,          
		 LOOKUP_VALUE_DESC AS PAYMENT_TYPE        
		FROM ACT_CHECK_INFORMATION       
		INNER JOIN MNT_LOOKUP_VALUES ON LOOKUP_UNIQUE_ID=PAYMENT_MODE        
		LEFT JOIN ACT_ACCOUNTS_POSTING_DETAILS ACCOUNTS      
		ON ACCOUNTS.CUSTOMER_ID = ACT_CHECK_INFORMATION.CUSTOMER_ID AND ACCOUNTS.POLICY_ID = ACT_CHECK_INFORMATION.POLICY_ID       
		AND ACCOUNTS.POLICY_VERSION_ID = ACT_CHECK_INFORMATION.POLICY_VER_TRACKING_ID      
		AND ACT_CHECK_INFORMATION.CHECK_AMOUNT = ACCOUNTS.TRANSACTION_AMOUNT      
		and ACCOUNTS.UPDATED_FROM = ''C'''       	        
		*/ --till here Commented For Itrack issue #6893 
		--Commented and add a new For Itrack Issue #6499
        --IF and else cases  For Itrack issue #6893 and sorting option has been change for posting_date  
IF @REPORTTYPE = 'VOID'
BEGIN
SELECT @SELECTCLAUSE = 'SELECT DISTINCT        
		 ACT_CHECK_INFORMATION.CHECK_ID,        
		 -- CHECK_TYPE,  -- UNIQUE ID FOR CLAIMS CHECK        
		 -- PAYMENT_MODE, -- CHECK / MANUAL CHECK / EFT        
		 isnull(CONVERT(CHAR,POSTING_DATE,101),'''') as  CHECK_DATE,  POSTING_DATE ,        
		 ISNULL(CHECK_NUMBER,''NOT GENERATED'') AS CHECK_NUMBER,         
		 CHECK_AMOUNT as CHECK_AMOUNT ,        
		 CASE         
		 WHEN ISNULL(CLAIM_TO_ORDER_DESC,'''') !=''''        
		 THEN ISNULL(CLAIM_TO_ORDER_DESC,'''')        
		 ELSE       
		 PAYEE_ENTITY_NAME + '' '' + PAYEE_ADD1 + '' '' + PAYEE_ADD2 +  '' '' + PAYEE_CITY + '' '' + '' '' + MCSL.STATE_NAME + '' '' + PAYEE_ZIP end as PAYEE_ENTITY_NAME,        
		 --TRAN_DESC AS CLAIM_NUMBER,        
		 --ISNULL(ACCOUNTS.TRAN_DESC,'''') AS CLAIM_NUMBER,
		 CLAIM_NUMBER,        
		 CASE WHEN ISNULL(MANUAL_CHECK,''N'') = ''Y'' THEN ''YES'' ELSE ''NO'' END AS MANUAL_CHECK,          
		 LOOKUP_VALUE_DESC AS PAYMENT_TYPE        
		 FROM ACT_CHECK_INFORMATION       
		 INNER JOIN MNT_LOOKUP_VALUES ON LOOKUP_UNIQUE_ID=PAYMENT_MODE 
		 INNER JOIN CLM_ACTIVITY ACT  
		 ON ACT.CHECK_ID = ACT_CHECK_INFORMATION.CHECK_ID
		 INNER JOIN  CLM_CLAIM_INFO CLM
		 ON CLM.CUSTOMER_ID = ACT_CHECK_INFORMATION.CUSTOMER_ID AND 
		 CLM.POLICY_ID = ACT_CHECK_INFORMATION.POLICY_ID       
		 AND CLM.POLICY_VERSION_ID = ACT_CHECK_INFORMATION.POLICY_VER_TRACKING_ID 
		 AND CLM.CLAIM_ID = ACT.CLAIM_ID	     
		 LEFT JOIN ACT_ACCOUNTS_POSTING_DETAILS ACCOUNTS      
		 ON ACCOUNTS.CUSTOMER_ID = ACT_CHECK_INFORMATION.CUSTOMER_ID AND ACCOUNTS.POLICY_ID = ACT_CHECK_INFORMATION.POLICY_ID       
		 AND ACCOUNTS.POLICY_VERSION_ID = ACT_CHECK_INFORMATION.POLICY_VER_TRACKING_ID      
		 AND ACT_CHECK_INFORMATION.CHECK_AMOUNT = ACCOUNTS.TRANSACTION_AMOUNT 
		 AND ACT_CHECK_INFORMATION.CHECK_ID = ACCOUNTS.SOURCE_ROW_ID and ACCOUNTS.UPDATED_FROM = ''C'' 
         INNER JOIN MNT_COUNTRY_STATE_LIST MCSL  ON MCSL.STATE_ID = ACT_CHECK_INFORMATION.PAYEE_STATE
		 and ISNULL(GL_UPDATE,0) = ''2'' and ACCOUNTS.ITEM_TRAN_CODE = ''VOID'''  

		 IF NOT @ORDERBY IS NULL        
		 BEGIN        
			SET @QUERY = @SELECTCLAUSE + '       ' + @WHERECLAUSE + ' ORDER BY ' + @ORDERBY        
            
		     EXEC (@QUERY)      
		 END        
		 ELSE      
		 BEGIN
			EXEC (@SELECTCLAUSE + '       ' + @WHERECLAUSE ) 
		 END  
  

END
ELSE
BEGIN
         SELECT @SELECTCLAUSE  = 'SELECT DISTINCT        
		 ACT_CHECK_INFORMATION.CHECK_ID,        
		 -- CHECK_TYPE,  -- UNIQUE ID FOR CLAIMS CHECK        
		 -- PAYMENT_MODE, -- CHECK / MANUAL CHECK / EFT        
		 CONVERT(CHAR,CHECK_DATE,101) CHECK_DATE, CHECK_DATE as  POSTING_DATE ,       
		 ISNULL(CHECK_NUMBER,''NOT GENERATED'') AS CHECK_NUMBER,         
		 CHECK_AMOUNT,        
		 CASE         
		 WHEN ISNULL(CLAIM_TO_ORDER_DESC,'''') !=''''       
		 THEN ISNULL(CLAIM_TO_ORDER_DESC,'''')        
		 ELSE       
		 PAYEE_ENTITY_NAME + '' '' + PAYEE_ADD1 + '' '' + PAYEE_ADD2 +  '' '' + PAYEE_CITY + '' '' + '' '' + MCSL.STATE_NAME + '' '' + PAYEE_ZIP end as PAYEE_ENTITY_NAME,        
		 --TRAN_DESC AS CLAIM_NUMBER,        
		 --ISNULL(ACCOUNTS.TRAN_DESC,'') AS CLAIM_NUMBER,
		 CLAIM_NUMBER,        
		 CASE WHEN ISNULL(MANUAL_CHECK,''N'') = ''Y'' THEN ''YES'' ELSE ''NO'' END AS MANUAL_CHECK,          
		 LOOKUP_VALUE_DESC AS PAYMENT_TYPE        
		 FROM ACT_CHECK_INFORMATION       
		 INNER JOIN MNT_LOOKUP_VALUES ON LOOKUP_UNIQUE_ID=PAYMENT_MODE 
		 INNER JOIN CLM_ACTIVITY ACT  
		 ON ACT.CHECK_ID = ACT_CHECK_INFORMATION.CHECK_ID
		 INNER JOIN  CLM_CLAIM_INFO CLM
		 ON CLM.CUSTOMER_ID = ACT_CHECK_INFORMATION.CUSTOMER_ID AND 
		 CLM.POLICY_ID = ACT_CHECK_INFORMATION.POLICY_ID       
		 AND CLM.POLICY_VERSION_ID = ACT_CHECK_INFORMATION.POLICY_VER_TRACKING_ID 
		 AND CLM.CLAIM_ID = ACT.CLAIM_ID	     
		 LEFT JOIN ACT_ACCOUNTS_POSTING_DETAILS ACCOUNTS      
		 ON ACCOUNTS.CUSTOMER_ID = ACT_CHECK_INFORMATION.CUSTOMER_ID AND ACCOUNTS.POLICY_ID = ACT_CHECK_INFORMATION.POLICY_ID       
		 AND ACCOUNTS.POLICY_VERSION_ID = ACT_CHECK_INFORMATION.POLICY_VER_TRACKING_ID      
		 AND ACT_CHECK_INFORMATION.CHECK_AMOUNT = ACCOUNTS.TRANSACTION_AMOUNT  
		 INNER JOIN MNT_COUNTRY_STATE_LIST MCSL  ON MCSL.STATE_ID = ACT_CHECK_INFORMATION.PAYEE_STATE    
		 AND ACCOUNTS.UPDATED_FROM = ''C'''  
		 
		       
		 IF NOT @ORDERBY IS NULL        
		 BEGIN        
		  SET @QUERY = @SELECTCLAUSE + '       ' + @WHERECLAUSE + ' ORDER BY ' + @ORDERBY        
		   
		   EXEC (@QUERY)			
		 END  
		 ELSE      
		 BEGIN      		        		  
          EXEC (@SELECTCLAUSE + '       ' + @WHERECLAUSE )      
		 END      
		      
  END    --------------
        
END       
--go
--exec PROC_CLAIMS_CHECK_LIST 'outstanding',null,NULL,NULL,NULL,'check_date' 
--rollback tran
      






GO

