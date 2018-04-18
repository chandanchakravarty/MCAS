CREATE PROCEDURE [dbo].[Proc_GetClaimantName]
	@AccidentClaimId [int]
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;  
 Select ClaimID,case when ClaimType=1 then 'OD'
                               when ClaimType=2 then 'PD'
                               when ClaimType=3 then 'BI' 
                               End
                         +substring(ClaimRecordNo,CHARINDEX('-',ClaimRecordNo)+1,len(ClaimRecordNo))+'/'+ClaimantName as ClaimantName   
    From CLM_Claims
    where ClaimantName is not null and AccidentClaimId=@AccidentClaimId order by ClaimType


