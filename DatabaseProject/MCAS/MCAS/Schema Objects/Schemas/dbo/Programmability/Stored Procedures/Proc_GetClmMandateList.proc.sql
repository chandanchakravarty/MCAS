CREATE PROCEDURE [dbo].[Proc_GetClmMandateList]
	@AccidentId [nvarchar](100)
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;  
 SELECT  MandateId,row_number()over(partition by mandate.ClaimType order by MandateId) RecordNumber,  
        (select ClaimRecordNo from CLM_Claims where ClaimID=mandate.ClaimID) as ClaimRecordNo,  
         PolicyId,AccidentId,ClaimID,ClaimantName,ClaimType , lk.Description as ClaimTypeDesc,  
         lk.Lookupdesc  as ClaimTypeCode ,ApproveRecommedations=case when ApproveRecommedations='Y' Then 'Yes'  
                                                                     when ApproveRecommedations='N'  Then 'No'  
                                                                End      
from    CLM_Mandate mandate (nolock)    
        left join MNT_Lookups lk (nolock) on lk.lookupvalue=mandate.ClaimType   
                  and lk.Category ='ClaimType'     
where AccidentId=@AccidentId and MandateId in(
                                     select CLM_Mandate.MandateId from 
                                     ( 
                                      select MAX(Createddate)Createddate,ClaimType,AccidentId,PolicyId,ClaimID
                                      from CLM_Mandate group by ClaimType,AccidentId,PolicyId,ClaimID
                                     )temp inner join CLM_Mandate on temp.Createddate=CLM_Mandate.Createddate
                                      and temp.AccidentId=CLM_Mandate.AccidentId and temp.ClaimType=CLM_Mandate.ClaimType
                                      and temp.PolicyId=CLM_Mandate.PolicyId and temp.ClaimID=CLM_Mandate.ClaimID
                                    ) order by ClaimType



