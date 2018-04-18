IF NOT EXISTS(SELECT * FROM MNT_MENU_LIST WHERE MENU_ID=571)
BEGIN


INSERT INTO [MNT_MENU_LIST]
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
     VALUES
           (571
           ,286
           ,'Marine Cargo Settling Agents'
           ,'/Cms/CmsWeb/Maintenance/MarineCargoSetAgentsIndex.aspx'
           ,NULL
           ,2
           ,6
           ,0
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,'Y')
           
           
END

           
GO

--delete from MNT_MENU_LIST where MENU_ID=570
--SELECT * FROM MNT_SCREEN_LIST WHERE SCREEN_ID='570'

INSERT INTO [dbo].[MNT_SCREEN_LIST]
           ([SCREEN_ID]
           ,[SCREEN_DESC]
           ,[SCREEN_PATH]
           ,[SCREEN_READ]
           ,[SCREEN_WRITE]
           ,[SCREEN_DELETE]
           ,[SCREEN_EXECUTE]
           ,[IS_ACTIVE]
           ,MENU_ID)
    SELECT
           '571'
           ,'Marine Cargo Settling Agents'
           ,'/cms/cmsweb/Maintenance/MarineCargoSetAgentsIndex.aspx'
           ,[SCREEN_READ]
           ,[SCREEN_WRITE]
           ,[SCREEN_DELETE]
           ,[SCREEN_EXECUTE]
           ,[IS_ACTIVE]
           ,571
           FROM [dbo].[MNT_SCREEN_LIST] WHERE SCREEN_ID='566'