if not exists( select * from MNT_APP_RELEASE_MASTER where ReleaseNumber=29)
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
'29',
'Client UAT Version',
getdate(),
'Yogesh',
'
Redmin ID #31031
'
)
end