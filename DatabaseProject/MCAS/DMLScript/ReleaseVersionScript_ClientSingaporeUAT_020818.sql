if not exists( select * from MNT_APP_RELEASE_MASTER where ReleaseNumber=31)
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
'1.70',
'1.0',
'31',
'Client UAT Version',
getdate(),
'Yogesh',
'
Redmin ID #32906,#32908
'
)
end