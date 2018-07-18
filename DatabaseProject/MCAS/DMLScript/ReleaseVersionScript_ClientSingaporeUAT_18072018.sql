if not exists( select * from MNT_APP_RELEASE_MASTER where ReleaseNumber=30)
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
'1.69',
'1.0',
'30',
'Client UAT Version',
getdate(),
'Yogesh',
'
Redmin ID #32542
'
)
end