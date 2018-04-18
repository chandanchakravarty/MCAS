CREATE TABLE [dbo].[CLM_ReserveDetails](
	[ReserveDetailID] [int] IDENTITY(1,1) NOT NULL,
	[ReserveId] [int] NOT NULL,
	[CmpCode] [nchar](15) NOT NULL,
	[InitialReserve] [numeric](18, 2) NULL,
	[PreReserve] [numeric](18, 2) NULL,
	[MovementReserve] [numeric](18, 2) NULL,
	[CurrentReserve] [numeric](18, 2) NULL,
	[PaymentId] [int] NULL,
	[InitialNoofdays] [nvarchar](10) NULL,
	[MovementNoofdays] [nvarchar](10) NULL,
	[CurrentNoofdays] [nvarchar](10) NULL,
	[InitialRateperday] [nvarchar](10) NULL,
	[MovementlRateperday] [nvarchar](10) NULL,
	[CurrentRateperday] [nvarchar](10) NULL,
	[Createdby] [nvarchar](max) NULL,
	[Createddate] [datetime] NULL,
	[Modifiedby] [nvarchar](max) NULL,
	[Modifieddate] [datetime] NULL,
	[IsActive] [char](1) NULL,
	[AccidentClaimId] [int] NOT NULL,
	[ClaimID] [int] NULL,
	[MovementType] [char](1) NULL,
 CONSTRAINT [PK_CLM_ReserveDetails] PRIMARY KEY CLUSTERED 
(
	[ReserveDetailID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ,
CONSTRAINT [FK_CLM_ReserveDetails_ClaimAccidentDetails] FOREIGN KEY([AccidentClaimId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId]),
CONSTRAINT [FK_CLM_ReserveDetails_CLM_ReserveSummary] FOREIGN KEY([ReserveId])
REFERENCES [dbo].[CLM_ReserveSummary] ([ReserveId])
)


