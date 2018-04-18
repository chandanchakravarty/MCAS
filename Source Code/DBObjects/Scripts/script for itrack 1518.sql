
IF NOT EXISTS (SELECT  * FROM MNT_SCREEN_LIST WHERE SCREEN_ID='533')
BEGIN
INSERT INTO MNT_SCREEN_LIST(SCREEN_ID, SCREEN_DESC,SCREEN_PATH, SCREEN_READ, SCREEN_WRITE, SCREEN_DELETE, SCREEN_EXECUTE, IS_ACTIVE)
VALUES
('533', 'Issuance Report', 'Cms\Reports\Aspx\IssuanceReport.aspx', 1, 1, 1, 1, 'Y')
END	

IF NOT EXISTS (SELECT  * FROM MNT_SCREEN_LIST_MULTILINGUAL WHERE SCREEN_ID='533')
BEGIN
INSERT INTO MNT_SCREEN_LIST_MULTILINGUAL(SCREEN_ID, SCREEN_DESC,LANG_ID)
VALUES
('533','Relatorio Emissao', 2)
END	

IF NOT EXISTS (SELECT  * FROM MNT_SCREEN_LIST WHERE SCREEN_ID='530')
BEGIN
INSERT INTO MNT_SCREEN_LIST(SCREEN_ID, SCREEN_DESC,SCREEN_PATH, SCREEN_READ, SCREEN_WRITE, SCREEN_DELETE, SCREEN_EXECUTE, IS_ACTIVE)
VALUES
('530', 'RI COI Breakdown', '/cms/Reports/Aspx/RI_COI_Breakdown.aspx', 1, 1, 1, 1, 'Y')
END	

IF NOT EXISTS (SELECT  * FROM MNT_SCREEN_LIST_MULTILINGUAL WHERE SCREEN_ID='530')
BEGIN
INSERT INTO MNT_SCREEN_LIST_MULTILINGUAL(SCREEN_ID, SCREEN_DESC,LANG_ID)VALUES('530','Relat�rio de Resseguro e Cosseguro', 2)
END	


----New Screen id and menu  id for Management Reports

IF NOT EXISTS(SELECT * FROM MNT_MENU_LIST WHERE MENU_ID=550 and PARENT_ID=326)
BEGIN
INSERT INTO MNT_MENU_LIST
(MENU_ID,PARENT_ID,MENU_NAME,MENU_LINK,MENU_TOOLTIP,NESTLEVEL,MENU_ORDER,HASCHILD,SHOWTYPE,HIDESTATUS,ISSECURITY,LANGID,MENU_IMAGE,DEFAULT_PAGE,MODULE_NAME,AGENCY_LEVEL,LOB_CODE,IS_ACTIVE)
VALUES
(550,326,'Management Reports','/cms/Reports/Aspx/ManagementReports.aspx',NULL,2,20,0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'Y')
END

IF NOT EXISTS(SELECT * FROM MNT_MENU_LIST_MULTILINGUAL WHERE MENU_ID=550 AND LANG_ID=2)
BEGIN
INSERT INTO MNT_MENU_LIST_MULTILINGUAL(MENU_ID,MENU_NAME,LANG_ID)
VALUES
(550,'Relat�rios de Gest�o',2)
END

IF NOT EXISTS (SELECT  * FROM MNT_SCREEN_LIST WHERE SCREEN_ID='550')
BEGIN
INSERT INTO MNT_SCREEN_LIST(SCREEN_ID, SCREEN_DESC,SCREEN_PATH, SCREEN_READ, SCREEN_WRITE, SCREEN_DELETE, SCREEN_EXECUTE, IS_ACTIVE)
VALUES
('550', 'Management Reports', '/cms/Reports/Aspx/ManagementReports.aspx', 1, 1, 1, 1, 'Y')
END	

IF NOT EXISTS (SELECT  * FROM MNT_SCREEN_LIST_MULTILINGUAL WHERE SCREEN_ID='550')
BEGIN
INSERT INTO MNT_SCREEN_LIST_MULTILINGUAL(SCREEN_ID, SCREEN_DESC,LANG_ID)VALUES('550','Relat�rios de Gest�o', 2)
END	



----Execute this script  for Removing Assigning right 
If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='336')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='336'
End

If Exists(select * from MNT_MENU_LIST where MENU_ID='336')
Begin
update MNT_MENU_LIST
set IS_ACTIVE='N' where MENU_ID='336'
End

If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='442_0')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='442_0'
End

If Exists(select * from MNT_MENU_LIST where MENU_ID='442')
Begin
update MNT_MENU_LIST
set IS_ACTIVE='N' where MENU_ID='442'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='328')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='328'
End

If Exists(select * from MNT_MENU_LIST where MENU_ID='328')
Begin
update MNT_MENU_LIST
set IS_ACTIVE='N' where MENU_ID='328'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='329')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='329'
End

If Exists(select * from MNT_MENU_LIST where MENU_ID='329')
Begin
update MNT_MENU_LIST
set IS_ACTIVE='N' where MENU_ID='329'
End


If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='331')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='331'
End

If Exists(select * from MNT_MENU_LIST where MENU_ID='331')
Begin
update MNT_MENU_LIST
set IS_ACTIVE='N' where MENU_ID='331'
End

If Exists(select * from MNT_SCREEN_LIST where SCREEN_ID='330')
Begin
update MNT_SCREEN_LIST
set IS_ACTIVE='N' where SCREEN_ID='330'
End

If Exists(select * from MNT_MENU_LIST where MENU_ID='330')
Begin
update MNT_MENU_LIST
set IS_ACTIVE='N' where MENU_ID='330'
End