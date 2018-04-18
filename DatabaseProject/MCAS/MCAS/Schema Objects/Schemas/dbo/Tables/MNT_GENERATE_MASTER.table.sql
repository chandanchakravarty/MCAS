CREATE TABLE [dbo].[MNT_GENERATE_MASTER](
	[Generate_Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Trigger_Id] [int] NOT NULL,
	[Package_Id] [int] NULL,
	[Template_Id] [int] NULL,
	[Entity_ID] [int] NULL,
 CONSTRAINT [PK_Generate_Master] PRIMARY KEY CLUSTERED 
(
	[Generate_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


