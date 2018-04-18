CREATE TABLE [dbo].[TClaim_ExpReserve](
	[ClaimRefNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ExpResNo] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ExpMainType] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ExpSubType] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ExpPayeeType] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ExpPayeeCode] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ExpPayeeName] [nvarchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RES_CurrencyCode] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[RES_ReserveFor] [decimal](18, 2) NULL,
	[RES_ReserveLoc] [decimal](18, 2) NULL,
	[RES_ExchangeRate] [decimal](18, 9) NULL,
	[RES_NewLoc] [decimal](18, 2) NULL CONSTRAINT [DF_TClaim_ExpReserve_RES_NewLoc]  DEFAULT ((0)),
	[RES_NewFor] [decimal](18, 2) NULL CONSTRAINT [DF_TClaim_ExpReserve_RES_NewFor]  DEFAULT ((0))
)


