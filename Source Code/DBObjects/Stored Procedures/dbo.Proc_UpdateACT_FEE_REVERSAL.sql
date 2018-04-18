IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_UpdateACT_FEE_REVERSAL]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_UpdateACT_FEE_REVERSAL]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*--------------------------------------------------------------------    
Proc Name       : dbo.Proc_UpdateACT_FEE_REVERSAL          
Created by      : Ashwani     
Date            : 19 June 2006         
Purpose        : To update records           
Revison History :          
Used In         : Wolverine          
---------------------------------------------------------------------------------          
Date     Review By          Comments          
------   ------------       ----------------------------------------------------*/          
CREATE Proc dbo.Proc_UpdateACT_FEE_REVERSAL          
(          
 @IDEN_ROW_ID     SMALLINT,          
 @FEES_REVERSE    DECIMAL(18,2),          
 @MODIFIED_BY     INT,          
 @LAST_UPDATED_DATETIME     DATETIME          
)          
AS          
BEGIN          
 UPDATE ACT_FEE_REVERSAL          
 SET           
      
 FEES_REVERSE =@FEES_REVERSE ,    
 MODIFIED_BY = @MODIFIED_BY,          
 LAST_UPDATED_DATETIME = @LAST_UPDATED_DATETIME        
     
 WHERE IDEN_ROW_ID = @IDEN_ROW_ID         
END          
          
        
      
    
  



GO

