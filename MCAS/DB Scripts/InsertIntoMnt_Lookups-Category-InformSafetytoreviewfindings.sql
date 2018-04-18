IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH (NOLOCK) WHERE Lookupvalue = 'Y' AND Category = 'InformSafetytoreviewfindings')
BEGIN
INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category],[IsActive]) VALUES (N'Y', N'Yes', N'Yes', N'InformSafetytoreviewfindings','Y')
END

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH (NOLOCK) WHERE Lookupvalue = 'N' AND Category = 'InformSafetytoreviewfindings')
BEGIN
INSERT [dbo].[MNT_Lookups] ([Lookupvalue], [Lookupdesc], [Description], [Category],[IsActive]) VALUES (N'N', N'N.A.', N'N.A.', N'InformSafetytoreviewfindings','Y')
END