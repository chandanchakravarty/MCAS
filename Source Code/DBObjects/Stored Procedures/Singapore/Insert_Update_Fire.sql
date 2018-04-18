update MNT_LOB_MASTER set IS_ACTIVE = 'Y' where LOB_ID = 1
update MNT_LOOKUP_VALUES set IS_ACTIVE = 'N' where LOOKUP_UNIQUE_ID in (11148,11149,11194,11195,11196,11245,11246) and LOOKUP_ID = 1195

update MNT_LOOKUP_VALUES set LOOKUP_VALUE_DESC = 'Replacement' where LOOKUP_UNIQUE_ID = 11192 and LOOKUP_ID = 1195
update MNT_LOOKUP_VALUES set LOOKUP_VALUE_DESC = 'Repair' where LOOKUP_UNIQUE_ID = 11193 and LOOKUP_ID = 1195

update MNT_LOB_MASTER set LOB_DESC = 'Fire' where LOB_ID = 1
update MNT_LOB_MASTER set IS_ACTIVE = 'N' where LOB_ID = 39
update MNT_LOB_MASTER set LOB_PREFIX = 'F' where LOB_ID = 1


UPDATE MNT_MENU_LIST SET MENU_NAME = 'Fire' where MENU_ID in (51,232)
update MNT_LOOKUP_VALUES set IS_ACTIVE = 'N' where LOOKUP_UNIQUE_ID = 11814

update MNT_MENU_LIST set MENU_NAME = 'Property Details/Coverage' where MENU_ID = 61
update MNT_MENU_LIST set MENU_NAME = 'Property Details/Coverage' where MENU_ID = 239


update MNT_MENU_LIST set IS_ACTIVE = 'N' where MENU_ID in (63,146,234,235)

insert into MNT_LOOKUP_VALUES 
select (select MAX(LOOKUP_UNIQUE_ID) + 1 from MNT_LOOKUP_VALUES ) as LOOKUP_UNIQUE_ID,
LOOKUP_ID,
LOOKUP_VALUE_ID,
LOOKUP_VALUE_CODE,
LOOKUP_VALUE_DESC,
LOOKUP_SYS_DEF,
IS_ACTIVE,
LAST_UPDATED_DATETIME,
LOOKUP_FRAME_OR_MASONRY,
[Type],
NSS_VALUE_CODE from [BRICS-LOCAL-DEV].dbo.MNT_LOOKUP_VALUES where LOOKUP_UNIQUE_ID = 14452



 insert into MNT_COVERAGE (COV_ID,COV_REF_CODE,COV_CODE,COV_DES,STATE_ID,LOB_ID,IS_ACTIVE,IS_DEFAULT,TYPE,PURPOSE,LIMIT_TYPE,DEDUCTIBLE_TYPE,IsLimitApplicable,
IsDeductApplicable,IS_MANDATORY,INCLUDED,COVERAGE_TYPE,RANK,EFFECTIVE_FROM_DATE,EFFECTIVE_TO_DATE,DISABLED_DATE,ISADDDEDUCTIBLE_APP,ADDDEDUCTIBLE_TYPE,COMPONENT_CODE,
FORM_NUMBER,EDITION_DATE,NSS_VALUE_CODE,NSS_SUBLINE_CODE,NSS_LOSSTYPE_CODE,REINSURANCE_LOB,REINSURANCE_COV,ASLOB,REINSURANCE_CALC,REIN_REPORT_BUCK,REIN_REPORT_BUCK_COMM,
COMM_VEHICLE,COMM_REIN_COV_CAT,REIN_ASLOB,COMM_CALC,SHOW_ACT_PREMIUM,IS_SYSTEM_GENERAED)
 select COV_ID,COV_REF_CODE,COV_CODE,COV_DES,STATE_ID,LOB_ID,IS_ACTIVE,IS_DEFAULT,TYPE,PURPOSE,LIMIT_TYPE,DEDUCTIBLE_TYPE,IsLimitApplicable,
IsDeductApplicable,IS_MANDATORY,INCLUDED,COVERAGE_TYPE,RANK,EFFECTIVE_FROM_DATE,EFFECTIVE_TO_DATE,DISABLED_DATE,ISADDDEDUCTIBLE_APP,ADDDEDUCTIBLE_TYPE,COMPONENT_CODE,
FORM_NUMBER,EDITION_DATE,NSS_VALUE_CODE,NSS_SUBLINE_CODE,NSS_LOSSTYPE_CODE,REINSURANCE_LOB,REINSURANCE_COV,ASLOB,REINSURANCE_CALC,REIN_REPORT_BUCK,REIN_REPORT_BUCK_COMM,
COMM_VEHICLE,COMM_REIN_COV_CAT,REIN_ASLOB,COMM_CALC,SHOW_ACT_PREMIUM,IS_SYSTEM_GENERAED from [BRICS-LOCAL-DEV].dbo.MNT_COVERAGE where LOB_ID = 1


update MNT_COVERAGE set COV_DES = 'Coverage A - Property' where COV_ID = 3

update MNT_COVERAGE set IS_ACTIVE = 'N' where COV_ID in (903,916,1063,90,874,936,56,910,944,58,279,281,283,285,875,10028,876,925,88,93,927,932,921,86,80,10044,905,907,970,287,291,289,877,
878,879,880,881,882,883,934,271,269,273,885,919,912,89,886,112,94,887,10030,29,10058,79,888,889,275,37,53,55,196,986,996,890,832,92,95,33,277,891,984,982,929,990,994,892,893,894,939,908,988,895,930,62,
914,896,898,897,899,900,87,198,10056,10057,813,901,31,84,267,946,942,91,66,73,57,10034,195,10036,189,10038,191,10040,193,10042,74,992,884,993)