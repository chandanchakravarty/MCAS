IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_SaveSuspensePaymentReport]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_SaveSuspensePaymentReport]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------                
Proc Name       : Dbo.Proc_SaveSuspensePaymentReport
Created by      : Ravindra 
Date            : 9-17-2008
Revison History :                

Used In     	 : Wolverine   (Cancellation Process)            
 
exec Proc_SaveSuspensePaymentReport 30,4,2008

------------------------------------------------------------                
Date     Review By          Comments                
------   ------------       -------------------------*/                
-- drop proc Dbo.Proc_SaveSuspensePaymentReport
CREATE PROC [dbo].[Proc_SaveSuspensePaymentReport]         
(     
	@DAY	Int,
	@MONTH	Int,
	@YEAR	Int
)
AS 
Begin

DECLARE @FROMDATE Datetime , 
		@ToDate   Datetime 
 
SET @FromDate = CAST(CONVERT(VARCHAR,@MONTH) +  '/'  + '01' + '/'+ CONVERT(VARCHAR,@YEAR) AS DATETIME ) 
SET @ToDate   = CAST(CONVERT(VARCHAR,@MONTH) +  '/' + CONVERT(VARCHAR,@DAY )  + '/'+ CONVERT(VARCHAR,@YEAR) AS DATETIME) 

INSERT INTO ACT_SUSPENSE_PAYMENT_REPORT (DAY,MONTH,YEAR,POLICY_NUMBER,DATE_PAID ,AMOUNT_PAID ) 
SELECT @DAY,@MONTH,@YEAR , 
	PCPL.POLICY_NUMBER, ACOI.SOURCE_EFF_DATE AS DATE_PAID, -(ISNULL(ACOI.TOTAL_DUE, 0) - ISNULL(ACOI.TOTAL_PAID, 0)) AS AMT_PAID
FROM ACT_CUSTOMER_OPEN_ITEMS ACOI 
INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL
	ON ACOI.CUSTOMER_ID = PCPL.CUSTOMER_ID
	AND ACOI.POLICY_ID = PCPL.POLICY_ID
	AND ACOI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID
WHERE  (ACOI.ITEM_STATUS = 'SP' OR ACOI.ITEM_STATUS = 'RSP')
	AND (ISNULL(ACOI.TOTAL_DUE, 0) - ISNULL(ACOI.TOTAL_PAID, 0)) <> 0
--AND cast((Convert(varchar,ACOI.SOURCE_EFF_DATE,101)) as datetime)  BETWEEN  @FROMDATE AND @ToDate
--Condition changed for itrack #6858.
AND cast((Convert(varchar,ACOI.SOURCE_EFF_DATE,101)) as datetime) <=  @ToDate
ORDER BY PCPL.POLICY_NUMBER

End  







GO

