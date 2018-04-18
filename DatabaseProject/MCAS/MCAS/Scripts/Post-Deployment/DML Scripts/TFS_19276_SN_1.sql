-- =============================================
-- Script Template
-- =============================================
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='CTPL' and Category='OwnerName') 
BEGIN 
INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category], [IsActive]) VALUES ('CTPL', 'COMFORTDELGRO TRANSPORTATION PTE LTD', 'COMFORTDELGRO TRANSPORTATION PTE LTD', 'OwnerName','Y')
END

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='CCPL' and Category='OwnerName') 
BEGIN 
INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category], [IsActive]) VALUES ('CCPL', 'CITYCAB PTE LTD', 'CITYCAB PTE LTD', 'OwnerName','Y')
END