if not exists( select * from MNT_APP_RELEASE_MASTER where ReleaseNumber=27)
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
'27',
'Client UAT Version',
getdate(),
'Yogesh',
'
Redmin ID #31152
'
)
end