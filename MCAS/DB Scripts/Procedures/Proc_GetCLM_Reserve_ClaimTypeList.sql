IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetCLM_Reserve_ClaimTypeList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetCLM_Reserve_ClaimTypeList]
GO

CREATE PROCEDURE [dbo].[Proc_GetCLM_Reserve_ClaimTypeList]
@w_ClaimType varchar(50),
@w_AccidentId varchar(50)
AS
SET FMTONLY OFF;
BEGIN
select row_number() over (order by @@rowcount) As SerialNo,row_number() over(partition by a.[ClaimantName] order by a.[ClaimantName]) AS [SNO],a.[ClaimantName],a.[Createdby],a.[Modifiedby],a.[Modifieddate],a.[Createddate],a.[Total_I],a.[Total_R],a.[Total_O],b.[ClaimRecordNo]
 from CLM_Reserve a LEFT JOIN CLM_Claims b ON a.ClaimID=b.ClaimID where a.ClaimType=@w_ClaimType AND a.AccidentId=@w_AccidentId order by a.ClaimantName
END
GO