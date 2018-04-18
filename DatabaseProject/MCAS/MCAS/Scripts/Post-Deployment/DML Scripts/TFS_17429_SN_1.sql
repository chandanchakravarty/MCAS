IF NOT EXISTS (SELECT
    *
  FROM MNT_Lookups
  WHERE Category = 'TabRedirect'
  AND Lookupvalue = '0')
BEGIN
  INSERT INTO MNT_Lookups (Lookupvalue, Lookupdesc, [Description], Category, IsActive,CreateBy,CreateDate,DisplayOrder)
    VALUES ('0', 'Go To', 'GoTo', 'TabRedirect', 'Y','Pravesh',GETDATE(),0)
END
IF NOT EXISTS (SELECT
    *
  FROM MNT_Lookups
  WHERE Category = 'TabRedirect'
  AND Lookupvalue = '208')
BEGIN
  INSERT INTO MNT_Lookups (Lookupvalue, Lookupdesc, Description, Category, IsActive,CreateBy,CreateDate,DisplayOrder)
    VALUES ('208', 'Claims Adjustments', 'CLM_REG', 'TabRedirect', 'Y','Pravesh',GETDATE(),1)
END
IF NOT EXISTS (SELECT
    *
  FROM MNT_Lookups
  WHERE Category = 'TabRedirect'
  AND Lookupvalue = '211')
BEGIN
  INSERT INTO MNT_Lookups (Lookupvalue, Lookupdesc, Description, Category, IsActive,CreateBy,CreateDate,DisplayOrder)
    VALUES ('211', 'Claims Payment Processing', 'CLM_PAY', 'TabRedirect', 'Y','Pravesh',GETDATE(),2)
END
IF NOT EXISTS (SELECT
    *
  FROM MNT_Lookups
  WHERE Category = 'TabRedirect'
  AND Lookupvalue = '215')
BEGIN
  INSERT INTO MNT_Lookups (Lookupvalue, Lookupdesc, Description, Category, IsActive,CreateBy,CreateDate,DisplayOrder)
    VALUES ('215', 'Claims Recovery Processing', 'CLM_REC', 'TabRedirect', 'Y','Pravesh',GETDATE(),3)
END