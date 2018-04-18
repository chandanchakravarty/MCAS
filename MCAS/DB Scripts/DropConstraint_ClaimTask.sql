  IF EXISTS (
               SELECT * FROM sys.foreign_keys  WHERE object_id = OBJECT_ID(N'FK_CLM_ClaimTask_CLM_Claims')
                 AND parent_object_id = OBJECT_ID(N'CLM_ClaimTask')
             )
        BEGIN
          ALTER TABLE CLM_ClaimTask DROP CONSTRAINT FK_CLM_ClaimTask_CLM_Claims
        END