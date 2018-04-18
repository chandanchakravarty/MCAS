CREATE Procedure Proc_LetterofGuarantee  
(  
 @LogId int=NULL  
)  
As            
Declare @tempTable             
 Table(        
        LogId varchar ,LogRefNo varchar,AccidentClaimId varchar,ClaimID varchar,        
        LOGAmount varchar,LOGDate varchar,CORemarks varchar,HospitalName varchar,HospitalAddress varchar,        
        HospitalContactNo varchar,HospitalFaxNo varchar,ContactPersonName varchar,        
        Email varchar,officeNo varchar,FaxNo varchar,HospitalAddress2 varchar,HospitalAddress3 varchar,        
        City varchar,[State] varchar,CountryName varchar,PostalCode varchar,FirstContactPersonName varchar,        
        MobileNo1 varchar,SecondContactPersonName varchar,EmailAddress2 varchar,        
        OffNo2 varchar,MobileNo2 varchar,Fax2 varchar,VehicleNo varchar,AccidentDate varchar,AccidentTime varchar,         
        UserFullName varchar,DID_No varchar,DeptName varchar, ClaimantName varchar ,ClaimantNRICPPNO varchar,ClaimantType varchar,
		LogApproverUserName varchar, LogApproverDeptName varchar, LogApproverGroupName varchar
      )              
             
IF( @LogId!=0)          
          
            
BEGIN              
               
SET NOCOUNT ON; 
Declare @LogApproverUser nvarchar(100),@LogQApproverDept nvarchar(100), @LogApproverGroup nvarchar(100)         
      Select @LogApproverUser = usr.UserFullName,@LogApproverGroup= grp.GroupName,@LogQApproverDept = dpt.DeptName from MNT_Users usr
      Left outer join MNT_GroupsMaster grp on usr.GroupId = grp.GroupId
      Left outer join MNT_Department dpt on usr.DeptCode = dpt.DeptCode
      where LOGApproverCheckbox = 1               
                
SELECT  logRqst.LogId,  
  IsNull(NullIf(Rtrim(Ltrim(logRqst.LogRefNo)),''),'Not Mapped') AS LogRefNo,  
  IsNull(NullIf(Rtrim(Ltrim(logRqst.AccidentClaimId)),''),'Not Mapped') AS AccidentClaimId,  
  IsNull(NullIf(Rtrim(Ltrim(logRqst.ClaimID)),''),'Not Mapped') AS ClaimID,        
        logRqst.LOGAmount,  
        logRqst.LOGDate,  
        IsNull(NullIf(Rtrim(Ltrim(logRqst.CORemarks)),''),'Not Mapped') AS CORemarks,  
        IsNull(NullIf(Rtrim(Ltrim(hptl.[HospitalName])),''),'Not Mapped') AS [HospitalName],  
        IsNull(NullIf(Rtrim(Ltrim(hptl.[HospitalAddress])),''),'Not Mapped') AS [HospitalAddress],        
        IsNull(NullIf(Rtrim(Ltrim(hptl.[HospitalContactNo])),''),'Not Mapped') AS [HospitalContactNo],  
        IsNull(NullIf(Rtrim(Ltrim(hptl.[HospitalFaxNo])),''),'Not Mapped') AS [HospitalFaxNo],  
        IsNull(NullIf(Rtrim(Ltrim(hptl.[ContactPersonName])),''),'Not Mapped') AS [ContactPersonName],        
        IsNull(NullIf(Rtrim(Ltrim(hptl.[Email])),''),'Not Mapped') AS [Email],  
        IsNull(NullIf(Rtrim(Ltrim(hptl.[officeNo])),''),'Not Mapped') AS [officeNo],  
        IsNull(NullIf(Rtrim(Ltrim(hptl.[FaxNo])),''),'Not Mapped') AS [FaxNo],  
        IsNull(NullIf(Rtrim(Ltrim(hptl.[HospitalAddress2])),''),'Not Mapped') AS [HospitalAddress2],  
        IsNull(NullIf(Rtrim(Ltrim(hptl.[HospitalAddress3])),''),'Not Mapped') AS [HospitalAddress3],        
        IsNull(NullIf(Rtrim(Ltrim(hptl.[City])),''),'Not Mapped') AS [City],  
        IsNull(NullIf(Rtrim(Ltrim(hptl.[State])),''),'Not Mapped') AS [State],  
        IsNull(NullIf(Rtrim(Ltrim(cnty.CountryName)),''),'Not Mapped') AS CountryName,  
        IsNull(NullIf(Rtrim(Ltrim(hptl.[PostalCode])),''),'Not Mapped') AS [PostalCode],  
        IsNull(NullIf(Rtrim(Ltrim(hptl.[FirstContactPersonName])),''),'Not Mapped') AS [FirstContactPersonName],        
        IsNull(NullIf(Rtrim(Ltrim(hptl.[MobileNo1])),''),'Not Mapped') AS [MobileNo1],  
        IsNull(NullIf(Rtrim(Ltrim(hptl.[SecondContactPersonName])),''),'Not Mapped') AS [SecondContactPersonName],  
        IsNull(NullIf(Rtrim(Ltrim(hptl.[EmailAddress2])),''),'Not Mapped') AS [EmailAddress2],        
        IsNull(NullIf(Rtrim(Ltrim(hptl.[OffNo2])),''),'Not Mapped') AS [OffNo2],  
        IsNull(NullIf(Rtrim(Ltrim(hptl.[MobileNo2])),''),'Not Mapped') AS [MobileNo2],  
        IsNull(NullIf(Rtrim(Ltrim(hptl.[Fax2])),''),'Not Mapped') AS [Fax2],  
        IsNull(NullIf(Rtrim(Ltrim(cad.VehicleNo)),''),'Not Mapped') AS VehicleNo,  
        cad.AccidentDate,  
        IsNull(NullIf(Rtrim(Ltrim(cad.AccidentTime)),''),'Not Mapped') AS AccidentTime,         
        IsNull(NullIf(Rtrim(Ltrim(usr.UserFullName)),''),'Not Mapped') AS UserFullName,  
        IsNull(NullIf(Rtrim(Ltrim(usr.DID_No)),''),'Not Mapped') AS DID_No,  
        IsNull(NullIf(Rtrim(Ltrim(dpt.DeptName)),''),'Not Mapped') AS DeptName,   
        IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantName)),''),'Not Mapped') AS ClaimantName,  
        IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantNRICPPNO)),''),'Not Mapped') AS ClaimantNRICPPNO,  
        IsNull(NullIf(Rtrim(Ltrim([Description])),''),'Not Mapped') AS ClaimantType,
		IsNull(NullIf(Rtrim(Ltrim(@LogApproverUser)),''),'Not Mapped') AS LogApproverUserName,
		IsNull(NullIf(Rtrim(Ltrim(@LogQApproverDept)),''),'Not Mapped') AS LogApproverDeptName,
		IsNull(NullIf(Rtrim(Ltrim(@LogApproverGroup)),''),'Not Mapped') AS LogApproverGroupName       
From clm_LogRequest logRqst        
      left outer join MNT_Hospital hptl on logRqst.hospital_Id = hptl.Id        
      left outer join MNT_Country cnty on hptl.Country = cnty.CountryShortCode         
      left outer join ClaimAccidentDetails cad on cad.AccidentClaimId = logRqst.AccidentClaimId         
      left outer join CLM_Claims clm on clm.ClaimId = logRqst.ClaimId         
      left outer join MNT_Users usr on usr.SNo = clm.ClaimsOfficer        
      left outer join MNT_Department dpt on usr.DeptCode = dpt.DeptCode        
      left outer join mnt_lookups lkup on clm.ClaimantType = lkup.Lookupvalue and lkup.category='ClaimantType' and lkup.IsActive = 'Y'        
      Where logRqst.LogId=@LogId            
END            
            
ELSE            
            
 BEGIN            
     Insert into  @tempTable (        
                              LogId,LogRefNo,AccidentClaimId,ClaimID,LOGAmount,LOGDate,CORemarks,HospitalName,HospitalAddress,        
                              HospitalContactNo,HospitalFaxNo,ContactPersonName,Email,officeNo,FaxNo,HospitalAddress2,HospitalAddress3,        
                              City,[State],CountryName,PostalCode,FirstContactPersonName,MobileNo1,SecondContactPersonName,EmailAddress2,        
                             OffNo2,MobileNo2,Fax2,VehicleNo,AccidentDate,AccidentTime,UserFullName,DID_No,DeptName,ClaimantName ,ClaimantNRICPPNO,ClaimantType,
							  LogApproverUserName,LogApproverDeptName,LogApproverGroupName           
                             )            
     Values(        
            NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,        
            NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL        
            )            
     Select * from @tempTable              
 END        
   