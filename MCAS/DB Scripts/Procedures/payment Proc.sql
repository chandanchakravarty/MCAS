
/****** Object:  StoredProcedure [dbo].[Proc_GetReserveList]    Script Date: 07/15/2014 18:49:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetReserveList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetReserveList]
GO


/****** Object:  StoredProcedure [dbo].[Proc_GetReserveList]    Script Date: 07/15/2014 18:49:19 ******/
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
  ReserveId [int]  NOT NULL,
  ClaimantID [int]  NOT NULL,
  CreatedDate [datetime] NULL,
  ReserveType [int] NULL,
  PreReserveOrgAmt [numeric](18, 2) NOT NULL,
  FinalReserveOrgAmt [numeric](18, 2) NOT NULL,
  MoveReserveOrgAmt [numeric](18, 2) NOT NULL
 )
INSERT INTO @ItemBack1 SELECT m1.ReserveId, m1.ClaimantID, m1.CreatedDate,m1.ReserveType,m1.PreReserveOrgAmt,m1.FinalReserveOrgAmt,m1.MoveReserveOrgAmt FROM CLM_ClaimReserve m1 LEFT JOIN CLM_ClaimReserve m2
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

select m1.ReserveId,m1.ClaimantID,m1.CreatedDate,m1.ReserveType,m1.PreReserveOrgAmt,m1.FinalReserveOrgAmt,m1.MoveReserveOrgAmt,m2.CompanyName 
from @ItemBack1 m1 LEFT JOIN @ItemBack2 m2 on m1.ClaimantID=m2.TPartyId order by m1.ReserveId desc, m1.ClaimantID desc




END





GO

















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
select m1.ReserveId,m1.CreatedDate,m1.ReserveType,m1.PreReserveOrgAmt,m1.PreReserveLocalAmt,m1.FinalReserveOrgAmt
,m1.FinalReserveLocalAmt,m1.MoveReserveOrgAmt,m1.MoveReserveLocalAmt,m2.CompanyName from dbo.CLM_ClaimReserve m1 LEFT JOIN dbo.CLM_ThirdParty m2 on
m1.ClaimantID=m2.TPartyId where m1.claimId=@ClaimID order by m1.ReserveId desc, m1.ClaimantID desc

END

GO






/****** Object:  StoredProcedure [dbo].[Proc_GetPaymentList]    Script Date: 07/15/2014 18:48:45 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPaymentList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPaymentList]
GO


/****** Object:  StoredProcedure [dbo].[Proc_GetPaymentList]    Script Date: 07/15/2014 18:48:45 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






create Proc [dbo].[Proc_GetPaymentList]
(
@ClaimID int
)
as
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










GO









/****** Object:  StoredProcedure [dbo].[Proc_GetPaymentListAll]    Script Date: 07/15/2014 18:47:25 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetPaymentListAll]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetPaymentListAll]
GO


SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






create Proc [dbo].[Proc_GetPaymentListAll]
(
@ClaimID int
)
as
BEGIN
select m1.PaymentId,m2.CompanyName,m1.CreatedDate,m1.ClaimantID,m1.PayeeType,m1.PaymentType,m1.Payee,m1.PaymentDueDate,m1.PayableOrgAmt,m1.PayableLocalAmt,m1.PreReserveOrgAmt,
m1.PreReserveLocalAmt,m1.FinalReserveOrgAmt,m1.FinalReserveLocalAmt from dbo.CLM_ClaimPayment m1 LEFT JOIN dbo.CLM_ThirdParty m2 on
m1.ClaimantID=m2.TPartyId where m1.claimId=@ClaimID order by m1.ClaimantID desc,m1.PaymentId desc

END
GO


