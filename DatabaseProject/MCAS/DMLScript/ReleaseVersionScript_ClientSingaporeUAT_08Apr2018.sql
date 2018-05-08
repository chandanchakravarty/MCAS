if not exists( select * from MNT_APP_RELEASE_MASTER where ReleaseNumber=26)
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
'26',
'Client UAT Version',
getdate(),
'Yogesh',
'
Redmin ID #29532 
'
)
end