CREATE PROCEDURE [dbo].[Proc_GetSupervisorList]
WITH EXECUTE AS CALLER
AS
BEGIN
SET FMTONLY OFF;
select * from MNT_Users where GroupId='90'
END


