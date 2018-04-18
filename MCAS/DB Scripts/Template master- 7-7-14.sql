USE [CDGI]
GO

/****** Object:  Table [dbo].[MNT_TEMPLATE_MASTER]    Script Date: 07/07/2014 17:23:52 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_TEMPLATE_MASTER]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_TEMPLATE_MASTER]
GO

USE [CDGI]
GO

/****** Object:  Table [dbo].[MNT_TEMPLATE_MASTER]    Script Date: 07/07/2014 17:23:52 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MNT_TEMPLATE_MASTER](
	[Template_Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [nvarchar](400) NULL,
	[Filename] [nvarchar](400) NOT NULL,
	[Display_Name] [nvarchar](400) NOT NULL,
	[Carrier_Id] [int] NULL,
	[Lob_Id] [int] NULL,
	[Template_Path] [nvarchar](400) NULL,
	[Is_System_Template] [bit] NOT NULL,
	[Template_Format_Id] [int] NOT NULL,
	[Is_Active] [bit] NOT NULL,
	[MappingXML_Path] [nvarchar](100) NULL,
	[MappingXML_FileName] [nvarchar](200) NULL,
	[Template_Code] [nvarchar](25) NULL,
	[Has_Dynamic_Data] [bit] NULL,
	[Has_Condition] [bit] NULL,
	[Has_Dynamic_Footer] [bit] NULL,
	[Has_Footer_Desc] [nvarchar](800) NULL,
	[Has_Dynamic_Header_Footer] [bit] NULL,
 CONSTRAINT [PK_MNT_TEMPLATE_MASTER_Template_Id] PRIMARY KEY CLUSTERED 
(
	[Template_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'd= CHECK THE DYNAMIC DATA' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MNT_TEMPLATE_MASTER', @level2type=N'COLUMN',@level2name=N'Has_Dynamic_Data'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'CHECK THE DYNAMIC DATA' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MNT_TEMPLATE_MASTER', @level2type=N'COLUMN',@level2name=N'Has_Dynamic_Data'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MNT_TEMPLATE_MASTER', @level2type=N'COLUMN',@level2name=N'Has_Dynamic_Data'
GO

EXEC sys.sp_addextendedproperty @name=N'Caption', @value=N'CHECK THE CONDITION' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MNT_TEMPLATE_MASTER', @level2type=N'COLUMN',@level2name=N'Has_Condition'
GO

EXEC sys.sp_addextendedproperty @name=N'Description', @value=N'CHECK THE CONDITION' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MNT_TEMPLATE_MASTER', @level2type=N'COLUMN',@level2name=N'Has_Condition'
GO

EXEC sys.sp_addextendedproperty @name=N'Domain', @value=N'' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'MNT_TEMPLATE_MASTER', @level2type=N'COLUMN',@level2name=N'Has_Condition'
GO


