IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetApplicationNumber]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetApplicationNumber]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

/*----------------------------------------------------------            
Proc Name       : Dbo.Proc_GetApplicationNumber            
Created by      : Swarup         
Date                    : 27/04/2007            
Purpose         : To get the application number            
Revison History :            
Used In         : Wolverine            
------------------------------------------------------------            
Date     Review By          Comments            
      
------   ------------       -------------------------*/            
  -- drop Proc dbo.Proc_GetApplicationNumber            
CREATE  PROC dbo.Proc_GetApplicationNumber   
(            
 @CUSTOMER_ID  int,            
 @APP_ID  int,            
 @APP_VERSION_ID int            
)            
AS            
BEGIN            
  
SELECT APP_NUMBER FROM APP_LIST   
WHERE CUSTOMER_ID=@CUSTOMER_ID  
AND APP_ID = @APP_ID  
AND APP_VERSION_ID = @APP_VERSION_ID  
END            
            
      
    
    
  
  
  


GO

