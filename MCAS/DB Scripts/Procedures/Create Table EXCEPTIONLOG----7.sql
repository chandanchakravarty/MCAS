/****** Object:  Table [dbo].[EXCEPTIONLOG]    Script Date: 07/22/2014 15:59:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EXCEPTIONLOG](
	[exceptionid] [int] IDENTITY(1,1) NOT NULL,
	[exceptiondate] [datetime] NULL,
	[exceptiondesc] [text] NULL,
	[customer_id] [int] NULL,
	[app_id] [int] NULL,
	[app_version_id] [smallint] NULL,
	[policy_id] [int] NULL,
	[policy_version_id] [smallint] NULL,
	[claim_id] [int] NULL,
	[qq_id] [int] NULL,
	[source] [nvarchar](50) NULL,
	[message] [nvarchar](500) NULL,
	[class_name] [nvarchar](100) NULL,
	[method_name] [nvarchar](100) NULL,
	[query_string_params] [nvarchar](500) NULL,
	[system_id] [nvarchar](30) NULL,
	[user_id] [int] NULL,
	[lob_id] [int] NULL,
	[exception_type] [nvarchar](100) NULL,
 CONSTRAINT [PK_EXCEPTIONLOG_EXCEPTIONID] PRIMARY KEY CLUSTERED 
(
	[exceptionid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
