IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_LANG_CULTURE_INFO]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_LANG_CULTURE_INFO]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Charles Gomes
-- Create date: 23-Mar-10
-- Description:	Populates Page for Multilingual Support

--DROP PROC PROC_LANG_CULTURE_INFO
-- =============================================
CREATE PROCEDURE PROC_LANG_CULTURE_INFO	

AS
BEGIN

	SET NOCOUNT ON;
	SELECT LANG_ID,LANG_CODE,LANG_NAME FROM MNT_LANGUAGE_MASTER WITH(NOLOCK)	

END 
GO

