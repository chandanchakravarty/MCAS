

/****** Object:  Table [dbo].[MNT_Hospital]    Script Date: 08/26/2014 16:47:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_Hospital]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_Hospital]
GO



/****** Object:  Table [dbo].[MNT_Hospital]    Script Date: 08/26/2014 16:47:59 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[MNT_Hospital](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HospitalName] [nvarchar](100) NOT NULL,
	[HospitalAddress] [nvarchar](150) NOT NULL,
	[HospitalContactNo] [nvarchar](20) NOT NULL,
	[HospitalFaxNo] [nvarchar](20) NULL,
	[ContactPersonName] [nvarchar](50) NULL,
	[Email] [nvarchar](50) NULL,
	[officeNo] [nvarchar](20) NULL,
	[FaxNo] [nvarchar](20) NULL,
	[CreatedBy] [varchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_MNT_Hospital] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO


