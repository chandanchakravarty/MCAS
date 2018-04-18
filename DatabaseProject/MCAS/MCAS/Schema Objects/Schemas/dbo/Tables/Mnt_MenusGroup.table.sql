CREATE TABLE [dbo].[Mnt_MenusGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MenuId] [int] NOT NULL,
	[GroupType] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [FK_Mnt_MenusGroup_MNT_Menus] FOREIGN KEY([MenuId])
REFERENCES [dbo].[MNT_Menus] ([MenuId])
)


