if not exists( select * from MNT_APP_RELEASE_MASTER where ReleaseNumber=77)
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
'77',
'Singapore UAT test Version',
getdate(),
'Yogesh',
'
Redmin ID #32447
'
)
end
