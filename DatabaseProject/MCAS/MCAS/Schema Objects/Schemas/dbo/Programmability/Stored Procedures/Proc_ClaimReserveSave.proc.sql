CREATE PROCEDURE [dbo].[Proc_ClaimReserveSave]
	@p_ClaimID [int],
	@p_ClaimantID [int],
	@p_ReserveType [int],
	@p_PreReserveLocalAmt [numeric](18, 0),
	@p_PreExRateLocalCurr [numeric](18, 0),
	@p_PreReserveOrgAmt [numeric](18, 0),
	@p_PreResOrgCurrCode [nchar](10),
	@p_PreExRateOrgCurr [numeric](18, 0),
	@p_FinalReserveLocalAmt [numeric](18, 0),
	@p_FinalResLocalCurrCode [nchar](10),
	@p_FinalReserveOrgAmt [numeric](18, 0),
	@p_FinalResOrgCurrCode [nchar](10),
	@p_MoveReserveLocalAmt [numeric](18, 0),
	@p_MoveResLocalCurrCode [nchar](10),
	@p_MoveReserveOrgAmt [numeric](18, 0),
	@p_MoveResOrgCurrCode [nchar](10),
	@p_CreatedDate [datetime],
	@p_CreatedBy [nchar](100),
	@p_isApprove [int]
WITH EXECUTE AS CALLER
AS
BEGIN
declare @tmp table(
	[ReserveId] [int] IDENTITY(1,1) NOT NULL,
	[ClaimID] [int] NOT NULL,
	[ClaimantID] [int] NOT NULL,
	[ReserveType] [int] NULL,
	[MovementType]  AS (case [ReserveType] when (0) then (0) when (1) then (0) when (2) then (0) when (3) then (0) when (4) then (1) when (5) then (1) when (6) then (3) when (7) then (3) when (8) then (2)  end),
	[PreReserveLocalAmt] [numeric](18, 2) NULL,
	[PreResLocalCurrCode] [nchar](10) NULL,
	[PreExRateLocalCurr] [numeric](18, 9) NULL,
	[PreReserveOrgAmt] [numeric](18, 2) NOT NULL,
	[PreResOrgCurrCode] [nchar](10) NULL,
	[PreExRateOrgCurr] [numeric](18, 9) NULL,
	[FinalReserveLocalAmt] [numeric](18, 2) NULL,
	[FinalResLocalCurrCode] [nchar](10) NULL,
	[FinalExRateLocalCurr] [numeric](18, 9) NULL,
	[FinalReserveOrgAmt] [numeric](18, 2) NOT NULL,
	[FinalResOrgCurrCode] [nchar](10) NULL,
	[FinalExRateOrgCurr] [numeric](18, 9) NULL,
	[MoveReserveLocalAmt] [numeric](18, 2) NULL,
	[MoveResLocalCurrCode] [nchar](10) NULL,
	[MoveExRateLocalCurr] [numeric](18, 9) NULL,
	[MoveReserveOrgAmt] [numeric](18, 2) NOT NULL,
	[MoveResOrgCurrCode] [nchar](10) NULL,
	[MoveExRateOrgCurr] [numeric](18, 9) NULL,
	[isApprove] [int] NOT NULL,
	[ApproveDate] [datetime] NULL,
	[ApproveBy] [nvarchar](25) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](25) NULL,
	[ClaimantType] [varchar](20) NULL
	) 
	DECLARE @OldData xml
    DECLARE @NewData xml
    DECLARE @Tname nvarchar(50)
    DECLARE @ClaimID int
    DECLARE @ChangedFiled nvarchar(4000)
    SET @Tname = 'CLM_ClaimReserve'
    DECLARE @Nodeval TABLE (
    Nodevalue varchar(30) NULL
  )
    --select * from @tmp
    
    
	Insert into CLM_ClaimReserve (ClaimID,ClaimantID,ReserveType,PreReserveLocalAmt,PreExRateLocalCurr,PreReserveOrgAmt,PreResOrgCurrCode,PreExRateOrgCurr,FinalReserveLocalAmt,
	FinalResLocalCurrCode,FinalReserveOrgAmt,FinalResOrgCurrCode,MoveReserveLocalAmt,MoveResLocalCurrCode,
	MoveReserveOrgAmt,MoveResOrgCurrCode,CreatedDate,CreatedBy,isApprove)
	values(@p_ClaimID,@p_ClaimantID,@p_ReserveType,@p_PreReserveLocalAmt,@p_PreExRateLocalCurr,@p_PreReserveOrgAmt,@p_PreResOrgCurrCode,@p_PreExRateOrgCurr,@p_FinalReserveLocalAmt
	,@p_FinalResLocalCurrCode,@p_FinalReserveOrgAmt,@p_FinalResOrgCurrCode,@p_MoveReserveLocalAmt,@p_MoveResLocalCurrCode,@p_MoveReserveOrgAmt,@p_MoveResOrgCurrCode,@p_CreatedDate,@p_CreatedBy,@p_isApprove)
	
	
	Insert into @tmp (ClaimID,ClaimantID,ReserveType,PreReserveLocalAmt,PreExRateLocalCurr,PreReserveOrgAmt,PreResOrgCurrCode,PreExRateOrgCurr,FinalReserveLocalAmt,
	FinalResLocalCurrCode,FinalReserveOrgAmt,FinalResOrgCurrCode,MoveReserveLocalAmt,MoveResLocalCurrCode,
	MoveReserveOrgAmt,MoveResOrgCurrCode,CreatedDate,CreatedBy,isApprove)
	values(@p_ClaimID,@p_ClaimantID,@p_ReserveType,@p_PreReserveLocalAmt,@p_PreExRateLocalCurr,@p_PreReserveOrgAmt,@p_PreResOrgCurrCode,@p_PreExRateOrgCurr,@p_FinalReserveLocalAmt
	,@p_FinalResLocalCurrCode,@p_FinalReserveOrgAmt,@p_FinalResOrgCurrCode,@p_MoveReserveLocalAmt,@p_MoveResLocalCurrCode,@p_MoveReserveOrgAmt,@p_MoveResOrgCurrCode,@p_CreatedDate,@p_CreatedBy,@p_isApprove)

--select * from @tmp



  SET @NewData = (SELECT
      ClaimID,ClaimantID,ReserveType,PreReserveLocalAmt,PreExRateLocalCurr,PreReserveOrgAmt,PreResOrgCurrCode,PreExRateOrgCurr,FinalReserveLocalAmt,
	FinalResLocalCurrCode,FinalReserveOrgAmt,FinalResOrgCurrCode,MoveReserveLocalAmt,MoveResLocalCurrCode,
	MoveReserveOrgAmt,MoveResOrgCurrCode,CreatedDate,CreatedBy,isApprove
    FROM @tmp AS CLM_ClaimReserve FOR xml AUTO, ELEMENTS)
    --SELECT @NewData
    --End Selecting New Data as xml after update
    --Select ClamID
      set @ClaimID =(@p_ClaimID)

INSERT INTO [MNT_TransactionAuditLog] ([TimeStamp], [TableName], [UserName], [Actions], [OldData], [NewData],
      [ChangedColumns], [TansDescription], [ClaimID])
        VALUES (GETDATE(), 'CLM_ClaimReserve', @p_CreatedBy, 'I', '', @NewData, '', 'Record Inserted ClaimReserve', @ClaimID)

DECLARE @EXR decimal,@EXAMT decimal

select @EXR= ExchangeRate from dbo.MNT_CurrencyTxn where CurrencyCode= @p_MoveResOrgCurrCode

SELECT @EXAMT = ExpensesAmt FROM [CLM_ThirdParty] WHERE TPartyId=@p_ClaimantID and ClaimId=@p_ClaimID


UPDATE [CLM_ThirdParty] SET [ReserveCurr] = @p_MoveResOrgCurrCode,[ReserveExRate] = @EXR
,[ReserveAmt] = @p_MoveReserveOrgAmt,[ModifiedBy] = @p_CreatedBy,[ModifiedDate] = getdate(),[TotalReserve] = @p_MoveReserveOrgAmt+@EXAMT WHERE TPartyId=@p_ClaimantID and ClaimId=@p_ClaimID
  
  
  END


