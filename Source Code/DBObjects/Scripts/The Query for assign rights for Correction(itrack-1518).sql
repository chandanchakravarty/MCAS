If Exists(Select * from MNT_SCREEN_LIST where SCREEN_ID='492_0')
Begin
update MNT_SCREEN_LIST
set SCREEN_DESC='Coverage Detail',SCREEN_PATH='/cms/cmsweb/maintenance/AddCoverageDetails.aspx'
where SCREEN_ID='492_0'
End


IF NOT EXISTS (SELECT  * FROM MNT_SCREEN_LIST WHERE SCREEN_ID='492')
BEGIN
INSERT INTO MNT_SCREEN_LIST(SCREEN_ID, SCREEN_DESC,SCREEN_PATH, SCREEN_READ, SCREEN_WRITE, SCREEN_DELETE, SCREEN_EXECUTE, IS_ACTIVE)
VALUES
('492', 'Coverage List', '/cms/cmsweb/maintenance/CoverageIndex.aspx', 1, 1, 1, 1, 'Y')
END	


IF NOT EXISTS (SELECT  * FROM MNT_SCREEN_LIST_MULTILINGUAL WHERE SCREEN_ID='492')
BEGIN
INSERT INTO MNT_SCREEN_LIST_MULTILINGUAL(SCREEN_ID, SCREEN_DESC,LANG_ID)VALUES('492','Lista de Coberturas', 2)
END



If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='492_1')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='492_1'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='189')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='189'
End

If Exists(select * from MNT_MENU_LIST where MENU_ID='189')
Begin
update MNT_MENU_LIST
set IS_ACTIVE='N' where MENU_ID='189'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='36')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='36'
End

If Exists(select * from MNT_MENU_LIST where MENU_ID='36')
Begin
update MNT_MENU_LIST
set IS_ACTIVE='N' where MENU_ID='36'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='36_0')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='36_0'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='36_1')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='36_1'
End



If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='36_1_0')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='36_1_0'
End



If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='36_2')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='36_2'
End



If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='36_2_0')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='36_2_0'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='128_1_1_1_0')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='128_1_1_1_0'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='128_1_2_0')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='128_1_2_0'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='128_1_1_1')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='128_1_1_1'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='128_1_2')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='128_1_2'
End

If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='128_1_1_1_1')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='128_1_1_1_1'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='128_1_3')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='128_1_3'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='128_1_1_2')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='128_1_1_2'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='128_1_4')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='128_1_4'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='262_5')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='262_5'
End



If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='262_6')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='262_6'
End




If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='262_7')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='262_7'
End




If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='262_8')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='262_8'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='124')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='124'
End

If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='124_0')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='124_0'
End

If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='124_0_0')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='124_0_0'
End

If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='124_1')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='124_1'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='124_1_0')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='124_1_0'
End



If Exists(select * from MNT_MENU_LIST where MENU_ID='124')
Begin
update MNT_MENU_LIST
set IS_ACTIVE='N' where MENU_ID='124'
End



If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='125_0')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='125_0'
End

If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='125_0_0')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='125_0_0'
End

If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='125_1')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='125_1'
End

If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='125_1_0')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='125_1_0'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='125_1_1')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='125_1_1'
End



If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='125_1_2')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='125_1_2'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='125_1_2_0')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='125_1_2_0'
End

If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='125_2')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='125_2'
End


If Exists(select * from MNT_MENU_LIST where MENU_ID='125')
Begin
update MNT_MENU_LIST
set IS_ACTIVE='N' where MENU_ID='125'
End

If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='126')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='126'
End

If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='126_0')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='126_0'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='126_1')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='126_1'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='126_2')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='126_2'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='126_3')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='126_3'
End

If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='126_4')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='126_4'
End

If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='126_5')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='126_5'
End


If Exists(select * from MNT_MENU_LIST where MENU_ID='125')
Begin
update MNT_MENU_LIST
set IS_ACTIVE='N' where MENU_ID='126'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='178')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='178'
End

If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='178_0')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='178_0'
End


If Exists(select * from MNT_MENU_LIST where MENU_ID='178')
Begin
update MNT_MENU_LIST
set IS_ACTIVE='N' where MENU_ID='178'
End

If Exists(select * from MNT_MENU_LIST where MENU_ID='179')
Begin
update MNT_MENU_LIST
set IS_ACTIVE='N' where MENU_ID='179'
End

If Exists(select * from MNT_MENU_LIST where MENU_ID='180')
Begin
update MNT_MENU_LIST
set IS_ACTIVE='N' where MENU_ID='180'
End

If Exists(select * from MNT_MENU_LIST where MENU_ID='385')
Begin
update MNT_MENU_LIST
set IS_ACTIVE='N' where MENU_ID='385'
End

If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='179')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='179'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='179_0')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='179_0'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='180')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='180'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='180_0')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='180_0'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='385')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='385'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='385_1')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='385_1'
End


If Exists(select * from MNT_MENU_LIST where MENU_ID='177')
Begin
update MNT_MENU_LIST
set IS_ACTIVE='N' where MENU_ID='177'
End



If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='184')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='184'
End

If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='184_0')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='184_0'
End


If Exists(select * from MNT_MENU_LIST where MENU_ID='184')
Begin
update MNT_MENU_LIST
set IS_ACTIVE='N' where MENU_ID='184'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='221')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='221'
End

If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='221_0')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='221_0'
End


If Exists(select * from MNT_MENU_LIST where MENU_ID='221')
Begin
update MNT_MENU_LIST
set IS_ACTIVE='N' where MENU_ID='221'
End



If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='364')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='364'
End

If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='364_0')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='364_0'
End


If Exists(select * from MNT_MENU_LIST where MENU_ID='364')
Begin
update MNT_MENU_LIST
set IS_ACTIVE='N' where MENU_ID='364'
End




















