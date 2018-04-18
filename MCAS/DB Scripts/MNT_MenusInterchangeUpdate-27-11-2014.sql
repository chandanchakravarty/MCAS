IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 277)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (119,
     277, 
     N'S_ADMN',
     N'Interchange',
     N'Y',
     N'N',
     N'1',
     N'/Masters/InterChangeIndex',
     N'ADM',
     N'ADM', 
     N'icon-double-angle-right', 
     277,
     N'N',
     N'N',
     0,
     N'N',
     N'Interchange', 
     150,
     17, 
     NULL,
     NULL,
     NULL,
     0,
     1,
     N'Y')
END
--------------------------------

IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 278)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (120,
     278,
     N'S_ADMN',
     N'Interchange Details',
     N'N',
     N'N',
     N'1',
     N'/Masters/InterChangeEditor',
     N'ADM',
     N'ADM',
     N'icon-double-angle-right',
     278,
     N'N',
     N'N',
     0,
     N'N',
     N'Interchange Details',
     150, 17, NULL, NULL, NULL, 0, 1, N'Y')
END


------------------------------------------------------------
/****** Object:  Table [dbo].[MNT_InterChange]    Script Date: 11/27/2014 18:12:19 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_InterChange]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_InterChange]
GO



/****** Object:  Table [dbo].[MNT_InterChange]    Script Date: 11/27/2014 18:12:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MNT_InterChange](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InterchangeName] [nvarchar](250) NOT NULL,
	[Address1] [nvarchar](100) NOT NULL,
	[Address2] [nvarchar](100) NULL,
	[Address3] [nvarchar](100) NULL,
	[City] [nvarchar](100) NULL,
	[State] [nvarchar](100) NULL,
	[Country] [nvarchar](100) NOT NULL,
	[PostalCode] [nvarchar](30) NULL,
	[Status] [nvarchar](1) NOT NULL,
	[EffectiveFrom] [datetime] NOT NULL,
	[EffectiveTo] [datetime] NULL,
	[Remarks] [nvarchar](800) NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](25) NULL,
	[ModifiedBy] [nvarchar](25) NULL,
 CONSTRAINT [PK_MNT_InterChange] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

