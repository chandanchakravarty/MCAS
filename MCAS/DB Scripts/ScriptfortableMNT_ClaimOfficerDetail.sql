/****** Object:  Table [dbo].[MNT_ClaimOfficerDetail]    Script Date: 06/25/2014 14:12:33 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_ClaimOfficerDetail](
	[TranId] [int] IDENTITY(1,1) NOT NULL,
	[UserGroup] [nvarchar](50) NOT NULL,
	[ClaimentOfficerName] [nvarchar](200) NOT NULL,
	[Department] [varchar](50) NOT NULL,
	[LastAssignmentDate] [datetime] NOT NULL,
	[Type] [varchar](5) NOT NULL,
	[ClaimNo] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_MNT_ClaimOfficerDetail] PRIMARY KEY CLUSTERED 
(
	[TranId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


