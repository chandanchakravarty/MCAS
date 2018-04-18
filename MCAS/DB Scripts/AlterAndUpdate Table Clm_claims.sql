UPDATE CLM_Claims SET ClaimType=Case WHEN ClaimType='OD' THEN '1'
                                     WHEN ClaimType='PD' THEN '2'
                                     ELSE 3
                                     END

IF EXISTS (SELECT * FROM INFORMATION_SCHEMA.COLUMNS where table_name = 'clm_claims' and column_name = 'ClaimType') 

BEGIN 

ALTER TABLE clm_claims ALTER COLUMN [ClaimType] [int] NULL

END 


