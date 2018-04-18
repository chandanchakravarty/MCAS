IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetMnt_HopitalList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetMnt_HopitalList]
GO

CREATE Proc [dbo].[Proc_GetMnt_HopitalList]
AS
BEGIN
SET FMTONLY OFF;
select * from MNT_Hospital order by HospitalName asc
END


GO


