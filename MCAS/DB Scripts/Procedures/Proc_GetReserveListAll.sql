/****** Object:  StoredProcedure [dbo].[Proc_GetReserveListAll]    Script Date: 07/15/2014 18:49:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetReserveListAll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetReserveListAll]
GO


/****** Object:  StoredProcedure [dbo].[Proc_GetReserveListAll]    Script Date: 07/15/2014 18:49:08 ******/
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
select m1.ReserveId,m1.ReserveType,m1.PreReserveOrgAmt,m1.PreReserveLocalAmt,m1.FinalReserveOrgAmt
,m1.FinalReserveLocalAmt,m1.MoveReserveOrgAmt,m1.MoveReserveLocalAmt,m2.CompanyName,m1.CreatedDate,m1.CreatedBy from dbo.CLM_ClaimReserve m1 LEFT JOIN dbo.CLM_ThirdParty m2 on
m1.ClaimantID=m2.TPartyId where m1.claimId=@ClaimID order by m1.ClaimantID desc,m1.ReserveId desc

END

GO
