IF NOT EXISTS (SELECT 1 FROM MNT_LookupsMaster WITH(NOLOCK) WHERE LookupCategoryCode='CollisionType') 
BEGIN 
INSERT INTO [dbo].[MNT_LookupsMaster] ([LookupCategoryCode],[LookupCategoryDesc],[IsActive],[IsCommonMaster])
     VALUES ('CollisionType','Collision Type','Y','Y')
END 