IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_TODODIARYLIST_ClaimAccidentDetails]') AND parent_object_id = OBJECT_ID(N'[dbo].[TODODIARYLIST]'))
ALTER TABLE [dbo].[TODODIARYLIST] DROP CONSTRAINT [FK_TODODIARYLIST_ClaimAccidentDetails]
GO

ALTER TABLE [dbo].[TODODIARYLIST]  WITH CHECK ADD  CONSTRAINT [FK_TODODIARYLIST_ClaimAccidentDetails] FOREIGN KEY([AccidentId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId])
GO