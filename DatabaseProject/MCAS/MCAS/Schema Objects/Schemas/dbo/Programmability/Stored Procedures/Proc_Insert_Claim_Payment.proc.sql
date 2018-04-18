CREATE Procedure [dbo].[Proc_Insert_Claim_Payment]          
 @PaymentId [int],         
 @AccidentClaimId [int],          
 @PolicyId [int],          
 @Payee nvarchar(MAX),          
 @AssignedToSupervisor nvarchar(MAX),          
 @TotalPaymentDue numeric(18,2),          
 @TotalAmountMandate numeric(18,2),          
 @Createddate datetime,          
 @PaymentRequestDate datetime,          
 @PaymentDueDate datetime,          
 @CreatedBy nvarchar(20),          
 @AssignedTo nvarchar(MAX),          
 @ClaimantName nvarchar(MAX),          
 @ClaimType int,          
 @IsActive char(1) = 'Y',          
 @ClaimID int,          
 @Address nvarchar(MAX),          
 @Address1  nvarchar(MAX),          
 @Address2 nvarchar(MAX),          
 @PostalCodes nvarchar(MAX),          
 @CoRemarks nvarchar(MAX),          
 @ApprovePayment  nvarchar(MAX),          
 @SupervisorRemarks  nvarchar(MAX),          
 @ApprovedDate datetime=Null,          
 @MovementType nvarchar(100),          
 @MandateId int,          
 @ReserveId int,
 @DateofNoticetoSafety [datetime],
 @EZLinkCardNo [varchar](1),
 @ODStatus [varchar](1),
 @RecoverableFromInsurerBI [varchar](1)          
AS          
BEGIN            
        
Declare @MandateIdTemp int         
Declare @ReserveIdTemp int  
Declare @SumTotalPayment decimal(18,2)        
Declare @SumTotalMandate  decimal(18,2)     
Declare @SumTotalMandateMovement  decimal(18,2)  

declare @CLM_PaymentDt  as CLM_PaymentDetails
insert into @CLM_PaymentDt select [CmpCode],[TotalPaymentDue],[TotalAmountMandate],[AccidentClaimId],[ReserveId],[MandateId],[PaymentId],[ClaimID] from CLM_PaymentDetails where PaymentId=@PaymentId

--********************************************************        
     --- INSERT PAYMENT INFO IN RESERVE TABLES---        
--********************************************************         
       
INSERT INTO CLM_ReserveSummary         
(        
    AccidentClaimId, ClaimID, ClaimType,         
    MovementType, InitialReserve, PreReserve,         
    MovementReserve, CurrentReserve,          
    PaymentId, Createdby, Createddate, IsActive        
)           
select top 1         
    @AccidentClaimId, @ClaimID, @ClaimType,        
    'P',  InitialReserve, CurrentReserve,         
    -(@TotalPaymentDue),(CurrentReserve - @TotalPaymentDue),         
    @PaymentId, @CreatedBy, GETDATE(), @IsActive         
FROM CLM_ReserveSummary with(nolock)        
where         
(        
    AccidentClaimId = @AccidentClaimId         
    and ReserveId = @ReserveId         
    and ClaimID = @ClaimID         
)        
order by ReserveId desc          
        
select @ReserveIdTemp = Scope_Identity()         
        
INSERT INTO CLM_ReserveDetails         
(        
    ReserveId, CmpCode, InitialReserve, PreReserve,        
    MovementReserve,CurrentReserve, PaymentId,         
    InitialNoofdays, MovementNoofdays, CurrentNoofdays,         
    InitialRateperday, MovementlRateperday,         
    CurrentRateperday, Createdby, Createddate, IsActive, AccidentClaimId,ClaimID ,MovementType       
)          
(        
select         
   @ReserveIdTemp, PD.CmpCode, RD.InitialReserve, RD.CurrentReserve,        
   -(PD.TotalPaymentDue), (RD.CurrentReserve - PD.TotalPaymentDue), @PaymentId,         
   RD.InitialNoofdays, RD.MovementNoofdays, RD.CurrentNoofdays,         
   RD.InitialRateperday, RD.MovementlRateperday,         
   RD.CurrentRateperday, @CreatedBy, @Createddate, @IsActive, @AccidentClaimId,@ClaimID ,@MovementType         
FROM @CLM_PaymentDt PD         
LEFT OUTER JOIN dbo.CLM_ReserveDetails RD with(nolock) ON         
   PD.CmpCode = RD.CmpCode         
WHERE         
(        
   RD.AccidentClaimId = @AccidentClaimId and RD.ReserveId = @ReserveId --and ClaimID = @ClaimID         
)        
        
) 


--********************************************************        
     --- INSERT PAYMENT INFO IN MANDATE TABLES---        
--********************************************************  
       
INSERT INTO CLM_mandateSummary         
(        
    AccidentClaimId, ReserveId, ClaimID, ClaimType, MovementType, AssignedTo,        
    InvestigationResult, Scenario, Evidence,RelatedFacts, COAssessment, SupervisorAssignto,        
    ApproveRecommedations, SupervisorRemarks, PreMandate,         
    MovementMandate, CurrentMandate,  
    PreMandateSP, MovementMandateSP,CurrentMandateSP,    
    PaymentId,InformSafetytoreviewfindings,         
    MandateRecordNo,PreviousOffers,TPCounterOffer,      
    Createdby, Createddate, IsActive,DateofNoticetoSafety,EZLinkCardNo,ODStatus,RecoverableFromInsurerBI )          
select top 1         
    @AccidentClaimId, @ReserveIdTemp, @ClaimID, @ClaimType, 'P', AssignedTo,         
    InvestigationResult, Scenario, Evidence,RelatedFacts, COAssessment, SupervisorAssignto,         
    ApproveRecommedations, SupervisorRemarks,PreMandate,         
    MovementMandate,CurrentMandate, 
    CurrentMandateSP,         
    -(@TotalPaymentDue),(CurrentMandateSP - @TotalPaymentDue),         
    @PaymentId, InformSafetytoreviewfindings,        
    MandateRecordNo,PreviousOffers,TPCounterOffer,          
    @CreatedBy, GETDATE(), @IsActive,@DateofNoticetoSafety,@EZLinkCardNo,@ODStatus,@RecoverableFromInsurerBI          
from CLM_mandateSummary with(nolock)        
where         
(        
    AccidentClaimId = @AccidentClaimId         
    AND MandateId = @MandateId         
)        
order by MandateId desc          
        
select @MandateIdTemp = Scope_Identity()     
    
select @SumTotalPayment = (select SUM(TotalPaymentDue) from  @CLM_PaymentDt where  CmpCode in('COR',	'LOU',	'LOUUN',	'LOR',	'Ex',	'GD',	'ME',	'FME',	'LME',	'LEC',	'LOEAR',	'LODE',	'TRAN'))

select @SumTotalMandate =(select SUM(CurrentMandateSP) from  CLM_MandateDetails where  CmpCode in('COR',	'LOU',	'LOUUN',	'LOR',	'Ex',	'GD',	'ME',	'FME',	'LME',	'LEC',	'LOEAR',	'LODE',	'TRAN') and MandateId =@MandateId)   

select @SumTotalMandateMovement = (@SumTotalMandate-@SumTotalPayment)

     
INSERT INTO CLM_MandateDetails         
(        
    MandateId, CmpCode, PreMandate, MovementMandate, CurrentMandate,  PreMandateSP, MovementMandateSP, CurrentMandateSP,       
    PaymentId, Createdby, Createddate, IsActive, AccidentClaimId, MovementType,ClaimID       
)          
(        
select @MandateIdTemp, m.CmpCode,m.PreMandate,m.MovementMandate,m.CurrentMandate, 
   case 
   when rtrim(ltrim(m.CmpCode)) = 'Labl' then m.CurrentMandateSP 
   when rtrim(ltrim(m.CmpCode)) = 'SubTotal' then @SumTotalMandate
   else TotalAmountMandate
   end 
   , 
   case
   when rtrim(ltrim(m.CmpCode)) = 'Labl' then m.MovementMandateSP 
   when rtrim(ltrim(m.CmpCode)) = 'SubTotal' then -(@SumTotalPayment)
   else -(TotalPaymentDue) 
   end 
   ,
   case 
   when rtrim(ltrim(m.CmpCode)) = 'Labl' then m.CurrentMandateSP
   when rtrim(ltrim(m.CmpCode)) = 'SubTotal' then @SumTotalMandateMovement
   else (TotalAmountMandate - TotalPaymentDue)
   end
   , 
@PaymentId, @CreatedBy, @Createddate, @IsActive, @AccidentClaimId,'P', @ClaimID        
from CLM_MandateDetails m
left join @CLM_PaymentDt t on t.MandateId = m.MandateId and t.CmpCode = m.CmpCode  where m.AccidentClaimId = @AccidentClaimId and m.mandateid=@MandateId
)        
END         

 







GO


