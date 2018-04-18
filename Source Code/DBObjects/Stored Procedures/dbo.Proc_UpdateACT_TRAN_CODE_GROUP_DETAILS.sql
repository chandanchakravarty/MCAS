IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateACT_TRAN_CODE_GROUP_DETAILS]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateACT_TRAN_CODE_GROUP_DETAILS]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


--sp_helptext Proc_UpdateACT_TRAN_CODE_GROUP_DETAILS      
/*----------------------------------------------------------    
Proc Name       : dbo.ACT_TRAN_CODE_GROUP_DETAILS    
Created by      : Ajit Chahal Chahal    
Date            : 6/9/2005    
Purpose       :To update records     
Revison History :    
Used In        : Wolverine    
------------------------------------------------------------    
Date     Review By          Comments    
------   ------------       -------------------------*/    
CREATE PROC Dbo.Proc_UpdateACT_TRAN_CODE_GROUP_DETAILS    
(    
@DETAIL_ID     smallint,    
@TRAN_GROUP_ID     smallint,    
@TRAN_ID     smallint,    
@DEF_SEQ     smallint,    
@MODIFIED_BY     int,    
@LAST_UPDATED_DATETIME     datetime    
)    
AS    
BEGIN    
 update ACT_TRAN_CODE_GROUP_DETAILS    
 set     
  --TRAN_GROUP_ID = @TRAN_GROUP_ID,    
  TRAN_ID =@TRAN_ID ,    
  DEF_SEQ = @DEF_SEQ,    
  MODIFIED_BY = @MODIFIED_BY,    
  LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME  
 -- TRAN_GROUP_ID = @TRAN_GROUP_ID      
 where DETAIL_ID = @DETAIL_ID   
END    
    
  



GO

