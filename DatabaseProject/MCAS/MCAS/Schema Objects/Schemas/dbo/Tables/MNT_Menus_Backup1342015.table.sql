CREATE TABLE [dbo].[MNT_Menus_Backup1342015](
	[TId] [int] NOT NULL,
	[MenuId] [int] NOT NULL,
	[TabId] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DisplayTitle] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsMenuItem] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[IsJsMenuItem] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[VirtualSource] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Hyp_Link_Address] [varchar](150) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ProductName] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[UserType] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Displayimg] [varchar](100) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[DisplayOrder] [int] NULL,
	[IsHeader] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SubMenu] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MainHeaderId] [int] NULL,
	[IsAdmin] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[AdminDisplayText] [varchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[MenuItemWidth] [int] NULL,
	[MenuItemHeight] [int] NULL,
	[ErrorMessDesc] [varchar](1000) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ErrorMessTitle] [varchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[ErrorMessHead] [varchar](30) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[SubTabId] [varchar](10) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[LangId] [int] NULL,
	[IsActive] [varchar](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[GroupType] [char](1) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
 CONSTRAINT [PK_MNT_Menus_Backup1342015] PRIMARY KEY CLUSTERED 
(
	[MenuId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)


