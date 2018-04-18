IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_ClaimAccidentDetails_IsComplete]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[ClaimAccidentDetails] DROP CONSTRAINT [DF_ClaimAccidentDetails_IsComplete]
END

ALTER TABLE [dbo].[ClaimAccidentDetails] ADD  CONSTRAINT [DF_ClaimAccidentDetails_IsComplete]  DEFAULT ('1') FOR [IsComplete]
GO