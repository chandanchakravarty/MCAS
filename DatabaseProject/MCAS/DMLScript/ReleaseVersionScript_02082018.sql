if not exists( select * from MNT_APP_RELEASE_MASTER where ReleaseNumber=76)
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
'76',
'Singapore UAT test Version',
getdate(),
'Yogesh',
'
Redmin ID #32906,#32908
'
)
end
