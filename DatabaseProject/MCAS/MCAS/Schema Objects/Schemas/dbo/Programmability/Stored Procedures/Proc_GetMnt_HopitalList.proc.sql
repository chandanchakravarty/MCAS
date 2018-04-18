CREATE PROCEDURE [dbo].[Proc_GetMnt_HopitalList]
WITH EXECUTE AS CALLER
AS
BEGIN
SET FMTONLY OFF;
select * from MNT_Hospital Where Status != 0 and (EffectiveTo is null OR Convert(Date,EffectiveTo,103) >= Convert(Date,GETDATE(),103)) order by HospitalName asc 
END


