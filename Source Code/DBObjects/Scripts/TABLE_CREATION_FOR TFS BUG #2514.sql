IF NOT EXISTS(SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='POL_RISK_DETAIL')
BEGIN 
CREATE TABLE [dbo].POL_RISK_DETAIL(
	[LOB_ID] [int] NOT NULL,
	[SUSEP_LOB_CODE] NVARCHAR (100) NULL,
	[DISPLAY_COLUMN_NAME] NVARCHAR(200),
	[TABLE_NAME] NVARCHAR(200),
	[RISK_ID] NVARCHAR(400)
)
END


-----------FOR [LOB_ID]------

EXEC sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_RISK_DETAIL', 'column',LOB_ID

EXEC sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_RISK_DETAIL', 'column',LOB_ID

EXEC sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_RISK_DETAIL', 'column',LOB_ID

-----------FOR [SUSEP_LOB_CODE]------

EXEC sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_RISK_DETAIL', 'column',SUSEP_LOB_CODE

EXEC sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_RISK_DETAIL', 'column',SUSEP_LOB_CODE

EXEC sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_RISK_DETAIL', 'column',SUSEP_LOB_CODE

-----------FOR [DISPLAY_COLUMN_NAME]------

EXEC sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_RISK_DETAIL', 'column',DISPLAY_COLUMN_NAME

EXEC sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_RISK_DETAIL', 'column',DISPLAY_COLUMN_NAME

EXEC sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_RISK_DETAIL', 'column',DISPLAY_COLUMN_NAME

-----------FOR [TABLE_NAME]------

EXEC sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_RISK_DETAIL', 'column',TABLE_NAME

EXEC sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_RISK_DETAIL', 'column',TABLE_NAME

EXEC sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_RISK_DETAIL', 'column',TABLE_NAME

-----------FOR [RISK_ID]------

EXEC sp_addextendedproperty 'Caption', '', 'user', dbo, 'table', 'POL_RISK_DETAIL', 'column',RISK_ID

EXEC sp_addextendedproperty 'Description', '', 'user', dbo, 'table', 'POL_RISK_DETAIL', 'column',RISK_ID

EXEC sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'POL_RISK_DETAIL', 'column',RISK_ID