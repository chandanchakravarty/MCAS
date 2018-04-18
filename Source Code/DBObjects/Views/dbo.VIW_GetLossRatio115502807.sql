IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VIW_GetLossRatio115502807]'))
DROP VIEW [dbo].[VIW_GetLossRatio115502807]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW dbo.VIW_GetLossRatio115502807 AS SELECT Agnlst.AGENCY_DISPLAY_NAME, INFO.CLAIMANT_NAME, INFO.POLICY_ID, INFO.CLAIM_NUMBER , INFO.LOSS_DATE, INFO.OUTSTANDING_RESERVE,          
Case when POLMAST.process_desc = 'Endorsement' Then (SELECT effective_dateTIME FROM POL_POLICY_PROCESS WHERE CUSTOMER_ID = INFO.CUSTOMER_ID          
AND POLICY_ID = INFO.POLICY_ID AND POLICY_VERSION_ID = INFO.POLICY_VERSION_ID AND ROW_ID =1)          
ELSE (SELECT app_effective_date FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID = INFO.CUSTOMER_ID AND POLICY_ID = INFO.POLICY_ID AND POLICY_VERSION_ID = INFO.POLICY_VERSION_ID)          
END as 'EFFECTIVE_DATE', Case when POLMAST.process_desc = 'Endorsement' Then (SELECT expiry_date FROM POL_POLICY_PROCESS WHERE CUSTOMER_ID = INFO.CUSTOMER_ID          
AND POLICY_ID = INFO.POLICY_ID AND POLICY_VERSION_ID = INFO.POLICY_VERSION_ID AND ROW_ID =1)          
ELSE (SELECT app_expiration_date FROM POL_CUSTOMER_POLICY_LIST WHERE CUSTOMER_ID = INFO.CUSTOMER_ID AND POLICY_ID = INFO.POLICY_ID AND POLICY_VERSION_ID = INFO.POLICY_VERSION_ID)          
END as 'EXPIRY_DATE' ,ACTDET.PREMIUM_AMOUNT, SUB1.TOTALPAID,INFO.RECOVERIES, LOOKUP.lookup_value_desc          
FROM CLM_CLAIM_INFO INFO           
LEFT OUTER JOIN POL_CUSTOMER_POLICY_LIST POLCUSTLST ON POLCUSTLST.CUSTOMER_ID = INFO.CUSTOMER_ID AND POLCUSTLST.POLICY_ID = INFO.POLICY_ID AND POLCUSTLST.POLICY_VERSION_ID = INFO.POLICY_VERSION_ID
LEFT OUTER JOIN MNT_AGENCY_LIST AGNLST ON POLCUSTLST.AGENCY_ID = AGNLST.AGENCY_ID
INNER JOIN MNT_LOOKUP_VALUES LOOKUP ON INFO.CLAIM_STATUS = LOOKUP.LOOKUP_UNIQUE_ID          
LEFT OUTER JOIN ACT_PREMIUM_PROCESS_DETAILS ACTDET ON INFO.CUSTOMER_ID = ACTDET.CUSTOMER_ID AND  INFO.POLICY_ID = ACTDET.POLICY_ID AND INFO.POLICY_VERSION_ID = ACTDET.POLICY_VERSION_ID          
LEFT OUTER JOIN (SELECT customer_id,policy_id,SUM(total_paid) AS TOTALPAID from ACT_CUSTOMER_OPEN_ITEMS  WHERE UPPER(ISNULL(ITEM_TRAN_CODE_TYPE,'')) <> 'FEES'  GROUP BY customer_id,policy_id) SUB1 ON INFO.customer_id = SUB1.customer_id AND INFO.policy_id = SUB1.policy_id LEFT OUTER JOIN POL_POLICY_PROCESS POLPRO ON INFO.CUSTOMER_ID = POLPRO.CUSTOMER_ID AND INFO.POLICY_ID = POLPRO.POLICY_ID AND INFO.POLICY_VERSION_ID = POLPRO.POLICY_VERSION_ID LEFT OUTER JOIN POL_PROCESS_MASTER POLMAST ON POLPRO.PROCESS_ID = POLMAST.PROCESS_ID WHERE INFO.CREATED_DATETIME >= '01/01/2007' AND INFO.CREATED_DATETIME <= '07/07/2008'
GO

