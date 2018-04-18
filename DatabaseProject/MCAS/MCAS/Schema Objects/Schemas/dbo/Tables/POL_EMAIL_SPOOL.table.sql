CREATE TABLE [dbo].[POL_EMAIL_SPOOL](
	[INDEN_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[EMAIL_FROM] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EMAIL_TO] [nvarchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EMAIL_TEXT] [nvarchar](max) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SENT_STATUS] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF__POL_EMAIL__SENT___0CFADF99]  DEFAULT ('N'),
	[SENT_TIME] [datetime] NULL,
	[REMARK] [nvarchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ERROR_DESCRIPTION] [text] COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF__POL_EMAIL__IsAct__0DEF03D2]  DEFAULT ('Y'),
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[EmailSubject] [nvarchar](400) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EMAIL_CC] [nvarchar](2000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SourceId] [int] NULL,
	[SourceType] [nvarchar](50)  NULL,
	[EmailType] [nvarchar](50)  NULL,
 CONSTRAINT [PK_POL_EMAIL_SPOOL] PRIMARY KEY CLUSTERED 
(
	[INDEN_ROW_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


