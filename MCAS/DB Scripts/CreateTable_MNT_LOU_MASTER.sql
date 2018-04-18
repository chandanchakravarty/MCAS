

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MNT_LOU_M__IsAct__32EB7E57]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MNT_LOU_MASTER] DROP CONSTRAINT [DF__MNT_LOU_M__IsAct__32EB7E57]
END

GO

IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__MNT_LOU_M__Creat__33DFA290]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[MNT_LOU_MASTER] DROP CONSTRAINT [DF__MNT_LOU_M__Creat__33DFA290]
END

GO


/****** Object:  Table [dbo].[MNT_LOU_MASTER]    Script Date: 08/25/2014 14:39:47 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_LOU_MASTER]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_LOU_MASTER]
GO


/****** Object:  Table [dbo].[MNT_LOU_MASTER]    Script Date: 08/25/2014 14:39:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_LOU_MASTER](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LouRate] [int] NULL,
	[EffectiveDate] [datetime] NULL,
	[IsActive] [char](1) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](25) NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [nvarchar](25) NULL,
	[ClaimId] [int] NULL,
 CONSTRAINT [PK_MNT_LOU_MASTER] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[MNT_LOU_MASTER] ADD  DEFAULT ('Y') FOR [IsActive]
GO

ALTER TABLE [dbo].[MNT_LOU_MASTER] ADD  DEFAULT (getdate()) FOR [CreatedDate]
GO


