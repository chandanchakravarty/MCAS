/****** Object:  Table [dbo].[MNT_MASTER_VALUE]    Script Date: 10/22/2011  ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_MASTER_VALUE]') AND type in (N'U'))
DROP TABLE [dbo].MNT_MASTER_VALUE
GO
/****** Object:  Table [dbo].[MNT_MASTER_VALUE]    Script Date: 10/22/2011 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].MNT_MASTER_VALUE
(
[TYPE_UNIQUE_ID]		[INT] NOT NULL,	
[TYPE_ID]               [INT] NULL,
[LOSS_TYPE]				[NVARCHAR](10) NULL,
[CODE]					[NVARCHAR](10) NULL,
[NAME]					[NVARCHAR](100) NULL,
[DESCRIPTION]			[NVARCHAR](100) NULL,
[RECOVERY_TYPE]			[NVARCHAR](10) NULL,
[IS_ACTIVE]				[nchar](2) NULL
CONSTRAINT [PK_MNT_MASTER_VALUE_TYPE_UNIQUE_ID] PRIMARY KEY CLUSTERED
([TYPE_UNIQUE_ID]ASC)	
)

-----------FOR TYPE_UNIQUE_ID------
EXEC   sp_addextendedproperty 'Caption', 'TYPE UNIQUE ID', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',TYPE_UNIQUE_ID
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',TYPE_UNIQUE_ID
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',TYPE_UNIQUE_ID

-----------FOR [TYPE_ID]------
EXEC   sp_addextendedproperty 'Caption', 'TYPE TYPE_ID ID', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',TYPE_ID
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',TYPE_ID
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',TYPE_ID
-----------FOR LOSS_TYPE------
EXEC   sp_addextendedproperty 'Caption', 'LOSS TYPE', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',LOSS_TYPE
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',LOSS_TYPE
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',LOSS_TYPE
-----------FOR CODE------
EXEC   sp_addextendedproperty 'Caption', 'CODE', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',CODE
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',CODE
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',CODE
-----------FOR NAME------
EXEC   sp_addextendedproperty 'Caption', 'NAME', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',NAME
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',NAME
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',NAME
-----------FOR DESCRIPTION------
EXEC   sp_addextendedproperty 'Caption', 'DESCRIPTION', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',DESCRIPTION
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',DESCRIPTION
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',DESCRIPTION
-----------FOR RECOVERY_TYPE------
EXEC   sp_addextendedproperty 'Caption', 'RECOVERY TYPE', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',RECOVERY_TYPE
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',RECOVERY_TYPE
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',RECOVERY_TYPE
---------FOR IS_ACTIVE------
EXEC   sp_addextendedproperty 'Caption', 'IS ACTIVE', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',IS_ACTIVE
EXEC   sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',IS_ACTIVE
EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'MNT_MASTER_VALUE', 'column',IS_ACTIVE

