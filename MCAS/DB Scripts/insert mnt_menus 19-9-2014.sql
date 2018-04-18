IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 120)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (41, 120, N'S_ADMN', N'Vehicle Class  ', N'Y', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/Masters/VehicleClassIndex', N'ADM', N'ADM', N'icon-th-large text-success', 120, N'N', N'Y', 0, N'N', N'Vehicle Class', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 121)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (42, 121, N'S_ADMN', N'Vehicle Make', N'Y', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/Masters/VehicleIndex', N'ADM', N'ADM', N'icon-unlink text-info', 121, N'N', N'Y', 0, N'N', N'Vehicle Make ', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 230)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (72, 230, N'CLAIM', N'Diary', N'N', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ToDoList', N'ADM', N'ADM', N'', 230, N'N', N'Y', 0, N'N', N'Diary', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 231)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (73, 231, N'S_ADMN', N'Insurer Details', N'Y', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/Cedant/CedantEditor', N'ADM', N'ADM', N'icon-user text-info', 231, N'N', N'Y', 0, N'N', N'Insurer Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 232)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (74, 232, N'S_ADMN', N'Nature of Loss Details', N'Y', N'N', N'1', N'ClaimMasters/LossNatureMasterEditor', N'ADM', N'ADM', N'icon-font text-facebook', 232, N'N', N'Y', 0, N'N', N'Nature of Loss Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 233)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (75, 233, N'S_ADMN', N'Type of Loss Details', N'Y', N'N', N'1', N'ClaimMasters/Create', N'ADM', N'ADM', N'icon-globe text-gplus', 233, N'N', N'Y', 0, N'N', N'Type of Loss Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 234)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (76, 234, N'S_ADMN', N'Main Class of Business Details', N'Y', N'N', N'1', N'ProductBusiness/ProductBusinessEditor', N'ADM', N'ADM', N'icon-dashboard text-danger', 234, N'N', N'Y', 0, N'N', N'Main Class of Business Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 235)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (77, 235, N'S_ADMN', N'Sub Class Details', N'Y', N'N', N'1', N'ProductBusiness/SubClassEditor', N'ADM', N'ADM', N'icon-file-text text-success', 235, N'N', N'Y', 0, N'N', N'Sub Class Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 236)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (78, 236, N'S_ADMN', N'Depot Master Details', N'Y', N'N', N'1', N'Masters/DepotMasterEditor', N'ADM', N'ADM', N'icon-suitcase text-info', 236, N'N', N'Y', 0, N'N', N'Depot Master Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 237)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (79, 237, N'CLAIM', N'Reserve', N'N', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ReverseChange', N'ADM', N'ADM', N'', 237, N'N', N'Y', 0, N'N', N'Reserve', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 238)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (80, 238, N'S_ADMN', N'Surveyor Master Details', N'Y', N'N', N'1', N'AdjusterMasters/SurveyorEditor', N'ADM', N'ADM', N'icon-eye-open text-facebook', 238, N'N', N'Y', 0, N'N', N'Surveyor Master Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 239)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (81, 239, N'CLAIM', N'Payment', N'N', N'N', N'1', N'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimPayment', N'ADM', N'ADM', N'', 239, N'N', N'Y', 0, N'N', N'Payment', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 240)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (82, 240, N'S_ADMN', N'Adjuster Master Details', N'Y', N'N', N'1', N'AdjusterMasters/AdjusterEditor', N'ADM', N'ADM', N'icon-eye-open text-facebook', 240, N'N', N'Y', 0, N'N', N'Adjuster Master Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 241)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (83, 241, N'S_ADMN', N'Solicitor Master Details', N'Y', N'N', N'1', N'AdjusterMasters/SolicitorEditor', N'ADM', N'ADM', N'icon-legal text-gplus', 241, N'N', N'Y', 0, N'N', N'Solicitor Master Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 242)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (84, 242, N'S_ADMN', N'Claim Expense Master Details', N'Y', N'N', N'1', N'ClaimMasters/ClaimExpenseEditor', N'ADM', N'ADM', N'icon-leaf text-danger', 242, N'N', N'Y', 0, N'N', N'Claim Expense Master Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 243)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (85, 243, N'S_ADMN', N'Claims Close Reason Master Details', N'Y', N'N', N'1', N'ClaimMasters/ClaimCloseEditor', N'ADM', N'ADM', N'icon-compass text-success', 243, N'N', N'Y', 0, N'N', N'Claims Close Reason Master Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 244)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (86, 244, N'S_ADMN', N'Currency Master Details', N'Y', N'N', N'1', N'ClaimMasters/CurrencyMasterEditor', N'ADM', N'ADM', N'icon-info-sign text-info', 244, N'N', N'Y', 0, N'N', N'Currency Master Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 245)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (87, 245, N'S_ADMN', N'Exchange Rate Master Details', N'Y', N'N', N'1', N'ClaimMasters/ExchangeEditor', N'ADM', N'ADM', N'icon-cog text-warning', 245, N'N', N'Y', 0, N'N', N'Exchange Rate Master Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 246)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (88, 246, N'S_ADMN', N'Country Master Details', N'Y', N'N', N'1', N'CountryMaster/CountryEditor', N'ADM', N'ADM', N'icon-globe text-gplus', 246, N'N', N'Y', 0, N'N', N'Country Master Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 247)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (89, 247, N'S_ADMN', N'Vehicle Class Details', N'Y', N'N', N'1', N'Masters/VehicleClassEditor', N'ADM', N'ADM', N'icon-th-large text-success', 247, N'N', N'Y', 0, N'N', N'Vehicle Class Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 248)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (90, 248, N'S_ADMN', N'Vehicle Make Details', N'Y', N'N', N'1', N'Masters/VehicleMaster', N'ADM', N'ADM', N'icon-unlink text-info', 248, N'N', N'Y', 0, N'N', N'Vehicle Make Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 249)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (91, 249, N'S_ADMN', N'Vehicle Model Details', N'Y', N'N', N'1', N'Masters/VehicleMaster', N'ADM', N'ADM', N'icon-unlink text-info', 249, N'N', N'Y', 0, N'N', N'Vehicle Model Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 250)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (92, 250, N'S_ADMN', N'Insurance Policy Details', N'Y', N'N', N'1', N'InsuranceMaster/InsurancePolicyMasterEditor', N'ADM', N'ADM', N'icon-user text-info', 249, N'N', N'Y', 0, N'N', N'Insurance Policy Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 251)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (93, 251, N'S_ADMN', N'GST Setting Details', N'Y', N'N', N'1', N'InsuranceMaster/InsurancePolicyMasterEditor', N'ADM', N'ADM', N'icon-list-ol text-warning', 251, N'N', N'Y', 0, N'N', N'GST Setting Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 252)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (94, 252, N'S_ADMN', N'Bus Captain Listing Details', N'Y', N'N', N'1', N'Masters/VehicleBusCaptainEditor', N'ADM', N'ADM', N'icon-list-ol text-warning', 252, N'N', N'Y', 0, N'N', N'Bus Captain Listing Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 253)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (95, 253, N'S_ADMN', N'Organization Country Details', N'Y', N'N', N'1', N'CountryMaster/OrgCountryEditor', N'ADM', N'ADM', N'icon-user text-info', 252, N'N', N'Y', 0, N'N', N'Organization Country Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 254)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (96, 254, N'S_ADMN', N'Claim Officer Duty Details', N'Y', N'N', N'1', N'Masters/ClaimOfficerDutyEditor', N'ADM', N'ADM', N'icon-list-ol text-warning', 252, N'N', N'Y', 0, N'N', N'Claim Officer Duty Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [MNT_Menus]
  WHERE [MenuId] = 255)
BEGIN
  INSERT [dbo].[MNT_Menus] ([TId], [MenuId], [TabId], [DisplayTitle], [IsMenuItem], [IsJsMenuItem], [VirtualSource], [Hyp_Link_Address], [ProductName], [UserType], [Displayimg], [DisplayOrder], [IsHeader], [SubMenu], [MainHeaderId], [IsAdmin], [AdminDisplayText], [MenuItemWidth], [MenuItemHeight], [ErrorMessDesc], [ErrorMessTitle], [ErrorMessHead], [SubTabId], [LangId], [IsExists])
    VALUES (97, 255, N'S_ADMN', N'LOU Rate Details', N'Y', N'N', N'1', N'Masters/LOUEditor', N'ADM', N'ADM', N'icon-list-ol text-warning', 252, N'N', N'Y', 0, N'N', N'LOU Rate Details', 150, 17, NULL, NULL, NULL, NULL, 1, N'Y')
END