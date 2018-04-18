IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetOrganizationListAccident]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetOrganizationListAccident]
GO

create Proc [dbo].[Proc_GetOrganizationListAccident]
(
@UserId nvarchar(100),
@Caller nvarchar(50) = ''
)
as
SET FMTONLY OFF;
BEGIN
IF(@Caller = 'New')
BEGIN
SELECT Distinct
  [Extent3].Id AS [OrgType],
  [Extent3].[OrganizationName] + '-' + [Extent1].[Lookupdesc] AS [Description]
FROM (SELECT
  [MNT_Lookups].[LookupID] AS [LookupID],
  [MNT_Lookups].[Lookupvalue] AS [Lookupvalue],
  [MNT_Lookups].[Lookupdesc] AS [Lookupdesc],
  [MNT_Lookups].[Description] AS [Description],
  [MNT_Lookups].[Category] AS [Category],
  [MNT_Lookups].[IsActive] AS [IsActive]
FROM [dbo].[MNT_Lookups] AS [MNT_Lookups]) AS [Extent1]
INNER JOIN [dbo].[MNT_UserOrgAccess] AS [Extent2]
  ON [Extent1].[Lookupvalue] = [Extent2].[OrgCode]
INNER JOIN [dbo].[MNT_OrgCountry] AS [Extent3]
  ON [Extent2].[OrgName] = [Extent3].[CountryOrgazinationCode]
WHERE [Extent2].[UserId] LIKE @UserId 
AND GETDATE() BETWEEN [Extent3].EffectiveFrom AND isnull([Extent3].EffectiveTo,'9999-01-01')
END
ELSE
BEGIN
SELECT Distinct
  [Extent3].Id AS [OrgType],
  [Extent3].[OrganizationName] + '-' + [Extent1].[Lookupdesc] AS [Description]
FROM (SELECT
  [MNT_Lookups].[LookupID] AS [LookupID],
  [MNT_Lookups].[Lookupvalue] AS [Lookupvalue],
  [MNT_Lookups].[Lookupdesc] AS [Lookupdesc],
  [MNT_Lookups].[Description] AS [Description],
  [MNT_Lookups].[Category] AS [Category],
  [MNT_Lookups].[IsActive] AS [IsActive]
FROM [dbo].[MNT_Lookups] AS [MNT_Lookups]) AS [Extent1]
INNER JOIN [dbo].[MNT_UserOrgAccess] AS [Extent2]
  ON [Extent1].[Lookupvalue] = [Extent2].[OrgCode]
INNER JOIN [dbo].[MNT_OrgCountry] AS [Extent3]
  ON [Extent2].[OrgName] = [Extent3].[CountryOrgazinationCode]
WHERE [Extent2].[UserId] LIKE @UserId --AND [Extent2].OrgCode in ('BU','TR')
END
END











GO


