update MNT_Menus set isactive='N' where MenuId in(100,231,106,236,107,238,108,240,109,241,128,220,125,253);




IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 256
  AND [TabId] = 'S_ADMN')
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (98, 256, N'S_ADMN', N'Service Provider Master', N'Y', N'N', N'1', N'/Cedant/CedantIndex', N'ADM', N'ADM', N'icon-eye-open text-facebook', 256, N'N', N'Y', 0, N'N', N'Service Provider Master', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
