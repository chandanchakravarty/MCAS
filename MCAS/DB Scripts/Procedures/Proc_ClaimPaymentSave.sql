
/****** Object:  StoredProcedure [dbo].[Proc_ClaimPaymentSave]    Script Date: 07/16/2014 12:21:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_ClaimPaymentSave]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_ClaimPaymentSave]
GO



/****** Object:  StoredProcedure [dbo].[Proc_ClaimPaymentSave]    Script Date: 07/16/2014 12:21:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Proc_ClaimPaymentSave] @p_ClaimID int,
@p_ClaimantID int,
@p_PayeeType nvarchar(10),
@p_PaymentType nvarchar(10),
@p_PaymentDueDate datetime,
@p_Payee nvarchar(50),
@p_PayeeAdd nvarchar(200),
@p_PaymentCurr nvarchar(10),
@p_PayableOrgID nvarchar(10),
@p_PayableLocalID nvarchar(10),
@p_PayableOrgAmt numeric,
@p_PayableLocalAmt numeric,
@p_PreReserveOrgAmt numeric,
@p_FinalReserveOrgAmt numeric,
@p_isApprove int,
@p_CreatedBy nchar(100),
@p_CreatedDate datetime
AS
BEGIN

  DECLARE @tmp TABLE (
    [PaymentId] [int] IDENTITY (1, 1) NOT NULL,
    [ClaimID] [int] NOT NULL,
    [ClaimantID] [int] NOT NULL,
    [PayeeType] [nvarchar](10) NULL,
    [PaymentType] [nvarchar](10) NULL,
    [PaymentDueDate] [datetime] NULL,
    [Payee] [nvarchar](50) NOT NULL,
    [PayeeAdd] [nvarchar](200) NOT NULL,
    [PaymentCurr] [nvarchar](10) NOT NULL,
    [PayableOrgID] [nvarchar](10) NOT NULL,
    [PayableLocalID] [nvarchar](10) NOT NULL,
    [PayableOrgAmt] [numeric](18, 2) NOT NULL,
    [PayableLocalAmt] [numeric](18, 2) NOT NULL,
    [PreReserveOrgAmt] [numeric](18, 2) NOT NULL,
    [PreReserveLocalAmt] [numeric](18, 2) NULL,
    [FinalReserveOrgAmt] [numeric](18, 2) NOT NULL,
    [FinalReserveLocalAmt] [numeric](18, 2) NULL,
    [isApprove] [int] NOT NULL,
    [ApproveDate] [datetime] NULL,
    [ApproveBy] [nvarchar](25) NULL,
    [CreatedDate] [datetime] NULL,
    [CreatedBy] [nvarchar](25) NULL
  )
  DECLARE @OldData xml
  DECLARE @NewData xml
  DECLARE @Tname nvarchar(50)
  DECLARE @ClaimID int
  DECLARE @ChangedFiled nvarchar(4000)
  SET @Tname = 'CLM_ClaimPayment'
  DECLARE @Nodeval TABLE (
    Nodevalue varchar(30) NULL
  )

  INSERT INTO CLM_ClaimPayment (ClaimID, ClaimantID, PayeeType, PaymentType, PaymentDueDate,
  Payee, PayeeAdd, PaymentCurr, PayableOrgID, PayableLocalID, PayableOrgAmt, PayableLocalAmt, PreReserveOrgAmt, FinalReserveOrgAmt,
  isApprove, CreatedDate, CreatedBy)
    VALUES (@p_ClaimID, @p_ClaimantID, @p_PayeeType, @p_PaymentType, @p_PaymentDueDate, @p_Payee, @p_PayeeAdd, @p_PaymentCurr, 
    @p_PayableOrgID, @p_PayableLocalID, @p_PayableOrgAmt, @p_PayableLocalAmt, @p_FinalReserveOrgAmt,(@p_FinalReserveOrgAmt-@p_PayableLocalAmt) , @p_isApprove, @p_CreatedDate, @p_CreatedBy)

  --inserting entery in reserve table
  SELECT TOP 1
    * INTO #TempTable
  FROM clm_claimReserve
  WHERE ClaimId = @p_ClaimID
  AND ClaimantId = @p_ClaimantID
  ORDER BY ReserveId DESC
  ALTER TABLE #TempTable DROP COLUMN ReserveId, MovementType
  UPDATE #TempTable
  SET FinalReserveOrgAmt = (SELECT TOP 1
        FinalReserveOrgAmt
      FROM clm_claimpayment
      WHERE ClaimId = @p_ClaimID
      AND ClaimantId = @p_ClaimantID
      ORDER BY PaymentId DESC),
      PreReserveOrgAmt = (SELECT TOP 1
        PreReserveOrgAmt
      FROM clm_claimpayment
      WHERE ClaimId = @p_ClaimID
      AND ClaimantId = @p_ClaimantID
      ORDER BY PaymentId DESC),
      MoveReserveOrgAmt = (SELECT TOP 1
        PayableOrgAmt * (-1)
      FROM clm_claimpayment
      WHERE ClaimId = @p_ClaimID
      AND ClaimantId = @p_ClaimantID
      ORDER BY PaymentId DESC),
      CreatedBy = @p_CreatedBy,
      CreatedDate = GETDATE()


  INSERT INTO CLM_ClaimReserve ([ClaimID], [ClaimantID], [ReserveType], [PreReserveLocalAmt], [PreResLocalCurrCode], [PreExRateLocalCurr], [PreReserveOrgAmt], [PreResOrgCurrCode], [PreExRateOrgCurr], [FinalReserveLocalAmt], [FinalResLocalCurrCode], [FinalExRateLocalCurr], [FinalReserveOrgAmt], [FinalResOrgCurrCode], [FinalExRateOrgCurr], [MoveReserveLocalAmt], [MoveResLocalCurrCode], [MoveExRateLocalCurr], [MoveReserveOrgAmt], [MoveResOrgCurrCode], [MoveExRateOrgCurr], [isApprove], [ApproveDate], [ApproveBy], [CreatedDate], [CreatedBy], [ClaimantType])
    SELECT
      *
    FROM #TempTable

  --end inserting entery in reserve table

  INSERT INTO @tmp (ClaimID, ClaimantID, PayeeType, PaymentType, PaymentDueDate,
  Payee, PayeeAdd, PaymentCurr, PayableOrgID, PayableLocalID, PayableOrgAmt, PayableLocalAmt, PreReserveOrgAmt, FinalReserveOrgAmt,
  isApprove, CreatedDate, CreatedBy)
    VALUES (@p_ClaimID, @p_ClaimantID, @p_PayeeType, @p_PaymentType, @p_PaymentDueDate, @p_Payee, @p_PayeeAdd, @p_PaymentCurr, @p_PayableOrgID, @p_PayableLocalID, @p_PayableOrgAmt, @p_PayableLocalAmt, @p_PreReserveOrgAmt, @p_FinalReserveOrgAmt, @p_isApprove, @p_CreatedDate, @p_CreatedBy)
  --select * from @tmp

  SET @NewData = (SELECT
    [PaymentId],
    [ClaimID],
    [ClaimantID],
    [PayeeType],
    [PaymentType],
    [PaymentDueDate],
    [Payee],
    [PayeeAdd],
    [PaymentCurr],
    [PayableOrgID],
    [PayableLocalID],
    [PayableOrgAmt],
    [PayableLocalAmt],
    [PreReserveOrgAmt],
    [PreReserveLocalAmt],
    [FinalReserveOrgAmt],
    [FinalReserveLocalAmt],
    [isApprove],
    [ApproveDate],
    [ApproveBy],
    [CreatedDate],
    [CreatedBy]
  FROM @tmp AS CLM_ClaimPayment
  FOR xml AUTO, ELEMENTS)
  --SELECT @NewData
  --End Selecting New Data as xml after update
  --Select ClamID
  SET @ClaimID = (@p_ClaimID)

  INSERT INTO [MNT_TransactionAuditLog] ([TimeStamp], [TableName], [UserName], [Actions], [OldData], [NewData],
  [ChangedColumns], [TansDescription], [ClaimID])
    VALUES (GETDATE(), 'CLM_ClaimPayment', @p_CreatedBy, 'I', '', @NewData, '', 'Record Inserted ClaimPayment', @ClaimID)

END





GO

