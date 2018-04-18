IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE MenuId = 269)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (111, 269, N'CLM_UPL', N'Upload', N'Y', N'N', N'1', N'#', N'CLMS', N'ADM', N'icon-briefcase text-danger', 269, N'Y', N'N', 0, N'N', N'Upload', 150, 17, NULL, NULL, NULL, N'0', 1, N'N')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE MenuId = 270)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (112, 270, N'CLM_UPL', N'TAC Upload', N'Y', N'N', N'1', N'/ClaimRecoveryProcessing/ClaimRecoveryProcessing?claimMode=Recovery', N'CLMS', N'ADM', N'icon-double-angle-right', 270, N'N', N'Y', 269, N'N', N'TAC Upload', 150, 17, NULL, NULL, NULL, N'0', 1, N'N')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE MenuId = 271)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (113, 271, N'CLM_UPL', N'Claims Upload', N'Y', N'N', N'1', N'/ClaimRecoveryProcessing/ClaimRecoveryProcessing?claimMode=Recovery', N'CLMS', N'ADM', N'icon-double-angle-right', 271, N'N', N'Y', 269, N'N', N'Claims Upload', 150, 17, NULL, NULL, NULL, N'0', 1, N'N')
END