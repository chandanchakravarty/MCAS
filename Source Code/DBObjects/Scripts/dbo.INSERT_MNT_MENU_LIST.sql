IF NOT EXISTS(SELECT * FROM MNT_MENU_LIST WHERE MENU_ID=195)
BEGIN


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
     VALUES
           (195
           ,133
           ,'Add Quick Quote'
           ,'/cms/Policies/Aspx/QuickQuote.aspx?CALLEDFROM=QAPP'
           ,NULL
           ,2
           ,4
           ,0
           ,NULL
           ,'1'
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,1
           ,NULL
           ,NULL)
           
           
END

           
GO

