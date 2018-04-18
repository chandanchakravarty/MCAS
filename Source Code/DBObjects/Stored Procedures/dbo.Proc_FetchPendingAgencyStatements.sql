IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchPendingAgencyStatements]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchPendingAgencyStatements]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------- 
/*---------------------------------------------------------------------------      
CREATE BY   	: Arun Dhingra    
CREATE DATETIME : 2 Aug 2007  
PURPOSE    	: 
REVIOSN HISTORY      
Revised By  Date  Reason      

Proc_FetchPendingAgencyStatements 8,2007,'Wolverine'
Proc_FetchPendingAgencyStatements 11,2007
exec Proc_FetchPendingAgencyStatements @MONTH = 11, @YEAR = 2007, @AGENCY_NAME = NULL
----------------------------------------------------------------------------*/      
--drop proc dbo.Proc_FetchPendingAgencyStatements 
CREATE  PROC dbo.Proc_FetchPendingAgencyStatements 
(      
 @MONTH  INT =null,      
 @YEAR  INT  =null,    
 @AGENCY_NAME VARCHAR(100) = null    
)      
AS    
  
BEGIN   
 
DECLARE @YES Int,
	@No Int,
	@EFT_MODE Int,
	@CHECK_MODE Int
SET @YES = 10963
SET @NO  = 10964
SET @EFT_MODE = 11976
SET @CHECK_MODE = 11975

DECLARE @WHEREPARAM VARCHAR(2000)                    
SET @WHEREPARAM = ''  

	SELECT 
	CASE ISNULL(ACI.CHECK_ID,0) 
		WHEN 0 THEN 'False'
		else  'True' END as SELECTED_ITEMS ,
	ACI.CHECK_ID, 
	AAS.AGENCY_ID,AGENCY.AGENCY_DISPLAY_NAME,AGENCY.AGENCY_CODE,
	AAS.MONTH_NUMBER AS [MONTH],AAS.MONTH_YEAR AS [YEAR],
	CONVERT(VARCHAR(30),AAS.MONTH_NUMBER) + ' ' +  CONVERT(VARCHAR(20),AAS.MONTH_YEAR) AS MONTHYEAR, 
	SUM(ISNULL(AAS.DUE_AMOUNT,0))  * -1 AS STMT_AMOUNT,
	(SUM(ISNULL(AAS.DUE_AMOUNT,0)) - SUM(ISNULL(AAS.TOTAL_PAID,0))) * -1  AS BALANCE_AMOUNT,
	ACI.CHECK_AMOUNT AS PAYMENT_AMOUNT, 
	CASE ISNULL(ACI.PAYMENT_MODE,0) 
	When 0 THEN
		CASE ISNULL(AGENCY.ALLOWS_EFT,@NO) 
			WHEN @YES THEN @EFT_MODE
			ELSE @CHECK_MODE END 
	ELSE ACI.PAYMENT_MODE END
	AS PAYMENT_MODE ,
	CASE ISNULL(AGENCY.ALLOWS_EFT,@NO) 
		WHEN @YES THEN 'YES'
	ELSE 'NO' END  AS ALLOW_EFT,
	'Distribute' AS DISTRIBUTE,
	ISNULL(ACI.IS_RECONCILED,'N') as IS_RECONCILED
	FROM ACT_AGENCY_STATEMENT AAS 
	LEFT JOIN TEMP_ACT_CHECK_INFORMATION ACI 
	ON AAS.AGENCY_ID = ACI.PAYEE_ENTITY_ID
 	AND AAS.MONTH_NUMBER = ACI.[MONTH]
	AND AAS.MONTH_YEAR = ACI.[YEAR]
	LEFT JOIN MNT_AGENCY_LIST AGENCY ON AGENCY.AGENCY_ID = AAS.AGENCY_ID
	WHERE AAS.MONTH_NUMBER = isnull(@MONTH,AAS.MONTH_NUMBER)  
	AND AAS.MONTH_YEAR = isnull(@YEAR,AAS.MONTH_YEAR) 
	AND AGENCY.AGENCY_DISPLAY_NAME =  isnull(@AGENCY_NAME,AGENCY.AGENCY_DISPLAY_NAME)
	AND ISNULL(AAS.IS_CHECK_CREATED,'N') <> 'Y'
	--AND AAS.COMM_TYPE=@COMM_TYPE 
	GROUP BY AAS.AGENCY_ID,AAS.MONTH_NUMBER,AAS.MONTH_YEAR,
	AGENCY.AGENCY_DISPLAY_NAME,AGENCY.AGENCY_CODE,ACI.CHECK_ID,ACI.IS_RECONCILED,
	ACI.CHECK_AMOUNT,ACI.PAYMENT_MODE,AGENCY.ALLOWS_EFT
	HAVING (SUM(ISNULL(AAS.DUE_AMOUNT,0)) - SUM(ISNULL(AAS.TOTAL_PAID,0)))< 0
	
END

   

  









GO

