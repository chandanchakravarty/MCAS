IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[rpt_SuspenseDeposits]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[rpt_SuspenseDeposits]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

---------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
  
--rpt_SuspenseDeposits Null,Null   
--rpt_SuspenseDeposits '1/1/2007','1/31/2007'  
--rpt_SuspenseDeposits '1/10/2007','1/10/2007'  
  
 --DROP PROC dbo.rpt_SuspenseDeposits      
CREATE PROC [dbo].[rpt_SuspenseDeposits]          
@FromDate DateTime = '01/01/1900',  
@ToDate  DateTime = '12/31/3000'  
  
as  
  
Begin  
  
-- Added by Asfa(23-June-2008) - iTrack #4373  
IF(@FromDate = '' OR (@FromDate IS NULL))  
  SET @FromDate = '01/01/1900'  
IF(@ToDate = '' OR (@ToDate IS NULL))  
  SET @ToDate = '12/31/3000'  
  
/*mODIFIED ON 19 SEP 2008 :   
If user selects only end date (no start date is specified) and there   
are records for this Day-Month-Year combination in Database then pull  
 the data from ACT_SUSPENSE_PAYMENT_REPORT, else fetch it as per existing logic.*/  
--IF(  
-- (@FromDate = '01/01/1900')  
-- AND   
-- (@ToDate <> '12/31/3000')  
--)  
--BEGIN  
  
 IF EXISTS (SELECT * FROM ACT_SUSPENSE_PAYMENT_REPORT ASPR  
   WHERE ASPR.[MONTH] = MONTH(@ToDate) AND ASPR.[YEAR] = YEAR(@ToDate) ) AND  (@FromDate = '01/01/1900')  
 AND   
 (@ToDate <> '12/31/3000')  
 BEGIN  
  
   SELECT ISNULL(POLICY_NUMBER,'') AS POLICY_NUMBER ,  
    ISNULL(DATE_PAID,0)AS DATE_PAID,  
    ISNULL(AMOUNT_PAID,0) AS AMT_PAID  
   FROM ACT_SUSPENSE_PAYMENT_REPORT ASPR  
   WHERE ASPR.[MONTH] = MONTH(@ToDate) AND ASPR.[YEAR] = YEAR(@ToDate)  
 END  
   
--END  
  
ELSE  
BEGIN  
/*END*/  
  
  
 /* By Rajan for Suspense Deposits */  
 IF  @FromDate IS NULL  AND  @ToDate IS NULL   
  begin  
   SELECT PCPL.POLICY_NUMBER, ACOI.SOURCE_EFF_DATE AS DATE_PAID, -(ISNULL(ACOI.TOTAL_DUE, 0) - ISNULL(ACOI.TOTAL_PAID, 0)) AS AMT_PAID  
   FROM ACT_CUSTOMER_OPEN_ITEMS ACOI   
   INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL  
    ON ACOI.CUSTOMER_ID = PCPL.CUSTOMER_ID  
    AND ACOI.POLICY_ID = PCPL.POLICY_ID  
    AND ACOI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID  
   WHERE  (ACOI.ITEM_STATUS = 'SP' OR ACOI.ITEM_STATUS = 'RSP')  
    AND (ISNULL(ACOI.TOTAL_DUE, 0) - ISNULL(ACOI.TOTAL_PAID, 0)) <> 0  
   ORDER BY PCPL.POLICY_NUMBER  
  end  
 else  
  begin  
   SELECT PCPL.POLICY_NUMBER, ACOI.SOURCE_EFF_DATE AS DATE_PAID, -(ISNULL(ACOI.TOTAL_DUE, 0) - ISNULL(ACOI.TOTAL_PAID, 0)) AS AMT_PAID  
   FROM ACT_CUSTOMER_OPEN_ITEMS ACOI   
   INNER JOIN POL_CUSTOMER_POLICY_LIST PCPL  
    ON ACOI.CUSTOMER_ID = PCPL.CUSTOMER_ID  
    AND ACOI.POLICY_ID = PCPL.POLICY_ID  
    AND ACOI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID  
   WHERE  (ACOI.ITEM_STATUS = 'SP' OR ACOI.ITEM_STATUS = 'RSP')  
    AND (ISNULL(ACOI.TOTAL_DUE, 0) - ISNULL(ACOI.TOTAL_PAID, 0)) <> 0  
   AND cast((Convert(varchar,ACOI.SOURCE_EFF_DATE,101)) as datetime)  BETWEEN  @FROMDATE AND @ToDate  
   ORDER BY PCPL.POLICY_NUMBER  
  end  
 /*  
 select * from act_customer_open_items where updated_from = 'd'  
 exec rpt_SuspenseDeposits    
 */  
END  
 End   
  
   
--EXEC rpt_SuspenseDeposits '','12/2/2000'  
  
  
GO

