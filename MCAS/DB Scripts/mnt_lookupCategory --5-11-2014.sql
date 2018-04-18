IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='BU' and Category='ORGCategory') 
BEGIN 

INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'BU', N'Bus', N'Bus', N'ORGCategory')

END 

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='TR' and Category='ORGCategory') 
BEGIN 

INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'TR', N'Train', N'Train', N'ORGCategory')

END 

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='TX' and Category='ORGCategory') 
BEGIN 

INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'TX', N'Taxi', N'Taxi', N'ORGCategory')

END 

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='PC' and Category='ORGCategory') 
BEGIN 

INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'PC', N'Private Cars', N'Private Cars', N'ORGCategory')

END 

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='PB' and Category='ORGCategory') 
BEGIN 

INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'PB', N'Private Bus', N'Private Bus', N'ORGCategory')

END 

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='RV' and Category='ORGCategory') 
BEGIN 

INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'RV', N'Rental Vehicles', N'Rental Vehicles', N'ORGCategory')

END 