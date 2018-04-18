

IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST WHERE MENU_ID=559)
			BEGIN
				INSERT INTO MNT_MENU_LIST (MENU_ID,PARENT_ID,MENU_NAME,MENU_LINK,MENU_TOOLTIP,NESTLEVEL,MENU_ORDER,HASCHILD,SHOWTYPE,HIDESTATUS,ISSECURITY,LANGID,MENU_IMAGE,DEFAULT_PAGE,MODULE_NAME,AGENCY_LEVEL,LOB_CODE,IS_ACTIVE)
									VALUES(559,524,'Monetary Index','/Cms/CmsWeb/maintenance/MonetaryIndex.aspx',NULL,3,1,0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'Y')
			END
			
			
IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST_MULTILINGUAL WHERE MENU_ID=559)
	BEGIN
		INSERT INTO MNT_MENU_LIST_MULTILINGUAL (MENU_ID,MENU_NAME,LANG_ID) VALUES(559,'�ndice monet�ria',2)
	END
				

IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST WHERE MENU_ID=560)
			BEGIN
				INSERT INTO MNT_MENU_LIST (MENU_ID,PARENT_ID,MENU_NAME,MENU_LINK,MENU_TOOLTIP,NESTLEVEL,MENU_ORDER,HASCHILD,SHOWTYPE,HIDESTATUS,ISSECURITY,LANGID,MENU_IMAGE,DEFAULT_PAGE,MODULE_NAME,AGENCY_LEVEL,LOB_CODE,IS_ACTIVE)
									VALUES(560,524,'Interest Rates','/Cms/CmsWeb/maintenance/InterestRateIndex.aspx',NULL,3,1,0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'Y')
			END	
			


IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST_MULTILINGUAL WHERE MENU_ID=560)
	BEGIN
		INSERT INTO MNT_MENU_LIST_MULTILINGUAL (MENU_ID,MENU_NAME,LANG_ID) VALUES(560,'Taxas de Juros',2)
	END
	
	
IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST WHERE MENU_ID=561)
		BEGIN
			INSERT INTO MNT_MENU_LIST (MENU_ID,PARENT_ID,MENU_NAME,MENU_LINK,MENU_TOOLTIP,NESTLEVEL,MENU_ORDER,HASCHILD,SHOWTYPE,HIDESTATUS,ISSECURITY,LANGID,MENU_IMAGE,DEFAULT_PAGE,MODULE_NAME,AGENCY_LEVEL,LOB_CODE,IS_ACTIVE)
								VALUES(561,524,'Fee Limit','/Cms/CmsWeb/maintenance/AddFeeLimit.aspx',NULL,3,1,0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'Y')
		END
		
	
IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST_MULTILINGUAL WHERE MENU_ID=561)
	BEGIN
		INSERT INTO MNT_MENU_LIST_MULTILINGUAL (MENU_ID,MENU_NAME,LANG_ID) VALUES(561,'Limite de taxa',2)
	END
	
	
	
IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST WHERE MENU_ID=562)
		BEGIN
			INSERT INTO MNT_MENU_LIST (MENU_ID,PARENT_ID,MENU_NAME,MENU_LINK,MENU_TOOLTIP,NESTLEVEL,MENU_ORDER,HASCHILD,SHOWTYPE,HIDESTATUS,ISSECURITY,LANGID,MENU_IMAGE,DEFAULT_PAGE,MODULE_NAME,AGENCY_LEVEL,LOB_CODE,IS_ACTIVE)
								VALUES(562,524,'IOF Percentage','/Cms/CmsWeb/maintenance/IOFIndex.aspx',NULL,3,1,0,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,'Y')
		END
		
	
IF NOT EXISTS (SELECT * FROM MNT_MENU_LIST_MULTILINGUAL WHERE MENU_ID=562)
	BEGIN
		INSERT INTO MNT_MENU_LIST_MULTILINGUAL (MENU_ID,MENU_NAME,LANG_ID) VALUES(562,'Percentual IOF',2)
	END
	
	
	
IF NOT EXISTS (SELECT * FROM MNT_SCREEN_LIST WHERE SCREEN_ID='560')
BEGIN 
  INSERT INTO MNT_SCREEN_LIST (SCREEN_ID,SCREEN_DESC,SCREEN_PATH,SCREEN_READ,SCREEN_WRITE,SCREEN_DELETE,SCREEN_EXECUTE,IS_ACTIVE) 
		VALUES('560','Interest Rates','/cms/cmsweb/maintenance/InterestRateIndex.aspx','1','1','1','1','Y')
END
		
IF NOT EXISTS (SELECT * FROM MNT_SCREEN_LIST_MULTILINGUAL  WHERE SCREEN_ID='560')
BEGIN 
  INSERT INTO MNT_SCREEN_LIST_MULTILINGUAL (SCREEN_ID,SCREEN_DESC,LANG_ID) 
		VALUES('560','Taxas de Juros',2)
END	


IF NOT EXISTS (SELECT * FROM MNT_SCREEN_LIST WHERE SCREEN_ID='560_0')
BEGIN 
  INSERT INTO MNT_SCREEN_LIST (SCREEN_ID,SCREEN_DESC,SCREEN_PATH,SCREEN_READ,SCREEN_WRITE,SCREEN_DELETE,SCREEN_EXECUTE,IS_ACTIVE) 
		VALUES('560_0','Interest Rates Details','/cms/cmsweb/maintenance/InterestRateDetail.aspx','1','1','1','1','Y')
END
		
IF NOT EXISTS (SELECT * FROM MNT_SCREEN_LIST_MULTILINGUAL  WHERE SCREEN_ID='560_0')
BEGIN 
  INSERT INTO MNT_SCREEN_LIST_MULTILINGUAL (SCREEN_ID,SCREEN_DESC,LANG_ID) 
		VALUES('560_0','Taxas de juros Detalhes',2)
END	


IF NOT EXISTS (SELECT * FROM MNT_SCREEN_LIST WHERE SCREEN_ID='561_0')
BEGIN 
  INSERT INTO MNT_SCREEN_LIST (SCREEN_ID,SCREEN_DESC,SCREEN_PATH,SCREEN_READ,SCREEN_WRITE,SCREEN_DELETE,SCREEN_EXECUTE,IS_ACTIVE) 
		VALUES('561_0','Fee Limit','/cms/cmsweb/maintenance/AddFeeLimit.aspx','1','1','1','1','Y')
END
		
IF NOT EXISTS (SELECT * FROM MNT_SCREEN_LIST_MULTILINGUAL  WHERE SCREEN_ID='561_0')
BEGIN 
  INSERT INTO MNT_SCREEN_LIST_MULTILINGUAL (SCREEN_ID,SCREEN_DESC,LANG_ID) 
		VALUES('561_0','Limite de taxa',2)
END


IF NOT EXISTS (SELECT * FROM MNT_SCREEN_LIST WHERE SCREEN_ID='562')
BEGIN 
  INSERT INTO MNT_SCREEN_LIST (SCREEN_ID,SCREEN_DESC,SCREEN_PATH,SCREEN_READ,SCREEN_WRITE,SCREEN_DELETE,SCREEN_EXECUTE,IS_ACTIVE) 
		VALUES('562','IOF Percentage','/cms/cmsweb/maintenance/IOFIndex.aspx','1','1','1','1','Y')
END
		
IF NOT EXISTS (SELECT * FROM MNT_SCREEN_LIST_MULTILINGUAL  WHERE SCREEN_ID='562')
BEGIN 
  INSERT INTO MNT_SCREEN_LIST_MULTILINGUAL (SCREEN_ID,SCREEN_DESC,LANG_ID) 
		VALUES('562','Percentual IOF',2)
END


IF NOT EXISTS (SELECT * FROM MNT_SCREEN_LIST WHERE SCREEN_ID='562_0')
BEGIN 
  INSERT INTO MNT_SCREEN_LIST (SCREEN_ID,SCREEN_DESC,SCREEN_PATH,SCREEN_READ,SCREEN_WRITE,SCREEN_DELETE,SCREEN_EXECUTE,IS_ACTIVE) 
		VALUES('562_0','Add IOF Percentage','/cms/cmsweb/maintenance/AddIOFDetails.aspx','1','1','1','1','Y')
END
		
IF NOT EXISTS (SELECT * FROM MNT_SCREEN_LIST_MULTILINGUAL  WHERE SCREEN_ID='562_0')
BEGIN 
  INSERT INTO MNT_SCREEN_LIST_MULTILINGUAL (SCREEN_ID,SCREEN_DESC,LANG_ID) 
		VALUES('562_0','Adicionar Percentagem IOF',2)
END