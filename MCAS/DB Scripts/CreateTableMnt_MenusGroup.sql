IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Mnt_MenusGroup_MNT_Menus]') AND parent_object_id = OBJECT_ID(N'[dbo].[Mnt_MenusGroup]'))
ALTER TABLE [dbo].[Mnt_MenusGroup] DROP CONSTRAINT [FK_Mnt_MenusGroup_MNT_Menus]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Mnt_MenusGroup]') AND type in (N'U'))
DROP TABLE [dbo].[Mnt_MenusGroup]
GO

CREATE TABLE [dbo].[Mnt_MenusGroup](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MenuId] [int] NOT NULL,
	[GroupType] [char](1) NULL,
	[IsActive] [char](1) NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[Mnt_MenusGroup]  WITH CHECK ADD  CONSTRAINT [FK_Mnt_MenusGroup_MNT_Menus] FOREIGN KEY([MenuId])
REFERENCES [dbo].[MNT_Menus] ([MenuId])
GO

ALTER TABLE [dbo].[Mnt_MenusGroup] CHECK CONSTRAINT [FK_Mnt_MenusGroup_MNT_Menus]
GO


