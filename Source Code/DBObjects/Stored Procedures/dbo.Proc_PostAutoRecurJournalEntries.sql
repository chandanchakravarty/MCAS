IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_PostAutoRecurJournalEntries]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_PostAutoRecurJournalEntries]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop proc dbo.Proc_PostAutoRecurJournalEntries
--go
/*----------------------------------------------------------                            
Proc Name       : dbo.Proc_PostAutoRecurJournalEntries                    
Created by      : Vijay Arora                        
Date            : 22-02-2006                 
Purpose         : Posting the Recurring Journal Entries from EOD Process.      
Revison History :                            
Used In         : Wolverine                     
Modified By		: Ravindra Gupta
Modified Date 	: 03-13-2007
Purpose			: Implementation issue (Gap in the behaviour of SP and the document
------------------------------------------------------------                            
Date     Review By          Comments                            
------   ------------       -------------------------*/                       
-- drop proc dbo.Proc_PostAutoRecurJournalEntries
CREATE PROC  [dbo].[Proc_PostAutoRecurJournalEntries]
(                      
	@FREQUENCY SMALLINT,
	@END_DATE DATETIME,
	@LAST_VALID_POSTING_DATE DATETIME,
	@DAY_OF_WK SMALLINT,
	@JOURNAL_ID INT,
	@START_DATE Datetime,
	@USER_ID Int,
	@RETVAL int out
)
AS                      
BEGIN                      
DECLARE @JE_TYPE_MANUAL  Varchar(4),
		@JOURNAL_ENTRY_NO Varchar(20),
		@TEMP_NEW_DATE DATETIME, 
		@TEMP_ID INT, 
		@TEMP_LINE_ID INT    ,
		@EOD_DATE Datetime,
		@LINE_ITEM_TOTAL_AMOUNT DECIMAL(18,2),
		@LINE_ITEM_COUNT INT
	

	SET @JE_TYPE_MANUAL   = 'ML'      
	SET	@EOD_DATE = GETDATE()

	SELECT @LINE_ITEM_COUNT = COUNT(JE_LINE_ITEM_ID) FROM ACT_JOURNAL_LINE_ITEMS (NOLOCK) WHERE JOURNAL_ID = @JOURNAL_ID
	IF(@LINE_ITEM_COUNT = 0) -- NO LINE ITEMS EXIST FOR THIS JE
	BEGIN
		SET @RETVAL = -2
		RETURN
	END
	SELECT @LINE_ITEM_TOTAL_AMOUNT = Sum(AMOUNT) FROM ACT_JOURNAL_LINE_ITEMS (NOLOCK) WHERE JOURNAL_ID = @JOURNAL_ID
	IF(@LINE_ITEM_TOTAL_AMOUNT != 0) -- If Proof is not 0 : then cannot Post JEs
	BEGIN
		SET @RETVAL = -1
		RETURN
	END

	CREATE TABLE #JOURNAL_LINE_ITEM_TEMP
	(    
	JE_LINE_ITEM_ID INT,       
	JOURNAL_ID  INT,       
	DIV_ID SMALLINT,       
	DEPT_ID SMALLINT,       
	PC_ID SMALLINT,      
	CUSTOMER_ID INT,       
	POLICY_ID SMALLINT,       
	POLICY_VERSION_ID SMALLINT,       
	AMOUNT DECIMAL(18,2),      
	TYPE NVARCHAR(10),      
	REGARDING VARCHAR(30),      
	REF_CUSTOMER INT,      
	ACCOUNT_ID INT,      
	BILL_TYPE NCHAR(4),      
	NOTE NVARCHAR(200),      
	IS_ACTIVE NCHAR(2),      
	CREATED_BY INT, 
	POLICY_NUMBER Varchar(20),
	TRAN_CODE Varchar(20),
	TEMP_ITEM_ID INT IDENTITY(1,1)
	) 

	DECLARE @FIRST_ENTRY Bit,
			@NEXT_POSTING_DATE DateTime
	
	SET @DAY_OF_WK = @DAY_OF_WK - 2
	SET @FIRST_ENTRY = 0

	--If no posting have done till now for this then set the last posting date to 
	--one day before the start date
	IF(@LAST_VALID_POSTING_DATE IS NULL) 
	BEGIN 
		SET @LAST_VALID_POSTING_DATE = ISNULL(@LAST_VALID_POSTING_DATE,DATEADD(DD,-1,@START_DATE))
		SET @FIRST_ENTRY = 1
	END

WHILE (1=1)
BEGIN 
		IF @FREQUENCY = 1 --WEEKLY      
		BEGIN
			IF (@FIRST_ENTRY = 1)
			BEGIN 
				SET @TEMP_NEW_DATE = DATEADD(WK, DATEDIFF(WK,0, @LAST_VALID_POSTING_DATE ), @DAY_OF_WK )
			END
			ELSE	
			BEGIN 
				SET @TEMP_NEW_DATE  = DATEADD(dd,7, @LAST_VALID_POSTING_DATE)				
			END	
			
			SET @FIRST_ENTRY = 0
		END

		
		IF @FREQUENCY = 2     --BI-WEEKLY  
		BEGIN 
			IF (@FIRST_ENTRY = 1)
			BEGIN 
				SET @TEMP_NEW_DATE = @START_DATE 
			END
			ELSE		
			BEGIN    
				SET @TEMP_NEW_DATE = DATEADD(DAY,15,@LAST_VALID_POSTING_DATE)       
			END
		END

		IF @FREQUENCY = 3     --MONTHLY    
		BEGIN 
			IF (@FIRST_ENTRY = 1)
			BEGIN 
				SET @TEMP_NEW_DATE = @START_DATE 
			END
			ELSE		
			BEGIN      
				SET @TEMP_NEW_DATE = DATEADD(MONTH,1,@LAST_VALID_POSTING_DATE)        
			END
		END
		
		IF @FREQUENCY = 4     --QUARTERLY      
		BEGIN 
			IF (@FIRST_ENTRY = 1)
			BEGIN 
				SET @TEMP_NEW_DATE = @START_DATE 
			END
			ELSE		
			BEGIN    
				SET @TEMP_NEW_DATE = DATEADD(MONTH,3,@LAST_VALID_POSTING_DATE)         
			END
		END
		
		IF @FREQUENCY = 5     --SEMI-ANNUALY    
		BEGIN 
			IF (@FIRST_ENTRY = 1)
			BEGIN 
				SET @TEMP_NEW_DATE = @START_DATE 
			END
			ELSE		
			BEGIN    
				SET @TEMP_NEW_DATE = DATEADD(MONTH,6,@LAST_VALID_POSTING_DATE)         
			END
		END
		
		IF @FREQUENCY = 6     --ANNUAL   
		BEGIN 
			IF (@FIRST_ENTRY = 1)
			BEGIN 
				SET @TEMP_NEW_DATE = @START_DATE 
			END
			ELSE		
			BEGIN       
				SET @TEMP_NEW_DATE = DATEADD(YEAR,1,@LAST_VALID_POSTING_DATE)       
			END
		END
		
		IF @TEMP_NEW_DATE > @LAST_VALID_POSTING_DATE AND  @TEMP_NEW_DATE <= @EOD_DATE --@END_DATE      
		BEGIN      

			SELECT @TEMP_ID = IsNull(Max(JOURNAL_ID), 0) + 1, 
				@JOURNAL_ENTRY_NO = IsNull(Max(Convert(numeric, RTrim(Ltrim(JOURNAL_ENTRY_NO)))), 0) + 1
			FROM ACT_JOURNAL_MASTER (NOLOCK)
	      
			INSERT INTO ACT_JOURNAL_MASTER(      
				JOURNAL_ID, JOURNAL_GROUP_TYPE, TRANS_DATE,  JOURNAL_GROUP_CODE,      
				JOURNAL_ENTRY_NO, [DESCRIPTION], DIV_ID, DEPT_ID,      
				PC_ID, GL_ID, FISCAL_ID, FREQUENCY,      
				START_DATE, END_DATE, DAY_OF_THE_WK, LAST_PROCESSED_DATE,      
				IS_COMMITED, DATE_COMMITED, IMPORTSTATUS, LAST_VALID_POSTING_DATE,      
				NO_OF_RUN, IS_ACTIVE, CREATED_BY, CREATED_DATETIME
				)      
			SELECT  @TEMP_ID, @JE_TYPE_MANUAL , @TEMP_NEW_DATE ,JOURNAL_GROUP_CODE,      
				@JOURNAL_ENTRY_NO, [DESCRIPTION],  DIV_ID, DEPT_ID,      
				PC_ID, GL_ID, FISCAL_ID, FREQUENCY,      
				NULL , NULL , NULL , NULL ,      
				IS_COMMITED, DATE_COMMITED, IMPORTSTATUS, NULL ,      
				NO_OF_RUN, IS_ACTIVE, CREATED_BY, @EOD_DATE      
			FROM  ACT_JOURNAL_MASTER (NOLOCK) WHERE JOURNAL_ID = @JOURNAL_ID      
	
	      
	 		SELECT @TEMP_LINE_ID = (MAX(JE_LINE_ITEM_ID) + 1) FROM ACT_JOURNAL_LINE_ITEMS (NOLOCK)     
	        
	        
	        TRUNCATE TABLE #JOURNAL_LINE_ITEM_TEMP    
		
			INSERT INTO #JOURNAL_LINE_ITEM_TEMP(      
				JE_LINE_ITEM_ID, JOURNAL_ID, DIV_ID,  DEPT_ID, 
				PC_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID,       
				AMOUNT,	TYPE, REGARDING, REF_CUSTOMER,      
				ACCOUNT_ID, BILL_TYPE, NOTE, IS_ACTIVE,      
				CREATED_BY, POLICY_NUMBER,TRAN_CODE
				)       
			SELECT  JE_LINE_ITEM_ID, @TEMP_ID, DIV_ID,DEPT_ID,       
				PC_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID,       
				AMOUNT, TYPE,REGARDING, REF_CUSTOMER,      
				ACCOUNT_ID, BILL_TYPE, NOTE, IS_ACTIVE,      
				CREATED_BY, POLICY_NUMBER,TRAN_CODE       
			FROM ACT_JOURNAL_LINE_ITEMS (NOLOCK) WHERE JOURNAL_ID = @JOURNAL_ID      
	   
			INSERT INTO ACT_JOURNAL_LINE_ITEMS(      
				JE_LINE_ITEM_ID, JOURNAL_ID , DIV_ID,  DEPT_ID, 
				PC_ID, CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID,       
				AMOUNT,	TYPE, REGARDING, REF_CUSTOMER,      
				ACCOUNT_ID, BILL_TYPE, NOTE, IS_ACTIVE,      
				CREATED_BY,CREATED_DATETIME , POLICY_NUMBER,TRAN_CODE
				)      
			SELECT 	
				(TEMP_ITEM_ID + @TEMP_LINE_ID),	JOURNAL_ID , DIV_ID, DEPT_ID,       
				PC_ID, 	CUSTOMER_ID, POLICY_ID,	POLICY_VERSION_ID,       
				AMOUNT, TYPE, REGARDING, REF_CUSTOMER,      
				ACCOUNT_ID, BILL_TYPE, NOTE, IS_ACTIVE,      
				CREATED_BY, @EOD_DATE , POLICY_NUMBER,TRAN_CODE      
			FROM #JOURNAL_LINE_ITEM_TEMP      
		
			-- Committ the JE 
			exec Proc_CommitACT_JOURNAL_MASTER  @TEMP_ID, @USER_ID ,null,null,null
			
			UPDATE ACT_JOURNAL_MASTER SET LAST_PROCESSED_DATE = @EOD_DATE 
				, NO_OF_RUN = ISNULL(NO_OF_RUN,0) + 1 , 
				LAST_VALID_POSTING_DATE  = @TEMP_NEW_DATE
			WHERE JOURNAL_ID = @JOURNAL_ID

			SET  @LAST_VALID_POSTING_DATE  = @TEMP_NEW_DATE

		END      
		ELSE
		BEGIN 
			BREAK
		END
END

DROP TABLE #JOURNAL_LINE_ITEM_TEMP    
UPDATE ACT_JOURNAL_MASTER SET LAST_PROCESSED_DATE = @EOD_DATE 
	--,LAST_VALID_POSTING_DATE = @LAST_VALID_POSTING_DATE
WHERE JOURNAL_ID = @JOURNAL_ID
	
SET @RETVAL = 1
RETURN
END                      


--GO 
--DECLARE @R int
--EXEC Proc_PostAutoRecurJournalEntries 1,'2008-12-30' , null , 5 , 188 , '2008-06-11' , 3 , @R out 
--
----select top 10 * from ACT_JOURNAL_MASTER order by journal_id desc 
--
--SELECT  A.JOURNAL_ID , A.FREQUENCY , A.DAY_OF_THE_WK , A.START_DATE ,
--A.END_DATE , A.LAST_VALID_POSTING_DATE , A.* 
--FROM ACT_JOURNAL_MASTER A 
--INNER JOIN ACT_FREQUENCY_MASTER F        
--ON A.FREQUENCY = F.FREQUENCY_CODE      
--WHERE  A.JOURNAL_GROUP_TYPE = 'RC'       
--AND A.START_DATE <= '06/11/2008' AND A.END_DATE >= '06/11/2008'
--AND (	SELECT ISNULL(SUM(AMOUNT),0) 
--FROM ACT_JOURNAL_LINE_ITEMS WHERE JOURNAL_ID = A.JOURNAL_ID) = 0 
--
--
--
--ROLLBACK TRAN 












GO

