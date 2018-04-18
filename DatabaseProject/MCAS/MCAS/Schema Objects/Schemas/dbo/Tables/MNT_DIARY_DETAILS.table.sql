﻿CREATE TABLE [dbo].[MNT_DIARY_DETAILS](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[MDD_MODULE_ID] [int] NOT NULL,
	[MDD_DIARYTYPE_ID] [int] NOT NULL,
	[MDD_LOB_ID] [int] NOT NULL,
	[MDD_USERGROUP_ID] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MDD_USERLIST_ID] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MDD_FOLLOWUP_DATE] [datetime] NULL,
	[MDD_SUBJECTLINE] [nvarchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MDD_PRIORITY] [nchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MDD_NOTIFICATION_LIST] [numeric](9, 0) NULL,
	[MDD_START_TIME] [datetime] NULL,
	[MDD_END_TIME] [datetime] NULL,
	[MDD_IS_ACTIVE] [nchar](2) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[MDD_CREATED_DATETIME] [datetime] NULL,
	[MDD_CREATED_BY] [int] NULL,
	[MDD_LAST_UPDATED_DATETIME] [datetime] NULL,
	[MDD_MODIFIED_BY] [int] NULL,
	[MDD_FOLLOWUP] [numeric](3, 0) NULL,
 CONSTRAINT [PK_MNT_DIARY_DETAILS] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


