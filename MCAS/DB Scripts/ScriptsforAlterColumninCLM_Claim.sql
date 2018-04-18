
IF EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'CLM_Claims' AND [COLUMN_NAME] = 'ClaimAmount')
BEGIN
  ALTER TABLE CLM_Claims
    ALTER COLUMN ClaimAmount [decimal](18, 9)
END


IF EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'CLM_Claims' AND [COLUMN_NAME] = 'ExpensesAmt')
BEGIN
  ALTER TABLE CLM_Claims
    ALTER COLUMN ExpensesAmt [decimal](18, 9)
END


IF EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'CLM_Claims' AND [COLUMN_NAME] = 'ClaimAmtPayout')
BEGIN
  ALTER TABLE CLM_Claims
    ALTER COLUMN ClaimAmtPayout [decimal](18, 9)
END


IF EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'CLM_Claims' AND [COLUMN_NAME] = 'ExpensesAmount')
BEGIN
  ALTER TABLE CLM_Claims
    ALTER COLUMN ExpensesAmount [decimal](18, 9)
END


IF EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'CLM_Claims' AND [COLUMN_NAME] = 'ReserveAmount')
BEGIN
  ALTER TABLE CLM_Claims
    ALTER COLUMN ReserveAmount [decimal](18, 9)
END