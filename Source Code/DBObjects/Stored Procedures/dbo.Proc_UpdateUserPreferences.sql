IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateUserPreferences]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateUserPreferences]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE   PROCEDURE Proc_UpdateUserPreferences  
(  
     @USERID int,  
     @MODIFIED_BY int,  
     @LAST_UPDATED_DATETIME datetime,  
     @USER_COLOR_SCHEME nvarchar,  
     @GRID_SIZE int,
     @Lang_ID int   
)  
AS  
BEGIN  
UPDATE MNT_USER_LIST  
SET MODIFIED_BY=@MODIFIED_BY,  
LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME,  
USER_COLOR_SCHEME=@USER_COLOR_SCHEME,  
USER_IMAGE_FOLDER='Images'+ cast(@USER_COLOR_SCHEME as nvarchar),  
GRID_SIZE=@GRID_SIZE,LANG_ID=@Lang_ID
WHERE USER_ID=@USERID  
END  
GO

