CREATE TABLE [dbo].[MNT_LookupsMaster](
	[LookUpMasterID] [int] IDENTITY(1,1) NOT NULL,
	[LookupMasterDesc] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LookupCategoryCode] [nvarchar](40) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LookupCategoryDesc] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsCommonMaster] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL
)


