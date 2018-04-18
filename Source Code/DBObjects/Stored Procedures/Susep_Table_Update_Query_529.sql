--BEGIN TRAN 
IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID = 1)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET ITRACK_NO = '1208' WHERE REPORT_ID = 1
END

IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID = 2)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET ITRACK_NO = '1208' WHERE REPORT_ID = 2
END

IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID = 3)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET ITRACK_NO = '1208' WHERE REPORT_ID = 3
END

IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID = 4)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET ITRACK_NO = '1208' WHERE REPORT_ID =4
END

-----
IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID = 1)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET FILE_NAME = 'APURACAO_DE.XLS' WHERE REPORT_ID = 1
END

IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID =2)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET FILE_NAME = 'SINISTRA_BROKER.XLS' WHERE REPORT_ID =2
END

IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID = 3)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET FILE_NAME = 'SINISTRA_CLAIM.XLS' WHERE REPORT_ID =3
END

IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID = 4)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET FILE_NAME = 'EMITIDA_E_SINIS.XLS' WHERE REPORT_ID = 4
END


-----
--LAST THREE LINES


IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID =42)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET FILE_NAME = 'Quadro 322.txt', ITRACK_NO='1062'  WHERE REPORT_ID = 42
END

IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID =43)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET FILE_NAME = 'SALRESAV.DBF' ,ITRACK_NO='1248' WHERE REPORT_ID =43
END
IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID = 44)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET FILE_NAME = 'SALRESAC.DBF',ITRACK_NO='1249' WHERE REPORT_ID = 44
END


--

IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID = 32)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET FILE_NAME = 'SINJUDAV.DBF' WHERE REPORT_ID = 32
END

IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID = 37)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET FILE_NAME = 'Quadro 270.txt' WHERE REPORT_ID = 37
END



-----

IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID = 5)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET FILE_NAME = 'Q14A.XLS' WHERE REPORT_ID = 5
END


IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID = 6)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET FILE_NAME = 'Q51.XLS' WHERE REPORT_ID = 6
END

IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID = 7)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET FILE_NAME = 'Q51R.XLS' WHERE REPORT_ID = 7
END


IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID = 8)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET FILE_NAME = 'Q52.XLS'  WHERE REPORT_ID = 8
END

IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID = 9)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET FILE_NAME = 'Q66.XLS' WHERE REPORT_ID = 9
END

IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID = 10)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET FILE_NAME = 'Quadro 271.txt' WHERE REPORT_ID = 10
END

IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID = 11)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET FILE_NAME = 'Quadro 272.txt'  WHERE REPORT_ID = 11
END

IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID = 12)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET FILE_NAME = 'Quadro 323.txt'  WHERE REPORT_ID = 12
END

IF EXISTS (SELECT * FROM MNT_SUSEP_REPORTS_LIST WHERE REPORT_ID = 13)
BEGIN
UPDATE MNT_SUSEP_REPORTS_LIST SET FILE_NAME = 'Quadro 324.txt'  WHERE REPORT_ID = 13
END
--select * from MNT_SUSEP_REPORTS_LIST

--ROLLBACK TRAN