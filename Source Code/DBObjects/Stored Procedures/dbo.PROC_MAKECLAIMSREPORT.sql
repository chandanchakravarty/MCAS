IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_MAKECLAIMSREPORT]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_MAKECLAIMSREPORT]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN
--DROP PROC [DBO].[PROC_MAKECLAIMSREPORT]
--GO 

/*----------------------------------------------------------                                                                          
Proc Name       : dbo.PROC_MAKECLAIMSREPORT                                         
Created by      : Shikha Dixit                                                                     
Date            : 16/07/2009                                                                          
Purpose         : Generates sum of Claims amount :- state, lob, month, year wise                      
Revewed by      :                                                                          
Revison History :                                                                          
Used In         : Wolverine                                                                          
------------------------------------------------------------                                                                          
Date     Review By          Comments                                                                          
------   ------------       -------------------------*/        
--DROP PROC [dbo].[PROC_MAKECLAIMSREPORT]
CREATE PROC [dbo].[PROC_MAKECLAIMSREPORT]
(                                        
	@MONTH int,
	@YEAR int,
	@STATE_ID varchar(50) = NULL
)                      
AS                                                                          
BEGIN
--	begin tran

CREATE TABLE #MOT_CLAIM_DETAIL
(
	LOB						VARCHAR(10),
	STATE_ID				INT,
	COVERAGE_ID				INT,
	AMOUNT					DECIMAL(18,2),
	DETAIL_TYPE_DESCRIPTION	VARCHAR(80),
	ACTV_MONTH				INT,
	ACTV_YEAR				INT,
	[LOSS TYPE]				INT
)

INSERT INTO #MOT_CLAIM_DETAIL 
SELECT LOB,STATE_ID,
CASE WHEN IS_COLLISION = 'YES' AND COVERAGE_ID IN (1023,1024) THEN 8000
WHEN IS_COLLISION = 'NO' AND COVERAGE_ID IN (1023,1024) THEN 8001
ELSE COVERAGE_ID END AS COVERAGE_ID,
AMOUNT,DETAIL_TYPE_DESCRIPTION,ACTV_MONTH,ACTV_YEAR,[LOSS TYPE]
FROM VW_MakeClaimsReport_MOT
WHERE STATE_ID = @STATE_ID AND ACTV_MONTH <= @MONTH AND ACTV_YEAR = @YEAR

--SELECT * FROM #MOT_CLAIM_DETAIL

	CREATE TABLE #TEMP_COVERAGE_DETAILS
	(
		COV_ID		INT,
		COV_CODE	VARCHAR(50),
		COV_DES		VARCHAR(100),
		PERCENTAGE	INT
	)
	
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (126,'CSL','Single Limits Liability (CSL)',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (127,'BI','Bodily Injury Liability (Split Limit)',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (128,'PD','Property Damage Liability',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (131,'UM/BI','Uninsured Motorists (CSL)',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (132,'UM/BI','Uninsured Motorists (BI Split Limit)',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (133,'UIM','Underinsured Motorists (CSL) (M-16)',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (199,'UM/PD','Uninsured Motorists (PD)',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (200,'COLL11','Collision',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (201,'COMP11','Other Than Collision (Comprehensive)',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (202,'RS','Road Service',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (203,'PD','Helmet & Riding Apparel Coverage (M-15)',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (204,'COMP','Motorcycle Trailer - M-49  Other than Collision',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (206,'CSL','Single Limits Liability (CSL)',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (207,'BI','Bodily Injury Liability (Split Limit)',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (208,'PD','Property Damage Liability',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (211,'UM/BI','Uninsured Motorists (CSL)',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (212,'UM/BI','Uninsured Motorists (BI Split Limit)',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (214,'UIM','Underinsured Motorists (BI Split Limit) (M-16)',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (216,'COLL22','Collision',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (217,'COMP22','Other Than Collision (Comprehensive)',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (218,'RS','Road Service',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (219,'PD','Helmet & Riding Apparel Coverage (M-15)',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (220,'COMP','Motorcycle Trailer - M-49  Other than Collision',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (769,'MP','Medical Payments -1st Party',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (770,'MP','Medical Payments',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (843,'MP',' Medical Payment - Option 2',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1021,'E17','Uninsured Motorist PD (M-17)',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1022,'UM/BI','Notice of Option to Reject or Modify A-9',100)
--INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1023,'PD','Additional Physical Damage Coverage (M-14)',100)
--INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1024,'PD','Additional Physical Damage Coverage (M-14)',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1025,'MPEMC','Motorcycle Policy (MC-1)',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1026,'MPEMC','Motorcycle Policy (MC-1)',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1027,'COLL','Motorcycle Trailer - M-49  Collision',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1028,'COLL','Motorcycle Trailer - M-49 Collision',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (20008,'MCCACYF','Michigan Statutory Assessments Fee',100)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (8000,'COLL','Collision',25)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (8000,'COMP','Other Than Collision (Comprehensive)',75)
INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (8001,'COMP','Other Than Collision (Comprehensive)',100)

IF (@STATE_ID IS NULL OR @STATE_ID = '')
	BEGIN 
	SELECT LOB, MS.STATE_NAME, 
	CASE WHEN TEMP.COV_ID IN (203,219,1023,1024) THEN 
			CASE WHEN VMM.[LOSS TYPE] = 124 THEN 'COLL' ELSE 'COMP' END
	ELSE TEMP.COV_CODE END AS LOSS_TYPE, 
	SUM(AMOUNT)* TEMP.PERCENTAGE / 100 AS AMOUNT,
	DETAIL_TYPE_DESCRIPTION, 
	ACTV_MONTH, ACTV_YEAR 
	FROM #MOT_CLAIM_DETAIL VMM
	INNER JOIN MNT_COUNTRY_STATE_LIST MS
	ON VMM.STATE_ID = MS.STATE_ID
	INNER JOIN #TEMP_COVERAGE_DETAILS TEMP
	ON VMM.COVERAGE_ID = TEMP.COV_ID
	--WHERE VMM.ACTV_MONTH <= @MONTH AND VMM.ACTV_YEAR = @YEAR
	GROUP BY LOB, MS.STATE_NAME, TEMP.COV_CODE, DETAIL_TYPE_DESCRIPTION,ACTV_MONTH, ACTV_YEAR,[LOSS TYPE],TEMP.COV_ID,
	TEMP.PERCENTAGE
	ORDER BY ACTV_MONTH, ACTV_YEAR 
	END 
	
ELSE
	
	SELECT LOB, MS.STATE_NAME, 
	CASE WHEN TEMP.COV_ID IN (203,219,1023,1024) THEN 
			CASE WHEN VMM.[LOSS TYPE] = 124 THEN 'COLL' ELSE 'COMP' END
	ELSE TEMP.COV_CODE END AS LOSS_TYPE, 
	SUM(AMOUNT)* TEMP.PERCENTAGE / 100 AS AMOUNT, DETAIL_TYPE_DESCRIPTION, 
	ACTV_MONTH, ACTV_YEAR 
	FROM #MOT_CLAIM_DETAIL VMM
	INNER JOIN MNT_COUNTRY_STATE_LIST MS
	ON VMM.STATE_ID = MS.STATE_ID
	INNER JOIN #TEMP_COVERAGE_DETAILS TEMP
	ON VMM.COVERAGE_ID = TEMP.COV_ID
	GROUP BY LOB, MS.STATE_NAME, TEMP.COV_CODE, DETAIL_TYPE_DESCRIPTION,ACTV_MONTH, ACTV_YEAR,[LOSS TYPE],TEMP.COV_ID,
	TEMP.PERCENTAGE
	ORDER BY ACTV_MONTH, ACTV_YEAR 



DROP TABLE #TEMP_COVERAGE_DETAILS
END 

--GO
--EXEC Proc_MakeClaimsReport 7,2009,14
--ROLLBACK TRAN


GO

