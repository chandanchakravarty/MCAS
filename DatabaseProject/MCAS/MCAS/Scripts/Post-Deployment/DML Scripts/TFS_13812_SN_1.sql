
If not exists(Select top 1* from MNT_TableDesc where TableName='MNT_UserOrgAccess')
Begin
INSERT INTO [MNT_TableDesc]([TableName],[TableDesc],[Type]) VALUES ('MNT_UserOrgAccess','System Admin','Organization Access')
end
If not exists(Select top 1* from MNT_TableDesc where TableName='MNT_LOU_MASTER')
Begin
INSERT INTO [MNT_TableDesc]([TableName],[TableDesc],[Type]) VALUES ('MNT_LOU_MASTER','System Admin','Lou Master')
end
If not exists(Select top 1* from MNT_TableDesc where TableName='MNT_FAL')
Begin
INSERT INTO [MNT_TableDesc]([TableName],[TableDesc],[Type]) VALUES ('MNT_FAL','System Admin','FAL')
end
If not exists(Select top 1* from MNT_TableDesc where TableName='ClaimAccidentHistoryDetails')
Begin
INSERT INTO [MNT_TableDesc]([TableName],[TableDesc],[Type]) VALUES ('ClaimAccidentHistoryDetails','Claims','Accident History')
end

