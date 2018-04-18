CREATE PROCEDURE [dbo].[PopulateTempTable]
WITH EXECUTE AS CALLER
AS
BEGIN

    SET NOCOUNT ON;

    IF OBJECT_ID('TempDB..##VehicleTemp') IS NOT NULL
        DROP TABLE ##VehicleTemp;

    CREATE TABLE ##VehicleTemp
    (
           VehicleRegNo NVARCHAR(100),VehicleMakeCode NVARCHAR(100),
VehicleModelCode NVARCHAR(100),VehicleClassCode NVARCHAR(100),ModelDescription NVARCHAR(100),
BusCaptainCode NVARCHAR(100)
    );

    INSERT INTO ##VehicleTemp 
        (VehicleRegNo, VehicleMakeCode, VehicleModelCode,VehicleClassCode,ModelDescription,BusCaptainCode)
    VALUES
        ('45', '1234', '1234','1234','1234','1234'),
        ('46', '1234', '1234','1234','1234','1234'),
        ('47', '1234', '1234','1234','1234','1234'),
        ('48', '1234', '1234','1234','1234','1234'),
        ('49', '1234', '1234','1234','1234','1234'),
        ('50', '1234', '1234','1234','1234','1234')
        
        select UploadFileId, UploadFileName, UploadPath from MNT_VehicleListingUpload       
 where Status='INCOMPLETE' and IS_ACTIVE='Y' and Is_processed='N'
END


