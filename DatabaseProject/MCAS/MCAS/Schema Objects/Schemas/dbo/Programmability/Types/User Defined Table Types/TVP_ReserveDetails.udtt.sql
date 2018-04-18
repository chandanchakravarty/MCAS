
CREATE TYPE [dbo].[TVP_ReserveDetails] AS TABLE(
	[ReserveId] [int] NOT NULL,
	[CmpCode] [nchar](15) NOT NULL,
	[InitialReserve] [numeric](25, 2) NULL,
	[PreReserve] [numeric](25, 2) NULL,
	[MovementReserve] [numeric](25, 2) NULL,
	[CurrentReserve] [numeric](25, 2) NULL,
	[InitialNoofdays] [nvarchar](10) NULL,
	[MovementNoofdays] [nvarchar](10) NULL,
	[CurrentNoofdays] [nvarchar](10) NULL,
	[InitialRateperday] [nvarchar](10) NULL,
	[MovementlRateperday] [nvarchar](10) NULL,
	[CurrentRateperday] [nvarchar](10) NULL,
	[Createdby] [nvarchar](200) NULL,
	[Createddate] [datetime] NULL,
	[IsActive] [char](1) NULL,
	[AccidentClaimId] [int] NOT NULL,
	[ClaimID] [int] NULL,
	[MovementType] [char](1) NULL
)



