IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 100)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 1,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Insurer',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Cedant/CedantIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-user text-info',
      [DisplayOrder] = 100,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Insurer',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = 'N'
  WHERE [MenuId] = 100
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 101)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 2,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Vehicle Listing Upload',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Masters/VehicleUploadIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-signin text-warning',
      [DisplayOrder] = 101,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Vehicle Listing Upload',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = 'Y'
  WHERE [MenuId] = 101
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 102)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 3,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Nature of Loss',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimMasters/LossNatureMasterList',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-font text-facebook',
      [DisplayOrder] = 102,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Nature of Loss',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = 'Y'
  WHERE [MenuId] = 102
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 103)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 4,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Type of Loss',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimMasters/Index',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-globe text-gplus',
      [DisplayOrder] = 103,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Type of Loss',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = 'Y'
  WHERE [MenuId] = 103
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 104)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 5,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Main Class of Business',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ProductBusiness/ProductBusinessIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-dashboard text-danger',
      [DisplayOrder] = 104,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Main Class of Business',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = 'Y'
  WHERE [MenuId] = 104
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 105)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 6,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Sub-Class',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ProductBusiness/SubClassIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-file-text text-success',
      [DisplayOrder] = 105,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Sub-Class',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = 'Y'
  WHERE [MenuId] = 105
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 106)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 7,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Depot Master',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Masters/DepotMasterIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-suitcase text-info',
      [DisplayOrder] = 106,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Depot Master',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = 'N'
  WHERE [MenuId] = 106
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 107)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 8,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Surveyor Master',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/AdjusterMasters/SurveyorIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-sitemap text-warning',
      [DisplayOrder] = 107,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Surveyor Master',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = 'N'
  WHERE [MenuId] = 107
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 108)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 9,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Adjuster Master',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/AdjusterMasters/AdjusterIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-eye-open text-facebook',
      [DisplayOrder] = 108,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Adjuster Master',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = 'N'
  WHERE [MenuId] = 108
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 109)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 10,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Solicitor  Master',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/AdjusterMasters/SolicitorIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-legal text-gplus',
      [DisplayOrder] = 109,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Lawyer Master',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = 'N'
  WHERE [MenuId] = 109
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 110)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 11,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Claims Expense Master',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimMasters/ClaimExpenseIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-leaf text-danger',
      [DisplayOrder] = 110,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Claims Expense Master',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 110
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 111)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 12,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Claims Status Master',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = 'claims-status-master.html',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-compass text-success',
      [DisplayOrder] = 111,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Claims Status Master',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'N'
  WHERE [MenuId] = 111
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 112)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 13,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Currency Master',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimMasters/CurrencyMasterIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-info-sign text-info',
      [DisplayOrder] = 112,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Currency Master',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 112
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 113)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 14,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Exchange Rate Master',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimMasters/ExchangeIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-cog text-warning',
      [DisplayOrder] = 113,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Exchange Rate Master',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 113
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 114)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 15,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'User Admin',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/UserAdmin/Index',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-user text-facebook',
      [DisplayOrder] = 114,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'User Admin',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 114
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 115)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 16,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Country Master',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/CountryMaster/Index',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-globe text-gplus',
      [DisplayOrder] = 115,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Country Master',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 115
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 116)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 17,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Diary and Follow-Up',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = 'diary-and-follow-up-master.html',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-bell text-danger',
      [DisplayOrder] = 116,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Diary and Follow-Up',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'N'
  WHERE [MenuId] = 116
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 117)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 18,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Vehicle Type',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = 'vehicle-type.html',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-th-large text-success',
      [DisplayOrder] = 117,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Vehicle Type',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'N'
  WHERE [MenuId] = 117
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 118)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 19,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Vehicle Make and Model',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = 'Masters/VehicleIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-unlink text-info',
      [DisplayOrder] = 118,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Vehicle Make and Model',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'N'
  WHERE [MenuId] = 118
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 119)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 20,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Bus Captain Listing',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Masters/VehicleBusCaptainIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-list-ol text-warning',
      [DisplayOrder] = 119,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Bus Captain Listing',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 119
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 200)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 21,
      [TabId] = 'DASH',
      [DisplayTitle] = 'Dashboard',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Home/Index',
      [ProductName] = 'DASH',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-dashboard text-white',
      [DisplayOrder] = 200,
      [IsHeader] = 'Y',
      [SubMenu] = 'N',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Dashboard',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 200
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 201)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 22,
      [TabId] = 'DIARY',
      [DisplayTitle] = 'Diary Listing',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '#',
      [ProductName] = 'DIARY',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-list-ol text-blue',
      [DisplayOrder] = 201,
      [IsHeader] = 'Y',
      [SubMenu] = 'N',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Diary Listing',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 201
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 202)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 23,
      [TabId] = 'DIARY',
      [DisplayTitle] = 'Diaried items',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Home/Index',
      [ProductName] = 'DIARY',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-double-angle-right',
      [DisplayOrder] = 202,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 201,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Diaried items',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 202
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 203)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 24,
      [TabId] = 'DIARY',
      [DisplayTitle] = 'Assigned Tasks',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Home/Index',
      [ProductName] = 'DIARY',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-double-angle-right',
      [DisplayOrder] = 203,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 201,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Assigned Tasks',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 203
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 204)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 25,
      [TabId] = 'DIARY',
      [DisplayTitle] = 'Escalation Tasks',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Home/Index',
      [ProductName] = 'DIARY',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-double-angle-right',
      [DisplayOrder] = 204,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 201,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Escalation Tasks',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 204
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 205)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 26,
      [TabId] = 'CLM_REG',
      [DisplayTitle] = 'Claims Registration',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '#',
      [ProductName] = 'CLMS',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-file-text text-orange',
      [DisplayOrder] = 205,
      [IsHeader] = 'Y',
      [SubMenu] = 'N',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Claims Registration',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 205
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 206)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 27,
      [TabId] = 'CLM_REG',
      [DisplayTitle] = 'New Claims Registration',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimRegistration?claimMode=New',
      [ProductName] = 'CLMS',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-double-angle-right',
      [DisplayOrder] = 206,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 205,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'New Claims Registration',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 206
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 207)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 28,
      [TabId] = 'CLM_REG',
      [DisplayTitle] = 'Incomplete Claims Registration',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimRegistration?claimMode=Incomplete',
      [ProductName] = 'CLMS',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-double-angle-right',
      [DisplayOrder] = 207,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 205,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Incomplete Claims Registration',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 207
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 208)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 29,
      [TabId] = 'CLM_REG',
      [DisplayTitle] = 'Claims Adjustments',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimRegistration?claimMode=Adjustment',
      [ProductName] = 'CLMS',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-double-angle-right',
      [DisplayOrder] = 208,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 205,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Claims Adjustments',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 208
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 209)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 30,
      [TabId] = 'CLM_REG',
      [DisplayTitle] = 'Claims Enquiry',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimRegistration?claimMode=RegEnquiry',
      [ProductName] = 'CLMS',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-double-angle-right',
      [DisplayOrder] = 209,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 205,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Claims Enquiry',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 209
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 210)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 31,
      [TabId] = 'CLM_PAY',
      [DisplayTitle] = 'Claims Payment',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '#',
      [ProductName] = 'CLMS',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-dollar text-blue',
      [DisplayOrder] = 210,
      [IsHeader] = 'Y',
      [SubMenu] = 'N',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Claims Payment',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 210
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 211)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 32,
      [TabId] = 'CLM_PAY',
      [DisplayTitle] = 'Claims Payment Processing',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimPaymentProcessing?claimMode=Payment',
      [ProductName] = 'CLMS',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-double-angle-right',
      [DisplayOrder] = 211,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 210,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Claims Payment Processing',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 211
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 212)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 33,
      [TabId] = 'CLM_PAY',
      [DisplayTitle] = 'Incomplete Claims Payment Registration',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimPaymentProcessing?claimMode=Payment',
      [ProductName] = 'CLMS',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-double-angle-right',
      [DisplayOrder] = 212,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 210,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Incomplete Claims Payment Registration',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 212
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 213)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 34,
      [TabId] = 'CLM_PAY',
      [DisplayTitle] = 'Claims Payment Document Enquiry',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimPaymentProcessing?claimMode=Payment',
      [ProductName] = 'CLMS',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-double-angle-right',
      [DisplayOrder] = 213,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 210,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Claims Payment Document Enquiry',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 213
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 214)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 35,
      [TabId] = 'CLM_REC',
      [DisplayTitle] = 'Claims Recovery',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '#',
      [ProductName] = 'CLMS',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-briefcase text-danger',
      [DisplayOrder] = 214,
      [IsHeader] = 'Y',
      [SubMenu] = 'N',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Claims Recovery',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 214
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 215)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 36,
      [TabId] = 'CLM_REC',
      [DisplayTitle] = 'Claims Recovery Processing',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimRecoveryProcessing/ClaimRecoveryProcessing?claimMode=Recovery',
      [ProductName] = 'CLMS',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-double-angle-right',
      [DisplayOrder] = 215,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 214,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Claims Recovery Processing',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 215
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 216)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 37,
      [TabId] = 'CLM_REC',
      [DisplayTitle] = 'Claims Recovery',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimRecoveryProcessing/ClaimRecoveryProcessing?claimMode=Recovery',
      [ProductName] = 'CLMS',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-double-angle-right',
      [DisplayOrder] = 216,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 214,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Claims Recovery',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 216
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 217)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 38,
      [TabId] = 'CLM_ENQ',
      [DisplayTitle] = 'Enquiry',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '#',
      [ProductName] = 'CLMS',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-book text-golden',
      [DisplayOrder] = 217,
      [IsHeader] = 'Y',
      [SubMenu] = 'N',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Enquiry',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 217
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 218)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 39,
      [TabId] = 'CLM_ENQ',
      [DisplayTitle] = 'Claims',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimEnquiry?claimMode=Enquiry',
      [ProductName] = 'CLMS',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-double-angle-right',
      [DisplayOrder] = 218,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 217,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Claims',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 218
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 219)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 40,
      [TabId] = 'CLM_ENQ',
      [DisplayTitle] = 'Claims Documents Printed',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimMasters/ClaimDocumentsPrintedIndex?claimMode=EnqDocPrinted',
      [ProductName] = 'CLMS',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-double-angle-right',
      [DisplayOrder] = 219,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 217,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Claims Documents Printed',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 219
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 120)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 41,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Vehicle Class ',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Masters/VehicleClassIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-th-large text-success',
      [DisplayOrder] = 120,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Vehicle Class',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 120
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 121)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 42,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Vehicle Make',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Masters/VehicleIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-unlink text-info',
      [DisplayOrder] = 121,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Vehicle Make ',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 121
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 122)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 43,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Vehicle Model',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Masters/VModelIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-unlink text-info',
      [DisplayOrder] = 122,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Vehicle Model',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 122
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 123)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 44,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Insurance Policy',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/InsuranceMaster/InsurancePolicyMasterIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-unlink text-info',
      [DisplayOrder] = 123,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Insurance Policy',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 123
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 124)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 45,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'GST Setting',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimMasters/GSTIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-list-ol text-warning',
      [DisplayOrder] = 124,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'GST Setting',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 124
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 125)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 46,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Organization Country',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/CountryMaster/OrgCountryIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-user text-info',
      [DisplayOrder] = 125,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Organization Country',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'N'
  WHERE [MenuId] = 125
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 126)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 47,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Claim Officer Duty',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Masters/ClaimOfficerDutyIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-list-ol text-warning',
      [DisplayOrder] = 126,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Claim Officer Duty',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 126
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 127)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 48,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'LOU Rate',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Masters/LOUIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-list-ol text-warning',
      [DisplayOrder] = 127,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'LOU Rate',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 127
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 128)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 49,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Hospital Information ',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimMasters/HospitalIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-th-large text-success',
      [DisplayOrder] = 128,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Hospital Information ',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'N'
  WHERE [MenuId] = 128
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 129)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 50,
      [TabId] = 'DAIRY',
      [DisplayTitle] = 'Re Assignment Editor',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ReAssignmentEditorP',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 129,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Re Assignment Editor',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 129
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 130)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 51,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'Accident',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimAccidentEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 130,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Accident',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 130
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 131)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 52,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'Own Damage',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/OwnDamage',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 131,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Own Damage',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 131
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 132)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 53,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'PD/BI',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/ThirdPartyEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 132,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'PD/BI Editor',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 132
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 133)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 54,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'Notes',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimNotesEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 133,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = ' Notes Editor',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 133
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 134)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 55,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'Tasks',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/TaskEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 134,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Tasks Editor',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 134
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 135)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 56,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'Mandate',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimMandateReqEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 135,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Mandate',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 135
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 136)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 57,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'Attachments',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimAttachmentsEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 136,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Attachments Editor',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 136
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 137)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 58,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'Diary',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/DiaryTaskEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 137,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Diary Editor',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 137
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 138)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 59,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'Reserve',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimReserveEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 138,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Reserve Editor',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 138
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 139)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 60,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'Payment',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/PaymentEditorNew',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 139,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Payment Editor',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 139
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 140)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 61,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'Transactions History',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/TransactionEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 140,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Transactions History',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 140
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 220)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 62,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Hospital Information Editor',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimMasters/HospitalEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 220,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Hospital Information Editor',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'N'
  WHERE [MenuId] = 220
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 221)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 63,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'Folder1',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAttachmentsEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 221,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Folder1',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 221
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 222)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 64,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'Folder2',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAttachmentsEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 222,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Folder2',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 222
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 223)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 65,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'Folder3',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAttachmentsEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 223,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Folder3',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 223
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 224)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 66,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'Folder4',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = 'CedantIndex,Cedant,/MCAS.Web/ClaimProcessing/ClaimAttachmentsEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 224,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Folder4',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 224
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 225)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 67,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'Attachments',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimAttachmentsList',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 225,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Attachments List',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 225
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 226)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 68,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'PD/BI',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/ThirdPartyList',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 226,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'PD/BI List',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 226
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 227)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 69,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'Notes',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimNotesList',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 227,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Notes List',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 227
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 228)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 70,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'Tasks',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/TaskIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 228,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Tasks List',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 228
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 229)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 71,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'Mandate',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimMandateList',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 229,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Mandate',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 229
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 230)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 72,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'Diary',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/ToDoList',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 230,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Diary List',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 230
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 231)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 73,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Insurer Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Cedant/CedantEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-user text-info',
      [DisplayOrder] = 231,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Insurer Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'N'
  WHERE [MenuId] = 231
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 232)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 74,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Nature of Loss Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimMasters/LossNatureMasterEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-font text-facebook',
      [DisplayOrder] = 232,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Nature of Loss Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 232
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 233)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 75,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Type of Loss Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimMasters/Create',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-globe text-gplus',
      [DisplayOrder] = 233,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Type of Loss Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 233
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 234)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 76,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Main Class of Business Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ProductBusiness/ProductBusinessEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-dashboard text-danger',
      [DisplayOrder] = 234,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Main Class of Business Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 234
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 235)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 77,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Sub Class Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ProductBusiness/SubClassEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-file-text text-success',
      [DisplayOrder] = 235,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Sub Class Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 235
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 236)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 78,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Depot Master Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Masters/DepotMasterEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-suitcase text-info',
      [DisplayOrder] = 236,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Depot Master Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'N'
  WHERE [MenuId] = 236
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 237)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 79,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'Reserve',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/ReverseChange',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 237,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Reserve List',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 237
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 238)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 80,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Surveyor Master Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/AdjusterMasters/SurveyorEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-eye-open text-facebook',
      [DisplayOrder] = 238,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Surveyor Master Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'N'
  WHERE [MenuId] = 238
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 239)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 81,
      [TabId] = 'CLAIM',
      [DisplayTitle] = 'Payment',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimProcessing/ClaimPayment',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = '',
      [DisplayOrder] = 239,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Payment List',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 239
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 240)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 82,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Adjuster Master Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/AdjusterMasters/AdjusterEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-eye-open text-facebook',
      [DisplayOrder] = 240,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Adjuster Master Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'N'
  WHERE [MenuId] = 240
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 241)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 83,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Solicitor Master Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/AdjusterMasters/SolicitorEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-legal text-gplus',
      [DisplayOrder] = 241,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Solicitor Master Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'N'
  WHERE [MenuId] = 241
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 242)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 84,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Claim Expense Master Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimMasters/ClaimExpenseEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-leaf text-danger',
      [DisplayOrder] = 242,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Claim Expense Master Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 242
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 243)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 85,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Claims Close Reason<br>Master',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimMasters/ClaimCloseIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-compass text-success',
      [DisplayOrder] = 243,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Claims Close Reason Master Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 22,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 243
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 244)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 86,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Currency Master Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimMasters/CurrencyMasterEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-info-sign text-info',
      [DisplayOrder] = 244,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Currency Master Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 244
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 245)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 87,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Exchange Rate Master Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/ClaimMasters/ExchangeEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-cog text-warning',
      [DisplayOrder] = 245,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Exchange Rate Master Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 245
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 246)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 88,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Country Master Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/CountryMaster/CountryEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-globe text-gplus',
      [DisplayOrder] = 246,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Country Master Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 246
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 247)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 89,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Vehicle Class Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Masters/VehicleClassEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-th-large text-success',
      [DisplayOrder] = 247,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Vehicle Class Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 247
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 248)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 90,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Vehicle Make Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Masters/VehicleMaster',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-unlink text-info',
      [DisplayOrder] = 248,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Vehicle Make Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 248
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 249)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 91,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Vehicle Model Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Masters/VehicleMaster',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-unlink text-info',
      [DisplayOrder] = 249,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Vehicle Model Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 249
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 250)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 92,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Insurance Policy Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/InsuranceMaster/InsurancePolicyMasterEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-user text-info',
      [DisplayOrder] = 250,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Insurance Policy Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 250
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 251)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 93,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'GST Setting Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/InsuranceMaster/InsurancePolicyMasterEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-list-ol text-warning',
      [DisplayOrder] = 251,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'GST Setting Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 251
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 252)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 94,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Bus Captain Listing Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Masters/VehicleBusCaptainEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-list-ol text-warning',
      [DisplayOrder] = 252,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Bus Captain Listing Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 252
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 253)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 95,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Organization Country Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/CountryMaster/OrgCountryEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-user text-info',
      [DisplayOrder] = 253,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Organization Country Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'N'
  WHERE [MenuId] = 253
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 254)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 96,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Claim Officer Duty Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Masters/ClaimOfficerDutyEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-list-ol text-warning',
      [DisplayOrder] = 254,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Claim Officer Duty Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 254
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 255)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 97,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'LOU Rate Details',
      [IsMenuItem] = 'N',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Masters/LOUEditor',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-list-ol text-warning',
      [DisplayOrder] = 255,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'LOU Rate Details',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 255
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = 256)
BEGIN
  UPDATE [dbo].[MNT_Menus]
  SET [TId] = 98,
      [TabId] = 'S_ADMN',
      [DisplayTitle] = 'Service Provider Master',
      [IsMenuItem] = 'Y',
      [IsJsMenuItem] = 'N',
      [VirtualSource] = '1',
      [Hyp_Link_Address] = '/Cedant/CedantIndex',
      [ProductName] = 'ADM',
      [UserType] = 'ADM',
      [Displayimg] = 'icon-eye-open text-facebook',
      [DisplayOrder] = 256,
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = 0,
      [IsAdmin] = 'N',
      [AdminDisplayText] = 'Service Provider Master',
      [MenuItemWidth] = 150,
      [MenuItemHeight] = 17,
      [ErrorMessDesc] = NULL,
      [ErrorMessTitle] = NULL,
      [ErrorMessHead] = NULL,
      [SubTabId] = NULL,
      [LangId] = 1,
      [IsActive] = N'Y'
  WHERE [MenuId] = 256
END