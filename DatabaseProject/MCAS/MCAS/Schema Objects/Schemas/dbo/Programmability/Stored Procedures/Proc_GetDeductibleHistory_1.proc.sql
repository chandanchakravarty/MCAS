--Updated for payment issue

CREATE PROCEDURE [dbo].[Proc_GetDeductibleHistory]
	@deductibleId nvarchar(20),   
	@TableName [nvarchar](20)    
AS  
SET FMTONLY OFF;  
BEGIN  
  
 select OrgCategory,OrgCategoryName into #temp from MNT_Deductible where DeductibleId = @deductibleId  
  
Select ROW_NUMBER() OVER(ORDER BY mt.TranAuditId) AS RowNo,md.EffectiveFrom, [NewData].value('(/MNT_Deductible//EffectiveTo/node())[1]', 'datetime') as EffectiveTo,[NewData].value('(/MNT_Deductible//DeductibleAmt/node())[1]', 'decimal(18,2)') as DeductibleAmt,md.CreatedBy,mt.CustomInfo as Remarks into #mytemptable  
 from MNT_Deductible md  
 inner join #temp b on md.OrgCategory = b.OrgCategory and md.OrgCategoryName = b.OrgCategoryName  
 inner join MNT_TransactionAuditLog mt on md.DeductibleId =  mt.EntityCode  
 where  mt.TableName = @TableName  
 order by mt.TranAuditId
   
 select EffectiveFrom,EffectiveTo,DeductibleAmt,CreatedBy,Remarks from #mytemptable order by RowNo desc
   
 drop table #temp  
 drop table #mytemptable  
END  