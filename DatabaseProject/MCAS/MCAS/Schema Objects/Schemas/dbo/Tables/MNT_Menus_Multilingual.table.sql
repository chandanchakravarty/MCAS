CREATE TABLE [dbo].[MNT_Menus_Multilingual](
	[MenuId] [int] NOT NULL,
	[LangId] [int] NOT NULL,
	[DisplayTitle] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AdminDisplayText] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ErrorMessDesc] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ErrorMessTitle] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ErrorMessHead] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_Menus_Multilingual_MenuId] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [FK_MNT_Menus_MNT_Menus_Multilingual_MenuId] FOREIGN KEY([MenuId])
REFERENCES [dbo].[MNT_Menus] ([MenuId])
)


