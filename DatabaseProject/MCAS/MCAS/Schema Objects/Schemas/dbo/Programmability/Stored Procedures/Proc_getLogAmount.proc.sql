CREATE PROCEDURE [dbo].[Proc_getLogAmount]
 @AccidentId [int],      
 @ClaimID [int],  
 @MandateId [int]    
WITH EXECUTE AS CALLER      
AS      
SET FMTONLY OFF;       
select mndtDtls.MovementMandateSP as LogMedicalExpenses_S from CLM_MandateSummary mdte , CLM_MandateDetails mndtDtls    
where mdte.AccidentClaimId=mndtDtls.AccidentClaimId and mdte.ClaimID=mndtDtls.ClaimID and   
       mdte.MandateId=mndtDtls.MandateId and  
       mndtDtls.AccidentClaimId=@AccidentId and mndtDtls.MandateId=@MandateId  
       and mndtDtls.CmpCode='LME' and mndtDtls.ClaimID=@ClaimID  
       and mdte.ClaimType=3


