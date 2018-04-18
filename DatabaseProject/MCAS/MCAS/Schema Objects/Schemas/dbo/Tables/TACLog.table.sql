CREATE TABLE [dbo].[TACLog](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PackageID] [uniqueidentifier] NULL,
	[Error] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Source] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PackageName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)


