
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__POL_EMAIL__EMAIL__0C06BB60]') AND type = 'D')
BEGIN
ALTER TABLE POL_EMAIL_SPOOL DROP CONSTRAINT [DF__POL_EMAIL__EMAIL__0C06BB60]
END

GO


