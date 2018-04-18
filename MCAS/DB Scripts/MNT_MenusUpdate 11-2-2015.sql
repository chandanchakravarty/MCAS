


IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 281)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (123, 281, N'DIARY', N'Diary', N'Y', N'N', N'1', N'#', N'DIARY', N'ADM', N'icon-user text-info', 281, N'Y', N'N', 0, N'N', N'Diary', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END

IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 282)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (124, 282, N'DIARY', N'Original Diary', N'Y', N'N', N'1', N'#', N'DIARY', N'ADM', N'icon-user text-info', 282, N'N', N'Y', 282, N'N', N'Original Diary', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END

IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 283)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (125, 283, N'DIARY', N'Reassigned Diary', N'Y', N'N', N'1', N'#', N'DIARY', N'ADM', N'icon-user text-info', 283, N'N', N'Y', 281, N'N', N'Reassigned Diary', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END

IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 284)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (126, 284, N'DIARY', N'Escalation Diary', N'Y', N'N', N'1', N'#', N'DIARY', N'ADM', N'icon-user text-info', 284, N'N', N'Y', 281, N'N', N'Escalation Diary', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END



IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 285)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (127, 285, N'DIARY', N'Tasks', N'Y', N'N', N'1', N'#', N'DIARY', N'ADM', N'icon-user text-info', 285, N'Y', N'N', 0, N'N', N'Tasks', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END


IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 286)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (128, 286, N'DIARY', N'Tasks', N'Y', N'N', N'1', N'#', N'DIARY', N'ADM', N'icon-user text-info', 286, N'N', N'Y', 285, N'N', N'Tasks', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
    END
    
    
    IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 287)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (129, 287, N'DIARY', N'Mandate', N'Y', N'N', N'1', N'#', N'DIARY', N'ADM', N'icon-user text-info', 287, N'Y', N'N', 0, N'N', N'Mandate', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
    
    
    
    
    IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 288)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (130, 288, N'DIARY', N'Mandate Request', N'Y', N'N', N'1', N'#', N'DIARY', N'ADM', N'icon-user text-info', 288, N'N', N'Y', 287, N'N', N'Mandate Request', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
    END
    
    IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 289)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (131, 289, N'DIARY', N'Mandate Approval', N'Y', N'N', N'1', N'#', N'DIARY', N'ADM', N'icon-user text-info', 289, N'N', N'Y', 287, N'N', N'Mandate Approval', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
    END
    
    
    IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 290)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (132, 290, N'DIARY', N'Payment', N'Y', N'N', N'1', N'#', N'DIARY', N'ADM', N'icon-user text-info', 290, N'Y', N'N', 0, N'N', N'Payment', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
   
   
   
   IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 291)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (133, 291, N'DIARY', N'Payment Request', N'Y', N'N', N'1', N'#', N'DIARY', N'ADM', N'icon-user text-info', 291, N'N', N'Y', 290, N'N', N'Payment Request', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
    END 
    
     
    
    
    
    IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 292)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (134, 292, N'DIARY', N'Payment Approval', N'Y', N'N', N'1', N'#', N'DIARY', N'ADM', N'icon-user text-info', 292, N'N', N'Y', 290, N'N', N'Payment Approval', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
    END
    
    
    
    
   