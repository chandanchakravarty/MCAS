
IF NOT EXISTS(SELECT * FROM MNT_LOOKUP_TABLES WHERE LOOKUP_ID=1439 AND LOOKUP_NAME='BNKTYP')
BEGIN
INSERT INTO MNT_LOOKUP_TABLES
(LOOKUP_ID,LOOKUP_NAME,LOOKUP_DESC,LOOKUP_TYPE,LOOKUP_TBL_EDIT,LOOKUP_CODE_WIDTH,LOOKUP_DISPLAY_FORMAT,LOOKUP_DESC_WIDTH,LOOKUP_LEVEL)
VALUES
(1439,'BNKTYP','Bank Type','H','Y',null,null,null,null)
END

IF NOT EXISTS(SELECT * FROM MNT_LOOKUP_TABLES_MULTILINGUAL WHERE LOOKUP_ID=1439)
BEGIN
INSERT INTO MNT_LOOKUP_TABLES_MULTILINGUAL
(LOOKUP_ID,LOOKUP_DESC,LANG_ID)
VALUES
(1439,'Tipo de banco',2)
END


IF NOT EXISTS(SELECT * FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID=14932 and LOOKUP_ID=1439)
	BEGIN
		INSERT INTO MNT_LOOKUP_VALUES
		(LOOKUP_UNIQUE_ID,LOOKUP_ID,LOOKUP_VALUE_ID,LOOKUP_VALUE_CODE,LOOKUP_VALUE_DESC,LOOKUP_SYS_DEF,IS_ACTIVE,LAST_UPDATED_DATETIME,LOOKUP_FRAME_OR_MASONRY,Type
		 ,NSS_VALUE_CODE)
		 VALUES
		 (14932,1439,1,'01','Receivables',null,'Y',null,null,null,null)
	 END
	 
	  IF NOT EXISTS(SELECT * FROM MNT_LOOKUP_VALUES_MULTILINGUAL WHERE LOOKUP_UNIQUE_ID=14932 AND LANG_ID=2)
	BEGIN
		INSERT INTO MNT_LOOKUP_VALUES_MULTILINGUAL
		(LOOKUP_UNIQUE_ID,LOOKUP_VALUE_DESC,LANG_ID)
		 VALUES
		 (14932,'recebíveis',2)
	 END
 GO
 
 
 
IF NOT EXISTS(SELECT * FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID=14931 and LOOKUP_ID=1439)
	BEGIN
		INSERT INTO MNT_LOOKUP_VALUES
		(LOOKUP_UNIQUE_ID,LOOKUP_ID,LOOKUP_VALUE_ID,LOOKUP_VALUE_CODE,LOOKUP_VALUE_DESC,LOOKUP_SYS_DEF,IS_ACTIVE,LAST_UPDATED_DATETIME,LOOKUP_FRAME_OR_MASONRY,Type
		 ,NSS_VALUE_CODE)
		 VALUES
		 (14931,1439,1,'01','Payables',null,'Y',null,null,null,null)
	 END
	 
	  IF NOT EXISTS(SELECT * FROM MNT_LOOKUP_VALUES_MULTILINGUAL WHERE LOOKUP_UNIQUE_ID=14931 AND LANG_ID=2)
	BEGIN
		INSERT INTO MNT_LOOKUP_VALUES_MULTILINGUAL
		(LOOKUP_UNIQUE_ID,LOOKUP_VALUE_DESC,LANG_ID)
		 VALUES
		 (14931,'contas a pagar',2)
	 END
 GO




