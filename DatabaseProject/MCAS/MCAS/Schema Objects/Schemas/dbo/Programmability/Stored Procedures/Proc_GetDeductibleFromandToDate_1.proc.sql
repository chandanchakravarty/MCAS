--Updated for payment issue

CREATE PROCEDURE [dbo].[Proc_GetDeductibleFromandToDate]
	@AccidentId [int]    
WITH EXECUTE AS CALLER    
AS    
SET FMTONLY OFF;      
BEGIN    
SET NOCOUNT ON;    
IF OBJECT_ID('tempdb..#mytemptable') IS NOT NULL DROP TABLE #mytemptable      
select * into #mytemptable FROM (SELECT Distinct [Extent3].CountryOrgazinationCode as OrgCategoryName,[Extent1].Lookupvalue AS [OrgCategory],  
cad.AccidentDate,[Extent3].OrganizationName  as OrganizationName     
FROM (SELECT [MNT_Lookups].[Lookupvalue] AS [Lookupvalue] FROM [dbo].[MNT_Lookups] AS [MNT_Lookups]) AS [Extent1]      
INNER JOIN [dbo].[MNT_UserOrgAccess] AS [Extent2] ON [Extent1].[Lookupvalue] = [Extent2].[OrgCode] INNER JOIN [dbo].[MNT_OrgCountry] AS [Extent3] ON [Extent2].[OrgName] = [Extent3].[CountryOrgazinationCode]    
INNER JOIN ClaimAccidentDetails cad on [Extent3].Id = cad.Organization  
where [Extent3].Id=(select Organization from ClaimAccidentDetails where AccidentClaimId=@AccidentId and cad.AccidentClaimId = @AccidentId))tbl    
      
select Top 1 a.EffectiveFrom,a.EffectiveTo,b.OrganizationName from MNT_Deductible a inner join #mytemptable b on a.OrgCategoryName=b.OrgCategoryName and a.OrgCategory =b.OrgCategory    
order by a.DeductibleId desc    
END