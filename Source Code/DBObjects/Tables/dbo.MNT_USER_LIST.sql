IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_USER_LIST]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_USER_LIST]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_USER_LIST](
	[USER_ID] [int] NOT NULL IDENTITY (1,1),
	[USER_LOGIN_ID] [nvarchar] (30) NULL,
	[USER_TYPE_ID] [smallint] NULL,
	[USER_PWD] [nvarchar] (100) NULL,
	[USER_TITLE] [nvarchar] (35) NULL,
	[USER_FNAME] [nvarchar] (65) NULL,
	[USER_LNAME] [nvarchar] (15) NULL,
	[USER_INITIALS] [nvarchar] (3) NULL,
	[USER_ADD1] [nvarchar] (70) NULL,
	[USER_ADD2] [nvarchar] (70) NULL,
	[USER_CITY] [nvarchar] (40) NULL,
	[USER_STATE] [nvarchar] (5) NULL,
	[USER_ZIP] [nvarchar] (11) NULL,
	[USER_PHONE] [nvarchar] (20) NULL,
	[USER_EXT] [nvarchar] (10) NULL,
	[USER_FAX] [nvarchar] (20) NULL,
	[USER_MOBILE] [nvarchar] (20) NULL,
	[USER_EMAIL] [nvarchar] (50) NULL,
	[USER_SPR] [nchar] (1) NULL,
	[USER_MGR_ID] [int] NULL,
	[USER_DEF_DIV_ID] [smallint] NULL,
	[USER_DEF_DEPT_ID] [smallint] NULL,
	[USER_DEF_PC_ID] [smallint] NULL,
	[USER_CHANGE_COMM] [nchar] (1) NULL,
	[USER_VIEW_COMM] [nchar] (1) NULL,
	[USER_BAD_LOGINS] [smallint] NULL,
	[USER_LOCKED] [nchar] (1) NULL,
	[USER_LOCKED_DATETIME] [datetime] NULL,
	[USER_TIME_ZONE] [nvarchar] (5) NULL,
	[USER_SHOW_COMPLETE_TODOLIST] [nchar] (1) NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[USER_INACTIVE_DATETIME] [datetime] NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[USER_SYSTEM_ID] [nvarchar] (8) NULL,
	[USER_IMAGE_FOLDER] [nvarchar] (15) NULL,
	[USER_COLOR_SCHEME] [nvarchar] (15) NULL,
	[GRID_SIZE] [int] NULL CONSTRAINT [DF__MNT_USER___GRID___5F54DBD1] DEFAULT (5),
	[COUNTRY] [nvarchar] (5) NULL,
	[SUB_CODE] [nvarchar] (10) NULL,
	[user_type_code] [nvarchar] (50) NULL,
	[SSN_NO] [nvarchar] (100) NULL,
	[DATE_OF_BIRTH] [datetime] NULL,
	[DRIVER_DRIV_TYPE] [nvarchar] (60) NULL,
	[DATE_EXPIRY] [datetime] NULL,
	[LICENSE_STATUS] [nvarchar] (10) NULL,
	[NON_RESI_LICENSE_STATE] [nvarchar] (5) NULL,
	[NON_RESI_LICENSE_NO] [nvarchar] (60) NULL,
	[LIC_BRICS_USER] [smallint] NULL,
	[NON_RESI_LICENSE_STATE2] [nvarchar] (30) NULL,
	[NON_RESI_LICENSE_NO2] [nvarchar] (30) NULL,
	[User_Notes] [nvarchar] (120) NULL,
	[PINK_SLIP_NOTIFY] [nchar] (1) NULL,
	[ADJUSTER_CODE] [nvarchar] (10) NULL,
	[LOGGED_STATUS] [char] (1) NULL,
	[SESSION_ID] [nvarchar] (200) NULL,
	[CHANGE_PWD_NEXT_LOGIN] [int] NULL,
	[NON_RESI_LICENSE_EXP_DATE] [datetime] NULL,
	[NON_RESI_LICENSE_STATUS] [nvarchar] (20) NULL,
	[NON_RESI_LICENSE_EXP_DATE2] [datetime] NULL,
	[NON_RESI_LICENSE_STATUS2] [nvarchar] (20) NULL,
	[AUTHENTICATION_TOKEN] [nvarchar] (2000) NULL,
	[LAST_VISITED_CUSTOMER] [nvarchar] (200) NULL,
	[LAST_VISITED_APPLICATION] [nvarchar] (200) NULL,
	[LAST_VISITED_POLICY] [nvarchar] (200) NULL,
	[LAST_VISITED_QUOTE] [nvarchar] (200) NULL,
	[LAST_VISITED_CLAIM] [nvarchar] (200) NULL,
	[LANG_ID] [int] NOT NULL CONSTRAINT [DF__MNT_USER___LANG___583040C9] DEFAULT ((1)),
	[CPF] [nvarchar] (40) NULL,
	[REGIONAL_IDENTIFICATION] [nvarchar] (40) NULL,
	[REG_ID_ISSUE_DATE] [datetime] NULL,
	[ACTIVITY] [int] NULL,
	[REG_ID_ISSUE] [nvarchar] (40) NULL,
	[CARRIER_CSR_ID] [int] NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_USER_LIST] ADD CONSTRAINT [PK_MNT_USER_LIST_USER_ID] PRIMARY KEY
	CLUSTERED
	(
		[USER_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[MNT_USER_LIST] ADD CONSTRAINT [FK__MNT_USER___LANG___59246502] FOREIGN KEY
	(
		[LANG_ID]
	)
	REFERENCES [dbo].[MNT_LANGUAGE_MASTER]
	(
		[LANG_ID]
	) 
GO

ALTER TABLE [dbo].[MNT_USER_LIST] WITH NOCHECK ADD CONSTRAINT [FK_MNT_USER_LIST_MNT_USER_TYPES] FOREIGN KEY
	(
		[USER_TYPE_ID]
	)
	REFERENCES [dbo].[MNT_USER_TYPES]
	(
		[USER_TYPE_ID]
	) 
GO

