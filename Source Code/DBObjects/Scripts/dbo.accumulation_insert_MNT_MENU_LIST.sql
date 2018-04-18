
INSERT INTO MNT_MENU_LIST (MENU_ID,PARENT_ID,MENU_NAME,NESTLEVEL,HASCHILD,IS_ACTIVE,MENU_ORDER) SELECT 
(
select MAX(menu_id)+1 from mnt_menu_list where menu_id>1 and menu_id<1000
),
PARENT_ID,
'Accumulation',
NESTLEVEL,
1,
IS_ACTIVE,
15 
FROM MNT_MENU_LIST WHERE MENU_NAME='Reinsurance'

GO
DECLARE @MAX_MENU_ID INT

SELECT @MAX_MENU_ID = MAX(MENU_ID) + 1 from MNT_MENU_LIST WHERE MENU_ID < 1000
PRINT @MAX_MENU_ID
INSERT INTO [dbo].[MNT_MENU_LIST]
           ([MENU_ID]
           ,[PARENT_ID]
           ,[MENU_NAME]
           ,[MENU_LINK]
           ,[MENU_TOOLTIP]
           ,[NESTLEVEL]
           ,[MENU_ORDER]
           ,[HASCHILD]
           ,[SHOWTYPE]
           ,[HIDESTATUS]
           ,[ISSECURITY]
           ,[LANGID]
           ,[MENU_IMAGE]
           ,[DEFAULT_PAGE]
           ,[MODULE_NAME]
           ,[AGENCY_LEVEL]
           ,[LOB_CODE]
           ,[IS_ACTIVE])
SELECT @MAX_MENU_ID
      ,558
      ,'Accumulation Criteria'
      ,'/cms/cmsweb/Construction.html'
      ,[MENU_TOOLTIP]
      ,[NESTLEVEL]
      ,1
      ,[HASCHILD]
      ,[SHOWTYPE]
      ,[HIDESTATUS]
      ,[ISSECURITY]
      ,[LANGID]
      ,[MENU_IMAGE]
      ,[DEFAULT_PAGE]
      ,[MODULE_NAME]
      ,[AGENCY_LEVEL]
      ,[LOB_CODE]
      ,[IS_ACTIVE]
  FROM [dbo].[MNT_MENU_LIST]
  
  WHERE MENU_ID = 125
GO

DECLARE @MAX_MENU_ID INT

SELECT @MAX_MENU_ID = MAX(MENU_ID) + 1 from MNT_MENU_LIST WHERE MENU_ID < 1000

INSERT INTO [dbo].[MNT_MENU_LIST]
           ([MENU_ID]
           ,[PARENT_ID]
           ,[MENU_NAME]
           ,[MENU_LINK]
           ,[MENU_TOOLTIP]
           ,[NESTLEVEL]
           ,[MENU_ORDER]
           ,[HASCHILD]
           ,[SHOWTYPE]
           ,[HIDESTATUS]
           ,[ISSECURITY]
           ,[LANGID]
           ,[MENU_IMAGE]
           ,[DEFAULT_PAGE]
           ,[MODULE_NAME]
           ,[AGENCY_LEVEL]
           ,[LOB_CODE]
           ,[IS_ACTIVE])
SELECT @MAX_MENU_ID
      ,558
      ,'Accumulation Details'
      ,'/cms/cmsweb/Construction.html'
      ,[MENU_TOOLTIP]
      ,[NESTLEVEL]
      ,2
      ,[HASCHILD]
      ,[SHOWTYPE]
      ,[HIDESTATUS]
      ,[ISSECURITY]
      ,[LANGID]
      ,[MENU_IMAGE]
      ,[DEFAULT_PAGE]
      ,[MODULE_NAME]
      ,[AGENCY_LEVEL]
      ,[LOB_CODE]
      ,[IS_ACTIVE]
  FROM [dbo].[MNT_MENU_LIST]
  
  WHERE MENU_ID = 126
GO
 

UPDATE MNT_MENU_LIST SET MENU_LINK='/cms/cmsweb/Maintenance/Accumulation/AccumulationCriteriaIndex.aspx' WHERE MENU_ID=567
UPDATE MNT_MENU_LIST SET MENU_LINK='/cms/cmsweb/Maintenance/Accumulation/AccumulationReferenceIndex.aspx' WHERE MENU_ID=568



INSERT INTO [dbo].[MNT_SCREEN_LIST]
           ([SCREEN_ID]
           ,[SCREEN_DESC]
           ,[SCREEN_PATH]
           ,[SCREEN_READ]
           ,[SCREEN_WRITE]
           ,[SCREEN_DELETE]
           ,[SCREEN_EXECUTE]
           ,[IS_ACTIVE])
    SELECT
           '567'
           ,'Accumulation Criteria'
           ,'/cms/cmsweb/Maintenance/Accumulation/AccumulationCriteriaIndex.aspx'
           ,[SCREEN_READ]
           ,[SCREEN_WRITE]
           ,[SCREEN_DELETE]
           ,[SCREEN_EXECUTE]
           ,[IS_ACTIVE]
           FROM [dbo].[MNT_SCREEN_LIST] WHERE SCREEN_ID='566'
GO

INSERT INTO [dbo].[MNT_SCREEN_LIST]
           ([SCREEN_ID]
           ,[SCREEN_DESC]
           ,[SCREEN_PATH]
           ,[SCREEN_READ]
           ,[SCREEN_WRITE]
           ,[SCREEN_DELETE]
           ,[SCREEN_EXECUTE]
           ,[IS_ACTIVE])
    SELECT
           '567_0'
           ,'Accumulation Criteria Details'
           ,'/cms/cmsweb/Maintenance/Accumulation/AddAccumulationCriteria.aspx'
           ,[SCREEN_READ]
           ,[SCREEN_WRITE]
           ,[SCREEN_DELETE]
           ,[SCREEN_EXECUTE]
           ,[IS_ACTIVE]
           FROM [dbo].[MNT_SCREEN_LIST] WHERE SCREEN_ID='566'
GO

--for accumulation reference
INSERT INTO [dbo].[MNT_SCREEN_LIST]
           ([SCREEN_ID]
           ,[SCREEN_DESC]
           ,[SCREEN_PATH]
           ,[SCREEN_READ]
           ,[SCREEN_WRITE]
           ,[SCREEN_DELETE]
           ,[SCREEN_EXECUTE]
           ,[IS_ACTIVE])
    SELECT
           '568'
           ,'Accumulation Reference'
           ,'/cms/cmsweb/Maintenance/Accumulation/AccumulationReferenceIndex.aspx'
           ,[SCREEN_READ]
           ,[SCREEN_WRITE]
           ,[SCREEN_DELETE]
           ,[SCREEN_EXECUTE]
           ,[IS_ACTIVE]
           FROM [dbo].[MNT_SCREEN_LIST] WHERE SCREEN_ID='567'
GO

INSERT INTO [dbo].[MNT_SCREEN_LIST]
           ([SCREEN_ID]
           ,[SCREEN_DESC]
           ,[SCREEN_PATH]
           ,[SCREEN_READ]
           ,[SCREEN_WRITE]
           ,[SCREEN_DELETE]
           ,[SCREEN_EXECUTE]
           ,[IS_ACTIVE])
    SELECT
           '568_0'
           ,'Accumulation Reference Details'
           ,'/cms/cmsweb/Maintenance/Accumulation/AddAccumulationReference.aspx'
           ,[SCREEN_READ]
           ,[SCREEN_WRITE]
           ,[SCREEN_DELETE]
           ,[SCREEN_EXECUTE]
           ,[IS_ACTIVE]
           FROM [dbo].[MNT_SCREEN_LIST] WHERE SCREEN_ID='567'
GO

--TO ADD  VALUES FOR NEWLY ADDED COLUMN MENU_ID

UPDATE MNT_SCREEN_LIST SET MENU_ID=568 WHERE SCREEN_ID IN('568','568_0')