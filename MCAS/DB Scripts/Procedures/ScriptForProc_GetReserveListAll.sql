/****** Object:  StoredProcedure [dbo].[Proc_GetReserveListAll]    Script Date: 07/14/2014 14:41:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





create Proc [dbo].[Proc_GetReserveListAll]
(
@ClaimID int
)
as
BEGIN
select m1.CreatedDate,m1.ReserveType,m1.PreReserveOrgAmt,m1.PreReserveLocalAmt,m1.FinalReserveOrgAmt
,m1.FinalReserveLocalAmt,m1.MoveReserveOrgAmt,m1.MoveReserveLocalAmt,m2.CompanyName from dbo.CLM_ClaimReserve m1 LEFT JOIN dbo.CLM_ThirdParty m2 on
m1.ClaimantID=m2.TPartyId where m1.claimId=@ClaimID order by m1.ClaimantID

END






GO


