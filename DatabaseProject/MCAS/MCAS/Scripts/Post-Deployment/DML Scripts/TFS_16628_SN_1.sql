Update cd 
SET cd.DriverEmployeeNo =bus.BusCaptainCode
FROM ClaimAccidentDetails cd
INNER JOIN MNT_BusCaptain bus
ON cd.DriverEmployeeNo=(convert(nvarchar(100),bus.TranId))


UPDATE ClaimAccidentDetails SET DriverEmployeeNo=NULL 
WHERE DriverEmployeeNo='0'
