If Exists(Select 1 from MNT_PasswordSetup where SetupID=1)
 Begin
    Update MNT_PasswordSetup set CreatedBy='Pravesh',CreatedDate='2014-05-30 16:04:10.960'
 End