CREATE TABLE [dbo].[MNT_TRIGGER_MASTER](
	[Trigger_Id] [int] IDENTITY(1,1) NOT NULL,
	[Code] [nvarchar](120) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Description] [nvarchar](300) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Generate] [bit] NOT NULL,
	[Print] [bit] NOT NULL,
	[Email] [bit] NOT NULL,
	[Fax] [bit] NOT NULL,
	[Diary] [bit] NOT NULL,
	[Is_Active] [bit] NOT NULL,
	[TRIGGER_GROUP] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Special_Handling_Required] [smallint] NULL,
	[VIEW_TYPE] [smallint] NULL,
	[Transaction_Description] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Trigger_Master] PRIMARY KEY CLUSTERED 
(
	[Trigger_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


