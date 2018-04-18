IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE MenuId = 272)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (114, 272, N'TacUpload', N'TAC File Upload', N'Y', N'N', N'1', N'/ClaimProcessing/TacFileUploadEditor', N'CLM', N'ADM', N'', 272, N'N', N'N', 0, N'N', N'TAC File Upload', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE MenuId = 273)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (115, 273, N'TacUpload', N'View TAC File Upload Status', N'Y', N'N', N'1', N'/ClaimProcessing/ViewTacFileUploadStatusEditor', N'CLM', N'ADM', N'', 273, N'N', N'N', 0, N'N', N'View TAC File Upload Status', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END

IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE MenuId = 274)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (116, 274, N'CUpload', N'Claim File Upload', N'Y', N'N', N'1', N'/ClaimProcessing/ClaimFileUploadEditor', N'CLM', N'ADM', N'', 274, N'N', N'N', 0, N'N', N'Claim File Upload', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END

IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE MenuId = 275)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (117, 275, N'CUpload', N'View Claim File Upload Status', N'Y', N'N', N'1', N'/ClaimProcessing/ViewClaimFileUploadStatusEditor', N'CLM', N'ADM', N'', 275, N'N', N'N', 0, N'N', N'View Claim File Upload Status', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END

update MNT_Menus set Hyp_Link_Address='/ClaimProcessing/ClaimFileUploadEditor?claimMode=CUpload' where MenuId =271