CREATE TABLE [dbo].[PROCESS_TRIGGER](
	[Process_Tigger_Id] [int] IDENTITY(1,1) NOT NULL,
	[Trigger_Id] [int] NOT NULL,
	[Carrier_Id] [varchar](6) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Customer_Id] [int] NULL,
	[Policy_Id] [int] NULL,
	[Policy_Version_Id] [int] NULL,
	[Policy_Process_Id] [int] NULL,
	[User_Id] [int] NULL,
	[Agency_Id] [int] NULL,
	[Vendor_Id] [int] NULL,
	[Claim_Id] [int] NULL,
	[Trigger_Status] [bit] NOT NULL,
	[Generate_Start_DateTime] [datetime] NULL,
	[Generate_End_DateTime] [datetime] NULL,
	[Generate_Complete] [bit] NULL,
	[Print_Complete] [bit] NULL,
	[Email_Complete] [bit] NULL,
	[Fax_Complete] [bit] NULL,
	[Diary_Complete] [bit] NULL,
	[TRIGGER_CREATE_DATE] [datetime] NULL,
	[INSTALLMENT_ID] [int] NULL,
	[Special_Handling_Complete] [smallint] NULL,
	[ADDITIONAL_INTEREST_ID] [int] NULL,
	[POLICY_LOB] [int] NULL,
	[ADDITIONAL_INTEREST_TYPE] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_Process_Trigger] PRIMARY KEY CLUSTERED 
(
	[Process_Tigger_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [FK_PROCESS_TRIGGER_MNT_TRIGGER_MASTER] FOREIGN KEY([Trigger_Id])
REFERENCES [dbo].[MNT_TRIGGER_MASTER] ([Trigger_Id])
)


