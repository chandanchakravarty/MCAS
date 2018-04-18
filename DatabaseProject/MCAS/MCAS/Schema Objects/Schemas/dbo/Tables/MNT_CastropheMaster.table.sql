CREATE TABLE [dbo].[MNT_CastropheMaster](
	[CastropheId] [int] IDENTITY(1,1) NOT NULL,
	[TranId] [int] NULL,
	[CastropheCode] [nvarchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[CastropheDesc] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductCode] [varchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MNT_CastropheMaster_CastropheId] PRIMARY KEY CLUSTERED 
(
	[CastropheId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_MNT_CastropheMaster_CastropheCode] UNIQUE NONCLUSTERED 
(
	[CastropheCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


