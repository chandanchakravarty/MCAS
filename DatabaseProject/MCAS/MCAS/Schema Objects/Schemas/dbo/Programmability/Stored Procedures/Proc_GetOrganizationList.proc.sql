CREATE PROCEDURE [dbo].[Proc_GetOrganizationList]
	@userid [nvarchar](100)
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;  
BEGIN
select a.OrgName,a.OrgCode,a.UserId,
b.CountryOrgazinationCode,b.OrganizationName,
c.lookupdesc,c.lookupvalue from MNT_Lookups c inner join MNT_UserOrgAccess a
on c.Lookupvalue=a.orgcode join MNT_OrgCountry b
on a.OrgName=b.CountryOrgazinationCode
 where a.UserId=@userid
END


