CREATE PROCEDURE [dbo].[Proc_GetCLM_ClaimNoTemp]
	@ClaimType [nvarchar](100)
WITH EXECUTE AS CALLER
AS
BEGIN  
SET FMTONLY OFF;  
select top 1 IDENT_CURRENT('clm_claim') as ClaimID from clm_claim  where ClaimType=@ClaimType order by Createddate desc  
END


