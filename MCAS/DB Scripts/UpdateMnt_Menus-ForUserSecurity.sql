IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '100')
BEGIN
  UPDATE [MNT_Menus]
  SET [IsMenuItem] = 'N',
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = '256',
      [SubTabId] = '1',
      [IsActive] = 'Y'
  WHERE MenuId = '100'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '106')
BEGIN
  UPDATE [MNT_Menus]
  SET [IsMenuItem] = 'N',
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = '256',
      [SubTabId] = '3',
      [IsActive] = 'Y'
  WHERE MenuId = '106'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '107')
BEGIN
  UPDATE [MNT_Menus]
  SET [IsMenuItem] = 'N',
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = '256',
      [SubTabId] = '5',
      [IsActive] = 'Y'
  WHERE MenuId = '107'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '108')
BEGIN
  UPDATE [MNT_Menus]
  SET [IsMenuItem] = 'N',
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = '256',
      [SubTabId] = '9',
      [IsActive] = 'Y'
  WHERE MenuId = '108'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '109')
BEGIN
  UPDATE [MNT_Menus]
  SET [IsMenuItem] = 'N',
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = '256',
      [SubTabId] = '7',
      [IsActive] = 'Y'
  WHERE MenuId = '109'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '128')
BEGIN
  UPDATE [MNT_Menus]
  SET [IsMenuItem] = 'N',
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = '256',
      [SubTabId] = '11',
      [IsActive] = 'Y'
  WHERE MenuId = '128'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '220')
BEGIN
  UPDATE [MNT_Menus]
  SET [IsMenuItem] = 'N',
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = '256',
      [SubTabId] = '12',
      [IsActive] = 'Y'
  WHERE MenuId = '220'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '231')
BEGIN
  UPDATE [MNT_Menus]
  SET [IsMenuItem] = 'N',
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = '256',
      [SubTabId] = '2',
      [IsActive] = 'Y'
  WHERE MenuId = '231'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '236')
BEGIN
  UPDATE [MNT_Menus]
  SET [IsMenuItem] = 'N',
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = '256',
      [SubTabId] = '4',
      [IsActive] = 'Y'
  WHERE MenuId = '236'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '238')
BEGIN
  UPDATE [MNT_Menus]
  SET [IsMenuItem] = 'N',
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = '256',
      [SubTabId] = '6',
      [IsActive] = 'Y'
  WHERE MenuId = '238'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '240')
BEGIN
  UPDATE [MNT_Menus]
  SET [IsMenuItem] = 'N',
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = '256',
      [SubTabId] = '10',
      [IsActive] = 'Y'
  WHERE MenuId = '240'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '241')
BEGIN
  UPDATE [MNT_Menus]
  SET [IsMenuItem] = 'N',
      [IsHeader] = 'N',
      [SubMenu] = 'Y',
      [MainHeaderId] = '256',
      [SubTabId] = '8',
      [IsActive] = 'Y'
  WHERE MenuId = '241'
END
IF EXISTS (SELECT
    1
  FROM MNT_Menus
  WHERE MenuId = '256')
BEGIN
  UPDATE [MNT_Menus]
  SET [IsMenuItem] = 'Y',
      [IsHeader] = 'Y',
      [SubMenu] = 'N',
      [MainHeaderId] = '0',
      [SubTabId] = 'NULL',
      [IsActive] = 'Y'
  WHERE MenuId = '256'
END