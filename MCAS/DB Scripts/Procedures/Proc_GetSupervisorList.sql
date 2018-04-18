IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetSupervisorList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetSupervisorList]
GO

CREATE Proc [dbo].[Proc_GetSupervisorList]
AS
BEGIN
SET FMTONLY OFF;
select * from MNT_Users where GroupId='90'
END


GO


