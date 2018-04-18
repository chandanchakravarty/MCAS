IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ProcessAutoRecurJournalEntries]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ProcessAutoRecurJournalEntries]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                            
Proc Name       : dbo.Proc_ProcessAutoRecurJournalEntries                    
Created by      : Ravindra Gupta
Date            : 03-13-2007                 
Purpose         : Posting the Recurring Journal Entries from EOD Process.      
Revison History :                            
Used In         : Wolverine                     
*****************************************************************/
-- drop proc dbo.Proc_ProcessAutoRecurJournalEntries
CREATE PROC  [dbo].[Proc_ProcessAutoRecurJournalEntries]
(
	@USER_ID Int
)
AS
BEGIN 

DECLARE	@FREQUENCY SMALLINT,
	@END_DATE DATETIME,
	@LAST_VALID_POSTING_DATE DATETIME,
	@DAY_OF_WK SMALLINT,
	@JOURNAL_ID INT,
	@START_DATE Datetime

CREATE  TABLE #TMP_RECURRING_JE
(
IDEN_COL Int Identity(1,1) not null, 
JOURNAL_ID INT,
FREQUENCY SMALLINT,
DAY_OF_WK SMALLINT,
START_DATE Datetime,
END_DATE DATETIME,
LAST_VALID_POSTING_DATE DATETIME
)	

INSERT INTO #TMP_RECURRING_JE(
	JOURNAL_ID , FREQUENCY , DAY_OF_WK , START_DATE ,
	END_DATE , LAST_VALID_POSTING_DATE 
	)	
SELECT  A.JOURNAL_ID , A.FREQUENCY , A.DAY_OF_THE_WK , A.START_DATE ,
	A.END_DATE , A.LAST_VALID_POSTING_DATE 
	FROM ACT_JOURNAL_MASTER A 
	INNER JOIN ACT_FREQUENCY_MASTER F        
	ON A.FREQUENCY = F.FREQUENCY_CODE      
	WHERE  A.JOURNAL_GROUP_TYPE = 'RC'       
	--AND A.START_DATE <= GETDATE() AND A.END_DATE >= GETDATE()      
	AND DATEDIFF(DD,GETDATE() , A.START_DATE) <=0 
	AND DATEDIFF(DD,GETDATE() , A.END_DATE) >=0 
	AND ISNULL(A.IS_ACTIVE,'Y') = 'Y'
	AND (	SELECT ISNULL(SUM(AMOUNT),0) 
		FROM ACT_JOURNAL_LINE_ITEMS WHERE JOURNAL_ID = A.JOURNAL_ID) = 0 

DECLARE @IDEN_COL Int,
	@RETVAL Int
SET @IDEN_COL  = 1
WHILE 1 = 1
BEGIN 
	IF NOT EXISTS (SELECT IDEN_COL FROM #TMP_RECURRING_JE WHERE IDEN_COL  = @IDEN_COL )
	BEGIN 
		BREAK
	END
	SELECT  @FREQUENCY = FREQUENCY , @END_DATE = END_DATE ,
		@LAST_VALID_POSTING_DATE = LAST_VALID_POSTING_DATE ,
		@DAY_OF_WK = DAY_OF_WK, @JOURNAL_ID = JOURNAL_ID , @START_DATE = START_DATE 
	FROM #TMP_RECURRING_JE WHERE IDEN_COL  = @IDEN_COL

	exec Proc_PostAutoRecurJournalEntries @FREQUENCY , @END_DATE,@LAST_VALID_POSTING_DATE,
		@DAY_OF_WK,@JOURNAL_ID,@START_DATE ,@USER_ID , @RETVAL out

	SELECT @FREQUENCY , @END_DATE,@LAST_VALID_POSTING_DATE,
		@DAY_OF_WK,@JOURNAL_ID,@START_DATE
	SET  @IDEN_COL = @IDEN_COL + 1
END

RETURN 1
END








GO

