IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='DBC') 
BEGIN 

INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'DBC', N'Date of BC''s appointment at law firms', N'Date of BC''s appointment at law firms', N'TASKTYPE')

END 

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='DCR') 
BEGIN 
INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'DCR', N'Date of CDRs/AD/Trials', N'Date of CDRs/AD/Trials', N'TASKTYPE')

END 

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='DDA') 
BEGIN 

INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'DDA', N'Due Date for Appearance after WOS', N'Due Date for Appearance after WOS', N'TASKTYPE')
END 

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='E10') 
BEGIN 
INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'E10', N'Expiry of 10-days notice', N'Expiry of 10-days notice', N'TASKTYPE')

END 

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='E3') 
BEGIN 
INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'E3', N'Expiry of 3 weeks', N'Expiry of 3 weeks', N'TASKTYPE')

END 

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='FFU') 
BEGIN 
INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'FFU', N'For follow up by CO', N'For follow up by CO', N'TASKTYPE')

END 

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='SCR') 
BEGIN 
INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES ( N'SCR', N'Sensitive cases - revert to claimants', N'Sensitive cases - revert to claimants', N'TASKTYPE')
END 

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='SMR') 
BEGIN 
INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'SMR', N'Submit medical report', N'Submit medical report', N'TASKTYPE')
END 

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='SRR') 
BEGIN 
INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'SRR', N'Submit reinspection report', N'Submit reinspection report', N'TASKTYPE')
END 

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='WC') 
BEGIN 
INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'WC', N'Writ Cases', N'Writ Cases', N'TASKTYPE')
END 

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='PMR') 
BEGIN 
INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'PMR', N'Pending Medical Report', N'Pending Medical Report', N'TASKTYPE')
END 

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='PSR') 
BEGIN 
INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'PSR', N'Pending Survey Report', N'Pending Survey Report', N'TASKTYPE')
END 

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='PCF') 
BEGIN 
INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'PCF', N'Pending CCTV footage', N'Pending CCTV footage', N'TASKTYPE')
END 

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='PIR') 
BEGIN 
INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'PIR', N'Pending IP Report', N'Pending IP Report', N'TASKTYPE')
END 

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='DFP') 
BEGIN 
INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'DFP', N'Due for Payment', N'Due for Payment', N'TASKTYPE')
END 

IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='O') 
BEGIN 
INSERT [dbo].[MNT_Lookups] ( [Lookupvalue], [Lookupdesc], [Description], [Category]) VALUES (N'O', N'Others', N'Others', N'TASKTYPE')
END 
