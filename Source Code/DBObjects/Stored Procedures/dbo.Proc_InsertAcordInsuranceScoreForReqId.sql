IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_InsertAcordInsuranceScoreForReqId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_InsertAcordInsuranceScoreForReqId]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC Proc_InsertAcordInsuranceScoreForReqId
(
	@ReqId NVARCHAR(100),
	@InsScore NVARCHAR(100),
	@InsRecDate NVARCHAR(100),
	@InsReas1 NVARCHAR(100),
	@InsReas2 NVARCHAR(100),
	@insReas3 NVARCHAR(100),
	@InsReas4 NVARCHAR(100),
	@INSU_SCO_ID	Varchar(100) Out     
)
AS
BEGIN
	DECLARE @INSU_SCO_ACEPT_ID INT
	SET @INSU_SCO_ACEPT_ID=0
	SELECT @INSU_SCO_ACEPT_ID = MAX(isnull(INSU_SCO_ACEPT_ID,0)) FROM ACORD_QUOTE_DETAILS
	SET @INSU_SCO_ACEPT_ID=@INSU_SCO_ACEPT_ID+1
	SET @INSU_SCO_ID=convert(nvarchar(100),@INSU_SCO_ACEPT_ID)
	IF EXISTS(SELECT INSURANCE_SVC_RQ FROM ACORD_QUOTE_DETAILS WHERE INSURANCE_SVC_RQ =@ReqId)
		BEGIN
			UPDATE ACORD_QUOTE_DETAILS
				SET CUSTOMER_INSURANCE_SCORE = @InsScore,
					CUSTOMER_INSURANCE_RECEIVED_DATE = @InsRecDate,
					CUSTOMER_REASON_CODE  = @InsReas1,
					CUSTOMER_REASON_CODE2 = @InsReas2,
					CUSTOMER_REASON_CODE3 = @insReas3,
					CUSTOMER_REASON_CODE4 = @InsReas4,
					INSU_SCO_ACEPT_ID	  = @INSU_SCO_ACEPT_ID
				WHERE INSURANCE_SVC_RQ =@ReqId			
		END		
END


GO

