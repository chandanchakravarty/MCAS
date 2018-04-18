
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 130
  AND [TabId] = 'CLAIM')
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (51, 130, N'CLAIM', N'Accident', N'N', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAccidentEditor', N'ADM', N'ADM', N'', 130, N'N', N'Y', 0, N'N', N'Accident', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END

IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 131
  AND [TabId] = 'CLAIM')
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (52, 131, N'CLAIM', N'Own Damage', N'N', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/OwnDamage', N'ADM', N'ADM', N'', 131, N'N', N'Y', 0, N'N', N'Own Damage', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END

IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 132
  AND [TabId] = 'CLAIM')
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (53, 132, N'CLAIM', N'PD/BI', N'N', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ThirdPartyEditor', N'ADM', N'ADM', N'', 132, N'N', N'Y', 0, N'N', N'PD/BI', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 133
  AND [TabId] = 'CLAIM')
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (54, 133, N'CLAIM', N'Notes', N'N', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimNotesEditor', N'ADM', N'ADM', N'', 133, N'N', N'Y', 0, N'N', N'Notes', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 134
  AND [TabId] = 'CLAIM')
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (55, 134, N'CLAIM', N'Tasks', N'N', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/TaskEditor', N'ADM', N'ADM', N'', 134, N'N', N'Y', 0, N'N', N'Tasks', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 135
  AND [TabId] = 'CLAIM')
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (56, 135, N'CLAIM', N'Mandate', N'N', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimMandateReqEditor', N'ADM', N'ADM', N'', 135, N'N', N'Y', 0, N'N', N'Mandate', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 136
  AND [TabId] = 'CLAIM')
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (57, 136, N'CLAIM', N'Attachments', N'N', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAttachmentsEditor', N'ADM', N'ADM', N'', 136, N'N', N'Y', 0, N'N', N'Attachments', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 137
  AND [TabId] = 'CLAIM')
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (58, 137, N'CLAIM', N'Diary', N'N', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/DiaryTaskEditor', N'ADM', N'ADM', N'', 137, N'N', N'Y', 0, N'N', N'Diary', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 138
  AND [TabId] = 'CLAIM')
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (59, 138, N'CLAIM', N'Reserve', N'N', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimReserveEditor', N'ADM', N'ADM', N'', 138, N'N', N'Y', 0, N'N', N'Reserve', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 139
  AND [TabId] = 'CLAIM')
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (60, 139, N'CLAIM', N'Payment', N'N', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/PaymentEditorNew', N'ADM', N'ADM', N'', 139, N'N', N'Y', 0, N'N', N'Payment', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 140
  AND [TabId] = 'CLAIM')
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (61, 140, N'CLAIM', N'Transactions History', N'N', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/TransactionEditor', N'ADM', N'ADM', N'', 140, N'N', N'Y', 0, N'N', N'Transactions History', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END