IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLM_ACTIVITY_RECOVERY]') AND type in (N'U'))
DROP TABLE [dbo].[CLM_ACTIVITY_RECOVERY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[CLM_ACTIVITY_RECOVERY](
	[CLAIM_ID] [int] NOT NULL,
	[RECOVERY_ID] [int] NOT NULL,
	[RESERVE_ID] [int] NOT NULL,
	[ACTIVITY_ID] [int] NOT NULL,
	[ACTION_ON_RECOVERY] [int] NOT NULL,
	[IS_ACTIVE] [char] (1) NOT NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[VEHICLE_ID] [int] NULL,
	[AMOUNT] [decimal] (18,2) NOT NULL CONSTRAINT [DF_CLM_ACTIVITY_RECOVERY_AMOUNT] DEFAULT ((0)),
	[DRACCTS] [int] NULL,
	[CRACCTS] [int] NULL,
	[PAYMENT_METHOD] [smallint] NULL,
	[CHECK_NUMBER] [nvarchar] (40) NULL,
	[ACTUAL_RISK_ID] [int] NULL,
	[ACTUAL_RISK_TYPE] [nvarchar] (10) NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CLM_ACTIVITY_RECOVERY] ADD CONSTRAINT [PK_CLM_ACTIVITY_RECOVERY_CLAIM_ID_RECOVERY_ID] PRIMARY KEY
	NONCLUSTERED
	(
		[CLAIM_ID] ASC
		,[RECOVERY_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
		,FILLFACTOR = 90
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[CLM_ACTIVITY_RECOVERY] ADD CONSTRAINT [FK_CLM_ACTIVITY_RECOVERY_CLAIM_ID] FOREIGN KEY
	(
		[CLAIM_ID]
	)
	REFERENCES [dbo].[CLM_CLAIM_INFO]
	(
		[CLAIM_ID]
	) 
GO

ALTER TABLE [dbo].[CLM_ACTIVITY_RECOVERY] ADD CONSTRAINT [FK_CLM_ACTIVITY_RECOVERY_CLAIM_ID_RESERVE_ID] FOREIGN KEY
	(
		[CLAIM_ID],
		[RESERVE_ID]
	)
	REFERENCES [dbo].[CLM_ACTIVITY_RESERVE]
	(
		[CLAIM_ID],
		[RESERVE_ID]
	) 
GO

