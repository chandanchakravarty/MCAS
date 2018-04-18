--select MAX(LOOKUP_ID) from MNT_LOOKUP_TABLES

IF NOT EXISTS(SELECT * FROM MNT_LOOKUP_TABLES WHERE LOOKUP_ID=1449 AND LOOKUP_NAME='FLTYP')
BEGIN
INSERT INTO MNT_LOOKUP_TABLES
(LOOKUP_ID,LOOKUP_NAME,LOOKUP_DESC,LOOKUP_TYPE,LOOKUP_TBL_EDIT,LOOKUP_CODE_WIDTH,LOOKUP_DISPLAY_FORMAT,LOOKUP_DESC_WIDTH,LOOKUP_LEVEL)
VALUES
(1449,'FLTYP','File Type','H','Y',null,null,null,null)
END
GO

IF NOT EXISTS(SELECT * FROM MNT_LOOKUP_TABLES_MULTILINGUAL WHERE LOOKUP_ID=1449 AND LANG_ID=2)
BEGIN
INSERT INTO MNT_LOOKUP_TABLES_MULTILINGUAL
(LOOKUP_ID,LOOKUP_DESC,LANG_ID)
VALUES
(1449,'File Type',2)
END
GO

-----------MNT_LOOKUP_VALUES------------------------
--select * FROM MNT_LOOKUP_VALUES order by LOOKUP_UNIQUE_ID asc

IF NOT EXISTS(SELECT * FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID=15014 and LOOKUP_ID=1449)
	BEGIN
		INSERT INTO MNT_LOOKUP_VALUES
		(LOOKUP_UNIQUE_ID,LOOKUP_ID,LOOKUP_VALUE_ID,LOOKUP_VALUE_CODE,LOOKUP_VALUE_DESC,LOOKUP_SYS_DEF,IS_ACTIVE,LAST_UPDATED_DATETIME,LOOKUP_FRAME_OR_MASONRY,Type
		 ,NSS_VALUE_CODE)
		 VALUES
		 (15014,1449,1,'UPL','Upload',null,'Y',null,null,null,null)
	 END
 
IF NOT EXISTS(SELECT * FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID=15015 and LOOKUP_ID=1449)
	BEGIN
		INSERT INTO MNT_LOOKUP_VALUES
		(LOOKUP_UNIQUE_ID,LOOKUP_ID,LOOKUP_VALUE_ID,LOOKUP_VALUE_CODE,LOOKUP_VALUE_DESC,LOOKUP_SYS_DEF,IS_ACTIVE,LAST_UPDATED_DATETIME,LOOKUP_FRAME_OR_MASONRY,Type
		 ,NSS_VALUE_CODE)
		 VALUES
		 (15015,1449,2,'GEN','Generate',null,'Y',null,null,null,null)
	END
-----------For MNT_LOOKUP_VALUES_MULTILINGUAL Table---------------
  
 IF NOT EXISTS(SELECT LOOKUP_UNIQUE_ID FROM MNT_LOOKUP_VALUES_MULTILINGUAL WHERE LOOKUP_UNIQUE_ID=15014 AND LANG_ID=2)
	BEGIN
		INSERT INTO MNT_LOOKUP_VALUES_MULTILINGUAL
		(LOOKUP_UNIQUE_ID,LOOKUP_VALUE_DESC,LANG_ID)
		 VALUES
		 (15014,'Carregar',2)
	 END
 GO
 
 IF NOT EXISTS(SELECT LOOKUP_UNIQUE_ID FROM MNT_LOOKUP_VALUES_MULTILINGUAL WHERE LOOKUP_UNIQUE_ID=15015 AND LANG_ID=2)
	BEGIN
		INSERT INTO MNT_LOOKUP_VALUES_MULTILINGUAL
		(LOOKUP_UNIQUE_ID,LOOKUP_VALUE_DESC,LANG_ID)
		 VALUES
		 (15015,'Gerar',2)
	 END
 GO
 