CREATE PROCEDURE [dbo].[Proc_GetReserveListAll]
	@ClaimID [int]
WITH EXECUTE AS CALLER
AS
BEGIN
select m1.ReserveId,m1.ReserveType,m1.PreReserveOrgAmt,m1.PreReserveLocalAmt,m1.FinalReserveOrgAmt
,m1.FinalReserveLocalAmt,m1.MoveReserveOrgAmt,m1.MoveReserveLocalAmt,m2.CompanyName,m1.CreatedDate,m1.CreatedBy from dbo.CLM_ClaimReserve m1 LEFT JOIN dbo.CLM_ThirdParty m2 on
m1.ClaimantID=m2.TPartyId where m1.claimId=@ClaimID order by m1.ClaimantID desc,m1.ReserveId desc

END


