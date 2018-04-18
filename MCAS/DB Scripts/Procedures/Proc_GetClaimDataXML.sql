IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimDataXML]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimDataXML]
GO


/* 
===========================================================================================================                                                                                            
Proc Name                : dbo.Proc_GetClaimDataXML
                                                                         
Created by               : Santosh Kumar
                                                                                 
Date                     : 21 July 2014                                                                                                                                          
Purpose                  : To get All the Claims Parent & Child table data as a XML        
Revison History          :                                                                                                                                                          
Used In                  : MCAS        
===========================================================================================================                                                                                            
Date     Review By          Comments                                                                                                                                                          
==========================================================================================================                                                                               
exec dbo.Proc_GetClaimDataXML 105
*/                                                                                           
CREATE proc [dbo].[Proc_GetClaimDataXML]                                                                 
(         
 @ACCIDENTCLAIM_ID    int                                                                                                        
)                                                                                                                                AS                                                                                                                            
    


           
BEGIN              
  
DECLARE @DATA AS TABLE (xml_DATA XML)  
DECLARE @MDATA AS TABLE (Mxml_DATA XML)  
DECLARE @ClaimId int   
  
Select @ClaimId = ClaimId From CLM_Claims  where AccidentClaimId = @ACCIDENTCLAIM_ID   
  
  
 INSERT INTO @DATA         
 SELECT ISNULL((       
   SELECT Accident.AccidentClaimId,Accident.PolicyId,Accident.IPNo,Accident.ClaimNo,Accident.BusServiceNo,Accident.VehicleNo,  
 convert(varchar,Accident.AccidentDate,107)AccidentDate,Accident.AccidentTime,Accident.ReportedDate,Accident.Facts,Accident.Damages,Accident.DateofFinding,  
 Accident.InvestStatus,Accident.Results,Accident.BOIResults,Accident.FinalLiability,Accident.DutyIO,Accident.Make,  
 Accident.ModelNo,Accident.DriverEmployeeNo,Accident.DriverName,Accident.DriverNRICNo,Accident.DriverMobileNo,  
 Accident.InitialEstimate,Accident.InsurerClaim,Accident.MandateReqd,Accident.Organization,Accident.AccidentImage,  
 Accident.LossTypeCode,Accident.LossNatureCode,Accident.TPClaimentStatus,Accident.TimePeriod,Accident.BusCaptainFault,  
 Accident.ODAssignmentTranId,Accident.TPAssignmentTranId,Accident.TempClaimNo,convert(varchar,GETDATE(),107) PreviewDocDate       
    FROM ClaimAccidentDetails Accident   
    where AccidentClaimId = @ACCIDENTCLAIM_ID       
    FOR XML Auto, Elements  
    ), '<Accident></Accident>')      
          
  
 INSERT INTO @DATA         
 SELECT ISNULL((              
    SELECT Claims.ClaimID,Claims.AccidentClaimId,Claims.PolicyId,Claims.ClaimType,Claims.ClaimDate,Claims.ClaimOfficer,  
 Claims.FinalSettleDate,Claims.ClaimStatus,Claims.TimeBarDate,Claims.CaseCategory,Claims.CaseStatus,Claims.DriversLiability,  
 Claims.ClaimAmount,Claims.PaidDate,Claims.BalanceLOG,Claims.LOGAmount,Claims.LOURate,Claims.LOUDays,  
 Claims.ReportSentInsurer,Claims.ReferredInsurers,Claims.InformInsurer,Claims.ExcessRecoveredDate,Claims.WritIssuedDate,  
 Claims.WritNo,Claims.SenstiveCase,Claims.MPLetter,Claims.ReserveCurr,Claims.ReserveExRate,Claims.ReserveAmt,  
 Claims.ExpensesCurr,Claims.ExpensesExRate,Claims.TotalReserve,Claims.AdjusterAppointed,Claims.LawyerAppointed,  
 Claims.SurveyorAppointed,Claims.DepotWorkshop,Claims.ExpensesAmt,Claims.PayableTo,Claims.ClaimAmountCurr,  
 Claims.ClaimAmtPayout,Claims.ClaimAmtPayoutExRate,Claims.ExpensesAmount,Claims.ReserveAmount  
    FROM CLM_Claims Claims where AccidentClaimId = @ACCIDENTCLAIM_ID       
    FOR XML Auto, Elements  
    ), '<Claims></Claims>')       
      
      
 INSERT INTO @DATA         
 SELECT ISNULL((              
    SELECT Claim_Notes.NoteId,Claim_Notes.PolicyId,Claim_Notes.ClaimId,Claim_Notes.NoteCode,  
 Claim_Notes.NoteDate,Claim_Notes.NoteTime,Claim_Notes.ImageCode,Claim_Notes.ImageId,Claim_Notes.NotesDescription  
    FROM CLM_Notes Claim_Notes where ClaimId = @ClaimId       
    FOR XML Auto, Elements  
    ), '<Claim_Notes></Claim_Notes>')      
  
  
  
 INSERT INTO @DATA         
 SELECT ISNULL((              
    SELECT Claim_ThirdParty.TPartyId,Claim_ThirdParty.ClaimId,Claim_ThirdParty.OtherPartyType,Claim_ThirdParty.CompanyName,  
 Claim_ThirdParty.Reference,Claim_ThirdParty.DateAppointed,Claim_ThirdParty.TPVehicleNo,Claim_ThirdParty.TPSurname,  
 Claim_ThirdParty.TPGivenName,Claim_ThirdParty.TPNRICNo,Claim_ThirdParty.TPAdd1,Claim_ThirdParty.TPAdd2,  
 Claim_ThirdParty.TPCountry,Claim_ThirdParty.TPPostalCode,Claim_ThirdParty.TPOfficeNo,Claim_ThirdParty.TPMobNo,  
 Claim_ThirdParty.TPFaxNo,Claim_ThirdParty.TPEmailAdd,Claim_ThirdParty.PaidThisYear,Claim_ThirdParty.PaidToDate,  
 Claim_ThirdParty.RecovThisYear,Claim_ThirdParty.RecovToDate,Claim_ThirdParty.VehicleRegnNo,Claim_ThirdParty.VehicleMake,  
 Claim_ThirdParty.VehicleModel,Claim_ThirdParty.LossDamageDesc,Claim_ThirdParty.TPAdjuster,Claim_ThirdParty.TPLawyer,  
 Claim_ThirdParty.TPWorkShop,Claim_ThirdParty.Remarks,Claim_ThirdParty.AttachedFile,Claim_ThirdParty.ReserveCurr,  
 Claim_ThirdParty.ReserveExRate,Claim_ThirdParty.ReserveAmt,Claim_ThirdParty.ExpensesCurr,Claim_ThirdParty.ExpensesExRate,  
 Claim_ThirdParty.ExpensesAmt,Claim_ThirdParty.TotalReserve,Claim_ThirdParty.ClaimAmount,Claim_ThirdParty.PaidDate,  
 Claim_ThirdParty.BalanceLOG,Claim_ThirdParty.LOGAmount,Claim_ThirdParty.LOURate,Claim_ThirdParty.LOUDays,  
 Claim_ThirdParty.ClaimAmtCurr,Claim_ThirdParty.ClaimAmtExRate,Claim_ThirdParty.ClaimAmt,  
 Claim_ThirdParty.PayableTo,Claim_ThirdParty.ExpensesAmount,Claim_ThirdParty.ReserveAmount  
    FROM CLM_ThirdParty Claim_ThirdParty where ClaimId = @ClaimId       
    FOR XML Auto, Elements  
    ), '<Claim_ThirdParty></Claim_ThirdParty>')      
  
  
  
 INSERT INTO @DATA         
 SELECT ISNULL((              
    SELECT Claim_Transaction.TransactionId,Claim_Transaction.ClaimId,Claim_Transaction.PolicyId,Claim_Transaction.TransactionDate,  
 Claim_Transaction.TransactionType,Claim_Transaction.CreditorName,Claim_Transaction.ExpenseCode,Claim_Transaction.AmountPaid,  
 Claim_Transaction.Authorizedby,Claim_Transaction.AuthorizedDate,Claim_Transaction.AuthorizedTime,  
 Claim_Transaction.ProcessedDate  
    FROM CLM_Transactions Claim_Transaction where ClaimId = @ClaimId       
    FOR XML Auto, Elements  
    ), '<Claim_Transaction></Claim_Transaction>')      
  
  
    
 INSERT INTO @DATA        
 SELECT ISNULL((              
    SELECT Claim_Reserve.ReserveId,Claim_Reserve.ClaimID,Claim_Reserve.ClaimantID,Claim_Reserve.ReserveType,  
 Claim_Reserve.MovementType,Claim_Reserve.PreReserveLocalAmt,Claim_Reserve.PreResLocalCurrCode,  
 Claim_Reserve.PreExRateLocalCurr,Claim_Reserve.PreReserveOrgAmt,Claim_Reserve.PreResOrgCurrCode,  
 Claim_Reserve.PreExRateOrgCurr,Claim_Reserve.FinalReserveLocalAmt,Claim_Reserve.FinalResLocalCurrCode,  
 Claim_Reserve.FinalExRateLocalCurr,Claim_Reserve.FinalReserveOrgAmt,Claim_Reserve.FinalResOrgCurrCode,  
 Claim_Reserve.FinalExRateOrgCurr,Claim_Reserve.MoveReserveLocalAmt,Claim_Reserve.MoveResLocalCurrCode,  
 Claim_Reserve.MoveExRateLocalCurr,Claim_Reserve.MoveReserveOrgAmt,Claim_Reserve.MoveResOrgCurrCode,  
 Claim_Reserve.MoveExRateOrgCurr,Claim_Reserve.ClaimantType  
    FROM CLM_ClaimReserve Claim_Reserve where ClaimId = @ClaimId       
    FOR XML Auto, Elements  
    ), '<Claim_Reserve></Claim_Reserve>')      
      
  
  
 INSERT INTO @DATA         
 SELECT ISNULL((              
    SELECT Claim_Payment.PaymentId,Claim_Payment.ClaimID,Claim_Payment.ClaimantID,Claim_Payment.PayeeType,  
 Claim_Payment.PaymentType,Claim_Payment.PaymentDueDate,Claim_Payment.Payee,Claim_Payment.PayeeAdd,  
 Claim_Payment.PaymentCurr,Claim_Payment.PayableOrgID,Claim_Payment.PayableLocalID,Claim_Payment.PayableOrgAmt,  
 Claim_Payment.PayableLocalAmt,Claim_Payment.PreReserveOrgAmt,Claim_Payment.PreReserveLocalAmt,  
 Claim_Payment.FinalReserveOrgAmt,Claim_Payment.FinalReserveLocalAmt  
    FROM CLM_ClaimPayment Claim_Payment where ClaimId = @ClaimId       
    FOR XML Auto, Elements  
    ), '<Claim_Payment></Claim_Payment>')            
 
 INSERT INTO @DATA         
 SELECT ISNULL((              
    SELECT top(1) CLM_Payment.Payee PayeeName
    FROM CLM_Payment where ClaimId = @ClaimId and AccidentClaimId = @ACCIDENTCLAIM_ID   
    FOR XML Auto, Elements  
    ), '<CLM_Payment></CLM_Payment>') 
               
INSERT INTO @DATA         
 SELECT ISNULL((              
    SELECT  upper(hosp.HospitalName) HospitalName,upper(hosp.HospitalAddress) HospitaAddress,case when ContactPersonName is not null and Email is not null then ContactPersonName+' (By email:'+hosp.Email+')'  
                                                                                                                         when ContactPersonName is not null and Email is null then ContactPersonName
                                                                                                                         when Email is not null and ContactPersonName is null then ' (By email:'+hosp.Email+')' 
                                                                                                                        Else ''
                                                                                                                     End as Attention
    FROM MNT_Hospital hosp,CLM_LogRequest logs where hosp.Id = logs.Hospital_Id 
         and logs.AccidentClaimId = @ACCIDENTCLAIM_ID and logs.ClaimID=@ClaimId 
    FOR XML Auto, Elements  
    ), '<MNT_Hospital></MNT_Hospital>')   
   
    
 INSERT INTO @MDATA   
 SELECT ISNULL(  
   
 (SELECT xml_DATA.query('/') FROM @DATA FOR XML PATH(''), TYPE)  
   
 , '<Accident></Accident>')    
      
 SELECT Mxml_DATA as 'INPUTXML' FROM @MDATA FOR XML PATH(''), TYPE  
          
  
End  






GO


