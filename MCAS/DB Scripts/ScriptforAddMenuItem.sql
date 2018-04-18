IF NOT EXISTS (SELECT 1 FROM [MNT_Menus] WHERE [MenuId] = 294 and  [TabId] = 'CLAIM')
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId],[TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (136, 294,N'CLAIM', N'Recovery', N'Y', N'N', N'1', N'ClaimRecoveryProcessing/ClaimRecoveryEditor', N'CLM', N'ADM', N'', 294, N'N', N'Y', 0, N'N', N'Recovery', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END