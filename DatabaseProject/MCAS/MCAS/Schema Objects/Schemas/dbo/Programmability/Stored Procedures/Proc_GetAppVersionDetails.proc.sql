CREATE PROCEDURE [dbo].[Proc_GetAppVersionDetails]
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT top 1 * FROM [MNT_APP_RELEASE_MASTER] order by ReleaseID desc
END


