IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_VIN_MOTORCYCLE_MASTER_backup_09152009]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_VIN_MOTORCYCLE_MASTER_backup_09152009]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_VIN_MOTORCYCLE_MASTER_backup_09152009](
	[ID] [int] NOT NULL,
	[Manufacturer] [nvarchar] (255) NULL,
	[Model] [nvarchar] (255) NULL,
	[Class] [nvarchar] (255) NULL,
	[Model_Year] [nvarchar] (4) NULL,
	[Model_CC] [int] NULL,
	[EFFECTIVE_FROM_DATE] [datetime] NULL,
	[EFFECTIVE_TO_DATE] [datetime] NULL
) ON [PRIMARY]
GO

