IF NOT EXISTS(SELECT * FROM MNT_LOOKUP_TABLES WHERE LOOKUP_ID=1437)
BEGIN

INSERT INTO [dbo].[MNT_LOOKUP_TABLES]
           ([LOOKUP_ID]
           ,[LOOKUP_NAME]
           ,[LOOKUP_DESC]
           ,[LOOKUP_TYPE]
           ,[LOOKUP_TBL_EDIT]
           ,[LOOKUP_CODE_WIDTH]
           ,[LOOKUP_DISPLAY_FORMAT]
           ,[LOOKUP_DESC_WIDTH]
           ,[LOOKUP_LEVEL])

SELECT 1437
      ,'CSTTYP'
      ,'Customer Type'
      ,[LOOKUP_TYPE]
      ,[LOOKUP_TBL_EDIT]
      ,[LOOKUP_CODE_WIDTH]
      ,[LOOKUP_DISPLAY_FORMAT]
      ,[LOOKUP_DESC_WIDTH]
      ,[LOOKUP_LEVEL]
      
FROM [MNT_LOOKUP_TABLES]
WHERE LOOKUP_ID = 1187


END

----############################################################################################################

IF NOT EXISTS(SELECT * FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID=114302)
BEGIN

INSERT INTO [dbo].[MNT_LOOKUP_VALUES]
           ([LOOKUP_UNIQUE_ID]
           ,[LOOKUP_ID]
           ,[LOOKUP_VALUE_ID]
           ,[LOOKUP_VALUE_CODE]
           ,[LOOKUP_VALUE_DESC]
           ,[LOOKUP_SYS_DEF]
           ,[IS_ACTIVE]
           ,[LAST_UPDATED_DATETIME]
           ,[LOOKUP_FRAME_OR_MASONRY]
           ,[Type]
           ,[NSS_VALUE_CODE])
SELECT     114302
           ,1437
           ,1
           ,'INDIV'
           ,'Individual'
           ,[LOOKUP_SYS_DEF]
           ,[IS_ACTIVE]
           ,[LAST_UPDATED_DATETIME]
           ,[LOOKUP_FRAME_OR_MASONRY]
           ,[Type]
           ,[NSS_VALUE_CODE]
FROM [MNT_LOOKUP_VALUES]
WHERE LOOKUP_UNIQUE_ID = 11109

END


GO

IF NOT EXISTS(SELECT * FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID=114303)
BEGIN

INSERT INTO [dbo].[MNT_LOOKUP_VALUES]
           ([LOOKUP_UNIQUE_ID]
           ,[LOOKUP_ID]
           ,[LOOKUP_VALUE_ID]
           ,[LOOKUP_VALUE_CODE]
           ,[LOOKUP_VALUE_DESC]
           ,[LOOKUP_SYS_DEF]
           ,[IS_ACTIVE]
           ,[LAST_UPDATED_DATETIME]
           ,[LOOKUP_FRAME_OR_MASONRY]
           ,[Type]
           ,[NSS_VALUE_CODE])
SELECT     114303
           ,1437
           ,2
           ,'CORP'
           ,'Corporate'
           ,[LOOKUP_SYS_DEF]
           ,[IS_ACTIVE]
           ,[LAST_UPDATED_DATETIME]
           ,[LOOKUP_FRAME_OR_MASONRY]
           ,[Type]
           ,[NSS_VALUE_CODE]
FROM [MNT_LOOKUP_VALUES]
WHERE LOOKUP_UNIQUE_ID = 11109


END


GO


