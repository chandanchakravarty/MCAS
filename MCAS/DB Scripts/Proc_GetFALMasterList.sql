/****** Object:  StoredProcedure [dbo].[Proc_GetFALMasterList]    Script Date: 03/24/2015 17:20:01 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetFALMasterList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetFALMasterList]
GO

/****** Object:  StoredProcedure [dbo].[Proc_GetFALMasterList]    Script Date: 03/24/2015 17:20:01 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Proc_GetFALMasterList] 
	@FALCat nvarchar(100),
	@FALName nvarchar(100),
	@Amount nvarchar(100)
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

GO


