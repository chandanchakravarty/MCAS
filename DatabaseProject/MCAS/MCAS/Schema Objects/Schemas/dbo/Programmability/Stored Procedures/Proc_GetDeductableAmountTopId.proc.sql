CREATE PROCEDURE [dbo].[Proc_GetDeductableAmountTopId]
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;  

;WITH cte AS
(
   SELECT DeductibleId,ROW_NUMBER() OVER (PARTITION BY OrgCategory,OrgCategoryName order by OrgCategory,OrgCategoryName,DeductibleId desc) AS rn
   FROM MNT_Deductible
)
SELECT *
FROM cte
WHERE rn = 1


