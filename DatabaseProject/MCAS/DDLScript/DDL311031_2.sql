if exists (select 1 from sys.tables where name='TM_CodeMaster' )
begin
alter table TM_CodeMaster add Id int identity(1,1)
end