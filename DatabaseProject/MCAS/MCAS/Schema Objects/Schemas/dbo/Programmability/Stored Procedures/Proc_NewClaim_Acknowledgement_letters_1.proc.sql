CREATE PROCEDURE [dbo].[Proc_NewClaim_Acknowledgement_letters]                
 @AccidentClaimId int=NULL,                  
 @ClaimId int= NULL,                  
 @PartyTypeId int =NULL                  
AS                  
Declare @tempTable                   
table(  Reference varchar,IPNo varchar,CompanyNameId varchar                    
 ,Address1 varchar                   
 ,Address2 varchar                   
 ,Address3  varchar                  
 ,CountryName varchar                   
 ,City   varchar                  
 ,[State]   varchar                  
 ,PostalCode varchar                     
 ,ContactPersonName varchar,VehicleNo varchar,ClaimantName varchar,                  
  AccidentDate varchar,AccidentTime varchar,VehicleRegnNo varchar                    
 ,UserFullName varchar,DeptName varchar,DID_No varchar,EmailId varchar,FAX_No varchar,                  
  MP varchar,Constituency varchar,SeverityReferenceNo varchar,Salutation  varchar,                  
  WritNo varchar,ClaimantContactNo varchar,ClaimantNRICPPNO varchar,AccidentLoc varchar              
  ,ClaimantAddress1 varchar,ClaimantAddress2 varchar,ClaimantAddress3 varchar,claimantPostalCode varchar ,            
  GroupCode varchar,ClaimantType varchar,ConfirmedAmount numeric(18,2),TotalPaymentDue numeric (18,2),ClaimantStatus varchar             
  )                    
                   
 if( ( IsNull(@AccidentClaimId,0)!=0)  and (IsNull(@ClaimId,0)!=0) and (IsNull(@PartyTypeId,0)!=0))                
                
                  
BEGIN                    
-- SET NOCOUNT ON added to prevent extra result sets from                      
 -- interfering with SELECT statements.                      
 SET NOCOUNT ON;                      
 Declare @tempPaymentSmry numeric(18,2)        
  Select @tempPaymentSmry = Sum(pmtSmry.TotalPaymentDue) from CLM_PaymentSummary pmtSmry          
 where pmtSmry.AccidentClaimId = @AccidentClaimId and pmtSmry.ClaimId = @ClaimId and pmtSmry.ApprovePayment='Y'         
                
    -- Insert statements for procedure here                      
 SELECT IsNull(NullIf(Rtrim(Ltrim(csv.Reference)),''),'Not Mapped')Reference,IsNull(NullIf(Rtrim(Ltrim(cad.IPNo)),''),'Not Mapped')IPNo,                      
 Case when PartyTypeId = 1 Then IsNull(NullIf(Rtrim(Ltrim(ced.CedantName)),''),'Not Mapped')                      
   When PartyTypeId = 2 Then IsNull(NullIf(Rtrim(Ltrim(adj.AdjusterName)),''),'Not Mapped')                      
   When PartyTypeId = 3 Then IsNull(NullIf(Rtrim(Ltrim(adj.AdjusterName)),''),'Not Mapped')                      
   When PartyTypeId = 4 Then IsNull(NullIf(Rtrim(Ltrim(dep.CompanyName)),''),'Not Mapped')                      
   Else IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantName)),''),'Not Mapped')                       
 End as CompanyNameId                    
 ,Case When PartyTypeId in (1,2,3,4) Then IsNull(NullIf(Rtrim(Ltrim(csv.Address1)),''),'Not Mapped') Else IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantAddress1)),''),'Not Mapped') End as Address1                    
 ,Case When PartyTypeId in (1,2,3,4) Then IsNull(NullIf(Rtrim(Ltrim(csv.Address2)),''),'Not Mapped') Else IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantAddress2)),''),'Not Mapped') End as Address2                    
 ,Case When PartyTypeId in (1,2,3,4) Then IsNull(NullIf(Rtrim(Ltrim(csv.Address3)),''),'Not Mapped') Else IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantAddress3)),''),'Not Mapped') End as Address3                    
 ,Case When PartyTypeId in (1,2,3,4) Then IsNull(NullIf(Rtrim(Ltrim(cnt.CountryName)),''),'Not Mapped') Else IsNull(NullIf(Rtrim(Ltrim(cnt1.CountryName)),''),'Not Mapped') End as CountryName                    
 ,Case When PartyTypeId in (1,2,3,4) Then IsNull(NullIf(Rtrim(Ltrim(csv.City)),''),'Not Mapped') Else IsNull(NullIf(Rtrim(Ltrim(clm.City)),''),'Not Mapped') End as City                     
 ,Case When PartyTypeId in (1,2,3,4) Then IsNull(NullIf(Rtrim(Ltrim(csv.[State])),''),'Not Mapped') Else IsNull(NullIf(Rtrim(Ltrim(clm.[State])),''),'Not Mapped') End as [State]                     
 ,Case When PartyTypeId in (1,2,3,4) Then IsNull(NullIf(Rtrim(Ltrim(csv.PostalCode)),''),'Not Mapped') Else IsNull(NullIf(Rtrim(Ltrim(clm.PostalCode)),''),'Not Mapped') End as PostalCode                      
 ,IsNull(NullIf(Rtrim(Ltrim(csv.ContactPersonName)),''),'Not Mapped')as ContactPersonName,IsNull(NullIf(Rtrim(Ltrim(cad.VehicleNo)),''),'Not Mapped')as VehicleNo,IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantName)),''),'Not Mapped')as ClaimantName,  
 IsNull(cad.AccidentDate,'Not Mapped')as AccidentDate,IsNull(NullIf(Rtrim(Ltrim(cad.AccidentTime)),''),'Not Mapped')as AccidentTime,IsNull(NullIf(Rtrim(Ltrim(clm.VehicleRegnNo)),''),'Not Mapped')as VehicleRegnNo                    
 ,IsNull(NullIf(Rtrim(Ltrim(usr.UserDispName)),'') ,'Not Mapped')as UserFullName,IsNull(NullIf(Rtrim(Ltrim(dpt.DeptName)),''),'Not Mapped')DeptName,  
 IsNull(NullIf(Rtrim(Ltrim(usr.DID_No)),''),'Not Mapped')as DID_No,IsNull(NullIf(Rtrim(Ltrim(usr.EmailId)),''),'Not Mapped')as EmailId,IsNull(NullIf(Rtrim(Ltrim(usr.FAX_No)),''),'Not Mapped')as FAX_No,                    
 IsNull(NullIf(Rtrim(Ltrim(ml.[Description])),''),'Not Mapped') as MP,IsNull(NullIf(Rtrim(Ltrim(ml1.[Description])),''),'Not Mapped') As Constituency,IsNull(NullIf(Rtrim(Ltrim(clm.SeverityReferenceNo)),''),'Not Mapped')SeverityReferenceNo, 'Not Mapped' as
  
 Salutation,IsNull(NullIf(Rtrim(Ltrim(clm.WritNo)),''),'Not Mapped')WritNo,IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantContactNo)),''),'Not Mapped')ClaimantContactNo,IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantNRICPPNO)),''),'Not Mapped')ClaimantNRICPPNO,          
 IsNull(NullIf(Rtrim(Ltrim(cad.AccidentLoc)),''),'Not Mapped')AccidentLoc,IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantAddress1)),''),'Not Mapped')ClaimantAddress1,IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantAddress2)),''),'Not Mapped')ClaimantAddress2,  
 IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantAddress3)),''),'Not Mapped')ClaimantAddress3,IsNull(NullIf(Rtrim(Ltrim(clm.PostalCode)),''),'Not Mapped') as claimantPostalCode,            
 IsNull(NullIf(Rtrim(Ltrim(grp.GroupName)),''),'Not Mapped') As GroupCode,IsNull(NullIf(Rtrim(Ltrim(ml2.[Description])),''),'Not Mapped') As ClaimantType,IsNull(NullIf(Rtrim(Ltrim(convert(varchar,clm.ConfirmedAmount))),''),'Not Mapped')ConfirmedAmount ,  
 IsNull(NullIf(Rtrim(Ltrim(Convert(varchar,@tempPaymentSmry))),''),'Not Mapped') as TotalPaymentDue,IsNull(NullIf(Rtrim(Ltrim(ml3.[Description])),''),'Not Mapped') As ClaimantStatus        
 from ClaimAccidentDetails cad                       
 left outer join CLM_Claims clm on cad.AccidentClaimId = clm.AccidentClaimId and clm.ClaimID = @ClaimId                      
 left outer join CLM_ServiceProvider csv on clm.AccidentClaimId = csv.AccidentId and clm.ClaimID = csv.ClaimantNameId and csv.ServiceProviderId = @PartyTypeId                      
 left outer join MNT_Lookups ml on clm.MP = ml.Lookupvalue and ml.category ='MP' and ml.IsActive='Y'               
 left outer join MNT_Lookups ml1 on  clm.Constituency = ml1.lookupValue and  ml1.category ='Constituency'  and ml1.IsActive='Y'              
 left outer join MNT_Lookups ml2 on  clm.ClaimantType = ml2.lookupValue and  ml2.category ='ClaimantType' and ml2.IsActive='Y'             
 left outer join MNT_Users usr on clm.ClaimsOfficer = usr.SNo                      
 left outer join MNT_Cedant ced on csv.CompanyNameId = ced.CedantId                       
 left outer join MNT_Adjusters adj on csv.CompanyNameId = adj.AdjusterId                      
 left outer join MNT_DepotMaster dep on csv.CompanyNameId = dep.DepotId                      
 left outer join MNT_Country cnt on csv.CountryId = cnt.CountryShortCode 
 left outer join MNT_Country cnt1 on cnt1.CountryShortCode = clm.Country                       
 left outer join MNT_Department dpt on usr.DeptCode = dpt.DeptCode              
 left outer join MNT_GroupsMaster grp on usr.GroupId = grp.GroupId             
 left outer join MNT_Lookups ml3 on  clm.ClaimantStatus = ml3.lookupValue and  ml3.category ='ClaimantStatus' and ml3.IsActive='Y'             
         
 where cad.AccidentClaimId = @AccidentClaimId --and clm.ClaimID = @ClaimId and csv.ServiceProviderId = @ServiceProviderId           
                 
END                  
              
ELSE                  
                  
 BEGIN     
Insert into  @tempTable (Reference,IPNo,CompanyNameId,Address1,Address2,Address3,CountryName,City,[State],PostalCode,                  
                  ContactPersonName,VehicleNo,ClaimantName,AccidentDate,AccidentTime,VehicleRegnNo,UserFullName,DeptName,DID_No,                  
                  EmailId,FAX_No,MP,Constituency,Salutation,SeverityReferenceNo,WritNo,ClaimantContactNo,ClaimantNRICPPNO,AccidentLoc              
                 ,ClaimantAddress1,ClaimantAddress2,ClaimantAddress3,claimantPostalCode,GroupCode,ClaimantType,ConfirmedAmount,TotalPaymentDue,ClaimantStatus)                  
                  values(NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)                  
     Select * from @tempTable                    
 END 