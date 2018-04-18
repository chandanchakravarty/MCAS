IF NOT EXISTS (SELECT 1 FROM MNT_APP_RELEASE_MASTER WITH(NOLOCK) WHERE AppVersion = '1.0') 
BEGIN 
INSERT INTO [dbo].[MNT_APP_RELEASE_MASTER]
           ([AppVersion]
           ,[AssemblyVersion]
           ,[ReleaseNumber]
           ,[ReleaseName]
           ,[ReleaseDate]
           ,[ReleasedBy]
           ,[Remarks])
     VALUES
           ('1.0'
           ,'1.0'
           ,'1'
           ,'Beta Version'
           ,GETDATE()
           ,'Pravesh'
           ,'First Release')
END 

