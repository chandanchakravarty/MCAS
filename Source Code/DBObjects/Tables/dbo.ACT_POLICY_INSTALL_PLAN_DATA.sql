IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACT_POLICY_INSTALL_PLAN_DATA]') AND type in (N'U'))
DROP TABLE [dbo].[ACT_POLICY_INSTALL_PLAN_DATA]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ACT_POLICY_INSTALL_PLAN_DATA](
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [int] NOT NULL,
	[CUSTOMER_ID] [int] NOT NULL,
	[PLAN_ID] [int] NOT NULL,
	[APP_ID] [int] NULL,
	[APP_VERSION_ID] [int] NULL,
	[PLAN_DESCRIPTION] [nvarchar] (35) NULL,
	[PLAN_TYPE] [nvarchar] (10) NULL,
	[NO_OF_PAYMENTS] [smallint] NULL,
	[MONTHS_BETWEEN] [smallint] NULL,
	[PERCENT_BREAKDOWN1] [decimal] (10,4) NULL,
	[PERCENT_BREAKDOWN2] [decimal] (10,4) NULL,
	[PERCENT_BREAKDOWN3] [decimal] (10,4) NULL,
	[PERCENT_BREAKDOWN4] [decimal] (10,4) NULL,
	[PERCENT_BREAKDOWN5] [decimal] (10,4) NULL,
	[PERCENT_BREAKDOWN6] [decimal] (10,4) NULL,
	[PERCENT_BREAKDOWN7] [decimal] (10,4) NULL,
	[PERCENT_BREAKDOWN8] [decimal] (10,4) NULL,
	[PERCENT_BREAKDOWN9] [decimal] (10,4) NULL,
	[PERCENT_BREAKDOWN10] [decimal] (10,4) NULL,
	[PERCENT_BREAKDOWN11] [decimal] (10,4) NULL,
	[PERCENT_BREAKDOWN12] [decimal] (10,4) NULL,
	[MODE_OF_DOWN_PAYMENT] [int] NULL,
	[INSTALLMENTS_IN_DOWN_PAYMENT] [int] NULL,
	[MODE_OF_PAYMENT] [int] NULL,
	[CURRENT_TERM] [smallint] NULL,
	[IS_ACTIVE_PLAN] [char] (1) NULL,
	[TOTAL_PREMIUM] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALL_PLAN_DATA_TOTAL_PREMIUM] DEFAULT ((0)),
	[TOTAL_INTEREST_AMOUNT] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALL_PLAN_DATA_TOTAL_INTEREST_AMOUNT] DEFAULT ((0)),
	[TOTAL_FEES] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALL_PLAN_DATA_TOTAL_FEES] DEFAULT ((0)),
	[TOTAL_TAXES] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALL_PLAN_DATA_TOTAL_TAXES] DEFAULT ((0)),
	[TOTAL_AMOUNT] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALL_PLAN_DATA_TOTAL_AMOUNT] DEFAULT ((0)),
	[TRAN_TYPE] [nvarchar] (10) NOT NULL CONSTRAINT [DF_ACT_POLICY_INSTALL_PLAN_DATA_TRAN_TYPE] DEFAULT (''),
	[TOTAL_TRAN_PREMIUM] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALL_PLAN_DATA_TOTAL_TRAN_PREMIUM] DEFAULT ((0)),
	[TOTAL_TRAN_INTEREST_AMOUNT] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALL_PLAN_DATA_TOTAL_TRAN_INTEREST_AMOUNT] DEFAULT ((0)),
	[TOTAL_TRAN_FEES] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALL_PLAN_DATA_TOTAL_TRAN_FEES] DEFAULT ((0)),
	[TOTAL_TRAN_TAXES] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALL_PLAN_DATA_TOTAL_TRAN_TAXES] DEFAULT ((0)),
	[TOTAL_TRAN_AMOUNT] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALL_PLAN_DATA_TOTAL_TRAN_AMOUNT] DEFAULT ((0)),
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[TOTAL_CHANGE_INFORCE_PRM] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALL_PLAN_DATA_TOTAL_CHANGE_INFORCE_PRM] DEFAULT ((0)),
	[PRM_DIST_TYPE] [int] NULL CONSTRAINT [DF_ACT_POLICY_INSTALL_PLAN_DATA_PRM_DIST_TYPE] DEFAULT ((0)),
	[TOTAL_INFO_PRM] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALL_PLAN_DATA_TOTAL_INFO_PRM] DEFAULT ((0)),
	[TOTAL_STATE_FEES] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALL_PLAN_DATA_TOTAL_STATE_FEES] DEFAULT ((0)),
	[TOTAL_TRAN_STATE_FEES] [decimal] (25,2) NULL CONSTRAINT [DF_ACT_POLICY_INSTALL_PLAN_DATA_TOTAL_TRAN_STATE_FEES] DEFAULT ((0)),
	[CO_APPLICANT_ID] [int] NULL CONSTRAINT [DF_ACT_POLICY_INSTALL_PLAN_DATA_CO_APPLICANT_ID] DEFAULT ((0))
) ON [PRIMARY]
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain  Policy id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain  Policy version id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain  Customer id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Get selected plan id from POL_CUSTOMER_POLICY_LIST' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'PLAN_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain Application id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'APP_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain Application version id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'APP_VERSION_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Get installmant decription from ACT_INSTALL of _PLAN_DATA according to plan id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'PLAN_DESCRIPTION'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'select from ACT_INSTALL_PLAN_DATA according to plan id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'PLAN_TYPE'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'No of installments to be pay' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'NO_OF_PAYMENTS'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Gap between two installments (How many days in between the two installments)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'MONTHS_BETWEEN'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'1st installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN1'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'2nd installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN2'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'3rd installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN3'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'4th installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN4'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'5th installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN5'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'6th installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN6'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'7th installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN7'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'8th installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN8'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'9th installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN9'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'10th installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN10'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'11th installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN11'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'12th installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN12'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain the down payment mode cash or cedit card' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'MODE_OF_DOWN_PAYMENT'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'No of installment in genrated installment in down payment' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'INSTALLMENTS_IN_DOWN_PAYMENT'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Mode of payment through credit or check' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'MODE_OF_PAYMENT'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'get select plan current term from ACT_INSTALL_PLAN_DATA' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'CURRENT_TERM'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'get plan active status from ACT_INSTALL_PLAN_DATA' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'IS_ACTIVE_PLAN'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Total  due policy premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'TOTAL_PREMIUM'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'user enter total interest amount on  payment' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'TOTAL_INTEREST_AMOUNT'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'user enter total fees applicable on policy' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'TOTAL_FEES'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'user enter total txes applicable on policy' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'TOTAL_TAXES'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'sum of total premium+taxes+fees+interest amounts' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'TOTAL_AMOUNT'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'get policy current tran_type from POL_POLICY_PROCESS. NBS or END' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'TRAN_TYPE'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'difference between nbs coverages amount and endorsment coverages premium  at endorcement in progress' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'TOTAL_TRAN_PREMIUM'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'difference between nbs interest amount and endorsment interest  at endorcement in progress' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'TOTAL_TRAN_INTEREST_AMOUNT'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'difference between nbs fees and endorsment fees  at endorcement in progress' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'TOTAL_TRAN_FEES'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'difference between nbs taxes and endorsment taxes  at endorcement in progress' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'TOTAL_TRAN_TAXES'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'TOTAL_TRAN_PREMIUM+TOTAL_TRAN_INTEREST_AMOUNT+TOTAL_TRAN_FEES+TOTAL_TRAN_TAXES' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'TOTAL_TRAN_AMOUNT'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Created User ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'CREATED_BY'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Created modified date and time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'CREATED_DATETIME'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Modified user id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'MODIFIED_BY'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Last modified Date and time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_POLICY_INSTALL_PLAN_DATA', @level2type=N'COLUMN',@level2name=N'LAST_UPDATED_DATETIME'
GO

ALTER TABLE [dbo].[ACT_POLICY_INSTALL_PLAN_DATA] ADD CONSTRAINT [PK_ACT_POLICY_INSTALL_PLAN_DATA_CUSTOMER_ID_POLICY_ID_POLICY_VERSION_ID] PRIMARY KEY
	NONCLUSTERED
	(
		[CUSTOMER_ID] ASC
		,[POLICY_ID] ASC
		,[POLICY_VERSION_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

