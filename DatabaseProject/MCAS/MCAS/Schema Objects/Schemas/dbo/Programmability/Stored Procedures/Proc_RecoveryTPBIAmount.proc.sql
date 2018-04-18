CREATE PROCEDURE [dbo].[Proc_RecoveryTPBIAmount]
	@AccidentId [nvarchar](100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    declare @count int
declare @payId int
declare @paymentamt numeric(18,2)
declare @dedutibleamt decimal(18,2)
declare @myPayment table(id int, PaymentId int)
declare @temp table(Amount decimal(18,2))
  
select * into #mytemptable FROM (SELECT Distinct [Extent3].CountryOrgazinationCode as OrgCategoryName,[Extent1].Lookupvalue AS [OrgCategory]    
FROM (SELECT [MNT_Lookups].[Lookupvalue] AS [Lookupvalue] FROM [dbo].[MNT_Lookups] AS [MNT_Lookups]) AS [Extent1]    
INNER JOIN [dbo].[MNT_UserOrgAccess] AS [Extent2] ON [Extent1].[Lookupvalue] = [Extent2].[OrgCode] INNER JOIN [dbo].[MNT_OrgCountry] AS [Extent3] ON [Extent2].[OrgName] = [Extent3].[CountryOrgazinationCode]  where [Extent3].Id=(select Organization from ClaimAccidentDetails where AccidentClaimId=@AccidentId))tbl  
    
if Exists(Select Top 1 a.DeductibleAmt from MNT_Deductible a inner join #mytemptable b on a.OrgCategoryName=b.OrgCategoryName and a.OrgCategory =b.OrgCategory and GETDATE() between a.EffectiveFrom and  a.EffectiveTo  order by a.DeductibleId desc)
begin
set @dedutibleamt = (Select Top 1 a.DeductibleAmt from MNT_Deductible a inner join #mytemptable b on a.OrgCategoryName=b.OrgCategoryName and a.OrgCategory =b.OrgCategory and GETDATE() between a.EffectiveFrom and  a.EffectiveTo  order by a.DeductibleId desc)  
end
else
begin
set @dedutibleamt = 0.00
end

set @count = (select COUNT(payoutamt.PaymentId) from CLM_PaymentSummary payoutamt where payoutamt.AccidentClaimId = @AccidentId and payoutamt.ClaimType = 3)

insert into @myPayment 
select ROW_NUMBER() over( order by PaymentId ) as id, payoutamt.PaymentId from CLM_PaymentSummary payoutamt where payoutamt.AccidentClaimId = 
@AccidentId and payoutamt.ClaimType = 3 and payoutamt.ApprovePayment = 'Y'

declare @id int =1
set @paymentamt=0
while @count > 0
Begin
  select top 1 @payId = PaymentId from @myPayment where id=@id order by PaymentId asc
 
  
  select  @paymentamt = @paymentamt + payoutamt.TotalPaymentDue from CLM_PaymentSummary payoutamt
  where payoutamt.AccidentClaimId = @AccidentId and payoutamt.ClaimType = 3 and PaymentId = @payId
  

	
insert into @temp
  select @paymentamt as Amount	
    if(@paymentamt > @dedutibleamt)
	break;
	
  set @count = @count-1
  set @id=@id+1
  --DROP TABLE #temp
end

select  top 1 * from @temp order by Amount desc
DROP TABLE #mytemptable

END