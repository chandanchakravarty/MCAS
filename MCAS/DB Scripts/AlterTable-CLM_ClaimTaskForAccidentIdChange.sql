
Truncate table CLM_ClaimTask

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_ClaimTask_ClaimAccidentDetails]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_ClaimTask]'))
ALTER TABLE [dbo].[CLM_ClaimTask] DROP CONSTRAINT [FK_CLM_ClaimTask_ClaimAccidentDetails]
GO

IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CLM_ClaimTask_CLM_Claims]') AND parent_object_id = OBJECT_ID(N'[dbo].[CLM_ClaimTask]'))
ALTER TABLE [dbo].[CLM_ClaimTask] DROP CONSTRAINT [FK_CLM_ClaimTask_CLM_Claims]
GO

ALTER TABLE [dbo].[CLM_ClaimTask]  WITH CHECK ADD  CONSTRAINT [FK_CLM_ClaimTask_ClaimAccidentDetails] FOREIGN KEY([AccidentClaimId])
REFERENCES [dbo].[ClaimAccidentDetails] ([AccidentClaimId])
GO

ALTER TABLE [dbo].[CLM_ClaimTask]  WITH CHECK ADD  CONSTRAINT [FK_CLM_ClaimTask_CLM_Claims] FOREIGN KEY([ClaimID])
REFERENCES [dbo].[CLM_Claims] ([ClaimID])
GO