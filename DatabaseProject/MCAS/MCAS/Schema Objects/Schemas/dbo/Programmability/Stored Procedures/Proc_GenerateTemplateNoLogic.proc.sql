CREATE PROCEDURE [dbo].[Proc_GenerateTemplateNoLogic]
 @AccidentClaimId int=null,                
 @ClaimId int= null                     
AS                
Declare @tempTable               
table( IPNo varchar,ClaimantName varchar             
,ClaimantNRICPPNO varchar               
 ,Address1 varchar               
 ,Address2 varchar               
 ,Address3  varchar              
 ,CountryName varchar               
 ,City   varchar              
 ,[State]   varchar              
 ,PostalCode varchar             
 ,ConfirmedAmount numeric(18,2)                  
  ,AccidentDate varchar,AccidentTime varchar,VehicleNo varchar,VehicleRegnNo varchar                
 ,ClaimantType varchar,AccidentLoc varchar,InfantName varchar            
  )                
               
 if(( isnull(@AccidentClaimId,0)!=0)  and (isnull(@ClaimId,0)!=0))            
            
              
BEGIN                
-- SET NOCOUNT ON added to prevent extra result sets from                  
 -- interfering with SELECT statements.                  
 SET NOCOUNT ON;                  
                  
    -- Insert statements for procedure here                  
 SELECT IsNull(NullIf(Rtrim(Ltrim(cad.IPNo)),''),'Not Mapped')IPNo,IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantName)),''),'Not Mapped')ClaimantName,        
 IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantNRICPPNO)),''),'Not Mapped')ClaimantNRICPPNO            
 ,IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantAddress1)),''),'Not Mapped')as Address1            
 ,IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantAddress2)),''),'Not Mapped')as Address2             
 ,IsNull(NullIf(Rtrim(Ltrim(clm.ClaimantAddress3)),''),'Not Mapped') as Address3                       
 ,IsNull(NullIf(Rtrim(Ltrim(mcr.CountryName)),''),'Not Mapped') as CountryName                
 ,IsNull(NullIf(Rtrim(Ltrim(clm.City)),''),'Not Mapped') as City                 
 ,IsNull(NullIf(Rtrim(Ltrim(clm.[State])),''),'Not Mapped') as [State]                 
 ,IsNull(NullIf(Rtrim(Ltrim(clm.PostalCode)),''),'Not Mapped') as PostalCode             
 ,IsNull(clm.ConfirmedAmount,0.00)  ConfirmedAmount               
 ,IsNull(cad.AccidentDate,'Not Mapped') AccidentDate           
 ,IsNull(NullIf(Rtrim(Ltrim(cad.AccidentTime )),''),'Not Mapped') AccidentTime          
 ,IsNull(NullIf(Rtrim(Ltrim(cad.VehicleNo)),''),'Not Mapped')   VehicleNo         
 ,IsNull(NullIf(Rtrim(Ltrim(clm.VehicleRegnNo )),''),'Not Mapped') VehicleRegnNo              
 ,IsNull(NullIf(Rtrim(Ltrim(ml.[Description])),''),'Not Mapped') as ClaimantType            
 ,IsNull(NullIf(Rtrim(Ltrim(cad.AccidentLoc)),''),'Not Mapped')  AccidentLoc        
 ,IsNull(NullIf(Rtrim(Ltrim(clm.InfantName)),''),'Not Mapped') InfantName           
 from ClaimAccidentDetails cad                   
 left outer join CLM_Claims clm on cad.AccidentClaimId = clm.AccidentClaimId              
 left outer join MNT_Lookups ml on clm.ClaimantType = ml.Lookupvalue and ml.Category = 'ClaimantType'  
 left outer join MNT_Country mcr on clm.Country = mcr.CountryShortCode                            
 where cad.AccidentClaimId = @AccidentClaimId  and clm.ClaimID = @ClaimId                     
END              
              
ELSE              
              
 BEGIN              
     Insert into  @tempTable (IPNo,ClaimantName,ClaimantNRICPPNO,Address1,Address2,Address3,CountryName,City,[State],PostalCode,              
                  ConfirmedAmount,AccidentDate,AccidentTime,VehicleNo,VehicleRegnNo,ClaimantType,AccidentLoc,InfantName)              
                  values(NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)              
     Select * from @tempTable                
 END 