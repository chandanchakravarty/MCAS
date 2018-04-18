IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'MNT_DepotMaster' and column_name = 'CompanyName') 
BEGIN 
ALTER TABLE MNT_DepotMaster 
ADD CompanyName NVARCHAR(500) 
END


update mnt_menus set IsActive='N' where MenuId=213