

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'Proc_Insert_MNT_Deductible')AND type in (N'P', N'PC'))
DROP PROCEDURE Proc_Insert_MNT_Deductible
GO

CREATE PROCEDURE Proc_Insert_MNT_Deductible
@p_OrgCategory nvarchar(50),
@p_OrgCategoryName nvarchar(100),
@p_DeductibleAmt decimal,
@p_EffectiveFrom datetime,
@p_EffectiveTo datetime,
@p_ModifiedDate datetime,
@p_CreatedDate datetime,
@p_CreatedBy nvarchar(50),
@p_ModifiedBy nvarchar(50)
AS
BEGIN
Insert into MNT_Deductible (OrgCategory,OrgCategoryName,DeductibleAmt,EffectiveFrom,EffectiveTo,ModifiedDate,CreatedDate,CreatedBy,ModifiedBy)
values(@p_OrgCategory,@p_OrgCategoryName,@p_DeductibleAmt,@p_EffectiveFrom,@p_EffectiveTo,@p_ModifiedDate,@p_CreatedDate,@p_CreatedBy,@p_ModifiedBy)
END
GO
