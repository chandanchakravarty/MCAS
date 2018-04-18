/****** Object:  StoredProcedure [dbo].[Proc_GetAppVersionDetails]    Script Date: 03/25/2015 13:25:51 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetAppVersionDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetAppVersionDetails]
GO

/****** Object:  StoredProcedure [dbo].[Proc_GetAppVersionDetails]    Script Date: 03/25/2015 13:25:51 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_GetAppVersionDetails]
As

SET FMTONLY OFF;
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT top 1 * FROM [MNT_APP_RELEASE_MASTER] order by ReleaseID desc
END

GO


