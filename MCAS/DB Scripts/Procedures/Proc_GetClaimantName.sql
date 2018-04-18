IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetClaimantName]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetClaimantName]
GO
Create Procedure [dbo].[Proc_GetClaimantName] 
(
 @AccidentClaimId int
) 
As  
SET FMTONLY OFF;  
 Select ClaimID,case when ClaimType=1 then 'OD'
                               when ClaimType=2 then 'PD'
                               when ClaimType=3 then 'BI' 
                               End
                         +substring(ClaimRecordNo,CHARINDEX('-',ClaimRecordNo)+1,len(ClaimRecordNo))+'/'+replace(ClaimantName,' ','')as ClaimantName   
    From CLM_Claims
    where ClaimantName is not null and AccidentClaimId=@AccidentClaimId order by ClaimType 