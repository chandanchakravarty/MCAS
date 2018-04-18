CREATE TABLE [dbo].[MNT_FAL](
	[FALId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[FALLevelName] [nvarchar](250) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FALAccessCategory] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Amount] [numeric](18, 2) NULL,
	[UnlimitedAmt] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL CONSTRAINT [DF__MNT_FAL__Unlimit__573EB8BD]  DEFAULT ('N'),
	[CreatedBy] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_FAL] PRIMARY KEY CLUSTERED 
(
	[FALId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [FK_FAL_FAL] FOREIGN KEY([UserId])
REFERENCES [dbo].[MNT_Users] ([SNo])
)


