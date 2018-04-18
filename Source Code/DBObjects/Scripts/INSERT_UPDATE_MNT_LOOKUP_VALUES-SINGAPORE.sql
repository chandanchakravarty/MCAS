--Add Giro and Cash into MNT_LookUp_values by checking that lookup_unique_id doesn't Exist 
IF NOT EXISTS (select * from MNT_LOOKUP_VALUES where LOOKUP_UNIQUE_ID = 14934)
BEGIN
INSERT INTO MNT_LOOKUP_VALUES
	   (
	    LOOKUP_UNIQUE_ID
           ,LOOKUP_ID
           ,LOOKUP_VALUE_ID
           ,LOOKUP_VALUE_CODE
           ,LOOKUP_VALUE_DESC
           ,LOOKUP_SYS_DEF
           ,IS_ACTIVE
           )
     VALUES
           (
           14934 
           ,1303
           ,5
           ,'PPYMOD5'
           ,'Cash'
           ,'5'
           ,'y'
           )
END

IF NOT EXISTS (select * from MNT_LOOKUP_VALUES where LOOKUP_UNIQUE_ID = 14935)
BEGIN
INSERT INTO MNT_LOOKUP_VALUES
	   (
	    LOOKUP_UNIQUE_ID
           ,LOOKUP_ID
           ,LOOKUP_VALUE_ID
           ,LOOKUP_VALUE_CODE
           ,LOOKUP_VALUE_DESC
           ,LOOKUP_SYS_DEF
           ,IS_ACTIVE
           )
     VALUES
           (
           14935 
           ,1303
           ,6
           ,'PPYMOD6'
           ,'Giro'
           ,'6'
           ,'y'
           )
END
--Add Giro and Cash into MNT_LOOKUP_VALUES_MULTILINGUAL by checking that lookup_unique_id doesn't Exist 
IF Not Exists (select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID = 14935)
BEGIN
INSERT INTO MNT_LOOKUP_VALUES_MULTILINGUAL
           (
				LOOKUP_UNIQUE_ID
			   ,LOOKUP_VALUE_DESC
			   ,LANG_ID
			)
     VALUES
           ( 14935,
			'Giro'
			,3
           )
End


IF Not Exists (select * from MNT_LOOKUP_VALUES_MULTILINGUAL where LOOKUP_UNIQUE_ID = 14934)
BEGIN
INSERT INTO MNT_LOOKUP_VALUES_MULTILINGUAL
           (
				LOOKUP_UNIQUE_ID
			   ,LOOKUP_VALUE_DESC
			   ,LANG_ID
			)
     VALUES
           ( 14934,
			'Cash'
			,3
           )
End
IF EXISTS(SELECT IS_ACTIVE from MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID=14558 AND LOOKUP_VALUE_DESC='Boleto' AND IS_ACTIVE='Y')
	BEGIN
		UPDATE MNT_LOOKUP_VALUES SET IS_ACTIVE='N' WHERE LOOKUP_UNIQUE_ID=14558 AND LOOKUP_VALUE_DESC='Boleto'
	END
IF EXISTS(SELECT IS_ACTIVE from MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID=11973 AND LOOKUP_VALUE_DESC='EFT' AND IS_ACTIVE='Y')
	BEGIN
		UPDATE MNT_LOOKUP_VALUES SET IS_ACTIVE='N' WHERE LOOKUP_UNIQUE_ID=11973 AND LOOKUP_VALUE_DESC='EFT'
	END
