IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 295
  AND [TabId] = 'S_ADMN')
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive],[GroupType])
    VALUES (137, 295, N'S_ADMN', N'Finance Authority Limit', N'Y', N'N', N'1', N'/FAL/FALIndex', N'ADM', N'ADM', N'icon-money text-success', 295, N'N', N'N', 0, N'N', N'Finance Authority Limit', 150, 17, NULL, NULL, NULL, 0, 1, N'Y',N'B')
END

IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 296
  AND [TabId] = 'S_ADMN')
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive],[GroupType])
    VALUES (138, 296, N'S_ADMN', N'Finance Authority Limit Details', N'N', N'N', N'1', N'/FAL/FALEditor', N'ADM', N'ADM', N'icon-money text-success', 296, N'N', N'N', 0, N'N', N'Finance Authority Limit Details', 150, 17, NULL, NULL, NULL, 0, 1, N'Y',NULL)
END