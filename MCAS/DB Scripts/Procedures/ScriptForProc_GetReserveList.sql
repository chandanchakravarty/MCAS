/****** Object:  StoredProcedure [dbo].[Proc_GetReserveList]    Script Date: 07/14/2014 14:41:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



create Proc [dbo].[Proc_GetReserveList]
(
@ClaimID int
)
as
BEGIN
	 DECLARE @ItemBack1 TABLE
(
  ClaimantID [int]  NOT NULL,
  CreatedDate [datetime] NULL,
  ReserveType [int] NULL,
  PreReserveOrgAmt [numeric](18, 2) NOT NULL,
  FinalReserveOrgAmt [numeric](18, 2) NOT NULL,
  MoveReserveOrgAmt [numeric](18, 2) NOT NULL
 )
INSERT INTO @ItemBack1 SELECT m1.ClaimantID, m1.CreatedDate,m1.ReserveType,m1.PreReserveOrgAmt,m1.FinalReserveOrgAmt,m1.MoveReserveOrgAmt FROM CLM_ClaimReserve m1 LEFT JOIN CLM_ClaimReserve m2
 ON (m1.ClaimantId = m2.ClaimantId AND m1.ReserveId < m2.ReserveId)
WHERE m2.ReserveId IS NULL AND m1.ClaimID=@ClaimID order by m1.ClaimantId

DECLARE @ItemBack2 TABLE
(
  CompanyName [nvarchar](200) NULL,
  TPartyId [int] NOT NULL)

INSERT INTO @ItemBack2 select CompanyName,TPartyId from dbo.CLM_ThirdParty where ClaimId=@ClaimID and TPartyId in (SELECT m1.ClaimantId
FROM CLM_ClaimReserve m1 LEFT JOIN CLM_ClaimReserve m2
 ON (m1.ClaimantId = m2.ClaimantId AND m1.ReserveId < m2.ReserveId)
WHERE m2.ReserveId IS NULL AND m1.ClaimID=@ClaimID)

select m1.ClaimantID,m1.CreatedDate,m1.ReserveType,m1.PreReserveOrgAmt,m1.FinalReserveOrgAmt,m1.MoveReserveOrgAmt,m2.CompanyName 
from @ItemBack1 m1 LEFT JOIN @ItemBack2 m2 on m1.ClaimantID=m2.TPartyId




END




GO


