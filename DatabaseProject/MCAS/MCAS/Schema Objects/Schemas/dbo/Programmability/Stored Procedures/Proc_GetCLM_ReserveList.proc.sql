CREATE PROCEDURE [dbo].[Proc_GetCLM_ReserveList]    
 @AccidentId [nvarchar](100)      
WITH EXECUTE AS CALLER      
AS      
SET FMTONLY OFF;      
SELECT cl.ClaimRecordNo,cl.ClaimantName,cl.ClaimID,cl.ClaimType,cl.AccidentClaimId,cl.PolicyId,rs.ReserveId,rs.CurrentReserve,MNT_Lookups.Description AS ClaimTypeDesc,MNT_Lookups.Lookupdesc AS ClaimTypeCode ,cl.ClaimantStatus,cl.TPVehicleNo,cl.TPInsurer  
  
FROM CLM_Claims cl  
LEFT JOIN CLM_ReserveSummary RS on cl.AccidentClaimId = rs.AccidentClaimId and cl.ClaimID = rs.ClaimID and rs.ReserveId in (select MAX(ReserveId) from CLM_ReserveSummary a where a.AccidentClaimId = @AccidentId and a.ClaimID = rs.ClaimID)  
LEFT JOIN MNT_Lookups (NOLOCK) ON MNT_Lookups.lookupvalue = cl.ClaimType     
AND MNT_Lookups.Category = 'ClaimType'    
where cl.AccidentClaimId=@AccidentId   
ORDER BY cl.ClaimType   