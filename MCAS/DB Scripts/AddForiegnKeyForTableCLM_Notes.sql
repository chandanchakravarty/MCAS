IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_Notes_CLM_Notes]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_Notes]'))
ALTER TABLE [dbo].[CLM_Notes] DROP CONSTRAINT [FK_CLM_Notes_CLM_Notes]

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_Notes_ClaimAccidentDetails]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_Notes]'))
ALTER TABLE [dbo].[CLM_Notes] DROP CONSTRAINT [FK_CLM_Notes_ClaimAccidentDetails]

ALTER TABLE [dbo].[CLM_Notes]  WITH CHECK ADD  CONSTRAINT [FK_CLM_Notes_ClaimAccidentDetails] FOREIGN KEY([AccidentId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId])