/*********************Insert Into [MNT_Lookups] for Organiztion****************************/
INSERT INTO [dbo].[MNT_Lookups]
           ([Lookupvalue]
           ,[Lookupdesc]
           ,[Description]
           ,[Category])
     VALUES
           ('CC'
           ,'CityCab'
           ,'City Cab'
           ,'ORG')
GO

INSERT INTO [dbo].[MNT_Lookups]
           ([Lookupvalue]
           ,[Lookupdesc]
           ,[Description]
           ,[Category])
     VALUES
           ('ST'
           ,'SBS Transit'
           ,'SBS Transit'
           ,'ORG')
GO

INSERT INTO [dbo].[MNT_Lookups]
           ([Lookupvalue]
           ,[Lookupdesc]
           ,[Description]
           ,[Category])
     VALUES
           ('CT'
           ,'Comfort Taxi'
           ,'Comfort Taxi'
           ,'ORG')
GO

INSERT INTO [dbo].[MNT_Lookups]
           ([Lookupvalue]
           ,[Lookupdesc]
           ,[Description]
           ,[Category])
     VALUES
           ('SMRT'
           ,'SMRT'
           ,'SMRT'
           ,'ORG')
GO

/*********************Alter table [ClaimAccidentDetails] for Add New Column****************************/
ALTER TABLE [dbo].[ClaimAccidentDetails] ADD Organization nvarchar(20),  AccidentImage nvarchar(100)

ALTER TABLE [dbo].[ClaimAccidentDetails] ADD LossTypeCode nvarchar(10),  LossNatureCode nvarchar(10)
ALTER TABLE [dbo].[ClaimAccidentDetails] ADD TPClaimentStatus varchar(1)
ALTER TABLE [dbo].[ClaimAccidentDetails] ADD TimePeriod int

ALTER TABLE [dbo].[ClaimAccidentDetails] Alter Column Damages nvarchar(500)

ALTER TABLE [dbo].[ClaimAccidentDetails] DROP COLUMN DriverName,DriverDoB,DriverGender,DriverDateJoined

EXEC sp_rename '[dbo].[ClaimAccidentDetails].[DriverSurname]', 'DriverName','Column'