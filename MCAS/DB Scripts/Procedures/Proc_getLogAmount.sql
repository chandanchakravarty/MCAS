IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_getLogAmount]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_getLogAmount]
GO
CREATE Procedure [dbo].[Proc_getLogAmount]
(
 @AccidentId int,
 @ClaimID int
)
As
SET FMTONLY OFF; 
select mdte.LogMedicalExpenses_S from CLM_Mandate mdte
where mdte.AccidentId=@AccidentId and mdte.ClaimID=@ClaimID and
      Modifieddate=(select max(Modifieddate) from CLM_Mandate mdte where mdte.AccidentId=@AccidentId and mdte.ClaimID=@ClaimID)
      and mdte.ClaimType=3


