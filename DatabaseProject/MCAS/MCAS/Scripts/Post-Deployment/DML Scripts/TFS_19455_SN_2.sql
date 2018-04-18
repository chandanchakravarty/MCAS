-- =============================================
-- Script Template
-- =============================================
IF NOT EXISTS (SELECT 1 FROM MNT_LookupsMaster WITH(NOLOCK) WHERE LookupCategoryCode = 'DistrictCode' and LookupCategoryDesc = 'District Code') 
BEGIN 
INSERT INTO [dbo].[MNT_LookupsMaster]
           ([LookupMasterDesc]
           ,[LookupCategoryCode]
           ,[LookupCategoryDesc]
           ,[IsActive]
           ,[IsCommonMaster]
           ,[CreatedBy]
           ,[CreatedDate]
           ,[ModifiedBy]
           ,[ModifiedDate])
     VALUES
           (NULL
           ,'DistrictCode'
           ,'District Code'
           ,'Y'
           ,'Y'
           ,NULL
           ,NULL
           ,NULL
           ,NULL)
END 