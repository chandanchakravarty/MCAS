IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetLanguageList]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetLanguageList]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE PROCEDURE [dbo].[Proc_GetLanguageList](
@LANG_ID INT=1)
As  
BEGIN  
IF (@LANG_ID =2)
 BEGIN
  select * from MNT_LANGUAGE_MASTER_MULTILINGUAL  
 END
ELSE 
 BEGIN
  SELECT * FROM MNT_LANGUAGE_MASTER
 END
END

GO

