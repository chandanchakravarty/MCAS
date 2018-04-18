CREATE PROCEDURE [dbo].[Proc_Insert_MNT_Deductible]
	@p_OrgCategory [nvarchar](50),
	@p_OrgCategoryName [nvarchar](100),
	@p_DeductibleAmt [decimal](18, 0),
	@p_EffectiveFrom [datetime],
	@p_EffectiveTo [datetime],
	@p_ModifiedDate [datetime],
	@p_CreatedDate [datetime],
	@p_CreatedBy [nvarchar](50),
	@p_ModifiedBy [nvarchar](50)
WITH EXECUTE AS CALLER
AS
BEGIN
Insert into MNT_Deductible (OrgCategory,OrgCategoryName,DeductibleAmt,EffectiveFrom,EffectiveTo,ModifiedDate,CreatedDate,CreatedBy,ModifiedBy)
values(@p_OrgCategory,@p_OrgCategoryName,@p_DeductibleAmt,@p_EffectiveFrom,Case when @p_EffectiveTo = '' then null else @p_EffectiveTo end,@p_ModifiedDate,@p_CreatedDate,@p_CreatedBy,@p_ModifiedBy)
END


