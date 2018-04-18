IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_EOD_TRANSACTIONS_MASTER]') AND type in (N'U'))
DROP TABLE [dbo].[POL_EOD_TRANSACTIONS_MASTER]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[POL_EOD_TRANSACTIONS_MASTER](
	[ROW_ID] [int] NOT NULL IDENTITY (1,1),
	[TRANSACTION_ID] [int] NOT NULL,
	[DATE_OF_LAUNCH] [datetime] NOT NULL,
	[STATUS] [varchar] (10) NOT NULL
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_EOD_TRANSACTIONS_MASTER] ADD CONSTRAINT [PK_POL_EOD_TRANSACTIONS_MASTER_ROW_ID] PRIMARY KEY
	NONCLUSTERED
	(
		[ROW_ID] ASC
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

ALTER TABLE [dbo].[POL_EOD_TRANSACTIONS_MASTER] ADD CONSTRAINT [CK__POL_EOD_T__STATU__52197331] CHECK 
([STATUS] = 'PENDING' or [STATUS] = 'COMPLETE')
GO

ALTER TABLE [dbo].[POL_EOD_TRANSACTIONS_MASTER] ADD CONSTRAINT [POL_EOD_TRANSACTIONS_MASTER_TRANSACTION_ID] FOREIGN KEY
	(
		[TRANSACTION_ID]
	)
	REFERENCES [dbo].[POL_EOD_TRANSACTIONS]
	(
		[TRANSACTION_ID]
	) 
GO

