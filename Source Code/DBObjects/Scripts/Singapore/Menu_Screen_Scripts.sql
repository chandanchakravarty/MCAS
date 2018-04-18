--select * from mnt_menu_list where menu_name like '%lookup%'
--select * from MNT_SCREEN_LIST where SCREEN_PATH like '%lookup%'
--select * from MNT_SCREEN_LIST where SCREEN_ID = '569'

--SELECT * FROM MNT_MENU_LIST ORDER BY MENU_ID 

--select * from mnt_menu_list where parent_id = 559

--select * from MNT_COUNTRY_LIST 

go

DECLARE @MENUID INT
SELECT @MENUID = MAX(MENU_ID) + 1 from mnt_menu_list WHERE MENU_ID < 5000
INSERT INTO MNT_MENU_LIST (MENU_ID,PARENT_ID,MENU_NAME,MENU_LINK,MENU_TOOLTIP,NESTLEVEL,MENU_ORDER,HASCHILD,IS_ACTIVE) VALUES
(@MENUID,559,'Vessel Master','/cms/cmsweb/maintenance/VesselMasterIndex.aspx','',3,1,0,'Y')


insert into MNT_SCREEN_LIST (SCREEN_ID,SCREEN_DESC,SCREEN_PATH,SCREEN_READ,SCREEN_WRITE,SCREEN_DELETE,SCREEN_EXECUTE,IS_ACTIVE,MENU_ID) values
('569','Vessel Master List','/cms/cmsweb/maintenance/VesselMasterIndex.aspx',1,1,1,1,'Y',@MENUID)


insert into MNT_SCREEN_LIST (SCREEN_ID,SCREEN_DESC,SCREEN_PATH,SCREEN_READ,SCREEN_WRITE,SCREEN_DELETE,SCREEN_EXECUTE,IS_ACTIVE,MENU_ID) values
('569_0','Vessel Master Info','/cms/cmsweb/maintenance/AddVesselMaster.aspx',1,1,1,1,'Y',@MENUID)








