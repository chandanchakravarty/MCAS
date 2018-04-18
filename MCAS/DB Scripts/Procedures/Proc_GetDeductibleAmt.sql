IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetDeductibleAmt]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetDeductibleAmt]
GO

CREATE PROCEDURE [dbo].[Proc_GetDeductibleAmt](  
@AccidentId int  
)  
AS
SET FMTONLY OFF;  
BEGIN
SET NOCOUNT ON;
IF OBJECT_ID('tempdb..#mytemptable') IS NOT NULL DROP TABLE #mytemptable  
select * into #mytemptable FROM (SELECT Distinct [Extent3].CountryOrgazinationCode as OrgCategoryName,[Extent1].Lookupvalue AS [OrgCategory]  
FROM (SELECT [MNT_Lookups].[Lookupvalue] AS [Lookupvalue] FROM [dbo].[MNT_Lookups] AS [MNT_Lookups]) AS [Extent1]  
INNER JOIN [dbo].[MNT_UserOrgAccess] AS [Extent2] ON [Extent1].[Lookupvalue] = [Extent2].[OrgCode] INNER JOIN [dbo].[MNT_OrgCountry] AS [Extent3] ON [Extent2].[OrgName] = [Extent3].[CountryOrgazinationCode]  where [Extent3].Id=(select Organization from ClaimAccidentDetails where AccidentClaimId=@AccidentId))tbl
  
select Top 1a.DeductibleAmt from MNT_Deductible a inner join #mytemptable b on a.OrgCategoryName=b.OrgCategoryName and a.OrgCategory =b.OrgCategory and GETDATE() between a.EffectiveFrom and a.EffectiveTo  order by a.DeductibleId desc
END

GO


