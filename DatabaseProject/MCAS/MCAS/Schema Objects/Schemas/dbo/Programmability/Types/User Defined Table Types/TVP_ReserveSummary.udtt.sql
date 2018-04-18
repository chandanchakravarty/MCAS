


CREATE TYPE [dbo].[TVP_ReserveSummary] AS TABLE(
	[AccidentClaimId] [int] NOT NULL,
	[ClaimID] [int] NOT NULL,
	[ClaimType] [int] NOT NULL,
	[MovementType] [nvarchar](2) NULL,
	[InitialReserve] [numeric](25, 2) NULL,
	[PreReserve] [numeric](25, 2) NULL,
	[MovementReserve] [numeric](25, 2) NULL,
	[CurrentReserve] [numeric](25, 2) NULL,
	[Createdby] [nvarchar](200) NULL,
	[Createddate] [datetime] NULL,
	[Modifiedby] [nvarchar](200) NULL,
	[Modifieddate] [datetime] NULL,
	[IsActive] [char](1) NULL
)



