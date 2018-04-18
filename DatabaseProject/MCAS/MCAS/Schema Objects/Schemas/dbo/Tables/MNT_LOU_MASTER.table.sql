CREATE TABLE [dbo].[MNT_LOU_MASTER](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LouRate] [decimal](18, 2) NULL,
	[EffectiveDate] [datetime] NULL,
	[IsActive] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL CONSTRAINT [DF__MNT_LOU_M__IsAct__32EB7E57]  DEFAULT ('Y'),
	[CreatedDate] [datetime] NULL CONSTRAINT [DF__MNT_LOU_M__Creat__33DFA290]  DEFAULT (getdate()),
	[CreatedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](25) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ClaimId] [int] NULL,
 CONSTRAINT [PK_MNT_LOU_MASTER] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


