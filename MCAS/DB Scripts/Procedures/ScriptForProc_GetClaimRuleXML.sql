/* ===========================================================================================================                                                                                            
Proc Name                : dbo.Proc_GetClaimRuleXML                                                                                                                                    
Created by               : Pravesh K Chandel                                                                                                                                                          
Date                     : 8 July. 2014                                                                                                                                          
Purpose                  : To get the Claim keys        
Revison History          :                                                                                                                                                          
Used In                  : MCAS        
===========================================================================================================                                                                                            
Date     Review By          Comments                                                                                                                                                          
==========================================================================================================                                                                               
drop proc dbo.Proc_GetClaimRuleXML 102  
*/                                                                                           
ALTER proc [dbo].[Proc_GetClaimRuleXML]                                                                                                                                               
(         
 @ACCIDENTCLAIM_ID    int,                                                                                                                                                          
 @CLAIM_ID    int  =null                                                                                                                                                        
)                                                                                                                                                          
AS                                                                                                                                           
BEGIN            
DECLARE @IsTPClaiment varchar(1) 
  
Select ClaimNo,AccidentDate,Facts,IPNo,BusServiceNo,VehicleNo,ReportedDate,TPClaimentStatus,DutyIO,Make,ModelNo,DriverEmployeeNo,DriverName,DriverNRICNo,DriverMobileNo    
 from ClaimAccidentDetails as ACCIDENTDETAIL where AccidentClaimId = @ACCIDENTCLAIM_ID     
FOR XML AUTO,ELEMENTS,ROOT('ACCIDENTS')     
  
  
    
Select ClaimType,isnull(convert(varchar,ClaimDate),'') as ClaimDate,ClaimOfficer,ClaimStatus,CaseCategory,CaseStatus,  
isnull(convert(varchar,InformInsurer),'') as  InformInsurer   
from CLM_Claims as CLAIMDETAIL where AccidentClaimId = @ACCIDENTCLAIM_ID     
 FOR XML AUTO,ELEMENTS,ROOT('CLAIMS')     
   
--Set @CLAIM_ID = (select ClaimID from CLM_Claims where AccidentClaimId = @ACCIDENTCLAIM_ID )   
   
SET @IsTPClaiment = (select TPClaimentStatus from ClaimAccidentDetails where AccidentClaimId = @ACCIDENTCLAIM_ID)   
   
 --select isnull(OtherPartyType,'')as OtherPartyType,isnull(CompanyName,'') as CompanyName,isnull(@IsTPClaiment,'') as TPClaimant  from CLM_ThirdParty as TPDETAIL where ClaimId = @CLAIM_ID     
 --FOR XML AUTO,ELEMENTS,ROOT('THIRDPARTIES')   
  
   
 select NoteCode,NoteDate,NoteTime,ImageCode,ImageId from CLM_Notes as NOTESDETAIL where AccidentId = @ACCIDENTCLAIM_ID    
 FOR XML AUTO,ELEMENTS,ROOT('CLAIMNOTES')   
   
 --select TransactionDate,TransactionType,CreditorName,Authorizedby,ExpenseCode from CLM_Transactions as TRANSDETAIL where ClaimId = @CLAIM_ID    
 --FOR XML AUTO,ELEMENTS,ROOT('CLAIMTRANSACTIONS')  
    
 select LISTTYPEID,TOUSERID,FROMUSERID,STARTTIME,ReminderBeforeCompletion,Escalation,SUBJECTLINE from TODODIARYLIST as DIARYDETAIL where AccidentId = @ACCIDENTCLAIM_ID    
 FOR XML AUTO,ELEMENTS,ROOT('CLAIMDIARY')    
 
END  
  