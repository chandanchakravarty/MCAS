if not exists( select * from MNT_APP_RELEASE_MASTER where ReleaseNumber=28)
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
'28',
'Client UAT Version',
getdate(),
'Yogesh',
'
Redmin ID #31152,#31031
'
)
end