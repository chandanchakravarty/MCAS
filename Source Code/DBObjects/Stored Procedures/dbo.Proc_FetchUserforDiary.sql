IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_FetchUserforDiary]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_FetchUserforDiary]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE PROC dbo.Proc_FetchUserforDiary  
(  
 @sqlquery nVARCHAR(2000)  
)  
AS  
  
BEGIN  
EXEC SP_EXECUTESQL @sqlquery  
END  


GO

