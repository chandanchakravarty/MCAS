if not exists( select * from MNT_APP_RELEASE_MASTER where ReleaseNumber=32)
begin
insert into MNT_APP_RELEASE_MASTER(
AppVersion,
AssemblyVersion,
ReleaseNumber,
ReleaseName,
ReleaseDate,
ReleasedBy,
Remarks)
values
(
'1.71',
'1.0',
'32',
'Client UAT Version',
getdate(),
'Yogesh',
'
Redmin ID #32447
'
)
end