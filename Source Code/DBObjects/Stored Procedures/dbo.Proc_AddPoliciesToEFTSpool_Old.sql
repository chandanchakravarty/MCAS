IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_AddPoliciesToEFTSpool_Old]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_AddPoliciesToEFTSpool_Old]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--BEGIN TRAN
--drop proc dbo.Proc_AddPoliciesToEFTSpool_Old
--go 
/*----------------------------------------------------------  
Proc Name        : dbo.Proc_AddPoliciesToEFTSpool_Old
Created by       : Ravinda Gupta 
Date             : 3-20-2007
Purpose      	 : 
Revison History  :  
Used In   :Wolverine  

Reviewed By	: Anurag Verma
Reviewed On	: 12-07-2007

Modified By	: Ravindra
Modofied On	: 11-01-2007
Purpose		: To Change Due Date logic now for Sweep date of EFT SWWEP_DATE column of ACT_CUSTOMER_OPEN_ITEMS
			need to be considered.

------------------------------------------------------------  
Date     Review By          Comments  
------------------------------------------------------------*/  
-- drop proc dbo.Proc_AddPoliciesToEFTSpool_Old
CREATE PROC [dbo].[Proc_AddPoliciesToEFTSpool_Old]
AS
BEGIN 
DECLARE @EFT Int,
		@EOD_DATE DateTime ,
		@NACHA_DEBIT_ENTRY  Int,
		@TEMP_ERROR_CODE INT              

SET 	@EOD_DATE  = GETDATE()
SET 	@NACHA_DEBIT_ENTRY = 11
SET		@EFT   = 11973

BEGIN TRAN

CREATE TABLE #TMP_POLICIES
(
	IDEN_COL    Int identity(1,1) not null,
	CUSTOMER_ID Int,
	POLICY_ID   Int,
	POLICY_VERSION_ID Int,
	POLICY_NUMBER Varchar(20),
	NET_DUE Decimal(18,2),
	DUE_DATE Datetime,
	OPEN_ITEM_ID INt
)

-- Fetching All Installments which are scheduled for  EFT Today and the tentative day for EFT is today

INSERT INTO #TMP_POLICIES(CUSTOMER_ID, POLICY_ID, POLICY_NUMBER,
		NET_DUE, DUE_DATE, 	OPEN_ITEM_ID )
SELECT CUSTOMER_ID,POLICY_ID,POLICY_NUMBER,
	SUM(ISNULL(TOTAL_DUE,0) ) - SUM(ISNULL(TOTAL_PAID,0)) AS NET_DUE,
	DUE_DATE ,
	OPEN_ITEM_ID
FROM 
(
	SELECT OI.IDEN_ROW_ID AS OPEN_ITEM_ID,OI.TOTAL_DUE ,OI.TOTAL_PAID,INSD.CUSTOMER_ID , 
	INSD.POLICY_ID, INSD.POLICY_VERSION_ID,
	CPL.POLICY_NUMBER AS POLICY_NUMBER,
	OI.SWEEP_DATE AS DUE_DATE,
	CPL.INSTALL_PLAN_ID,
	OI.INSTALLMENT_ROW_ID AS INSTALLMENT_ROW_ID
	FROM ACT_CUSTOMER_OPEN_ITEMS OI WITH(NOLOCK)
	INNER JOIN POL_CUSTOMER_POLICY_LIST CPL
	ON  OI.CUSTOMER_ID  = CPL.CUSTOMER_ID 
	AND OI.POLICY_ID   = CPL.POLICY_ID 
	AND OI.POLICY_VERSION_ID = CPL.POLICY_VERSION_ID
	INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS INSD WITH(NOLOCK)
	ON INSD.ROW_ID = OI.INSTALLMENT_ROW_ID
	--Added by Charles on 30-Oct-09 for Itrack 6000
	INNER JOIN ACT_INSTALL_PLAN_DETAIL INS WITH(NOLOCK)  
	ON CPL.INSTALL_PLAN_ID = INS.IDEN_PLAN_ID
	--Added till here
	WHERE ISNULL(INSD.PAYMENT_MODE,0) = @EFT 
	AND ISNULL(OI.SPOOLED_FOR_EFT,'N') <> 'Y'
	--Added by Charles on 30-Oct-09 for Itrack 6000
	AND ISNULL(OI.TOTAL_DUE,0) <> ISNULL(OI.TOTAL_PAID,0)
	AND (
		SELECT  ((  SUM(ISNULL(OI1.TOTAL_DUE,0)) - SUM(ISNULL(OI1.TOTAL_PAID,0)) )
		/
		(SUM(ISNULL(OI1.TOTAL_DUE,0) - ISNULL(OI1.APPLIED_AT_CANCELLATION,0) )))	
		* 100
		FROM ACT_CUSTOMER_OPEN_ITEMS OI1 WITH(NOLOCK)
		LEFT  JOIN ACT_POLICY_INSTALLMENT_DETAILS INSD1 WITH(NOLOCK) 
		ON OI1.INSTALLMENT_ROW_ID  = INSD1.ROW_ID
		WHERE 	OI1.CUSTOMER_ID = CPL.CUSTOMER_ID 
		AND OI1.POLICY_ID = CPL.POLICY_ID
		AND INSD1.CURRENT_TERM  = CPL.CURRENT_TERM
		AND INSD1.INSTALLMENT_NO = INSD.INSTALLMENT_NO
		AND OI1.ITEM_TRAN_CODE_TYPE = 'PREM'
	) > INS.AMTUNDER_PAYMENT
	--Added till here
) TEMP
WHERE  CAST(CONVERT(VARCHAR,TEMP.DUE_DATE,101) AS DATETIME)   
 	<= CAST(CONVERT(VARCHAR, @EOD_DATE ,101) AS DATETIME) 
GROUP BY CUSTOMER_ID,POLICY_ID,DUE_DATE,POLICY_NUMBER ,INSTALL_PLAN_ID,OPEN_ITEM_ID


SELECT @TEMP_ERROR_CODE = @@ERROR                                                             
IF (@TEMP_ERROR_CODE <> 0) 
	GOTO PROBLEM                                                          

-- Fetching other items(Fees etc) for which Due date is today or less
INSERT INTO #TMP_POLICIES(CUSTOMER_ID, POLICY_ID, POLICY_VERSION_ID,POLICY_NUMBER,
	NET_DUE,DUE_DATE,OPEN_ITEM_ID )
SELECT OI.CUSTOMER_ID,OI.POLICY_ID,OI.POLICY_VERSION_ID,CPL.POLICY_NUMBER,
	ISNULL(TOTAL_DUE ,0) - ISNULL(TOTAL_PAID,0) , 
	@EOD_DATE ,IDEN_ROW_ID
FROM ACT_CUSTOMER_OPEN_ITEMS OI  WITH(NOLOCK)
INNER JOIN POL_CUSTOMER_POLICY_LIST CPL  WITH(NOLOCK)
ON OI.CUSTOMER_ID = CPL.CUSTOMER_ID 
AND OI.POLICY_ID = CPL.POLICY_ID 
AND OI.POLICY_VERSION_ID = CPL.POLICY_VERSION_ID 
WHERE OI.IDEN_ROW_ID NOT IN (SELECT OPEN_ITEM_ID  FROM #TMP_POLICIES)
AND EXISTS 
	(SELECT CUSTOMER_ID FROM #TMP_POLICIES TEMP WHERE TEMP.CUSTOMER_ID = OI.CUSTOMER_ID AND TEMP.POLICY_ID = OI.POLICY_ID )
AND   CAST(CONVERT(VARCHAR, ISNULL(OI.SWEEP_DATE,OI.DUE_DATE ) ,101) AS DATETIME)   
 	<= CAST(CONVERT(VARCHAR, @EOD_DATE ,101) AS DATETIME) 
AND   ISNULL(OI.SPOOLED_FOR_EFT,'N') <> 'Y'
AND   (TOTAL_DUE - TOTAL_PAID) > 0


SELECT @TEMP_ERROR_CODE = @@ERROR                                                             
IF (@TEMP_ERROR_CODE <> 0) 
	GOTO PROBLEM          


CREATE TABLE #RECORDS_TO_EFT 
(
	IDEN_COL Int identity(1,1) not null,
	CUSTOMER_ID Int,
	POLICY_ID   Int,
	POLICY_NUMBER Varchar(20),
	AMOUNT Decimal(18,2)
)

SELECT @TEMP_ERROR_CODE = @@ERROR                                                     
IF (@TEMP_ERROR_CODE <> 0) 
	GOTO PROBLEM                                                          

 
INSERT INTO #RECORDS_TO_EFT (CUSTOMER_ID , POLICY_ID   ,POLICY_NUMBER ,AMOUNT )
SELECT CUSTOMER_ID, POLICY_ID,POLICY_NUMBER ,SUM(NET_DUE) 
FROM #TMP_POLICIES
GROUP BY CUSTOMER_ID, POLICY_ID,POLICY_NUMBER


SELECT @TEMP_ERROR_CODE = @@ERROR                                                             
IF (@TEMP_ERROR_CODE <> 0) 
	GOTO PROBLEM                                                          


DECLARE @IDEN_COl Int,
		@POLICY_NUMBER VARCHAR(20),
		@AMOUNT Decimal(18,2),
		@CUSTOMER_ID int , 
		@POLICY_ID	Int, 
		@PREVIOUS_BALANCE Decimal(18,2) 
SET @IDEN_COL = 1
WHILE 1 = 1
BEGIN 
	IF NOT EXISTS(SELECT IDEN_COL FROM #RECORDS_TO_EFT WHERE IDEN_COL = @IDEN_COL )
	BEGIN 
		Break
	END
	SELECT  @CUSTOMER_ID	= CUSTOMER_ID,
			@POLICY_NUMBER	= POLICY_NUMBER ,
			@AMOUNT			= AMOUNT , 
			@POLICY_ID		= POLICY_ID ,
			@PREVIOUS_BALANCE = 0 
	FROM #RECORDS_TO_EFT WHERE IDEN_COL = @IDEN_COL 


	SELECT 	@PREVIOUS_BALANCE = SUM(ISNULL(TOTAL_DUE,0 )  - ISNULL(TOTAL_PAID , 0 ) ) 
	FROM ACT_CUSTOMER_OPEN_ITEMS OI  WITH(NOLOCK)
	WHERE OI.CUSTOMER_ID = @CUSTOMER_ID 
	AND OI.POLICY_ID = @POLICY_ID 
	AND   OI.IDEN_ROW_ID NOT IN (SELECT OPEN_ITEM_ID  FROM #TMP_POLICIES)
	AND   CAST(CONVERT(VARCHAR, ISNULL(OI.SWEEP_DATE,OI.DUE_DATE ) ,101) AS DATETIME)   
		<= CAST(CONVERT(VARCHAR, @EOD_DATE ,101) AS DATETIME) 
	AND   ISNULL(OI.SPOOLED_FOR_EFT,'N') =  'Y'
	AND   (TOTAL_DUE - TOTAL_PAID) > 0

	SET @AMOUNT = @AMOUNT  + ISNULL( @PREVIOUS_BALANCE,0) 

	IF( @AMOUNT > 0)
	BEGIN 
		exec Proc_EOD_AddREcordToEFTSpool @CUSTOMER_ID,'CUST',@POLICY_NUMBER,NULL,NULL,
					NULL,NULL,NULL,@AMOUNT,@NACHA_DEBIT_ENTRY , 'Y',1 , 104
	
		SELECT @TEMP_ERROR_CODE = @@ERROR                                                             
		IF (@TEMP_ERROR_CODE <> 0) 
			GOTO PROBLEM                                                          
	END


	SET @IDEN_COL = @IDEN_COL + 1
END


UPDATE ACT_CUSTOMER_OPEN_ITEMS SET SPOOLED_FOR_EFT = 'Y'
WHERE IDEN_ROW_ID IN (SELECT OPEN_ITEM_ID  FROM #TMP_POLICIES)

SELECT @TEMP_ERROR_CODE = @@ERROR                                                             
IF (@TEMP_ERROR_CODE <> 0) 
	GOTO PROBLEM                                                          

DROP TABLE #RECORDS_TO_EFT 
DROP TABLE #TMP_POLICIES

SELECT @TEMP_ERROR_CODE = @@ERROR                                                             
IF (@TEMP_ERROR_CODE <> 0) 
	GOTO PROBLEM                                                          

                                                          
COMMIT TRAN                                                                                               

PROBLEM:                                                                                              
	IF (@TEMP_ERROR_CODE <> 0)                              
	BEGIN                                                                                   
 		ROLLBACK TRAN                    
	END                                                                                              
END

--
--go 
--exec Proc_AddPoliciesToEFTSpool_Old
--
--select * from EOD_EFT_SPOOL WHERE iden_row_id > 10667 order by iden_Row_Id desc
--
--rollback tran
--


GO

