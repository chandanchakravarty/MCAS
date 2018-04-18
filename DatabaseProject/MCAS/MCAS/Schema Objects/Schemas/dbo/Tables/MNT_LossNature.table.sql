CREATE TABLE [dbo].[MNT_LossNature](
	[TranId] [int] IDENTITY(1,1) NOT NULL,
	[LossType] [varchar](7) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LossNatureCode] [nvarchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LossNatureName] [nvarchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LossNatureDescription] [nvarchar](80) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductCode] [nvarchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[SubClassCode] [nvarchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_LossNature_TranId] PRIMARY KEY CLUSTERED 
(
	[TranId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_MNT_LossNature_LossTypeCode] UNIQUE NONCLUSTERED 
(
	[LossNatureCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


