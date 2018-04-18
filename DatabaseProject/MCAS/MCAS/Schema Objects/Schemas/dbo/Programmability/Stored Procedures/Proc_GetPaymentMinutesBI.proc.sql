CREATE PROCEDURE [dbo].[Proc_GetPaymentMinutesBI]          
 @AccidentClaimId int=0,                 
 @ClaimId int=0,                
 @PaymentId int=0          
AS          
              
BEGIN                  
-- SET NOCOUNT ON ADDED TO PREVENT EXTRA RESULT SETS FROM                    
-- INTERFERING WITH SELECT STATEMENTS.                    
      
SET NOCOUNT ON;         
      
DECLARE @TEMPTABLE TABLE(            
 PaymentId INT            
,Payee NVARCHAR(50)            
,CPSTotalPaymentDue NUMERIC(18,2)            
,Address1 NVARCHAR(100)            
,Address2 NVARCHAR(100)            
,Address3 NVARCHAR(100)            
,PostalCodes NVARCHAR(20)            
,Reference NVARCHAR(50)       
,SeverityReferenceNo NVARCHAR(50)            
,AssignedToSP VARCHAR(50)            
,GroupNameSP VARCHAR(50)            
,AssignedToPD VARCHAR(50)            
,GroupNamePD VARCHAR(50)            
,InformSafetytoreviewfindings VARCHAR(20)          
,PayeeName VARCHAR(50)        
,GD NUMERIC(18,2)            
,ME NUMERIC(18,2)            
,FME NUMERIC(18,2)            
,LME NUMERIC(18,2)            
,LEC NUMERIC(18,2)            
,LOEAR NUMERIC(18,2)            
,LODE NUMERIC(18,2)            
,[TRAN] NUMERIC(18,2)            
,COR NUMERIC(18,2)            
,LOUUN NUMERIC(18,2)            
,LOE NUMERIC(18,2)            
,LOR NUMERIC(18,2)            
,EX NUMERIC(18,2)            
,LOU NUMERIC(18,2)            
,OE NUMERIC(18,2)            
,IEF NUMERIC(18,2)            
,MR NUMERIC(18,2)            
,PTF NUMERIC(18,2)            
,SF NUMERIC(18,2)            
,RSF NUMERIC(18,2)            
,LPRF NUMERIC(18,2)            
,OPEF NUMERIC(18,2)            
,PLC NUMERIC(18,2)            
,PLD NUMERIC(18,2)            
,OLC NUMERIC(18,2)            
,OLD NUMERIC(18,2)            
,TOTAL NUMERIC(18,2)            
)        
       
IF((ISNULL(@AccidentClaimId, 0) != 0) AND (ISNULL(@ClaimId, 0) != 0) AND (ISNULL(@PaymentId, 0) != 0))      
 BEGIN      
  DECLARE @cols AS NVARCHAR(MAX),            
  @query  AS NVARCHAR(MAX), @count NVARCHAR(MAX),@payId int   , @PayeeName nvarchar(Max)      
      
  Declare @PartyTypeId int      
  Set @PartyTypeId = (select PartyTypeId  from CLM_ServiceProvider csp       
  join CLM_PaymentSummary cps on cps.AccidentClaimId = csp.AccidentId and cps.ClaimID = csp.ClaimantNameId and SUBSTRING(Payee,3,LEN(Payee) - 1) = csp.ServiceProviderId   
  where AccidentId = @AccidentClaimId and claimantnameid = @ClaimId and PaymentId = @PaymentId)      
      
  Declare @PayeeId  int       
  set @PayeeId = (Select SUBSTRING(Payee,3,LEN(Payee) - 1) from CLM_PaymentSummary where AccidentClaimId = @AccidentClaimId and ClaimId = @ClaimId and PaymentId = @PaymentId)      
      
  if (@PayeeId = @ClaimId)      
   Begin      
    Set @PayeeName = (Select claimantname from CLM_Claims where AccidentClaimId = @AccidentClaimId and ClaimId = @ClaimId)      
   End      
  Else      
   Begin      
    Set @PayeeName = (select      
    Case when @PartyTypeId = 1 Then isnull(ced.CedantName,'')                
    When @PartyTypeId = 2 Then isnull(adj.AdjusterName,'')                
    When @PartyTypeId = 3 Then isnull(adj.AdjusterName,'')                
    When @PartyTypeId = 4 Then isnull(dep.CompanyName,'')                   
    End as CompanyNameId       
    From  CLM_PaymentSummary cps      
    join CLM_Claims clm on cps.AccidentClaimId = clm.AccidentClaimId and cps.ClaimID = clm.ClaimID      
    left outer join CLM_ServiceProvider csv on clm.AccidentClaimId = csv.AccidentId and clm.ClaimID = csv.ClaimantNameId and SUBSTRING(Payee,3,LEN(Payee) - 1) = csv.ServiceProviderId         
    left outer join MNT_Cedant ced on csv.CompanyNameId = ced.CedantId                 
    left outer join MNT_Adjusters adj on csv.CompanyNameId = adj.AdjusterId                
    left outer join MNT_DepotMaster dep on csv.CompanyNameId = dep.DepotId       
    where csv.AccidentId = @AccidentClaimId and claimantnameid = @ClaimId and cps.PaymentId = @PaymentId)       
   End         
      
   SELECT @cols = STUFF((SELECT ',' + QUOTENAME(LTRIM(RTRIM(Lookupvalue)))             
  FROM MNT_Lookups WHERE Category = 'TranComponent' and IsActive = 'Y' and Lookupvalue not in ('SubTotal','Labl','INVA','CELOR','CELOI','CELOU','CEME','OTH1S','LF','CESF','TPGIA','LTA','CR','CC','OTH2S') Order by DisplayOrder  FOR XML PATH(''), TYPE ).value('.', 'NVARCHAR(MAX)'),1,1,'')           
      
  SET @query = 'SELECT PaymentId,Payee,CPSTotalPaymentDue,Address,Address1,Address2,PostalCodes,Reference,SeverityReferenceNo,AssignedToSP,GroupNameSP,AssignedToPD,GroupNamePD            
     ,InformSafetytoreviewfindins,PayeeName, ' + @cols + '             
     FROM ( SELECT CPD.CmpCode,CPD.TotalPaymentDue,CPS.PaymentId,CPS.Payee,CPS.TotalPaymentDue AS CPSTotalPaymentDue,CPS.Address,CPS.Address1,CPS.Address2,CPS.PostalCodes,CSP.Reference,CLM.SeverityReferenceNo,USR1.UserDispName AS AssignedToSP,  
     GRP1.GroupName AS GroupNameSP,USR.UserDispName AS AssignedToPD,GRP.GroupName AS GroupNamePD,CPS.InformSafetytoreviewfindings,Null AS PayeeName            
     FROM CLM_PaymentDetails CPD            
     LEFT OUTER JOIN CLM_PaymentSummary CPS ON CPD.PaymentId = CPS.PaymentId           
     LEFT OUTER JOIN CLM_Claims CLM ON CPS.ClaimID = CLM.ClaimID and CPS.AccidentClaimId = CLM.AccidentClaimId        
     LEFT OUTER JOIN CLM_ServiceProvider CSP ON CPS.AccidentClaimId = CSP.AccidentId AND CPS.ClaimID = CSP.ClaimantNameId AND CSP.ServiceProviderId = SUBSTRING(CPS.Payee,3,(len(CPS.Payee) - 2))              
     LEFT OUTER JOIN MNT_Users USR ON CPS.AssignedTo = USR.SNo            
     LEFT OUTER JOIN MNT_GroupsMaster GRP ON USR.GroupId = GRP.GroupId            
     LEFT OUTER JOIN MNT_Users USR1 ON CPS.AssignedToSupervisor = USR1.SNo            
     LEFT OUTER JOIN MNT_GroupsMaster GRP1 ON USR1.GroupId = GRP1.GroupId            
     WHERE CPS.PaymentId =  '+ CAST(@PaymentId AS varchar) +'  AND CPS.AccidentClaimId = '+ CAST(@AccidentClaimId  AS varchar)+' AND CPS.ClaimID = '+ CAST(@ClaimId  AS varchar)+') X            
     pivot             
     (            
     MAX(TotalPaymentDue)            
     FOR CmpCode IN (InformSafetytoreviewfindins, ' + @cols + ')            
     ) p '            
      
      
  INSERT INTO @TEMPTABLE EXEC sp_executesql @query                      
      
  IF EXISTS( SELECT * FROM @TEMPTABLE )            
  BEGIN 
   IF NOT EXISTS(Select * From CLM_PaymentSummary  Where PaymentId = @PaymentId and ApprovePayment = 'Y')
    BEGIN
     UPDATE @TEMPTABLE Set AssignedToPD=NULL,GroupNamePD=NULL WHERE PaymentId=@PaymentId
    END          
   SELECT IsNull(NullIf(Rtrim(Ltrim(PaymentId)),''),'Not Mapped') AS PaymentId         
   ,IsNull(NullIf(Rtrim(Ltrim(Payee)),''),'Not Mapped') AS Payee      
   ,IsNull(NullIf(Rtrim(Ltrim(CPSTotalPaymentDue)),''),'Not Mapped') AS CPSTotalPaymentDue      
   ,IsNull(NullIf(Rtrim(Ltrim(Address1)),''),'Not Mapped') AS Address1      
   ,IsNull(NullIf(Rtrim(Ltrim(Address2)),''),'Not Mapped') AS  Address2      
   ,IsNull(NullIf(Rtrim(Ltrim(Address3)),''),'Not Mapped') AS Address3      
   ,IsNull(NullIf(Rtrim(Ltrim(PostalCodes)),''),'Not Mapped') AS  PostalCodes       
   ,IsNull(NullIf(Rtrim(Ltrim(Reference)),''),'Not Mapped') AS Reference      
   ,IsNull(NullIf(Rtrim(Ltrim(SeverityReferenceNo)),''),'Not Mapped') AS SeverityReferenceNo      
   ,IsNull(NullIf(Rtrim(Ltrim(AssignedToSP)),''),'Not Mapped') AS  AssignedToSP         
   ,IsNull(NullIf(Rtrim(Ltrim(GroupNameSP)),''),'Not Mapped') AS GroupNameSP      
   ,IsNull(NullIf(Rtrim(Ltrim(AssignedToPD)),''),'Not Mapped') AS  AssignedToPD        
   ,IsNull(NullIf(Rtrim(Ltrim(GroupNamePD)),''),'Not Mapped') AS GroupNamePD      
   ,IsNull(NullIf(Rtrim(Ltrim(InformSafetytoreviewfindings)),''),'Not Mapped') AS  InformSafetytoreviewfindings      
   ,IsNull(NullIf(Rtrim(Ltrim(@PayeeName)),''),'Not Mapped') AS PayeeName       
   ,GD       
   ,ME      
   ,FME       
   ,LME        
   ,LEC         
   ,LOEAR      
   ,LODE       
   ,[TRAN]          
   ,COR      
   ,LOUUN      
   ,LOE        
   ,LOR      
   ,EX      
   ,LOU        
   ,OE      
   ,IEF      
   ,MR      
   ,PTF        
   ,SF      
   ,RSF        
   ,LPRF       
   ,OPEF        
   ,PLC      
   ,PLD      
   ,OLC      
   ,OLD      
   ,TOTAL      
   FROM @TEMPTABLE       
  END      
 END      
ELSE      
 BEGIN      
      
  INSERT INTO @TEMPTABLE VALUES            
  (            
   NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,            
   NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,            
   NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL            
  )             
  SELECT * FROM @TEMPTABLE      
      
 END      
      
END


GO


