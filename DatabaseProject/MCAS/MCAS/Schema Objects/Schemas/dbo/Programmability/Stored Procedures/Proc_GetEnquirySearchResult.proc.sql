CREATE PROCEDURE [dbo].[Proc_GetEnquirySearchResult]
	@AccidentDateFrom [datetime] = null,
	@AccidentDateTo [datetime] = null,
	@ClaimRegDateFrom [datetime] = null,
	@ClaimRegDateTo [datetime] = null,
	@ClaimNo [nvarchar](50) = null,
	@VehicleNo [nvarchar](50) = null,
	@OrgCountries [nvarchar](100) = null,
	@LegalCase [nvarchar](100) = null,
	@ClaimOfficer [nvarchar](100) = null
WITH EXECUTE AS CALLER
AS
BEGIN TRY
	SELECT u.PolicyId,
	   g.AccidentClaimId,
		u.PolicyNo,
		u.ProductId,
		u.CedantId,
		u.SubClassId,
		0 AS PremiumAmount,
		0 AS Deductible ,
		g.DutyIO,
		org.OrganizationName,
		u.PolicyEffectiveTo,
		u.PolicyEffectiveFrom,
		c.ProductCode,
		c.ProductDisplayName,
		isnull(p.ClassDesc ,'') ClassDesc,
		isnull(g.ClaimNo ,'') ClaimNo,
		isnull(g.VehicleNo ,'') VehicleNo,
		d.CedantCode,
		isnull(d.CedantName ,'') CedantName,
		isnull(g.DriverName ,'') DriverName,
		isnull(clm.ClaimantName ,'') ClaimantName,
		isnull(clm.ClaimStatus ,'') ClaimStatus,
		isnull(g.IsComplete ,1) IsComplete,
		isnull(co.UserDispName ,'') ClaimentOfficerName,
		clm.ClaimsOfficer,
		clm.ClaimDate
from ClaimAccidentDetails g 
left outer join MNT_InsruanceM u on u.PolicyId = g.PolicyId
left outer join MNT_Products c on u.ProductId = c.ProductId
left outer join MNT_Cedant d on u.CedantId = d.CedantId
left outer join MNT_ProductClass p on u.SubClassId = p.ID
left outer join CLM_Claims clm on g.AccidentClaimId = clm.AccidentClaimId and DateDiff(d, clm.ClaimDate, isnull(@ClaimRegDateFrom,clm.ClaimDate)) <= 0 and DateDiff(d, clm.ClaimDate, isnull(@ClaimRegDateTo,clm.ClaimDate)) >= 0 
left outer join CLM_ServiceProvider tp on clm.ClaimID = tp.ClaimantNameId and tp.AccidentId = g.AccidentClaimId 
left join MNT_users co on clm.ClaimsOfficer = co.SNo --and isnull(co.UserDispName,'') like ('%Neha%')
left outer join MNT_OrgCountry org on g.Organization = org.Id  
where
DateDiff(d, g.AccidentDate, isnull(@AccidentDateFrom,g.AccidentDate)) <= 0 and DateDiff(d, g.AccidentDate, isnull(@AccidentDateTo,g.AccidentDate)) >= 0 and
isnull(co.UserDispName,'') like (@ClaimOfficer) and 
g.ClaimNo like (@ClaimNo) and
g.VehicleNo like (@VehicleNo) and
org.CountryOrgazinationCode like (@OrgCountries) 
END TRY  
   
 BEGIN CATCH  
  RAISERROR('PROBLEM IN QUERY',16,1)  
 END CATCH


