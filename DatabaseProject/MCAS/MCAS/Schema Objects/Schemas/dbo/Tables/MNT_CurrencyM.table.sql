CREATE TABLE [dbo].[MNT_CurrencyM](
	[TranId] [int] IDENTITY(1,1) NOT NULL,
	[CurrencyCode] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CurrencyDispCode] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Description] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UserId] [int] NULL,
	[TranDate] [datetime] NULL,
	[IsLocal] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MNT_CurrencyM] PRIMARY KEY CLUSTERED 
(
	[CurrencyCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


