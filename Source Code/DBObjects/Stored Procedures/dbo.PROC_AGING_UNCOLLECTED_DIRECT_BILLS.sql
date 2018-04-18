IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_AGING_UNCOLLECTED_DIRECT_BILLS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_AGING_UNCOLLECTED_DIRECT_BILLS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--DROP PROC [PROC_AGING_UNCOLLECTED_DIRECT_BILLS]              
CREATE PROC [dbo].[PROC_AGING_UNCOLLECTED_DIRECT_BILLS]                    
AS                     
BEGIN       
  
SET NOCOUNT ON

CREATE TABLE #AGING_DB_UNCOLLECTED_PREMIUMS                  
(               
  CUSTOMER_ID INT,                  
  POLICY_ID INT,                   
  CURRENT_TERM SMALLINT,                    
  TOTAL DECIMAL(20,2),                  
  DUE_DATE DATETIME              
)   
  
INSERT INTO #AGING_DB_UNCOLLECTED_PREMIUMS (CUSTOMER_ID,POLICY_ID,CURRENT_TERM,TOTAL,DUE_DATE)                  
(  
SELECT UCP.CUSTOMER_ID , UCP.POLICY_ID , UCP.CURRENT_TERM ,  
(UCP.TOTAL_DUE - UCP.TOTAL_PAID - UCP.SUSPENSE_PREMIUM)+   
(  
CASE WHEN  
 (SELECT ISNULL(SUM(ISNULL(OI.TOTAL_DUE,0) - ISNULL(OI.TOTAL_PAID,0)),0) FROM ACT_CUSTOMER_OPEN_ITEMS OI WITH(NOLOCK)  
 LEFT OUTER JOIN ACT_POLICY_INSTALLMENT_DETAILS APID  WITH(NOLOCK)  
 ON OI.INSTALLMENT_ROW_ID = APID.ROW_ID   
 WHERE OI.ITEM_TRAN_CODE_TYPE = 'PREM' AND OI.CUSTOMER_ID = UCP.CUSTOMER_ID AND OI.POLICY_ID = UCP.POLICY_ID   
 AND APID.CURRENT_TERM=UCP.CURRENT_TERM-1 AND UCP.CURRENT_TERM>1) <0   
THEN  
 (SELECT ISNULL(SUM(ISNULL(OI.TOTAL_DUE,0) - ISNULL(OI.TOTAL_PAID,0)),0) FROM ACT_CUSTOMER_OPEN_ITEMS OI WITH(NOLOCK)  
 LEFT OUTER JOIN ACT_POLICY_INSTALLMENT_DETAILS APID  WITH(NOLOCK)  
 ON OI.INSTALLMENT_ROW_ID = APID.ROW_ID   
 WHERE OI.ITEM_TRAN_CODE_TYPE = 'PREM' AND OI.CUSTOMER_ID = UCP.CUSTOMER_ID AND OI.POLICY_ID = UCP.POLICY_ID   
 AND APID.CURRENT_TERM=UCP.CURRENT_TERM-1 AND UCP.CURRENT_TERM>1)  
ELSE 0  
END  
),    
CASE               
 WHEN [DBO].[IsRenewedPolicy]              
  (              
  UCP.CUSTOMER_ID,              
  UCP.POLICY_ID,              
  (SELECT MAX(CPL.POLICY_VERSION_ID) FROM POL_CUSTOMER_POLICY_LIST CPL WITH(NOLOCK) WHERE CPL.CUSTOMER_ID=UCP.CUSTOMER_ID AND CPL.POLICY_ID=UCP.POLICY_ID)              
  ) = 0               
  AND ISNULL(UCP.TOTAL_PAID,0) = 0                   
  THEN CONVERT(VARCHAR,UCP.APP_EFFECTIVE_DATE,101)                  
 ELSE  
  ISNULL  
   (  
    (  
     SELECT CONVERT(VARCHAR,MIN(T.INSTALLMENT_EFFECTIVE_DATE),101) FROM   
     (      
      SELECT MIN(B.INSTALLMENT_EFFECTIVE_DATE) AS INSTALLMENT_EFFECTIVE_DATE           
      FROM ACT_CUSTOMER_OPEN_ITEMS A  WITH(NOLOCK)           
      INNER JOIN ACT_POLICY_INSTALLMENT_DETAILS B  WITH(NOLOCK)           
      ON A.INSTALLMENT_ROW_ID = B.ROW_ID          
      WHERE A.CUSTOMER_ID = UCP.CUSTOMER_ID  AND A.POLICY_ID = UCP.POLICY_ID AND B.CURRENT_TERM = UCP.CURRENT_TERM          
      AND A.ITEM_TRAN_CODE_TYPE <> 'FEES'   
      GROUP BY B.INSTALLMENT_EFFECTIVE_DATE           
      HAVING SUM(ISNULL(A.TOTAL_DUE,0) - ISNULL(A.TOTAL_PAID ,0))>0            
     )T   
    )   
    ,  
    (  
     SELECT CONVERT(VARCHAR,MIN(OI1.SOURCE_EFF_DATE),101) FROM ACT_CUSTOMER_OPEN_ITEMS OI1 WITH(NOLOCK)   
     WHERE OI1.CUSTOMER_ID = UCP.CUSTOMER_ID  AND OI1.POLICY_ID = UCP.POLICY_ID  
     AND OI1.UPDATED_FROM IN ('P','J')  
     AND ISNULL(OI1.TOTAL_DUE,0) - ( ISNULL(OI1.TOTAL_PAID,0) - ISNULL(OI1.APPLIED_AT_CANCELLATION,0) ) > 0   
    )  
   )  
 END AS DUE_DATE    
FROM   
(  
      SELECT  
      UCPD.CUSTOMER_ID,  
      UCPD.POLICY_ID,  
   UCPD.CURRENT_TERM ,  
   UCPD.APP_EFFECTIVE_DATE ,  
      SUM(UCPD.TOTAL_DUE) AS TOTAL_DUE,  
      SUM(UCPD.TOTAL_PAID) AS TOTAL_PAID,  
      SUM(UCPD.SUSPENSE_PREMIUM) AS SUSPENSE_PREMIUM,  
      SUM(UCPD.ADVANCE_PAYMENT) AS ADVANCE_PAYMENT,  
      SUM(UCPD.SUSPENSE_PAYMENT) AS SUSPENSE_PAYMENT  
      FROM  
      (    
   SELECT  
            ACOI.CUSTOMER_ID,  
            ACOI.POLICY_ID,  
   PCPL.CURRENT_TERM ,   
   PCPL.APP_EFFECTIVE_DATE ,  
            ISNULL(ACOI.TOTAL_DUE, 0.0) AS TOTAL_DUE,  
            ISNULL(ACOI.TOTAL_PAID, 0.0) AS TOTAL_PAID,  
  
            -- DOES THIS RECORD INCLUDE PREMIUM IN SUSPENSE?  
            -- PREMIUM IS IN SUSPENSE WHEN IT IS POSTED FOR A POLICY VERSION WITH A FUTURE EFFECTIVE DATE  
            CASE WHEN ACOI.ITEM_TRAN_CODE_TYPE = 'PREM'  
                          AND DATEDIFF(DD,GETDATE(),PCPL.POL_VER_EFFECTIVE_DATE ) > 0    
                        THEN TOTAL_DUE  - ISNULL(WRITE_OFF_AMOUNT,0)  
                   ELSE 0.0  
            END AS SUSPENSE_PREMIUM,  
  
            -- DOES THIS RECORD INCLUDE AN ADVANCE PAYMENT?  
            -- A PAYMENT IS IN ADVANCE WHEN IT IS MADE AGAINST A POLICY WHOSE EFFECTIVE DATE IS IN THE NEXT MONTH  
         CASE WHEN ACOI.ITEM_TRAN_CODE_TYPE = 'PREM'  
                        AND ( YEAR(PCPL.APP_EFFECTIVE_DATE) > YEAR(GETDATE())  
                        OR ( MONTH(PCPL.APP_EFFECTIVE_DATE) > MONTH(GETDATE())  
                        AND YEAR(PCPL.APP_EFFECTIVE_DATE) = YEAR(GETDATE())  
                        )  
                        ) THEN TOTAL_PAID - ISNULL(WRITE_OFF_AMOUNT,0)  
                        WHEN ACOI.ITEM_TRAN_CODE_TYPE = 'DEP'  
                        AND ( YEAR(PCPL.APP_EFFECTIVE_DATE) > YEAR(GETDATE())  
                        OR ( MONTH(PCPL.APP_EFFECTIVE_DATE) > MONTH(GETDATE())  
                        AND YEAR(PCPL.APP_EFFECTIVE_DATE) = YEAR(GETDATE())  
                        )  
                        ) THEN (isnull(TOTAL_DUE,0) - isnull(TOTAL_PAID, 0))  * - 1   
                        ELSE 0.0  
                  END AS ADVANCE_PAYMENT,  
            
            -- DOES THIS RECORD INCLUDE AN SUSPENSE PAYMENT?  
            -- A PAYMENT IS IN SUSPENSE WHEN IT IS MADE AGAINST A NEW BUSINESS POLICY IN PROGRESS OR SUSPENDED STATES  
            CASE WHEN ( PCPL.POLICY_STATUS = 'UISSUE'  
                              OR PCPL.POLICY_STATUS = 'Suspended'  
                          )  
                          AND ACOI.ITEM_TRAN_CODE_TYPE = 'DEP' THEN ABS(TOTAL_DUE - ISNULL(TOTAL_PAID,0))  
                   ELSE 0.0  
            END AS SUSPENSE_PAYMENT  
        FROM  
            ACT_CUSTOMER_OPEN_ITEMS AS ACOI WITH(NOLOCK)  
            INNER JOIN POL_CUSTOMER_POLICY_LIST AS PCPL WITH(NOLOCK)  
                  ON ACOI.CUSTOMER_ID = PCPL.CUSTOMER_ID  
                     AND ACOI.POLICY_ID = PCPL.POLICY_ID  
                     AND ACOI.POLICY_VERSION_ID = PCPL.POLICY_VERSION_ID  
        WHERE  
            -- SELECT RECORDS THAT WERE UPDATED DUE TO C(REFUND CHECKS), D(DEPOSIT), F(FEE REVERSAL)  
            ACOI.UPDATED_FROM IN ( 'C', 'D', 'F')  
            -- OR ONLY P(PREMIUM & BILLING FEE) RECORDS WITH A TRANSACTION CODE OF PREMIUM (IGNORE ALL FEE ENTRIES)  
            OR ( ACOI.UPDATED_FROM IN ( 'P' )  
                   AND ACOI.ITEM_TRAN_CODE_TYPE = 'PREM'  
               )  
            -- OR J(JOURNAL ENTRY)  
                  OR ( ACOI.UPDATED_FROM IN ( 'J' )  
                   AND ACOI.ITEM_TRAN_CODE_TYPE = 'JE'  
               )  
      ) AS UCPD  
      GROUP BY  
      UCPD.CUSTOMER_ID, UCPD.POLICY_ID, UCPD.CURRENT_TERM, UCPD.APP_EFFECTIVE_DATE   
) UCP   
WHERE UCP.TOTAL_DUE - UCP.TOTAL_PAID - UCP.SUSPENSE_PREMIUM> 0   
)  
  
SELECT   
CPL.POLICY_NUMBER,  
AUP.TOTAL,             
CASE WHEN CAST(CONVERT(VARCHAR,GETDATE(),101)AS DATETIME) <= AUP.DUE_DATE THEN CAST(AUP.TOTAL AS VARCHAR) ELSE '' END AS 'CURRENT',              
CASE WHEN DATEDIFF(DAY,AUP.DUE_DATE,CAST(CONVERT(VARCHAR,GETDATE(),101)AS DATETIME))>=1 AND DATEDIFF(DAY,AUP.DUE_DATE,CAST(CONVERT(VARCHAR,GETDATE(),101)AS DATETIME))<=30 THEN CAST(AUP.TOTAL AS VARCHAR) ELSE '' END AS '1_TO_30',              
CASE WHEN DATEDIFF(DAY,AUP.DUE_DATE,CAST(CONVERT(VARCHAR,GETDATE(),101)AS DATETIME))>=31 AND DATEDIFF(DAY,AUP.DUE_DATE,CAST(CONVERT(VARCHAR,GETDATE(),101)AS DATETIME))<=60 THEN CAST(AUP.TOTAL AS VARCHAR) ELSE '' END AS '31_TO_60',              
CASE WHEN DATEDIFF(DAY,AUP.DUE_DATE,CAST(CONVERT(VARCHAR,GETDATE(),101)AS DATETIME))>=61 AND DATEDIFF(DAY,AUP.DUE_DATE,CAST(CONVERT(VARCHAR,GETDATE(),101)AS DATETIME))<=90 THEN CAST(AUP.TOTAL AS VARCHAR) ELSE '' END AS '61_TO_90',              
CASE WHEN DATEDIFF(DAY,AUP.DUE_DATE,CAST(CONVERT(VARCHAR,GETDATE(),101)AS DATETIME))>90 THEN CAST(AUP.TOTAL AS VARCHAR) ELSE '' END AS 'More_Than_90'              
FROM #AGING_DB_UNCOLLECTED_PREMIUMS AUP WITH(NOLOCK)  
LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST CPL WITH(NOLOCK)  
ON CPL.CUSTOMER_ID = AUP.CUSTOMER_ID AND CPL.POLICY_ID = AUP.POLICY_ID AND CPL.POLICY_VERSION_ID =   
(SELECT MAX(PL.POLICY_VERSION_ID) FROM POL_CUSTOMER_POLICY_LIST PL WITH(NOLOCK)   
 LEFT OUTER JOIN POL_POLICY_PROCESS PP WITH(NOLOCK)  
 ON PP.CUSTOMER_ID=PL.CUSTOMER_ID AND PP.POLICY_ID=PL.POLICY_ID  
 WHERE PL.CUSTOMER_ID=AUP.CUSTOMER_ID AND PL.POLICY_ID = AUP.POLICY_ID AND PP.PROCESS_STATUS='COMPLETE')    
ORDER BY CPL.POLICY_NUMBER          
              
DROP TABLE #AGING_DB_UNCOLLECTED_PREMIUMS  
END  
  
GO

