-- =============================================
-- Script Template
-- =============================================
update CLM_PaymentSummary set MandateRecord = MandateRecordNo from CLM_MandateSummary where CLM_PaymentSummary.MandateId = CLM_MandateSummary.MandateId  and CLM_PaymentSummary.MandateRecord is null