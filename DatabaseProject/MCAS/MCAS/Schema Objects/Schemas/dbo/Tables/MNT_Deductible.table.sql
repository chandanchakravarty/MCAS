CREATE TABLE [dbo].[MNT_Deductible](
	[DeductibleId] [int] IDENTITY(1,1) NOT NULL,
	[OrgCategory] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OrgCategoryName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DeductibleAmt] [decimal](18, 2) NULL,
	[EffectiveFrom] [datetime] NULL,
	[EffectiveTo] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_Deductible] PRIMARY KEY CLUSTERED 
(
	[DeductibleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


