
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 257)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (99, 257, N'S_ADMN', N'Entity Details', N'N', N'N', N'1', N'UserAdmin/UserEntityIndex', N'ADM', N'ADM', N'icon-list-ol text-warning', 257, N'N', N'Y', 114, N'N', N'Entity Details', null, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END

IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 258)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (100, 258, N'S_ADMN', N'Entity Details', N'N', N'N', N'1', N'UserAdmin/UserEntityIndex', N'ADM', N'ADM', N'icon-list-ol text-warning', 258, N'N', N'Y', 0, N'N', N'Entity Details', 114, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END

IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 259)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (101, 259, N'S_ADMN', N'User Groups', N'N', N'N', N'1', N'UserAdmin/UserGroupsIndex', N'ADM', N'ADM', N'icon-list-ol text-warning', 259, N'N', N'Y', 114, N'N', N'Entity Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END

IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 260)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (102, 260, N'S_ADMN', N'User Groups Details', N'N', N'N', N'1', N'UserAdmin/UserGroupsEditor', N'ADM', N'ADM', N'icon-list-ol text-warning', 260, N'N', N'Y', 114, N'N', N'User Groups Details', 150, 17, NULL, NULL, NULL, 4, 1, N'Y')
END


IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 261)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (103, 261, N'S_ADMN', N'User', N'N', N'N', N'1', N'UserAdmin/UserAdminMastersList', N'ADM', N'ADM', N'icon-list-ol text-warning', 261, N'N', N'Y', 114, N'N', N'User', 150, 17, NULL, NULL, NULL, 5, 1, N'Y')
END

IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 262)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (104, 262, N'S_ADMN', N'User Details', N'N', N'N', N'1', N'UserAdmin/UserAdminMastersEditor', N'ADM', N'ADM', N'icon-list-ol text-warning', 262, N'N', N'Y', 114, N'N', N'User Details', 150, 17, NULL, NULL, NULL, 6, 1, N'Y')
END

IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 263)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (105, 263, N'S_ADMN', N'Department Master', N'N', N'N', N'1', N'UserAdmin/UserDeptIndex', N'ADM', N'ADM', N'icon-list-ol text-warning', 263, N'N', N'Y', 114, N'N', N'Department Master', 150, 17, NULL, NULL, NULL, 7, 1, N'Y')
END


IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 264)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (106, 264, N'S_ADMN', N'Department Master Details', N'N', N'N', N'1', N'UserAdmin/UserDeptEditor', N'ADM', N'ADM', N'icon-list-ol text-warning', 264, N'N', N'Y', 114, N'N', N'Department Master Details', 150, 17, NULL, NULL, NULL, 8, 1, N'Y')
END


IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 265)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (107, 265, N'S_ADMN', N'Password Setup', N'N', N'N', N'1', N'UserAdmin/PasswordSetUpEditor', N'ADM', N'ADM', N'icon-list-ol text-warning', 265, N'N', N'Y', 114, N'N', N'Password Setup', 150, 17, NULL, NULL, NULL, 10, 1, N'Y')
END



IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 266)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (108, 266, N'S_ADMN', N'Release Locked Users', N'N', N'N', N'1', N'UserAdmin/PasswordSetUpEditor', N'ADM', N'ADM', N'icon-list-ol text-warning', 266, N'N', N'Y', 114, N'N', N'Release Locked Users', 150, 17, NULL, NULL, NULL, 11, 1, N'Y')
END

IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 267)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (109, 267, N'S_ADMN', N'Country for User Access', N'N', N'N', N'1', N'UserAdmin/UserCountryIndex', N'ADM', N'ADM', N'icon-list-ol text-warning', 267, N'N', N'Y', 114, N'N', N'Country for User Access', 150, 17, NULL, NULL, NULL, 12, 1, N'Y')
END



IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 268)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (110, 268, N'S_ADMN', N'Country for User Access Details', N'N', N'N', N'1', N'UserAdmin/UserCountryEditor', N'ADM', N'ADM', N'icon-list-ol text-warning', 268, N'N', N'Y', 114, N'N', N'Country for User Access Details', 150, 17, NULL, NULL, NULL, 13, 1, N'Y')
END

update MNT_Menus
set SubTabId=0
where SubTabId is null

update MNT_Menus
set MainHeaderId=114,MenuItemWidth=150, subtabid=1 
where menuid=257

update MNT_Menus
set MainHeaderId=114, subtabid=2 
where menuid=258


update MNT_Menus
set  subtabid=3 
where menuid=259


update mnt_menus set submenu ='N' where menuid in (101, 102, 103, 104, 105, 110, 111, 112, 113, 114, 115, 116, 117, 118, 119, 120, 121, 122, 123, 124, 125, 126, 127, 129, 130, 131, 132, 133, 134, 135, 136, 137, 138, 139, 140, 200, 201, 202, 203, 204, 205, 206, 207, 208, 209, 210, 211, 212, 213, 214, 215, 216, 217, 218, 219, 221, 222, 223, 224, 225, 226, 227, 228, 229, 230, 232, 233, 234, 235, 237, 239, 242, 243, 244, 245, 246, 247, 248, 249, 250, 251, 252, 253, 254, 255, 256)

update MNT_Menus
set IsHeader='Y'
where MenuId=114

update MNT_Menus
set Hyp_Link_Address='UserAdmin/UserEntityEditor'
where MenuId=258

update MNT_Menus
set Hyp_Link_Address='UserAdmin/ReleasedLockedUser'
where MenuId=266

