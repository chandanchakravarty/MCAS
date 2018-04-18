IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_DeletePoliciesFromPremiumNoticeSpool]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_DeletePoliciesFromPremiumNoticeSpool]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran 
--drop PROC dbo.Proc_DeletePoliciesFromPremiumNoticeSpool
--go 
/*----------------------------------------------------------                
Proc Name       :  dbo.Proc_DeletePoliciesFromPremiumNoticeSpool
Created by      :  Ravindra
Date            :  4-9-2008
Purpose         :  To delete unprocessed record from premium notice spool before adding new records
					if there are faliures in generating Notice in EOD recalculate Premium Notice 
					eligibility again
Revison History :                
Used In         :  Wolverine                
                
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
-- drop PROC dbo.Proc_DeletePoliciesFromPremiumNoticeSpool
CREATE PROC dbo.Proc_DeletePoliciesFromPremiumNoticeSpool
AS
BEGIN 

	CREATE TABLE #ACT_PREMIUM_NOTICE_SPOOL
	(
		IDEN_COL			Int Identity(1,1),
		SPOOL_ID			Int,
		REF_IDS			Varchar(500)
	)

	INSERT INTO #ACT_PREMIUM_NOTICE_SPOOL( SPOOL_ID , REF_IDS) 
	SELECT SPOOL_ID , SENT_AGAINST FROM ACT_PREMIUM_NOTICE_SPOOL
	WHERE ISNULL(PROCESSED,0) <> 1

	DECLARE	@IDEN_COL	Int,
			@SPOOL_ID	Int,
			@REF_IDS	Varchar(500)

	SET @IDEN_COL = 1

	WHILE(1=1)
	BEGIN 
		IF NOT EXISTS (	SELECT SPOOL_ID FROM #ACT_PREMIUM_NOTICE_SPOOL WHERE IDEN_COL = @IDEN_COL ) 
		BEGIN 
			BREAK
		END
		SELECT @SPOOL_ID  =  SPOOL_ID , @REF_IDS = REF_IDS FROM #ACT_PREMIUM_NOTICE_SPOOL WHERE IDEN_COL = @IDEN_COL 

		DECLARE @QUERY VARCHAR(600)
		SET @QUERY = 'UPDATE ACT_CUSTOMER_OPEN_ITEMS SET NOTICE_SEND = ''N'' 
		WHERE IDEN_ROW_ID IN (' +  @REF_IDS + ')'
		
		EXEC (@QUERY)	
	
		DELETE FROM ACT_PREMIUM_NOTICE_SPOOL WHERE SPOOL_ID = @SPOOL_ID
	
		SET @IDEN_COL = @IDEN_COL + 1 
	END
	
	
	DROP TABLE #ACT_PREMIUM_NOTICE_SPOOL
END


GO

