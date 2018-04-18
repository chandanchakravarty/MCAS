IF EXISTS(SELECT * FROM MNT_GroupsMaster WHERE GroupCode='SP')
     BEGIN
         UPDATE MNT_GroupsMaster SET GroupName='Supervisor' WHERE GroupCode='SP'
     END