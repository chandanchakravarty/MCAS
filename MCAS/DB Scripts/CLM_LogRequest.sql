IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLM_LogRequest]') AND type in (N'U'))
DROP TABLE [dbo].[CLM_LogRequest]
GO

CREATE TABLE [dbo].[CLM_LogRequest](
	[LogId] [int] IDENTITY(1,1) NOT NULL,
	[LogRefNo] [nvarchar](15) NULL,
	[AccidentClaimId] [int] NULL,
	[ClaimID] [int] NULL,
	[ClaimantNameId] [int] NOT NULL,
	[Hospital_Id] [int] NOT NULL,
	[AssignTo] [int] NOT NULL,
	[LOGAmount] [numeric](10, 2) NOT NULL,
	[LOGDate] [datetime] NULL,
	[CORemarks] [nvarchar](500) NULL,
	[IsActive] [char](1) NULL,
	[Createdby] [nvarchar](30) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](30) NULL,
	[ModifiedDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[LogId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[CLM_LogRequest]  WITH CHECK ADD FOREIGN KEY([AccidentClaimId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId])
GO

ALTER TABLE [dbo].[CLM_LogRequest]  WITH CHECK ADD FOREIGN KEY([ClaimID])
REFERENCES [dbo].[CLM_Claims] ([ClaimID])
GO

ALTER TABLE [dbo].[CLM_LogRequest]  WITH CHECK ADD FOREIGN KEY([Hospital_Id])
REFERENCES [dbo].[MNT_Hospital] ([Id])
GO


