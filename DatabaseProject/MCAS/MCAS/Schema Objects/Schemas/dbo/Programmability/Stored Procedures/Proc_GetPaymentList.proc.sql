CREATE PROCEDURE [dbo].[Proc_GetPaymentList]
	@ClaimID [int]
WITH EXECUTE AS CALLER
AS
BEGIN
	 DECLARE @ItemBack1 TABLE
(
  CreatedDate [datetime] null,
  PaymentId [int]  NOT NULL,
  ClaimantID [int]  NOT NULL,
  PayeeType [nvarchar](10) NULL,
  PaymentType [nvarchar](10) NULL,
  Payee [nvarchar](50) NOT NULL,
  PaymentDueDate [datetime] NULL,
  PayableOrgAmt [numeric](18, 2) NOT NULL,
  PreReserveOrgAmt [numeric](18, 2) NOT NULL,
  FinalReserveOrgAmt [numeric](18, 2) NOT NULL
 )
INSERT INTO @ItemBack1 SELECT m1.CreatedDate, m1.PaymentId,m1.ClaimantID, m1.PayeeType,m1.PaymentType,m1.Payee,m1.PaymentDueDate,m1.PayableOrgAmt,m1.PreReserveOrgAmt,
m1.FinalReserveOrgAmt 
FROM CLM_ClaimPayment m1 LEFT JOIN CLM_ClaimPayment m2
 ON (m1.ClaimantId = m2.ClaimantId AND m1.PaymentId < m2.PaymentId)
WHERE m2.PaymentId IS NULL AND m1.ClaimID=@ClaimID order by m1.ClaimantId

DECLARE @ItemBack2 TABLE
(
  CompanyName [nvarchar](200) NULL,
  TPartyId [int] NOT NULL)

INSERT INTO @ItemBack2 select CompanyName,TPartyId from dbo.CLM_ThirdParty where ClaimId=@ClaimID and TPartyId in (SELECT m1.ClaimantId
FROM CLM_ClaimPayment m1 LEFT JOIN CLM_ClaimPayment m2
 ON (m1.ClaimantId = m2.ClaimantId AND m1.PaymentId < m2.PaymentId)
WHERE m2.PaymentId IS NULL AND m1.ClaimID=@ClaimID)

select m1.PaymentId,m1.CreatedDate,m1.ClaimantID,m1.PayeeType,m1.PaymentType,m1.Payee,m1.PaymentDueDate,m1.PayableOrgAmt,m1.PreReserveOrgAmt,m1.FinalReserveOrgAmt,m2.CompanyName 
from @ItemBack1 m1 LEFT JOIN @ItemBack2 m2 on m1.ClaimantID=m2.TPartyId order by m1.PaymentId desc,m1.ClaimantID desc




END


