if not exists( select * from MNT_APP_RELEASE_MASTER where ReleaseNumber=72)
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
'1.66',
'1.0',
'72',
'Singapore UAT test Version',
getdate(),
'Yogesh',
'
Redmin ID #31152
'
)
end
