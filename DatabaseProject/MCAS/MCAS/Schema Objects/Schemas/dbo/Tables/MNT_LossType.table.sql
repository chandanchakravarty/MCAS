CREATE TABLE [dbo].[MNT_LossType](
	[TranId] [int] IDENTITY(1,1) NOT NULL,
	[LossTypeCode] [varchar](7) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LossTypeName] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LossTypeDescription] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Dept] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductCode] [nvarchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SubClassCode] [nvarchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_LossType_TranId] PRIMARY KEY CLUSTERED 
(
	[TranId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_MNT_LossType_LossTypeCode] UNIQUE NONCLUSTERED 
(
	[LossTypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


