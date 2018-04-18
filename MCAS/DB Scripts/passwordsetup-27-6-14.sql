CREATE TABLE [dbo].[MNT_PasswordSetup](
    [SetupID] [int] IDENTITY(1,1) NOT NULL,
	[EnforcePasswordHistory] [varchar](1) NULL,
	[MaxPasswordAge] [int] NULL,
	[MinPasswordAge] [int] NULL,
	[MinPasswordLength] [int] NULL,
	[PasswordComplexity] [varchar](1) NULL,
	[AccLockoutDuration] [int] NULL,
	[AccLockoutThreshold] [int] NULL,
	[ResetAccCounterAfter] [int] NULL,
	[EnforceLogonRestrict] [varchar](1) NULL,
	[MaxLifeTimeServiceTicket] [int] NULL,
	[MaxLifeTimeUserTicket] [int] NULL,
	[MaxLifeTimeUserTicketRenewal] [int] NULL,
	[CreatedBy] [varchar](200) NULL,
	[ModifiedBy] [varchar](200) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MNT_PasswordSetup] PRIMARY KEY CLUSTERED 
(
	[SetupID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


--select * from [MNT_PasswordSetup]