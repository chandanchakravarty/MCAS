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
	[ParentId] [int] NULL,
	[ScreenId] [int] NULL,
	[OutPutFormat] [varchar](50) NULL,
	[Id] [int] NULL,
	[Is_Header] [char](1) NULL,
	[HasPartyType] [char](1) NULL,
	[PartyToShown] [int] NULL,
 CONSTRAINT [PK_MNT_TEMPLATE_MASTER_Template_Id] PRIMARY KEY CLUSTERED 
(
	[Template_Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO