IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 100)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (1, 100, N'S_ADMN', N'Insurer', N'N', N'N', N'1', N'/Cedant/CedantIndex', N'ADM', N'ADM', N'icon-user text-info', 100, N'N', N'Y', 256, N'N', N'Insurer', 150, 17, NULL, NULL, NULL, N'1', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 101)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (2, 101, N'S_ADMN', N'Upload Main Menu', N'Y', N'N', N'1', N'/Masters/VehicleUploadIndex', N'ADM', N'ADM', N'icon-signin text-warning', 101, N'N', N'N', 0, N'N', N'Upload Main Menu', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 102)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (3, 102, N'S_ADMN', N'Nature of Loss', N'Y', N'N', N'1', N'/ClaimMasters/LossNatureMasterList', N'ADM', N'ADM', N'icon-font text-facebook', 102, N'N', N'N', 0, N'N', N'Nature of Loss', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 103)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (4, 103, N'S_ADMN', N'Type of Loss', N'Y', N'N', N'1', N'/ClaimMasters/Index', N'ADM', N'ADM', N'icon-globe text-gplus', 103, N'N', N'N', 0, N'N', N'Type of Loss', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 104)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (5, 104, N'S_ADMN', N'Main Class of Business', N'Y', N'N', N'1', N'/ProductBusiness/ProductBusinessIndex', N'ADM', N'ADM', N'icon-dashboard text-danger', 104, N'N', N'N', 0, N'N', N'Main Class of Business', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 105)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (6, 105, N'S_ADMN', N'Sub-Class', N'Y', N'N', N'1', N'/ProductBusiness/SubClassIndex', N'ADM', N'ADM', N'icon-file-text text-success', 105, N'N', N'N', 0, N'N', N'Sub-Class', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 106)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (7, 106, N'S_ADMN', N'Depot Master', N'N', N'N', N'1', N'/Masters/DepotMasterIndex', N'ADM', N'ADM', N'icon-suitcase text-info', 106, N'N', N'Y', 256, N'N', N'Depot Master', 150, 17, NULL, NULL, NULL, N'3', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 107)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (8, 107, N'S_ADMN', N'Surveyor Master', N'N', N'N', N'1', N'/AdjusterMasters/SurveyorIndex', N'ADM', N'ADM', N'icon-sitemap text-warning', 107, N'N', N'Y', 256, N'N', N'Surveyor Master', 150, 17, NULL, NULL, NULL, N'5', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 108)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (9, 108, N'S_ADMN', N'Adjuster Master', N'N', N'N', N'1', N'/AdjusterMasters/AdjusterIndex', N'ADM', N'ADM', N'icon-eye-open text-facebook', 108, N'N', N'Y', 256, N'N', N'Adjuster Master', 150, 17, NULL, NULL, NULL, N'9', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 109)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (10, 109, N'S_ADMN', N'Solicitor  Master', N'N', N'N', N'1', N'/AdjusterMasters/SolicitorIndex', N'ADM', N'ADM', N'icon-legal text-gplus', 109, N'N', N'Y', 256, N'N', N'Lawyer Master', 150, 17, NULL, NULL, NULL, N'7', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 110)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (11, 110, N'S_ADMN', N'Claims Expense Master', N'Y', N'N', N'1', N'/ClaimMasters/ClaimExpenseIndex', N'ADM', N'ADM', N'icon-leaf text-danger', 110, N'N', N'N', 0, N'N', N'Claims Expense Master', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 111)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (12, 111, N'S_ADMN', N'Claims Close Reason Master', N'Y', N'N', N'1', N'/ClaimMasters/ClaimCloseIndex', N'ADM', N'ADM', N'icon-compass text-success', 111, N'N', N'N', 0, N'N', N'Claims Close Reason Master', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 112)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (13, 112, N'S_ADMN', N'Currency Master', N'Y', N'N', N'1', N'/ClaimMasters/CurrencyMasterIndex', N'ADM', N'ADM', N'icon-info-sign text-info', 112, N'N', N'N', 0, N'N', N'Currency Master', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 113)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (14, 113, N'S_ADMN', N'Exchange Rate Master', N'Y', N'N', N'1', N'/ClaimMasters/ExchangeIndex', N'ADM', N'ADM', N'icon-cog text-warning', 113, N'N', N'N', 0, N'N', N'Exchange Rate Master', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 114)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (15, 114, N'S_ADMN', N'User Admin', N'Y', N'N', N'1', N'/UserAdmin/UserEntityIndex', N'ADM', N'ADM', N'icon-user text-facebook', 114, N'Y', N'N', 0, N'N', N'User Admin', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 115)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (16, 115, N'S_ADMN', N'Country Master', N'Y', N'N', N'1', N'/CountryMaster/CountryIndex', N'ADM', N'ADM', N'icon-globe text-gplus', 115, N'N', N'N', 0, N'N', N'Country Master', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 116)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (17, 116, N'S_ADMN', N'Diary and Follow-Up', N'Y', N'N', N'1', N'diary-and-follow-up-master.html', N'ADM', N'ADM', N'icon-bell text-danger', 116, N'N', N'N', 0, N'N', N'Diary and Follow-Up', 150, 17, NULL, NULL, NULL, N'0', 1, N'N')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 117)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (18, 117, N'S_ADMN', N'Vehicle Type', N'Y', N'N', N'1', N'vehicle-type.html', N'ADM', N'ADM', N'icon-th-large text-success', 117, N'N', N'N', 0, N'N', N'Vehicle Type', 150, 17, NULL, NULL, NULL, N'0', 1, N'N')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 118)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (19, 118, N'S_ADMN', N'Vehicle Make and Model', N'Y', N'N', N'1', N'Masters/VehicleIndex', N'ADM', N'ADM', N'icon-unlink text-info', 118, N'N', N'N', 0, N'N', N'Vehicle Make and Model', 150, 17, NULL, NULL, NULL, N'0', 1, N'N')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 119)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (20, 119, N'S_ADMN', N'Bus Captain Listing', N'Y', N'N', N'1', N'/Masters/VehicleBusCaptainIndex', N'ADM', N'ADM', N'icon-list-ol text-warning', 119, N'N', N'N', 0, N'N', N'Bus Captain Listing', 150, 17, NULL, NULL, NULL, N'0', 1, N'N')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 120)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (41, 120, N'S_ADMN', N'Vehicle Class ', N'Y', N'N', N'1', N'/Masters/VehicleClassIndex', N'ADM', N'ADM', N'icon-th-large text-success', 120, N'N', N'N', 0, N'N', N'Vehicle Class', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 121)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (42, 121, N'S_ADMN', N'Vehicle Make', N'Y', N'N', N'1', N'/Masters/VehicleIndex', N'ADM', N'ADM', N'icon-unlink text-info', 121, N'N', N'N', 0, N'N', N'Vehicle Make ', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 122)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (43, 122, N'S_ADMN', N'Vehicle Model', N'Y', N'N', N'1', N'/Masters/VModelIndex', N'ADM', N'ADM', N'icon-unlink text-info', 122, N'N', N'N', 0, N'N', N'Vehicle Model', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 123)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (44, 123, N'S_ADMN', N'Insurance Policy', N'Y', N'N', N'1', N'/InsuranceMaster/InsurancePolicyMasterIndex', N'ADM', N'ADM', N'icon-unlink text-info', 123, N'N', N'N', 0, N'N', N'Insurance Policy', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 124)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (45, 124, N'S_ADMN', N'GST Setting', N'Y', N'N', N'1', N'/ClaimMasters/GSTIndex', N'ADM', N'ADM', N'icon-list-ol text-warning', 124, N'N', N'N', 0, N'N', N'GST Setting', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 125)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (46, 125, N'S_ADMN', N'Organization', N'Y', N'N', N'1', N'/CountryMaster/OrgCountryIndex', N'ADM', N'ADM', N'icon-user text-info', 125, N'N', N'N', 0, N'N', N'Organization', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 126)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (47, 126, N'S_ADMN', N'Claim Officer Duty', N'Y', N'N', N'1', N'/Masters/ClaimOfficerDutyIndex', N'ADM', N'ADM', N'icon-list-ol text-warning', 126, N'N', N'N', 0, N'N', N'Claim Officer Duty', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 127)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (48, 127, N'S_ADMN', N'LOU Rate', N'Y', N'N', N'1', N'/Masters/LOUIndex', N'ADM', N'ADM', N'icon-list-ol text-warning', 127, N'N', N'N', 0, N'N', N'LOU Rate', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 128)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (49, 128, N'S_ADMN', N'Hospital Information ', N'N', N'N', N'1', N'/ClaimMasters/HospitalIndex', N'ADM', N'ADM', N'icon-th-large text-success', 128, N'N', N'Y', 256, N'N', N'Hospital Information ', 150, 17, NULL, NULL, NULL, N'11', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 129)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (50, 129, N'DAIRY', N'Re Assignment Editor', N'N', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ReAssignmentEditorP', N'DRY', N'ADM', N'', 129, N'N', N'N', 0, N'N', N'Re Assignment Editor', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 130)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (51, 130, N'CLAIM', N'Accident', N'Y', N'N', N'1', N'/ClaimProcessing/ClaimAccidentEditor', N'CLM', N'ADM', N'', 130, N'N', N'N', 0, N'N', N'Accident', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 131)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (52, 131, N'CLAIM', N'Own Damage', N'Y', N'N', N'1', N'/ClaimProcessing/OwnDamage', N'CLM', N'ADM', N'', 131, N'N', N'N', 0, N'N', N'Own Damage', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 132)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (53, 132, N'CLAIM', N'Service Provider', N'Y', N'N', N'1', N'/ClaimProcessing/ServiceProvider', N'CLM', N'ADM', N'', 132, N'N', N'N', 0, N'N', N'Service Provider', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 133)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (54, 133, N'CLAIM', N'Notes', N'Y', N'N', N'1', N'/ClaimProcessing/ClaimNotesEditor', N'CLM', N'ADM', N'', 133, N'N', N'N', 0, N'N', N' Notes', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 134)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (55, 134, N'CLAIM', N'Tasks', N'Y', N'N', N'1', N'/ClaimProcessing/TaskEditor', N'CLM', N'ADM', N'', 134, N'N', N'N', 0, N'N', N'Tasks', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 135)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (56, 135, N'CLAIM', N'Mandate', N'Y', N'N', N'1', N'/ClaimProcessing/ClaimMandateReqEditor', N'CLM', N'ADM', N'', 135, N'N', N'N', 0, N'N', N'Mandate', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 136)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (57, 136, N'CLAIM', N'Attachments', N'Y', N'N', N'1', N'/ClaimProcessing/ClaimAttachmentsEditor', N'CLM', N'ADM', N'', 136, N'N', N'N', 0, N'N', N'Attachments', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 137)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (58, 137, N'CLAIM', N'Diary', N'Y', N'N', N'1', N'/ClaimProcessing/DiaryTaskEditor', N'CLM', N'ADM', N'', 137, N'N', N'N', 0, N'N', N'Diary', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 138)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (59, 138, N'CLAIM', N'Reserve', N'Y', N'N', N'1', N'/ClaimProcessing/ClaimReserveEditor', N'CLM', N'ADM', N'', 138, N'N', N'N', 0, N'N', N'Reserve', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 139)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (60, 139, N'CLAIM', N'Payment', N'Y', N'N', N'1', N'/ClaimProcessing/PaymentEditorNew', N'CLM', N'ADM', N'', 139, N'N', N'N', 0, N'N', N'Payment', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 140)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (61, 140, N'CLAIM', N'Transactions History', N'Y', N'N', N'1', N'/ClaimProcessing/TransactionEditor', N'CLM', N'ADM', N'', 140, N'N', N'N', 0, N'N', N'Transactions History', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 200)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (21, 200, N'DASH', N'Dashboard', N'Y', N'N', N'1', N'/Home/Index', N'DASH', N'ADM', N'icon-dashboard text-white', 200, N'Y', N'N', 0, N'N', N'Dashboard', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 201)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (22, 201, N'DIARY', N'Diary Listing', N'Y', N'N', N'1', N'#', N'DIARY', N'ADM', N'icon-list-ol text-blue', 201, N'Y', N'N', 0, N'N', N'Diary Listing', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 202)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (23, 202, N'DIARY', N'Diaried items', N'Y', N'N', N'1', N'/Home/Index', N'DIARY', N'ADM', N'icon-double-angle-right', 202, N'N', N'N', 201, N'N', N'Diaried items', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 203)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (24, 203, N'DIARY', N'Assigned Tasks', N'Y', N'N', N'1', N'/Home/Index', N'DIARY', N'ADM', N'icon-double-angle-right', 203, N'N', N'N', 201, N'N', N'Assigned Tasks', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 204)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (25, 204, N'DIARY', N'Escalation Tasks', N'Y', N'N', N'1', N'/Home/Index', N'DIARY', N'ADM', N'icon-double-angle-right', 204, N'N', N'N', 201, N'N', N'Escalation Tasks', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 205)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (26, 205, N'CLM_REG', N'Claims Registration', N'Y', N'N', N'1', N'#', N'CLMS', N'ADM', N'icon-file-text text-orange', 205, N'Y', N'N', 0, N'N', N'Claims Registration', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 206)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (27, 206, N'CLM_REG', N'New Claims Registration', N'Y', N'N', N'1', N'/ClaimProcessing/ClaimRegistration?claimMode=New', N'CLMS', N'ADM', N'icon-double-angle-right', 206, N'N', N'Y', 205, N'N', N'New Claims Registration', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 207)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (28, 207, N'CLM_REG', N'Incomplete Claims Registration', N'Y', N'N', N'1', N'/ClaimProcessing/ClaimRegistration?claimMode=Incomplete', N'CLMS', N'ADM', N'icon-double-angle-right', 207, N'N', N'Y', 205, N'N', N'Incomplete Claims Registration', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 208)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (29, 208, N'CLM_REG', N'Claims Adjustments', N'Y', N'N', N'1', N'/ClaimProcessing/ClaimRegistration?claimMode=Adjustment', N'CLMS', N'ADM', N'icon-double-angle-right', 208, N'N', N'Y', 205, N'N', N'Claims Adjustments', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 209)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (30, 209, N'CLM_REG', N'Claims Enquiry', N'Y', N'N', N'1', N'/ClaimProcessing/ClaimRegistration?claimMode=RegEnquiry', N'CLMS', N'ADM', N'icon-double-angle-right', 209, N'N', N'Y', 205, N'N', N'Claims Enquiry', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 210)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (31, 210, N'CLM_PAY', N'Claims Payment', N'Y', N'N', N'1', N'#', N'CLMS', N'ADM', N'icon-dollar text-blue', 210, N'Y', N'N', 0, N'N', N'Claims Payment', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 211)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (32, 211, N'CLM_PAY', N'Claims Payment Processing', N'Y', N'N', N'1', N'/ClaimProcessing/ClaimPaymentProcessing?claimMode=Payment', N'CLMS', N'ADM', N'icon-double-angle-right', 211, N'N', N'Y', 210, N'N', N'Claims Payment Processing', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 212)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (33, 212, N'CLM_PAY', N'Incomplete Claims Payment Registration', N'N', N'N', N'1', N'/ClaimProcessing/ClaimPaymentProcessing?claimMode=Payment', N'CLMS', N'ADM', N'icon-double-angle-right', 212, N'N', N'Y', 210, N'N', N'Incomplete Claims Payment Registration', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 213)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (34, 213, N'CLM_PAY', N'Claims Payment Document Enquiry', N'Y', N'N', N'1', N'/ClaimProcessing/ClaimPaymentProcessing?claimMode=Payment', N'CLMS', N'ADM', N'icon-double-angle-right', 213, N'N', N'Y', 210, N'N', N'Claims Payment Document Enquiry', 150, 17, NULL, NULL, NULL, N'0', 1, N'N')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 214)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (35, 214, N'CLM_REC', N'Claims Recovery', N'Y', N'N', N'1', N'#', N'CLMS', N'ADM', N'icon-briefcase text-danger', 214, N'Y', N'N', 0, N'N', N'Claims Recovery', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 215)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (36, 215, N'CLM_REC', N'Claims Recovery Processing', N'Y', N'N', N'1', N'/ClaimRecoveryProcessing/ClaimRecoveryProcessing?claimMode=Recovery', N'CLMS', N'ADM', N'icon-double-angle-right', 215, N'N', N'Y', 214, N'N', N'Claims Recovery Processing', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 216)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (37, 216, N'CLM_REC', N'Claims Recovery', N'Y', N'N', N'1', N'/ClaimRecoveryProcessing/ClaimRecoveryProcessing?claimMode=Recovery', N'CLMS', N'ADM', N'icon-double-angle-right', 216, N'N', N'Y', 214, N'N', N'Claims Recovery', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 217)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (38, 217, N'CLM_ENQ', N'Enquiry', N'Y', N'N', N'1', N'#', N'CLMS', N'ADM', N'icon-book text-golden', 217, N'Y', N'N', 0, N'N', N'Enquiry', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 218)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (39, 218, N'CLM_ENQ', N'Claims', N'Y', N'N', N'1', N'/ClaimProcessing/ClaimEnquiry?claimMode=Enquiry', N'CLMS', N'ADM', N'icon-double-angle-right', 218, N'N', N'Y', 217, N'N', N'Claims', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 219)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (40, 219, N'CLM_ENQ', N'Claims Documents Printed', N'Y', N'N', N'1', N'/ClaimMasters/ClaimDocumentsPrintedIndex?claimMode=EnqDocPrinted', N'CLMS', N'ADM', N'icon-double-angle-right', 219, N'N', N'Y', 217, N'N', N'Claims Documents Printed', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 220)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (62, 220, N'S_ADMN', N'Hospital Information Editor', N'N', N'N', N'1', N'/ClaimMasters/HospitalEditor', N'ADM', N'ADM', N'', 220, N'N', N'Y', 256, N'N', N'Hospital Information Editor', 150, 17, NULL, NULL, NULL, N'12', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 221)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (63, 221, N'CLAIM', N'Folder1', N'N', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAttachmentsEditor', N'CLM', N'ADM', N'', 221, N'N', N'N', 0, N'N', N'3rd Party''s Documents', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 222)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (64, 222, N'CLAIM', N'Folder2', N'N', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAttachmentsEditor', N'CLM', N'ADM', N'', 222, N'N', N'N', 0, N'N', N'Insured''s Documents', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 223)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (65, 223, N'CLAIM', N'Folder3', N'N', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAttachmentsEditor', N'CLM', N'ADM', N'', 223, N'N', N'N', 0, N'N', N'Correspondences', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 224)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (66, 224, N'CLAIM', N'Folder4', N'N', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAttachmentsEditor', N'CLM', N'ADM', N'', 224, N'N', N'N', 0, N'N', N'Internal Documents', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 225)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (67, 225, N'CLAIM', N'Attachments', N'N', N'N', N'1', N'/ClaimProcessing/ClaimAttachmentsList', N'CLM', N'ADM', N'', 225, N'N', N'N', 0, N'N', N'Attachments List', 150, 17, NULL, NULL, NULL, N'0', 1, N'N')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 226)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (68, 226, N'CLAIM', N'PD/BI', N'N', N'N', N'1', N'/ClaimProcessing/ThirdPartyList', N'CLM', N'ADM', N'', 226, N'N', N'N', 0, N'N', N'PD/BI List', 150, 17, NULL, NULL, NULL, N'0', 1, N'N')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 227)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (69, 227, N'CLAIM', N'Notes', N'N', N'N', N'1', N'/ClaimProcessing/ClaimNotesList', N'CLM', N'ADM', N'', 227, N'N', N'N', 0, N'N', N'Notes List', 150, 17, NULL, NULL, NULL, N'0', 1, N'N')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 228)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (70, 228, N'CLAIM', N'Tasks', N'N', N'N', N'1', N'/ClaimProcessing/TaskIndex', N'CLM', N'ADM', N'', 228, N'N', N'N', 0, N'N', N'Tasks List', 150, 17, NULL, NULL, NULL, N'0', 1, N'N')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 229)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (71, 229, N'CLAIM', N'Mandate', N'N', N'N', N'1', N'/ClaimProcessing/ClaimMandateList', N'CLM', N'ADM', N'', 229, N'N', N'N', 0, N'N', N'Mandate', 150, 17, NULL, NULL, NULL, N'0', 1, N'N')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 230)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (72, 230, N'CLAIM', N'Diary', N'N', N'N', N'1', N'/ClaimProcessing/ToDoList', N'CLM', N'ADM', N'', 230, N'N', N'N', 0, N'N', N'Diary List', 150, 17, NULL, NULL, NULL, N'0', 1, N'N')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 231)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (73, 231, N'S_ADMN', N'Insurer Details', N'N', N'N', N'1', N'/Cedant/CedantEditor', N'ADM', N'ADM', N'icon-user text-info', 231, N'N', N'Y', 256, N'N', N'Insurer Details', 150, 17, NULL, NULL, NULL, N'2', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 232)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (74, 232, N'S_ADMN', N'Nature of Loss Details', N'N', N'N', N'1', N'/ClaimMasters/LossNatureMasterEditor', N'ADM', N'ADM', N'icon-font text-facebook', 232, N'N', N'N', 0, N'N', N'Nature of Loss Details', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 233)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (75, 233, N'S_ADMN', N'Type of Loss Details', N'N', N'N', N'1', N'/ClaimMasters/Create', N'ADM', N'ADM', N'icon-globe text-gplus', 233, N'N', N'N', 0, N'N', N'Type of Loss Details', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 234)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (76, 234, N'S_ADMN', N'Main Class of Business Details', N'N', N'N', N'1', N'/ProductBusiness/ProductBusinessEditor', N'ADM', N'ADM', N'icon-dashboard text-danger', 234, N'N', N'N', 0, N'N', N'Main Class of Business Details', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 235)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (77, 235, N'S_ADMN', N'Sub Class Details', N'N', N'N', N'1', N'/ProductBusiness/SubClassEditor', N'ADM', N'ADM', N'icon-file-text text-success', 235, N'N', N'N', 0, N'N', N'Sub Class Details', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 236)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (78, 236, N'S_ADMN', N'Depot Master Details', N'N', N'N', N'1', N'/Masters/DepotMasterEditor', N'ADM', N'ADM', N'icon-suitcase text-info', 236, N'N', N'Y', 256, N'N', N'Depot Master Details', 150, 17, NULL, NULL, NULL, N'4', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 237)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (79, 237, N'CLAIM', N'Reserve', N'N', N'N', N'1', N'/ClaimProcessing/ReverseChange', N'CLM', N'ADM', N'', 237, N'N', N'N', 0, N'N', N'Reserve List', 150, 17, NULL, NULL, NULL, N'0', 1, N'N')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 238)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (80, 238, N'S_ADMN', N'Surveyor Master Details', N'N', N'N', N'1', N'/AdjusterMasters/SurveyorEditor', N'ADM', N'ADM', N'icon-eye-open text-facebook', 238, N'N', N'Y', 256, N'N', N'Surveyor Master Details', 150, 17, NULL, NULL, NULL, N'6', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 239)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (81, 239, N'CLAIM', N'Payment', N'N', N'N', N'1', N'/ClaimProcessing/ClaimPayment', N'CLM', N'ADM', N'', 239, N'N', N'N', 0, N'N', N'Payment List', 150, 17, NULL, NULL, NULL, N'0', 1, N'N')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 240)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (82, 240, N'S_ADMN', N'Adjuster Master Details', N'N', N'N', N'1', N'/AdjusterMasters/AdjusterEditor', N'ADM', N'ADM', N'icon-eye-open text-facebook', 240, N'N', N'Y', 256, N'N', N'Adjuster Master Details', 150, 17, NULL, NULL, NULL, N'10', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 241)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (83, 241, N'S_ADMN', N'Solicitor Master Details', N'N', N'N', N'1', N'/AdjusterMasters/SolicitorEditor', N'ADM', N'ADM', N'icon-legal text-gplus', 241, N'N', N'Y', 256, N'N', N'Solicitor Master Details', 150, 17, NULL, NULL, NULL, N'8', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 242)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (84, 242, N'S_ADMN', N'Claim Expense Master Details', N'N', N'N', N'1', N'/ClaimMasters/ClaimExpenseEditor', N'ADM', N'ADM', N'icon-leaf text-danger', 242, N'N', N'N', 0, N'N', N'Claim Expense Master Details', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 243)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (85, 243, N'S_ADMN', N'Claims Close Reason<br>Master', N'N', N'N', N'1', N'/ClaimMasters/ClaimCloseEditor', N'ADM', N'ADM', N'icon-compass text-success', 243, N'N', N'N', 0, N'N', N'Claims Close Reason Master Details', 150, 22, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 244)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (86, 244, N'S_ADMN', N'Currency Master Details', N'N', N'N', N'1', N'/ClaimMasters/CurrencyMasterEditor', N'ADM', N'ADM', N'icon-info-sign text-info', 244, N'N', N'N', 0, N'N', N'Currency Master Details', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 245)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (87, 245, N'S_ADMN', N'Exchange Rate Master Details', N'N', N'N', N'1', N'/ClaimMasters/ExchangeEditor', N'ADM', N'ADM', N'icon-cog text-warning', 245, N'N', N'N', 0, N'N', N'Exchange Rate Master Details', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 246)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (88, 246, N'S_ADMN', N'Country Master Details', N'N', N'N', N'1', N'/CountryMaster/CountryEditor', N'ADM', N'ADM', N'icon-globe text-gplus', 246, N'N', N'N', 0, N'N', N'Country Master Details', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 247)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (89, 247, N'S_ADMN', N'Vehicle Class Details', N'N', N'N', N'1', N'/Masters/VehicleClassEditor', N'ADM', N'ADM', N'icon-th-large text-success', 247, N'N', N'N', 0, N'N', N'Vehicle Class Details', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 248)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (90, 248, N'S_ADMN', N'Vehicle Make Details', N'N', N'N', N'1', N'/Masters/VehicleMaster', N'ADM', N'ADM', N'icon-unlink text-info', 248, N'N', N'N', 0, N'N', N'Vehicle Make Details', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 249)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (91, 249, N'S_ADMN', N'Vehicle Model Details', N'N', N'N', N'1', N'/Masters/VehicleMaster', N'ADM', N'ADM', N'icon-unlink text-info', 249, N'N', N'N', 0, N'N', N'Vehicle Model Details', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 250)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (92, 250, N'S_ADMN', N'Insurance Policy Details', N'N', N'N', N'1', N'/InsuranceMaster/InsurancePolicyMasterEditor', N'ADM', N'ADM', N'icon-user text-info', 250, N'N', N'N', 0, N'N', N'Insurance Policy Details', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 251)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (93, 251, N'S_ADMN', N'GST Setting Details', N'N', N'N', N'1', N'/InsuranceMaster/InsurancePolicyMasterEditor', N'ADM', N'ADM', N'icon-list-ol text-warning', 251, N'N', N'N', 0, N'N', N'GST Setting Details', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 252)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (94, 252, N'S_ADMN', N'Bus Captain Listing Details', N'N', N'N', N'1', N'/Masters/VehicleBusCaptainEditor', N'ADM', N'ADM', N'icon-list-ol text-warning', 252, N'N', N'N', 0, N'N', N'Bus Captain Listing Details', 150, 17, NULL, NULL, NULL, N'0', 1, N'N')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 253)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (95, 253, N'S_ADMN', N'Organization Country Details', N'N', N'N', N'1', N'/CountryMaster/OrgCountryEditor', N'ADM', N'ADM', N'icon-user text-info', 253, N'N', N'N', 0, N'N', N'Organization Country Details', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 254)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (96, 254, N'S_ADMN', N'Claim Officer Duty Details', N'N', N'N', N'1', N'/Masters/ClaimOfficerDutyEditor', N'ADM', N'ADM', N'icon-list-ol text-warning', 254, N'N', N'N', 0, N'N', N'Claim Officer Duty Details', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 255)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (97, 255, N'S_ADMN', N'LOU Rate Details', N'N', N'N', N'1', N'/Masters/LOUEditor', N'ADM', N'ADM', N'icon-list-ol text-warning', 255, N'N', N'N', 0, N'N', N'LOU Rate Details', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 256)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (98, 256, N'S_ADMN', N'Service Provider Master', N'Y', N'N', N'1', N'/Cedant/CedantIndex', N'ADM', N'ADM', N'icon-eye-open text-facebook', 256, N'Y', N'N', 0, N'N', N'Service Provider Master', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 257)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (99, 257, N'S_ADMN', N'Entity Details', N'N', N'N', N'1', N'UserAdmin/UserEntityIndex', N'ADM', N'ADM', N'icon-list-ol text-warning', 257, N'N', N'Y', 114, N'N', N'Entity Details', 114, 17, NULL, NULL, NULL, N'1', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 258)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (100, 258, N'S_ADMN', N'Entity Details', N'N', N'N', N'1', N'UserAdmin/UserEntityEditor', N'ADM', N'ADM', N'icon-list-ol text-warning', 258, N'N', N'Y', 114, N'N', N'Entity Details', 150, 17, NULL, NULL, NULL, N'2', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 259)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (101, 259, N'S_ADMN', N'User Groups', N'N', N'N', N'1', N'UserAdmin/UserGroupsIndex', N'ADM', N'ADM', N'icon-list-ol text-warning', 259, N'N', N'Y', 114, N'N', N'Entity Details', 150, 17, NULL, NULL, NULL, N'3', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 260)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (102, 260, N'S_ADMN', N'User Groups Details', N'N', N'N', N'1', N'UserAdmin/UserGroupsEditor', N'ADM', N'ADM', N'icon-list-ol text-warning', 260, N'N', N'Y', 114, N'N', N'User Groups Details', 150, 17, NULL, NULL, NULL, N'4', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 261)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (103, 261, N'S_ADMN', N'User', N'N', N'N', N'1', N'UserAdmin/UserAdminMastersList', N'ADM', N'ADM', N'icon-list-ol text-warning', 261, N'N', N'Y', 114, N'N', N'User', 150, 17, NULL, NULL, NULL, N'5', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 262)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (104, 262, N'S_ADMN', N'User Details', N'N', N'N', N'1', N'UserAdmin/UserAdminMastersEditor', N'ADM', N'ADM', N'icon-list-ol text-warning', 262, N'N', N'Y', 114, N'N', N'User Details', 150, 17, NULL, NULL, NULL, N'6', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 263)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (105, 263, N'S_ADMN', N'Department Master', N'N', N'N', N'1', N'UserAdmin/UserDeptIndex', N'ADM', N'ADM', N'icon-list-ol text-warning', 263, N'N', N'Y', 114, N'N', N'Department Master', 150, 17, NULL, NULL, NULL, N'7', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 264)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (106, 264, N'S_ADMN', N'Department Master Details', N'N', N'N', N'1', N'UserAdmin/UserDeptEditor', N'ADM', N'ADM', N'icon-list-ol text-warning', 264, N'N', N'Y', 114, N'N', N'Department Master Details', 150, 17, NULL, NULL, NULL, N'8', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 265)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (107, 265, N'S_ADMN', N'Password Setup', N'N', N'N', N'1', N'UserAdmin/PasswordSetUpEditor', N'ADM', N'ADM', N'icon-list-ol text-warning', 265, N'N', N'Y', 114, N'N', N'Password Setup', 150, 17, NULL, NULL, NULL, N'10', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 266)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (108, 266, N'S_ADMN', N'Release Locked Users', N'N', N'N', N'1', N'UserAdmin/ReleasedLockedUser', N'ADM', N'ADM', N'icon-list-ol text-warning', 266, N'N', N'Y', 114, N'N', N'Release Locked Users', 150, 17, NULL, NULL, NULL, N'11', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 267)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (109, 267, N'S_ADMN', N'Country for User Access', N'N', N'N', N'1', N'UserAdmin/UserCountryIndex', N'ADM', N'ADM', N'icon-list-ol text-warning', 267, N'N', N'Y', 114, N'N', N'Country for User Access', 150, 17, NULL, NULL, NULL, N'12', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 268)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (110, 268, N'S_ADMN', N'Country for User Access Details', N'N', N'N', N'1', N'UserAdmin/UserCountryEditor', N'ADM', N'ADM', N'icon-list-ol text-warning', 268, N'N', N'Y', 114, N'N', N'Country for User Access Details', 150, 17, NULL, NULL, NULL, N'13', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 269)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (111, 269, N'CLM_UPL', N'Upload', N'Y', N'N', N'1', N'#', N'CLMS', N'ADM', N'icon-briefcase text-danger', 269, N'Y', N'N', 0, N'N', N'Upload', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 270)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (112, 270, N'CLM_UPL', N'TAC Upload', N'Y', N'N', N'1', N'/ClaimProcessing/TacFileUploadEditor?claimMode=Upload', N'CLMS', N'ADM', N'icon-double-angle-right', 270, N'N', N'Y', 269, N'N', N'TAC Upload', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 271)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (113, 271, N'CLM_UPL', N'Claim Upload', N'Y', N'N', N'1', N'/ClaimProcessing/ClaimFileUploadEditor?claimMode=CUpload', N'CLMS', N'ADM', N'icon-double-angle-right', 271, N'N', N'Y', 269, N'N', N'Claim Upload', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 272)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (114, 272, N'TacUpload', N'TAC File Upload', N'Y', N'N', N'1', N'/ClaimProcessing/TacFileUploadEditor', N'CLM', N'ADM', N'', 272, N'N', N'N', 0, N'N', N'TAC File Upload', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 273)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (115, 273, N'TacUpload', N'View TAC File Upload Status', N'Y', N'N', N'1', N'/ClaimProcessing/ViewTacFileUploadStatusEditor', N'CLM', N'ADM', N'', 273, N'N', N'N', 0, N'N', N'View TAC File Upload Status', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 274)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (116, 274, N'CUpload', N'Claim File Upload', N'Y', N'N', N'1', N'/ClaimProcessing/ClaimFileUploadEditor', N'CLM', N'ADM', N'', 274, N'N', N'N', 0, N'N', N'Claim File Upload', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 275)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (117, 275, N'CUpload', N'View Claim File Upload Status', N'Y', N'N', N'1', N'/ClaimProcessing/ViewClaimFileUploadStatusEditor', N'CLM', N'ADM', N'', 275, N'N', N'N', 0, N'N', N'View Claim File Upload Status', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 276)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (118, 276, N'CLM_ENQ', N'Job Schedule Enquiry', N'Y', N'N', N'1', N'/ClaimProcessing/JobScheduleEnquiry?claimMode=JobScheduleEnquiry', N'CLMS', N'ADM', N'icon-double-angle-right', 276, N'N', N'Y', 217, N'N', N'Job Schedule Enquiry', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 277)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (119, 277, N'S_ADMN', N'Interchange ', N'Y', N'N', N'1', N'/Masters/InterChangeIndex', N'ADM', N'ADM', N'icon-double-angle-right', 277, N'N', N'N', 0, N'N', N'Interchange', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 278)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (120, 278, N'S_ADMN', N'Interchange Details ', N'N', N'N', N'1', N'/Masters/InterChangeEditor', N'ADM', N'ADM', N'icon-double-angle-right', 278, N'N', N'N', 0, N'N', N'Interchange Details', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [mnt_menus]
  WHERE MenuId = 279)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsActive])
    VALUES (121, 279, N'S_ADMN', N'Deductible Details', N'Y', N'N', N'1', N'/Masters/DeductibleEditor', N'ADM', N'ADM', N'icon-double-angle-right', 279, N'N', N'N', 0, N'N', N'Deductible Details', 150, 17, NULL, NULL, NULL, N'0', 1, N'Y')
END