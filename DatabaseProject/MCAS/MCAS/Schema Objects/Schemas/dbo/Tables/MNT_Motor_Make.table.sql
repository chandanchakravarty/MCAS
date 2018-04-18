CREATE TABLE [dbo].[MNT_Motor_Make](
	[TranId] [int] IDENTITY(1,1) NOT NULL,
	[MakeCode] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MakeName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SubClassCode] [varchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[status] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MNT_Motor_Make] PRIMARY KEY CLUSTERED 
(
	[TranId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


