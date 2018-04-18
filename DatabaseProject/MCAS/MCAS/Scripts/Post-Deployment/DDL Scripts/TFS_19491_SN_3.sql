IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_Claims' and column_name = 'WsActRcvr') 
BEGIN
ALTER TABLE CLM_Claims
ALTER COLUMN WsActRcvr decimal(18,2)
END