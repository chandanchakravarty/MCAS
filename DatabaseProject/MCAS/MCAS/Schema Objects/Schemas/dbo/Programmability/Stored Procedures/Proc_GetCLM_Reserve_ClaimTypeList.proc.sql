CREATE PROCEDURE [dbo].[Proc_GetCLM_Reserve_ClaimTypeList]    
 @w_ClaimType [varchar](50),    
 @w_AccidentId [varchar](50)    
WITH EXECUTE AS CALLER    
AS    
SET FMTONLY OFF;    
BEGIN    
select row_number() over (order by @@rowcount) As SerialNo,row_number() over(partition by b.[ClaimantName] order by b.[ClaimantName]) AS [SNO],a.[MovementType] as [MovementType], b.ClaimantName As ClaimantName, a.[Createdby],b.ClaimType as ClaimType, a.[ReserveId] as ReserveId, a.[Modifiedby],a.[Modifieddate],a.[Createddate],a.InitialReserve as [Total_I],a.MovementReserve as [Total_R],a.CurrentReserve as [Total_O],b.[ClaimRecordNo]    
from CLM_ReserveSummary a   
LEFT JOIN   
CLM_Claims b   
ON a.ClaimID=b.ClaimID where a.ClaimID=@w_ClaimType AND a.AccidentClaimId=@w_AccidentId    
   
END 



GO


