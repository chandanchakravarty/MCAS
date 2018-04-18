WITH TempEmp (Name,duplicateRecCount)
AS
(
SELECT CountryShortCode,ROW_NUMBER() OVER(PARTITION by CountryShortCode ORDER BY CountryShortCode) 
AS duplicateRecCount
FROM MNT_Country
)
DELETE FROM TempEmp
WHERE duplicateRecCount > 1

/********************************/

WITH TempEmp (Lookupvalue,duplicateRecCount)
AS
(
SELECT Lookupvalue,ROW_NUMBER() OVER(PARTITION by Lookupvalue ORDER BY Lookupvalue) 
AS duplicateRecCount
FROM MNT_Lookups where Category='ClaimantType'
)
DELETE FROM TempEmp
WHERE duplicateRecCount > 1