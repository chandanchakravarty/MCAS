-- Shopping Center
INSERT INTO [dbo].[MNT_MENU_LIST]
           ([MENU_ID]
           ,[PARENT_ID]
           ,[MENU_NAME]
           ,[MENU_LINK]
           ,[MENU_TOOLTIP]
           ,[NESTLEVEL]
           ,[MENU_ORDER]
           ,[HASCHILD]
           ,[SHOWTYPE]
           ,[HIDESTATUS]
           ,[ISSECURITY]
           ,[LANGID]
           ,[MENU_IMAGE]
           ,[DEFAULT_PAGE]
           ,[MODULE_NAME]
           ,[AGENCY_LEVEL]
           ,[LOB_CODE]
           ,[IS_ACTIVE])
	SELECT 573
		  ,562
		  ,'Shopping/Strip Center'
		  ,'/cms/Policies/Aspx/BOP/AddShop.aspx'
		  ,NULL
		  ,0
		  ,16
		  ,0
		  ,NULL
		  ,NULL
		  ,NULL
		  ,NULL
		  ,NULL
		  ,NULL
		  ,NULL
		  ,1
		  ,'BOP'
		  ,'Y'
  FROM [dbo].[MNT_MENU_LIST]
  WHERE MENU_ID = 50
  
  GO
  
  -- Warehouse
  
  INSERT INTO [dbo].[MNT_MENU_LIST]
           ([MENU_ID]
           ,[PARENT_ID]
           ,[MENU_NAME]
           ,[MENU_LINK]
           ,[MENU_TOOLTIP]
           ,[NESTLEVEL]
           ,[MENU_ORDER]
           ,[HASCHILD]
           ,[SHOWTYPE]
           ,[HIDESTATUS]
           ,[ISSECURITY]
           ,[LANGID]
           ,[MENU_IMAGE]
           ,[DEFAULT_PAGE]
           ,[MODULE_NAME]
           ,[AGENCY_LEVEL]
           ,[LOB_CODE]
           ,[IS_ACTIVE])
	SELECT 574
		  ,562
		  ,'Warehouse'
		  ,'/cms/Policies/Aspx/BOP/AddWarehouseDetail.aspx'
		  ,NULL
		  ,0
		  ,17
		  ,0
		  ,NULL
		  ,NULL
		  ,NULL
		  ,NULL
		  ,NULL
		  ,NULL
		  ,NULL
		  ,1
		  ,'BOP'
		  ,'Y'
  FROM [dbo].[MNT_MENU_LIST]
  WHERE MENU_ID = 50
  
  GO
  
  --- Restaurant
  
  INSERT INTO [dbo].[MNT_MENU_LIST]
           ([MENU_ID]
           ,[PARENT_ID]
           ,[MENU_NAME]
           ,[MENU_LINK]
           ,[MENU_TOOLTIP]
           ,[NESTLEVEL]
           ,[MENU_ORDER]
           ,[HASCHILD]
           ,[SHOWTYPE]
           ,[HIDESTATUS]
           ,[ISSECURITY]
           ,[LANGID]
           ,[MENU_IMAGE]
           ,[DEFAULT_PAGE]
           ,[MODULE_NAME]
           ,[AGENCY_LEVEL]
           ,[LOB_CODE]
           ,[IS_ACTIVE])
	SELECT 575
		  ,562
		  ,'Restaurant'
		  ,'/cms/Policies/Aspx/BOP/AddRestaurant.aspx'
		  ,NULL
		  ,0
		  ,18
		  ,0
		  ,NULL
		  ,NULL
		  ,NULL
		  ,NULL
		  ,NULL
		  ,NULL
		  ,NULL
		  ,1
		  ,'BOP'
		  ,'Y'
  FROM [dbo].[MNT_MENU_LIST]
  WHERE MENU_ID = 50
  
  GO
  
  -- CONTRACTORS
  
  INSERT INTO [dbo].[MNT_MENU_LIST]
           ([MENU_ID]
           ,[PARENT_ID]
           ,[MENU_NAME]
           ,[MENU_LINK]
           ,[MENU_TOOLTIP]
           ,[NESTLEVEL]
           ,[MENU_ORDER]
           ,[HASCHILD]
           ,[SHOWTYPE]
           ,[HIDESTATUS]
           ,[ISSECURITY]
           ,[LANGID]
           ,[MENU_IMAGE]
           ,[DEFAULT_PAGE]
           ,[MODULE_NAME]
           ,[AGENCY_LEVEL]
           ,[LOB_CODE]
           ,[IS_ACTIVE])
	SELECT 576
		  ,562
		  ,'Contractors'
		  ,'/cms/Policies/Aspx/BOP/AddContractorsDetail.aspx'
		  ,NULL
		  ,0
		  ,19
		  ,0
		  ,NULL
		  ,NULL
		  ,NULL
		  ,NULL
		  ,NULL
		  ,NULL
		  ,NULL
		  ,1
		  ,'BOP'
		  ,'Y'
  FROM [dbo].[MNT_MENU_LIST]
  WHERE MENU_ID = 50
  
  GO
  
    --  OLD BUILDING
  
  INSERT INTO [dbo].[MNT_MENU_LIST]
           ([MENU_ID]
           ,[PARENT_ID]
           ,[MENU_NAME]
           ,[MENU_LINK]
           ,[MENU_TOOLTIP]
           ,[NESTLEVEL]
           ,[MENU_ORDER]
           ,[HASCHILD]
           ,[SHOWTYPE]
           ,[HIDESTATUS]
           ,[ISSECURITY]
           ,[LANGID]
           ,[MENU_IMAGE]
           ,[DEFAULT_PAGE]
           ,[MODULE_NAME]
           ,[AGENCY_LEVEL]
           ,[LOB_CODE]
           ,[IS_ACTIVE])
	SELECT 577
		  ,562
		  ,'Old Buildings'
		  ,'/cms/Policies/Aspx/BOP/AddOldBuildDetails.aspx'
		  ,NULL
		  ,0
		  ,20
		  ,0
		  ,NULL
		  ,NULL
		  ,NULL
		  ,NULL
		  ,NULL
		  ,NULL
		  ,NULL
		  ,1
		  ,'BOP'
		  ,'Y'
  FROM [dbo].[MNT_MENU_LIST]
  WHERE MENU_ID = 50
  
  GO