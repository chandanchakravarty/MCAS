CREATE PROCEDURE [dbo].[Proc_GetServiceProverList]
	@AccidentId [nvarchar](100)
WITH EXECUTE AS CALLER
AS
BEGIN
SET FMTONLY OFF;

SELECT * INTO #TMP1 FROM (  select row_number() over(partition by ClaimTypeId order by ServiceProviderId) RecordNumber,a.ClaimRecordNo ,a.ClaimantName,a.TPVehicleNo,  
 case when PartyTypeId = 1 then isnull(c.CedantName,'')  
  when PartyTypeId = 2 then isnull(d.AdjusterName,'')  
  when PartyTypeId = 3 then isnull(d.AdjusterName,'')  
  when PartyTypeId = 4 then isnull(e.CompanyName,'')  
  ELSE '' End   
 as CompanyName,isnull(b.PartyTypeId,0) as PartyTypeId,isnull(a.ClaimType,0) as ClaimTypeId,a.AccidentClaimId,a.PolicyId,isnull(b.ServiceProviderId,0) as ServiceProviderId,isnull(b.ServiceProviderTypeId,0) as ServiceProviderTypeId,  
    CASE     
     WHEN PartyTypeId =  1 THEN 'Insurer'    
     WHEN PartyTypeId =  2 THEN 'Surveyor'    
     WHEN PartyTypeId =  3 THEN 'Lawyer'    
     WHEN PartyTypeId =  4 THEN 'Workshop'    
     ELSE ''    
  END AS PartyName,    
  CASE     
     WHEN ServiceProviderTypeId =  0 THEN '0'    
     ELSE '1'    
  END AS ServiceProviderOption,    
  CASE     
     WHEN ServiceProviderTypeId =  0 THEN 'Own'    
     ELSE 'Third Party'    
  END AS ServiceProviderOption1   
  from CLM_Claims a  
  left outer join CLM_ServiceProvider  b on a.ClaimID = b.ClaimantNameId  
  left outer join MNT_Cedant c on b.CompanyNameId = c.CedantId   
    left outer join MNT_Adjusters d on b.CompanyNameId = d.AdjusterId  
  left outer join MNT_DepotMaster e on b.CompanyNameId = e.DepotId  
  where a.AccidentClaimId=@AccidentId ) TBL1  
  
 SELECT *,lk.Description AS ClaimTypeDesc,lk.Lookupdesc AS ClaimTypeCode FROM #TMP1 ServiceProvider (NOLOCK) LEFT JOIN MNT_Lookups lk (NOLOCK) ON lk.lookupvalue = ServiceProvider.ClaimTypeId AND lk.Category = 'ClaimType' WHERE AccidentClaimId = @AccidentId 
 ORDER BY ClaimTypeId

END


