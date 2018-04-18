IF EXISTS (SELECT 1 FROM mnt_menus WHERE MenuId = 135 AND DisplayTitle = 'Mandate')
BEGIN
  UPDATE [mnt_menus] SET [MenuId] = 1138,[DisplayOrder] = 1138 WHERE MenuId = 135 AND DisplayTitle = 'Mandate'
  UPDATE [mnt_menus] SET [MenuId] = 1135,[DisplayOrder] = 1135 WHERE MenuId = 138 AND DisplayTitle = 'Reserve'
  UPDATE [mnt_menus] SET [MenuId] = 135,[DisplayOrder] = 135 WHERE MenuId = 1135 AND DisplayTitle = 'Reserve'
  UPDATE [mnt_menus] SET [MenuId] = 138,[DisplayOrder] = 138 WHERE MenuId = 1138 AND DisplayTitle = 'Mandate'
END


IF EXISTS (SELECT 1 FROM mnt_menus WHERE MenuId = 135 AND DisplayTitle = 'Reserve')
BEGIN
UPDATE [mnt_menus] SET [MenuId] = 2135,[DisplayOrder] = 2135 WHERE MenuId = 135 AND DisplayTitle = 'Reserve'
UPDATE [mnt_menus] SET [MenuId] = 2136,[DisplayOrder] = 2136 WHERE MenuId = 136 AND DisplayTitle = 'Attachments'
UPDATE [mnt_menus] SET [MenuId] = 2137,[DisplayOrder] = 2137 WHERE MenuId = 137 AND DisplayTitle = 'Diary'
UPDATE [mnt_menus] SET [MenuId] = 135,[DisplayOrder] = 135 WHERE MenuId = 2136 AND DisplayTitle = 'Attachments'
UPDATE [mnt_menus] SET [MenuId] = 136,[DisplayOrder] = 136 WHERE MenuId = 2137 AND DisplayTitle = 'Diary'
UPDATE [mnt_menus] SET [MenuId] = 137,[DisplayOrder] = 137 WHERE MenuId = 2135 AND DisplayTitle = 'Reserve'
END 