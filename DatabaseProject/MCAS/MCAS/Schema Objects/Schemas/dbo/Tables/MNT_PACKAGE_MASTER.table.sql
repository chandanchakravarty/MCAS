CREATE TABLE [dbo].[MNT_PACKAGE_MASTER](
	[Package_Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Name] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Communication_Mode] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Lob_Id] [int] NULL,
	[State_Id] [int] NULL,
	[View_Type] [int] NULL,
 CONSTRAINT [PK_MNT_PACKAGE_MASTER_1] PRIMARY KEY CLUSTERED 
(
	[Package_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


