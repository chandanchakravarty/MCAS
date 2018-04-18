IF EXISTS(SELECT TOP 1 1 FROM INFORMATION_SCHEMA.COLUMNS WHERE [TABLE_NAME] = 'clm_claim' AND [COLUMN_NAME] = 'ClaimStatus')
BEGIN
update clm_claim set [ClaimStatus]=1 where [ClaimStatus] is null
END