IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Proc_GetApplicationStatus]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Proc_GetApplicationStatus]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


/*----------------------------------------------------------        
Proc Name       : dbo.Proc_GetApplicationStatus
Created by      : Vijay Arora    
Date            : 09-02-206
Purpose         : It will status of the Application.
Revison History :        
Used In         : Wolverine          
------------------------------------------------------------        
Date     Review By          Comments        
------   ------------       -------------------------*/        
CREATE PROC Proc_GetApplicationStatus    
@CUSTOMER_ID INT,    
@APP_ID INT,    
@APP_VERSION_ID SMALLINT  
AS    
BEGIN    
SELECT IS_ACTIVE FROM APP_LIST WHERE CUSTOMER_ID = @CUSTOMER_ID  AND APP_ID = @APP_ID  
AND APP_VERSION_ID = @APP_VERSION_ID  
END    
  



GO

