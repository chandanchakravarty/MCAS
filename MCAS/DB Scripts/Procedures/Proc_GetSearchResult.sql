/****** Object:  StoredProcedure [dbo].[Proc_GetSearchResult]    Script Date: 04/08/2015 15:42:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetSearchResult]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetSearchResult]
GO

/****** Object:  StoredProcedure [dbo].[Proc_GetSearchResult]    Script Date: 04/08/2015 15:42:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_GetSearchResult]
	@AccidentDate NVarchar(100),
	@VehicleNo Nvarchar(100) = '',
	@Organaization nvarchar(100) = '',
	@Status nvarchar(2)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT isnull(u.PolicyId,0) as PolicyId,g.AccidentClaimId,u.PolicyNo,u.ProductId, u.CedantId, u.SubClassId,u.PolicyEffectiveTo,u.PolicyEffectiveFrom,c.ProductCode,c.ProductDisplayName,isnull(co.UserDispName,'') as ClaimOfficer,g.DutyIO,isnull(p.ClassDesc,'') as ClassDescription,isnull(g.ClaimNo,'') as ClaimNo,isnull(g.VehicleNo,'') as VehicleNo,d.CedantCode,isnull(org.OrganizationName,'') as CedantName,isnull(g.DriverName,'') as DriverName,isnull(clm.ClaimantName,'') as TPSurname,isnull(g.IsComplete,0) as ClaimStatus,g.AccidentDate
FROM ClaimAccidentDetails g 
left join CLM_Claims clm on g.PolicyId = clm.PolicyId and g.AccidentClaimId = clm.AccidentClaimId
left join MNT_InsruanceM u on u.PolicyId = g.PolicyId 
left join MNT_Products c on u.ProductId = c.ProductId
left join MNT_Cedant d on u.CedantId = d.CedantId
left join MNT_ProductClass p on u.SubClassId = p.ID
left join MNT_Users co on co.SNo  = clm.ClaimsOfficer
left join CLM_ServiceProvider sp on sp.AccidentId = g.AccidentClaimId and sp.ClaimantNameId = clm.ClaimID
left join MNT_Adjusters ad on ad.AdjusterId = sp.CompanyNameId and ad.AdjusterTypeCode in ('ADJ','SVY')
left join MNT_OrgCountry org on org.Id = g.Organization
WHERE CONVERT(VARCHAR, g.AccidentDate, 103) LIKE  ('%' + @AccidentDate + '%')
and g.VehicleNo like ('%' + @VehicleNo + '%') 
and org.OrganizationName like ('%' + @Organaization + '%')
and g.IsComplete = @Status
END

GO


