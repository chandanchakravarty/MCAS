
GO

/****** Object:  Table [dbo].[MNT_RETENTION_LIMIT]    Script Date: 11/29/2011 17:56:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_RETENTION_LIMIT]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_RETENTION_LIMIT]
GO


GO

/****** Object:  Table [dbo].[MNT_RETENTION_LIMIT]    Script Date: 11/29/2011 17:56:37 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MNT_RETENTION_LIMIT](
	[RETENTION_LIMIT_ID] [int] IDENTITY(1,1) NOT NULL,
	[REF_SUSEP_LOB_ID] [int] NULL,
	[RETENTION_LIMIT] [decimal](25, 2) NULL
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MNT_RETENTION_LIMIT', @level2type=N'COLUMN',@level2name=N'RETENTION_LIMIT_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MNT_RETENTION_LIMIT', @level2type=N'COLUMN',@level2name=N'RETENTION_LIMIT_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MNT_RETENTION_LIMIT', @level2type=N'COLUMN',@level2name=N'RETENTION_LIMIT_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MNT_RETENTION_LIMIT', @level2type=N'COLUMN',@level2name=N'REF_SUSEP_LOB_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MNT_RETENTION_LIMIT', @level2type=N'COLUMN',@level2name=N'REF_SUSEP_LOB_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MNT_RETENTION_LIMIT', @level2type=N'COLUMN',@level2name=N'REF_SUSEP_LOB_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MNT_RETENTION_LIMIT', @level2type=N'COLUMN',@level2name=N'RETENTION_LIMIT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MNT_RETENTION_LIMIT', @level2type=N'COLUMN',@level2name=N'RETENTION_LIMIT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MNT_RETENTION_LIMIT', @level2type=N'COLUMN',@level2name=N'RETENTION_LIMIT'
GO


