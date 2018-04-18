
/****** Object:  Table [dbo].[CLM_ClaimTask]    Script Date: 09/01/2014 17:23:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CLM_ClaimTask]') AND type in (N'U'))
DROP TABLE [dbo].[CLM_ClaimTask]
GO

/****** Object:  Table [dbo].[CLM_ClaimTask]    Script Date: 09/01/2014 17:23:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CLM_ClaimTask](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaskNo] [int] NULL,
	[ClaimID] [int] NULL,
	[ActionDue] [datetime] NULL,
	[CloseDate] [datetime] NULL,
	[PromtDetails] [nvarchar](100) NULL,
	[isApprove] [int] NOT NULL,
	[ApproveDate] [datetime] NULL,
	[ApproveBy] [nvarchar](25) NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](25) NULL,
	[ModifiedBy] [nvarchar](25) NULL,
	[Remarks] [nvarchar](500) NULL,
 CONSTRAINT [PK_CLM_ClaimTask] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


