/****** Object:  Table [dbo].[Claim_ReAssignmentDairy]    Script Date: 07/09/2014 12:42:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Claim_ReAssignmentDairy](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DairyId] [nvarchar](100) NOT NULL,
	[TypeOfAssignment] [char](1) NULL,
	[ReAssignTo] [nvarchar](50) NULL,
	[DairyFromUser] [nvarchar](50) NULL,
	[ReAssignDateFrom] [datetime] NULL,
	[ReAssignDateTo] [datetime] NULL,
	[Remark] [nvarchar](4000) NULL,
	[EmailId] [nvarchar](50) NULL,
	[Status] [char](1) NULL,
	[IsActive] [char](1) NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](25) NULL,
	[ModifiedBy] [nvarchar](25) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_Claim_ReAssignmentDairy] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


