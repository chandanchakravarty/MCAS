IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_INSERT_ACCOUNTS_POSTING_ON_COMMIT_CLAIM_ACTIVITY]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_INSERT_ACCOUNTS_POSTING_ON_COMMIT_CLAIM_ACTIVITY]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- DROP PROC dbo.PROC_INSERT_ACCOUNTS_POSTING_ON_COMMIT_CLAIM_ACTIVITY       
/*
Itrack 5543 : Praveen kasana
Problem : @PREV_GENERAL_ACTIVITY was coming wrong if Activity is of Reserve Type
11773 -->Reserve 
11774 -->Not Reserve : Expense Payment
11775 -->Loss Paid , this effects reserve
11776 -->Not Reserve  : Recovery
11805 -->

While calculating @PREV_GENERAL_ACTIVITY and @PREV_REINS_ACTIVITY
we are Excluding Transaction of EXPENSE PAYMENT and RECOVERY

-------------------------Modifed on 16 March 2009 
Allow following Reserve Transaction ACTION_ON_PAYMENT

--General ID
165		New Reserve
166		Change Reserve
167		Close Reserve
168		Re-Open Reserve
205		Reserve Update

--Reinsurance ID
169		New Reinsurance Reserve
170		Change Reinsurance Reserve
171		Close Reinsurance Reserve
172		Re-Open Reinsurance Reserve



Allow Complete and IS ACTIVE Transactions

Modified by :Praveen Kasana
Modified on :16 Sep 2009
Purpose : Corrected @Payment_Method 


*/
CREATE PROC [dbo].[PROC_INSERT_ACCOUNTS_POSTING_ON_COMMIT_CLAIM_ACTIVITY]       
(        
    @CLAIM_ID INTEGER = NULL,        
    @ACTIVITY_ID  INTEGER ,        
    @USER_ID  INTEGER = NULL,        
    @DIV_ID INTEGER = NULL,        
    @DEPT_ID  INTEGER = NULL,        
    @PC_ID  INTEGER = NULL,        
    @DEBIT_ACCOUNT_ID INT = NULL,        
    @CREDIT_ACCOUNT_ID INT = NULL        
  --  @ACCOUNT_ID INT=NULL,         
    --@TRANSACTION_AMOUNT DECIMAL=NULL        
)        
AS        
BEGIN        
      
       
 --GET MAX ROW ID        
 --DECLARE @CURR_MAX_ID AS INTEGER        
 --SELECT @CURR_MAX_ID = ISNULL(MAX(IDENTITY_ROW_ID),0) FROM ACT_ACCOUNTS_POSTING_DETAILS        
         
 --GET ACTIVITY TYPE     
 DECLARE @RESERVE_UPDATE_ACTIVITY INT    
 declare @CLOSE_RESERVE int       
 DECLARE @ACTIVITY_REASON  AS INTEGER        
 DECLARE @PAYMENT_METHOD  AS INTEGER        
 DECLARE @ACTION_ON_PAYMENT AS INTEGER    
 DECLARE @TRANSACTION_CATEGORY AS VARCHAR(20)    
 DECLARE @PREV_REINS_ACTIVITY INT, @PREV_GENERAL_ACTIVITY INT    
    
 SET @RESERVE_UPDATE_ACTIVITY = 205    
      
 SELECT  @ACTIVITY_REASON  = ISNULL(ACTIVITY_REASON,0),    
 @ACTION_ON_PAYMENT=ACTION_ON_PAYMENT      
FROM CLM_ACTIVITY WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID        
 --PRINT   @ACTIVITY_REASON          
     
 SELECT  @TRANSACTION_CATEGORY = TRANSACTION_CATEGORY FROM CLM_TYPE_DETAIL WHERE     
  @ACTIVITY_REASON =TRANSACTION_CODE AND  @ACTION_ON_PAYMENT = DETAIL_TYPE_ID    
    
 IF @TRANSACTION_CATEGORY = 'Reinsurance'    
 BEGIN    
    SET @PREV_REINS_ACTIVITY= (SELECT TOP 1 ACTIVITY_ID 
	FROM CLM_ACTIVITY CA WITH (NOLOCK) LEFT OUTER JOIN CLM_TYPE_DETAIL CTD    
	ON CA.ACTIVITY_REASON =CTD.TRANSACTION_CODE AND  CA.ACTION_ON_PAYMENT = CTD.DETAIL_TYPE_ID    
	WHERE CA.CLAIM_ID=@CLAIM_ID AND CA.ACTIVITY_ID<@ACTIVITY_ID 
	AND CTD.TRANSACTION_CATEGORY = 'Reinsurance'    
	--AND ISNULL(CA.ACTIVITY_REASON,0) NOT IN (11774,11776) --: Itrack 5543
	----: ITRACK 5543	 
	AND ISNULL(CA.ACTION_ON_PAYMENT,0) IN (169,170,171,172) 
	AND ISNULL(CA.ACTIVITY_STATUS,0) IN (11801) --Complete Activity
	AND ISNULL(CA.IS_ACTIVE,'N') ='Y'

    ORDER BY ACTIVITY_ID DESC)    
 END    
 ELSE    
 BEGIN    
    SET @PREV_GENERAL_ACTIVITY= (SELECT TOP 1 ACTIVITY_ID FROM CLM_ACTIVITY CA WITH (NOLOCK) LEFT OUTER JOIN CLM_TYPE_DETAIL CTD    
	ON CA.ACTIVITY_REASON =CTD.TRANSACTION_CODE AND  CA.ACTION_ON_PAYMENT = CTD.DETAIL_TYPE_ID    
	WHERE CA.CLAIM_ID=@CLAIM_ID AND CA.ACTIVITY_ID<@ACTIVITY_ID AND CTD.TRANSACTION_CATEGORY != 'Reinsurance'    
	--AND ISNULL(CA.ACTIVITY_REASON,0) NOT IN (11774,11776) --: Itrack 5543
	----: ITRACK 5543	 
	AND ISNULL(CA.ACTION_ON_PAYMENT,0) IN (165,166,167,168,205) --205 SYSTEM GENERATED
	AND ISNULL(CA.ACTIVITY_STATUS,0) IN (11801) --Complete Activity
	AND ISNULL(CA.IS_ACTIVE,'N') ='Y'

    ORDER BY ACTIVITY_ID DESC)    
 END    
    
 
 DECLARE @UPDATED_FROM NCHAR(10)        
 DECLARE @CREDIT_TO_SALVAGE_EXPENSE NCHAR(10)        
 DECLARE @CREDIT_TO_SUBROGATION_EXPENSE NCHAR(10)        
 DECLARE @ITEM_TRAN_CODE NCHAR(10)        
 DECLARE @ITEM_TRAN_CODE_TYPE  NCHAR(10)        
 DECLARE @SOURCE_ROW_ID  INT        
 DECLARE @ACTIVITY_AMOUNT AS decimal(20,2)        
 declare @PREVIOUS_RESERVE_AMOUNT AS DECIMAL(20,2) 

set @CREDIT_TO_SUBROGATION_EXPENSE = 242       
set @CREDIT_TO_SALVAGE_EXPENSE = 244
       
set @CLOSE_RESERVE = 167   
set @SOURCE_ROW_ID   = @CLAIM_ID -- To set Act Posting Source Rowid.      
         
 IF (@ACTIVITY_REASON = '11776') -- RECOVERY        
 BEGIN         
  SELECT @UPDATED_FROM='L'        
  SELECT @ITEM_TRAN_CODE=''        
  SELECT @ITEM_TRAN_CODE_TYPE=''        
--  SELECT @SOURCE_ROW_ID = NULL        
		SELECT @ACTIVITY_AMOUNT= SUM(ISNULL(AMOUNT,0)) FROM CLM_ACTIVITY_RECOVERY 
		WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID           
  SELECT @PAYMENT_METHOD= ISNULL(PAYMENT_METHOD,0) FROM CLM_ACTIVITY_RECOVERY WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID           
 END        
         
 ELSE IF (@ACTIVITY_REASON = '11774') -- EXPENSE PAYMENT          
 BEGIN 
   IF(@ACTION_ON_PAYMENT = @CREDIT_TO_SUBROGATION_EXPENSE OR @ACTION_ON_PAYMENT = @CREDIT_TO_SALVAGE_EXPENSE)        
	BEGIN
		SELECT @UPDATED_FROM='L'        
		SELECT @ITEM_TRAN_CODE=''        
		SELECT @ITEM_TRAN_CODE_TYPE=''        
	END
   ELSE
	BEGIN
		SELECT @UPDATED_FROM='C'        
		SELECT @ITEM_TRAN_CODE='CHK'        
		SELECT @ITEM_TRAN_CODE_TYPE='CRTD'        
	END
--  SELECT @SOURCE_ROW_ID = ISNULL(MAX(CHECK_ID),0)+1 FROM ACT_CHECK_INFORMATION        
  SELECT @ACTIVITY_AMOUNT= SUM(ISNULL(PAYMENT_AMOUNT,0)) +MAX(ISNULL(ADDITIONAL_EXPENSE,0))  FROM CLM_ACTIVITY_EXPENSE WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID            
  SELECT @PAYMENT_METHOD= ISNULL(PAYMENT_METHOD,0) FROM CLM_ACTIVITY_EXPENSE WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID            
 END        
         
 ELSE IF (@ACTIVITY_REASON = '11775') -- INDEMNITY PAYMENT        
 BEGIN    
  SELECT @UPDATED_FROM='C'        
  SELECT @ITEM_TRAN_CODE='CHK'        
  SELECT @ITEM_TRAN_CODE_TYPE='CRTD'        
--  SELECT @SOURCE_ROW_ID = ISNULL(MAX(CHECK_ID),0)+1 FROM ACT_CHECK_INFORMATION        
  SELECT @ACTIVITY_AMOUNT= SUM(ISNULL(PAYMENT_AMOUNT,0)) FROM CLM_ACTIVITY_PAYMENT WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID            
  SELECT @PAYMENT_METHOD= ISNULL(PAYMENT_METHOD,0) FROM CLM_ACTIVITY_PAYMENT WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID            
 END         
 ELSE IF (@ACTIVITY_REASON = '11773' OR @ACTIVITY_REASON = '11836') -- RESERVE UPDATE/New Reserve        
 BEGIN         
    
  SELECT @UPDATED_FROM='L'        
  SELECT @ITEM_TRAN_CODE=''        
  SELECT @ITEM_TRAN_CODE_TYPE=''        
--  SELECT @SOURCE_ROW_ID = NULL        
  --SELECT TOP 1  @PREVIOUS_RESERVE_AMOUNT = ISNULL(CLAIM_RESERVE_AMOUNT,0) FROM CLM_ACTIVITY WHERE CLAIM_ID=@CLAIM_ID ORDER BY ACTIVITY_ID DESC        
       
   if(@ACTION_ON_PAYMENT = @CLOSE_RESERVE OR @ACTION_ON_PAYMENT = 171)       
   BEGIN      
     SET @PREVIOUS_RESERVE_AMOUNT = 0      
--     SELECT  @PREVIOUS_RESERVE_AMOUNT = ISNULL(RESERVE_AMOUNT,0) FROM CLM_ACTIVITY         
--     WHERE CLAIM_ID=@CLAIM_ID  AND ACTIVITY_ID= @ACTIVITY_ID       
   END      
   ELSE      
   BEGIN      
--     SELECT  TOP 1  @PREVIOUS_RESERVE_AMOUNT = ISNULL(CLAIM_RESERVE_AMOUNT,0) FROM CLM_ACTIVITY         
--     WHERE CLAIM_ID=@CLAIM_ID  AND ACTIVITY_ID<>@ACTIVITY_ID ORDER BY ACTIVITY_ID DESC     
 IF(@TRANSACTION_CATEGORY = 'Reinsurance')    
  BEGIN       
		SELECT  @PREVIOUS_RESERVE_AMOUNT = 
		(CASE WHEN @TRANSACTION_CATEGORY = 'Reinsurance' 
		THEN SUM(ISNULL(RI_RESERVE,0))    
		ELSE SUM(ISNULL(OUTSTANDING,0)) END) FROM CLM_ACTIVITY_RESERVE WITH (NOLOCK)      
		WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@PREV_REINS_ACTIVITY    
  END    
  ELSE 
  BEGIN    
    SELECT  @PREVIOUS_RESERVE_AMOUNT = (CASE WHEN @TRANSACTION_CATEGORY = 'Reinsurance' THEN SUM(ISNULL(RI_RESERVE,0))    
    ELSE SUM(ISNULL(OUTSTANDING,0)) END) FROM CLM_ACTIVITY_RESERVE WITH (NOLOCK)      
    WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@PREV_GENERAL_ACTIVITY   
   --- Added by Rajan on 3/13/2008 to fix 3879, in first case of chg reserve after new, amount was wrong.  
    IF (@PREV_GENERAL_ACTIVITY = 1 and @TRANSACTION_CATEGORY<> 'Reinsurance')  
    BEGIN   
      SELECT  @PREVIOUS_RESERVE_AMOUNT = SUM(ISNULL(OUTSTANDING,0))    
     FROM CLM_ACTIVITY_RESERVE WITH (NOLOCK)      
         WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=0   
    END   
  END     
   END      
      
        
      
  IF(@ACTIVITY_ID = 1)      
   BEGIN      
    SET @PREVIOUS_RESERVE_AMOUNT = 0      
    SELECT @ACTIVITY_AMOUNT= (CASE WHEN @TRANSACTION_CATEGORY = 'Reinsurance' THEN SUM(ISNULL(RI_RESERVE,0))    
 ELSE SUM(ISNULL(OUTSTANDING,0)) END) FROM CLM_ACTIVITY_RESERVE WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=0      
   END      
  ELSE      
  BEGIN      
    IF(@ACTION_ON_PAYMENT = @CLOSE_RESERVE OR @ACTION_ON_PAYMENT = 171)      
      BEGIN      
        SELECT @ACTIVITY_AMOUNT= (CASE WHEN @TRANSACTION_CATEGORY = 'Reinsurance' THEN SUM(ISNULL(RI_RESERVE,0))    
 ELSE SUM(ISNULL(RESERVE_AMOUNT,0)) END) FROM CLM_ACTIVITY WITH (NOLOCK)        
        WHERE CLAIM_ID=@CLAIM_ID  AND ACTIVITY_ID= @ACTIVITY_ID       
      END     
    ELSE IF(@ACTION_ON_PAYMENT = @RESERVE_UPDATE_ACTIVITY)      
      BEGIN    
        --- Added by Rajan on 3/13/2008 to fix 3879, in case of follow up update amount sign was wrong.  
  SET @PREVIOUS_RESERVE_AMOUNT = 0     
  SELECT @ACTIVITY_AMOUNT= -SUM(ISNULL(PAYMENT_AMOUNT,0)) FROM CLM_ACTIVITY_PAYMENT WITH (NOLOCK)     
  WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=(SELECT TOP 1 ACTIVITY_ID FROM CLM_ACTIVITY_PAYMENT WHERE CLAIM_ID=@CLAIM_ID AND IS_ACTIVE='Y' ORDER BY ACTIVITY_ID DESC)    
      END     
    ELSE      
      BEGIN      
        SELECT @ACTIVITY_AMOUNT= (CASE WHEN @TRANSACTION_CATEGORY = 'Reinsurance' THEN SUM(ISNULL(RI_RESERVE,0))    
 ELSE SUM(ISNULL(OUTSTANDING,0)) END) FROM CLM_ACTIVITY_RESERVE WITH (NOLOCK) WHERE CLAIM_ID=@CLAIM_ID AND ACTIVITY_ID=@ACTIVITY_ID         
      END      
  END      
     
  --Make entry in the accounting tables only for the difference between the two amounts        
  SET @ACTIVITY_AMOUNT = ISNULL(@ACTIVITY_AMOUNT,0) - ISNULL(@PREVIOUS_RESERVE_AMOUNT,0)    

		IF (@ACTION_ON_PAYMENT = 170) -- For 3585
		begin
			select @ACTIVITY_AMOUNT = ISNULL(RI_RESERVE , 0) FROM CLM_ACTIVITY  WHERE CLAIM_ID = @CLAIM_ID AND ACTIVITY_ID = @ACTIVITY_ID
		end
 
   
 END        
        
/*      
Commented by Rajan Agrawal - 02-Nov-2007      
      
 if(ISNULL(@PAYMENT_METHOD,0)<>11787)  --11787=Check..if payment is other than check, return        
--  SELECT @SOURCE_ROW_ID = NULL         
        
*/      
    
---PAYEE DETAILS WHERE UPDATED_FROM IS C 05 Dec 2007    
DECLARE @TRAN_ENTITY VARCHAR(200)    
    
IF(@UPDATED_FROM = 'C')    
BEGIN    
 SELECT    
    
 @TRAN_ENTITY = CASE WHEN ISNULL(CLM_PAYEE.TO_ORDER_DESC,'') = '' THEN    
   ISNULL(CLM_PARTIES.NAME,'')    
 ELSE    
 CLM_PAYEE.TO_ORDER_DESC END    
    
 FROM    
 CLM_CLAIM_INFO WITH (NOLOCK)        
 INNER JOIN MNT_USER_LIST     
  ON MNT_USER_LIST.USER_ID = @USER_ID         
 INNER JOIN CLM_PAYEE WITH (NOLOCK)     
  ON CLM_PAYEE.CLAIM_ID= CLM_CLAIM_INFO.CLAIM_ID     
 INNER JOIN CLM_ACTIVITY WITH(NOLOCK)    
  ON CLM_ACTIVITY.CLAIM_ID = CLM_CLAIM_INFO.CLAIM_ID     
  AND CLM_ACTIVITY.ACTIVITY_ID =  CLM_PAYEE.ACTIVITY_ID    
 INNER JOIN CLM_PARTIES WITH(NOLOCK) ON    
   CLM_PARTIES.CLAIM_ID = CLM_PAYEE.CLAIM_ID     
  AND  CLM_PARTIES.PARTY_ID = CLM_PAYEE.PARTY_ID     
 WHERE CLM_CLAIM_INFO.CLAIM_ID=@CLAIM_ID   AND CLM_PAYEE.ACTIVITY_ID=@ACTIVITY_ID       
    
--      
--  SELECT @CHECK_ID = ISNULL(CHECK_ID,0)    
--  FROM CLM_ACTIVITY WITH(NOLOCK)     
--  WHERE     
--  CLM_ACTIVITY.CLAIM_ID=@CLAIM_ID       
--  AND CLM_ACTIVITY.ACTIVITY_ID=@ACTIVITY_ID      
    
END    
ELSE --if Updated from L    
BEGIN    
 SELECT    
 --@TRAN_ENTITY = ISNULL(MNT_LOOKUP_VALUES.LOOKUP_VALUE_DESC,'')    
 @TRAN_ENTITY = ISNULL(CLM_TYPE_DETAIL.DETAIL_TYPE_DESCRIPTION,'')    
 FROM    
 CLM_CLAIM_INFO WITH (NOLOCK)        
 INNER JOIN MNT_USER_LIST     
  ON MNT_USER_LIST.USER_ID = @USER_ID         
 INNER JOIN CLM_ACTIVITY WITH(NOLOCK)    
  ON CLM_ACTIVITY.CLAIM_ID = CLM_CLAIM_INFO.CLAIM_ID     
 INNER JOIN CLM_TYPE_DETAIL WITH(NOLOCK)    
  ON CLM_TYPE_DETAIL.DETAIL_TYPE_ID = CLM_ACTIVITY.ACTION_ON_PAYMENT    
 INNER JOIN MNT_LOOKUP_VALUES WITH(NOLOCK)    
  ON MNT_LOOKUP_VALUES.LOOKUP_UNIQUE_ID = CLM_TYPE_DETAIL.TRANSACTION_CODE    
 WHERE CLM_CLAIM_INFO.CLAIM_ID=@CLAIM_ID   AND CLM_ACTIVITY.ACTIVITY_ID=@ACTIVITY_ID       
     
     
END    
    
---END    
    
    

    
IF (@ACTIVITY_AMOUNT <> 0.0 )  
BEGIN       
    
	         
	--Perform Debit Account Entry        
	INSERT INTO ACT_ACCOUNTS_POSTING_DETAILS (        
		--IDENTITY_ROW_ID   , -- 111        
	  ACCOUNT_ID   , -- 2        
		UPDATED_FROM   , -- 3      
		SUBLEDGER_TYPE   , -- 4        
		SOURCE_ROW_ID   , -- 5   Claim ID      
		MAPPING_SUBLEDGER_ID  , -- 6  Activity ID      
		SOURCE_NUM   , -- 7   Claim Number   --Modified CLAM_ID - ACTIVITY_ID    
		SOURCE_TRAN_DATE   , -- 8        
		SOURCE_EFF_DATE   , -- 9        
		POSTING_DATE   , -- 10        
		TRANSACTION_AMOUNT   , -- 11        
		AGENCY_COMM_PERC   , -- 12        
		AGENCY_COMM_AMT   , -- 13        
		AGENCY_ID   , -- 14        
		CUSTOMER_ID   , -- 15        
		POLICY_ID   , -- 16        
		POLICY_VERSION_ID   , -- 17        
		DIV_ID   , -- 18        
		DEPT_ID   , -- 19        
		PC_ID   , -- 20        
		IS_PREBILL   , -- 21        
		BILL_CODE   , -- 22        
		GROSS_AMOUNT   , -- 23        
		ITEM_TRAN_CODE   , -- 24        
		ITEM_TRAN_CODE_TYPE   , -- 25        
		TRAN_ID   , -- 26        
		LOB_ID   , -- 27        
		SUB_LOB_ID   , -- 28        
		COUNTRY_ID   , -- 29        
		STATE_ID   , -- 30        
		VENDOR_ID   , -- 31        
		TAX_ID   , -- 32        
		ADNL_INFO   , -- 33        
		IS_BANK_RECONCILED   , -- 34        
		RECUR_JOURNAL_VERSION   , -- 35        
		IN_BNK_RECON   , -- 36        
		IS_BALANCE_UPDATED   , -- 37        
		COMMITED_BY   , -- 38        
		COMMITED_BY_CODE   , -- 39        
		COMMITED_BY_NAME   , -- 40            
		COMMISSION_TYPE   , -- 41        
		RISK_ID   , -- 42        
		RISK_TYPE   , -- 43        
		SOURCE_GROUP_ID   , -- 44        
		TRAN_ENTITY   , -- 45        
		TRAN_DESC    -- 46        
	)        
	SELECT        
	        
	  -- @CURR_MAX_ID + 1   , -- 1           IDENTITY_ROW_ID        
	   --@ACCOUNT_ID    , -- 2           ACCOUNT_ID        
	   @DEBIT_ACCOUNT_ID, --2 ACCOUNT_ID         
	   @UPDATED_FROM    , -- 3           UPDATED_FROM        
	   NULL   , -- 4           SUBLEDGER_TYPE        
	   @SOURCE_ROW_ID     , -- 5           SOURCE_ROW_ID        
	--   @CLAIM_ID, --Removed       
	--   NULL   , -- 6           MAPPING_SUBLEDGER_ID        
	   @ACTIVITY_ID,      
	--   NULL   , -- 7     SOURCE_NUM        
	   --CLAIM_NUMBER ,      
	   CAST(@CLAIM_ID AS VARCHAR) + ' - ' + CAST(@ACTIVITY_ID AS VARCHAR),    
	   GETDATE()   , -- 8           SOURCE_TRAN_DATE        
	   GETDATE()   , -- 9           SOURCE_EFF_DATE        
	   GETDATE()   , -- 10           POSTING_DATE        
	   @ACTIVITY_AMOUNT , -- 11           TRANSACTION_AMOUNT        
	   NULL   , -- 12           AGENCY_COMM_PERC        
	   NULL   , -- 13           AGENCY_COMM_AMT        
	   NULL   , -- 14           AGENCY_ID        
	   CCI.CUSTOMER_ID,  -- 15           CUSTOMER_ID        
	   CCI.POLICY_ID,  -- 16           POLICY_ID       
	   CCI.POLICY_VERSION_ID  , -- 17  POLICY_VERSION_ID        
	   @DIV_ID    , -- 18           DIV_ID        
	   @DEPT_ID     , -- 19           DEPT_ID        
	   @PC_ID   , -- 20           PC_ID        
	   NULL   , -- 21           IS_PREBILL        
	   NULL  , -- 22           BILL_CODE        
	   @ACTIVITY_AMOUNT , -- 23           GROSS_AMOUNT        
	   @ITEM_TRAN_CODE , -- 24           ITEM_TRAN_CODE        
	   @ITEM_TRAN_CODE_TYPE , -- 25           ITEM_TRAN_CODE_TYPE        
	   NULL   , -- 26           TRAN_ID        
	--   NULL   , -- 27           LOB_ID        
	   PCPL.POLICY_LOB AS LOB_ID   , -- 27           LOB_ID        
	   NULL   , -- 28           SUB_LOB_ID        
	   NULL   , -- 29           COUNTRY_ID        
	--   NULL   , -- 30           STATE_ID        
	   PCPL.STATE_ID   , -- 30           STATE_ID        
	   NULL   , -- 31           VENDOR_ID        
	   NULL   , -- 32           TAX_ID        
	   NULL  , -- 33           ADNL_INFO        
	   NULL   , -- 34           IS_BANK_RECONCILED        
	   NULL   , -- 35           RECUR_JOURNAL_VERSION        
	   NULL   , -- 36           IN_BNK_RECON        
	   NULL   , -- 37           IS_BALANCE_UPDATED        
	   @USER_ID     , -- 38           COMMITED_BY        
	   USER_LOGIN_ID    , -- 39     COMMITED_BY_CODE        
	   ISNULL(USER_FNAME,' ') + ' ' + ISNULL(USER_LNAME,' ')   , -- 40           COMMITED_BY_NAME        
	   NULL   , -- 41           COMMISSION_TYPE        
	   NULL   , -- 42           RISK_ID        
	   NULL   , -- 43           RISK_TYPE        
	   NULL   , -- 44           SOURCE_GROUP_ID        
	   @TRAN_ENTITY,    
	   --CLAIMANT_NAME   , -- 45           TRAN_ENTITY        
	   CLAIM_NUMBER   -- 46           TRAN_DESC        
	        
	  -- FROM CLM_PAYEE         
	   --LEFT  JOIN CLM_CLAIM_INFO ON CLM_PAYEE.CLAIM_ID= CLM_CLAIM_INFO.CLAIM_ID         
	FROM  CLM_CLAIM_INFO CCI WITH (NOLOCK)
	INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL ON PCPL.CUSTOMER_ID= CCI.CUSTOMER_ID 
	AND PCPL.POLICY_ID= CCI.POLICY_ID AND PCPL.POLICY_VERSION_ID = CCI.POLICY_VERSION_ID
	INNER JOIN MNT_USER_LIST     
	 ON MNT_USER_LIST.USER_ID = @USER_ID         
	    
	  -- WHERE CLM_PAYEE.CLAIM_ID=@CLAIM_ID    AND ACTIVITY_ID=@ACTIVITY_ID        
	WHERE CCI.CLAIM_ID=@CLAIM_ID       
	        
	        
	--Perform Credit Account Entry        
	INSERT INTO ACT_ACCOUNTS_POSTING_DETAILS (        
		--IDENTITY_ROW_ID   , -- 111        
		ACCOUNT_ID   , -- 2        
		UPDATED_FROM   , -- 3        
		SUBLEDGER_TYPE   , -- 4        
		SOURCE_ROW_ID   , -- 5        
		MAPPING_SUBLEDGER_ID  , -- 6        
		SOURCE_NUM   , -- 7        
		SOURCE_TRAN_DATE   , -- 8        
		SOURCE_EFF_DATE   , -- 9        
		POSTING_DATE   , -- 10        
		TRANSACTION_AMOUNT   , -- 11        
		AGENCY_COMM_PERC   , -- 12        
		AGENCY_COMM_AMT   , -- 13        
		AGENCY_ID   , -- 14        
		CCI.CUSTOMER_ID   , -- 15        
		CCI.POLICY_ID   , -- 16        
		CCI.POLICY_VERSION_ID   , -- 17        
		DIV_ID   , -- 18        
		DEPT_ID   , -- 19        
		PC_ID   , -- 20      
		IS_PREBILL   , -- 21        
		BILL_CODE   , -- 22        
		GROSS_AMOUNT   , -- 23        
		ITEM_TRAN_CODE   , -- 24        
		ITEM_TRAN_CODE_TYPE   , -- 25        
		TRAN_ID   , -- 26        
		LOB_ID   , -- 27        
		SUB_LOB_ID   , -- 28        
		COUNTRY_ID   , -- 29        
		STATE_ID   , -- 30        
		VENDOR_ID   , -- 31        
		TAX_ID   , -- 32        
		ADNL_INFO   , -- 33        
		IS_BANK_RECONCILED   , -- 34        
		RECUR_JOURNAL_VERSION   , -- 35        
		IN_BNK_RECON   , -- 36        
		IS_BALANCE_UPDATED   , -- 37        
		COMMITED_BY   , -- 38        
		COMMITED_BY_CODE   , -- 39        
		COMMITED_BY_NAME   , -- 40        
		COMMISSION_TYPE   , -- 41        
		RISK_ID   , -- 42        
		RISK_TYPE   , -- 43        
		SOURCE_GROUP_ID   , -- 44      
		TRAN_ENTITY   , -- 45        
		TRAN_DESC    -- 46        
	)        
	SELECT        
	        
	  -- @CURR_MAX_ID + 1   , -- 1           IDENTITY_ROW_ID        
	   --@ACCOUNT_ID    , -- 2           ACCOUNT_ID        
	   @CREDIT_ACCOUNT_ID, --2 ACCOUNT_ID         
	   @UPDATED_FROM    , -- 3           UPDATED_FROM        
	   NULL   , -- 4           SUBLEDGER_TYPE        
	   @SOURCE_ROW_ID     , -- 5           SOURCE_ROW_ID        
	--   @CLAIM_ID,      
	--   NULL   , -- 6           MAPPING_SUBLEDGER_ID        
	   @ACTIVITY_ID,      
	--   NULL   , -- 7     SOURCE_NUM        
	   --CLAIM_NUMBER,      
		CAST(@CLAIM_ID AS VARCHAR) + ' - ' + CAST(@ACTIVITY_ID AS VARCHAR),    
	   GETDATE()   , -- 8 SOURCE_TRAN_DATE        
	   GETDATE()   , -- 9           SOURCE_EFF_DATE        
	   GETDATE()   , -- 10           POSTING_DATE        
	   -1 * @ACTIVITY_AMOUNT , -- 11           TRANSACTION_AMOUNT        
	   NULL   , -- 12           AGENCY_COMM_PERC        
	   NULL   , -- 13           AGENCY_COMM_AMT        
	   NULL   , -- 14           AGENCY_ID        
	   CCI.CUSTOMER_ID,  -- 15           CUSTOMER_ID        
	   CCI.POLICY_ID,  -- 16           POLICY_ID        
	   CCI.POLICY_VERSION_ID   , -- 17  POLICY_VERSION_ID        
	   @DIV_ID    , -- 18           DIV_ID        
	   @DEPT_ID     , -- 19           DEPT_ID        
	   @PC_ID   , -- 20           PC_ID        
	   NULL   , -- 21           IS_PREBILL        
	   NULL   , -- 22           BILL_CODE        
	   -1 * @ACTIVITY_AMOUNT , -- 23           GROSS_AMOUNT        
	   @ITEM_TRAN_CODE , -- 24           ITEM_TRAN_CODE        
	   @ITEM_TRAN_CODE_TYPE , -- 25           ITEM_TRAN_CODE_TYPE        
	   NULL   , -- 26           TRAN_ID        
	--   NULL   , -- 27           LOB_ID        
	   PCPL.POLICY_LOB AS LOB_ID   , -- 27           LOB_ID        
	   NULL   , -- 28           SUB_LOB_ID        
	   NULL   , -- 29           COUNTRY_ID        
	--   NULL   , -- 30           STATE_ID        
	   PCPL.STATE_ID   , -- 30           STATE_ID        
	   NULL   , -- 31           VENDOR_ID        
	   NULL  , -- 32           TAX_ID        
	   NULL   , -- 33           ADNL_INFO        
	   NULL   , -- 34           IS_BANK_RECONCILED        
	   NULL   , -- 35           RECUR_JOURNAL_VERSION        
	   NULL   , -- 36           IN_BNK_RECON        
	   NULL   , -- 37           IS_BALANCE_UPDATED        
	   @USER_ID     , -- 38           COMMITED_BY        
	   USER_LOGIN_ID    , -- 39     COMMITED_BY_CODE        
	   ISNULL(USER_FNAME,' ') + ' ' + ISNULL(USER_LNAME,' ')   , -- 40           COMMITED_BY_NAME        
	   NULL   , -- 41           COMMISSION_TYPE        
	   NULL   , -- 42           RISK_ID        
	   NULL   , -- 43           RISK_TYPE        
	   NULL   , -- 44           SOURCE_GROUP_ID       
	   @TRAN_ENTITY,    
	  --CLAIMANT_NAME   , -- 45          TRAN_ENTITY        
	   CLAIM_NUMBER   -- 46           TRAN_DESC        
	        
	  -- FROM CLM_PAYEE         
	   --LEFT  JOIN CLM_CLAIM_INFO ON CLM_PAYEE.CLAIM_ID= CLM_CLAIM_INFO.CLAIM_ID         
	   from  CLM_CLAIM_INFO  CCI WITH (NOLOCK)       
		INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL ON PCPL.CUSTOMER_ID= CCI.CUSTOMER_ID 
		AND PCPL.POLICY_ID= CCI.POLICY_ID AND PCPL.POLICY_VERSION_ID = CCI.POLICY_VERSION_ID
		inner JOIN MNT_USER_LIST ON MNT_USER_LIST.USER_ID = @USER_ID     
	    
	 -- WHERE CLM_PAYEE.CLAIM_ID=@CLAIM_ID    AND ACTIVITY_ID=@ACTIVITY_ID        
	 WHERE CCI.CLAIM_ID=@CLAIM_ID      

END
	        
END        
    
-- select @updated_from    
-- select @check_id    
-- select * from ACT_ACCOUNTS_POSTING_DETAILS where source_num = '607 - 21'    
    
--      






GO

