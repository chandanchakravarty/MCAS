if not exists( select * from MNT_APP_RELEASE_MASTER where ReleaseNumber=71)
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
'1.65',
'1.0',
'71',
'Singapore UAT test Version',
getdate(),
'Yogesh',
'
Redmin ID #29532
'
)
end
