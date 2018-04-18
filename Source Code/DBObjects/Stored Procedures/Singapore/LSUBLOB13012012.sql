DECLARE @MAX_SUB_LOB_ID INT
SELECT @MAX_SUB_LOB_ID = MAX(SUB_LOB_ID) + 1 from MNT_SUB_LOB_MASTER

INSERT INTO [dbo].[MNT_SUB_LOB_MASTER]
           ([LOB_ID]
           ,[SUB_LOB_ID]
           ,[SUB_LOB_DESC]
           ,[SUB_LOB_CODE]
           )
           
   VALUES
           (38
           ,@MAX_SUB_LOB_ID
           ,'Motor Standard'
           ,'MOTST'
          
          )
GO





DECLARE @MAX_SUB_LOB_ID INT
SELECT @MAX_SUB_LOB_ID = MAX(SUB_LOB_ID) + 1 from MNT_SUB_LOB_MASTER 
INSERT INTO [dbo].[MNT_SUB_LOB_MASTER]
           ([LOB_ID]
           ,[SUB_LOB_ID]
           ,[SUB_LOB_DESC]
           ,[SUB_LOB_CODE]
			)
   VALUES
           (38
           ,@MAX_SUB_LOB_ID
           ,'Motor Premium'
           ,'MOTPM'
			)
GO


DECLARE @MAX_SUB_LOB_ID INT
SELECT @MAX_SUB_LOB_ID = MAX(SUB_LOB_ID) + 1 from MNT_SUB_LOB_MASTER 
INSERT INTO [dbo].[MNT_SUB_LOB_MASTER]
           ([LOB_ID]
           ,[SUB_LOB_ID]
           ,[SUB_LOB_DESC]
           ,[SUB_LOB_CODE]
           )
   VALUES
           (38
           ,@MAX_SUB_LOB_ID
           ,'Motor Premium Plus'
           ,'MOTPL'
        )
GO
