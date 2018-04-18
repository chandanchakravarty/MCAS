IF EXISTS (SELECT
    1
  FROM MNT_Lookups
  WHERE Category = 'ClaimType'
  AND Lookupdesc = 'Own Damage (OD) '
  AND Description = 'Own Damage (OD) ')
BEGIN
  UPDATE [MNT_Lookups]
  SET [Lookupdesc] = 'OD'
  WHERE Category = 'ClaimType'
  AND Lookupdesc = 'Own Damage (OD) '
  AND Description = 'Own Damage (OD) '
END
IF EXISTS (SELECT
    1
  FROM MNT_Lookups
  WHERE Category = 'ClaimType'
  AND Lookupdesc = 'Property Damage (TPPD) '
  AND Description = 'Property Damage (TPPD) ')
BEGIN
  UPDATE [MNT_Lookups]
  SET [Lookupdesc] = 'TPPD'
  WHERE Category = 'ClaimType'
  AND Lookupdesc = 'Property Damage (TPPD) '
  AND Description = 'Property Damage (TPPD) '
END
IF EXISTS (SELECT
    1
  FROM MNT_Lookups
  WHERE Category = 'ClaimType'
  AND Lookupdesc = 'Body Injury (TPBI)'
  AND Description = 'Body Injury (TPBI)')
BEGIN
  UPDATE [MNT_Lookups]
  SET [Lookupdesc] = 'TPBI'
  WHERE Category = 'ClaimType'
  AND Lookupdesc = 'Body Injury (TPBI)'
  AND Description = 'Body Injury (TPBI)'
END