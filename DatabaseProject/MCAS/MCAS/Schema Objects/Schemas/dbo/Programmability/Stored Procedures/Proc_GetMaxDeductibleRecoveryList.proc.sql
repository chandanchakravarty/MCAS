CREATE PROCEDURE [dbo].[Proc_GetMaxDeductibleRecoveryList] 
 @AccidentId [int],  
 @ClaimType [int]  
WITH EXECUTE AS CALLER  
AS  
BEGIN  
 SET NOCOUNT ON;  
DECLARE @deductAmout NUMERIC(18,2),@i INT,@PaymentAmt INT,@numrows INT    
DECLARE @totalpay NUMERIC(18,2) = 0.00    
DECLARE @tmptable TABLE (    
    idx SMALLINT PRIMARY KEY IDENTITY(1,1)    
    , PaymentId INT,    
    total_Amt NUMERIC(18,2)    
)    
DECLARE @finaltable TABLE (    
     PaymentId INT    
)    
  
select * into #mytemptable FROM (SELECT Distinct [Extent3].CountryOrgazinationCode as OrgCategoryName,[Extent1].Lookupvalue AS [OrgCategory],  
cad.AccidentDate       
FROM (SELECT [MNT_Lookups].[Lookupvalue] AS [Lookupvalue] FROM [dbo].[MNT_Lookups] AS [MNT_Lookups]) AS [Extent1]      
INNER JOIN [dbo].[MNT_UserOrgAccess] AS [Extent2] ON [Extent1].[Lookupvalue] = [Extent2].[OrgCode] INNER JOIN [dbo].[MNT_OrgCountry] AS [Extent3] ON [Extent2].[OrgName] = [Extent3].[CountryOrgazinationCode]    
INNER JOIN ClaimAccidentDetails cad on [Extent3].Id = cad.Organization  
where [Extent3].Id=(select Organization from ClaimAccidentDetails where AccidentClaimId=@AccidentId and cad.AccidentClaimId = @AccidentId))tbl  
  
SET @deductAmout =  (select Top 1 a.DeductibleAmt from MNT_Deductible a inner join #mytemptable b on a.OrgCategoryName=b.OrgCategoryName and a.OrgCategory =b.OrgCategory and b.AccidentDate between a.EffectiveFrom and  case when a.EffectiveTo Is null then '2099-12-31 00:00:00.000' else a.EffectiveTo end  order by a.DeductibleId desc)    
  
  
--(SELECT c.DeductibleAmt FROM ClaimAccidentDetails a     
--   JOIN MNT_OrgCountry b ON a.Organization = b.id     
--   JOIN MNT_Deductible c ON b.InsurerType = c.OrgCategory and b.CountryOrgazinationCode = c.OrgCategoryName    
--   WHERE AccidentClaimId = 251)    
       
INSERT @tmptable    
SELECT DISTINCT PaymentId,TotalPaymentDue FROM CLM_PaymentSummary WHERE AccidentClaimId = @AccidentId and ClaimType = @ClaimType    
    
SET @i = 1    
SET @numrows = (SELECT COUNT(*) FROM @tmptable)    
IF @numrows > 0    
 begin    
    WHILE (@i <= (SELECT MAX(idx) FROM @tmptable))    
    BEGIN    
    
        SET @PaymentAmt = (SELECT total_Amt FROM @tmptable WHERE idx = @i)    
  SET @totalpay =  @totalpay + @PaymentAmt     
        IF Isnull(@deductAmout,0) < @totalpay    
       insert into @finaltable SELECT PaymentId FROM @tmptable WHERE  idx = @i    
      
        SET @i = @i + 1    
    END    
  end    
      
  select * from @finaltable     
    
END


