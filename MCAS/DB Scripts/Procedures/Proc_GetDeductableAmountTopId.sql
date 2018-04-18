IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDeductableAmountTopId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDeductableAmountTopId]
GO

CREATE PROC [dbo].[Proc_GetDeductableAmountTopId] 
AS  
SET FMTONLY OFF;  

;WITH cte AS
(
   SELECT DeductibleId,ROW_NUMBER() OVER (PARTITION BY OrgCategory order by OrgCategory desc,DeductibleId desc) AS rn
   FROM MNT_Deductible
)
SELECT *
FROM cte
WHERE rn = 1

GO



