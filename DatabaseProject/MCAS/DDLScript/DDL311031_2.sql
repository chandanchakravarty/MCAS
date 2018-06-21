IF not EXISTS(SELECT 1 FROM sys.columns 
          WHERE Name = N'Id'
          AND Object_ID = Object_ID(N'TM_CodeMaster'))
begin
alter table TM_CodeMaster add Id int identity(1,1)
end