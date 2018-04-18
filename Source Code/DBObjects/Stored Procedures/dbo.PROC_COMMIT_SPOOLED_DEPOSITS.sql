IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_COMMIT_SPOOLED_DEPOSITS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_COMMIT_SPOOLED_DEPOSITS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------        
Proc Name       : PROC_COMMIT_SPOOLED_DEPOSITS        
Created by      : Praveen kasana         
Date            : 24/March/2008        
Purpose			: Commits Spooled Deposits Called from EOD
Revison History :        
Used In			: Wolverine        
        
exec PROC_COMMIT_SPOOLED_DEPOSITS 3, '2008-4-17'
-----------------------------------------------------------        
Date     Review By          Comments        
------   ------------       --------------------------------*/  
----drop proc dbo.PROC_COMMIT_SPOOLED_DEPOSITS
CREATE PROC dbo.PROC_COMMIT_SPOOLED_DEPOSITS
(
	@COMMITTED_BY int,
	@DATE_COMMITED datetime
	
)
AS
BEGIN

CREATE TABLE #SPOOLED_DEPOSITS    
(          
	[IDENT_COL] [int] IDENTITY(1,1) NOT NULL ,    
	[DEPOSIT_ID] int ,
	DEPOSIT_NUMBER	Int
)

INSERT INTO #SPOOLED_DEPOSITS
([DEPOSIT_ID] ,DEPOSIT_NUMBER )
SELECT [DEPOSIT_ID]  ,DEPOSIT_NUMBER
FROM  ACT_CURRENT_DEPOSITS WITH(NOLOCK) 
WHERE ISNULL(IS_COMMITED_TO_SPOOL,'N') = 'Y'  AND ISNULL(IS_COMMITED,'N') = 'N'

DECLARE @DEPOSIT_ID INT,
		@DEPOSIT_NUMBER	Int,
		@START_DATETIME	Datetime 

DECLARE @RETVALUE INT
DECLARE @IDENT_COL Int    
SET @IDENT_COL = 1   

WHILE 1 = 1 
BEGIN
	IF NOT EXISTS (SELECT IDENT_COL FROM #SPOOLED_DEPOSITS with(nolock) WHERE IDENT_COL = @IDENT_COL)                  
	BEGIN                  
		BREAK                  
	END   

	SELECT @DEPOSIT_ID = [DEPOSIT_ID] , @DEPOSIT_NUMBER = DEPOSIT_NUMBER , @START_DATETIME = GETDATE()
	FROM #SPOOLED_DEPOSITS WITH(NOLOCK)   
	WHERE IDENT_COL = @IDENT_COL  

	EXECUTE @RETVALUE =  dbo.Proc_CommitACT_CURRENT_DEPOSITS @DEPOSIT_ID ,@DATE_COMMITED,@COMMITTED_BY,NULL,NULL,NULL,NULL

	INSERT INTO EOD_PROCESS_LOG (ACTIVITY_DESCRIPTION , START_DATETIME ,END_DATETIME ,STATUS ,ACTIVITY ,SUB_ACTIVITY )
	VALUES ('Committing deposit number:- ' + CONVERT(VARCHAR,@DEPOSIT_NUMBER) + '.' , @START_DATETIME ,GETDATE() , 'SUCCEDDED' , 100 , 100   ) 

	SET @IDENT_COL = @IDENT_COL + 1
	
END
	DROP TABLE #SPOOLED_DEPOSITS 
END















GO

