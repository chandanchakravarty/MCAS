if not exists( select * from MNT_APP_RELEASE_MASTER where ReleaseNumber=73)
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
'1.67',
'1.0',
'73',
'Singapore UAT test Version',
getdate(),
'Yogesh',
'
Redmin ID #31152,#31031
'
)
end
