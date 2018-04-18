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
           '572'
           ,'Underwriting Questions'
           ,'/Cms/Policies/Aspx/MariTime/PolicyUWQMarine.aspx'
           ,[SCREEN_READ]
           ,[SCREEN_WRITE]
           ,[SCREEN_DELETE]
           ,[SCREEN_EXECUTE]
           ,[IS_ACTIVE]
           ,572
           FROM [dbo].[MNT_SCREEN_LIST] WHERE SCREEN_ID='566'