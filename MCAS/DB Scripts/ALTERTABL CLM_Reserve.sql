IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Reserve' and column_name = 'LOGDate') 

BEGIN 

ALTER TABLE CLM_Reserve ALTER COLUMN [LOGDate] [datetime] NULL

END 


IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Reserve' and column_name = 'LOGAmount') 

BEGIN 

ALTER TABLE CLM_Reserve ALTER COLUMN [LOGAmount] [decimal](18, 2) NULL

END 


IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_CLM_Reserve_LOGAmount]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[CLM_Reserve] DROP CONSTRAINT [DF_CLM_Reserve_LOGAmount]
END

ALTER TABLE [dbo].[CLM_Reserve] ADD  CONSTRAINT [DF_CLM_Reserve_LOGAmount]  DEFAULT ((0.00)) FOR [LOGAmount]
GO

