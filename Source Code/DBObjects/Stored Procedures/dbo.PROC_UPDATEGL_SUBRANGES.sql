IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_UPDATEGL_SUBRANGES]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_UPDATEGL_SUBRANGES]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------    
PROC NAME       : DBO.INSERTGL_SUBRANGES    
CREATED BY      : AJIT SINGH CHAHAL    
DATE            : 5/12/2005    
PURPOSE     : TO UPDATE  SUB RANGES FOR CHART OF ACCOUNTS.    
REVISON HISTORY :    
USED IN  : WOLVERINE    
------------------------------------------------------------    
DATE     REVIEW BY          COMMENTS    
------   ------------       -------------------------*/    
-- drop proc dbo.PROC_UPDATEGL_SUBRANGES   
CREATE  PROC dbo.PROC_UPDATEGL_SUBRANGES    
(    
@CATEGORY_ID     SMALLINT,    
@PARENT_CATEGORY_ID     SMALLINT,    
@CATEGORY_DESC     VARCHAR(70),    
@RANGE_FROM     DECIMAL(7,2),    
@RANGE_TO     DECIMAL(7,2),    
@IS_ACTIVE     NCHAR(2),    
@MODIFIED_BY     INT,    
@LAST_UPDATED_DATETIME     DATETIME    
)    
AS    
BEGIN    
 /*if exists (select ACCOUNT_ID from ACT_GL_ACCOUNTS     
 where ACC_NUMBER >=(select RANGE_FROM from ACT_GL_ACCOUNT_RANGES WHERE CATEGORY_ID = @CATEGORY_ID)and    
 ACC_NUMBER >=(select RANGE_TO from ACT_GL_ACCOUNT_RANGES WHERE CATEGORY_ID = @CATEGORY_ID)    
 )  
 BEGIN    
  return -2--returning as accounts are defined in range    
 END  
 ELSE   
 BEGIN */  
   UPDATE  ACT_GL_ACCOUNT_RANGES    
    SET    
    CATEGORY_ID=@CATEGORY_ID,    
    PARENT_CATEGORY_ID=@PARENT_CATEGORY_ID,    
    CATEGORY_DESC=@CATEGORY_DESC,    
    RANGE_FROM=@RANGE_FROM,    
    RANGE_TO=@RANGE_TO,    
    IS_ACTIVE=@IS_ACTIVE,    
    MODIFIED_BY=@MODIFIED_BY,    
    LAST_UPDATED_DATETIME=@LAST_UPDATED_DATETIME    
    WHERE CATEGORY_ID = @CATEGORY_ID    
 --END  
END    
  



GO

