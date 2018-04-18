IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ReserveDetails' and column_name = 'InitialReserve')
BEGIN
alter table CLM_ReserveDetails alter column InitialReserve numeric (25,2)
END



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ReserveDetails' and column_name = 'MovementReserve')
BEGIN
alter table CLM_ReserveDetails alter column MovementReserve numeric (25,2)
END



IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ReserveDetails' and column_name = 'CurrentReserve')
BEGIN
alter table CLM_ReserveDetails alter column CurrentReserve numeric (25,2)
END


IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'CLM_ReserveDetails' and column_name = 'PreReserve')
BEGIN
alter table CLM_ReserveDetails alter column PreReserve numeric (25,2)
END