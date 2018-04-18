CREATE TABLE [dbo].[CLM_ClaimPayment](
	[PaymentId] [int] IDENTITY(1,1) NOT NULL,
	[ClaimID] [int] NOT NULL,
	[ClaimantID] [int] NOT NULL,
	[PayeeType] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PaymentType] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PaymentDueDate] [datetime] NULL,
	[Payee] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PayeeAdd] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PaymentCurr] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PayableOrgID] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PayableLocalID] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[PayableOrgAmt] [numeric](18, 2) NOT NULL,
	[PayableLocalAmt] [numeric](18, 2) NOT NULL,
	[PreReserveOrgAmt] [numeric](18, 2) NOT NULL,
	[PreReserveLocalAmt] [numeric](18, 2) NULL,
	[FinalReserveOrgAmt] [numeric](18, 2) NOT NULL,
	[FinalReserveLocalAmt] [numeric](18, 2) NULL,
	[isApprove] [int] NOT NULL,
	[ApproveDate] [datetime] NULL,
	[ApproveBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_CLM_ClaimPayment] PRIMARY KEY CLUSTERED 
(
	[PaymentId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


