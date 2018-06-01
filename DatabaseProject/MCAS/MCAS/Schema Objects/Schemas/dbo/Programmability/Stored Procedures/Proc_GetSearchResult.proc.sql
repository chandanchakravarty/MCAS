CREATE PROCEDURE [dbo].[Proc_GetSearchResult]    
 @ClaimNo [nvarchar](100) = NULL,    
 @IPNo [nvarchar](100) = NULL,     
@AccidentDate [nvarchar](100) = NULL,     
 @VehicleNo [nvarchar](100) =NULL,      
 @TPSurname [nvarchar](100) = NULL,     
 @VehicleRegnNo [nvarchar](100) = NULL,       
 @Status [nvarchar](2)    
WITH EXECUTE AS CALLER    
AS    
BEGIN    
 -- SET NOCOUNT ON added to prevent extra result sets from    
 -- interfering with SELECT statements.    
 SET NOCOUNT ON;    
    
if @Status = 0    
begin  
SELECT distinct isnull(u.PolicyId,0) as PolicyId,g.AccidentClaimId,u.PolicyNo,u.ProductId, u.CedantId, u.SubClassId,u.PolicyEffectiveTo,u.PolicyEffectiveFrom,c.ProductCode,c.ProductDisplayName,isnull(co.UserDispName,'') as ClaimOfficer,g.DutyIO,isnull(p.ClassDesc,'') as ClassDescription,isnull(g.ClaimNo,'') as ClaimNo,isnull(g.VehicleNo,'') as VehicleNo,d.CedantCode,isnull(org.OrganizationName,'') as CedantName,isnull(g.DriverName,'') as DriverName,isnull(clm.ClaimantName,'') as TPSurname,isnull(g.IsComplete,0) as ClaimStatus,g.AccidentDate,g.IPNo,clm.ClaimDate,clm.VehicleRegnNo,clm.ClaimantStatus,ISNULL(g.IsReported,0) AS IsReported,ISNULL(g.IsReadOnly,0) AS IsReadOnly, ISNULL(g1.ClaimNo,'') AS LinkedClaimNo,clm.ClaimType, org.InsurerType   
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
left join ClaimAccidentDetails g1 on g.LinkedAccidentClaimId =g1.AccidentClaimId      
WHERE (isnull(@AccidentDate,'') = '' or CONVERT(VARCHAR, g.AccidentDate, 103)=@AccidentDate )      
and(@VehicleNo IS NULL OR (g.VehicleNo  like '%'+@VehicleNo+'%'))        
and(@ClaimNo IS NULL OR (g.ClaimNo  like '%'+@ClaimNo+'%'))          
and(@IPNo IS NULL OR (g.IPNo  like '%'+@IPNo+'%'))    
and(@TPSurname IS NULL OR (clm.ClaimantName  like '%'+@TPSurname+'%'))       
and(@VehicleRegnNo IS NULL OR (clm.VehicleRegnNo  like '%'+@VehicleRegnNo+'%'))    
--and g.IsComplete = @Status  
end
else
begin
SELECT distinct isnull(u.PolicyId,0) as PolicyId,g.AccidentClaimId,u.PolicyNo,u.ProductId, u.CedantId, u.SubClassId,u.PolicyEffectiveTo,u.PolicyEffectiveFrom,c.ProductCode,c.ProductDisplayName,isnull(co.UserDispName,'') as ClaimOfficer,g.DutyIO,isnull(p.ClassDesc,'') as ClassDescription,isnull(g.ClaimNo,'') as ClaimNo,isnull(g.VehicleNo,'') as VehicleNo,d.CedantCode,isnull(org.OrganizationName,'') as CedantName,isnull(g.DriverName,'') as DriverName,isnull(clm.ClaimantName,'') as TPSurname,isnull(g.IsComplete,0) as ClaimStatus,g.AccidentDate,g.IPNo,clm.ClaimDate,clm.VehicleRegnNo,clm.ClaimantStatus,ISNULL(g.IsReported,0) AS IsReported,ISNULL(g.IsReadOnly,0) AS IsReadOnly, ISNULL(g1.ClaimNo,'') AS LinkedClaimNo,clm.ClaimType, org.InsurerType   
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
left join ClaimAccidentDetails g1 on g.LinkedAccidentClaimId =g1.AccidentClaimId      
WHERE (isnull(@AccidentDate,'') = '' or CONVERT(VARCHAR, g.AccidentDate, 103)=@AccidentDate )      
and(@VehicleNo IS NULL OR (g.VehicleNo  like '%'+@VehicleNo+'%'))        
and(@ClaimNo IS NULL OR (g.ClaimNo  like '%'+@ClaimNo+'%'))          
and(@IPNo IS NULL OR (g.IPNo  like '%'+@IPNo+'%'))    
and(@TPSurname IS NULL OR (clm.ClaimantName  like '%'+@TPSurname+'%'))       
and(@VehicleRegnNo IS NULL OR (clm.VehicleRegnNo  like '%'+@VehicleRegnNo+'%'))    
--and g.IsComplete = @Status
end
END


