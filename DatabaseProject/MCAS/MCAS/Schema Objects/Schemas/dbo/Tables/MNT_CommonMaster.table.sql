CREATE TABLE [dbo].[MNT_CommonMaster](
	[MasterId] [int] IDENTITY(1,1) NOT NULL,
	[MasterDescriptionId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ValueDescription] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ValueCode] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Status] [nvarchar](5) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL
)


