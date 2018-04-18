IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMinimumDueForLastNotice]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMinimumDueForLastNotice]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran 
--drop proc   dbo.Proc_GetMinimumDueForLastNotice
--go 
/*
----------------------------------------------------------------------------------------------------------------------------------
Proc Name          : dbo.Proc_GetMinimumDueForLastNotice    
Created by         : PRAVEEN KASANA      
Date               :       
Purpose            : Fetches Minimum Due will be greater of "Due amount of last cancellation or Premium notice" or Minimum Due '      
Revison History    :      
Used In            :   Wolverine    

Changed	By		   : Ravindra
Changed	On		   : 03-11-2008
Purpose            : Fetches Minimum Due will be due amount due as on due date of Last Notice or gurrent date (whicever is greater)
---------------------------------------------------------------------------------------------------------------------------------      
Date     Review By          Comments      
------   ------------       -------------------------*/     
--drop proc   dbo.Proc_GetMinimumDueForLastNotice
CREATE  PROCEDURE dbo.Proc_GetMinimumDueForLastNotice
(      
	@CUSTOMER_ID   Int ,    
	@POLICY_ID    Int ,    
	@POLICY_VERSION_ID  SmallInt
 )      
AS      
BEGIN      
	DECLARE		@LAST_NOTICE_DUE_DATE			Datetime , 
				@AS_ON							Datetime

	SELECT TOP 1 @LAST_NOTICE_DUE_DATE = DUE_DATE 
	FROM ACT_CUSTOMER_BALANCE_INFORMATION WITH(NOLOCK)
	WHERE CUSTOMER_ID=@CUSTOMER_ID 
	AND POLICY_ID=@POLICY_ID 
	AND NOTICE_TYPE IN ('CANC_NOTICE','PREM_NOTICE')
	ORDER BY CREATED_DATE DESC

	IF ( CAST(CONVERT(VARCHAR,@LAST_NOTICE_DUE_DATE,101) AS DATETIME) >  
			CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME)) 
	BEGIN 
		SET @AS_ON = CAST(CONVERT(VARCHAR,@LAST_NOTICE_DUE_DATE,101) AS DATETIME)
	END
	ELSE
	BEGIN
		SET @AS_ON = CAST(CONVERT(VARCHAR,GETDATE(),101) AS DATETIME)
	END

	--Ravindra(12-29-2008): If no premium due as on date display minimum due as 0 , 
	--no need to show installment fees as minimum due (ref iTrack 5196) .

	--Exec Proc_GetTotalAndMinimumDue @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID , @AS_ON

	CREATE TABLE #TEMP_DATA
	(
		MINIMUM_DUE			decimal(18,2),
		TOTAL_DUE			decimal(18,2),
		AGENCY_ID			INT,
		AGENCYCODE			VARCHAR(50),
		PREM				Decimal(18,2) , 
		FEE					Decimal(18,2),            
		FIRST_INS_FEE		Decimal(18,2), 
		TOTAL_PREMIUM_DUE	Decimal(18,2)
	)
	INSERT INTO #TEMP_DATA
	Exec Proc_GetTotalAndMinimumDue @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID , @AS_ON

	SELECT CASE ISNULL(PREM,0) WHEN 0 THEN 0 ELSE MINIMUM_DUE END AS MINIMUM_DUE ,
		TOTAL_DUE	,AGENCY_ID, AGENCYCODE	,PREM ,	FEE	,FIRST_INS_FEE ,TOTAL_PREMIUM_DUE
	FROM #TEMP_DATA
	
	DROP TABLE #TEMP_DATA 

--		-- minimum due calculation  
--	DECLARE @MINIMUM_DUE DECIMAL(18,2)
--	DECLARE @MINIMUM_DUE_FOR_LAST_LOTICE DECIMAL(18,2)
--	DECLARE @FINAL_MINIMUM_DUE DECIMAL(18,2)
--
--	--GET MIN DUE from NOTICES
--	SELECT TOP 1 @MINIMUM_DUE_FOR_LAST_LOTICE = ISNULL(MIN_DUE,0)
--	FROM ACT_CUSTOMER_BALANCE_INFORMATION WITH(NOLOCK)
--	WHERE CUSTOMER_ID=@CUSTOMER_ID 
--	AND POLICY_ID=@POLICY_ID 
--	AND NOTICE_TYPE IN ('CANC_NOTICE','PREM_NOTICE')
--	ORDER BY CREATED_DATE DESC
--
--	--FETCH FORM Proc_GetTotalAndMinimumDue
--	CREATE TABLE #TEMP_DATA
--	(
--		MINIMUM_DUE decimal(18,2),
--		TOTAL_DUE decimal(18,2),
--		AGENCY_ID INT,
--		AGENCY_CODE VARCHAR(20),
--		PREM Decimal(18,2) , 
--		FEE Decimal(18,2),            
--		FIRST_INS_FEE Decimal(18,2)
--	)
--	INSERT INTO #TEMP_DATA
--	Exec Proc_GetTotalAndMinimumDue @CUSTOMER_ID,@POLICY_ID,@POLICY_VERSION_ID ,null
--
--	SELECT @MINIMUM_DUE = MINIMUM_DUE FROM #TEMP_DATA
--
--	IF(@MINIMUM_DUE_FOR_LAST_LOTICE IS NOT NULL)
--	BEGIN
--		
--		IF(@MINIMUM_DUE > @MINIMUM_DUE_FOR_LAST_LOTICE)
--		BEGIN 
--			SET @FINAL_MINIMUM_DUE = @MINIMUM_DUE
--		END
--		ELSE
--		BEGIN
--			SET @FINAL_MINIMUM_DUE = @MINIMUM_DUE_FOR_LAST_LOTICE
--		END
--	END
--	ELSE
--	BEGIN
--		SET @FINAL_MINIMUM_DUE = @MINIMUM_DUE
--	END	
--
--
--	SELECT @FINAL_MINIMUM_DUE AS MINIMUM_DUE ,TOTAL_DUE,AGENCY_ID,AGENCY_CODE AS AGENCYCODE FROM #TEMP_DATA
--
--	DROP TABLE #TEMP_DATA

END

--go 
--
--exec Proc_GetMinimumDueForLastNotice 1432, 62, 1
--
--rollback tran 
--




GO

