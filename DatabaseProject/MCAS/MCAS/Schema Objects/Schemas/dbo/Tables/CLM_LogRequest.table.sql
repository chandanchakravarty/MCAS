CREATE TABLE [dbo].[CLM_LogRequest](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[LogRefNo] [nvarchar](15) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccidentClaimId] [int] NULL,
	[ClaimID] [int] NULL,
	[ClaimantNameId] [int] NOT NULL,
	[Hospital_Id] [int] NOT NULL,
	[AssignTo] [int] NOT NULL,
	[LOGAmount] [numeric](10, 2) NOT NULL,
	[LOGDate] [datetime] NULL,
	[CORemarks] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsActive] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Createdby] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[IsVoid] [bit] NOT NULL,
	[MandateId] [int] NULL
	 CONSTRAINT [DF__CLM_LogRe__IsVoi__19AB9A98]  DEFAULT ((0)),
 CONSTRAINT [PK__CLM_LogR__5E54864838652BE2] PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [FK__CLM_LogRe__Accid__3A4D7454] FOREIGN KEY([AccidentClaimId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId]),
 CONSTRAINT [FK__CLM_LogRe__Claim__3B41988D] FOREIGN KEY([ClaimID])
REFERENCES [dbo].[CLM_Claims] ([ClaimID]),
 CONSTRAINT [FK__CLM_LogRe__Hospi__3C35BCC6] FOREIGN KEY([Hospital_Id])
REFERENCES [dbo].[MNT_Hospital] ([Id])
)


