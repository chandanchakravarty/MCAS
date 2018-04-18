CREATE TABLE [dbo].[MNT_TransactionAuditLog](
	[TranAuditId] [int] IDENTITY(1,1) NOT NULL,
	[TimeStamp] [datetime] NULL,
	[TableName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UserName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Actions] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OldData] [xml] NULL,
	[NewData] [xml] NULL,
	[ChangedColumns] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[TansDescription] [nvarchar](400) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ClaimID] [int] NULL,
	[EntityCode] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EntityType] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EntityTypeId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccidentId] [int] NULL,
	[IsValidXml][bit] NULL DEFAULT(0),
	[CustomInfo] [nvarchar](250) NULL,
 CONSTRAINT [PK_MNT_TransactionAuditLog] PRIMARY KEY CLUSTERED 
(
	[TranAuditId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


