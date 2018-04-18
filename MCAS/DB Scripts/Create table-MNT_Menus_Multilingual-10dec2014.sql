IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_MNT_Menus_MNT_Menus_Multilingual_MenuId]') AND parent_object_id = OBJECT_ID(N'[dbo].[MNT_Menus_Multilingual]'))
ALTER TABLE [dbo].[MNT_Menus_Multilingual] DROP CONSTRAINT [FK_MNT_Menus_MNT_Menus_Multilingual_MenuId]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_Menus_Multilingual]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_Menus_Multilingual]
GO

CREATE TABLE [dbo].[MNT_Menus_Multilingual](
	[MenuId] [int] NOT NULL,
	[LangId] [int] NOT NULL,
	[DisplayTitle] [varchar](200) NULL,
	[AdminDisplayText] [varchar](200) NULL,
	[ErrorMessDesc] [varchar](1000) NULL,
	[ErrorMessTitle] [varchar](50) NULL,
	[ErrorMessHead] [varchar](30) NULL,
 CONSTRAINT [PK_MNT_Menus_Multilingual_MenuId] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[MNT_Menus_Multilingual]  WITH CHECK ADD  CONSTRAINT [FK_MNT_Menus_MNT_Menus_Multilingual_MenuId] FOREIGN KEY([MenuId])
REFERENCES [dbo].[MNT_Menus] ([MenuId])
GO

ALTER TABLE [dbo].[MNT_Menus_Multilingual] CHECK CONSTRAINT [FK_MNT_Menus_MNT_Menus_Multilingual_MenuId]
GO


