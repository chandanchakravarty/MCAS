IF  EXISTS (SELECT * FROM sys.views WHERE object_id = OBJECT_ID(N'[dbo].[VW_LOSS_ADJUSTMENT_EXPENSE_SCHEDULE_P_SUMMUARY_BY_ASLOB]'))
DROP VIEW [dbo].[VW_LOSS_ADJUSTMENT_EXPENSE_SCHEDULE_P_SUMMUARY_BY_ASLOB]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--drop VIEW VW_LOSS_ADJUSTMENT_EXPENSE_SCHEDULE_P_SUMMUARY_BY_ASLOB    
CREATE  VIEW VW_LOSS_ADJUSTMENT_EXPENSE_SCHEDULE_P_SUMMUARY_BY_ASLOB    
   
AS    
    
SELECT VPL.CLAIM_ID,VPL.LOB_ID,AGENCY_ID,LOB_DESC,
CASE WHEN VPL.LOB_ID IN (6) THEN 
		CASE WHEN (LOSS_TYPE + ',' LIKE '%32,%') OR (LOSS_TYPE + ',' LIKE '%37,%')  
		THEN 'Fire'  
		WHEN (LOSS_TYPE + ',' LIKE '%51,%') OR (LOSS_TYPE + ',' LIKE '%52,%')OR (LOSS_TYPE + ',' LIKE '%53,%') OR (LOSS_TYPE + ',' LIKE '%55,%')                          
		THEN 'Liability'  
		ELSE 'EC'  
		END 
	ELSE T.CATEGORY_DESC
	END
	CATEGORY_DESC,

MONTH_NUMBER,YEAR_NUMBER,YEAR(CLM.LOSS_DATE) INCURRED_YEAR,
SUM(ISNULL(LOSS_ADJUST_EXPENSE,0))AS LOSS_ADJUST_EXPENSE ,SUM(ISNULL(LEGAL_EXPENSE,0)) AS LEGAL_EXPENSE,    
SUM(ISNULL(LESS_SALVAGE_EXPENSE,0)) AS LESS_SALVAGE_EXPENSE,SUM(ISNULL(LESS_RECOVERY_EXPENSE,0)) AS LESS_RECOVERY_EXPENSE,
SUM(ISNULL(REIN_LOSS_ADJUST_EXPENSE,0)) AS REIN_LOSS_ADJUST_EXPENSE,
SUM(ISNULL(REIN_LEGAL_EXPENSE,0)) AS REIN_LEGAL_EXPENSE,
SUM(ISNULL(REINS_SALVAGE_RECOVERY,0)) AS REINS_SALVAGE_RECOVERY,
SUM(ISNULL(OTHER_EXPENSE,0)) AS OTHER_EXPENSE,
SUM(ISNULL(REIN_OTHER_EXPENSE,0))AS REIN_OTHER_EXPENSE,
SUM(ISNULL(REIN_LOSS_RECOVERY,0)) AS REIN_LOSS_RECOVERY,
SUM(ISNULL(END_RESERVE,0)) AS END_RESERVE   , 
SUM(ISNULL(END_RESERVE_REINSURANCE,0)) AS END_RESERVE_REINSURANCE,    
SUM(ISNULL(RESERVE_REINSURANCE_ADJUSTMENT,0)) AS RESERVE_REINSURANCE_ADJUSTMENT    

FROM VW_LOSS_ADJUSTMENT_EXPENSE_SCHEDULE_P_SUMMUARY VPL    WITH(NOLOCK)
INNER JOIN CLM_CLAIM_INFO CLM WITH(NOLOCK) ON CLM.CLAIM_ID=VPL.CLAIM_ID
LEFT OUTER JOIN 
(
SELECT COV_ID,ASLOB FROM MNT_COVERAGE 
UNION
SELECT COV_ID,ASLOB FROM MNT_COVERAGE_EXTRA 
)MC    
ON VPL.COVERAGE_ID = MC.COV_ID 
LEFT OUTER JOIN MNT_LOOKUP_VALUES MLV
ON MC.ASLOB = MLV.LOOKUP_UNIQUE_ID 
INNER JOIN MNT_LOB_DESC T
ON MLV.LOOKUP_UNIQUE_ID = T.ASLOB_ID
GROUP BY VPL.CLAIM_ID,LOSS_TYPE,VPL.LOB_ID,VPL.AGENCY_ID,VPL.LOB_DESC,
--T.CATEGORY_DESC, 
CASE WHEN VPL.LOB_ID IN (6) THEN 
		CASE WHEN (LOSS_TYPE + ',' LIKE '%32,%') OR (LOSS_TYPE + ',' LIKE '%37,%')  
		THEN 'Fire'  
		WHEN (LOSS_TYPE + ',' LIKE '%51,%') OR (LOSS_TYPE + ',' LIKE '%52,%')OR (LOSS_TYPE + ',' LIKE '%53,%') OR (LOSS_TYPE + ',' LIKE '%55,%')                          
		THEN 'Liability'  
		ELSE 'EC'  
		END 
	ELSE T.CATEGORY_DESC
	END,
VPL.MONTH_NUMBER,VPL.YEAR_NUMBER,YEAR(CLM.LOSS_DATE)    








GO

