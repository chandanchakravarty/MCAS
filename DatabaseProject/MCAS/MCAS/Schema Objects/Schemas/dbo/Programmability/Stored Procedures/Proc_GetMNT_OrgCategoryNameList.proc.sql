CREATE PROCEDURE [dbo].[Proc_GetMNT_OrgCategoryNameList]
	@OrgCategory [nvarchar](100),
	@OrgCategoryName [nvarchar](100)
WITH EXECUTE AS CALLER
AS
BEGIN  
SET FMTONLY OFF;  
  
--IF @OrgCategoryName=''  AND @OrgCategory=''
	--Begin  
	--	select a.DeductibleId,a.OrgCategoryName,a.OrgCategory,a.DeductibleAmt,a.EffectiveFrom,a.EffectiveTo,b.OrganizationName 
	--	from MNT_Deductible a left join MNT_OrgCountry b on a.OrgCategoryName = b.CountryOrgazinationCode
	--END 
	--else IF @OrgCategoryName!='' AND @OrgCategory !=''
	--Begin  
	--	select a.DeductibleId,a.OrgCategoryName,a.OrgCategory,a.DeductibleAmt,a.EffectiveFrom,a.EffectiveTo,b.OrganizationName 
	--	from MNT_Deductible a left join MNT_OrgCountry b on a.OrgCategoryName = b.CountryOrgazinationCode  
	--	where b.CountryOrgazinationCode = @OrgCategoryName and  a.OrgCategory=@OrgCategory
	--END
	--ELSE  
	--Begin  
	--	select a.DeductibleId,a.OrgCategoryName,a.OrgCategory,a.DeductibleAmt,a.EffectiveFrom,a.EffectiveTo,b.OrganizationName 
	--	from MNT_Deductible a left join MNT_OrgCountry b on a.OrgCategoryName = b.CountryOrgazinationCode  
	--	where b.CountryOrgazinationCode = @OrgCategoryName OR  a.OrgCategory=@OrgCategory
	--END

--SELECT FirstTable.DeductibleId,FirstTable.OrgCategoryName,FirstTable.OrgCategory,FirstTable.DeductibleAmt,FirstTable.EffectiveFrom,  
-- FirstTable.EffectiveTo,FirstTable.OrganizationName   
-- FROM   
-- (select a.DeductibleId,a.OrgCategoryName,a.OrgCategory,a.DeductibleAmt,a.EffectiveFrom,a.EffectiveTo,b.OrganizationName   
--  from MNT_Deductible a   
--  left join MNT_OrgCountry b on a.OrgCategoryName = b.CountryOrgazinationCode   
--  where (ISNULL (@OrgCategory,'')='' OR a.OrgCategory=@OrgCategory)   
--  AND (ISNULL (@OrgCategoryName,'')='' OR b.CountryOrgazinationCode = @OrgCategoryName)) AS FirstTable   
-- INNER JOIN        
-- (SELECT OrgCategoryName,
-- CASE WHEN MAX(CASE WHEN EffectiveTo IS NULL THEN 1 ELSE 0 END) = 0
--        THEN MAX(EffectiveTo)
--   END AS EffectiveTo
-- FROM MNT_Deductible  
-- WHERE (ISNULL (@OrgCategory,'')='' OR OrgCategory=@OrgCategory)   
--  AND (ISNULL (@OrgCategoryName,'')='' OR OrgCategoryName= @OrgCategoryName)  
-- GROUP BY OrgCategoryName) AS SecondTable   
-- ON FirstTable.OrgCategoryName=SecondTable .OrgCategoryName AND (CASE WHEN FirstTable.EffectiveTo IS NULL THEN 1 ELSE FirstTable.EffectiveTo END)  = (CASE WHEN  SecondTable.EffectiveTo IS NULL THEN 1 ELSE SecondTable.EffectiveTo END) 
  
   ;WITH cte AS  
(  
   SELECT DeductibleId,OrgCategoryName,OrgCategory,DeductibleAmt,EffectiveFrom,EffectiveTo,ROW_NUMBER() OVER (PARTITION BY OrgCategory,OrgCategoryName order by OrgCategory,OrgCategoryName,DeductibleId desc) AS rn  
   FROM MNT_Deductible  
) 

SELECT a.DeductibleId,a.OrgCategoryName,a.OrgCategory,a.DeductibleAmt,a.EffectiveFrom,a.EffectiveTo,b.OrganizationName   
FROM cte  a
left join MNT_OrgCountry b on a.OrgCategoryName = b.CountryOrgazinationCode   
WHERE rn = 1 AND (ISNULL (@OrgCategory,'')='' OR a.OrgCategory=@OrgCategory)   
AND (ISNULL (@OrgCategoryName,'')='' OR b.CountryOrgazinationCode = @OrgCategoryName)

END


