SET IDENTITY_INSERT [dbo].[Mnt_MenusGroup] ON
IF NOT EXISTS (SELECT
    1
  FROM [Mnt_MenusGroup]
  WHERE [GroupType] = 'A'
  AND MenuId = '101')
BEGIN
  INSERT [dbo].[Mnt_MenusGroup] ([Id], [MenuId], [GroupType], [IsActive])
    VALUES (1, 101, N'A', N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [Mnt_MenusGroup]
  WHERE [GroupType] = 'B'
  AND MenuId = '103')
BEGIN
  INSERT [dbo].[Mnt_MenusGroup] ([Id], [MenuId], [GroupType], [IsActive])
    VALUES (2, 103, N'B', N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [Mnt_MenusGroup]
  WHERE [GroupType] = 'B'
  AND MenuId = '104')
BEGIN
  INSERT [dbo].[Mnt_MenusGroup] ([Id], [MenuId], [GroupType], [IsActive])
    VALUES (3, 104, N'B', N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [Mnt_MenusGroup]
  WHERE [GroupType] = 'B'
  AND MenuId = '105')
BEGIN
  INSERT [dbo].[Mnt_MenusGroup] ([Id], [MenuId], [GroupType], [IsActive])
    VALUES (4, 105, N'B', N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [Mnt_MenusGroup]
  WHERE [GroupType] = 'B'
  AND MenuId = '110')
BEGIN
  INSERT [dbo].[Mnt_MenusGroup] ([Id], [MenuId], [GroupType], [IsActive])
    VALUES (5, 110, N'B', N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [Mnt_MenusGroup]
  WHERE [GroupType] = 'B'
  AND MenuId = '111')
BEGIN
  INSERT [dbo].[Mnt_MenusGroup] ([Id], [MenuId], [GroupType], [IsActive])
    VALUES (6, 111, N'B', N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [Mnt_MenusGroup]
  WHERE [GroupType] = 'B'
  AND MenuId = '112')
BEGIN
  INSERT [dbo].[Mnt_MenusGroup] ([Id], [MenuId], [GroupType], [IsActive])
    VALUES (7, 112, N'B', N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [Mnt_MenusGroup]
  WHERE [GroupType] = 'B'
  AND MenuId = '113')
BEGIN
  INSERT [dbo].[Mnt_MenusGroup] ([Id], [MenuId], [GroupType], [IsActive])
    VALUES (8, 113, N'B', N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [Mnt_MenusGroup]
  WHERE [GroupType] = 'A'
  AND MenuId = '114')
BEGIN
  INSERT [dbo].[Mnt_MenusGroup] ([Id], [MenuId], [GroupType], [IsActive])
    VALUES (9, 114, N'A', N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [Mnt_MenusGroup]
  WHERE [GroupType] = 'A'
  AND MenuId = '115')
BEGIN
  INSERT [dbo].[Mnt_MenusGroup] ([Id], [MenuId], [GroupType], [IsActive])
    VALUES (10, 115, N'A', N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [Mnt_MenusGroup]
  WHERE [GroupType] = 'B'
  AND MenuId = '120')
BEGIN
  INSERT [dbo].[Mnt_MenusGroup] ([Id], [MenuId], [GroupType], [IsActive])
    VALUES (11, 120, N'B', N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [Mnt_MenusGroup]
  WHERE [GroupType] = 'B'
  AND MenuId = '121')
BEGIN
  INSERT [dbo].[Mnt_MenusGroup] ([Id], [MenuId], [GroupType], [IsActive])
    VALUES (12, 121, N'B', N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [Mnt_MenusGroup]
  WHERE [GroupType] = 'B'
  AND MenuId = '122')
BEGIN
  INSERT [dbo].[Mnt_MenusGroup] ([Id], [MenuId], [GroupType], [IsActive])
    VALUES (13, 122, N'B', N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [Mnt_MenusGroup]
  WHERE [GroupType] = 'B'
  AND MenuId = '123')
BEGIN
  INSERT [dbo].[Mnt_MenusGroup] ([Id], [MenuId], [GroupType], [IsActive])
    VALUES (14, 123, N'B', N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [Mnt_MenusGroup]
  WHERE [GroupType] = 'B'
  AND MenuId = '124')
BEGIN
  INSERT [dbo].[Mnt_MenusGroup] ([Id], [MenuId], [GroupType], [IsActive])
    VALUES (15, 124, N'B', N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [Mnt_MenusGroup]
  WHERE [GroupType] = 'A'
  AND MenuId = '125')
BEGIN
  INSERT [dbo].[Mnt_MenusGroup] ([Id], [MenuId], [GroupType], [IsActive])
    VALUES (16, 125, N'A', N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [Mnt_MenusGroup]
  WHERE [GroupType] = 'A'
  AND MenuId = '127')
BEGIN
  INSERT [dbo].[Mnt_MenusGroup] ([Id], [MenuId], [GroupType], [IsActive])
    VALUES (17, 127, N'A', N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [Mnt_MenusGroup]
  WHERE [GroupType] = 'A'
  AND MenuId = '256')
BEGIN
  INSERT [dbo].[Mnt_MenusGroup] ([Id], [MenuId], [GroupType], [IsActive])
    VALUES (18, 256, N'A', N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [Mnt_MenusGroup]
  WHERE [GroupType] = 'A'
  AND MenuId = '277')
BEGIN
  INSERT [dbo].[Mnt_MenusGroup] ([Id], [MenuId], [GroupType], [IsActive])
    VALUES (19, 277, N'A', N'Y')
END
IF NOT EXISTS (SELECT
    1
  FROM [Mnt_MenusGroup]
  WHERE [GroupType] = 'A'
  AND MenuId = '279')
BEGIN
  INSERT [dbo].[Mnt_MenusGroup] ([Id], [MenuId], [GroupType], [IsActive])
    VALUES (20, 279, N'A', N'Y')
END

SET IDENTITY_INSERT [dbo].[Mnt_MenusGroup] OFF