if not exists(select 1 from TM_CodeMaster where code='SORA' and CurrentMonth=06 and CurrentYear=2018 )
begin
insert into TM_CodeMaster values('SORA','Sora Serial No',0,2018,06)
end