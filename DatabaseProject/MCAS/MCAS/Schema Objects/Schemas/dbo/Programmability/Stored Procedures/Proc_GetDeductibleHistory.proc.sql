CREATE PROCEDURE [dbo].[Proc_GetDeductibleHistory] 
	-- Add the parameters for the stored procedure here
	@deductibleId nvarchar(20), 
	@TableName [nvarchar](20)  
AS
SET FMTONLY OFF;
BEGIN

	select OrgCategory,OrgCategoryName into #temp from MNT_Deductible where DeductibleId = @deductibleId
	
    -- Insert statements for procedure here
	Select md.EffectiveFrom,md.EffectiveTo,md.DeductibleAmt,md.CreatedBy,mt.CustomInfo as Remarks into #mytemptable
	from MNT_Deductible md
	inner join #temp b on md.OrgCategory = b.OrgCategory and md.OrgCategoryName = b.OrgCategoryName
	inner join MNT_TransactionAuditLog mt on md.DeductibleId = mt.EntityCode
	where mt.TableName = @TableName
	order by md.CreatedDate desc
	
	select * from #mytemptable
	
	drop table #temp
	drop table #mytemptable
END
