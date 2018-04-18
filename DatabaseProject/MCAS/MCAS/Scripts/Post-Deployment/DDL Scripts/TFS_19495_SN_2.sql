DROP PROCEDURE Proc_SaveMandate;
DROP TYPE TVP_MandateSummary


CREATE TYPE [dbo].[TVP_MandateSummary] AS TABLE(
	[MandateId] [int] NOT NULL,
	[AccidentClaimId] [int] NOT NULL,
	[ReserveId] [int] NOT NULL,
	[ClaimID] [int] NOT NULL,
	[ClaimType] [int] NOT NULL,
	[MovementType] [nvarchar](2) NULL,
	[AssignedTo] [int] NULL,
	[InvestigationResult] [int] NULL,
	[Scenario] [nvarchar](max) NULL,
	[Evidence] [nvarchar](max) NULL,
	[RelatedFacts] [nvarchar](max) NULL,
	[COAssessment] [nvarchar](max) NULL,
	[SupervisorAssignto] [int] NULL,
	[ApproveRecommedations] [nvarchar](2) NULL,
	[SupervisorRemarks] [nvarchar](max) NULL,
	[PreMandate] [numeric](18, 2) NULL,
	[MovementMandate] [numeric](18, 2) NULL,
	[CurrentMandate] [numeric](18, 2) NULL,
	[InformSafetytoreviewfindings] [nvarchar](10) NULL,
	[MandateRecordNo] [nvarchar](100) NULL,
	[Createdby] [nvarchar](200) NULL,
	[Createddate] [datetime] NULL,
	[Modifiedby] [nvarchar](200) NULL,
	[Modifieddate] [datetime] NULL,
	[IsActive] [char](1) NULL,
	[PreMandateSP] [numeric](18, 2) NULL,
	[MovementMandateSP] [numeric](18, 2) NULL,
	[CurrentMandateSP] [numeric](18, 2) NULL,
	[PreviousOffers] [numeric](18, 2) NULL,
	[TPCounterOffer] [numeric](18, 2) NULL,
	[DateofNoticetoSafety] [datetime] NULL,
	[InformedInsurer] [datetime] NULL,
	[EZLinkCardNo] [varchar](1) NULL,
	[ODStatus] [varchar](1) NULL,
	[RecoverableFromInsurerBI] [varchar](1) NULL,
	[ProposedLiability] [numeric](18, 2) NULL
)
GO

CREATE PROCEDURE Proc_SaveMandate    
(    
 @MandateSummary AS [dbo].[TVP_MandateSummary] READONLY,    
 @MandateDetailsList AS [dbo].[TVP_MandateDetails] READONLY,    
 @XMLSummary NVARCHAR(MAX)=NULL,    
 @XMLDetails NVARCHAR(MAX)=NULL,    
 @ActionSummary NVARCHAR(10),    
 @ActionDetails NVARCHAR(10)    
)    
AS    
BEGIN    
 DECLARE @mandateId INT     
 DECLARE @AccidentClaimId [INT]    
 DECLARE @ClaimID [INT]    
 DECLARE @UserId NVARCHAR(100)    
      
 BEGIN TRANSACTION;    
 BEGIN TRY     
     
 --IF condition satisfy, then insert ELSE Update    
 IF EXISTS(SELECT 1 FROM @MandateSummary WHERE MandateId=0)     
 BEGIN    
      
  SELECT @AccidentClaimId=AccidentClaimId,@ClaimID=ClaimID,@UserId=Createdby FROM @MandateSummary    
     
  INSERT INTO CLM_MandateSummary(AccidentClaimId,ReserveId,ClaimID,ClaimType,MovementType,AssignedTo,InvestigationResult,    
    Scenario,Evidence,RelatedFacts,COAssessment,SupervisorAssignto,ApproveRecommedations,SupervisorRemarks,    
    PreMandate,MovementMandate,CurrentMandate,InformSafetytoreviewfindings,MandateRecordNo,Createdby,    
    Createddate,IsActive,PreMandateSP,MovementMandateSP,CurrentMandateSP,PreviousOffers,TPCounterOffer,DateofNoticetoSafety,InformedInsurer,EZLinkCardNo,ODStatus,RecoverableFromInsurerBI,ProposedLiability)    
  SELECT AccidentClaimId,ReserveId,ClaimID,ClaimType,MovementType,AssignedTo,InvestigationResult,    
    Scenario,Evidence,RelatedFacts,COAssessment,SupervisorAssignto,ApproveRecommedations,SupervisorRemarks,    
    PreMandate,MovementMandate,CurrentMandate,InformSafetytoreviewfindings,MandateRecordNo,Createdby,    
    Createddate,IsActive,PreMandateSP,MovementMandateSP,CurrentMandateSP,PreviousOffers,TPCounterOffer,DateofNoticetoSafety,InformedInsurer,EZLinkCardNo,ODStatus,RecoverableFromInsurerBI,ProposedLiability      
  FROM @MandateSummary    
      
  SET @mandateId=SCOPE_IDENTITY()    
      
  INSERT INTO CLM_MandateDetails(MandateId,CmpCode,PreMandate,MovementMandate,CurrentMandate,Createdby,    
    Createddate,IsActive,AccidentClaimId,ClaimID,PreMandateSP,MovementMandateSP,CurrentMandateSP,PreviousOffers,    
    TPCounterOffer,ReserveId,MovementType)    
  SELECT @mandateId,CmpCode,PreMandate,MovementMandate,CurrentMandate,Createdby,    
    Createddate,IsActive,AccidentClaimId,ClaimID,PreMandateSP,MovementMandateSP,CurrentMandateSP,PreviousOffers,    
    TPCounterOffer,ReserveId,MovementType      
  FROM @MandateDetailsList    
      
 END    
 ELSE    
 BEGIN    
      
  SELECT @mandateId=MandateId,@AccidentClaimId=AccidentClaimId,@ClaimID=ClaimID,@UserId=Modifiedby FROM @MandateSummary    
      
  UPDATE tblSummary    
   SET     
    --tblSummary.AccidentClaimId=tvpSummary.AccidentClaimId,    
    tblSummary.ReserveId=tvpSummary.ReserveId,    
    --tblSummary.ClaimID=tvpSummary.ClaimID,    
    tblSummary.ClaimType=tvpSummary.ClaimType,    
    tblSummary.MovementType=tvpSummary.MovementType,    
    tblSummary.AssignedTo=tvpSummary.AssignedTo,    
    tblSummary.InvestigationResult=tvpSummary.InvestigationResult,    
    tblSummary.Scenario=tvpSummary.Scenario,    
    tblSummary.Evidence=tvpSummary.Evidence,    
    tblSummary.RelatedFacts=tvpSummary.RelatedFacts,    
    tblSummary.COAssessment=tvpSummary.COAssessment,    
    tblSummary.SupervisorAssignto=tvpSummary.SupervisorAssignto,    
    tblSummary.ApproveRecommedations=tvpSummary.ApproveRecommedations,    
    tblSummary.SupervisorRemarks=tvpSummary.SupervisorRemarks,    
    tblSummary.PreMandate=tvpSummary.PreMandate,    
    tblSummary.MovementMandate=tvpSummary.MovementMandate,    
    tblSummary.CurrentMandate=tvpSummary.CurrentMandate,    
    tblSummary.InformSafetytoreviewfindings=tvpSummary.InformSafetytoreviewfindings,    
    tblSummary.MandateRecordNo=tvpSummary.MandateRecordNo,    
    tblSummary.Modifiedby=tvpSummary.Modifiedby,    
    tblSummary.Modifieddate =tvpSummary.Modifieddate,    
    tblSummary.IsActive=tvpSummary.IsActive,    
    tblSummary.PreMandateSP=tvpSummary.PreMandateSP,    
    tblSummary.MovementMandateSP=tvpSummary.MovementMandateSP,    
    tblSummary.CurrentMandateSP=tvpSummary.CurrentMandateSP,    
    tblSummary.PreviousOffers=tvpSummary.PreviousOffers,    
    tblSummary.TPCounterOffer=tvpSummary.TPCounterOffer,  
    tblSummary.DateofNoticetoSafety=tvpSummary.DateofNoticetoSafety,    
    tblSummary.InformedInsurer=tvpSummary.InformedInsurer,    
    tblSummary.EZLinkCardNo=tvpSummary.EZLinkCardNo,    
    tblSummary.ODStatus=tvpSummary.ODStatus,    
    tblSummary.RecoverableFromInsurerBI=tvpSummary.RecoverableFromInsurerBI,
    tblSummary.ProposedLiability=tvpSummary.ProposedLiability 
  FROM CLM_MandateSummary AS tblSummary    
   INNER JOIN @MandateSummary AS tvpSummary    
  ON tblSummary.MandateId=tvpSummary.MandateId     
      
  UPDATE tblDetails    
   SET     
    --tblDetails.MandateId=tvpDetails.MandateId,    
    --tblDetails.CmpCode=tvpDetails.CmpCode,    
    tblDetails.PreMandate=tvpDetails.PreMandate,    
    tblDetails.MovementMandate=tvpDetails.MovementMandate,    
    tblDetails.CurrentMandate=tvpDetails.CurrentMandate,    
    --tblDetails.Createdby=tvpDetails.Createdby,    
    --tblDetails.Createddate=tvpDetails.Createddate,    
    tblDetails.Modifiedby=tvpDetails.Modifiedby,    
    tblDetails.Modifieddate=tvpDetails.Modifieddate,    
    --tblDetails.IsActive=tvpDetails.IsActive,    
    --tblDetails.AccidentClaimId=tvpDetails.AccidentClaimId,    
    --tblDetails.ClaimID=tvpDetails.ClaimID,    
    tblDetails.PreMandateSP=tvpDetails.PreMandateSP,    
    tblDetails.MovementMandateSP=tvpDetails.MovementMandateSP,    
    tblDetails.CurrentMandateSP=tvpDetails.CurrentMandateSP,    
    tblDetails.PreviousOffers=tvpDetails.PreviousOffers,    
    tblDetails.TPCounterOffer=tvpDetails.TPCounterOffer    
    --tblDetails.ReserveId=tvpDetails.ReserveId,    
    --tblDetails.MovementType=tvpDetails.MovementType      
  FROM CLM_MandateDetails AS tblDetails    
   INNER JOIN @MandateDetailsList AS tvpDetails    
  ON tblDetails.MandateDetailId=tvpDetails.MandateDetailId    
     
 END    
     
 EXEC Proc_InsertTransactionAuditLog 'CLM_MandateSummary','MandateId',@UserId,@ClaimID,@AccidentClaimId,@ActionSummary,@XMLSummary    
 EXEC Proc_InsertTransactionAuditLog 'CLM_MandateDetails','MandateDetailId',@UserId,@ClaimID,@AccidentClaimId,@ActionDetails,@XMLDetails    
     
 END TRY    
 BEGIN CATCH    
  IF @@TRANCOUNT > 0    
        ROLLBACK TRANSACTION;    
           
        DECLARE @ErrorProcedure nvarchar(100)    
        DECLARE @ErrorMessage nvarchar(MAX)    
        DECLARE @entityType nvarchar(100)          
  DECLARE @SNo [INT]    
            
        --SELECT @AccidentClaimId=AccidentClaimId,@ClaimID=ClaimID,@UserId=Createdby FROM @MandateSummary          
  SELECT @entityType=[Type] from MNT_TableDesc where TableName='CLM_MandateSummary'             
  SELECT @ErrorProcedure=ERROR_PROCEDURE(),@ErrorMessage=ERROR_MESSAGE()      
  select @SNo=SNo from MNT_Users where UserId=@UserId    
          
  EXEC Proc_InsertExceptionLog @ErrorMessage,null,@AccidentClaimId,null,null,@ClaimID,null,@entityType,@ErrorProcedure,@ErrorMessage,null,null,null,'MCAS',@SNo,'SqlException'             
 END CATCH    
     
 IF @@TRANCOUNT > 0    
 BEGIN    
  COMMIT TRANSACTION;    
  --RETURN 1;    
  --SELECT @mandateId;        
 END    
 ELSE    
 BEGIN    
  --RETURN 0;    
  --SELECT 0;    
  SET @mandateId=0    
 END    
     
 RETURN @mandateId    
END