CREATE TABLE [dbo].[CLM_ClaimReserve](
	[ReserveId] [int] IDENTITY(1,1) NOT NULL,
	[ClaimID] [int] NOT NULL,
	[ClaimantID] [int] NOT NULL,
	[ReserveType] [int] NULL,
	[MovementType]  AS (case [ReserveType] when (0) then (0) when (1) then (0) when (2) then (0) when (3) then (0) when (4) then (1) when (5) then (1) when (6) then (3) when (7) then (3) when (8) then (2)  end),
	[PreReserveLocalAmt] [numeric](18, 2) NULL,
	[PreResLocalCurrCode] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PreExRateLocalCurr] [numeric](18, 9) NULL,
	[PreReserveOrgAmt] [numeric](18, 2) NOT NULL,
	[PreResOrgCurrCode] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PreExRateOrgCurr] [numeric](18, 9) NULL,
	[FinalReserveLocalAmt] [numeric](18, 2) NULL,
	[FinalResLocalCurrCode] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FinalExRateLocalCurr] [numeric](18, 9) NULL,
	[FinalReserveOrgAmt] [numeric](18, 2) NOT NULL,
	[FinalResOrgCurrCode] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FinalExRateOrgCurr] [numeric](18, 9) NULL,
	[MoveReserveLocalAmt] [numeric](18, 2) NULL,
	[MoveResLocalCurrCode] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MoveExRateLocalCurr] [numeric](18, 9) NULL,
	[MoveReserveOrgAmt] [numeric](18, 2) NOT NULL,
	[MoveResOrgCurrCode] [nchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MoveExRateOrgCurr] [numeric](18, 9) NULL,
	[isApprove] [int] NOT NULL,
	[ApproveDate] [datetime] NULL,
	[ApproveBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ClaimantType] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_CLM_ClaimReserve] PRIMARY KEY CLUSTERED 
(
	[ReserveId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


