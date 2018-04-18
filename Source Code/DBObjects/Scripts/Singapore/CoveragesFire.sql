GO

update MNT_COVERAGE set IS_ACTIVE = 'N' where COV_ID in (10049,10050,10011,10012,10046,10048,69,152,172,257)

update MNT_COVERAGE set IS_MANDATORY = 0 where COV_ID in (3,5,7,8,134,135,136,137,844,845,10,170,13,171,261,948,950)

update MNT_COVERAGE set COV_DES = 'Addl Residence- Rented To Others - 1 Family' where COV_ID = 948
update MNT_COVERAGE set COV_DES = 'Addl Residence - Rented To Others - 2 Family' where COV_ID = 950


update MNT_COVERAGE set COV_DES = 'Liability Extended to Addl Premises/Rented' where COV_ID = 260
update MNT_COVERAGE set COV_DES = 'Liability Extended Addl Premises/Rented' where COV_ID = 261

update MNT_COVERAGE set COV_DES = 'Other Liability - Owned/Operated By Employees' where COV_ID in (10,170)
update MNT_COVERAGE set COV_DES = 'Other Liability - Owned Farms Rented To Others' where COV_ID in (13,171)
update MNT_COVERAGE set COV_DES = 'Other Liability - Incidental Farming On Premises' where COV_ID = 261



GO

ALTER TABLE POL_OTHER_STRUCTURE_DWELLING ALTER COLUMN PREMISES_LOCATION VARCHAR(100)



