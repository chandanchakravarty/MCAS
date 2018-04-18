

IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST WHERE MENU_ID=561)
			BEGIN
				INSERT INTO MNT_MENU_LIST (MENU_ID,PARENT_ID,MENU_NAME,MENU_LINK,MENU_TOOLTIP,NESTLEVEL,MENU_ORDER,HASCHILD,SHOWTYPE,HIDESTATUS,ISSECURITY,LANGID,MENU_IMAGE,DEFAULT_PAGE,MODULE_NAME,AGENCY_LEVEL,LOB_CODE,IS_ACTIVE)
									VALUES(561,13,'Master Setup','/cms/cmsweb/Maintenance/MasterSetupIndex.aspx',NULL,2,21,0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)
			END


IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST_MULTILINGUAL WHERE MENU_ID=561)
	BEGIN
		INSERT INTO MNT_MENU_LIST_MULTILINGUAL (MENU_ID,MENU_NAME,LANG_ID) VALUES(561,'Configuração mestre',2)
	END		

----------------FOR Consultant Master--------------------
IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST WHERE MENU_ID=562)
			BEGIN
				INSERT INTO MNT_MENU_LIST (MENU_ID,PARENT_ID,MENU_NAME,MENU_LINK,MENU_TOOLTIP,NESTLEVEL,MENU_ORDER,HASCHILD,SHOWTYPE,HIDESTATUS,ISSECURITY,LANGID,MENU_IMAGE,DEFAULT_PAGE,MODULE_NAME,AGENCY_LEVEL,LOB_CODE,IS_ACTIVE)
									VALUES(562,561,'Consultant Master','/cms/cmsweb/Maintenance/MasterSetupIndex.aspx?TYPE_ID=1',NULL,3,1,0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)
			END


IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST_MULTILINGUAL WHERE MENU_ID=562)
	BEGIN
		INSERT INTO MNT_MENU_LIST_MULTILINGUAL (MENU_ID,MENU_NAME,LANG_ID) VALUES(562,'Mestre consultor',2)
	END	


---------------FOR Surveyor Master------------------------
IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST WHERE MENU_ID=563)
			BEGIN
				INSERT INTO MNT_MENU_LIST (MENU_ID,PARENT_ID,MENU_NAME,MENU_LINK,MENU_TOOLTIP,NESTLEVEL,MENU_ORDER,HASCHILD,SHOWTYPE,HIDESTATUS,ISSECURITY,LANGID,MENU_IMAGE,DEFAULT_PAGE,MODULE_NAME,AGENCY_LEVEL,LOB_CODE,IS_ACTIVE)
									VALUES(563,561,'Surveyor Master','/cms/cmsweb/Maintenance/MasterSetupIndex.aspx?TYPE_ID=2',NULL,3,1,0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)
			END


IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST_MULTILINGUAL WHERE MENU_ID=563)
	BEGIN
		INSERT INTO MNT_MENU_LIST_MULTILINGUAL (MENU_ID,MENU_NAME,LANG_ID) VALUES(563,'Mestre Surveyor',2)
	END	
			
			
---------------FOR WorkShop Master------------------------
IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST WHERE MENU_ID=564)
			BEGIN
				INSERT INTO MNT_MENU_LIST (MENU_ID,PARENT_ID,MENU_NAME,MENU_LINK,MENU_TOOLTIP,NESTLEVEL,MENU_ORDER,HASCHILD,SHOWTYPE,HIDESTATUS,ISSECURITY,LANGID,MENU_IMAGE,DEFAULT_PAGE,MODULE_NAME,AGENCY_LEVEL,LOB_CODE,IS_ACTIVE)
									VALUES(564,561,'WorkShop Master','/cms/cmsweb/Maintenance/MasterSetupIndex.aspx?TYPE_ID=3',NULL,3,1,0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)
			END


IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST_MULTILINGUAL WHERE MENU_ID=564)
	BEGIN
		INSERT INTO MNT_MENU_LIST_MULTILINGUAL (MENU_ID,MENU_NAME,LANG_ID) VALUES(564,'WorkShop Mestre',2)
	END	
				
---------------FOR Solicitor Master------------------------
IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST WHERE MENU_ID=565)
			BEGIN
				INSERT INTO MNT_MENU_LIST (MENU_ID,PARENT_ID,MENU_NAME,MENU_LINK,MENU_TOOLTIP,NESTLEVEL,MENU_ORDER,HASCHILD,SHOWTYPE,HIDESTATUS,ISSECURITY,LANGID,MENU_IMAGE,DEFAULT_PAGE,MODULE_NAME,AGENCY_LEVEL,LOB_CODE,IS_ACTIVE)
									VALUES(565,561,'Solicitor Master','/cms/cmsweb/Maintenance/MasterSetupIndex.aspx?TYPE_ID=4',NULL,3,1,0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)
			END


IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST_MULTILINGUAL WHERE MENU_ID=565)
	BEGIN
		INSERT INTO MNT_MENU_LIST_MULTILINGUAL (MENU_ID,MENU_NAME,LANG_ID) VALUES(565,'procurador Mestre',2)
	END	
			
			
---------------FOR Loss Nature Master Details------------------------
IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST WHERE MENU_ID=566)
			BEGIN
				INSERT INTO MNT_MENU_LIST (MENU_ID,PARENT_ID,MENU_NAME,MENU_LINK,MENU_TOOLTIP,NESTLEVEL,MENU_ORDER,HASCHILD,SHOWTYPE,HIDESTATUS,ISSECURITY,LANGID,MENU_IMAGE,DEFAULT_PAGE,MODULE_NAME,AGENCY_LEVEL,LOB_CODE,IS_ACTIVE)
									VALUES(566,561,'Loss Nature Master Details','/cms/cmsweb/Maintenance/MasterValuesIndex.aspx?TYPE_ID=1',NULL,3,1,0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'N')
			END


IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST_MULTILINGUAL WHERE MENU_ID=566)
	BEGIN
		INSERT INTO MNT_MENU_LIST_MULTILINGUAL (MENU_ID,MENU_NAME,LANG_ID) VALUES(566,'Detalhes perda de dominar a natureza',2)
	END	
					

---------------FOR Recovery Master Details------------------------
IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST WHERE MENU_ID=567)
			BEGIN
				INSERT INTO MNT_MENU_LIST (MENU_ID,PARENT_ID,MENU_NAME,MENU_LINK,MENU_TOOLTIP,NESTLEVEL,MENU_ORDER,HASCHILD,SHOWTYPE,HIDESTATUS,ISSECURITY,LANGID,MENU_IMAGE,DEFAULT_PAGE,MODULE_NAME,AGENCY_LEVEL,LOB_CODE,IS_ACTIVE)
									VALUES(567,561,'Recovery Master Details','/cms/cmsweb/Maintenance/MasterValuesIndex.aspx?TYPE_ID=2',NULL,3,1,0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)
			END


IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST_MULTILINGUAL WHERE MENU_ID=567)
	BEGIN
		INSERT INTO MNT_MENU_LIST_MULTILINGUAL (MENU_ID,MENU_NAME,LANG_ID) VALUES(567,'Mestre Detalhes recuperação',2)
	END	
			
			
---------------FOR Fee Master Details------------------------
IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST WHERE MENU_ID=568)
			BEGIN
				INSERT INTO MNT_MENU_LIST (MENU_ID,PARENT_ID,MENU_NAME,MENU_LINK,MENU_TOOLTIP,NESTLEVEL,MENU_ORDER,HASCHILD,SHOWTYPE,HIDESTATUS,ISSECURITY,LANGID,MENU_IMAGE,DEFAULT_PAGE,MODULE_NAME,AGENCY_LEVEL,LOB_CODE,IS_ACTIVE)
									VALUES(568,561,'Fee Master Details','/cms/cmsweb/Maintenance/MasterValuesIndex.aspx?TYPE_ID=3',NULL,3,1,0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)
			END


IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST_MULTILINGUAL WHERE MENU_ID=568)
	BEGIN
		INSERT INTO MNT_MENU_LIST_MULTILINGUAL (MENU_ID,MENU_NAME,LANG_ID) VALUES(568,'Mestre Detalhes taxa',2)
	END	

