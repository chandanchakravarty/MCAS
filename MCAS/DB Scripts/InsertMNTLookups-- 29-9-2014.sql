IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='0' and Category='InsurerType') 
BEGIN 

INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'0', N'Own', N'Own', N'InsurerType')

END 

--2
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='1' and Category='InsurerType') 
BEGIN 

INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'1', N'TP', N'TP', N'InsurerType')

END 

--3
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='2' and Category='InsurerType') 
BEGIN 

INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'2', N'Both', N'Both', N'InsurerType')

END 
