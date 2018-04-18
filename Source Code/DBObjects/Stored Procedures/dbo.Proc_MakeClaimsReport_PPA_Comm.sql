IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_MakeClaimsReport_PPA_Comm]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_MakeClaimsReport_PPA_Comm]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--begin tran
--drop proc dbo.Proc_MakeClaimsReport_PPA_Comm
--go 

/*----------------------------------------------------------                                                                          
Proc Name       : dbo.Proc_MakeClaimsReport_PPA_Comm                                          
Created by      : Shikha Dixit                                                                     
Date            : 20/08/2008                                                                          
Purpose         : Generates sum of Claims amount :- state, lob, month, year wise                      
Revewed by      :                                                                          
Revison History :                                                                          
Used In        : Wolverine                                                                          
------------------------------------------------------------                                                                          
Date     Review By          Comments                                                                          
------   ------------       -------------------------*/                                                                          
--DROP PROC dbo.Proc_MakeClaimsReport_PPA_Comm
CREATE PROC [dbo].[Proc_MakeClaimsReport_PPA_Comm]                                          
(                                        
	@MONTH int,                                         
	@YEAR int,
	@STATE_ID varchar(50) = NULL
)                      
AS                                                                          
BEGIN
--	begin tran
	CREATE TABLE #TEMP_COVERAGE_DETAILS
	(
		COV_ID		INT,
		COV_CODE	VARCHAR(50),
		COV_DES		VARCHAR(100),
		PERCENTAGE	DECIMAL(18,2)
	)
	

	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1,'CSL','Single Limit Liability (CSL)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (2,'BI','Bodily Injury Liability (Split Limits)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (4,'PD','Property Damage Liability',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (6,'MP','Medical Payments',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (9,'UM/BI','Uninsured Motorist (CSL)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (12,'UM/BI','Uninsured Motorist (BI Split Limits)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (14,'UIM','Underinsured Motorist (CSL)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (34,'UIM','Underinsured Motorist (BI Split Limits)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (36,'UM/PD','Uninsured Motorist PD',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (38,'Collision','Collision',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (42,'COMP','Other than Collision (Comprehensive)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (44,'Road Service','Road Service',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (45,'Rental Reimb','Rental Reimbursement (A-89)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (46,'LLGC-Collision','Loan Lease –Collision',69)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (46,'LLGC-Comp ','Loan Lease –Comprehensive',31)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (49,'Collision','Customizing Equipment ( A - 14)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (50,'COMP','Sound Reproducing - Tapes (A-29)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (52,'BI','Extended Non-Owned Coverage for Named Individual (A-35)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (54,'BI','Hired Automobiles - Cost of hire basis (A-85)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (113,'CSL','Single Limit Liability (CSL)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (114,'BI','Bodily Injury Liability (Split Limits)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (115,'PD','Property Damage Liability',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (116,'PIP','Personal Injury Protection',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (117,'PPI','Property Protection Insurance',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (118,'LPD','Limited Property Damage Liability (Mini-Tort)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (119,'UM/BI','Uninsured Motorist (CSL)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (120,'UM/BI','Uninsured Motorist (BI Split Limits)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (121,'UIM','Underinsured Motorist (BI Split Limits)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (122,'Collision','Collision',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (123,'COMP','Other Than Collision (Comprehensive)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (124,'Road Service','Road Service',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (125,'Rental Reimb','Rental Reimbursement (A-89)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (248,'BI','Employers Non-Ownership Liability (A-80)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (249,'LLGC-Collision','Loan Lease –Collision ',69)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (249,'LLGC-Comp ','Loan Lease –Comprehensive',31)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (251,'Collision','Customizing Equipment (A-14)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (252,'COMP','Sound Reproducing - Tapes (A-29)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (254,'BI','Extended Non-Owned Coverage for Named Individual (A-34)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (297,'BI','Employers Non-Ownership Liability (A-80)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (298,'BI','Hired Automobiles – Cost of Hire Basis (A-85)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (300,'COMP','Miscellaneous Extra Equipment - Comprehensive (A-16)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (301,'COMP','Miscellaneous Extra Equipment - Comprehensive A-15',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (302,'Collision','Miscellaneous Extra Equipment - Collision (A-16)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (303,'Collision','Miscellaneous Extra Equipment - Collision A-15',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (304,'UIM','Underinsured Motorist (CSL)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (997,'PIP','Coordination of Benefits  A-91',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (998,'Collision','Regular Collision Coverage A-68',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (999,'Collision','Diminishing Deductible A-25',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1000,'Collision','Antique Motor Car Endorsement A-49',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1001,'Collision','Classic Car Endorsement  A-46',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1002,'BI','Excluded Person(s) Endorsement A-95',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1003,'Collision','Motor Homes, Truck or Van Campers & Travel Trailers A-22',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1004,'INVALID','Personal Auto Policy - TB-6',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1005,'Rental Reimb','Transportation Expense A-90',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1006,'PIP','PIP Waiver/Rejection of Work Loss Benefit A-94',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1007,'UM/BI',' Rejection / Reduction of Uninsured & Underinsured Motorist Coverage (A-9)',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1008,'Collision','Antique Motor Car Endorsement A-49',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1009,'Collision','Classic Car Endorsement  A-46',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1010,'BI','Excluded Person(s) Endorsement A-96',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1011,'Collision','Stated Amount - A-44',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1012,'INVALID','Trailblazer Auto Policy - TB-7',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1013,'Rental Reimb','Transportation Expense A-90',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1014,'PD','Snow -Plowing A-8',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1015,'TE92','Terrorism Endorsement C-92',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1017,'PD','Snow -Plowing A-64',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1018,'Collision','Stated Amount - A-45',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1019,'TE92','Terrorism Endorsement C-92',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1020,'Collision','Motor Homes, Truck or Van Campers & Travel Trailers A-22',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1029,'COMP','Sound Receiving and Transmitting Equipment A-31',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (1030,'COMP','Sound Receiving and Transmitting Equipment A-31',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (10018,'INVALID','Amendment of Provisions - Michigan',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (20007,'MCCAF','Michigan Statutory Assessments Fee',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (20009,'MCCAF','Michigan Statutory Assessments Fee',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (50001,'PIP','MEDICAL',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (50002,'PIP','WORK LOSS',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (50003,'PIP','DEATH BENEFITS',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (50004,'PIP','SURVIVOR BENEFITS',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (50005,'BI','Single Limit Liability (CSL) BI',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (50006,'BI','Single Limit Liability (CSL) PD',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (50007,'UM/BI','Uninsured Motorist (CSL) BI',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (50008,'UM/PD','Uninsured Motorist (CSL) PD',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (50009,'UIM','Underinsured Motorist (CSL) BI',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (50010,'UIM','Underinsured Motorist (CSL) PD',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (50011,'BI','Single Limit Liability (CSL) BI',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (50012,'BI','Single Limit Liability (CSL) PD',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (50013,'UM/BI','Uninsured Motorist (CSL) BI',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (50014,'UM/PD','Uninsured Motorist (CSL) PD',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (50015,'UIM','Underinsured Motorist (CSL) BI',100)
	INSERT INTO #TEMP_COVERAGE_DETAILS VALUES (50016,'UIM','Underinsured Motorist (CSL) PD',100)
	
--	select * from #TEMP_COVERAGE_DETAILS
--	rollback tran
;
/*CREATE TABLE #TEMP
(
	LOB							VARCHAR(200),
	STATE						VARCHAR(50),
	LOSS_TYPE					VARCHAR(50),
	AMOUNT						DECIMAL(18,9),
	DETAIL_TYPE_DESCRIPTION		VARCHAR(100),
	ACTV_MONTH					INT,
	ACTV_YEAR					INT
)
DECLARE @MNTH	INT
DECLARE @YR		INT
SET @MNTH = 1
SET @YR = @YEAR
WHILE @MNTH <= @MONTH
BEGIN
	INSERT INTO #TEMP
		SELECT 'Commercial Auto', 22,COV_CODE,'0.00','Other Expense',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
		SELECT 'Commercial Auto', 22,COV_CODE,'0.00','Legal Expense',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
		SELECT 'Commercial Auto', 22,COV_CODE,'0.00','Adjustment Expense',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
		SELECT 'Commercial Auto', 22,COV_CODE,'0.00','Salvage Expense',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
		SELECT 'Commercial Auto', 22,COV_CODE,'0.00','Subrogation Expense',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
		SELECT 'Commercial Auto', 22,COV_CODE,'0.00','Paid Loss',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
		SELECT 'Commercial Auto', 22,COV_CODE,'0.00','Salvage',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS  UNION
		SELECT 'Commercial Auto', 22,COV_CODE,'0.00','Subrogation (Check Received)',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
--		SELECT 'Commercial Auto', 22,COV_CODE,'0.00','Other',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
		--********************--
		SELECT 'Commercial Auto', 14,COV_CODE,'0.00','Other Expense',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
		SELECT 'Commercial Auto', 14,COV_CODE,'0.00','Legal Expense',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
		SELECT 'Commercial Auto', 14,COV_CODE,'0.00','Adjustment Expense',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
		SELECT 'Commercial Auto', 14,COV_CODE,'0.00','Salvage Expense',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
		SELECT 'Commercial Auto', 14,COV_CODE,'0.00','Subrogation Expense',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
		SELECT 'Commercial Auto', 14,COV_CODE,'0.00','Paid Loss',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
		SELECT 'Commercial Auto', 14,COV_CODE,'0.00','Salvage',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS  UNION
		SELECT 'Commercial Auto', 14,COV_CODE,'0.00','Subrogation (Check Received)',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
--		SELECT 'Commercial Auto', 14,COV_CODE,'0.00','Other',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
		--*************************--
		SELECT 'Commercial Auto', 49,COV_CODE,'0.00','Other Expense',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
		SELECT 'Commercial Auto', 49,COV_CODE,'0.00','Legal Expense',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
		SELECT 'Commercial Auto', 49,COV_CODE,'0.00','Adjustment Expense',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
		SELECT 'Commercial Auto', 49,COV_CODE,'0.00','Salvage Expense',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
		SELECT 'Commercial Auto', 49,COV_CODE,'0.00','Subrogation Expense',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
		SELECT 'Commercial Auto', 49,COV_CODE,'0.00','Paid Loss',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS UNION
		SELECT 'Commercial Auto', 49,COV_CODE,'0.00','Salvage',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS  UNION
		SELECT 'Commercial Auto', 49,COV_CODE,'0.00','Subrogation (Check Received)',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS
--		SELECT 'Commercial Auto', 49,COV_CODE,'0.00','Other',@MNTH,@YR FROM #TEMP_COVERAGE_DETAILS
		
SET @MNTH = @MNTH + 1
END*/
--SELECT * FROM #TEMP




	IF (@STATE_ID IS NULL OR @STATE_ID = '')
	BEGIN 
		SELECT LOB, MS.STATE_NAME, TEMP.COV_CODE AS LOSS_TYPE, SUM(AMOUNT)*TEMP.PERCENTAGE/100 AS AMOUNT, DETAIL_TYPE_DESCRIPTION, 
		ACTV_MONTH, ACTV_YEAR 
		FROM VW_MakeClaimsReport_PPA_Comm VPC
		INNER JOIN MNT_COUNTRY_STATE_LIST MS
		ON VPC.STATE_ID = MS.STATE_ID
		INNER JOIN #TEMP_COVERAGE_DETAILS TEMP
		ON VPC.COVERAGE_ID = TEMP.COV_ID
		WHERE VPC.ACTV_MONTH <= @MONTH AND VPC.ACTV_YEAR = @YEAR
		GROUP BY LOB, MS.STATE_NAME, TEMP.COV_CODE, DETAIL_TYPE_DESCRIPTION,ACTV_MONTH, ACTV_YEAR,TEMP.PERCENTAGE 
		ORDER BY ACTV_MONTH, ACTV_YEAR 
		/*UNION
		SELECT LOB, MS.STATE_NAME, LOSS_TYPE, SUM(AMOUNT)AS AMOUNT,DETAIL_TYPE_DESCRIPTION,
		ACTV_MONTH,ACTV_YEAR 
		FROM #TEMP T
		INNER JOIN MNT_COUNTRY_STATE_LIST MS
		ON T.STATE = MS.STATE_ID
		WHERE T.ACTV_MONTH <= @MONTH AND T.ACTV_YEAR = @YEAR
		GROUP BY LOB, MS.STATE_NAME, LOSS_TYPE, DETAIL_TYPE_DESCRIPTION,ACTV_MONTH, ACTV_YEAR
		ORDER BY ACTV_MONTH, ACTV_YEAR 	*/
	END 
	
	ELSE
	
	SELECT LOB, MS.STATE_NAME, TEMP.COV_CODE AS LOSS_TYPE, SUM(AMOUNT)*TEMP.PERCENTAGE/100 AS AMOUNT, DETAIL_TYPE_DESCRIPTION, 
	ACTV_MONTH, ACTV_YEAR 
	FROM VW_MakeClaimsReport_PPA_Comm VPC
	INNER JOIN MNT_COUNTRY_STATE_LIST MS
	ON VPC.STATE_ID = MS.STATE_ID
	INNER JOIN #TEMP_COVERAGE_DETAILS TEMP
	ON VPC.COVERAGE_ID = TEMP.COV_ID
	WHERE VPC.STATE_ID = @STATE_ID AND VPC.ACTV_MONTH <= @MONTH AND VPC.ACTV_YEAR = @YEAR
	GROUP BY LOB, MS.STATE_NAME, TEMP.COV_CODE, DETAIL_TYPE_DESCRIPTION,ACTV_MONTH, ACTV_YEAR,TEMP.PERCENTAGE
	ORDER BY ACTV_MONTH, ACTV_YEAR 
	/*UNION
	SELECT LOB, MS.STATE_NAME, LOSS_TYPE, SUM(AMOUNT)AS AMOUNT,DETAIL_TYPE_DESCRIPTION,
	ACTV_MONTH,ACTV_YEAR 
	FROM #TEMP T
	INNER JOIN MNT_COUNTRY_STATE_LIST MS
	ON T.STATE = MS.STATE_ID
	WHERE T.ACTV_MONTH <= @MONTH AND T.ACTV_YEAR = @YEAR
	GROUP BY LOB, MS.STATE_NAME, LOSS_TYPE, DETAIL_TYPE_DESCRIPTION,ACTV_MONTH, ACTV_YEAR
	ORDER BY ACTV_MONTH, ACTV_YEAR 	*/


DROP TABLE #TEMP_COVERAGE_DETAILS
END 

--GO
--EXEC Proc_MakeClaimsReport_PPA_Comm 8,2009,''
--ROLLBACK TRAN




















GO

