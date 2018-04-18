CREATE TABLE [dbo].[MNT_RiskType](
	[RiskTypeCode] [int] IDENTITY(1,1) NOT NULL,
	[RiskTypeDesc] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MainClass] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MNT_RiskType] PRIMARY KEY CLUSTERED 
(
	[RiskTypeCode] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


