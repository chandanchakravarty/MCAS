IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ProcessEarnedPremiumReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ProcessEarnedPremiumReport]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran 
--drop proc Proc_ProcessEarnedPremiumReport 
--go 
/*----------------------------------------------------------                
 Proc Name       : Dbo.Proc_ProcessEarnedPremiumReport
 Created by      : Ravindra 
 Date            : 06-25-2007
 Purpose         : Process Earned premium report for particular month year
 Modified by     : Arun 
 Date            : 01-09-2008 
 Revison History :                
 Used In     	 : Wolverine   (Reporting)

 Modified by     : Ravindra
 Date            : 04-29-2008 
 Purpose		 : Logic correction
------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/    
-- drop proc Dbo.Proc_ProcessEarnedPremiumReport            
CREATE PROC [dbo].[Proc_ProcessEarnedPremiumReport]
(                
	@MONTH 			SMALLINT,
	@YEAR  			INT
)                
AS               
BEGIN 
--Delete records from DB if processed interim for this month 
DELETE FROM ACT_EARNED_PREMIUM WHERE MONTH_NUMBER = @MONTH AND YEAR_NUMBER = @YEAR 
	
DECLARE @ENDORSEMENT_PROCESS 	INT,
		@RESCINDPROCESS 	INT ,
		@CANCELLATIONPROCESS  	INT,
		@FIRST_DAY_OF_NEXT_MONTH		DATETIME,
		@LAST_DAY_OF_PREVIOUS_MONTH Datetime , 
		@LAST_DAY_OF_THIS_MONTH Datetime 

SET @ENDORSEMENT_PROCESS  = 14
SET @RESCINDPROCESS		  = 29	
SET @CANCELLATIONPROCESS  = 12	

IF @MONTH = 12
   SET @FIRST_DAY_OF_NEXT_MONTH= CONVERT(DATETIME,'1/1/' + convert(varchar,@YEAR+1))
ELSE
   SET @FIRST_DAY_OF_NEXT_MONTH=CONVERT(DATETIME,convert(varchar,@MONTH+1) + '/' + '1' + '/' + convert(varchar,@YEAR))

SET @LAST_DAY_OF_PREVIOUS_MONTH= DATEADD(DD,-1,CONVERT(DATETIME,convert(varchar,@MONTH) + '/' + '1' + '/' + convert(varchar,@YEAR))) 

SET @LAST_DAY_OF_THIS_MONTH  = DATEADD(DD, -1 , @FIRST_DAY_OF_NEXT_MONTH ) 


CREATE TABLE #TMP_EFFECTIVE_POLICIES 
( 
   IDEN_ROW_ID Int Identity(1,1),	
   POLICY_NUMBER Varchar(20),
   CUSTOMER_ID INt,
   POLICY_ID Int,
   CURRENT_TERM Int,
   AGENCY_ID Int,	
   STATE_ID Int ,
   POLICY_EFFECTIVE_DATE DateTime,
   POLICY_EXPIRATION_DATE DateTime,
   TRAN_EFFECTIVE_DATE Datetime,	
   PROCESS_ID Int,
   POLICY_TERM Int,
   MONTH_ELAPSED Int,
   UNEARNED_FACTOR_END Decimal(18,6),	
   UNEARNED_FACTOR_BEG Decimal(18,6),		
   INFORCE_PREMIUM Decimal(18,6),
   BEGINNING_UNEARNED Decimal(18,6),
   WRITTEN_PREMIUM 	Decimal(18,6),
   ENDING_UNEARNED Decimal(18,2)
)

CREATE TABLE #ACT_EARNED_PREMIUM_TEMP 
( 
   IDEN_ROW_ID Int Identity(1,1),	
   POLICY_NUMBER Varchar(20),
   CUSTOMER_ID INt,
   POLICY_ID Int,
   CURRENT_TERM Int,
   AGENCY_ID Int,	
   STATE_ID Int ,
   POLICY_EFFECTIVE_DATE DateTime,
   POLICY_EXPIRATION_DATE DateTime,
   TRAN_EFFECTIVE_DATE Datetime,	
   PROCESS_ID Int,
   POLICY_TERM Int,
   MONTH_ELAPSED Int,
   UNEARNED_FACTOR_END Decimal(18,6),	
   UNEARNED_FACTOR_BEG Decimal(18,6),		
   INFORCE_PREMIUM Decimal(18,6),
   BEGINNING_UNEARNED Decimal(18,6),
   WRITTEN_PREMIUM 	Decimal(18,6),
   ENDING_UNEARNED Decimal(18,2),
   COVERAGEID NVARCHAR(20),
   EARNED_PREMIUM As ISNULL(BEGINNING_UNEARNED,0) + ISNULL(WRITTEN_PREMIUM,0) - ISNULL(ENDING_UNEARNED,0),
   RISK_ID	Int , 
   RISK_TYPE Varchar(20),
   VERSION_FOR_RISK Int
)
   
INSERT INTO #TMP_EFFECTIVE_POLICIES ( POLICY_NUMBER,   
	CUSTOMER_ID , POLICY_ID , CURRENT_TERM ,
	POLICY_EFFECTIVE_DATE , POLICY_EXPIRATION_DATE , POLICY_TERM ,
	MONTH_ELAPSED , UNEARNED_FACTOR_END , UNEARNED_FACTOR_BEG ,
	TRAN_EFFECTIVE_DATE, AGENCY_ID , STATE_ID
	)
SELECT DISTINCT CPL.POLICY_NUMBER ,
	CPL.CUSTOMER_ID , CPL.POLICY_ID  , CPL.CURRENT_TERM,
        CPL.APP_EFFECTIVE_DATE, CPL.APP_EXPIRATION_DATE, CPL.APP_TERMS,
	-- ((Y2- Y1) * 12) + M2 - M1 + 1
	((@YEAR - DATEPART(YYYY,APP_EFFECTIVE_DATE)) * 12 )  +  
	 @MONTH  - DATEPART(MM,APP_EFFECTIVE_DATE)  + 1  ,
	-- Factor for ending unearned premium 
	CASE WHEN DATEPART(YYYY,APP_EXPIRATION_DATE) = @YEAR AND 
		 DATEPART(MM,APP_EXPIRATION_DATE)  = @MONTH THEN 0
	ELSE 
	(( CAST((CPL.APP_TERMS * 2 ) AS DECIMAL) ) - 
	(((((@YEAR - DATEPART(YYYY,APP_EFFECTIVE_DATE)) * 12 )  +  
	 @MONTH  - DATEPART(MM,APP_EFFECTIVE_DATE)  + 1 ) * 2) - 1) )  / (CPL.APP_TERMS * 2 ) 
	END ,
	0, -- Begning earned facor will be fetched form last saved report 
CPL.APP_EFFECTIVE_DATE , CPL.AGENCY_ID , CPL.STATE_ID
FROM POL_CUSTOMER_POLICY_LIST CPL WITH(NOLOCK)
INNER JOIN POL_POLICY_PROCESS PPP WITH(NOLOCK)
ON CPL.CUSTOMER_ID = PPP.CUSTOMER_ID 
AND CPL.POLICY_ID = PPP.POLICY_ID 
AND CPL.POLICY_VERSION_ID = PPP.NEW_POLICY_VERSION_ID 
AND PPP.PROCESS_STATUS = 'COMPLETE'
WHERE CPL.APP_EFFECTIVE_DATE < @FIRST_DAY_OF_NEXT_MONTH 
	  AND CPL.APP_EXPIRATION_DATE >= @LAST_DAY_OF_PREVIOUS_MONTH
--Ravindra(11-18-2008): Excludes Policies from AS400 which are either not renewed or 
--revertback performed on renewals done
AND CPL.POLICY_VERSION_ID >= 
	( 
		SELECT MIN(PPPIN.NEW_POLICY_VERSION_ID) FROM POL_POLICY_PROCESS PPPIN
		WHERE PPPIN.CUSTOMER_ID = CPL.CUSTOMER_ID 
		AND PPPIN.POLICY_ID		= CPL.POLICY_ID 
		AND PROCESS_ID	IN (25,18)  
		AND PROCESS_STATUS		='COMPLETE'-- MIN OF NBS OR RENEWAL
		AND ISNULL(REVERT_BACK,'N') = 'N'
	) 

ORDER BY CPL.CUSTOMER_ID , CPL.POLICY_ID , CPL.CURRENT_TERM


CREATE TABLE #POSTED_PREMIUM_TEMP
(

	IDENT_COL		Int IDENTITY(1,1) NOT NULL ,
	COVERAGE_ID		Int ,
	WRITTEN_PREMIUM Decimal(18,6) , 
	INFORCE_PREMIUM Decimal(18,6) ,
	VERSION_ID		Int ,
	RISK_ID				Int ,
	RISK_TYPE			Varchar(20),
	VERSION_FOR_RISK	Int
)

CREATE TABLE #POSTED_PREMIUM
(	
	IDENT_COL		Int IDENTITY(1,1) NOT NULL ,
	COVERAGE_ID		Int ,
	WRITTEN_PREMIUM Decimal(18,6) , 
	INFORCE_PREMIUM Decimal(18,6) ,
	ENDING_UNEARNED Decimal(18,2) ,
	BEGINNING_UNEARNED	Decimal(18,2) ,
	RISK_ID				Int ,
	RISK_TYPE			Varchar(20),
	VERSION_FOR_RISK	Int
)

DECLARE @IDEN_ROW_ID Int,
		@CUSTOMER_ID Int,
		@POLICY_ID Int,
		@CURRENT_TERM Int,
		@POLICY_EFFECTIVE_DATE DateTime,
  		@POLICY_EXPIRATION_DATE DateTime,
		@TOTAL_GROSS_PREMIUM 	Decimal(18,2) , 
		@WRITTEN_PREMIUM Decimal(18,2),	
		@POSTED_PREMIUM_XML VARCHAR(4000),
		@UNEARNED_FACTOR_END DECIMAL(18,6),
		@UNEARNED_FACTOR_BEG DECIMAL(18,6),
		@NEW_TRAN_EFFECTIVE_DATE DateTime,
		@POLICY_NUMBER Varchar(20),
		@AGENCY_ID Int,
		@STATE_ID Int,
		@TRAN_EFFECTIVE_DATE DateTime,
		@PROCESS_ID Int,
		@POLICY_TERM Int,
		@MONTH_ELAPSED Int,
		@BEGINNING_UNEARNED Decimal(18,6),
		@ENDING_UNEARNED Decimal(18,6)



SET @IDEN_ROW_ID = 1 

WHILE (1 =1 ) 
BEGIN 	

	IF NOT EXISTS (SELECT IDEN_ROW_ID FROM #TMP_EFFECTIVE_POLICIES  WHERE IDEN_ROW_ID = @IDEN_ROW_ID)
	BEGIN 
		BREAK
	END 
	
	SET @TOTAL_GROSS_PREMIUM  = 0 
	SET @WRITTEN_PREMIUM = 0
	SET @UNEARNED_FACTOR_BEG  = NULL

	SELECT  @CUSTOMER_ID = CUSTOMER_ID ,
			@POLICY_ID   = POLICY_ID ,
			@CURRENT_TERM = CURRENT_TERM ,
			@POLICY_EFFECTIVE_DATE  = POLICY_EFFECTIVE_DATE,
			@POLICY_EXPIRATION_DATE  =POLICY_EXPIRATION_DATE ,
			@UNEARNED_FACTOR_END  = UNEARNED_FACTOR_END,
			@POLICY_NUMBER=POLICY_NUMBER,
			@AGENCY_ID=AGENCY_ID,
			@STATE_ID=STATE_ID,
			@TRAN_EFFECTIVE_DATE=TRAN_EFFECTIVE_DATE,
			@PROCESS_ID=PROCESS_ID,
			@POLICY_TERM=POLICY_TERM,
			@MONTH_ELAPSED =MONTH_ELAPSED,
			@UNEARNED_FACTOR_END =UNEARNED_FACTOR_END,	
			@UNEARNED_FACTOR_BEG 	=UNEARNED_FACTOR_BEG,	
			@BEGINNING_UNEARNED =BEGINNING_UNEARNED,
			@WRITTEN_PREMIUM =WRITTEN_PREMIUM,
			@ENDING_UNEARNED=ENDING_UNEARNED
			FROM #TMP_EFFECTIVE_POLICIES 
			WHERE IDEN_ROW_ID = @IDEN_ROW_ID


	IF NOT EXISTS 
				( 
					SELECT PPP.CUSTOMER_ID FROM POL_POLICY_PROCESS PPP WITH(NOLOCK) WHERE PPP.CUSTOMER_ID = @CUSTOMER_ID 
					AND PPP.POLICY_ID = @POLICY_ID AND PPP.NEW_POLICY_VERSION_ID IN 
							( 
								SELECT CPL.POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST CPL 
								WHERE CPL.CUSTOMER_ID = @CUSTOMER_ID AND CPL.POLICY_ID = @POLICY_ID 
								AND CPL.CURRENT_TERM = @CURRENT_TERM 
							) 
					AND PPP.PROCESS_ID	IN (25,18,32)  
					AND PPP.PROCESS_STATUS		='COMPLETE'
					AND ISNULL(PPP.REVERT_BACK,'N') = 'N'
				) 
	BEGIN 
		SET @IDEN_ROW_ID = @IDEN_ROW_ID + 1
		CONTINUE
	END

	SET @NEW_TRAN_EFFECTIVE_DATE = @POLICY_EFFECTIVE_DATE

	TRUNCATE TABLE #POSTED_PREMIUM 
	TRUNCATE TABLE #POSTED_PREMIUM_TEMP 

	INSERT INTO #POSTED_PREMIUM_TEMP
	(	
		COVERAGE_ID , WRITTEN_PREMIUM ,VERSION_ID ,
		RISK_ID	, RISK_TYPE , VERSION_FOR_RISK	
	)
	SELECT 	TEMP.COVERAGE_ID , CASE WHEN ISNUMERIC(TEMP.WRITTEN_PREM) = 0 THEN 0 ELSE CAST(TEMP.WRITTEN_PREM AS DECIMAL(18,6)) END  ,
			TEMP.VERSION_ID 	, 
			TEMP.RISK_ID , TEMP.RISK_TYPE , TEMP.ACTL_POLICY_VERSION_ID
	FROM 
	(
		SELECT SPLIT.POLICY_VERSION_ID AS VERSION_ID, PPP.PROCESS_ID , CPL.POL_VER_EFFECTIVE_DATE , PPP.COMPLETED_DATETIME , 
		DET.COMP_EXT AS COVERAGE_ID , 
		
		CASE WHEN DET.EPR_MONTH = @MONTH AND DET.EPR_YEAR = @YEAR  THEN DET.WRITTEN_PREM  
		ELSE '0' END AS WRITTEN_PREM ,
		SPLIT.RISK_ID , SPLIT.RISK_TYPE , SPLIT.ACTL_POLICY_VERSION_ID
		FROM CLT_PREMIUM_SPLIT SPLIT  WITH(NOLOCK)
		INNER JOIN CLT_PREMIUM_SPLIT_DETAILS DET   WITH(NOLOCK) 
		ON SPLIT.UNIQUE_ID = DET.SPLIT_UNIQUE_ID 
		INNER JOIN POL_POLICY_PROCESS PPP  WITH(NOLOCK)
		ON SPLIT.CUSTOMER_ID = PPP.CUSTOMER_ID 
		AND SPLIT.POLICY_ID = PPP.POLICY_ID 
		AND SPLIT.POLICY_VERSION_ID = PPP.NEW_POLICY_VERSION_ID 
		AND SPLIT.PROCESS_TYPE = PPP.PROCESS_ID 
		AND PPP.PROCESS_STATUS = 'COMPLETE' 
		INNER JOIN POL_CUSTOMER_POLICY_LIST CPL  WITH(NOLOCK)
		ON SPLIT.CUSTOMER_ID = CPL.CUSTOMER_ID 
		AND SPLIT.POLICY_ID = CPL.POLICY_ID 
		AND SPLIT.POLICY_VERSION_ID = CPL.POLICY_VERSION_ID
		WHERE SPLIT.CUSTOMER_ID = @CUSTOMER_ID AND SPLIT.POLICY_ID = @POLICY_ID 
		AND SPLIT.POLICY_VERSION_ID 
		IN ( SELECT SUB.POLICY_VERSION_ID FROM POL_CUSTOMER_POLICY_LIST SUB  WITH(NOLOCK)
									WHERE SUB.CUSTOMER_ID  = @CUSTOMER_ID AND SUB.POLICY_ID = @POLICY_ID AND SUB.CURRENT_TERM = @CURRENT_TERM ) 

		AND ISNULL(DET.COMP_EXT ,'') <> '' 
		--AND DATEDIFF(DD, @FIRST_DAY_OF_NEXT_MONTH , PPP.COMPLETED_DATETIME )  < 0 
--		AND DET.EPR_MONTH <= @MONTH	
--		AND DET.EPR_YEAR  <= @YEAR 
		AND
		CONVERT(DATETIME,convert(varchar,DET.EPR_MONTH ) + '/' + '1' + '/' + convert(varchar,DET.EPR_YEAR ))
		<=
		CONVERT(DATETIME,convert(varchar,@MONTH) + '/' + '1' + '/' + convert(varchar,@YEAR ))
		AND SPLIT.PROCESS_TYPE <>'9' -- CUP
	)AS TEMP
	

	--select * from #POSTED_PREMIUM_TEMP 

	-- If policy Cancelled make Enforce 0 else enforce will be enforce of last policy version considered for calculation
	DECLARE @MAX_POLICY_VERSION Int,
			@IDENT_COL			Int,
			@COVERAGE_ID		Int,
			@INFORCE_PREMIUM	Decimal(18,6) ,
			@RISK_ID			Int, 
			@RISK_TYPE			Varchar(20), 
			@VERSION_FOR_RISK   Int,
			@MAX_VERSION_FOR_RISK	Int

	SELECT @MAX_POLICY_VERSION = MAX(VERSION_ID) FROM #POSTED_PREMIUM_TEMP 


	DELETE FROM #POSTED_PREMIUM_TEMP  WHERE VERSION_ID = VERSION_FOR_RISK 
	AND VERSION_ID <> @MAX_POLICY_VERSION AND ISNULL(WRITTEN_PREMIUM ,0) = 0

	INSERT INTO #POSTED_PREMIUM
	(	
		COVERAGE_ID , WRITTEN_PREMIUM , RISK_ID , RISK_TYPE , VERSION_FOR_RISK
	)
	SELECT COVERAGE_ID , SUM(ISNULL(WRITTEN_PREMIUM ,0) ) , RISK_ID , RISK_TYPE , VERSION_FOR_RISK
	FROM #POSTED_PREMIUM_TEMP
	GROUP BY COVERAGE_ID , RISK_ID , RISK_TYPE , VERSION_FOR_RISK

	SET @IDENT_COL = 1 
	WHILE ( 1 = 1) 
	BEGIN 
		IF NOT EXISTS (SELECT  IDENT_COL FROM #POSTED_PREMIUM WHERE IDENT_COL = @IDENT_COL ) 
			BREAK

		SELECT @COVERAGE_ID = COVERAGE_ID ,
		@RISK_ID			= RISK_ID, 
		@RISK_TYPE			= RISK_TYPE, 
		@VERSION_FOR_RISK   = VERSION_FOR_RISK ,
		@UNEARNED_FACTOR_BEG  = null,
		@BEGINNING_UNEARNED	= null,
		@MAX_VERSION_FOR_RISK = NULL 
		FROM #POSTED_PREMIUM WHERE IDENT_COL  = @IDENT_COL
	
		-- If policy is cancelled make inforce Zero
	
		IF (
				( 
				SELECT DATEDIFF(DD,EFFECTIVE_DATETIME,@FIRST_DAY_OF_NEXT_MONTH)  
				FROM POL_POLICY_PROCESS WHERE CUSTOMER_ID = @CUSTOMER_ID 
				AND POLICY_ID = @POLICY_ID AND NEW_POLICY_VERSION_ID = @MAX_POLICY_VERSION 
				AND PROCESS_ID IN (@RESCINDPROCESS  , @CANCELLATIONPROCESS  ) 
				AND PROCESS_STATUS = 'COMPLETE' ) > 0 
			)
		BEGIN 
			SET @INFORCE_PREMIUM = 0 
		END
		ELSE
		BEGIN
			-- Ravindra (07-17-2008): Policy expiring day before last day of month will have inforce zero
			IF ( 
					(
					SELECT APP_EXPIRATION_DATE FROM POL_CUSTOMER_POLICY_LIST 
					WHERE CUSTOMER_ID = @CUSTOMER_ID 
					AND POLICY_ID = @POLICY_ID AND POLICY_VERSION_ID = @MAX_POLICY_VERSION
					) < @LAST_DAY_OF_THIS_MONTH
				) 
			BEGIN 
				SET @INFORCE_PREMIUM = 0 
			END
			ELSE
			BEGIN 
				SELECT @INFORCE_PREMIUM = SUM(CASE WHEN ISNUMERIC(DET.COMP_ACT_PREMIUM) = 0 THEN 0 ELSE CAST(DET.COMP_ACT_PREMIUM AS DECIMAL(18,6))  END)
				FROM CLT_PREMIUM_SPLIT SPLIT  WITH(NOLOCK)
				INNER JOIN CLT_PREMIUM_SPLIT_DETAILS DET   WITH(NOLOCK)
				ON SPLIT.UNIQUE_ID = DET.SPLIT_UNIQUE_ID 
				WHERE SPLIT.CUSTOMER_ID = @CUSTOMER_ID AND SPLIT.POLICY_ID = @POLICY_ID 
				AND SPLIT.POLICY_VERSION_ID = @MAX_POLICY_VERSION 
				AND ISNULL(DET.COMP_EXT ,'') = CONVERT(VARCHAR , @COVERAGE_ID ) 
				AND ISNULL(IS_PART_OF_REVERT,'') <> 'Y'
				AND SPLIT.RISK_ID			= @RISK_ID 
				AND	SPLIT.RISK_TYPE			= @RISK_TYPE
			END
		END

		-- Fetch prevoius month unearned preium from saved records

		DECLARE @LAST_MONTH_MAX_VERSION Int
		
		SELECT @LAST_MONTH_MAX_VERSION  = MAX(VERSION_FOR_RISK) 
		FROM ACT_EARNED_PREMIUM 
		WHERE MONTH_NUMBER = DATEPART(MM,@LAST_DAY_OF_PREVIOUS_MONTH) 
		AND YEAR_NUMBER = DATEPART(YY,@LAST_DAY_OF_PREVIOUS_MONTH)  
		AND CUSTOMER_ID = @CUSTOMER_ID 
		AND POLICY_ID   = @POLICY_ID  
		AND CURRENT_TERM = @CURRENT_TERM 
		AND COVERAGEID	= @COVERAGE_ID  
		AND RISK_ID		= @RISK_ID
		AND RISK_TYPE	= @RISK_TYPE


		--RAvindra(08-14-09): In case when Risk was deleted in Endorsement there will be multiple versions 
		-- MAx version would be the version in which risk is deleted having a reference to this version will
		-- create issue while linking EPR with Risk Level tables. In such cases version in which risk actually 
		-- exists should be reffered.
--		
--		SELECT @MAX_VERSION_FOR_RISK = MAX(VERSION_FOR_RISK) FROM #POSTED_PREMIUM
--		WHERE COVERAGE_ID	= @COVERAGE_ID  
--		AND RISK_ID		= @RISK_ID
--		AND RISK_TYPE	= @RISK_TYPE


		IF EXISTS (		
					SELECT IDENT_COL FROM #POSTED_PREMIUM	WHERE COVERAGE_ID	= @COVERAGE_ID  
					AND RISK_ID		= @RISK_ID	AND RISK_TYPE	= @RISK_TYPE	AND WRITTEN_PREMIUM <> 0 
					)
		BEGIN
			SELECT @MAX_VERSION_FOR_RISK = MAX(VERSION_FOR_RISK) FROM #POSTED_PREMIUM
			WHERE COVERAGE_ID	= @COVERAGE_ID  
			AND RISK_ID		= @RISK_ID
			AND RISK_TYPE	= @RISK_TYPE
			AND WRITTEN_PREMIUM <> 0 
		END
		ELSE
		BEGIN 
			SELECT @MAX_VERSION_FOR_RISK = MAX(VERSION_FOR_RISK) FROM #POSTED_PREMIUM
			WHERE COVERAGE_ID	= @COVERAGE_ID  
			AND RISK_ID		= @RISK_ID
			AND RISK_TYPE	= @RISK_TYPE
		END

		SELECT @UNEARNED_FACTOR_BEG  = UNEARNED_FACTOR_END,
			   @BEGINNING_UNEARNED	= 	ENDING_UNEARNED  	
		FROM ACT_EARNED_PREMIUM 
		WHERE MONTH_NUMBER = DATEPART(MM,@LAST_DAY_OF_PREVIOUS_MONTH) 
		AND YEAR_NUMBER = DATEPART(YY,@LAST_DAY_OF_PREVIOUS_MONTH)  
		AND CUSTOMER_ID = @CUSTOMER_ID 
		AND POLICY_ID   = @POLICY_ID  
		AND CURRENT_TERM = @CURRENT_TERM 
		AND COVERAGEID	= @COVERAGE_ID  
		AND RISK_ID		= @RISK_ID
		AND RISK_TYPE	= @RISK_TYPE
		AND VERSION_FOR_RISK = @LAST_MONTH_MAX_VERSION

		IF @UNEARNED_FACTOR_BEG  IS NULL 
			SET @UNEARNED_FACTOR_BEG  = 0 
		IF @BEGINNING_UNEARNED IS NULL
			SET @BEGINNING_UNEARNED=0

		UPDATE #POSTED_PREMIUM SET INFORCE_PREMIUM = ABS(ISNULL(@INFORCE_PREMIUM,0)),
					BEGINNING_UNEARNED = @BEGINNING_UNEARNED
		WHERE COVERAGE_ID = @COVERAGE_ID  
		AND RISK_ID		= @RISK_ID
		AND RISK_TYPE	= @RISK_TYPE
		--AND VERSION_FOR_RISK = @MAX_POLICY_VERSION	
		AND VERSION_FOR_RISK = @MAX_VERSION_FOR_RISK

		SET  @IDENT_COL = @IDENT_COL + 1 
	END 
	
	INSERT INTO #ACT_EARNED_PREMIUM_TEMP(POLICY_NUMBER,CUSTOMER_ID,POLICY_ID,CURRENT_TERM,
				POLICY_EFFECTIVE_DATE,POLICY_EXPIRATION_DATE,POLICY_TERM,MONTH_ELAPSED,UNEARNED_FACTOR_END,
				UNEARNED_FACTOR_BEG,TRAN_EFFECTIVE_DATE,AGENCY_ID,STATE_ID,PROCESS_ID,
				INFORCE_PREMIUM,
				BEGINNING_UNEARNED,
				WRITTEN_PREMIUM,
				ENDING_UNEARNED,
				COVERAGEID , RISK_ID , RISK_TYPE , VERSION_FOR_RISK )
	SELECT @POLICY_NUMBER,@CUSTOMER_ID,@POLICY_ID,@CURRENT_TERM,
		   @POLICY_EFFECTIVE_DATE,@POLICY_EXPIRATION_DATE,@POLICY_TERM,@MONTH_ELAPSED,@UNEARNED_FACTOR_END,
		   @UNEARNED_FACTOR_BEG,@TRAN_EFFECTIVE_DATE,@AGENCY_ID,@STATE_ID,@PROCESS_ID,
		   INFORCE_PREMIUM ,
		   BEGINNING_UNEARNED ,	
		   WRITTEN_PREMIUM ,
		   INFORCE_PREMIUM  * @UNEARNED_FACTOR_END, 	
		   COVERAGE_ID , RISK_ID , RISK_TYPE , VERSION_FOR_RISK 
	FROM #POSTED_PREMIUM

	SET @IDEN_ROW_ID = @IDEN_ROW_ID + 1

END
 

INSERT INTO ACT_EARNED_PREMIUM(
		MONTH_NUMBER,YEAR_NUMBER,POLICY_NUMBER,CUSTOMER_ID,POLICY_ID,CURRENT_TERM,AGENCY_ID,
		STATE_ID,POLICY_EFFECTIVE_DATE,POLICY_EXPIRATION_DATE,TRAN_EFFECTIVE_DATE,PROCESS_ID,
		POLICY_TERM,MONTH_ELAPSED,UNEARNED_FACTOR_END,UNEARNED_FACTOR_BEG,INFORCE_PREMIUM,
		BEGINNING_UNEARNED,WRITTEN_PREMIUM,ENDING_UNEARNED,EARNED_PREMIUM,COVERAGEID , 
		RISK_ID , RISK_TYPE , VERSION_FOR_RISK 
		)
SELECT 
		@MONTH , @YEAR ,POLICY_NUMBER,CUSTOMER_ID,POLICY_ID,CURRENT_TERM,AGENCY_ID,
		STATE_ID,POLICY_EFFECTIVE_DATE,POLICY_EXPIRATION_DATE,TRAN_EFFECTIVE_DATE,PROCESS_ID,
		POLICY_TERM,MONTH_ELAPSED,UNEARNED_FACTOR_END,UNEARNED_FACTOR_BEG,INFORCE_PREMIUM,
		BEGINNING_UNEARNED,WRITTEN_PREMIUM,ENDING_UNEARNED,EARNED_PREMIUM,COVERAGEID , 
		RISK_ID , RISK_TYPE , VERSION_FOR_RISK 
		FROM #ACT_EARNED_PREMIUM_TEMP 
		WHERE NOT (INFORCE_PREMIUM = 0 AND WRITTEN_PREMIUM = 0 AND EARNED_PREMIUM = 0 ) 

DROP TABLE #TMP_EFFECTIVE_POLICIES
DROP TABLE #ACT_EARNED_PREMIUM_TEMP 
DROP TABLE #POSTED_PREMIUM
DROP TABLE #POSTED_PREMIUM_TEMP 

END


--
--go 
--
--
--exec Proc_ProcessEarnedPremiumReport 7 , 2008
--
--SELECT M.COV_DES,E.* FROM ACT_EARNED_PREMIUM E
--LEFT JOIN MNT_COVERAGE M 
--ON E.COVERAGEID = M.COV_ID
--WHERE POLICY_NUMBER IN ( 'W6775400' ) 
--order by E.MONTH_NUMBER
--
--rollback tran  
--




GO

