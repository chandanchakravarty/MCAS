CREATE TABLE [dbo].[CLM_ReserveSummary](
	[ReserveId] [int] IDENTITY(1,1) NOT NULL,
	[AccidentClaimId] [int] NOT NULL,
	[ClaimID] [int] NOT NULL,
	[ClaimType] [int] NOT NULL,
	[MovementType] [nvarchar](2) NULL,
	[InitialReserve] [numeric](18, 2) NULL,
	[PreReserve] [numeric](18, 2) NULL,
	[MovementReserve] [numeric](18, 2) NULL,
	[CurrentReserve] [numeric](18, 2) NULL,
	[PaymentId] [int] NULL,
	[Createdby] [nvarchar](max) NULL,
	[Createddate] [datetime] NULL,
	[Modifiedby] [nvarchar](max) NULL,
	[Modifieddate] [datetime] NULL,
	[IsActive] [char](1) NULL,
 CONSTRAINT [PK_CLM_ReserveSummary] PRIMARY KEY CLUSTERED 
(
	[ReserveId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),CONSTRAINT [FK_CLM_ReserveSummary_ClaimAccidentDetails] FOREIGN KEY([AccidentClaimId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId]),
CONSTRAINT [FK_CLM_ReserveSummary_CLM_Claims] FOREIGN KEY([ClaimID])
REFERENCES [dbo].[CLM_Claims] ([ClaimID])
)

