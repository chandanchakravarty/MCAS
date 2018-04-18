﻿CREATE TABLE [dbo].[PRINT_JOBS](
	[PRINT_JOBS_ID] [int] IDENTITY(1,1) NOT NULL,
	[CUSTOMER_ID] [int] NULL,
	[POLICY_ID] [int] NULL,
	[POLICY_VERSION_ID] [int] NULL,
	[AGENCY_ID] [int] NULL,
	[CLAIM_ID] [int] NULL,
	[ACTIVITY_ID] [int] NULL,
	[PROCESS_ID] [int] NULL,
	[ENTITY_ID] [int] NULL,
	[ENTITY_TYPE] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DOCUMENT_CODE] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FILE_NAME] [nvarchar](400) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[URL_PATH] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ONDEMAND_FLAG] [nchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DUPLEX] [nchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IS_ACTIVE] [nchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF_PRINT_JOBS_IsActive]  DEFAULT ('Y'),
	[IS_PROCESSED] [bit] NULL CONSTRAINT [DF__PRINT_JOB__IS_PR__01D3D6DF]  DEFAULT ((0)),
	[GENERATED_FROM] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PICKFROM] [nvarchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PICKED_BY] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ATTEMPTS] [smallint] NULL,
	[IS_FILE_AVAILABLE] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DOCUMENT_DOWNLODED] [nvarchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DOCUMENT_DOWNLODED_DATE] [datetime] NULL,
	[PRINT_SUCCESSFUL] [nchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PRINT_DATETIME] [datetime] NULL,
	[PRINTED_DATETIME] [datetime] NULL,
	[CreatedBy] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[PRINT_REQUIRED] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PROCESS_TRIGGER_ID] [int] NULL,
	[PAGE_COUNT] [int] NULL,
	[PICKED_DATE] [datetime] NULL,
	[PICKED_FOLDER] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ClaimNo] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_PRINT_JOBS_PRINT_JOBS_ID] PRIMARY KEY CLUSTERED 
(
	[PRINT_JOBS_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


