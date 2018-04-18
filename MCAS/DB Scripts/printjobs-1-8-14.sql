

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_PRINT_JOBS_IsActive]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PRINT_JOBS] DROP CONSTRAINT [DF_PRINT_JOBS_IsActive]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__PRINT_JOB__IS_PR__01D3D6DF]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[PRINT_JOBS] DROP CONSTRAINT [DF__PRINT_JOB__IS_PR__01D3D6DF]
END

GO



/****** Object:  Table [dbo].[PRINT_JOBS]    Script Date: 08/01/2014 10:55:27 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PRINT_JOBS]') AND type in (N'U'))
DROP TABLE [dbo].[PRINT_JOBS]
GO



/****** Object:  Table [dbo].[PRINT_JOBS]    Script Date: 08/01/2014 10:55:27 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[PRINT_JOBS](
	[PRINT_JOBS_ID] [int] IDENTITY(1,1) NOT NULL,
	[CUSTOMER_ID] [int] NULL,
	[POLICY_ID] [int] NULL,
	[POLICY_VERSION_ID] [int] NULL,
	[AGENCY_ID] [int] NULL,
	[CLAIM_ID] [int] NULL,
	[ACTIVITY_ID] [int] NULL,
	[PROCESS_ID] [int] NULL,
	[ENTITY_ID] [int] NULL,
	[ENTITY_TYPE] [nvarchar](20) NULL,
	[DOCUMENT_CODE] [nvarchar](50) NULL,
	[FILE_NAME] [nvarchar](400) NULL,
	[URL_PATH] [nvarchar](200) NULL,
	[ONDEMAND_FLAG] [nchar](1) NULL,
	[DUPLEX] [nchar](1) NULL,
	[IS_ACTIVE] [nchar](1) NULL,
	[IS_PROCESSED] [bit] NULL,
	[GENERATED_FROM] [nvarchar](50) NULL,
	[PICKFROM] [nvarchar](2) NULL,
	[PICKED_BY] [nvarchar](20) NULL,
	[ATTEMPTS] [smallint] NULL,
	[IS_FILE_AVAILABLE] [nvarchar](1) NULL,
	[DOCUMENT_DOWNLODED] [nvarchar](1) NULL,
	[DOCUMENT_DOWNLODED_DATE] [datetime] NULL,
	[PRINT_SUCCESSFUL] [nchar](1) NULL,
	[PRINT_DATETIME] [datetime] NULL,
	[PRINTED_DATETIME] [datetime] NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[PRINT_REQUIRED] [char](1) NULL,
	[PROCESS_TRIGGER_ID] [int] NULL,
	[PAGE_COUNT] [int] NULL,
	[PICKED_DATE] [datetime] NULL,
	[PICKED_FOLDER] [nvarchar](100) NULL,
 CONSTRAINT [PK_PRINT_JOBS_PRINT_JOBS_ID] PRIMARY KEY CLUSTERED 
(
	[PRINT_JOBS_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PRINT_JOBS_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PRINT_JOBS_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PRINT_JOBS_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'AGENCY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'AGENCY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'AGENCY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'CLAIM_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'CLAIM_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'CLAIM_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'ACTIVITY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'ACTIVITY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'ACTIVITY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PROCESS_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PROCESS_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PROCESS_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'ENTITY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'ENTITY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'ENTITY_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'ENTITY_TYPE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'ENTITY_TYPE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'ENTITY_TYPE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'DOCUMENT_CODE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'DOCUMENT_CODE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'DOCUMENT_CODE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'FILE_NAME'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'FILE_NAME'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'FILE_NAME'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'URL_PATH'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'URL_PATH'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'URL_PATH'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'ONDEMAND_FLAG'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'ONDEMAND_FLAG'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'ONDEMAND_FLAG'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'DUPLEX'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'DUPLEX'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'DUPLEX'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'IS_ACTIVE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'IS_ACTIVE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'IS_ACTIVE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'IS_PROCESSED'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'IS_PROCESSED'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'IS_PROCESSED'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'GENERATED_FROM'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'GENERATED_FROM'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'GENERATED_FROM'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PICKFROM'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PICKFROM'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PICKFROM'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PICKED_BY'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PICKED_BY'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PICKED_BY'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'ATTEMPTS'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'ATTEMPTS'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'ATTEMPTS'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'IS_FILE_AVAILABLE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'IS_FILE_AVAILABLE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'IS_FILE_AVAILABLE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'DOCUMENT_DOWNLODED'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'DOCUMENT_DOWNLODED'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'DOCUMENT_DOWNLODED'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'DOCUMENT_DOWNLODED_DATE'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'DOCUMENT_DOWNLODED_DATE'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'DOCUMENT_DOWNLODED_DATE'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PRINT_SUCCESSFUL'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PRINT_SUCCESSFUL'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PRINT_SUCCESSFUL'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PRINT_DATETIME'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PRINT_DATETIME'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PRINT_DATETIME'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PRINTED_DATETIME'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PRINTED_DATETIME'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PRINTED_DATETIME'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'CREATED_BY'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'CREATED_BY'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'CREATED_BY'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'CREATED_DATETIME'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'CREATED_DATETIME'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'CREATED_DATETIME'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'MODIFIED_BY'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'MODIFIED_BY'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'MODIFIED_BY'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'LAST_UPDATED_DATETIME'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'LAST_UPDATED_DATETIME'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'LAST_UPDATED_DATETIME'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'PRINT_REQUIRED' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PRINT_REQUIRED'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PRINT_REQUIRED'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PRINT_REQUIRED'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'PROCESS_TRIGGER_ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PROCESS_TRIGGER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PROCESS_TRIGGER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N' ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PROCESS_TRIGGER_ID'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'CHECK THE PAGE COUNT' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PAGE_COUNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'CHECK THE PAGE COUNT' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PAGE_COUNT'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'PRINT_JOBS', @level2type=N'COLUMN',@level2name=N'PAGE_COUNT'
GO

ALTER TABLE [dbo].[PRINT_JOBS] ADD  CONSTRAINT [DF_PRINT_JOBS_IsActive]  DEFAULT ('Y') FOR [IS_ACTIVE]
GO

ALTER TABLE [dbo].[PRINT_JOBS] ADD  CONSTRAINT [DF__PRINT_JOB__IS_PR__01D3D6DF]  DEFAULT ((0)) FOR [IS_PROCESSED]
GO


