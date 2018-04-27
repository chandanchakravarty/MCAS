IF not EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'Remarks'
          AND Object_ID = Object_ID(N'CLM_Claims'))
BEGIN
   alter table CLM_Claims Add Remarks nvarchar(500)
END
IF not EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'SettlementType'
          AND Object_ID = Object_ID(N'CLM_Claims'))
BEGIN
   alter table CLM_Claims Add SettlementType nvarchar(200)
END


IF EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'ReportedDate'
          AND Object_ID = Object_ID(N'ClaimAccidentDetails'))
BEGIN
alter table  ClaimAccidentDetails alter column ReportedDate datetime null
END

IF EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'AccidentTime'
          AND Object_ID = Object_ID(N'ClaimAccidentDetails'))
BEGIN
alter table  ClaimAccidentDetails alter column AccidentTime nvarchar(40) null 
END
