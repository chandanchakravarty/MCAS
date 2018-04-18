CREATE PROCEDURE [dbo].[Proc_GetLouMasterList]
	@p_LouRate [nvarchar](100),
	@p_EffectiveDate [nvarchar](100)
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;  

select Lou.* from MNT_LOU_MASTER Lou 
where Lou.LouRate like ('%'+@p_LouRate+'%') and convert(varchar, Lou.EffectiveDate, 103) like ('%'+@p_EffectiveDate+'%')order by Lou.EffectiveDate asc


