if not exists(select 1 from MNT_LookupsMaster where  LookupCategoryCode='SettlementType')
begin
insert into MNT_LookupsMaster (LookupMasterDesc,	LookupCategoryCode,	LookupCategoryDesc,	IsActive,	IsCommonMaster,	CreatedBy,	CreatedDate,	ModifiedBy,	ModifiedDate) values (	NULL,	'SettlementType',	'Settlement Type',	'Y',	'Y',	NULL,	NULL,	NULL,	NULL)
end
if not exists(select 1 from MNT_LookupsMaster where  LookupCategoryCode='Lawyer/GIADRM')
begin
insert into MNT_LookupsMaster (LookupMasterDesc,	LookupCategoryCode,	LookupCategoryDesc,	IsActive,	IsCommonMaster,	CreatedBy,	CreatedDate,	ModifiedBy,	ModifiedDate) values (	NULL,	'Lawyer/GIADRM',	'Lawyer / GIA DRM',	'Y',	'Y',	NULL,	NULL,	NULL,	NULL)
end
if not exists(select 1 from MNT_LookupsMaster where  LookupCategoryCode='DriverType')
begin
insert into MNT_LookupsMaster (LookupMasterDesc,	LookupCategoryCode,	LookupCategoryDesc,	IsActive,	IsCommonMaster,	CreatedBy,	CreatedDate,	ModifiedBy,	ModifiedDate) values (	NULL,	'DriverType',	'Driver Type',	'Y',	'Y',	NULL,	NULL,	NULL,	NULL)
end
if not exists(select 1 from MNT_LookupsMaster where  LookupCategoryCode='CaseTypeL1')
begin
insert into MNT_LookupsMaster (LookupMasterDesc,	LookupCategoryCode,	LookupCategoryDesc,	IsActive,	IsCommonMaster,	CreatedBy,	CreatedDate,	ModifiedBy,	ModifiedDate) values (	NULL,	'CaseTypeL1',	'Case Type L1',	'Y',	'Y',	NULL,	NULL,	NULL,	NULL)
end
if not exists(select 1 from MNT_LookupsMaster where  LookupCategoryCode='CaseTypeL2')
begin
insert into MNT_LookupsMaster (LookupMasterDesc,	LookupCategoryCode,	LookupCategoryDesc,	IsActive,	IsCommonMaster,	CreatedBy,	CreatedDate,	ModifiedBy,	ModifiedDate) values (	NULL,	'CaseTypeL2',	'Case Type L2',	'Y',	'Y',	NULL,	NULL,	NULL,	NULL)
end
if not exists(select 1 from MNT_LookupsMaster where  LookupCategoryCode='ShareAllocation')
begin
insert into MNT_LookupsMaster (LookupMasterDesc,	LookupCategoryCode,	LookupCategoryDesc,	IsActive,	IsCommonMaster,	CreatedBy,	CreatedDate,	ModifiedBy,	ModifiedDate) values (	NULL,	'ShareAllocation',	'Share Allocation',	'Y',	'Y',	NULL,	NULL,	NULL,	NULL)
end