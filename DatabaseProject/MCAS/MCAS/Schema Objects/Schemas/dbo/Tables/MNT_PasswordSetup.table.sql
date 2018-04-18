CREATE TABLE [dbo].[MNT_PasswordSetup](
	[SetupID] [int] IDENTITY(1,1) NOT NULL,
	[EnforcePasswordHistory] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaxPasswordAge] [int] NULL,
	[MinPasswordAge] [int] NULL,
	[MinPasswordLength] [int] NULL,
	[PasswordComplexity] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccLockoutDuration] [int] NULL,
	[AccLockoutThreshold] [int] NULL,
	[ResetAccCounterAfter] [int] NULL,
	[EnforceLogonRestrict] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MaxLifeTimeServiceTicket] [int] NULL,
	[MaxLifeTimeUserTicket] [int] NULL,
	[MaxLifeTimeUserTicketRenewal] [int] NULL,
	[CreatedBy] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedBy] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[SendForgetPwdThroughMail] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_PasswordSetup] PRIMARY KEY CLUSTERED 
(
	[SetupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


