CREATE PROCEDURE [dbo].[Proc_GetFALMasterList]
	@FALCat [nvarchar](100),
	@FALName [nvarchar](100),
	@Amount [nvarchar](100)
WITH EXECUTE AS CALLER
AS
SET FMTONLY OFF;  
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT fal.* From MNT_FAL fal
	 WHERE FALAccessCategory like ('%'+@FALCat+'%') and
                               FALLevelName like ('%'+@FALName+'%') and Amount like ('%'+@Amount+'%')
END


