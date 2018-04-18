IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPoliciesToGeneratePremiumNotice]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPoliciesToGeneratePremiumNotice]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran 
--drop PROC dbo.Proc_GetPoliciesToGeneratePremiumNotice
--go 
/*----------------------------------------------------------                
Proc Name       :  dbo.Proc_GetPoliciesToGeneratePremiumNotice
Created by      :  Ravindra
Date            :  7-9-207
Purpose         :  
Revison History :                
Used In         :  Wolverine                
                
exec dbo.Proc_GetPoliciesToGeneratePremiumNotice
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
-- drop PROC dbo.Proc_GetPoliciesToGeneratePremiumNotice
CREATE PROC dbo.Proc_GetPoliciesToGeneratePremiumNotice
AS
BEGIN 
DECLARE @EOD_DATE Datetime 

SET @EOD_DATE = DATEADD(DD, 1 , GETDATE() ) 
	
CREATE TABLE #TMP_NOTICES 
(
	IDEN_COL			Int Identity(1,1),
	SPOOL_ID			Int,
	CUSTOMER_ID			Int,
	POLICY_ID			Int,
	POLICY_VERSION_ID	Int,
	NOTICE_DUE_DATE		DateTime,
	NEW_DUE_DATE		Datetime,	
	MINDAYS_FROM_NOTICE Int,
	SENT_AGAINST		Varchar(500)
) 

INSERT INTO #TMP_NOTICES 
(
	SPOOL_ID , 	CUSTOMER_ID	, 	POLICY_ID	, 
	POLICY_VERSION_ID	, 	
	NOTICE_DUE_DATE		,
	NEW_DUE_DATE , 	
	SENT_AGAINST 
) 
SELECT  SPOOL.SPOOL_ID ,SPOOL.CUSTOMER_ID , SPOOL.POLICY_ID , 
	SPOOL.POLICY_VERSION_ID , SPOOL.NOTICE_DUE_DATE ,
	SPOOL.NOTICE_DUE_DATE , 
-- 	CASE WHEN DATEDIFF(DD,@EOD_DATE,SPOOL.NOTICE_DUE_DATE) >= SPOOL.MINDAYS_FROM_NOTICE
-- 			THEN SPOOL.NOTICE_DUE_DATE
-- 		ELSE DATEADD(DD,SPOOL.MINDAYS_FROM_NOTICE ,  @EOD_DATE ) 
-- 	END ,
	SPOOL.SENT_AGAINST
FROM ACT_PREMIUM_NOTICE_SPOOL SPOOL
WHERE ISNULL(PROCESSED,0) <> 1
--and SPOOL.CUSTOMER_ID   = 1448

DECLARE @IDEN_COL Int,
		@REF_IDS Varchar(500),
		@NEW_DUE_DATE Datetime
SET @IDEN_COL = 1
WHILE (1=1)
BEGIN 
	IF NOT EXISTS ( SELECT IDEN_COL FROM #TMP_NOTICES WHERE IDEN_COL = @IDEN_COL)
	BEGIN 
		BREAK
	END	
	
	SELECT @NEW_DUE_DATE = NEW_DUE_DATE ,
			@REF_IDS = SENT_AGAINST 
	FROM #TMP_NOTICES
	WHERE IDEN_COL  = @IDEN_COL 

	DECLARE @QUERY VARCHAR(1600)

 	SET @QUERY = 'UPDATE ACT_CUSTOMER_OPEN_ITEMS SET DUE_DATE = ''' + CONVERT(VARCHAR,@NEW_DUE_DATE) + 
 		'''  WHERE IDEN_ROW_ID IN (' +  @REF_IDS + ')'
 
 	EXEC (@QUERY)	
	
	
	--Ravindra(11-1-07) Need to update fees item associated with instalment along with premium item
	--Ravindra(1-12-2010): As per Enhancement 6906, Make Sweep date for installment equal to due date 
	SET @QUERY = 'UPDATE ACT_CUSTOMER_OPEN_ITEMS SET DUE_DATE = ''' + CONVERT(VARCHAR,@NEW_DUE_DATE) + 
			''' , SWEEP_DATE = ''' + CONVERT(VARCHAR,@NEW_DUE_DATE) + '''  WHERE INSTALLMENT_ROW_ID IN
			(SELECT ROW_ID FROM ACT_POLICY_INSTALLMENT_DETAILS WITH(NOLOCK) WHERE ROW_ID IN 
		(SELECT INSTALLMENT_ROW_ID FROM ACT_CUSTOMER_OPEN_ITEMS WITH(NOLOCK) WHERE IDEN_ROW_ID IN (' +  @REF_IDS + ')))'

	EXEC (@QUERY)	


	SET @IDEN_COL = @IDEN_COL + 1
	
		
END	

SELECT  SPOOL.SPOOL_ID ,SPOOL.CUSTOMER_ID , SPOOL.POLICY_ID , 
SPOOL.POLICY_VERSION_ID , 
SPOOL.NEW_DUE_DATE AS NOTICE_DUE_DATE
FROM #TMP_NOTICES SPOOL
DROP TABLE #TMP_NOTICES 
END



--
--go 
--
--exec Proc_GetPoliciesToGeneratePremiumNotice
--
--rollback tran









GO

