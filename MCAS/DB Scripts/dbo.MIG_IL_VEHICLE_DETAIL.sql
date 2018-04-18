CREATE TABLE dbo.MIG_IL_VEHICLE_DETAIL
(

UPLOAD_FILE_ID INT,
UPLOAD_SERIAL_NO INT IDENTITY(1,1) NOT NULL,
VEHICLE_REGISTRATION_NO nvarchar(100) null,
VEHICLE_MAKE nvarchar(50) null,
VEHICLE_MODEL INT null,
 VEHICLE_CLASS nvarchar(50) null,
 MODEL_DESCRIPTION nvarchar(2000) null,
 BUS_CAPTAIN nvarchar(50) null,
 IMPORT_ACTION nvarchar(10)null,
 constraint [PK_MIG_IL_VEHICLE_DETAIL] primary key clustered
 (Upload_File_ID,Upload_SERIAL_NO)
);










