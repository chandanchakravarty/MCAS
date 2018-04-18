IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PROC_DELETEACT_FEE_REVERSAL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[PROC_DELETEACT_FEE_REVERSAL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*-------------------------------------------------------------------------------------------  
Proc Name       : dbo.Proc_DeleteACT_FEE_REVERSAL   
Created by      : Ashwani   
Date            : 19 June,2006  
Purpose       : to delete records from ACT_FEE_REVERSAL    
Revison History :    
Used In         : Wolverine    
----------------------------------------------------------------------------------------------   
Date     Review By          Comments    
------   ------------       ------------------------- ------------------------------------------- */    
CREATE PROCEDURE DBO.PROC_DELETEACT_FEE_REVERSAL  
 (@IDEN_ROW_ID INT)  
AS    
BEGIN    
  DELETE FROM ACT_FEE_REVERSAL   
 WHERE IDEN_ROW_ID = @IDEN_ROW_ID    
END    
          
  
  
  



GO

