CREATE PROCEDURE [dbo].[Proc_SavePayment]
(
	@PaymentSummary AS [dbo].[TVP_PaymentSummary] READONLY,
	@PaymentDetailsList AS [dbo].[TVP_PaymentDetails] READONLY,
	@XMLSummary NVARCHAR(MAX)=NULL,
	@XMLDetails NVARCHAR(MAX)=NULL,
	@ActionSummary NVARCHAR(10),
	@ActionDetails NVARCHAR(10)
)
AS
BEGIN
	DECLARE @PaymentId INT	
	
	DECLARE @AccidentClaimId int
	DECLARE @PolicyId int
	DECLARE @Payee nvarchar(max)
	DECLARE @AssignedToSuperVisor nvarchar(max)
	DECLARE @TotalPaymentDue numeric(18, 2)
	DECLARE @TotalAmountMandate numeric(18, 2)
	DECLARE @CreatedDate datetime
	DECLARE @Modifieddate datetime
	DECLARE @PaymentRequestDate datetime
	DECLARE @PaymentDueDate datetime
	DECLARE @CreatedBy nvarchar(100)
	DECLARE @Modifiedby nvarchar(100)
	DECLARE @AssignedTo nvarchar(max)
	DECLARE @ClaimantName nvarchar(max)
	DECLARE @PaymentRecordNo nvarchar(max)
	DECLARE @ClaimType int
	DECLARE @IsActive char(1) 
	DECLARE @ClaimId int
	DECLARE @Address nvarchar(max) 
	DECLARE @Address1 nvarchar(max)
	DECLARE @Address2 nvarchar(max)
	DECLARE @PostalCodes nvarchar(max)
	DECLARE @CoRemarks nvarchar(max)
	DECLARE @ApprovePayment nvarchar(max)
	DECLARE @SuperVisorRemarks nvarchar(max)
	DECLARE @ApprovedDate datetime
	DECLARE @MovementType nvarchar(50)
	DECLARE @MandateId int
	DECLARE @ReserveId int
	DECLARE @MandateRecord nvarchar(max)
	DECLARE @UserId NVARCHAR(100)
	DECLARE @DateofNoticetoSafety [datetime]
	DECLARE @InformSafetytoreviewfindings [nvarchar](10)
	DECLARE @EZLinkCardNo [varchar](1)
	DECLARE @ODStatus [varchar](1)
	DECLARE @RecoverableFromInsurerBI [varchar](1)
	
	BEGIN TRANSACTION;
	BEGIN TRY 
	
	SELECT 
		@PaymentId=PaymentId,
		@AccidentClaimId=AccidentClaimId,
		@PolicyId=PolicyId,
		@Payee=Payee,
		@AssignedToSuperVisor=AssignedToSupervisor,
		@TotalPaymentDue=TotalPaymentDue,
		@TotalAmountMandate=TotalAmountMandate,
		@CreatedDate=Createddate,
		@Modifieddate=Modifieddate,
		@PaymentRequestDate=PaymentRequestDate,
		@PaymentDueDate=PaymentDueDate,
		@CreatedBy=CreatedBy,
		@Modifiedby=Modifiedby,
		@AssignedTo=AssignedTo,
		@ClaimantName=ClaimantName,
		@PaymentRecordNo=PaymentRecordNo,
		@ClaimType=ClaimType,
		@IsActive=IsActive,
		@ClaimId=ClaimID,
		@Address=Address,
		@Address1=ISNULL(Address1,''),
		@Address2=ISNULL(Address2,''),
		@PostalCodes=ISNULL(PostalCodes,''),
		@CoRemarks=ISNULL(CoRemarks,''),
		@ApprovePayment=ISNULL(ApprovePayment,''),
		@SuperVisorRemarks=ISNULL(SupervisorRemarks,''),
		@ApprovedDate=ApprovedDate,
		@MovementType=MovementType,
		@MandateId=MandateId,
		@ReserveId=ReserveId,
		@DateofNoticetoSafety=DateofNoticetoSafety,
	    @InformSafetytoreviewfindings=InformSafetytoreviewfindings,
	    @EZLinkCardNo=EZLinkCardNo,
	    @ODStatus=ODStatus,
	    @RecoverableFromInsurerBI=RecoverableFromInsurerBI		
		FROM @PaymentSummary
	
	--IF condition satisfy, then insert ELSE Update
	IF EXISTS(SELECT 1 FROM @PaymentSummary WHERE PaymentId=0) 
	BEGIN
		
		SELECT @UserId=Createdby FROM @PaymentSummary      
		
		INSERT INTO CLM_PaymentSummary(AccidentClaimId,PolicyId,Payee,AssignedToSupervisor,TotalPaymentDue,TotalAmountMandate,Createddate,
			PaymentRequestDate,PaymentDueDate,CreatedBy,AssignedTo,ClaimantName,PaymentRecordNo,ClaimType,
			IsActive,ClaimID,Address,Address1,Address2,PostalCodes,CoRemarks,ApprovePayment,SupervisorRemarks,ApprovedDate,
			MovementType,MandateId,ReserveId,MandateRecord,DateofNoticetoSafety,InformSafetytoreviewfindings,EZLinkCardNo,ODStatus,RecoverableFromInsurerBI)		
		SELECT AccidentClaimId,PolicyId,Payee,AssignedToSupervisor,TotalPaymentDue,TotalAmountMandate,Createddate,
			PaymentRequestDate,PaymentDueDate,CreatedBy,AssignedTo,ClaimantName,PaymentRecordNo,ClaimType,
			IsActive,ClaimID,Address,Address1,Address2,PostalCodes,CoRemarks,ApprovePayment,SupervisorRemarks,
			CASE 
				WHEN ApprovePayment='Y' 
				THEN ApprovedDate 
				ELSE NULL 
			END,
			MovementType,MandateId,ReserveId,MandateRecord,DateofNoticetoSafety,InformSafetytoreviewfindings,EZLinkCardNo,ODStatus,RecoverableFromInsurerBI			
		FROM @PaymentSummary
		
		SET @PaymentId=SCOPE_IDENTITY()
		
		INSERT INTO CLM_PaymentDetails(CmpCode,TotalPaymentDue,TotalAmountMandate,Createdby,Createddate,IsActive,AccidentClaimId,
				ReserveId,MandateId,PaymentId,ClaimId)		
		SELECT CmpCode,TotalPaymentDue,TotalAmountMandate,Createdby,Createddate,IsActive,AccidentClaimId,
				ReserveId,MandateId,@PaymentId,ClaimId
		FROM @PaymentDetailsList
		
	END
	ELSE
	BEGIN		
		SELECT @PaymentId=PaymentId,@UserId=Modifiedby FROM @PaymentSummary
		
		UPDATE tblSummary
			SET
				--tblSummary.AccidentClaimId=tvpSummary.AccidentClaimId,
				tblSummary.PolicyId=tvpSummary.PolicyId,
				tblSummary.Payee=tvpSummary.Payee,
				tblSummary.AssignedToSupervisor=tvpSummary.AssignedToSupervisor,
				tblSummary.TotalPaymentDue=tvpSummary.TotalPaymentDue,
				tblSummary.TotalAmountMandate=tvpSummary.TotalAmountMandate,
				tblSummary.Modifieddate=tvpSummary.Modifieddate,
				tblSummary.PaymentRequestDate=tvpSummary.PaymentRequestDate,
				tblSummary.PaymentDueDate=tvpSummary.PaymentDueDate,
				tblSummary.Modifiedby=tvpSummary.Modifiedby,
				tblSummary.AssignedTo=tvpSummary.AssignedTo,
				tblSummary.ClaimantName=tvpSummary.ClaimantName,
				tblSummary.PaymentRecordNo=tvpSummary.PaymentRecordNo,
				tblSummary.ClaimType=tvpSummary.ClaimType,
				--tblSummary.ClaimID=tvpSummary.ClaimID,
				tblSummary.Address=tvpSummary.Address,
				tblSummary.Address1=tvpSummary.Address1,
				tblSummary.Address2=tvpSummary.Address2,
				tblSummary.PostalCodes=tvpSummary.PostalCodes,
				tblSummary.CoRemarks=tvpSummary.CoRemarks,
				tblSummary.ApprovePayment=tvpSummary.ApprovePayment,
				tblSummary.SupervisorRemarks=tvpSummary.SupervisorRemarks,
				tblSummary.ApprovedDate=CASE WHEN ISNULL(tvpSummary.ApprovePayment,'')='Y' THEN tvpSummary.ApprovedDate ELSE NULL END,
				tblSummary.MovementType=tvpSummary.MovementType,
				tblSummary.MandateId=tvpSummary.MandateId,
				tblSummary.ReserveId=tvpSummary.ReserveId,
				tblSummary.DateofNoticetoSafety=tvpSummary.DateofNoticetoSafety,
				tblSummary.InformSafetytoreviewfindings=tvpSummary.InformSafetytoreviewfindings,
				tblSummary.EZLinkCardNo=tvpSummary.EZLinkCardNo,
				tblSummary.ODStatus=tvpSummary.ODStatus,
				tblSummary.RecoverableFromInsurerBI=tvpSummary.RecoverableFromInsurerBI				
		FROM CLM_PaymentSummary AS tblSummary
		INNER JOIN @PaymentSummary AS tvpSummary
		ON tblSummary.PaymentId=tvpSummary.PaymentId
		
		
		UPDATE tblDetails
			SET 
				tblDetails.CmpCode=tvpDetails.CmpCode,
				tblDetails.TotalPaymentDue=tvpDetails.TotalPaymentDue,
				tblDetails.TotalAmountMandate=tvpDetails.TotalAmountMandate,
				tblDetails.Modifiedby=tvpDetails.Modifiedby,
				tblDetails.Modifieddate=tvpDetails.Modifieddate,
				tblDetails.IsActive=tvpDetails.IsActive,
				--tblDetails.AccidentClaimId=tvpDetails.AccidentClaimId,
				tblDetails.ReserveId=tvpDetails.ReserveId,
				tblDetails.MandateId=tvpDetails.MandateId
				--tblDetails.PaymentId=tvpDetails.PaymentId
				--tblDetails.ClaimId=tvpDetails.ClaimId			
		FROM CLM_PaymentDetails AS tblDetails
		INNER JOIN @PaymentDetailsList AS tvpDetails
		ON tblDetails.PaymentDetailID=tvpDetails.PaymentDetailID
		
	END
	
	--When ApprovePayment is 'Y'		
	IF(ISNULL(@ApprovePayment,'')='Y')
	BEGIN
		EXEC Proc_Insert_Claim_Payment  @PaymentId,@AccidentClaimId,@PolicyId,@Payee,
							@AssignedToSuperVisor,@TotalPaymentDue,@TotalAmountMandate,
							@CreatedDate,@PaymentRequestDate,@PaymentDueDate,@CreatedBy,
							@AssignedTo,@ClaimantName,@ClaimType,'Y',@ClaimId,@Address,
							@Address1,@Address2,@PostalCodes,@CoRemarks,@ApprovePayment,@SuperVisorRemarks,
							@ApprovedDate,@MovementType,@MandateId,@ReserveId,@DateofNoticetoSafety,  
 @EZLinkCardNo,  
 @ODStatus,  
 @RecoverableFromInsurerBI
	END		
	
	--Maintain Transaction Audit	
	EXEC Proc_InsertTransactionAuditLog 'CLM_PaymentSummary','PaymentId',@UserId,@ClaimId,@AccidentClaimId,@ActionSummary,@XMLSummary
	EXEC Proc_InsertTransactionAuditLog 'CLM_PaymentDetails','PaymentDetailID',@UserId,@ClaimId,@AccidentClaimId,@ActionDetails,@XMLDetails
	
	END TRY
	BEGIN CATCH
		IF @@TRANCOUNT > 0
        ROLLBACK TRANSACTION;
       
         DECLARE @ErrorProcedure nvarchar(100)
        DECLARE @ErrorMessage nvarchar(MAX)
        DECLARE @entityType nvarchar(100)        		
		DECLARE @SNo [INT]        
        
		SELECT @entityType=[Type] from MNT_TableDesc where TableName='CLM_PaymentSummary'	        
		SELECT @ErrorProcedure=ERROR_PROCEDURE(),@ErrorMessage=ERROR_MESSAGE()		
		select @SNo=SNo from MNT_Users where UserId=@UserId
						
		EXEC Proc_InsertExceptionLog @ErrorMessage,null,@AccidentClaimId,null,null,@ClaimId,null,@entityType,@ErrorProcedure,@ErrorMessage,null,null,null,'MCAS',@SNo,'SqlException'               
	END CATCH
	
	IF @@TRANCOUNT > 0
	BEGIN
		COMMIT TRANSACTION;
		--RETURN 1;
		--SELECT @paymentId;    
	END
	ELSE
	BEGIN
		--RETURN 0;
		--SELECT 0;
		SET @PaymentId=0
	END
	
	RETURN @PaymentId
END

GO


