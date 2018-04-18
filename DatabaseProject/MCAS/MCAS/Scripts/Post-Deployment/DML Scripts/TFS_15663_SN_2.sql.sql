WITH TempEmp (Lookupvalue,duplicateRecCount)
AS
(
SELECT DeptCode,ROW_NUMBER() OVER(PARTITION by DeptCode ORDER BY DeptCode) 
AS duplicateRecCount
FROM MNT_Department 
)
DELETE FROM TempEmp
WHERE duplicateRecCount > 1