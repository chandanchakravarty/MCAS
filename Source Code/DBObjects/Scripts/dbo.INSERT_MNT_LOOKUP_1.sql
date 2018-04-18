GO

Declare @MAX_LOOKUP_ID smallint

select @MAX_LOOKUP_ID = MAX(LOOKUP_ID) + 1 from [MNT_LOOKUP_TABLES]

IF NOT EXISTS(SELECT * FROM MNT_LOOKUP_TABLES WHERE LOOKUP_ID=@MAX_LOOKUP_ID)
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
     VALUES
           (@MAX_LOOKUP_ID
           ,'PRODSG'
           ,'Product type for EBXSG'
           ,NULL
           ,NULL
           ,NULL
           ,NULL
           ,255
           ,NULL)
           
END



IF NOT EXISTS(SELECT * FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID=901)
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
     VALUES
           (901
           ,@MAX_LOOKUP_ID
           ,1
           ,'MOT'
           ,'Motor'
           ,'1'
           ,'Y'
           ,NULL
           ,NULL
           ,NULL
           ,NULL)
           
END


IF NOT EXISTS(SELECT * FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID=902)
BEGIN

INSERT INTO [EBX-ALBA-INITIAL-LOAD].[dbo].[MNT_LOOKUP_VALUES]
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
     VALUES
           (902
           ,@MAX_LOOKUP_ID
           ,2
           ,'FIR'
           ,'Fire'
           ,'2'
           ,'Y'
           ,NULL
           ,NULL
           ,NULL
           ,NULL)
           
END           


IF NOT EXISTS(SELECT * FROM MNT_LOOKUP_VALUES WHERE LOOKUP_UNIQUE_ID=903)
BEGIN
INSERT INTO [EBX-ALBA-INITIAL-LOAD].[dbo].[MNT_LOOKUP_VALUES]
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
     VALUES
           (903
           ,@MAX_LOOKUP_ID
           ,3
           ,'MCAR'
           ,'Marine Cargo'
           ,'3'
           ,'Y'
           ,NULL
           ,NULL
           ,NULL
           ,NULL)
           
END           
