IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'CLM_TYPE_DETAIL' 
                        AND COLUMN_NAME ='LOSS_TYPE_CODE') 
BEGIN

ALTER TABLE CLM_TYPE_DETAIL
	ADD LOSS_TYPE_CODE nvarchar(40)
	
END

EXEC   sp_addextendedproperty 'Caption', 'LOSS TYPE CODE', 'user', dbo, 'table', 'CLM_TYPE_DETAIL', 'column',LOSS_TYPE_CODE                   
EXEC   sp_addextendedproperty 'Description', 'LOSS TYPE CODE', 'user', dbo, 'table', 'CLM_TYPE_DETAIL', 'column',LOSS_TYPE_CODE
--EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'CLM_ADJUSTER', 'column',SUB_ADJUSTER_GST
	
GO

IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'CLM_TYPE_DETAIL' 
                        AND COLUMN_NAME ='LOSS_DEPARTMENT')
BEGIN

ALTER TABLE CLM_TYPE_DETAIL
	ADD LOSS_DEPARTMENT nvarchar(20)
	
END

EXEC   sp_addextendedproperty 'Caption', 'LOSS DEPARTMENT', 'user', dbo, 'table', 'CLM_TYPE_DETAIL', 'column',LOSS_DEPARTMENT                   
EXEC   sp_addextendedproperty 'Description', 'LOSS DEPARTMENT', 'user', dbo, 'table', 'CLM_TYPE_DETAIL', 'column',LOSS_DEPARTMENT
--EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'CLM_ADJUSTER', 'column',SUB_ADJUSTER_GST
	
GO

IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'CLM_TYPE_DETAIL' 
                        AND COLUMN_NAME ='LOSS_EXTRA_COVER')
BEGIN

ALTER TABLE CLM_TYPE_DETAIL
	ADD LOSS_EXTRA_COVER nvarchar(20)
	
END

EXEC   sp_addextendedproperty 'Caption', 'LOSS EXTRA COVER', 'user', dbo, 'table', 'CLM_TYPE_DETAIL', 'column',LOSS_EXTRA_COVER                   
EXEC   sp_addextendedproperty 'Description', 'LOSS EXTRA COVER', 'user', dbo, 'table', 'CLM_TYPE_DETAIL', 'column',LOSS_EXTRA_COVER
--EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'CLM_ADJUSTER', 'column',SUB_ADJUSTER_GST