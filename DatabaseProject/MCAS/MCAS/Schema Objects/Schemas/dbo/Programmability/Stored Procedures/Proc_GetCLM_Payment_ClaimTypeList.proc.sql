CREATE PROCEDURE [dbo].[Proc_GetCLM_Payment_ClaimTypeList]
	@w_ClaimType [varchar](50),
	@w_AccidentId [varchar](50)
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;
BEGIN
select row_number() over (order by @@rowcount) As SerialNo,row_number() over(partition by a.[ClaimantName] order by a.[ClaimantName]) AS [SNO],a.[ClaimantName],a.[Createdby],a.[Modifiedby],a.[Modifieddate],a.[Createddate],ISNULL(a.[ApprovePayment],'N') as ApprovePayment,isnull(a.[Total_S],'0.00') as Total_S,isnull(a.[Total_D],'0.00') as Total_D,(isnull(a.[Total_S],'0.00') - isnull(a.[Total_D],'0.00'))as Total_O, b.[ClaimRecordNo]
 from CLM_Payment a LEFT JOIN CLM_Claims b ON a.ClaimID=b.ClaimID where a.ClaimType=@w_ClaimType AND a.AccidentClaimId=@w_AccidentId order by a.ClaimantName
END


