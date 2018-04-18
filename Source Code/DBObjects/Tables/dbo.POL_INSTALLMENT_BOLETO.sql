IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[POL_INSTALLMENT_BOLETO]') AND type in (N'U'))
DROP TABLE [dbo].[POL_INSTALLMENT_BOLETO]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[POL_INSTALLMENT_BOLETO](
	[BOLETO_ID] [int] NOT NULL IDENTITY (1,1),
	[CUSTOMER_ID] [int] NOT NULL,
	[POLICY_ID] [int] NOT NULL,
	[POLICY_VERSION_ID] [int] NOT NULL,
	[INSTALLEMT_ID] [int] NOT NULL,
	[INSTALLMENT_NO] [int] NOT NULL,
	[BANK_ID] [smallint] NOT NULL,
	[BOLETO_HTML] [text] NULL,
	[CREATED_BY] [int] NULL,
	[CREATED_DATETIME] [datetime] NOT NULL,
	[MODIFIED_BY] [int] NULL,
	[LAST_UPDATED_DATETIME] [datetime] NULL,
	[IS_ACTIVE] [char] (1) NULL CONSTRAINT [DF_POL_INSTALLMENT_BOLETO_IS_ACTIVE] DEFAULT ('Y'),
	[OUR_NUMBER] [nvarchar] (20) NULL,
	[BOLETO_BARCODE_NUMBER] [nvarchar] (50) NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'PK ( Unique id for the Boleto printing)' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_INSTALLMENT_BOLETO', @level2type=N'COLUMN',@level2name=N'BOLETO_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Customer ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_INSTALLMENT_BOLETO', @level2type=N'COLUMN',@level2name=N'CUSTOMER_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Policy id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_INSTALLMENT_BOLETO', @level2type=N'COLUMN',@level2name=N'POLICY_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Policy Version id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_INSTALLMENT_BOLETO', @level2type=N'COLUMN',@level2name=N'POLICY_VERSION_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'This is the row_id of ACT_POLICY_INSTALLMENT_DETAILS table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_INSTALLMENT_BOLETO', @level2type=N'COLUMN',@level2name=N'INSTALLEMT_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain the Insallment No' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_INSTALLMENT_BOLETO', @level2type=N'COLUMN',@level2name=N'INSTALLMENT_NO'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain the Bank Id ,which refer to the bank information table' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_INSTALLMENT_BOLETO', @level2type=N'COLUMN',@level2name=N'BANK_ID'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain the Boleto HTMl while printing the boleto' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_INSTALLMENT_BOLETO', @level2type=N'COLUMN',@level2name=N'BOLETO_HTML'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Contain  the users id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_INSTALLMENT_BOLETO', @level2type=N'COLUMN',@level2name=N'CREATED_BY'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'row created datetime' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_INSTALLMENT_BOLETO', @level2type=N'COLUMN',@level2name=N'CREATED_DATETIME'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Modified users id' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_INSTALLMENT_BOLETO', @level2type=N'COLUMN',@level2name=N'MODIFIED_BY'
GO
EXEC sp_addextendedproperty @name=N'MS_Description', @value=N'Last Modified date and time' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'POL_INSTALLMENT_BOLETO', @level2type=N'COLUMN',@level2name=N'LAST_UPDATED_DATETIME'
GO

ALTER TABLE [dbo].[POL_INSTALLMENT_BOLETO] ADD CONSTRAINT [PK_POL_INSTALLMENT_BOLETO_ID] PRIMARY KEY
	NONCLUSTERED
	(
		[BOLETO_ID] ASC
	)	WITH
	(
		PAD_INDEX = OFF
		,STATISTICS_NORECOMPUTE = OFF
		,IGNORE_DUP_KEY = OFF
		,ALLOW_ROW_LOCKS = ON
		,ALLOW_PAGE_LOCKS = ON
	) ON [PRIMARY]
GO

ALTER TABLE [dbo].[POL_INSTALLMENT_BOLETO] WITH NOCHECK ADD CONSTRAINT [FK_POL_INSTALLMENT_BOLETO_CUSTOMER_ID_POLICY_ID_POLICY_VERSION_ID_INSTALLEMT_ID] FOREIGN KEY
	(
		[CUSTOMER_ID],
		[POLICY_ID],
		[POLICY_VERSION_ID],
		[INSTALLEMT_ID]
	)
	REFERENCES [dbo].[ACT_POLICY_INSTALLMENT_DETAILS]
	(
		[CUSTOMER_ID],
		[POLICY_ID],
		[POLICY_VERSION_ID],
		[ROW_ID]
	) 
GO

