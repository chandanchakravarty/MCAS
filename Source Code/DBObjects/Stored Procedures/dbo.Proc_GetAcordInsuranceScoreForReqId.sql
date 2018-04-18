IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAcordInsuranceScoreForReqId]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAcordInsuranceScoreForReqId]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC Proc_GetAcordInsuranceScoreForReqId
(
	@ReqId NVARCHAR(100)
)
AS
	BEGIN
		SELECT ISNULL(CUSTOMER_INSURANCE_SCORE,'-1') AS ReqInsuranceScore, CUSTOMER_INSURANCE_RECEIVED_DATE,CUSTOMER_REASON_CODE,
		CUSTOMER_REASON_CODE2,CUSTOMER_REASON_CODE3,CUSTOMER_REASON_CODE4 FROM ACORD_QUOTE_DETAILS WHERE convert(nvarchar(100),isnull(INSU_SCO_ACEPT_ID,0)) =@ReqId
	END


GO

