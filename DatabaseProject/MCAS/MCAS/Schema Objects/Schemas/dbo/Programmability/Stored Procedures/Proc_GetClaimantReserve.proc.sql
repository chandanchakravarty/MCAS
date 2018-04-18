CREATE PROCEDURE [dbo].[Proc_GetClaimantReserve]
	@ClaimID [int] = null,
	@ClaimentId [int] = null,
	@AccidentClaimId [int] = null
WITH EXECUTE AS CALLER
AS
SELECT TPartyId,
       [ReserveCurr]
      ,[ReserveExRate]
      ,[ReserveAmt]
      ,[ReserveAmount]
  FROM [CDGI].[dbo].[CLM_ThirdParty]
where TPartyId=@ClaimID


