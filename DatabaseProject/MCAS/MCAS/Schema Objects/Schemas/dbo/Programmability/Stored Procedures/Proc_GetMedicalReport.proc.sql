CREATE PROCEDURE [dbo].[Proc_GetMedicalReport]    
 -- Add the parameters for the stored procedure here       
 @AccidentClaimId int= null,      
 @ClaimId int = null ,      
 @HospitalNameId int = null      
AS      
Declare @tempTable           
table( Reference varchar,IPNo varchar,HospitalName varchar            
 ,HospitalAddress1 varchar           
 ,HospitalAddress2 varchar           
 ,HospitalAddress3  varchar          
 ,HospitalCountryName varchar           
 ,HospitalCity   varchar          
 ,HospitalState  varchar          
 ,PostalCode varchar             
 ,VehicleNo varchar,ClaimantName varchar,ClaimantNRICPPNO varchar        
 ,AccidentDate varchar,AccidentTime varchar,VehicleRegnNo varchar            
 ,UserFullName varchar,DeptName varchar,DID_No varchar,EmailId varchar,FAX_No varchar          
 ,ClaimantAddress1 varchar,ClaimantAddress2 varchar,ClaimantAddress3 varchar,ClaimantCountry varchar        
 ,ClaimantCity varchar,ClaimantState varchar,ClaimantPostalCode varchar        
  )        
IF( ( ISNULL(@AccidentClaimId,0)!=0)  and (ISNULL(@ClaimId,0)!=0) and (ISNULL(@HospitalNameId,0)!=0))          
 BEGIN        
 -- SET NOCOUNT ON added to prevent extra result sets from        
 -- interfering with SELECT statements.        
 SET NOCOUNT ON;        
  Declare @HospitalName varchar(max),    
  @HospitalAddress varchar (max),    
  @HospitalAddress2 varchar (max),    
  @HospitalAddress3 varchar (max),    
  @HospitalCity varchar (max),    
  @HospitalState varchar (max),    
  @PostalCode varchar (max) ,  
  @CountryCode varchar (max),  
  @CountryName varchar (max)  
  Select @HospitalName= HospitalName From MNT_Hospital Where Id=@HospitalNameId    
  Select @HospitalAddress= HospitalAddress From MNT_Hospital Where Id=@HospitalNameId    
  Select @HospitalAddress2= HospitalAddress2 From MNT_Hospital Where Id=@HospitalNameId    
  Select @HospitalAddress3= HospitalAddress3 From MNT_Hospital Where Id=@HospitalNameId    
  Select @HospitalCity= City From MNT_Hospital Where Id=@HospitalNameId    
  Select @HospitalState= [State] From MNT_Hospital Where Id=@HospitalNameId    
  Select @PostalCode= PostalCode From MNT_Hospital  Where Id=@HospitalNameId    
  Select @CountryCode= Country From MNT_Hospital  Where Id=@HospitalNameId   
  Select @CountryName= CountryName From MNT_Country  Where CountryShortCode=@CountryCode  
    
    -- Insert statements for procedure here        
 SELECT distinct         
 IsNull(NullIf(Rtrim(Ltrim(clm.SeverityReferenceNo)),''),'Not Mapped') as Reference,        
 IsNull(NullIf(Rtrim(Ltrim(cad.IPNo)),''),'Not Mapped') as IPNo,        
 IsNull(@HospitalName,'Not Mapped') as HospitalName,        
 IsNull(@HospitalAddress,'Not Mapped') as HospitalAddress1,        
 IsNull(@HospitalAddress2,'Not Mapped') as HospitalAddress2,        
 IsNull(@HospitalAddress3,'Not Mapped') as HospitalAddress3,         
 IsNull(@CountryName,'Not Mapped') as HospitalCountryName,         
 IsNull(@HospitalCity,'Not Mapped') as HospitalCity,        
 IsNull(@HospitalState,'Not Mapped') as HospitalState,        
 IsNull(@PostalCode,'Not Mapped') as PostalCode,        
 IsNull(NullIf(Rtrim(Ltrim(cad.VehicleNo)),''),'Not Mapped') as VehicleNo,         
 IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantName)),''),'Not Mapped') as ClaimantName,        
 IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantNRICPPNO)),''),'Not Mapped') as ClaimantNRICPPNO,        
 IsNuLL(cad.AccidentDate, 'Not Mapped') as AccidentDate,        
 IsNull(NullIf(Rtrim(Ltrim(cad.AccidentTime)),''),'Not Mapped') as AccidentTime,        
 IsNull(NullIf(Rtrim(Ltrim(clm.VehicleRegnNo)),''),'Not Mapped') as VehicleRegnNo,        
 IsNull(NullIf(Rtrim(Ltrim(usr.UserDispName)),''),'Not Mapped') as UserFullName,        
 IsNull(NullIf(Rtrim(Ltrim(dpt.DeptName)),''),'Not Mapped') as DeptName,        
 IsNull(NullIf(Rtrim(Ltrim(usr.DID_No)),''),'Not Mapped') as DID_No,        
 IsNull(NullIf(Rtrim(Ltrim(usr.EmailId)),''),'Not Mapped') as EmailId,        
 IsNull(NullIf(Rtrim(Ltrim(usr.FAX_No)),''),'Not Mapped') as FAX_No,        
 IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantAddress1)),''),'Not Mapped') as ClaimantAddress1,        
 IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantAddress2)),''),'Not Mapped') as ClaimantAddress2,        
 IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantAddress3)),''),'Not Mapped') as ClaimantAddress3,        
 IsNull(NullIf(Rtrim(Ltrim(cnt1.CountryName)),''),'Not Mapped') as ClaimantCountry,        
 IsNull(NullIf(Rtrim(Ltrim(clm.City)),''),'Not Mapped') as ClaimantCity,        
 IsNull(NullIf(Rtrim(Ltrim(clm.[State])),''),'Not Mapped') as ClaimantState,        
 IsNull(NullIf(Rtrim(Ltrim(clm.PostalCode)),''),'Not Mapped') as ClaimantPostalCode        
 FROM ClaimAccidentDetails cad               
 left outer join CLM_Claims clm on cad.AccidentClaimId = clm.AccidentClaimId and clm.ClaimID = @ClaimId              
 left outer join CLM_LogRequest clr on clm.AccidentClaimId = clr.AccidentClaimId and clm.ClaimID = clr.ClaimID and clr.Hospital_Id= @HospitalNameId       
 left outer join MNT_Hospital mnh on clr.Hospital_Id = mnh.Id        
 left outer join MNT_Lookups ml on clm.MP = ml.Lookupvalue            
 left outer join MNT_Country cnt on mnh.Country = cnt.CountryShortCode 
 left outer join MNT_Country cnt1 on cnt1.CountryShortCode = clm.Country        
 left outer join MNT_Users usr on clm.ClaimsOfficer = usr.SNo                 
 left outer join MNT_Department dpt on usr.DeptCode = dpt.DeptCode              
 where cad.AccidentClaimId = @AccidentClaimId        
 END        
ELSE          
 BEGIN         
  INSERT INTO  @tempTable (Reference,IPNo,HospitalName,HospitalAddress1,HospitalAddress2,HospitalAddress3,HospitalCountryName,HospitalCity          
        ,HospitalState,PostalCode,VehicleNo,ClaimantName,ClaimantNRICPPNO,AccidentDate,AccidentTime,VehicleRegnNo,UserFullName                     
        ,DeptName,DID_No,EmailId,FAX_No,ClaimantAddress1,ClaimantAddress2,ClaimantAddress3,ClaimantCountry,ClaimantCity                   
        ,ClaimantState,ClaimantPostalCode)          
                  values(NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)          
     SELECT * FROM @tempTable         
 END 

