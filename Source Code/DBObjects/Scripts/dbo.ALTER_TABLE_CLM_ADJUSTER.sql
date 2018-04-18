IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'CLM_ADJUSTER' 
                        AND COLUMN_NAME ='SUB_ADJUSTER_GST') 
BEGIN 
    ALTER TABLE CLM_ADJUSTER
     ADD SUB_ADJUSTER_GST nvarchar(120)
END

EXEC   sp_addextendedproperty 'Caption', 'SUB ADJUSTER GST', 'user', dbo, 'table', 'CLM_ADJUSTER', 'column',SUB_ADJUSTER_GST                   
EXEC   sp_addextendedproperty 'Description', 'SUB ADJUSTER GST', 'user', dbo, 'table', 'CLM_ADJUSTER', 'column',SUB_ADJUSTER_GST
--EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'CLM_ADJUSTER', 'column',SUB_ADJUSTER_GST
                   
Go

IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'CLM_ADJUSTER' 
                        AND COLUMN_NAME ='SUB_ADJUSTER_GST_REG_NO') 
BEGIN 
    ALTER TABLE CLM_ADJUSTER
     ADD SUB_ADJUSTER_GST_REG_NO nvarchar(120)
END

EXEC   sp_addextendedproperty 'Caption', 'SUB ADJUSTER GST REG NO', 'user', dbo, 'table', 'CLM_ADJUSTER', 'column',SUB_ADJUSTER_GST
EXEC   sp_addextendedproperty 'Description', 'SUB ADJUSTER GST REG NO', 'user', dbo, 'table', 'CLM_ADJUSTER', 'column',SUB_ADJUSTER_GST_REG_NO
--EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'CLM_ADJUSTER', 'column',SUB_ADJUSTER_GST_REG_NO
                   
Go

IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'CLM_ADJUSTER' 
                        AND COLUMN_NAME ='SUB_ADJUSTER_MOBILE') 
BEGIN 
    ALTER TABLE CLM_ADJUSTER
     ADD SUB_ADJUSTER_MOBILE nvarchar(120)
END

EXEC   sp_addextendedproperty 'Caption', 'SUB_ADJUSTER_MOBILE', 'user', dbo, 'table', 'CLM_ADJUSTER', 'column',SUB_ADJUSTER_GST
EXEC   sp_addextendedproperty 'Description', 'SUB_ADJUSTER_MOBILE', 'user', dbo, 'table', 'CLM_ADJUSTER', 'column',SUB_ADJUSTER_MOBILE
--EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'CLM_ADJUSTER', 'column',SUB_ADJUSTER_MOBILE
                   
Go

IF NOT EXISTS(SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS 
                  WHERE TABLE_NAME = 'CLM_ADJUSTER' 
                        AND COLUMN_NAME ='SUB_ADJUSTER_CLASSIFICATION') 
BEGIN 
    ALTER TABLE CLM_ADJUSTER
     ADD SUB_ADJUSTER_CLASSIFICATION nvarchar(120)
END

EXEC   sp_addextendedproperty 'Caption', 'SUB_ADJUSTER_CLASSIFICATION', 'user', dbo, 'table', 'CLM_ADJUSTER', 'column',SUB_ADJUSTER_GST
EXEC   sp_addextendedproperty 'Description', 'SUB_ADJUSTER_CLASSIFICATION', 'user', dbo, 'table', 'CLM_ADJUSTER', 'column',SUB_ADJUSTER_CLASSIFICATION
--EXEC   sp_addextendedproperty 'Domain', '', 'user', dbo, 'table', 'CLM_ADJUSTER', 'column',SUB_ADJUSTER_CLASSIFICATION
                   
Go