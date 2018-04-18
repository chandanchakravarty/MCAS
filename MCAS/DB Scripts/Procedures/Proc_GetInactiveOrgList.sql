-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.Proc_GetInactiveOrgList
(
@UserId nvarchar(100)
)
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
GO
