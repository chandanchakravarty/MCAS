IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_1099_Check_Details]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_1099_Check_Details]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROC [dbo].[Proc_1099_Check_Details] 
(
 @FORM_1099_ID int	
)
as


DECLARE @EFT_SWEEP INT
SET @EFT_SWEEP = 11976

DECLARE @CHECK_MODE INT
SET @CHECK_MODE = 11787



DECLARE @CHECK_TYPE varchar(20)
DECLARE @DEPOSIT_TYPE varchar(20)
DECLARE @CLAIM_TYPE VARCHAR(20)
SET @CHECK_TYPE = 'Check'
SET @DEPOSIT_TYPE = 'Deposit'
set @CLAIM_TYPE = 'Check(Claims)'

SELECT 
ISNULL(CHECK_NUMBER,'') AS REF_NUMBER ,
CHECK_DATE AS [DATE],
'' AS CLAIM_NUMBER, 
CHECK_AMOUNT AS AMOUNT,
CASE PAYMENT_MODE WHEN @CHECK_MODE THEN 'Check'
				  WHEN @EFT_SWEEP THEN 'EFT'
ELSE @CHECK_TYPE END AS TYPE,
ISNULL(PAYEE_ENTITY_NAME,'') AS PAYEE_ENTITY_NAME
FROM ACT_CHECK_INFORMATION CHK
INNER JOIN CHECK_DETAILS_1099 DETAIL
ON DETAIL.REF_ID = CHK.CHECK_ID
WHERE FORM_1099_ID  = @FORM_1099_ID
and REF_TYPE = 'C'

UNION

SELECT 
CAST(ISNULL(DEPOSIT_NUMBER,'') AS VARCHAR) AS REF_NUMBER ,
DEPOSIT_TRAN_DATE AS [DATE],
'' AS CLAIM_NUMBER, 
ISNULL(TOTAL_DEPOSIT_AMOUNT,0) AS AMOUNT,
@DEPOSIT_TYPE AS TYPE,
ISNULL(RECEIPT_FROM_NAME,'') AS PAYEE_ENTITY_NAME
FROM ACT_CURRENT_DEPOSIT_LINE_ITEMS DEPLI
INNER JOIN CHECK_DETAILS_1099 DETAIL
ON DETAIL.REF_ID = DEPLI.CD_LINE_ITEM_ID
inner join ACT_CURRENT_DEPOSITS DEP
on DEP.DEPOSIT_ID 	= DEPLI.DEPOSIT_ID
WHERE FORM_1099_ID  = @FORM_1099_ID
and REF_TYPE = 'D'

UNION


--SELECT 
--ISNULL(CHECK_NUMBER,'') AS REF_NUMBER ,
--CHECK_DATE AS [DATE],
--CHECK_AMOUNT AS AMOUNT,
--@CLAIM_TYPE AS TYPE,
--ISNULL(PAYEE_ENTITY_NAME,'') AS PAYEE_ENTITY_NAME
--FROM ACT_CHECK_INFORMATION CHK
--INNER JOIN CHECK_DETAILS_1099 DETAIL
--ON DETAIL.REF_ID = CHK.CHECK_ID
--WHERE FORM_1099_ID  = @FORM_1099_ID
--and REF_TYPE = 'CLMV'
SELECT 
ISNULL(CHECK_NUMBER,'') AS REF_NUMBER ,
CHECK_DATE AS [DATE],
CLAIM_NUMBER AS CLAIM_NUMBER, 
CHECK_AMOUNT AS AMOUNT,
'Check' AS TYPE,
ISNULL(PAYEE_ENTITY_NAME,'') AS PAYEE_ENTITY_NAME
FROM ACT_CHECK_INFORMATION CHK WITH(NOLOCK)
INNER JOIN CHECK_DETAILS_1099 DETAIL WITH(NOLOCK)
ON DETAIL.REF_ID = CHK.CHECK_ID
LEFT JOIN CLM_ACTIVITY CLM_ACT WITH(NOLOCK)
ON CLM_ACT.CHECK_ID = DETAIL.REF_ID
LEFT JOIN CLM_CLAIM_INFO CLM_INFO WITH(NOLOCK)
ON CLM_INFO.CLAIM_ID = CLM_ACT.CLAIM_ID
WHERE FORM_1099_ID  = @FORM_1099_ID
AND REF_TYPE = 'CLMV'

------------------------



GO

