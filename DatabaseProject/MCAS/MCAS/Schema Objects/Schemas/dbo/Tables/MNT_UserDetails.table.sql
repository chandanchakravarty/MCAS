CREATE TABLE [dbo].[MNT_UserDetails](
	[TranId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductCode] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UndSumInsured] [decimal](18, 2) NULL,
	[UndPremium] [decimal](18, 2) NULL,
	[UndRefundPremium] [decimal](18, 2) NULL,
	[ClmReserve] [decimal](18, 2) NULL,
	[ClmReopen] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ClmSettlement] [decimal](18, 2) NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[Period] [int] NULL,
	[UndGrossPremium] [decimal](18, 2) NULL,
	[UndTSI] [decimal](18, 2) NULL
)


