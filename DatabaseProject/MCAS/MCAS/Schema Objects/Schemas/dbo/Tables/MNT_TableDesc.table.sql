CREATE TABLE [dbo].[MNT_TableDesc](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TableName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[TableDesc] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Type] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL
)


