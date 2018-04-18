Create Procedure Proc_GetReserveReportDetails    
 @AccidentClaimId int=NULL,                  
 @ClaimId int= NULL,                  
 @PartyTypeId int =NULL,    
 @ReserveId int = Null    
As          
        
Declare @tempTable                         
table(  Reference varchar,IPNo varchar,CompanyNameId varchar                          
 ,Address1 varchar                         
 ,Address2 varchar                         
 ,Address3  varchar                        
 ,CountryName varchar                         
 ,City   varchar                        
 ,[State]   varchar                        
 ,PostalCode varchar                           
 ,VehicleNo varchar              
 ,VehicleRegnNo varchar              
 ,ClaimantName varchar              
 ,AccidentDate varchar,AccidentTime varchar               
 ,COR varchar              
 ,LOU varchar              
 ,LPRF varchar              
 ,SF varchar              
 ,OE varchar              
 ,TOTAL numeric(18,2)              
 ,CurrentNoofdays varchar(50)                                                                     
 ,CurrentRateperday varchar(50)                    
 ,UserFullName varchar,DeptName varchar,DID_No varchar,EmailId varchar,FAX_No varchar,AccidentLoc varchar                  
  )               
              
 if( ( isnull(@AccidentClaimId,0)!=0)  and (isnull(@ClaimId,0)!=0) and (isnull(@PartyTypeId,0)!=0) and (isnull(@ReserveId,0)!=0))                      
                      
                        
BEGIN               
SET NOCOUNT ON;                
Declare @CurrentNoofDays varchar(max),@CurrentRateperday varchar(max)              
Select @CurrentNoofDays = InitialNoofdays from CLM_ReserveDetails where AccidentClaimId = @AccidentClaimId and ClaimID = @ClaimId and ReserveId = @ReserveId and CmpCode = 'LOU'              
Select @CurrentRateperday = MLM.LouRate from CLM_ReserveDetails CRD    
       left outer join MNT_LOU_MASTER MLM on crd.InitialRateperday = MLM.Id    
       where AccidentClaimId = @AccidentClaimId and crd.ClaimID = @ClaimId and ReserveId = @ReserveId and CmpCode = 'LOU'              
              
              
SELECT Reference,IsNull(NullIf(Rtrim(Ltrim(IPNo)),''),'Not Mapped')IPNo,IsNull(NullIf(Rtrim(Ltrim(CompanyNameId)),''),'Not Mapped')CompanyNameId,  
       IsNull(NullIf(Rtrim(Ltrim(Address1)),''),'Not Mapped')Address1,IsNull(NullIf(Rtrim(Ltrim(Address2)),''),'Not Mapped')Address2,
       IsNull(NullIf(Rtrim(Ltrim(Address3)),''),'Not Mapped')Address3,IsNull(NullIf(Rtrim(Ltrim(CountryName)),''),'Not Mapped')CountryName,
       IsNull(NullIf(Rtrim(Ltrim(City)),''),'Not Mapped')City,IsNull(NullIf(Rtrim(Ltrim([State])),''),'Not Mapped')[State],
       IsNull(NullIf(Rtrim(Ltrim(PostalCode)),''),'Not Mapped')PostalCode,   
       IsNull(NullIf(Rtrim(Ltrim(VehicleNo)),''),'Not Mapped')VehicleNo,IsNull(NullIf(Rtrim(Ltrim(VehicleRegnNo)),''),'Not Mapped')VehicleRegnNo,
	   IsNull(NullIf(Rtrim(Ltrim(ClaimantName)),''),'Not Mapped')ClaimantName,      
       IsNull(AccidentDate,'Not Mapped')AccidentDate,IsNull(AccidentTime,'Not Mapped')AccidentTime ,
	   IsNull(NullIf(Rtrim(Ltrim(UserFullName)),''),'Not Mapped')UserFullName,IsNull(NullIf(Rtrim(Ltrim(DeptName)),''),'Not Mapped')DeptName,        
       IsNull(NullIf(Rtrim(Ltrim(DID_No)),''),'Not Mapped')DID_No,IsNull(NullIf(Rtrim(Ltrim(EmailId)),''),'Not Mapped')EmailId,
	   IsNull(NullIf(Rtrim(Ltrim(FAX_No)),''),'Not Mapped')FAX_No,IsNull(NullIf(Rtrim(Ltrim([COR])),''),'0')As [COR],      
       IsNull(NullIf(Rtrim(Ltrim([LOU])),''),'0')As [LOU],IsNull(NullIf(Rtrim(Ltrim([LPRF])),''),'0')As [LPRF],IsNull(NullIf(Rtrim(Ltrim([SF])),''),'0')As [SF],
	   IsNull(NullIf(Rtrim(Ltrim([OE])),''),'0')As [OE],  
       IsNull(NullIf(Rtrim(Ltrim([TOTAL])),''),'Not Mapped')[TOTAL],IsNull(NullIf(Rtrim(Ltrim(@CurrentNoofDays)),''),'0') as CurrentNoofdays ,        
       IsNull(NullIf(Rtrim(Ltrim(AccidentLoc)),''),'Not Mapped')AccidentLoc,IsNull(NullIf(Rtrim(Ltrim(@CurrentRateperday)),''),'0') As CurrentRateperday            
FROM               
(  
  SELECT isnull(csv.Reference,'Not Mapped') as Reference,cad.IPNo,                            
 Case when PartyTypeId = 1 Then isnull(ced.CedantName,'')                            
   When PartyTypeId = 2 Then isnull(adj.AdjusterName,'')                            
   When PartyTypeId = 3 Then isnull(adj.AdjusterName,'')                            
   When PartyTypeId = 4 Then isnull(dep.CompanyName,'')                            
   Else isnull(clm.ClaimantName,'')                             
 End as CompanyNameId              
 ,Case When PartyTypeId in (1,2,3,4) Then isnull(csv.Address1,'') Else isnull(clm.ClaimantAddress1,'') End as Address1                          
 ,Case When PartyTypeId in (1,2,3,4) Then isnull(csv.Address2,'') Else isnull(clm.ClaimantAddress2,'') End as Address2                          
 ,Case When PartyTypeId in (1,2,3,4) Then isnull(csv.Address3,'') Else isnull(clm.ClaimantAddress3,'') End as Address3                          
 ,Case When PartyTypeId in (1,2,3,4) Then isnull(cnty.CountryName,'') Else IsNull(NullIf(Rtrim(Ltrim(cnt1.CountryName)),''),'Not Mapped') End as CountryName                          
 ,Case When PartyTypeId in (1,2,3,4) Then isnull(csv.City,'') Else IsNull(NullIf(Rtrim(Ltrim(clm.City)),''),'Not Mapped') End as City                           
 ,Case When PartyTypeId in (1,2,3,4) Then isnull(csv.[State],'') Else IsNull(NullIf(Rtrim(Ltrim(clm.[State])),''),'Not Mapped') End as [State]                           
 ,Case When PartyTypeId in (1,2,3,4) Then isnull(csv.PostalCode,'') Else isnull(clm.PostalCode,'') End as PostalCode               
 ,cad.VehicleNo,clm.VehicleRegnNo,clm.ClaimantName,cad.AccidentDate,cad.AccidentTime                          
 ,usr.UserDispName as UserFullName,dpmt.DeptName,usr.DID_No,usr.EmailId,usr.FAX_No,crd.CurrentReserve,crd.CmpCode           
 ,cad.AccidentLoc             
From ClaimAccidentDetails cad                             
left outer join CLM_Claims clm on cad.AccidentClaimId = clm.AccidentClaimId and clm.ClaimID = @ClaimId               
left outer join CLM_ServiceProvider csv on clm.AccidentClaimId = csv.AccidentId and clm.ClaimID = csv.ClaimantNameId and csv.ServiceProviderId = @PartyTypeId               
left outer join CLM_ReserveDetails crd on cad.AccidentClaimId = crd.AccidentClaimId and clm.ClaimID = crd.ClaimID and crd.ReserveId = @ReserveId              
left outer join MNT_Cedant ced on csv.CompanyNameId = ced.CedantId                             
left outer join MNT_Adjusters adj on csv.CompanyNameId = adj.AdjusterId                            
left outer join MNT_DepotMaster dep on csv.CompanyNameId = dep.DepotId               
left outer join MNT_Users usr on usr.SNo = clm.ClaimsOfficer              
left outer join MNT_Country cnty on cnty.CountryShortCode = csv.CountryId 
left outer join MNT_Country cnt1 on cnt1.CountryShortCode = clm.Country                 
left outer join MNT_Department dpmt on dpmt.DeptCode=usr.DeptCode              
where cad.AccidentClaimId = @AccidentClaimId) X              
pivot                     
            (                    
                MAX(CurrentReserve)                    
                FOR CmpCode IN ([COR],[LOU],[LPRF],[SF],[OE],[TOTAL],[CurrentNoofDays])                    
            ) p              
END              
ELSE                        
                        
 BEGIN                        
Insert into  @tempTable (Reference,IPNo,CompanyNameId,Address1,Address2,Address3,CountryName,City,[State],PostalCode,                        
                  VehicleNo,VehicleRegnNo,ClaimantName,AccidentDate,AccidentTime,COR,LOU,LPRF,SF,OE,TOTAL,CurrentRateperday,CurrentNoofdays              
                  ,UserFullName,DeptName,DID_No,EmailId,FAX_No,AccidentLoc)                        
                  values(NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)                        
     Select * from @tempTable                          
 END 