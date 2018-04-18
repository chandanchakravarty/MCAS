CREATE TABLE [dbo].[MNT_Users](
	[SNo] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[LoginPassword] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GroupId] [int] NULL,
	[UserFullName] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UserDispName] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DeptCode] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[BranchCode] [nvarchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[PaymentLimit] [decimal](18, 4) NULL,
	[CreditNoteLimit] [decimal](18, 4) NULL,
	[IsActive] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsEnabled] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UserTypeCode] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AccessLevel] [tinyint] NULL,
	[FirstTime] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF__TM_Users__FirstT__02FC7413]  DEFAULT ('Y'),
	[MainClass] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[EmailId] [nvarchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[OrgCategory] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FAL_OD] [int] NULL,
	[FAL_PDBI] [int] NULL,
	[DID_No] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[FAX_No] [varchar](20) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SessionId] [nvarchar](200) NULL,
	[LOGApproverCheckbox] [bit] NULL,
	[Initial] [varchar](5) NULL,
 CONSTRAINT [PK_MNT_Users] PRIMARY KEY CLUSTERED 
(
	[SNo] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [IX_MNT_Users_UserId] UNIQUE NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


