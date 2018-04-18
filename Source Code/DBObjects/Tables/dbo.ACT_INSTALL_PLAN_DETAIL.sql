IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ACT_INSTALL_PLAN_DETAIL]') AND type in (N'U'))
DROP TABLE [dbo].[ACT_INSTALL_PLAN_DETAIL]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[ACT_INSTALL_PLAN_DETAIL](
	[IDEN_PLAN_ID] [int] NOT NULL,
	[PLAN_CODE] [nvarchar] (10) NULL,
	[PLAN_DESCRIPTION] [nvarchar] (35) NULL,
	[PLAN_TYPE] [nvarchar] (10) NULL,
	[NO_OF_PAYMENTS] [smallint] NULL,
	[MONTHS_BETWEEN] [smallint] NULL,
	[PERCENT_BREAKDOWN1] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWN2] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWN3] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWN4] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWN5] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWN6] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWN7] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWN8] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWN9] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWN10] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWN11] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWN12] [decimal] (7,4) NULL,
	[IS_ACTIVE] [nchar] (1) NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[INSTALLMENT_FEES] [decimal] (18,2) NULL,
	[NON_SUFFICIENT_FUND_FEES] [decimal] (18,2) NULL,
	[REINSTATEMENT_FEES] [decimal] (18,2) NULL,
	[MIN_INSTALLMENT_AMOUNT] [decimal] (9,2) NULL,
	[AMTUNDER_PAYMENT] [decimal] (9,2) NULL,
	[MINDAYS_PREMIUM] [smallint] NULL,
	[MINDAYS_CANCEL] [smallint] NULL,
	[POST_PHONE] [smallint] NULL,
	[POST_CANCEL] [smallint] NULL,
	[DEFAULT_PLAN] [bit] NULL,
	[SYSTEM_GENERATED_FULL_PAY] [bit] NULL,
	[PERCENT_BREAKDOWNRP1] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWNRP2] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWNRP3] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWNRP4] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWNRP5] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWNRP6] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWNRP7] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWNRP8] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWNRP9] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWNRP10] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWNRP11] [decimal] (7,4) NULL,
	[PERCENT_BREAKDOWNRP12] [decimal] (7,4) NULL,
	[LATE_FEES] [decimal] (9,2) NULL,
	[SERVICE_CHARGE] [decimal] (9,2) NULL,
	[CONVENIENCE_FEES] [decimal] (9,2) NULL,
	[GRACE_PERIOD] [smallint] NULL,
	[APPLABLE_POLTERM] [int] NULL,
	[PLAN_PAYMENT_MODE] [int] NULL,
	[NO_INS_DOWNPAY] [int] NULL,
	[MODE_OF_DOWNPAY] [int] NULL,
	[NO_INS_DOWNPAY_RENEW] [int] NULL,
	[MODE_OF_DOWNPAY_RENEW] [int] NULL,
	[MODE_OF_DOWNPAY1] [int] NULL,
	[MODE_OF_DOWNPAY2] [int] NULL,
	[MODE_OF_DOWNPAY_RENEW1] [int] NULL,
	[MODE_OF_DOWNPAY_RENEW2] [int] NULL,
	[PAST_DUE_RENEW] [numeric] (18,0) NULL,
	[PRO_STATMNT_BEFORE_DAYS] [int] NULL,
	[DAYS_DUE_PREM_NOTICE_PRINTD] [int] NULL,
	[DAYS_SUBSEQUENT_INSTALLMENTS] [smallint] NULL,
	[SUBSEQUENT_INSTALLMENTS_OPTION] [nvarchar] (10) NULL,
	[INTREST_RATES] [decimal] (7,4) NULL,
	[BASE_DATE_DOWNPAYMENT] [int] NULL,
	[DUE_DAYS_DOWNPYMT] [smallint] NULL,
	[BDATE_INSTALL_NXT_DOWNPYMT] [int] NULL,
	[DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT] [smallint] NULL,
	[DOWN_PAYMENT_PLAN] [smallint] NULL,
	[DOWN_PAYMENT_PLAN_RENEWAL] [smallint] NULL,
	[APP_LOB] [int] NULL,
	[RECVE_NOTIFICATION_DOC] [int] NULL
) ON [PRIMARY]
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Installment plan id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'IDEN_PLAN_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Installment Plan code' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PLAN_CODE'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain the details description of installmet plan' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PLAN_DESCRIPTION'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain the type of billing installment plan' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PLAN_TYPE'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'No of installments to be pay' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'NO_OF_PAYMENTS'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Gap between two installments (How many days in between the two installments)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'MONTHS_BETWEEN'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'1st installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN1'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'2nd installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN2'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'3rd installment percentage against total premium1st installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN3'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'4th installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN4'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'5th installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN5'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'6th installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN6'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'7th installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN7'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'8th installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN8'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'9th installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN9'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'10th installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN10'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'11th installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN11'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'12th installment percentage against total premium' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWN12'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'If it is Active then it contain Y else N' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'IS_ACTIVE'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Created User Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'CREATED_BY'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Created date and time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'CREATED_DATETIME'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Modified user id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'MODIFIED_BY'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Last Updated date and time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'LAST_UPDATED_DATETIME'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains the installment fee' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'INSTALLMENT_FEES'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Non Sufficient Fund Fees' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'NON_SUFFICIENT_FUND_FEES'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Reinstatement Fees' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'REINSTATEMENT_FEES'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains the Minimum Amount for Billing Plan' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'MIN_INSTALLMENT_AMOUNT'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Percentage of underpayment allowed on billing installment' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'AMTUNDER_PAYMENT'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain the defauld plan' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'DEFAULT_PLAN'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'1st installment percentage against total premium for the renewal process' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWNRP1'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'2nd installment percentage against total premium for the renewal process' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWNRP2'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'3rd installment percentage against total premium for the renewal process' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWNRP3'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'4th installment percentage against total premium for the renewal process' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWNRP4'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'5th installment percentage against total premium for the renewal process' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWNRP5'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'6th installment percentage against total premium for the renewal process' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWNRP6'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'7th installment percentage against total premium for the renewal process' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWNRP7'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'8th installment percentage against total premium for the renewal process' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWNRP8'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'9th installment percentage against total premium for the renewal process' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWNRP9'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'10th installment percentage against total premium for the renewal process' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWNRP10'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'11th installment percentage against total premium for the renewal process' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWNRP11'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'12th installment percentage against total premium for the renewal process' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PERCENT_BREAKDOWNRP12'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains Late Fees' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'LATE_FEES'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain the No of Grace Period Cancellation Notice' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'GRACE_PERIOD'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'PLAN_PAYMENT_MODE'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Mode of Down Payment' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'MODE_OF_DOWNPAY'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Mode of Down Payment for renewal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'MODE_OF_DOWNPAY_RENEW'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains the Intrest Rate' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'INTREST_RATES'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Base Date Down Payment (Application Submission,Policy Effective, Policy Insurance)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'BASE_DATE_DOWNPAYMENT'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains the Number of due days for Down Payment' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'DUE_DAYS_DOWNPYMT'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Base Date For installments next to Down Payment (Application Submission,Policy Effective, Policy Insurance)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'BDATE_INSTALL_NXT_DOWNPYMT'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Number of Due Days for installment next to Down Payment' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'DAYS_LEFT_DUEPYMT_NXT_DOWNPYMNT'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains the 0 (zero) means no downpayment , and 1 for downpayment at the time of first premium installment' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'DOWN_PAYMENT_PLAN'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contains the 0 (zero) means no downpayment , and 1 for downpayment at the time of first premium installment renewal' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'DOWN_PAYMENT_PLAN_RENEWAL'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'It contains Product Id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'APP_LOB'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'When to generate receivable notification document:( Contain The Lookup value)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ACT_INSTALL_PLAN_DETAIL', @level2type=N'COLUMN',@level2name=N'RECVE_NOTIFICATION_DOC'
GO

ALTER TABLE [dbo].[ACT_INSTALL_PLAN_DETAIL] ADD CONSTRAINT [PK_ACT_INSTALL_PLAN_DETAIL_IDEN_PLAN_ID] PRIMARY KEY
	CLUSTERED
	(
		[IDEN_PLAN_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

