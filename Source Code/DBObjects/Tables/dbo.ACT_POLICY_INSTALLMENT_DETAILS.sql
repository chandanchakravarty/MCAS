IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACT_POLICY_INSTALLMENT_DETAILS]') AND type in (N'U'))
DROP TABLE [dbo].[ACT_POLICY_INSTALLMENT_DETAILS]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ACT_POLICY_INSTALLMENT_DETAILS](
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [int] NOT NULL,
	[CUSTOMER_ID] [int] NOT NULL,
	[APP_ID] [int] NOT NULL,
	[APP_VERSION_ID] [int] NOT NULL,
	[INSTALLMENT_AMOUNT] [decimal] (25,2) NULL,
	[INSTALLMENT_EFFECTIVE_DATE] [datetime] NULL,
	[RELEASED_STATUS] [char] (1) NULL,
	[ROW_ID] [int] NOT NULL IDENTITY (1,1),
	[INSTALLMENT_NO] [int] NULL,
	[RISK_ID] [int] NULL,
	[RISK_TYPE] [nvarchar] (15) NULL,
	[PAYMENT_MODE] [int] NULL,
	[CURRENT_TERM] [smallint] NULL,
	[PERCENTAG_OF_PREMIUM] [decimal] (9,4) NULL,
	[INTEREST_AMOUNT] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALLMENT_DETAILS_INTEREST_AMOUNT] DEFAULT ((0)),
	[FEE] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALLMENT_DETAILS_FEE] DEFAULT ((0)),
	[TAXES] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALLMENT_DETAILS_TAXES] DEFAULT ((0)),
	[TOTAL] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALLMENT_DETAILS_TOTAL] DEFAULT ((0)),
	[TRAN_INTEREST_AMOUNT] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALLMENT_DETAILS_TRAN_INTEREST_AMOUNT] DEFAULT ((0)),
	[TRAN_FEE] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALLMENT_DETAILS_TRAN_FEE] DEFAULT ((0)),
	[TRAN_TAXES] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALLMENT_DETAILS_TRAN_TAXES] DEFAULT ((0)),
	[TRAN_TOTAL] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALLMENT_DETAILS_TRAN_TOTAL] DEFAULT ((0)),
	[BOLETO_NO] [nvarchar] (100) NULL,
	[IS_BOLETO_GENRATED] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[TRAN_PREMIUM_AMOUNT] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALLMENT_DETAILS_TRAN_PREMIUM_AMOUNT] DEFAULT ((0)),
	[CO_APPLICANT_ID] [int] NULL CONSTRAINT [DF_ACT_POLICY_INSTALLMENT_DETAILS_CO_APPLICANT_ID] DEFAULT ((0)),
	[PAID_AMOUNT] [decimal] (18,2) NOT NULL CONSTRAINT [DF_ACT_POLICY_INSTALLMENT_DETAILS_PAID_AMOUNT] DEFAULT ((0)),
	[RECEIVED_AMOUNT] [decimal] (25,8) NULL,
	[RECEIVED_DATE] [datetime] NULL,
	[INSTALLMENT_EXPIRE_DATE] [datetime] NULL,
	[ACC_CO_DISCOUNT] [decimal] (9,2) NULL,
	[IS_COMMISSION_PROCESS] [varchar] (2) NULL,
	[IS_PAID_TO_PAGNET] [varchar] (2) NULL,
	[PAGNET_DATE] [datetime] NULL
) ON [PRIMARY]
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain  Policy id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain  Policy version id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain Customer id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain Application id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'APP_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Application version id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'APP_VERSION_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain Installment Amount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'INSTALLMENT_AMOUNT'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain Installment Effective date' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'INSTALLMENT_EFFECTIVE_DATE'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'when payament is completed then it is Y else N' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'RELEASED_STATUS'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Auto id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'ROW_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'It contain the installment no of that policy' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'INSTALLMENT_NO'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain risk id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'RISK_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'?' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'RISK_TYPE'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'contain the according to the selected billing plan' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'PAYMENT_MODE'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'contan the default (M )' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'CURRENT_TERM'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'percentag of premium against total policy premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'PERCENTAG_OF_PREMIUM'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'User will enter interest amount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'INTEREST_AMOUNT'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'The amount for the interest being charged on this installment' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'FEE'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'User can enter the total policy premium for the policy period' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'TAXES'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'User enters  the total interest amount for the policy period' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'TOTAL'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Transaction Amount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'TRAN_INTEREST_AMOUNT'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Calculated transaction fee' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'TRAN_FEE'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Calculated transaction taxs' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'TRAN_TAXES'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Calculated transaction total' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'TRAN_TOTAL'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain the Boleto no' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'BOLETO_NO'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'it Contain 1 when the boleto is generated otherwise 0 (Zero)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'IS_BOLETO_GENRATED'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Created User ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'CREATED_BY'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Created User datetime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'CREATED_DATETIME'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Modified User ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'MODIFIED_BY'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Last Modified User id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALLMENT_DETAILS', @level2type=N'COLUMN',@level2name=N'LAST_UPDATED_DATETIME'
GO

ALTER TABLE [dbo].[ACT_POLICY_INSTALLMENT_DETAILS] ADD CONSTRAINT [PK_ACT_POLICY_INSTALLMENT_DETAILS_CUSTOMER_ID_POLICY_ID_POLICY_VERSION_ID_ROW_ID] PRIMARY KEY
	NONCLUSTERED
	(
		[CUSTOMER_ID] ASC
		,[POLICY_ID] ASC
		,[POLICY_VERSION_ID] ASC
		,[ROW_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[ACT_POLICY_INSTALLMENT_DETAILS] WITH NOCHECK ADD CONSTRAINT [FK_ACT_POLICY_INSTALLMENT_DETAILS_CUSTOMER_ID_POLICY_ID_POLICY_VERSION_ID] FOREIGN KEY
	(
		[CUSTOMER_ID],
		[POLICY_ID],
		[POLICY_VERSION_ID]
	)
	REFERENCES [dbo].[ACT_POLICY_INSTALL_PLAN_DATA]
	(
		[CUSTOMER_ID],
		[POLICY_ID],
		[POLICY_VERSION_ID]
	) 
GO

