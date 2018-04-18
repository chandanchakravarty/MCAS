CREATE TABLE [dbo].[MNT_GST](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[GSTCode] [varchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GSTType] [nvarchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[GSTDesc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[EffDateFrom] [datetime] NOT NULL,
	[EffDateTo] [datetime] NOT NULL,
	[Rate] [decimal](18, 2) NOT NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MNT_GST] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


