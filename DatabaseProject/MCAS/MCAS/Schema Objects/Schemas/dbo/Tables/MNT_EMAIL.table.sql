﻿CREATE TABLE [dbo].[MNT_EMAIL](
	[IDEN_ROW_ID] [int] IDENTITY(1,1) NOT NULL,
	[NAME] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[EMAIL] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF__MNT_EMAIL__IsAct__60283922]  DEFAULT ('Y'),
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MNT_EMAIL_IDEN_ROW_ID] PRIMARY KEY NONCLUSTERED 
(
	[IDEN_ROW_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


