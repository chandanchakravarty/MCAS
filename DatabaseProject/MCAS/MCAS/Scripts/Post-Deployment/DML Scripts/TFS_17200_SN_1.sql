IF NOT EXISTS (SELECT
    *
  FROM MNT_Lookups
  WHERE Category = 'TranComponent'
  AND Lookupvalue = 'IEF')
BEGIN
  INSERT INTO MNT_Lookups (Lookupvalue, Lookupdesc, Description, Category, IsActive, DisplayOrder)
    VALUES ('IEF', 'Insurance Excess Field', 'InsuranceExcessField', 'TranComponent', 'Y', 18)
END

UPDATE MNT_Lookups
SET DisplayOrder = 1
WHERE Lookupvalue = 'GD'
UPDATE MNT_Lookups
SET DisplayOrder = 2
WHERE Lookupvalue = 'ME'
UPDATE MNT_Lookups
SET DisplayOrder = 3
WHERE Lookupvalue = 'FME'
UPDATE MNT_Lookups
SET DisplayOrder = 4
WHERE Lookupvalue = 'LME'
UPDATE MNT_Lookups
SET DisplayOrder = 5
WHERE Lookupvalue = 'LEC'
UPDATE MNT_Lookups
SET DisplayOrder = 6
WHERE Lookupvalue = 'LOEAR'
UPDATE MNT_Lookups
SET DisplayOrder = 7
WHERE Lookupvalue = 'LODE'
UPDATE MNT_Lookups
SET DisplayOrder = 8
WHERE Lookupvalue = 'TRAN'
UPDATE MNT_Lookups
SET DisplayOrder = 9
WHERE Lookupvalue = 'COR'
UPDATE MNT_Lookups
SET DisplayOrder = 10
WHERE Lookupvalue = 'LOUUN'
UPDATE MNT_Lookups
SET DisplayOrder = 11
WHERE Lookupvalue = 'LOE'
UPDATE MNT_Lookups
SET DisplayOrder = 12
WHERE Lookupvalue = 'LOR'
UPDATE MNT_Lookups
SET DisplayOrder = 13
WHERE Lookupvalue = 'Ex'
UPDATE MNT_Lookups
SET DisplayOrder = 14
WHERE Lookupvalue = 'LOU'
UPDATE MNT_Lookups
SET DisplayOrder = 15
WHERE Lookupvalue = 'Labl'
UPDATE MNT_Lookups
SET DisplayOrder = 16
WHERE Lookupvalue = 'SubTotal'
UPDATE MNT_Lookups
SET DisplayOrder = 17
WHERE Lookupvalue = 'OE'
UPDATE MNT_Lookups
SET DisplayOrder = 19
WHERE Lookupvalue = 'MR'
UPDATE MNT_Lookups
SET DisplayOrder = 20
WHERE Lookupvalue = 'PTF'
UPDATE MNT_Lookups
SET DisplayOrder = 21
WHERE Lookupvalue = 'SF'
UPDATE MNT_Lookups
SET DisplayOrder = 22
WHERE Lookupvalue = 'RSF'
UPDATE MNT_Lookups
SET DisplayOrder = 23
WHERE Lookupvalue = 'LPRF'
UPDATE MNT_Lookups
SET DisplayOrder = 24
WHERE Lookupvalue = 'OPEF'
UPDATE MNT_Lookups
SET DisplayOrder = 25
WHERE Lookupvalue = 'PLC'
UPDATE MNT_Lookups
SET DisplayOrder = 26
WHERE Lookupvalue = 'PLD'
UPDATE MNT_Lookups
SET DisplayOrder = 27
WHERE Lookupvalue = 'OLC'
UPDATE MNT_Lookups
SET DisplayOrder = 28
WHERE Lookupvalue = 'OLD'
UPDATE MNT_Lookups
SET DisplayOrder = 29
WHERE Lookupvalue = 'TOTAL'