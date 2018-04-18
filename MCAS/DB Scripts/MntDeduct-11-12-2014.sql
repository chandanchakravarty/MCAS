
/****** Object:  Table [dbo].[MNT_Deductible]    Script Date: 12/11/2014 16:26:44 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[MNT_Deductible]') AND type in (N'U'))
DROP TABLE [dbo].[MNT_Deductible]
GO


/****** Object:  Table [dbo].[MNT_Deductible]    Script Date: 12/11/2014 16:26:44 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[MNT_Deductible](
	[DeductibleId] [int] IDENTITY(1,1) NOT NULL,
	[OrgCategory] [nvarchar](50) NULL,
	[OrgCategoryName] [nvarchar](100) NULL,
	[DeductibleAmt] [decimal](18, 2) NULL,
	[EffectiveFrom] [datetime] NULL,
	[EffectiveTo] [datetime] NULL,
	[ModifiedDate] [datetime] NULL,
	[CreatedDate] [datetime] NULL,
	[CreatedBy] [nvarchar](50) NULL,
	[ModifiedBy] [nvarchar](50) NULL,
 CONSTRAINT [PK_MNT_Deductible] PRIMARY KEY CLUSTERED 
(
	[DeductibleId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


------------------------------------------------------------------
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 280)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (122,
     280,
     N'S_ADMN',
     N'Deductible Details',
     N'N',
     N'N',
     N'1',
     N'/Masters/DeductibleEditor',
     N'ADM',
     N'ADM',
     N'icon-double-angle-right',
     280,
     N'N',
     N'N',
     0,
     N'N',
     N'Deductible Details',
     150, 17, NULL, NULL, NULL, 0, 1, N'Y')
END

--------------------------------------------------

update [mnt_menus] set [DisplayTitle]='Deductible',Hyp_Link_Address='/Masters/DeductibleIndex',
 AdminDisplayText='Deductible'
 where MenuId=279
