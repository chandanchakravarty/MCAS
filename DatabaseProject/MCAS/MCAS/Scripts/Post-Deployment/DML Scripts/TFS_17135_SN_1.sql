/***************Insert Query*********************************/

SET IDENTITY_INSERT MNT_Lookups ON
go
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='CNR' and Category='AlertDesc') 
BEGIN 
INSERT INTO [dbo].[MNT_Lookups] ([LookupID],[Lookupvalue],[Lookupdesc],[Description],[Category],[IsActive])
     VALUES (569,'CNR','Claims Notification received, Bus Captain(BC) at fault? Yes/No','Claims Notification received, Bus Captain(BC) at fault? Yes/No','AlertDesc','Y')
END 
SET IDENTITY_INSERT MNT_Lookups OFF
go

SET IDENTITY_INSERT MNT_Lookups ON
go
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='PD' and Category='AlertDesc') 
BEGIN
INSERT INTO [dbo].[MNT_Lookups] ([LookupID],[Lookupvalue],[Lookupdesc],[Description],[Category],[IsActive])
     VALUES (546,'PD','Pending Documents','Pending Documents','AlertDesc','Y')
END 
SET IDENTITY_INSERT MNT_Lookups OFF
go

SET IDENTITY_INSERT MNT_Lookups ON
go
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='CA' and Category='AlertDesc') 
BEGIN
INSERT INTO [dbo].[MNT_Lookups] ([LookupID],[Lookupvalue],[Lookupdesc],[Description],[Category],[IsActive])
     VALUES (547,'CA','Case Assignment','Case Assignment','AlertDesc','Y')
END 
SET IDENTITY_INSERT MNT_Lookups OFF
go

SET IDENTITY_INSERT MNT_Lookups ON
go
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='LOD' and Category='AlertDesc') 
BEGIN
INSERT INTO [dbo].[MNT_Lookups] ([LookupID],[Lookupvalue],[Lookupdesc],[Description],[Category],[IsActive])
     VALUES (548,'LOD','LOD Sent','LOD Sent','AlertDesc','Y')
END 
SET IDENTITY_INSERT MNT_Lookups OFF
go

SET IDENTITY_INSERT MNT_Lookups ON
go
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='CRD' and Category='AlertDesc') 
BEGIN
INSERT INTO [dbo].[MNT_Lookups] ([LookupID],[Lookupvalue],[Lookupdesc],[Description],[Category],[IsActive])
     VALUES (549,'CRD','Set Case Review Date','Set Case Review Date','AlertDesc','Y')
END
SET IDENTITY_INSERT MNT_Lookups OFF
go 

SET IDENTITY_INSERT MNT_Lookups ON
go
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='SR' and Category='AlertDesc') 
BEGIN
INSERT INTO [dbo].[MNT_Lookups] ([LookupID],[Lookupvalue],[Lookupdesc],[Description],[Category],[IsActive])
     VALUES (550,'SR','Settlement Reached','Settlement Reached','AlertDesc','Y')
END
SET IDENTITY_INSERT MNT_Lookups OFF
go

SET IDENTITY_INSERT MNT_Lookups ON
go
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='PR' and Category='AlertDesc') 
BEGIN
INSERT INTO [dbo].[MNT_Lookups] ([LookupID],[Lookupvalue],[Lookupdesc],[Description],[Category],[IsActive])
     VALUES (551,'PR','Payment Received','Payment Received','AlertDesc','Y')
END 
SET IDENTITY_INSERT MNT_Lookups OFF
go

SET IDENTITY_INSERT MNT_Lookups ON
go
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='PC' and Category='AlertDesc') 
BEGIN
INSERT INTO [dbo].[MNT_Lookups] ([LookupID],[Lookupvalue],[Lookupdesc],[Description],[Category],[IsActive])
     VALUES (552,'PC','Property Claims','Property Claims','AlertDesc','Y')
END
SET IDENTITY_INSERT MNT_Lookups OFF
go 

SET IDENTITY_INSERT MNT_Lookups ON
go
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='CGT' and Category='AlertDesc') 
BEGIN
INSERT INTO [dbo].[MNT_Lookups] ([LookupID],[Lookupvalue],[Lookupdesc],[Description],[Category],[IsActive])
     VALUES (553,'CGT','Injury Claims – Claims Amount more than $15k','Injury Claims – Claims Amount more than $15k','AlertDesc','Y')
END
SET IDENTITY_INSERT MNT_Lookups OFF
go 

SET IDENTITY_INSERT MNT_Lookups ON
go
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='CLT' and Category='AlertDesc') 
BEGIN
INSERT INTO [dbo].[MNT_Lookups] ([LookupID],[Lookupvalue],[Lookupdesc],[Description],[Category],[IsActive])
     VALUES (554,'CLT','Injury Claims – Claims Amount less than $15k','Injury Claims – Claims Amount less than $15k','AlertDesc','Y')
END
SET IDENTITY_INSERT MNT_Lookups OFF
go 

SET IDENTITY_INSERT MNT_Lookups ON
go
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='UCD' and Category='AlertDesc') 
BEGIN
INSERT INTO [dbo].[MNT_Lookups] ([LookupID],[Lookupvalue],[Lookupdesc],[Description],[Category],[IsActive])
     VALUES (555,'UCD','Update Claim Development','Update Claim Development','AlertDesc','Y')
END
SET IDENTITY_INSERT MNT_Lookups OFF
go 

SET IDENTITY_INSERT MNT_Lookups ON
go
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='RC' and Category='AlertDesc') 
BEGIN
INSERT INTO [dbo].[MNT_Lookups] ([LookupID],[Lookupvalue],[Lookupdesc],[Description],[Category],[IsActive])
     VALUES (556,'RC','Review Of Claim','Review Of Claim','AlertDesc','Y')
END
SET IDENTITY_INSERT MNT_Lookups OFF
go

SET IDENTITY_INSERT MNT_Lookups ON
go
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='WOS' and Category='AlertDesc') 
BEGIN
INSERT INTO [dbo].[MNT_Lookups] ([LookupID],[Lookupvalue],[Lookupdesc],[Description],[Category],[IsActive])
     VALUES (557,'WOS','Writ of Summons (WOS) Received','Writ of Summons (WOS) Received','AlertDesc','Y')
END
SET IDENTITY_INSERT MNT_Lookups OFF
go 

SET IDENTITY_INSERT MNT_Lookups ON
go
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='CRP' and Category='AlertDesc') 
BEGIN
INSERT INTO [dbo].[MNT_Lookups] ([LookupID],[Lookupvalue],[Lookupdesc],[Description],[Category],[IsActive])
     VALUES (558,'CRP','Case Resolved. Payment Required','Case Resolved. Payment Required','AlertDesc','Y')
END
SET IDENTITY_INSERT MNT_Lookups OFF
go 

SET IDENTITY_INSERT MNT_Lookups ON
go
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='TP' and Category='AlertDesc') 
BEGIN
INSERT INTO [dbo].[MNT_Lookups] ([LookupID],[Lookupvalue],[Lookupdesc],[Description],[Category],[IsActive])
     VALUES (559,'TP','Total Payment >15K','Total Payment >15K','AlertDesc','Y')
END
SET IDENTITY_INSERT MNT_Lookups OFF
go 

SET IDENTITY_INSERT MNT_Lookups ON
go
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='DN' and Category='AlertDesc') 
BEGIN
INSERT INTO [dbo].[MNT_Lookups] ([LookupID],[Lookupvalue],[Lookupdesc],[Description],[Category],[IsActive])
     VALUES (560,'DN','Debit Note Approved by SV','Debit Note Approved by SV','AlertDesc','Y')
END
SET IDENTITY_INSERT MNT_Lookups OFF
go 

SET IDENTITY_INSERT MNT_Lookups ON
go
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='PP' and Category='AlertDesc') 
BEGIN
INSERT INTO [dbo].[MNT_Lookups] ([LookupID],[Lookupvalue],[Lookupdesc],[Description],[Category],[IsActive])
     VALUES (561,'PP','Payment Processing','Payment Processing','AlertDesc','Y')
END
SET IDENTITY_INSERT MNT_Lookups OFF
go 

SET IDENTITY_INSERT MNT_Lookups ON
go
IF NOT EXISTS (SELECT 1 FROM MNT_Lookups WITH(NOLOCK) WHERE Lookupvalue='PS' and Category='AlertDesc') 
BEGIN
INSERT INTO [dbo].[MNT_Lookups] ([LookupID],[Lookupvalue],[Lookupdesc],[Description],[Category],[IsActive])
     VALUES (562,'PS','Payment Settlement','Payment Settlement','AlertDesc','Y')
END
SET IDENTITY_INSERT MNT_Lookups OFF
go 


/*** update TODODIARYLIST ***/

update TODODIARYLIST set LISTTYPEID = 569 where LISTTYPEID = 1
update TODODIARYLIST set LISTTYPEID = 546 where LISTTYPEID = 2
update TODODIARYLIST set LISTTYPEID = 547 where LISTTYPEID in (3,8)
update TODODIARYLIST set LISTTYPEID = 548 where LISTTYPEID = 4
update TODODIARYLIST set LISTTYPEID = 549 where LISTTYPEID = 5
update TODODIARYLIST set LISTTYPEID = 550 where LISTTYPEID = 6
update TODODIARYLIST set LISTTYPEID = 551 where LISTTYPEID = 7
update TODODIARYLIST set LISTTYPEID = 552 where LISTTYPEID = 9
update TODODIARYLIST set LISTTYPEID = 553 where LISTTYPEID = 10
update TODODIARYLIST set LISTTYPEID = 554 where LISTTYPEID = 11
update TODODIARYLIST set LISTTYPEID = 555 where LISTTYPEID = 12
update TODODIARYLIST set LISTTYPEID = 556 where LISTTYPEID = 13
update TODODIARYLIST set LISTTYPEID = 557 where LISTTYPEID = 14
update TODODIARYLIST set LISTTYPEID = 558 where LISTTYPEID = 15
update TODODIARYLIST set LISTTYPEID = 559 where LISTTYPEID = 16
update TODODIARYLIST set LISTTYPEID = 560 where LISTTYPEID = 17
update TODODIARYLIST set LISTTYPEID = 561 where LISTTYPEID = 18
update TODODIARYLIST set LISTTYPEID = 562 where LISTTYPEID = 19
