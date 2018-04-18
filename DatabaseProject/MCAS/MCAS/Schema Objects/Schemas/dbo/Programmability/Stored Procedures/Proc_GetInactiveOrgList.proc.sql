CREATE PROCEDURE [dbo].[Proc_GetInactiveOrgList]
	@UserId [nvarchar](100)
WITH EXECUTE AS CALLER
AS
BEGIN
	SET NOCOUNT ON;
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
	AND [Extent3].Id NOT IN (SELECT DISTINCT OC.Id FROM [MNT_UserOrgAccess] UOA
								INNER JOIN MNT_Lookups LU ON UOA.OrgCode = LU.Lookupvalue
								INNER JOIN MNT_OrgCountry OC ON UOA.OrgName = OC.CountryOrgazinationCode
								WHERE UOA.UserId LIKE @UserId
								AND GETDATE() BETWEEN OC.EffectiveFrom AND isnull(OC.EffectiveTo,'9999-01-01'))
END


