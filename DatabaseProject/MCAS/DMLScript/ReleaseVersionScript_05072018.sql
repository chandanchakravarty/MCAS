if not exists( select * from MNT_APP_RELEASE_MASTER where ReleaseNumber=74)
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
'1.68',
'1.0',
'74',
'Singapore UAT test Version',
getdate(),
'Yogesh',
'
Redmin ID #31031
'
)
end
