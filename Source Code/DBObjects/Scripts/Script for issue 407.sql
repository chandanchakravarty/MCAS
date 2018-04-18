

--Begin Transaction Abhinav
If exists(select * from MNT_USER_LIST where USER_ID=3)
Begin
Update MNT_USER_LIST 
set USER_FNAME='Administrador',USER_LNAME='' where USER_ID=3
End
--Rollback Transaction Abhinav