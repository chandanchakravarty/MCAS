USE [CDGI]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_FAL_FAL]') AND parent_object_id = OBJECT_ID(N'[dbo].[MNT_FAL]'))
ALTER TABLE [dbo].[MNT_FAL] DROP CONSTRAINT [FK_FAL_FAL]
GO
/****** Object:  Table [dbo].[MNT_FAL]    Script Date: 03/18/2015 15:22:05 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_FAL]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_FAL]
GO


/****** Object:  Table [dbo].[MNT_FAL]    Script Date: 03/18/2015 15:22:05 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MNT_FAL](
	[FALId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NULL,
	[FALLevelName] [nvarchar](250) NULL,
	[FALAccessCategory] [nvarchar](500) NULL,
	[Amount] [numeric](18, 2) NULL,
 CONSTRAINT [PK_FAL] PRIMARY KEY CLUSTERED 
(
	[FALId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[MNT_FAL]  WITH CHECK ADD  CONSTRAINT [FK_FAL_FAL] FOREIGN KEY([UserId])
REFERENCES [dbo].[MNT_Users] ([SNo])
GO

ALTER TABLE [dbo].[MNT_FAL] CHECK CONSTRAINT [FK_FAL_FAL]
GO


