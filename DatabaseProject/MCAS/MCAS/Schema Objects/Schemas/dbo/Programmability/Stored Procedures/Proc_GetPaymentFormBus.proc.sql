CREATE PROCEDURE [dbo].[Proc_GetPaymentFormBus]        
 @AccidentClaimId int=null,            
 @ClaimId int= null,            
 @PartyTypeId int =null,
 @PaymentId int =null,    
 @loggedInId int             
AS      
Declare @tempTable             
table(IPNo varchar        
 ,CompanyNameId varchar              
 ,Address1 varchar             
 ,Address2 varchar             
 ,Address3  varchar            
 ,CountryName varchar             
 ,City   varchar            
 ,[State]   varchar            
 ,PostalCode varchar               
 ,VehicleNo varchar        
 ,ClaimantName varchar            
 ,VehicleRegnNo varchar            
 ,AccidentDate varchar        
 ,AccidentTime varchar          
 ,TotalPaymentDue varchar--numeric(18,2)        
 ,UserFullName varchar        
 ,DepartmentName varchar        
 ,ApprovingPaymentUserName varchar        
 ,ApprovingPaymentDptName varchar        
 ,GeneratedFormUserName varchar        
 ,GeneratedFormDptName varchar        
  )              
             
 if( ( isnull(@AccidentClaimId,0)!=0)  and (isnull(@ClaimId,0)!=0) and (isnull(@PartyTypeId,0)!=0))          
          
             
BEGIN              
-- SET NOCOUNT ON added to prevent extra result sets from                
 -- interfering with SELECT statements.                
 SET NOCOUNT ON;                
                
    -- Insert statements for procedure here                
 SELECT cad.IPNo,                
 Case when PartyTypeId = 1 Then IsNull(NullIf(Rtrim(Ltrim(ced.CedantName)),''),'Not Mapped')              
   When PartyTypeId = 2 Then IsNull(NullIf(Rtrim(Ltrim(adj.AdjusterName)),''),'Not Mapped')                
   When PartyTypeId = 3 Then IsNull(NullIf(Rtrim(Ltrim(adj.AdjusterName)),''),'Not Mapped')                
   When PartyTypeId = 4 Then IsNull(NullIf(Rtrim(Ltrim(dep.CompanyName)),''),'Not Mapped')                
   Else IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantName)),''),'Not Mapped')               
 End as CompanyNameId              
 ,Case When PartyTypeId in (1,2,3,4) Then IsNull(NullIf(Rtrim(Ltrim(csv.Address1)),''),'Not Mapped') Else IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantAddress1)),''),'Not Mapped') End as Address1              
 ,Case When PartyTypeId in (1,2,3,4) Then IsNull(NullIf(Rtrim(Ltrim(csv.Address2)),''),'Not Mapped') Else IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantAddress2)),''),'Not Mapped') End as Address2              
 ,Case When PartyTypeId in (1,2,3,4) Then IsNull(NullIf(Rtrim(Ltrim(csv.Address3)),''),'Not Mapped') Else IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantAddress3)),''),'Not Mapped') End as Address3              
 ,Case When PartyTypeId in (1,2,3,4) Then IsNull(NullIf(Rtrim(Ltrim(cnt.CountryName)),''),'Not Mapped') Else IsNull(NullIf(Rtrim(Ltrim(cnt1.CountryName)),''),'Not Mapped') End  as CountryName              
 ,Case When PartyTypeId in (1,2,3,4) Then IsNull(NullIf(Rtrim(Ltrim(csv.City)),''),'Not Mapped') Else IsNull(NullIf(Rtrim(Ltrim(clm.City)),''),'Not Mapped') End as City               
 ,Case When PartyTypeId in (1,2,3,4) Then IsNull(NullIf(Rtrim(Ltrim(csv.[State])),''),'Not Mapped') Else IsNull(NullIf(Rtrim(Ltrim(clm.[State])),''),'Not Mapped') End as [State]               
 ,Case When PartyTypeId in (1,2,3,4) Then IsNull(NullIf(Rtrim(Ltrim(csv.PostalCode)),''),'Not Mapped') Else IsNull(NullIf(Rtrim(Ltrim(clm.PostalCode)),''),'Not Mapped') End as PostalCode                
 ,IsNull(NullIf(Rtrim(Ltrim(cad.VehicleNo)),''),'Not Mapped') AS VehicleNo    
 ,IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantName)),''),'Not Mapped') AS ClaimantName    
 ,IsNull(NullIf(Rtrim(Ltrim(clm.VehicleRegnNo)),''),'Not Mapped') AS VehicleRegnNo    
 ,IsNull(cad.AccidentDate,'Not Mapped') AS AccidentDate    
 ,IsNull(NullIf(Rtrim(Ltrim(cad.AccidentTime)),''),'Not Mapped') AS AccidentTime    
 ,cps.TotalPaymentDue     
 ,IsNull(NullIf(Rtrim(Ltrim(usr.UserDispName)),''),'Not Mapped') as UserFullName    
 ,IsNull(NullIf(Rtrim(Ltrim(dpt.DeptName)),''),'Not Mapped') as DepartmentName     
 ,IsNull(NullIf(Rtrim(Ltrim(usr1.UserDispName)),''),'Not Mapped') as ApprovingPaymentUserName    
 ,IsNull(NullIf(Rtrim(Ltrim(dpt1.DeptName)),''),'Not Mapped') as ApprovingPaymentDptName    
 ,IsNull(NullIf(Rtrim(Ltrim(usr2.UserDispName)),''),'Not Mapped') as GeneratedFormUserName    
 ,IsNull(NullIf(Rtrim(Ltrim(dpt2.DeptName)),''),'Not Mapped') as GeneratedFormDptName          
 from ClaimAccidentDetails cad                 
 left outer join CLM_Claims clm on cad.AccidentClaimId = clm.AccidentClaimId and clm.ClaimID = @ClaimId                
 left outer join CLM_ServiceProvider csv on clm.AccidentClaimId = csv.AccidentId and clm.ClaimID = csv.ClaimantNameId and csv.ServiceProviderId = @PartyTypeId                
 left outer join MNT_Lookups ml on clm.MP = ml.Lookupvalue              
 left outer join MNT_Users usr on clm.ClaimsOfficer = usr.SNo                
 left outer join MNT_Cedant ced on csv.CompanyNameId = ced.CedantId                 
 left outer join MNT_Adjusters adj on csv.CompanyNameId = adj.AdjusterId                
 left outer join MNT_DepotMaster dep on csv.CompanyNameId = dep.DepotId                
 left outer join MNT_Country cnt on csv.CountryId = cnt.CountryShortCode   
 left outer join MNT_Country cnt1 on cnt1.CountryShortCode = clm.Country               
 left outer join MNT_Department dpt on usr.DeptCode = dpt.DeptCode        
 left outer join CLM_PaymentSummary cps on cps.AccidentClaimId = cad.AccidentClaimId and cps.ClaimID = clm.ClaimID  and cps.PaymentId=@PaymentId and cps.ApprovePayment = 'Y'           
 left outer join MNT_Users usr1 on cps.AssignedTo = usr1.SNo              
 left outer join MNT_Department dpt1 on usr1.DeptCode = dpt1.DeptCode           
 left outer join MNT_Users usr2 on usr2.SNo = @loggedInId        
 left outer join MNT_Department dpt2 on usr2.DeptCode = dpt2.DeptCode              
 where cad.AccidentClaimId = @AccidentClaimId     
END            
            
ELSE            
            
 BEGIN            
     Insert into  @tempTable (IPNo,CompanyNameId,Address1,Address2,Address3,CountryName,City,[State],PostalCode,            
                  VehicleNo,ClaimantName,VehicleRegnNo,AccidentDate,AccidentTime,TotalPaymentDue,UserFullName,DepartmentName,          ApprovingPaymentUserName,ApprovingPaymentDptName,GeneratedFormUserName,GeneratedFormDptName)            
                  values(NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)            
     Select * from @tempTable              
 END    