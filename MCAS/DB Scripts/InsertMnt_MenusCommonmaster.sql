
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 297
  AND [TabId] = 'S_ADMN')
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive],[GroupType])
    VALUES (139, 297, N'S_ADMN', N'Common Master', N'Y', N'N', N'1', N'/Masters/CommonMasterIndex', N'ADM', N'ADM', N'icon-rss', 297, N'N', N'N', 0, N'N', N'Common Master', 150, 17, NULL, NULL, NULL, 0, 1, N'Y',N'B')
END




IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 298
  AND [TabId] = 'S_ADMN')
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive],[GroupType])
    VALUES (140, 298, N'S_ADMN', N'Common Master Details', N'N', N'N', N'1', N'/Masters/CommonMasterEditor', N'ADM', N'ADM', N'icon-money text-success', 298, N'N', N'N', 0, N'N', N'Common Master Details', 150, 17, NULL, NULL, NULL, 0, 1, N'Y',NULL)
END


IF NOT EXISTS (SELECT TOP 1* FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'MNT_Lookups' AND [COLUMN_NAME] = 'ModifiedBy')
BEGIN
  ALTER TABLE [dbo].[MNT_Lookups] ADD ModifiedBy nvarchar(50) NULL
END

IF NOT EXISTS (SELECT TOP 1* FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'MNT_Lookups' AND [COLUMN_NAME] = 'ModifiedDate')
BEGIN
  ALTER TABLE [dbo].[MNT_Lookups] ADD ModifiedDate datetime NULL
END


ALTER TABLE MNT_Lookups
ADD CONSTRAINT pk_MNT_Lookups_LookupID PRIMARY KEY(LookupID)
GO








